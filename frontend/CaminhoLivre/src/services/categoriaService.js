import api from '../api/axios';

export const categoriaService = {

    async listar() {
        try {
            const response = await api.get('/api/v1/Categoria');
            return response.data.itens;
        } catch (error) {
            console.error('Error fetching categories:', error);
            throw error;
        }
    },

    async criar(categoria) {
        try {
            const response = await api.post('/api/v1/Categoria', categoria);
            return response.data;
        } catch (error) {
            console.error('Error criar categoria:', error);
            throw error;
        }
    },
    async atualizar(id, categoria) {
        try {
            const response = await api.put(`/api/v1/Categoria/${id}`, categoria);
            return response.data;
        } catch (error) {
            console.error('Error atualizar categoria:', error);
            throw error;
        }
    }
};