import {produtoService} from '@/services/catalogo/produtoService';
import { ref } from 'vue';

export function useProdutos() {
    const produtos = ref([]);
    const loading = ref(false);
    const salvando = ref(false);
    const error = ref(null);   


    const produtoSelecionado = ref({
        id: null,   
        nome: '',
        descricao: '',
        sku: '',
        precoVenda: 0,
        precoCusto: 0,
        categoriaId: null,
        ativo: 'Ativo'
    });

    const carregarProdutos = async () => {
        loading.value = true;
        try {
            produtos.value = await produtoService.listar();
        } catch (error) {
            console.error('Error fetching produtos:', error);
        } finally {
            loading.value = false;
        }
    };

    const salvarProduto = async () => {

        if (!produtoSelecionado.value.nome || !produtoSelecionado.value.nome.trim()) {
            console.warn('Nome é obrigatório.');
            return;
        }
        
        const payload = {
            id: produtoSelecionado.value.id || 0, // Adicionado para garantir o ID no PUT/POST
            nome: produtoSelecionado.value.nome, 
            descricao: produtoSelecionado.value.descricao,
            sku: produtoSelecionado.value.sku,
            precoVenda: produtoSelecionado.value.precoVenda,
            precoCusto: produtoSelecionado.value.precoCusto,
            categoriaId: produtoSelecionado.value.categoriaId,
            ativo: produtoSelecionado.value.ativo === 'Ativo'
        };

        salvando.value = true;
        try {

            if (payload.id) {
                await produtoService.atualizar(payload.id, payload);
            } else {
                 await produtoService.criar(payload);
            }
           
            await carregarProdutos(); 
            limparFormulario();


        } catch (error) {
            console.error('Error saving produto:', error);
        }
        finally {
            salvando.value = false;
        }
    }

    const limparFormulario = () => {
        produtoSelecionado.value = {
            id: null,   
            nome: '',
            descricao: '',
            sku: '',
            precoVenda: 0,
            precoCusto: 0,
            categoriaId: null,
            ativo: ''
        };
    };  

    return {
        produtos,
        error,
        loading,
        salvando,
        produtoSelecionado,
        carregarProdutos,
        salvarProduto,
        limparFormulario
    };
}