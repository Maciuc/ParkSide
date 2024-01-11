<script setup>
import { RouterLink, RouterView } from "vue-router";
</script>

<template>
  <link rel="preconnect" href="https://fonts.googleapis.com" />
  <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
  <link
    href="https://fonts.googleapis.com/css2?family=Raleway:wght@400;500;600;700&display=swap"
    rel="stylesheet"
  />

  <link
    href="node_modules/froala-editor/css/froala_editor.pkgd.min.css"
    rel="stylesheet"
    type="text/css"
  />

  <div class="page d-flex">
    <aside class="d-flex flex-column">
      <img class="logo img-fluid container align-self-center" src="@/images/logo.png" />

      <nav>
        <div class="categoryTitle">MENIU PRINCIPAL</div>
        <ul class="dashboard-menu">

          <router-link
            class="nav-link d-flex"
            :to="{ name: 'news' }"
            exact
          >
            <font-awesome-icon class="img-link" :icon="['far', 'newspaper']"  />
            <div >Știri</div>
          </router-link>

          <router-link
            tag="li"
            class="nav-link d-flex"
            :to="{ name: 'partners' }"
            exact
          >
            <font-awesome-icon class="img-link" :icon="['far', 'handshake']"  />
            <div>Parteneri</div>
          </router-link>

          <router-link
            tag="li"
            class="nav-link d-flex"
            :to="{ name: 'players' }"
            exact
          >
            <font-awesome-icon class="img-link" :icon="['fas', 'people-group']"  />
            <div>Jucatori</div>
          </router-link>

          <router-link
            tag="li"
            class="nav-link d-flex"
            :to="{ name: 'Users' }"
            exact
          >
            <font-awesome-icon class="img-link" :icon="['fas', 'user-gear']"  />
            <div>Utilizatori</div>
          </router-link>
        </ul>
      </nav>
    </aside>

    <div class="main-section">
      <BreadCrumb></BreadCrumb>

      <RouterView />
    </div>
  </div>
</template>

<script>
import BreadCrumb from "./components/BreadCrumb.vue";
export default {
  name: "App",
  components: {
    BreadCrumb,
  },
};
</script>

<style scoped>
aside {
  border-right: 1px solid #0000001a;
  padding-left: 7px;
  padding-right: 7px;
  justify-content: flex-start;
  background-color: #fafafe;
  width: 14%;
  min-width: 160px;
}

.main-section {
  width: 100%;
  padding-left: 2rem;
  padding-right: 2rem;
}

.nav-link {
  color: #688088;
  font-family: "Raleway";
  font-weight: 400;
  font-size: 13px;
  line-height: 18px;
  letter-spacing: 0.6px ;
  padding-left: 10px;
  margin-right: 5px;
  justify-content: flex-start;
  align-items: center;
  height: 32px;
  
}
.router-link-active {
  color: black;
  font-weight: 600;
  background-color: #eceefd;
  border-radius: 8px;
}

.nav-link:hover{
  color: #3E4D52;
  background-color: #F0F2F3;
  border-radius: 8px;
}


.dashboard-menu {
  display: flex;
  flex-direction: column;
  padding-left: 5px;
  padding-bottom: 12px;
  /* border-bottom: 1px solid #E1E1E5; */
  gap: 4px;
}

.img-link{
  width: 20px;
  height: 20px;
  margin-right: 10px;
}

.categoryTitle {
  font-family: "Raleway";
  font-weight: 700;
  color: #1f2629;
  line-height: 12px;
  font-size: 0.65rem;
  margin-left: 5px;
  margin-bottom: 8px;
}

.logo {
  margin-top: 1rem;
  margin-bottom: 2rem;
  max-width: 150px;
}


</style>
