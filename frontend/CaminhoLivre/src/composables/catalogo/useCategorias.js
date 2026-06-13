import { ref } from 'vue'
import { categoriaService } from '@/services/catalogo/categoriaService'

export function useCategorias() {
    // 1. ESTADOS REATIVOS SEMPRE NO TOPO (Evita o erro de undefined)
    const categorias = ref([]);
    const error = ref(null);   
    const loading = ref(false);
    const salvando = ref(false);

    const categoriaSelecionada = ref({
        id: null,   
        nome: '',
        descricao: '',
        ativo: 'Ativo'
    });

    // 2. FUNÇÃO: CARREGAR
    const carregarCategorias = async () => {
        loading.value = true;
        error.value = null;
        try {
            categorias.value = await categoriaService.listar();
        } catch (err) {
            error.value = 'Erro ao carregar categorias: ' + err.message;
        } finally {
            loading.value = false;
        }   
    };

    // 3. FUNÇÃO: SALVAR
    const salvarCategoria = async () => {
        // Correção da validação: Só sai se NÃO tiver nome OU se o nome limpo for vazio
        if (!categoriaSelecionada.value.nome || !categoriaSelecionada.value.nome.trim()) {
            console.warn('Nome é obrigatório.');
            return;
        }

        salvando.value = true;
        error.value = null;
        
        try {
            const payload = {
                id: categoriaSelecionada.value.id || 0, // Adicionado para garantir o ID no PUT/POST
                nome: categoriaSelecionada.value.nome,
                descricao: categoriaSelecionada.value.descricao,
                ativo: categoriaSelecionada.value.ativo === 'Ativo'
            };

            if (payload.id > 0) {
                await categoriaService.atualizar(payload.id, payload);
            } else {
                await categoriaService.criar(payload);
            }

            // Recarrega a tabela e limpa o form automaticamente
            await carregarCategorias();
            limparFormulario();

        } catch (err) {
            error.value = 'Erro ao salvar categoria: ' + err.message;
            throw err;
        } finally {
            salvando.value = false;
        }
    }

    
    const limparFormulario = () => {
        categoriaSelecionada.value = {
            id: null,   
            nome: '',
            descricao: '',
            ativo: ''
        };
    }

    return {
        categorias,
        error,
        loading,
        salvando,
        categoriaSelecionada,
        carregarCategorias,
        salvarCategoria,
        limparFormulario
    };
}