import {produtoService} from '@/services/produtoService';
import { ref } from 'vue';

export function useProdutos() {
    const produtos = ref([]);
    const loading = ref(false);

    const listarProdutos = async () => {
        loading.value = true;
        try {
            produtos.value = await produtoService.listar();
        } catch (error) {
            console.error('Error fetching produtos:', error);
        } finally {
            loading.value = false;
        }
    };

    const criarProduto = async (produto) => {
        try {
            const novoProduto = await produtoService.criar(produto);
            produtos.value.push(novoProduto);
        } catch (error) {
            console.error('Error creating produto:', error);
        }
    };

    const atualizarProduto = async (id, produto) => {
        try {
            const produtoAtualizado = await produtoService.atualizar(id, produto);
            const index = produtos.value.findIndex(p => p.id === id);
            if (index !== -1) {
                produtos.value[index] = produtoAtualizado;
            }
        } catch (error) {
            console.error('Error updating produto:', error);
        }
    };

    return {
        produtos,
        loading,
        listarProdutos,
        criarProduto,
        atualizarProduto
    };
}