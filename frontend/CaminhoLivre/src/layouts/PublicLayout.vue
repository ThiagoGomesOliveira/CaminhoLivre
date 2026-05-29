<script setup>
import { useRouter } from 'vue-router';
import Button from 'primevue/button';

const router = useRouter();

const irParaAdmin = () => {
  router.push('/admin');
};

const abrirDemonstracao = () => {
  window.open('https://www.youtube.com/watch?v=f1gs0s1g8eI', '_blank');
};
</script>

<template>
  <div class="layout-wrapper">

    <div class="background-overlay"></div>

    <nav class="topbar">
      <div class="logo-area">
        <i class="pi pi-truck"></i>
        <span class="logo-text">
          Caminho <strong>Livre</strong>
        </span>
      </div>

      <div class="menu-links">
        <a href="#">Início</a>
        <a href="#">Recursos</a>
        <a href="#">Sobre</a>
        <a href="#">Planos</a>
        <a href="#">Contato</a>
      </div>

      <div class="right-actions">
        <Button
          label="Acessar Sistema"
          icon="pi pi-arrow-right"
          severity="primary"
          rounded
          @click="irParaAdmin"
        />
      </div>
    </nav>

    <section class="hero-section">

      <div class="hero-left">

        <div class="mini-badge">
          ERP COMPLETO PARA TRANSPORTADORAS
        </div>

        <h1>
          Gestão inteligente
          para levar seu negócio
          <span>mais longe.</span>
        </h1>

        <p>
          Sistema completo para gestão de transportes,
          e-commerce e operações logísticas.
          Mais controle, eficiência e resultados
          para sua transportadora.
        </p>

        <div class="feature-tags">
          <div class="tag">E-commerce</div>
          <div class="tag">Logística</div>
          <div class="tag">Frotas</div>
          <div class="tag">Financeiro</div>
        </div>

        <div class="hero-buttons">
          <Button
            label="Solicitar Demonstração"
            icon="pi pi-arrow-right"
            severity="primary"
            size="large"
            @click="abrirDemonstracao"
          />

          <Button
            label="Conhecer Recursos"
            icon="pi pi-th-large"
            outlined
            size="large"
          />
        </div>

      </div>

      <div class="hero-right">
        <div class="truck-scene">

          <div class="road"></div>

          <div class="truck-container">

            <div class="truck-cabin"></div>

            <div class="truck-body">
              <div class="truck-logo">
                <i class="pi pi-truck"></i>
                <div>
                  <h2>Caminho Livre</h2>
                  <span>Gestão que move resultados</span>
                </div>
              </div>
            </div>

            <div class="wheel wheel-1"></div>
            <div class="wheel wheel-2"></div>
            <div class="wheel wheel-3"></div>

          </div>
        </div>
      </div>

    </section>

    <section class="stats-section">

      <div class="stat-card">
        <i class="pi pi-send"></i>
        <div>
          <h3>1.248</h3>
          <span>Entregas Hoje</span>
        </div>
      </div>

      <div class="stat-card">
        <i class="pi pi-truck"></i>
        <div>
          <h3>86</h3>
          <span>Veículos Ativos</span>
        </div>
      </div>

      <div class="stat-card">
        <i class="pi pi-shopping-cart"></i>
        <div>
          <h3>2.317</h3>
          <span>Pedidos E-commerce</span>
        </div>
      </div>

      <div class="stat-card">
        <i class="pi pi-dollar"></i>
        <div>
          <h3>R$ 1,2M</h3>
          <span>Faturamento</span>
        </div>
      </div>

    </section>

    <main class="content-area">
      <RouterView />
    </main>

  </div>
</template>

<style scoped>

