import api from '../api/axios';

export const produtoService = {

    async listar() {
        try {
            const response = await api.get('/api/v1/Produto');
            return response.data.itens;
        } catch (error) {
            console.error('Error obter produtos:', error);
            throw error;
        }
    },

    async criar(produto) {
        try {
            const response = await api.post('/api/v1/Produto', produto);
            return response.data;
        } catch (error) {
            console.error('Error criação produto:', error);
            throw error;
        }
    },
    
    async atualizar(id, produto) {
        try {
            const response = await api.put(`/api/v1/Produto/${id}`, produto);
            return response.data;
        } catch (error) {
            console.error('Error atualizacao produto:', error);
            throw error;
        }
    }
};