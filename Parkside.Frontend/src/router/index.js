import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/players',
      name: 'players',
      component: () => import('../views/Players/PlayersView.vue'),
      meta: { breadcrumb: 'Membri' },
    },
    {
      path: '/add-player',
      name: 'add-player',
      component: () => import('../views/Players/AddPlayerView.vue'),
      meta: { breadcrumb: 'Adăugare jucator' },
    },
    {
      path: '/edit-player/:id',
      name: 'edit-player',
      component: () => import('../views/Players/EditPlayerView.vue'),
      meta: { breadcrumb: 'Editare jucator' },
    },
    {
      path: '/users',
      name: 'Users',
      component: () => import('../views/UsersView.vue'),
      meta: { breadcrumb: 'Utilizatori' },
    },
    {
      path: '/news',
      name: 'news',
      component: () => import('../views/News/NewsView.vue'),
      meta: { breadcrumb: 'Știri' },
    },
    {
      path: '/add-news',
      name: 'add-news',
      component: () => import('../views/News/AddNewsView.vue'),
      meta: { breadcrumb: 'Adaugă știre' },
    },
    {
      path: '/edit-news/:id',
      name: 'edit-news',
      component: () => import('../views/News/EditNewsView.vue'),
      meta: { breadcrumb: 'Editează știre' },
    },
    {
      path: '/sponsors',
      name: 'sponsors',
      component: () => import('../views/SponsorsView.vue'),
      meta: { breadcrumb: 'Sponsori' },
      children: [
      ],
    },
    
  ]
})

export default router
