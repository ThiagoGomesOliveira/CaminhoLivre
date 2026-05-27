<script setup>
import { ref } from 'vue';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import InputText from 'primevue/inputtext';
import Textarea from 'primevue/textarea';
import Dropdown from 'primevue/dropdown';
import Button from 'primevue/button';

// Dados fictícios para a tabela rodar
const categorias = ref([
  { id: 1, nome: 'Eletrônicos', descricao: 'Componentes e dispositivos', status: 'Ativo' },
  { id: 2, nome: 'Escritório', descricao: 'Materiais de escritório', status: 'Ativo' },
  { id: 3, nome: 'Logística', descricao: 'Suprimentos e caixas', status: 'Inativo' },
]);

// Estado do formulário lateral
const categoriaSelecionada = ref({ id: null, nome: '', descricao: '', status: 'Ativo' });

// Função para quando clicar em uma linha da tabela
const selecionarCategoria = (event) => {
  categoriaSelecionada.value = { ...event.data };
};

// Limpa o formulário
const novaCategoria = () => {
  categoriaSelecionada.value = { id: null, nome: '', descricao: '', status: 'Ativo' };
};

// Salva ou Edita
const salvarCategoria = () => {
  if (!categoriaSelecionada.value.nome) return;
  
  if (categoriaSelecionada.value.id) {
    const index = categorias.value.findIndex(c => c.id === categoriaSelecionada.value.id);
    categorias.value[index] = { ...categoriaSelecionada.value };
  } else {
    categoriaSelecionada.value.id = categorias.value.length + 1;
    categorias.value.push({ ...categoriaSelecionada.value });
  }
  novaCategoria();
};
</script>

<template>
  <div class="page-container">
    
    <div class="main-column">
      <div class="view-header">
        <div>
          <h1 class="text-xl font-bold text-color">Categorias de Produtos</h1>
          <p class="text-sm text-slate-500">Gerencie a classificação do catálogo para a logística.</p>
        </div>
        <span class="p-input-icon-left">
          <i class="pi pi-search" />
          <InputText placeholder="Pesquisar categoria..." class="p-inputtext-sm" />
        </span>
      </div>

      <DataTable 
        :value="categorias" 
        class="p-datatable-sm custom-table" 
        selectionMode="single" 
        dataKey="id"
        @row-select="selecionarCategoria"
      >
        <Column field="id" header="Cód." headerStyle="width: 4rem"></Column>
        <Column field="nome" header="Nome da Categoria" sortable></Column>
        <Column field="status" header="Status" headerStyle="width: 6rem">
          <template #body="slotProps">
            <span :class="'badge-' + slotProps.data.status.toLowerCase()">
              {{ slotProps.data.status }}
            </span>
          </template>
        </Column>
      </DataTable>
    </div>

    <aside class="form-column">
      <h2 class="text-lg font-bold text-color mb-4">
        {{ categoriaSelecionada.id ? 'Editar Registro' : 'Nova Categoria' }}
      </h2>
      
      <div class="form-grid">
        <div class="field">
          <label for="nome">Nome *</label>
          <InputText id="nome" v-model="categoriaSelecionada.nome" class="w-full p-inputtext-sm" />
        </div>

        <div class="field">
          <label for="descricao">Descrição</label>
          <Textarea id="descricao" v-model="categoriaSelecionada.descricao" rows="4" class="w-full text-sm" />
        </div>

        <div class="field">
          <label for="status">Status Operacional</label>
          <Dropdown id="status" v-model="categoriaSelecionada.status" :options="['Ativo', 'Inativo']" class="w-full p-inputtext-sm" />
        </div>
      </div>

      <div class="form-actions">
        <Button label="Salvar Categoria" severity="success" icon="pi pi-check" size="small" @click="salvarCategoria" />
        <Button label="Cancelar" severity="secondary" text size="small" @click="novaCategoria" />
      </div>
    </aside>

  </div>
</template>

<style scoped>
/* Layout Split Page */
.page-container {
  display: flex;
  gap: 1.5rem;
  height: 100%;
  align-items: flex-start;
  padding: 0.5rem; 
}

.main-column {
  flex: 1;
  /* TROCA: Sai o #ffffff fixo e entra a variável de fundo de conteúdo do PrimeVue */
  background: var(--p-content-background); 
  border-radius: 8px;
  padding: 1.5rem;
  border: 1px solid var(--p-content-border-color); /* Borda acompanha o tema */
  box-shadow: 0 1px 3px 0 rgba(15, 23, 42, 0.06);
}

.view-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1.5rem;
  border-bottom: 1px solid #f1f5f9;
  padding-bottom: 1rem;
}

.form-column {
  width: 360px;
  /* TROCA: Mesma coisa aqui, deixa o PrimeVue controlar o fundo */
  background: var(--p-content-background); 
  border-radius: 8px;
  padding: 1.5rem;
  border: 1px solid var(--p-content-border-color); /* Borda acompanha o tema */
  box-shadow: 0 1px 3px 0 rgba(15, 23, 42, 0.06);
  position: sticky;
  top: 0;
}

/* Formulário Denso */
.form-grid {
  display: flex;
  flex-direction: column;
  gap: 1.25rem;
  margin-bottom: 1.5rem;
}

.field {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.field label {
  font-size: 0.75rem;
  font-weight: 700;       /* Texto mais encorpado para leitura rápida */
  color: #334155;          /* Escurecido para dar contraste com o fundo branco */
  text-transform: uppercase;
  letter-spacing: 0.05em;
}

.form-actions {
  display: flex;
  justify-content: flex-end;
  gap: 0.75rem;
  border-top: 1px solid #e2e8f0;
  padding-top: 1.25rem;
}

/* Badges de Status - Tons escurecidos para atender regras de acessibilidade */
.badge-ativo {
  background-color: #dcfce7;
  color: #166534; 
  padding: 0.25rem 0.625rem;
  border-radius: 4px;
  font-size: 0.75rem;
  font-weight: 700;
}

.badge-inativo {
  background-color: #fee2e2;
  color: #991b1b; 
  padding: 0.25rem 0.625rem;
  border-radius: 4px;
  font-size: 0.75rem;
  font-weight: 700;
}
</style>