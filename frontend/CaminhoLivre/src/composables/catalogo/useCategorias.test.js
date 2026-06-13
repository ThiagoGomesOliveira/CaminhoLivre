import { describe, it, expect, vi, beforeEach } from 'vitest'
import { useCategorias } from './useCategorias'
// Importamos o service real para poder manipular seus mocks mais abaixo
import { categoriaService } from '../../services/catalogo/categoriaService'

// 1. Criamos o mock completo do CategoriaService
vi.mock('../../services/catalogo/categoriaService', () => {
  return {
    categoriaService: {
      listar: vi.fn(),
      criar: vi.fn(),
      atualizar: vi.fn()
    }
  }
})

describe('Composable: useCategorias', () => {

  beforeEach(() => {
    vi.clearAllMocks()
  })

  // ==========================================
  // TESTES: Estado Inicial e Carregamento
  // ==========================================
  it('Deve iniciar com os estados padrão corretos', () => {
    const { categorias, error, loading, salvando, categoriaSelecionada } = useCategorias()

    expect(categorias.value).toEqual([])
    expect(error.value).toBeNull()
    expect(loading.value).toBe(false)
    expect(salvando.value).toBe(false)
    expect(categoriaSelecionada.value.nome).toBe('')
  })

  it('Deve gerenciar o loading e preencher as categorias ao carregar com sucesso', async () => {
    const mockLista = [{ id: 1, nome: 'Madeira' }, { id: 2, nome: 'Plástico' }]
    vi.mocked(categoriaService.listar).mockResolvedValue(mockLista)

    const { categorias, loading, carregarCategorias, error } = useCategorias()

    // Dispara a função (sem o await imediato para testar o meio do caminho)
    const promessa = carregarCategorias()

    expect(loading.value).toBe(true) // O loading deve ativar imediatamente
    expect(error.value).toBeNull()

    await promessa

    expect(loading.value).toBe(false) // Terminou, volta para false
    expect(categorias.value).toEqual(mockLista) // Salvou a lista da API
  })

  it('Deve injetar mensagem de erro caso a API falhe no carregamento', async () => {
    vi.mocked(categoriaService.listar).mockRejectedValue(new Error('Conexão recusada'))

    const { categorias, loading, carregarCategorias, error } = useCategorias()

    await carregarCategorias()

    expect(loading.value).toBe(false)
    expect(categorias.value).toEqual([]) // Continua vazio
    expect(error.value).toBe('Erro ao carregar categorias: Conexão recusada')
  })

  // ==========================================
  // TESTES: Fluxo de Salvar (Criação e Edição)
  // ==========================================
  it('Não deve salvar nem chamar a API se o nome da categoria estiver vazio', async () => {
    const spyConsoleWarn = vi.spyOn(console, 'warn').mockImplementation(() => {})
    const { categoriaSelecionada, salvarCategoria } = useCategorias()

    categoriaSelecionada.value.nome = '   ' // Apenas espaços em branco

    await salvarCategoria()

    // Garante que bloqueou na validação e não chamou nenhum método da API
    expect(categoriaService.criar).not.toHaveBeenCalled()
    expect(spyConsoleWarn).toHaveBeenCalledWith('Nome é obrigatório.')
    
    spyConsoleWarn.mockRestore()
  })

  it('Deve chamar o método CRIAR da API quando o id for nulo e limpar o formulário depois', async () => {
    vi.mocked(categoriaService.criar).mockResolvedValue({})
    vi.mocked(categoriaService.listar).mockResolvedValue([]) // Mock do recarregamento interno

    const { categoriaSelecionada, salvarCategoria } = useCategorias()

    // Preenche o formulário simulando a digitação do usuário
    categoriaSelecionada.value.id = null
    categoriaSelecionada.value.nome = 'Paletes Metálicos'
    categoriaSelecionada.value.descricao = 'Estruturas de ferro'
    categoriaSelecionada.value.ativo = 'Ativo'

    await salvarCategoria()

    // Confere se o payload foi montado corretamente (id convertido para 0 e ativo para true)
    expect(categoriaService.criar).toHaveBeenCalledWith({
      id: 0,
      nome: 'Paletes Metálicos',
      descricao: 'Estruturas de ferro',
      ativo: true
    })

    // Garante que o form foi limpo automaticamente após o sucesso
    expect(categoriaSelecionada.value.nome).toBe('')
    expect(categoriaSelecionada.value.id).toBeNull()
  })

  it('Deve chamar o método ATUALIZAR da API quando a categoria já possuir um ID válido', async () => {
    vi.mocked(categoriaService.atualizar).mockResolvedValue({})
    vi.mocked(categoriaService.listar).mockResolvedValue([])

    const { categoriaSelecionada, salvarCategoria } = useCategorias()

    categoriaSelecionada.value.id = 5
    categoriaSelecionada.value.nome = 'Madeiras Reforçadas'
    categoriaSelecionada.value.ativo = 'Inativo' // Vai virar false no payload

    await salvarCategoria()

    // Passa o ID no primeiro argumento e o payload no segundo conforme o seu service exige
    expect(categoriaService.atualizar).toHaveBeenCalledWith(5, {
      id: 5,
      nome: 'Madeiras Reforçadas',
      descricao: '',
      ativo: false
    })
  })
})