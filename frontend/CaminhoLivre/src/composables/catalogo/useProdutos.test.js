import { describe, it, expect, vi, beforeEach } from 'vitest'
import { useProdutos } from './useProdutos'
// Importamos o service usando caminhos relativos para evitar ruídos no Vitest
import { produtoService } from '../../services/catalogo/produtoService'

// 1. Criamos o mock do produtoService com os métodos necessários
vi.mock('../../services/catalogo/produtoService', () => {
  return {
    produtoService: {
      listar: vi.fn(),
      criar: vi.fn(),
      atualizar: vi.fn()
    }
  }
})

describe('Composable: useProdutos', () => {

  beforeEach(() => {
    vi.clearAllMocks()
  })

  // ==========================================
  // TESTES: Estado Inicial e Carregamento
  // ==========================================
  it('Deve iniciar com os estados padrão reativos corretos', () => {
    const { produtos, loading, salvando, error, produtoSelecionado } = useProdutos()

    expect(produtos.value).toEqual([])
    expect(loading.value).toBe(false)
    expect(salvando.value).toBe(false)
    expect(error.value).toBeNull()
    expect(produtoSelecionado.value.nome).toBe('')
    expect(produtoSelecionado.value.precoVenda).toBe(0)
  })

  it('Deve gerenciar o loading e armazenar a lista de produtos obtida com sucesso', async () => {
    const mockLista = [
      { id: 1, nome: 'Palete PBR', precoVenda: 120 },
      { id: 2, nome: 'Caixa Plástica', precoVenda: 45 }
    ]
    vi.mocked(produtoService.listar).mockResolvedValue(mockLista)

    const { produtos, loading, carregarProdutos } = useProdutos()

    // Dispara sem await para verificar o loading ativo durante a requisição
    const promessa = carregarProdutos()
    expect(loading.value).toBe(true)

    await promessa

    expect(loading.value).toBe(false)
    expect(produtos.value).toEqual(mockLista)
  })

  it('Deve capturar o erro com console.error se o carregamento falhar', async () => {
    const erroSimulado = new Error('Falha de conexão')
    vi.mocked(produtoService.listar).mockRejectedValue(erroSimulado)
    
    // Intercepta o console.error para não poluir o terminal do Vitest
    const spyConsoleError = vi.spyOn(console, 'error').mockImplementation(() => {})

    const { loading, carregarProdutos } = useProdutos()

    await carregarProdutos()

    expect(loading.value).toBe(false)
    expect(spyConsoleError).toHaveBeenCalledWith('Error fetching produtos:', erroSimulado)
    
    spyConsoleError.mockRestore()
  })

  // ==========================================
  // TESTES: Fluxo de Salvar (Criação e Edição)
  // ==========================================
  it('Não deve chamar a API se tentar salvar um produto sem nome', async () => {
    const spyConsoleWarn = vi.spyOn(console, 'warn').mockImplementation(() => {})
    const { produtoSelecionado, salvarProduto } = useProdutos()

    produtoSelecionado.value.nome = '' // Nome inválido

    await salvarProduto()

    expect(produtoService.criar).not.toHaveBeenCalled()
    expect(spyConsoleWarn).toHaveBeenCalledWith('Nome é obrigatório.')
    
    spyConsoleWarn.mockRestore()
  })

  it('Deve mapear o payload corretamente e chamar CRIAR quando o id for nulo', async () => {
    vi.mocked(produtoService.criar).mockResolvedValue({})
    vi.mocked(produtoService.listar).mockResolvedValue([]) // Mock do recarregamento automático

    const { produtoSelecionado, salvarProduto } = useProdutos()

    // Montando o cenário simulando a tela de cadastro
    produtoSelecionado.value.id = null
    produtoSelecionado.value.nome = 'Palete Metálico Revestido'
    produtoSelecionado.value.sku = 'PAL-MET-001'
    produtoSelecionado.value.precoVenda = 250.50
    produtoSelecionado.value.precoCusto = 180.00
    produtoSelecionado.value.categoriaId = 3
    produtoSelecionado.value.ativo = 'Ativo'

    await salvarProduto()

    // Garante que transformou o id nulo em 0 e mapeou o booleano do status
    expect(produtoService.criar).toHaveBeenCalledWith({
      id: 0,
      nome: 'Palete Metálico Revestido',
      descricao: '',
      sku: 'PAL-MET-001',
      precoVenda: 250.50,
      precoCusto: 180.00,
      categoriaId: 3,
      ativo: true
    })

    // Garante que o formulário foi resetado após salvar com sucesso
    expect(produtoSelecionado.value.nome).toBe('')
    expect(produtoSelecionado.value.id).toBeNull()
  })

  it('Deve chamar o método ATUALIZAR quando o produto contiver um ID válido', async () => {
    vi.mocked(produtoService.atualizar).mockResolvedValue({})
    vi.mocked(produtoService.listar).mockResolvedValue([])

    const { produtoSelecionado, salvarProduto } = useProdutos()

    produtoSelecionado.value.id = 42
    produtoSelecionado.value.nome = 'Caixa Plástica Modificada'
    produtoSelecionado.value.ativo = 'Inativo' // Deve virar false no payload

    await salvarProduto()

    expect(produtoService.atualizar).toHaveBeenCalledWith(42, {
      id: 42,
      nome: 'Caixa Plástica Modificada',
      descricao: '',
      sku: '',
      precoVenda: 0,
      precoCusto: 0,
      categoriaId: null,
      ativo: false
    })
  })
})