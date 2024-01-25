import { createRouter, createWebHistory } from 'vue-router'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'start',
      component: () => import('../views/Players/PlayersView.vue'),
      meta: { breadcrumb: 'Jucatori' },
    },
    {
      path: '/players',
      name: 'players',
      component: () => import('../views/Players/PlayersView.vue'),
      meta: { breadcrumb: 'Jucatori' },
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
      path: '/stuff',
      name: 'stuff',
      component: () => import('../views/Stuff/StuffView.vue'),
      meta: { breadcrumb: 'Staff' },
    },
    {
      path: '/add-stuff',
      name: 'add-stuff',
      component: () => import('../views/Stuff/AddStuffView.vue'),
      meta: { breadcrumb: 'Adăugare staff' },
    },
    {
      path: '/edit-stuff/:id',
      name: 'edit-stuff',
      component: () => import('../views/Stuff/EditStuffView.vue'),
      meta: { breadcrumb: 'Editare staff' },
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
    {
      path: '/teams',
      name: 'teams',
      component: () => import('../views/TeamsView.vue'),
      meta: { breadcrumb: 'Echipe' },
      children: [
      ],
    },
    {
      path: '/matches',
      name: 'matches',
      component: () => import('../views/MatchesView.vue'),
      meta: { breadcrumb: 'Meciuri' },
      children: [
      ],
    },
    {
      path: '/trofees',
      name: 'trofees',
      component: () => import('../views/TrofeesView.vue'),
      meta: { breadcrumb: 'Trofee' },
      children: [
      ],
    },
    {
      path: '/socialMedia',
      name: 'socialMedia',
      component: () => import('../views/SocialMediaView.vue'),
      meta: { breadcrumb: 'Social media' },
      children: [
      ],
    },
    {
      path: '/championships',
      name: 'championships',
      component: () => import('../views/ChampionshipView.vue'),
      meta: { breadcrumb: 'Campionate' },
      children: [
      ],
    },
    {
      path: '/playerHistory',
      name: 'playerHistory',
      component: () => import('../views/PlayerHistoryView.vue'),
      meta: { breadcrumb: 'Participari jucatori' },
      children: [
      ],
    },
    {
      path: '/stuffHistory',
      name: 'stuffHistory',
      component: () => import('../views/StuffHistoryView.vue'),
      meta: { breadcrumb: 'Participari staff' },
      children: [
      ],
    },
    
    // {
    //   path: '/home',
    //   name: 'home',
    //   component: () => import('../home/acasa.html')
    // },
    
  ]
})

export default router
