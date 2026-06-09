import { describe, it, expect, vi, beforeEach } from 'vitest'
import { produtoService } from './produtoService'

// 1. Corrigimos o mock para retornar um export 'default' que simula o Axios
vi.mock('../api/axios', () => {
  return {
    default: {
      get: vi.fn()
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
})