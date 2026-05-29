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

const isDark = ref(true);

const toggleDarkMode = () => {
  const element = document.querySelector('html');
  if (element) {
    element.classList.toggle('p-dark');
    isDark.value = element.classList.contains('p-dark');
    localStorage.setItem('erp-theme', isDark.value ? 'dark' : 'light');
  }
};

onMounted(() => {
  const savedTheme = localStorage.getItem('erp-theme');
  const element = document.querySelector('html');
  
  if ((savedTheme === 'dark' || !savedTheme) && element) {
    element.classList.add('p-dark');
    isDark.value = true;
  } else if (savedTheme === 'light' && element) {
    element.classList.remove('p-dark');
    isDark.value = false;
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
    <div v-if="isDark" class="background-overlay"></div>

    <header class="topbar">
      <div class="logo-area">
        <i class="pi pi-truck"></i>
        <span class="logo-text">
          Caminho <strong>Livre</strong>
        </span>
      </div>
      
      <div class="actions-wrapper">
        <Button 
          :icon="isDark ? 'pi pi-sun' : 'pi pi-moon'" 
          severity="secondary" 
          text 
          rounded 
          @click="toggleDarkMode" 
          v-tooltip.bottom="isDark ? 'Modo Claro' : 'Modo Escuro'"
          class="theme-toggle-btn"
        />

        <button 
          @click="handleLogout"
          class="logout-btn"
        >
          <span>SAIR</span>
          <i class="pi pi-sign-out"></i>
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
  overflow: hidden;
  position: relative;
  background: var(--p-surface-0);
  color: var(--p-text-color);
  font-family: Inter, sans-serif;
  transition: background-color 0.3s, color 0.3s;
}

:global(html.p-dark) .layout-wrapper {
  background:
    radial-gradient(circle at top right, rgba(37,99,235,0.25), transparent 25%),
    linear-gradient(135deg, #020617 0%, #081225 45%, #0f172a 100%);
}

.background-overlay {
  position: absolute;
  inset: 0;
  background: radial-gradient(circle at center, rgba(255,255,255,0.04), transparent 60%);
  pointer-events: none;
}

.topbar {
  position: relative;
  z-index: 999;
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 75px;
  padding: 0 4rem;
  backdrop-filter: blur(12px);
  background: var(--p-content-background);
  border-bottom: 1px solid var(--p-content-border-color);
  transition: background-color 0.3s, border-color 0.3s;
}

:global(html.p-dark) .topbar {
  background: rgba(255, 255, 255, 0.01);
  border-bottom: 1px solid rgba(255, 255, 255, 0.08);
}

.logo-area {
  display: flex;
  align-items: center;
  gap: .8rem;
}

.logo-area i {
  font-size: 1.8rem;
  color: #3b82f6 !important;
}

.logo-text {
  font-size: 1.6rem;
  font-weight: 700;
  color: var(--p-text-color);
}

.logo-text strong {
  color: #3b82f6 !important;
}

.actions-wrapper {
  display: flex;
  align-items: center;
  gap: 1.5rem;
}

.theme-toggle-btn {
  color: var(--p-text-muted-color) !important;
  transition: 0.3s;
}

.theme-toggle-btn:hover {
  color: var(--p-text-color) !important;
  background: var(--p-surface-hover) !important;
}

.logout-btn {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.5rem 1rem;
  border-radius: 999px;
  background: rgba(239, 68, 68, 0.1);
  border: 1px solid rgba(239, 68, 68, 0.2);
  color: #ef4444;
  font-weight: 600;
  font-size: 0.85rem;
  cursor: pointer;
  transition: 0.3s;
}

.logout-btn:hover {
  background: rgba(239, 68, 68, 0.2);
  color: #f87171;
  transform: translateY(-1px);
}

.user-profile {
  display: flex;
  align-items: center;
  font-size: 0.95rem;
  color: var(--p-text-muted-color);
  border-left: 1px solid var(--p-content-border-color);
  padding-left: 1.5rem;
  height: 30px;
}

:global(html.p-dark) .user-profile {
  border-left: 1px solid rgba(255, 255, 255, 0.15);
}

/* Cor do ícone do usuário em azul */
.user-profile i {
  color: #3b82f6 !important;
}

.layout-container {
  display: flex;
  flex: 1;
  overflow: hidden;
  position: relative;
  z-index: 5;
}

.sidebar {
  width: 280px;
  background: var(--p-content-background);
  border-right: 1px solid var(--p-content-border-color);
  padding: 2rem 1rem;
  transition: background-color 0.3s, border-color 0.3s;
}

:global(html.p-dark) .sidebar {
  background: rgba(255, 255, 255, 0.02);
  backdrop-filter: blur(5px);
  border-right: 1px solid rgba(255, 255, 255, 0.08);
}

.custom-menu {
  border: none !important;
  width: 100% !important;
  background: transparent !important;
}

:deep(.p-menu) {
  background: transparent;
  border: none;
}

:deep(.p-menu-submenu-label) {
  background: transparent;
  color: #3b82f6 !important;
  font-weight: 700;
  text-transform: uppercase;
  font-size: 0.8rem;
  letter-spacing: 1px;
  margin-top: 1rem;
  padding: 0.5rem 0.75rem;
}

:deep(.p-menuitem-link) {
  color: var(--p-text-muted-color) !important;
  padding: 0.75rem 1rem !important;
  border-radius: 12px;
  transition: 0.3s !important;
  margin-bottom: 0.25rem;
}

:deep(.p-menuitem-link:hover) {
  background: var(--p-surface-hover) !important;
  color: var(--p-text-color) !important;
}

:global(html.p-dark) :deep(.p-menuitem-link) {
  color: #cbd5e1 !important;
}

:global(html.p-dark) :deep(.p-menuitem-link:hover) {
  background: rgba(255, 255, 255, 0.06) !important;
  color: white !important;
}

:deep(.p-menuitem-icon) {
  color: #3b82f6 !important;
  margin-right: 0.75rem;
}

.content {
  flex: 1;
  padding: 2.5rem 4rem;
  overflow-y: auto;
  background: var(--p-surface-50);
  transition: background-color 0.3s;
}

:global(html.p-dark) .content {
  background: transparent;
}

.card-container {
  background: var(--p-content-background);
  border: 1px solid var(--p-content-border-color);
  border-radius: 24px;
  padding: 2.5rem;
  min-height: 100%;
  transition: background-color 0.3s, border-color 0.3s;
}

:global(html.p-dark) .card-container {
  background: rgba(255, 255, 255, 0.05);
  border: 1px solid rgba(255, 255, 255, 0.08);
  backdrop-filter: blur(10px);
}

@media (max-width: 992px) {
  .topbar {
    padding: 0 1.5rem;
  }
  .content {
    padding: 1.5rem;
  }
  .sidebar {
    width: 240px;
  }
}
</style>
