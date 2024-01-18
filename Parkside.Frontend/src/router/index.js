import { createRouter, createWebHistory } from 'vue-router'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
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
      path: '/coaches',
      name: 'coaches',
      component: () => import('../views/Coaches/CoachesView.vue'),
      meta: { breadcrumb: 'Antrenori' },
    },
    {
      path: '/add-coach',
      name: 'add-coach',
      component: () => import('../views/Coaches/AddCoachView.vue'),
      meta: { breadcrumb: 'Adăugare antrenor' },
    },
    {
      path: '/edit-coach/:id',
      name: 'edit-coach',
      component: () => import('../views/Coaches/EditCoachView.vue'),
      meta: { breadcrumb: 'Editare antrenor' },
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
    // {
    //   path: '/home',
    //   name: 'home',
    //   component: () => import('../home/acasa.html')
    // },
    
  ]
})

export default router
