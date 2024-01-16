import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'
import axios from 'axios'
import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';
import {
    library
} from '@fortawesome/fontawesome-svg-core';
import {
    FontAwesomeIcon
} from '@fortawesome/vue-fontawesome';
import {
    fas
} from '@fortawesome/free-solid-svg-icons';
import {
    far
} from '@fortawesome/free-regular-svg-icons';

library.add(fas);
library.add(far);


const app = createApp(App)

app.component('font-awesome-icon', FontAwesomeIcon)
app.component('VueDatePicker', VueDatePicker);

const axiosInstance = axios.create({
    withCredentials: true,
    baseURL: 'https://localhost:7260',
  })
app.config.globalProperties.$axios = { ...axiosInstance }
app.use(router)
app.use(VueSweetalert2)
app.mount('#app')
