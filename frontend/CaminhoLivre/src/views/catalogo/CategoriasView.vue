<script setup>
import { ref } from 'vue';
import {onMounted} from 'vue';
import {useCategorias} from '../../composables/catalogo/useCategorias';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import InputText from 'primevue/inputtext';
import Textarea from 'primevue/textarea';
import Dropdown from 'primevue/dropdown';
import Button from 'primevue/button';
import ProgressSpinner from 'primevue/progressspinner';
import { FilterMatchMode } from '@primevue/core/api';

const {categorias,
      loading,
      error,
      carregarCategorias,
      salvarCategoria,
      limparFormulario,
      salvando,
      categoriaSelecionada
     } = useCategorias();

const filtros = ref({
    global: { value: null, matchMode: 'contains' }
});

onMounted(async () => {
  await carregarCategorias();
});


// Função para quando clicar em uma linha da tabela
const selecionarCategoria = (event) => {
  categoriaSelecionada.value = { ...event.data };

  if (categoriaSelecionada.value.ativo === true) {
    categoriaSelecionada.value.ativo = 'Ativo';
  } else {
    categoriaSelecionada.value.ativo = 'Inativo';
  }
};

const definirEstiloLinha = (data) => {
  // Se a categoria estiver inativa, aplica uma classe CSS customizada
  return data.status === false || data.status === 'Inativo' ? 'linha-inativa' : '';
};
</script>

<template>
  <div class="page-container">
    
    <div class="main-column">
      <div class="view-header">
        <div>
          <h1 class="text-xl font-bold text-color">Categorias</h1>
        </div>
        <span class="p-input-icon-left">
          <InputText v-model="filtros.global.value" placeholder="Pesquisar categoria..." class="p-inputtext-sm" />
        </span>
      </div>
      
      <div class="card-container">
    <div v-if="loading" class="flex justify-center p-5">
      <ProgressSpinner style="width: 50px; height: 50px" />
    </div>

    <div v-else-if="error" class="p-error mb-4">
      {{ error }}
    </div>

      <DataTable 
        :value="categorias"
        v-model:filters="filtros"
        :globalFilterFields="['nome']"
        class="p-datatable-sm custom-table" 
        selectionMode="single" 
        dataKey="id"
        :rowClass="definirEstiloLinha"
        @row-select="selecionarCategoria"
      >
        <Column field="id" header="Cód." headerStyle="width: 4rem"></Column>
        <Column field="nome" header="Nome" sortable></Column>
        <Column field="descricao" header="Descrição"></Column>
        <Column field="ativo" header="Status" headerStyle="width: 8rem" sortable>
        <template #body="slotProps">
          <span v-if="slotProps.data.ativo === true" class="status-badge badge-success">
                 <i class="pi pi-check-circle text-xs mr-1"></i>
                Ativo
          </span>
          <span v-else class="status-badge badge-danger">
                <i class="pi pi-times-circle text-xs mr-1"></i>
                Inativo
          </span>
        </template>
        </Column>
      </DataTable>
      </div>
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
          <Dropdown id="status" v-model="categoriaSelecionada.ativo" :options="['Ativo', 'Inativo']" class="w-full p-inputtext-sm" />
        </div>
      </div>

      <div class="form-actions">
        <Button label="Salvar Categoria" severity="success" icon="pi pi-check" size="small" @click="salvarCategoria" />
        <Button label="Cancelar" severity="secondary" text size="small" @click="limparFormulario" />
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
/* Base dos Badges de Status */
.status-badge {
  display: inline-flex;
  align-items: center;
  padding: 0.25rem 0.625rem;
  border-radius: 6px;
  font-size: 0.75rem;
  font-weight: 700;
  text-transform: uppercase;
  letter-spacing: 0.05em;
  border: 1px solid transparent;
}

/* Status Ativo (Verde) */
.badge-success {
  background-color: rgba(34, 197, 94, 0.15); /* Fundo verde translúcido */
  color: #4ade80;                            /* Texto verde vivo para Dark Mode */
  border-color: rgba(34, 197, 94, 0.3);
}

/* Status Inativo (Vermelho) */
.badge-danger {
  background-color: rgba(239, 68, 68, 0.15);  /* Fundo vermelho translúcido */
  color: #f87171;                             /* Texto vermelho vivo para Dark Mode */
  border-color: rgba(239, 68, 68, 0.3);
}

/* Micro-ajuste automático caso o sistema volte para o Modo Claro (.p-dark desativado) */
:not(html.p-dark) .badge-success {
  background-color: #dcfce7;
  color: #166534;
}

:not(html.p-dark) .badge-danger {
  background-color: #fee2e2;
  color: #991b1b;
}

/* Aplica uma opacidade de 60% na linha inteira do grid se o produto/categoria estiver inativo */
::v-deep(.linha-inativa) {
  opacity: 0.6;
  text-decoration: line-through rgba(255, 255, 255, 0.2); /* Opcional: risca de leve o texto */
}
</style>