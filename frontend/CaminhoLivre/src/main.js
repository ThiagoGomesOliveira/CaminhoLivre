import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'

// 1. Importa o coração do PrimeVue
import PrimeVue from 'primevue/config'

// 2. Importa o tema Aura oficial do pacote que você acabou de instalar
import Aura from '@primevue/themes/aura'

// 3. Importa os ícones (que já estão funcionando!)
import 'primeicons/primeicons.css'

const app = createApp(App)

app.use(createPinia())
app.use(router)

// 4. Inicializa o PrimeVue injetando o tema Aura
app.use(PrimeVue, {
    theme: {
        preset: Aura,
        options: {
            darkModeSelector: 'none' // Garante que o sistema comece no modo claro
        }
    }
})

app.mount('#app')