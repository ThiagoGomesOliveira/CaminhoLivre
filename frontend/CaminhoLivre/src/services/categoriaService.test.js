import { describe, it, expect, vi, beforeEach } from 'vitest'
import { categoriaService } from './categoriaService'

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

describe('Service: categoriaService', () => {
  
  beforeEach(() => {
    vi.clearAllMocks()
  })    

    // ==========================================
    // TESTES DO MÉTODO: listar()
    // ==========================================   

    it('Deve retornar a lista de itens quando o GET responder com sucesso', async () => {
        // --- ARRANGE ---
        const mockResponse = {
            data: { 
                itens: [
                    { id: 1, nome: 'Embalagem' },
                    { id: 2, nome: 'Equipamento' }
                ] 
            }
        }   
        apiMockado.get.mockResolvedValue(mockResponse)
        // --- ACT ---
        const resultado = await categoriaService.listar()
        // --- ASSERT ---
        expect(apiMockado.get).toHaveBeenCalledWith('/api/v1/Categoria')
        expect(resultado).toBeDefined()
        expect(resultado.length).toBe(2)
        expect(resultado[0].nome).toBe('Embalagem')
    })

    // ==========================================
    // TESTES DO MÉTODO: criar()
    // ==========================================   
    it('Deve enviar os dados corretos e retornar a categoria criada com sucesso', async () => {
        // --- ARRANGE ---

        const novaCategoriaParaCriar = {
            nome: 'Embalagem Plástica'
        }

        const mockResponse = {  
            data: {
                id: 3,
                nome: 'Embalagem Plástica'
            }
        }
        apiMockado.post.mockResolvedValue(mockResponse)

        // --- ACT ---
        const resultado = await categoriaService.criar(novaCategoriaParaCriar)  
        // --- ASSERT ---
        expect(apiMockado.post).toHaveBeenCalledWith('/api/v1/Categoria', novaCategoriaParaCriar)
        expect(resultado).toBeDefined()
        expect(resultado.id).toBe(3)
        expect(resultado.nome).toBe('Embalagem Plástica')
    })

    it('Deve lançar um erro (throw) e fazer o console.error se a API falhar no cadastro', async () => {
        // --- ARRANGE ---
        const novaCategoriaParaCriar = {
            nome: 'Embalagem Plástica'
        }
        const erroSimulado = new Error('Erro ao criar categoria')
        apiMockado.post.mockRejectedValue(erroSimulado) 
        const consoleErrorSpy = vi.spyOn(console, 'error').mockImplementation(() => {})

        // --- ACT & ASSERT ---
        await expect(categoriaService.criar(novaCategoriaParaCriar)).rejects.toThrow('Erro ao criar categoria')
        expect(consoleErrorSpy).toHaveBeenCalledWith('Error criar categoria:', erroSimulado)
        consoleErrorSpy.mockRestore()   
    })

    // ==========================================
    // TESTES DO MÉTODO: atualizar()
    // ==========================================   

    it('Deve enviar os dados corretos e retornar a categoria atualizada com sucesso', async () => {
        // --- ARRANGE ---
        const idCategoriaParaAtualizar = 2
        const categoriaAtualizada = {
            nome: 'Equipamento Industrial'
        }       
        const mockResponse = {
            data: { 
                id: 2,
                nome: 'Equipamento Industrial'
            }
        }
        apiMockado.put.mockResolvedValue(mockResponse)

        // --- ACT ---
        const resultado = await categoriaService.atualizar(idCategoriaParaAtualizar, categoriaAtualizada)

        // --- ASSERT ---
        expect(apiMockado.put).toHaveBeenCalledWith(`/api/v1/Categoria/${idCategoriaParaAtualizar}`, categoriaAtualizada)
        expect(resultado).toBeDefined()
        expect(resultado.id).toBe(2)
        expect(resultado.nome).toBe('Equipamento Industrial')
    })

    it('Deve lançar um erro (throw) e fazer o console.error se a API falhar na atualização', async () => {
        // --- ARRANGE ---
        const idCategoriaParaAtualizar = 2
        const categoriaComErro = { nome: '' }
        const erroSimulado = new Error('Erro interno do servidor (500)')

        apiMockado.put.mockRejectedValue(erroSimulado)

        const spyConsoleError = vi.spyOn(console, 'error').mockImplementation(() => {})

         // --- ACT & ASSERT ---
        await expect(categoriaService.atualizar(idCategoriaParaAtualizar, categoriaComErro)).rejects.toThrow('Erro interno do servidor (500)')

        expect(spyConsoleError).toHaveBeenCalledWith('Error atualizar categoria:', erroSimulado)

        spyConsoleError.mockRestore()
    })
})
