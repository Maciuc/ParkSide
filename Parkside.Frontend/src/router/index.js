import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/members',
      name: 'members',
      component: () => import('../views/Members/MembersView.vue'),
      meta: { breadcrumb: 'Membri' },
    },
    {
      path: '/add-member',
      name: 'add-member',
      component: () => import('../views/Members/AddMemberView.vue'),
      meta: { breadcrumb: 'Adăugare membru' },
    },
    {
      path: '/edit-member/:id',
      name: 'edit-member',
      component: () => import('../views/Members/EditMemberView.vue'),
      meta: { breadcrumb: 'Editare membru' },
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
      path: '/partners',
      name: 'partners',
      component: () => import('../views/PartnersView.vue'),
      meta: { breadcrumb: 'Parteneri' },
      children: [
      ],
    },
    
  ]
})

export default router
