import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      // ============================================================
      // GROUP 1: ROTAS PÚBLICAS (Utilizam o PublicLayout)
      // ============================================================
      path: '/',
      component: () => import('../layouts/PublicLayout.vue'),
      children: [
        {
          path: '',
          name: 'site-home',
          component: () => import('../views/public/HomeSiteView.vue')
        }
      ]
    },
    {
      // ============================================================
      // GROUP 2: ROTAS ADMINISTRATIVAS / ERP (Utilizam o AdminLayout)
      // ============================================================
      path: '/admin',
      component: () => import('../layouts/AdminLayout.vue'),
      children: [
        {
          path: '',
          name: 'admin-dashboard',
          component: () => import('../views/DashboardView.vue')
        },
        {
          path: 'categorias',
          name: 'admin-categorias',
          component: () => import('../views/catalogo/CategoriasView.vue')
        },
        {
          path: 'produtos',
          name: 'admin-produtos',
          component: () => import('../views/catalogo/ProdutosView.vue')
        }
      ]
    }
  ]
})

export default router