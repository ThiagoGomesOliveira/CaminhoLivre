<script setup>
import { ref } from 'vue';
import { onMounted } from 'vue';
import { useProdutos } from '../../composables/useProdutos';

const { produtos,  loading, listarProdutos ,  criarProduto, atualizarProduto } = useProdutos();

onMounted(async () => {
    await listarProdutos();
});

// 1. FILTROS E CONTROLE DO MODAL
const filtros = ref({
    global: { value: null, matchMode: 'contains' }
});
const exibirModal = ref(false); // Controla a exibição do Modal/Dialog

// Componentes do PrimeVue v4
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import Button from 'primevue/button';
import InputText from 'primevue/inputtext';
import InputNumber from 'primevue/inputnumber';
import Dropdown from 'primevue/dropdown';
import IconField from 'primevue/iconfield';
import InputIcon from 'primevue/inputicon';
import Dialog from 'primevue/dialog';


const salvando = ref(false);

// Simulação de categorias para o Dropdown
const categoriasDisponiveis = ref([
    { id: 1, nome: 'Eletrônicos' },
    { id: 2, nome: 'Vestuário' },
    { id: 3, nome: 'Logística / Embalagens' }
]);

// Objeto reativo do formulário
const produtoSelecionado = ref({
    id: null,
    codigoBarras: '',
    nome: '',
    descricao: '',
    precoVenda: 0,
    precoCusto: 0,
    sku: '',
    categoriaId: null,
    ativo: 'Ativo'
});

// Ações do Fluxo de Telas
const abrirNovoProduto = () => {
    limparFormulario();
    exibirModal.value = true;
};

const selecionarProduto = (event) => {
    produtoSelecionado.value = { ...event.data };
    produtoSelecionado.value.ativo = event.data.status;
    
    const cat = categoriasDisponiveis.value.find(c => c.nome === event.data.categoriaNome);
    produtoSelecionado.value.categoriaId = cat ? cat.id : null;
    
    exibirModal.value = true;
};

const limparFormulario = () => {
    produtoSelecionado.value = {
        id: null,
        nome: '',
        descricao: '',
        precoVenda: 0,
        precoCusto: 0,
        categoriaId: null,
        ativo: 'Ativo',
        sku: ''
    };
};

const fecharModal = () => {
    exibirModal.value = false;
};

const salvarProdutoMock = () => {
    console.log('Dados prontos para enviar à API .NET:', produtoSelecionado.value);
    alert('Produto salvo com sucesso no Mock!');
    exibirModal.value = false;
};
</script>

