<script setup>
import { useRouter } from 'vue-router';
import Button from 'primevue/button';

const router = useRouter();

const irParaAdmin = () => {
  router.push('/admin');
};
</script>

<template>
  <div class="public-wrapper">
    <!-- BARRA SUPERIOR SIMPLES DO SITE -->
    <nav class="public-nav">
      <div class="nav-brand">
        <i class="pi pi-globe text-xl mr-2"></i>
        <span>Caminho Livre</span>
      </div>
      
      <div class="nav-links">
        <a href="#" class="nav-link">Início</a>
        <a href="#" class="nav-link">Catálogo Público</a>
      </div>

      <div class="nav-actions">
        <!-- Botão para saltar para o Painel Admin do ERP -->
        <Button label="Acessar ERP" icon="pi pi-sign-in" severity="success" size="small" @click="irParaAdmin" />
      </div>
    </nav>

    <!-- CONTEÚDO DINÂMICO DO SITE PÚBLICO -->
    <main class="public-content">
      <RouterView />
    </main>

    <!-- RODAPÉ -->
    <footer class="public-footer">
      <p>&copy; 2026 Caminho Livre. Todos os direitos reservados.</p>
    </footer>
  </div>
</template>

<style scoped>
.public-wrapper {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
  font-family: var(--p-font-family);
  background-color: #ffffff;
}

.public-nav {
  display: flex;
  justify-content: space-between;
  align-items: center;
  height: 70px;
  padding: 0 2rem;
  background-color: #ffffff;
  border-bottom: 1px solid #f1f5f9;
}

.nav-brand {
  display: flex;
  align-items: center;
  font-weight: 700;
  font-size: 1.3rem;
  color: #334155;
}

.nav-brand i {
  color: #10b981;
}

.nav-links {
  display: flex;
  gap: 2rem;
}

.nav-link {
  text-decoration: none;
  color: #64748b;
  font-weight: 500;
}

.public-content {
  flex: 1;
  padding: 2rem;
  max-width: 1200px;
  width: 100%;
  margin: 0 auto;
}

.public-footer {
  background-color: #f8fafc;
  padding: 1.5rem;
  text-align: center;
  font-size: 0.875rem;
  color: #94a3b8;
  border-top: 1px solid #e2e8f0;
}
</style>