<template>
  <div class="breadcrumb align-items-center mt-4" aria-label="breadcrumbs">
    <ul class="d-flex flex-start align-items-center">
      <li><div>Dashboard</div></li>
      
      <li v-for="(crumb, index) in breadcrumbs" :key="index">
        <font-awesome-icon
          class="ms-3 me-3"
          :icon="['fas', 'sort-down']"
          rotation="270"
          style="color: #688088"
          
        />
        <router-link :to="crumb.to">{{ crumb.label }}</router-link>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  computed: {
    breadcrumbs() {
      const crumbs = [];
      this.$route.matched.forEach((route) => {
        if (route.meta && route.meta.breadcrumb) {
          crumbs.push({
            label: route.meta.breadcrumb,
            to: route.path,
          });
        }
      });
      return crumbs;
    },
  },
};
</script>

<style>
.breadcrumb ul {
  text-decoration: none;
  padding-left: 0px;
  margin-bottom: 0px;
}
.breadcrumb div {
  font-size: 11.5px;
  font-family: "Raleway";
  line-height: 12px;
  letter-spacing: 0.7px;
  font-weight: 400;
}

.breadcrumb li {
  font-size: 11.5px;
  font-weight: 700;
  list-style: none;
  font-family: "Raleway";
  line-height: 12px;
  letter-spacing: 0.6px;
  color: #688088;
}

.breadcrumb a {
  text-decoration: none;
  color: #2da017;
}

.router-link-active{
  color: blue;
}
</style>