<template>
  <div style="width: 100%; padding: 16px; background-color: #020617; color: #f8fafc; box-sizing: border-box;">
    
    <div class="bg-slate-900 border border-slate-800 p-5 rounded-xl w-full">
      
      <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px; width: 100%;">
        
        <div>
          <h1 class="text-xl font-bold text-white tracking-wide">Produtos</h1>
        </div>
        
        <div style="display: flex; align-items: center; gap: 12px;">
          
          <Button 
            label="Novo Produto" 
            icon="pi pi-plus" 
            severity="success" 
            size="small" 
            @click="abrirNovoProduto" 
          />

          <IconField iconPosition="left">
            <InputIcon class="pi pi-search" />
            <InputText 
              v-model="filtros.global.value" 
              placeholder="Pesquisar produto..." 
              class="p-inputtext-sm bg-slate-950 border-slate-800 text-slate-200"
              style="width: 240px;"
            />
          </IconField>
          
        </div>
      </div>
      
      <DataTable 
        :value="produtos" 
        v-model:filters="filtros"
        :globalFilterFields="['nome', 'codigoBarras', 'categoriaNome']"
        class="p-datatable-sm text-sm w-full" 
        selectionMode="single" 
        dataKey="id"
        @row-select="selecionarProduto"
      >
        <Column field="id" header="Cód." headerStyle="width: 5rem" class="text-slate-400"></Column>
        <Column field="nome" header="Produto" sortable class="font-medium text-slate-200"></Column>
        <Column field="sku" header="Sku" sortable class="text-slate-400" headerStyle="width: 10rem"></Column>
        <Column field="categoriaNome" header="Categoria" sortable class="text-slate-400" headerStyle="width: 15rem"></Column>
        
        <Column field="precoVenda" header="Preço de Venda" sortable headerStyle="width: 10rem" class="font-semibold">
          <template #body="slotProps">
            {{ slotProps.data.precoVenda.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }) }}
          </template>
        </Column>

        <Column field="precoCusto" header="Preço de Custo" sortable headerStyle="width: 10rem" class="font-semibold">
          <template #body="slotProps">
            {{ slotProps.data.precoCusto.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }) }}
          </template>
        </Column>

        
        <Column field="ativo" header="Status" headerStyle="width: 8rem">
          <template #body="slotProps">
            <span :class="slotProps.data.ativo === 'Ativo' ? 'text-green-400 font-bold' : 'text-red-400 font-bold'">
              {{ slotProps.data.ativo }}
            </span>
          </template>
        </Column>
      </DataTable>
    </div>

    <Dialog 
      v-model:visible="exibirModal" 
      modal 
      :header="produtoSelecionado.id ? '🔑 Editar Detalhes do Produto' : '📦 Cadastrar Novo Produto'" 
      style="width: 460px; background-color: #0f172a; border: 1px solid #334155;"
      :breakpoints="{ '1199px': '75vw', '575px': '90vw' }"
    >
      <div style="display: flex; flex-direction: column; gap: 16px; padding: 12px 0;">

         <div style="display: flex; flex-direction: column; gap: 4px; text-align: left;">
          <label for="nome" class="text-xs font-medium text-slate-400 uppercase">Nome do Produto</label>
          <InputText id="nome" v-model="produtoSelecionado.nome" class="w-full p-inputtext-sm bg-slate-950 border-slate-800 text-white" placeholder="Ex: Caixa Master Papelão" />
        </div>

        <div style="display: flex; flex-direction: column; gap: 4px; text-align: left;">
          <label for="sku" class="text-xs font-medium text-slate-400 uppercase">SKU</label>
          <InputText id="sku" v-model="produtoSelecionado.sku" class="w-full p-inputtext-sm bg-slate-950 border-slate-800 text-white" placeholder="Ex: VES-CAM-PRM" />
        </div>

        <div style="display: flex; flex-direction: column; gap: 4px; text-align: left;">
          <label for="categoria" class="text-xs font-medium text-slate-400 uppercase">Categoria</label>
          <Dropdown 
            id="categoria" 
            v-model="produtoSelecionado.categoriaId" 
            :options="categoriasDisponiveis" 
            optionLabel="nome" 
            optionValue="id" 
            placeholder="Selecione a categoria correspondente"
            class="w-full p-inputtext-sm bg-slate-950 border-slate-800 text-left text-white" 
          />
        </div>

        <div style="display: flex; flex-direction: column; gap: 4px; text-align: left;">
          <label for="precoVenda" class="text-xs font-medium text-slate-400 uppercase">Preço de Venda</label>
          <InputNumber id="precoVenda" v-model="produtoSelecionado.precoVenda" mode="currency" currency="BRL" locale="pt-BR" class="w-full p-inputtext-sm bg-slate-950 border-slate-800 text-white" />
        </div>

        <div style="display: flex; flex-direction: column; gap: 4px; text-align: left;">
          <label for="precoCusto" class="text-xs font-medium text-slate-400 uppercase">Preço de Custo</label>
          <InputNumber id="precoCusto" v-model="produtoSelecionado.precoCusto" mode="currency" currency="BRL" locale="pt-BR" class="w-full p-inputtext-sm bg-slate-950 border-slate-800 text-white" />
        </div>

        <div style="display: flex; flex-direction: column; gap: 4px; text-align: left;">
          <label for="descricao" class="text-xs font-medium text-slate-400 uppercase">Descrição Detalhada</label>
          <InputText id="descricao" v-model="produtoSelecionado.descricao" class="w-full p-inputtext-sm bg-slate-950 border-slate-800 text-white" placeholder="Especificações técnicas, dimensões..." />
        </div>

        <div style="display: flex; flex-direction: column; gap: 4px; text-align: left;">
          <label for="ativo" class="text-xs font-medium text-slate-400 uppercase">Status Operacional</label>
          <Dropdown 
            id="ativo" 
            v-model="produtoSelecionado.ativo" 
            :options="['Ativo', 'Inativo']" 
            class="w-full p-inputtext-sm bg-slate-950 border-slate-800 text-left text-white" 
          />
        </div>
      </div>

      <template #footer>
        <div class="flex justify-end gap-2 pt-2 border-t border-slate-800">
          <Button label="Cancelar" icon="pi pi-times" severity="secondary" text size="small" @click="fecharModal" />
          <Button label="Salvar Produto" icon="pi pi-save" severity="success" size="small" :loading="salvando" @click="salvarProdutoMock" />
        </div>
      </template>
    </Dialog>

  </div>
</template>