.layout-wrapper {
  min-height: 100vh;
  overflow: hidden;
  position: relative;
  background:
    radial-gradient(circle at top right, rgba(37,99,235,0.25), transparent 25%),
    linear-gradient(135deg, #020617 0%, #081225 45%, #0f172a 100%);
  color: white;
  font-family: Inter, sans-serif;
}

.background-overlay {
  position: absolute;
  inset: 0;
  background:
    radial-gradient(circle at center, rgba(255,255,255,0.04), transparent 60%);
  pointer-events: none;
}

.topbar {
  position: relative;
  z-index: 10;
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 1.5rem 4rem;
  backdrop-filter: blur(12px);
}

.logo-area {
  display: flex;
  align-items: center;
  gap: .8rem;
}

.logo-area i {
  font-size: 2rem;
  color: #3b82f6;
}

.logo-text {
  font-size: 2rem;
  font-weight: 700;
}

.logo-text strong {
  color: #3b82f6;
}

.menu-links {
  display: flex;
  gap: 2rem;
}

.menu-links a {
  color: #cbd5e1;
  text-decoration: none;
  transition: .3s;
}

.menu-links a:hover {
  color: white;
}

.hero-section {
  position: relative;
  z-index: 5;
  display: grid;
  grid-template-columns: 1fr 1fr;
  align-items: center;
  padding: 4rem;
  min-height: 75vh;
}

.hero-left h1 {
  font-size: 4.5rem;
  line-height: 1.05;
  margin: 1.5rem 0;
  font-weight: 800;
}

.hero-left h1 span {
  color: #3b82f6;
}

.hero-left p {
  color: #cbd5e1;
  font-size: 1.2rem;
  line-height: 1.8;
  max-width: 650px;
}

.mini-badge {
  display: inline-block;
  padding: .7rem 1.2rem;
  border-radius: 999px;
  background: rgba(59,130,246,.12);
  border: 1px solid rgba(59,130,246,.4);
  color: #60a5fa;
  font-size: .9rem;
  font-weight: 600;
}

.feature-tags {
  display: flex;
  gap: 1rem;
  margin-top: 2rem;
  flex-wrap: wrap;
}

.tag {
  padding: .7rem 1.1rem;
  border-radius: 999px;
  background: rgba(255,255,255,.06);
  border: 1px solid rgba(255,255,255,.08);
  color: white;
  font-size: .95rem;
}

.hero-buttons {
  display: flex;
  gap: 1rem;
  margin-top: 2.5rem;
}

/* CAMINHÃO */
.hero-right {
  display: flex;
  justify-content: center;
  align-items: center;
}

.truck-scene {
  position: relative;
  width: 100%;
  height: 500px;
}

.road {
  position: absolute;
  bottom: 70px;
  width: 100%;
  height: 8px;
  background: rgba(255,255,255,.12);
  border-radius: 999px;
}

.truck-container {
  position: absolute;
  bottom: 90px;
  left: -100px;
  display: flex;
  align-items: center;
  animation: moveTruck 8s ease-in-out infinite alternate;
}

.truck-cabin {
  width: 110px;
  height: 100px;
  background: linear-gradient(135deg, #dbeafe, #93c5fd);
  border-radius: 20px 0 0 20px;
  margin-right: 8px;
  position: relative;
  box-shadow: 0 15px 40px rgba(0,0,0,.4);
}

.truck-cabin::before {
  content: '';
  position: absolute;
  width: 55px;
  height: 35px;
  background: rgba(15,23,42,.6);
  top: 15px;
  left: 15px;
  border-radius: 10px;
}

.truck-body {
  width: 430px;
  height: 180px;
  border-radius: 22px;
  background: linear-gradient(135deg, #0f172a, #1e3a8a);
  border: 1px solid rgba(255,255,255,.08);
  display: flex;
  justify-content: center;
  align-items: center;
  box-shadow: 0 25px 60px rgba(0,0,0,.55);
  position: relative;
  overflow: hidden;
}

.truck-body::after {
  content: '';
  position: absolute;
  inset: 0;
  background:
    linear-gradient(
      120deg,
      transparent,
      rgba(255,255,255,.08),
      transparent
    );
}

.truck-logo {
  display: flex;
  align-items: center;
  gap: 1rem;
  z-index: 2;
}

.truck-logo i {
  font-size: 4rem;
  color: #3b82f6;
}

.truck-logo h2 {
  font-size: 2.4rem;
  margin: 0;
  font-weight: 800;
}

.truck-logo span {
  color: #cbd5e1;
  font-size: 1rem;
}

.wheel {
  position: absolute;
  bottom: -24px;
  width: 54px;
  height: 54px;
  border-radius: 50%;
  background: #020617;
  border: 7px solid #94a3b8;
  animation: rotateWheel .9s linear infinite;
}

.wheel-1 {
  left: 35px;
}

.wheel-2 {
  right: 120px;
}

.wheel-3 {
  right: 35px;
}

@keyframes rotateWheel {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}

@keyframes moveTruck {
  from {
    transform: translateX(-40px);
  }

  to {
    transform: translateX(40px);
  }
}

.stats-section {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 1.5rem;
  padding: 0 4rem 4rem;
}

.stat-card {
  background: rgba(255,255,255,.05);
  border: 1px solid rgba(255,255,255,.08);
  border-radius: 24px;
  padding: 2rem;
  display: flex;
  align-items: center;
  gap: 1rem;
  backdrop-filter: blur(10px);
  transition: .3s;
}

.stat-card:hover {
  transform: translateY(-6px);
  background: rgba(255,255,255,.08);
}

.stat-card i {
  font-size: 2rem;
  color: #3b82f6;
}

.stat-card h3 {
  margin: 0;
  font-size: 2rem;
}

.stat-card span {
  color: #cbd5e1;
}

.content-area {
  padding: 2rem 4rem 5rem;
}

@media (max-width: 1200px) {

  .hero-section {
    grid-template-columns: 1fr;
    text-align: center;
  }

  .hero-buttons,
  .feature-tags {
    justify-content: center;
  }

  .stats-section {
    grid-template-columns: repeat(2, 1fr);
  }

  .hero-left h1 {
    font-size: 3.5rem;
  }
}

@media (max-width: 768px) {

  .topbar {
    flex-direction: column;
    gap: 1.5rem;
    padding: 1.5rem;
  }

  .menu-links {
    flex-wrap: wrap;
    justify-content: center;
  }

  .hero-section {
    padding: 2rem;
  }

  .hero-left h1 {
    font-size: 2.5rem;
  }

  .stats-section {
    grid-template-columns: 1fr;
    padding: 2rem;
  }

  .truck-body {
    width: 280px;
    height: 130px;
  }

  .truck-logo h2 {
    font-size: 1.5rem;
  }

  .truck-logo i {
    font-size: 2.5rem;
  }

  .truck-cabin {
    width: 80px;
    height: 70px;
  }
}

</style>
