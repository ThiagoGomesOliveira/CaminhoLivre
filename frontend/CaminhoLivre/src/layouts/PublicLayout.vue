
<template>
  <div class="layout-wrapper">
    <header class="topbar">
      <div class="logo">
        <i class="pi pi-shield text-xl mr-2"></i>
        <span>Caminho Livre ERP</span>
      </div>
      <div class="user-profile">
        <i class="pi pi-user mr-2"></i>
        <span>Desenvolvedor</span>
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

<style>
/* Layout Estrutural em L */
.layout-wrapper {
  display: flex;
  flex-direction: column;
  height: 100vh;
  font-family: var(--p-font-family);
  background-color: #f1f5f9;
}

.topbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 60px;
  background-color: #ffffff;
  padding: 0 1.5rem;
  border-bottom: 1px solid #e2e8f0;
  z-index: 999;
}

.logo {
  display: flex;
  align-items: center;
  font-weight: 700;
  color: #10b981; /* Cor Verde do PrimeVue Aura */
  font-size: 1.2rem;
}

.layout-container {
  display: flex;
  flex: 1;
  overflow: hidden;
}

.sidebar {
  width: 260px;
  background-color: #ffffff;
  border-right: 1px solid #e2e8f0;
  padding: 1rem 0.5rem;
}

.custom-menu {
  border: none !important;
  width: 100% !important;
}

.content {
  flex: 1;
  padding: 1.5rem;
  overflow-y: auto;
}

.card-container {
  background-color: #ffffff;
  border-radius: 8px;
  padding: 1.5rem;
  box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.1);
  min-height: 100%;
}
</style>
