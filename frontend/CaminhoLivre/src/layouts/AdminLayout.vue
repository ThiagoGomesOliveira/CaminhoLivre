<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import Menu from 'primevue/menu';
import Button from 'primevue/button';

const router = useRouter();

function handleLogout() {
  localStorage.removeItem('token')
  localStorage.removeItem('user')  
  router.push('/')
}

const isDark = ref(false);

const toggleDarkMode = () => {
  const element = document.querySelector('html');
  if (element) {
    element.classList.toggle('p-dark');
    isDark.value = element.classList.contains('p-dark');
    
    // Salva a preferência do usuário no navegador
    localStorage.setItem('erp-theme', isDark.value ? 'dark' : 'light');
  }
};

// Quando o sistema carrega, verifica se o usuário já tinha uma preferência salva
onMounted(() => {
  const savedTheme = localStorage.getItem('erp-theme');
  const element = document.querySelector('html');
  
  if (savedTheme === 'dark' && element) {
    element.classList.add('p-dark');
    isDark.value = true;
  }
});

const menuItems = ref([
  {
    label: 'Principal',
    items: [
      { label: 'Dashboard', icon: 'pi pi-chart-bar', command: () => router.push('/admin') }
    ]
  },
  {
    label: 'Cadastros',
    items: [
      { label: 'Categorias', icon: 'pi pi-tags', command: () => router.push('/admin/categorias') },
      { label: 'Produtos', icon: 'pi pi-box', command: () => router.push('/admin/produtos') }
    ]
  }
]);
</script>

<template>
  <div class="layout-wrapper">
    <header class="topbar">
      <div class="logo">
        <span>Caminho Livre</span>
      </div>
      
      <div class="actions-wrapper">
        <Button 
          :icon="isDark ? 'pi pi-sun' : 'pi pi-moon'" 
          severity="secondary" 
          text 
          rounded 
          @click="toggleDarkMode" 
          v-tooltip.bottom="isDark ? 'Modo Claro' : 'Modo Escuro'"
          class="mr-2"
        />

         <button 
        @click="handleLogout"
        class="flex items-center gap-2 px-3 py-1.5 rounded-md git  hover:bg-red-600/20 hover:text-red-500 transition-colors"
      >
        <span>SAIR</span>
        <XMarkIcon class="w-4 h-4" />
      </button>

        <div class="user-profile">
          <i class="pi pi-user mr-2"></i>
          <span>Desenvolvedor</span>
        </div>
      </div>
    </header>

    <div class="layout-container">
      <aside class="sidebar">
        <Menu :model="menuItems" class="custom-menu" />
      </aside>

      <main class="content">
        <div class="card-container">
          <RouterView />
        </div>
      </main>
    </div>
  </div>
</template>

<style scoped>
.layout-wrapper {
  display: flex;
  flex-direction: column;
  height: 100vh;
  font-family: var(--p-font-family);
  /* Varáveis do PrimeVue que mudam sozinhas no Dark Mode */
  background-color: var(--p-content-background); 
}

.topbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 60px;
  background-color: var(--p-content-background);
  padding: 0 1.5rem;
  border-bottom: 1px solid var(--p-content-border-color);
  z-index: 999;
}

.logo {
  display: flex;
  align-items: center;
  font-weight: 700;
  color: #10b981;
  font-size: 1.2rem;
}

.actions-wrapper {
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.user-profile {
  display: flex;
  align-items: center;
  font-size: 0.9rem;
  color: var(--p-text-color);
  border-left: 1px solid var(--p-content-border-color);
  padding-left: 1rem;
  height: 30px;
}

.layout-container {
  display: flex;
  flex: 1;
  overflow: hidden;
  background-color: var(--p-content-background);
}

.sidebar {
  width: 260px;
  background-color: var(--p-content-background);
  border-right: 1px solid var(--p-content-border-color);
  padding: 1rem 0.5rem;
}

.custom-menu {
  border: none !important;
  width: 100% !important;
  background: transparent !important;
}

.content {
  flex: 1;
  padding: 1.5rem;
  overflow-y: auto;
  /* Dá o tom cinza de fundo no Light e grafite escuro no Dark */
  background-color: var(--p-textarea-background); 
}

.card-container {
  background-color: var(--p-content-background);
  border-radius: 8px;
  padding: 1.5rem;
  border: 1px solid var(--p-content-border-color);
  min-height: 100%;
}
</style>