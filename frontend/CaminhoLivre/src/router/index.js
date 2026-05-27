import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('../views/DashboardView.vue')
    },
    {
      path: '/categorias',
      name: 'categorias',
      // Ajustado: adicionado o diretório /catalogo/
      component: () => import('../views/catalogo/CategoriasView.vue')
    },
    {
      path: '/produtos',
      name: 'produtos',
      // Ajustado: adicionado o diretório /catalogo/
      component: () => import('../views/catalogo/ProdutosView.vue')
    }
  ]
})

export default router