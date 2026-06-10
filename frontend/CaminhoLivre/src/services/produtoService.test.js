import { describe, it, expect, vi, beforeEach } from 'vitest'
import { produtoService } from './produtoService'

// 1. Corrigimos o mock para retornar um export 'default' que simula o Axios
vi.mock('../api/axios', () => {
  return {
    default: {
      get: vi.fn(),
      post: vi.fn(),
      put: vi.fn()
    }
  }
})

// 2. Importamos o mock usando a mesma sintaxe padrão (sem chaves)
import apiMockado from '../api/axios'

describe('Service: produtoService', () => {
  
  beforeEach(() => {
    vi.clearAllMocks()
  })

  it('Deve retornar a lista de itens quando o GET responder com sucesso', async () => {
    // --- ARRANGE ---
    const mockResponse = {
      data: {
        itens: [
          { id: 1, nome: 'Palete PBR' },
          { id: 2, nome: 'Caixa Plástica' }
        ]
      }
    }

    // Injeta o valor de sucesso no método get falso usando a variável que importamos
    apiMockado.get.mockResolvedValue(mockResponse)

    // --- ACT ---
    const resultado = await produtoService.listar()

    // --- ASSERT ---
    // Verifica se o GET foi chamado na rota certinha
    expect(apiMockado.get).toHaveBeenCalledWith('/api/v1/Produto')
    
    // Verifica se o retorno do método é exatamente o array filtrado
    expect(resultado).toBeDefined()
    expect(resultado.length).toBe(2)
    expect(resultado[0].nome).toBe('Palete PBR')
  })

  // ==========================================
  // TESTES DO MÉTODO: criar()
  // ==========================================

  it('Deve enviar os dados corretos e retornar o produto criado com sucesso', async () => {
    // --- ARRANGE ---
    const novoProdutoParaCriar = {
      nome: 'Palete PBR Novo',
      preco: 150.00,
      categoriaId: 5
    }

    const mockResponseDoBackend = {
      data: {
        id: 99,
        nome: 'Palete PBR Novo',
        preco: 150.00,
        categoriaId: 5
      }
    }

    apiMockado.post.mockResolvedValue(mockResponseDoBackend)

    // --- ACT ---
    const resultado = await produtoService.criar(novoProdutoParaCriar)

    // --- ASSERT ---
    // Garante que o front enviou os dados certos para a rota certa
    expect(apiMockado.post).toHaveBeenCalledWith('/api/v1/Produto', novoProdutoParaCriar)
    
    // Garante que o service retornou o "response.data" que veio do servidor
    expect(resultado).toBeDefined()
    expect(resultado.id).toBe(99)
    expect(resultado.nome).toBe('Palete PBR Novo')
  })

  it('Deve lançar um erro (throw) e fazer o console.error se a API falhar no cadastro', async () => {
    // --- ARRANGE ---
    const produtoComErro = { nome: '' }
    const erroSimulado = new Error('Erro interno do servidor (500)')

    // Forçamos o POST a rejeitar a requisição simulando a falha do servidor
    apiMockado.post.mockRejectedValue(erroSimulado)

    // Interceptamos o console.error para não poluir o terminal do teste
    const spyConsoleError = vi.spyOn(console, 'error').mockImplementation(() => {})

    // --- ACT & ASSERT ---
    // Verifica se o método realmente joga o erro para frente (throw error)
    await expect(produtoService.criar(produtoComErro)).rejects.toThrow('Erro interno do servidor (500)')

    // Verifica se o console.error foi disparado com a mensagem configurada no catch
    expect(spyConsoleError).toHaveBeenCalledWith('Error criar produto:', erroSimulado)

    // Limpa a espionagem do console
    spyConsoleError.mockRestore()
  })

  // ==========================================
  // TESTES DO MÉTODO: Atualizar()
  // ==========================================

  it('Deve enviar os dados corretos e retornar o produto atualizado com sucesso', async () => {

      // --- ARRANGE ---

      const idProdutoParaAtualizar = 99
      const produtoAtualizado = {
        nome: 'Palete PBR Atualizado',
        preco: 175.00,
        categoriaId: 5
      }

      const mockResponseDoBackend = {
        data: {
          id: 99,
          nome: 'Palete PBR Atualizado',
          preco: 175.00,
          categoriaId: 5
        }
      }
      apiMockado.put.mockResolvedValue(mockResponseDoBackend)

      // --- ACT ---
      const resultado = await produtoService.atualizar(idProdutoParaAtualizar, produtoAtualizado)

      // --- ASSERT ---
      // Garante que o front enviou os dados certos para a rota certa
      expect(apiMockado.put).toHaveBeenCalledWith(`/api/v1/Produto/${idProdutoParaAtualizar}`, produtoAtualizado)
      
      // Garante que o service retornou o "response.data" que veio do servidor
      expect(resultado).toBeDefined()
      expect(resultado.id).toBe(99)
      expect(resultado.nome).toBe('Palete PBR Atualizado')
  })

  it('Deve lançar um erro (throw) e fazer o console.error se a API falhar na atualização', async () => {

      // --- ARRANGE ---
      const idProdutoParaAtualizar = 99
      const produtoComErro = { nome: '' }
      const erroSimulado = new Error('Erro interno do servidor (500)')    

      // Forçamos o PUT a rejeitar a requisição simulando a falha do servidor
      apiMockado.put.mockRejectedValue(erroSimulado)  

      // Interceptamos o console.error para não poluir o terminal do teste
      const spyConsoleError = vi.spyOn(console, 'error').mockImplementation(() => {})

      // --- ACT & ASSERT ---
      // Verifica se o método realmente joga o erro para frente (throw error)
      await expect(produtoService.atualizar(idProdutoParaAtualizar, produtoComErro)).rejects.toThrow('Erro interno do servidor (500)')

      // Verifica se o console.error foi disparado com a mensagem configurada no catch
      expect(spyConsoleError).toHaveBeenCalledWith('Error atualizacao produto:', erroSimulado)

      // Limpa a espionagem do console
      spyConsoleError.mockRestore()
  })
})