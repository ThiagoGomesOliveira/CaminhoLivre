import {ref} from 'vue'
import {categoriaService} from '@/services/categoriaService'

export function useCategorias() {
    const categorias = ref([]);
    const error = ref(null);   
    const loading = ref(false);

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

    return {
        categorias,
        error,
        loading,
        carregarCategorias
    };
}