<script setup>
// Importamos o componente de Botão do PrimeVue
import Button from 'primevue/button';
</script>

<template>
  <div style="padding: 2rem; text-align: center;">
    <h1>Painel Caminho Livre</h1>
    <p>Se você estiver vendo o botão estilizado e o ícone de "check" abaixo, a configuração deu certo!</p>
    
    <Button label="Configuração Concluída" icon="pi pi-check" severity="success" />
  </div>
</template>