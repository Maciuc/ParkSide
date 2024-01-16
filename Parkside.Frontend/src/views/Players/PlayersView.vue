<template>
  <div>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Jucatori</div>
      </div>

      <div class="col-auto">
        <router-link :to="{ name: 'add-player' }" class="button green">
          Adaugă
          <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
        </router-link>
      </div>
    </div>

    <div class="row d-flex flex-row mt-3 new-form">
      <div class="col-xxl-4 col-xl-4 col-lg-5 col-md-6 col-sm-8">
        <div class="input-group-custom mb-3">
          <div class="d-flex">
            <div class="d-flex justify-content-center align-items-center search-separator">
              <font-awesome-icon
                class="search_icon"
                :icon="['fas', 'magnifying-glass']"
                style="color: #688088"
              />
              <div class="separator"></div>
            </div>
          <input
            type="text"
            class="form-control search"
            placeholder="Caută partener"
            aria-label="Username"
            aria-describedby="basic-addon1"
            v-model="filter.SearchText"
            v-on:keyup.enter="GetAllPartners()"
          />
          </div>
        </div>
      </div>


      <div class="col-xxl-3 col-xl-4 col-lg-5 col-md-6 col-sm-8 custom-control">
        <select
          class="form-select form-control"
          aria-label="Default select example"
          v-model="filter.RoleFilter"
          @change="GetAllPlayers()"
        >
          <option value="" selected>Rol</option>
          <option v-for="(fnct, index) in Roles" :key="index">
            {{ fnct.name }}
          </option>
        </select>
      </div>
    </div>

    <table class="table table-custom">
      <thead>
        <tr>
          <th width="20%" @click="OrderBy('name')" class="cursor-pointer">
            <font-awesome-icon
              v-if="filter.OrderBy === 'name'"
              :icon="['fas', 'arrow-up-wide-short']"
              style="color: #29be00"
              rotation="180"
              size="xl"
              class="me-2"
            />

            <font-awesome-icon
              v-else-if="filter.OrderBy === 'name_desc'"
              :icon="['fas', 'arrow-up-short-wide']"
              rotation="180"
              style="color: #29be00"
              size="xl"
              class="me-2"
            />
            <font-awesome-icon
              v-else
              :icon="['fas', 'arrow-up-wide-short']"
              rotation="180"
              size="xl"
              class="me-2"
            />
            Nume & Avatar
          </th>

          <th scope="25" width="20%">Numar</th>
          <th scope="25" width="30%">Inaltime</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(player, index) in PlayersList.Items" :key="index">
          <td>
            <div class="d-flex align-items-center">
              <div class="img-container-avatar me-2">
                <img
                  :src="ShowDynamicImage(player.ImageBase64)"
                  class="me-2 icon-avatar"
                />
              </div>

              <span>{{ player.Lastname }}</span>
              <span>{{ player.FirstName }}</span>
            </div>
          </td>
          <td>{{ player.Number }}</td>
          <td>{{ player.Height }}</td>

          <td>
            <div class="editButtons">
              <router-link
                :to="{ name: 'edit-player', params: { id: player.Id } }"
                class="button-edit"
              >
                <font-awesome-icon :icon="['far', 'pen-to-square']" />
              </router-link>

              <button
                type="button"
                class="button-delete"
                @click="DeletePlayer(player.Id)"
              >
                <font-awesome-icon :icon="['fas', 'trash']" />
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>

    <Pagination
      :totalPages="PlayersList.NumberOfPages"
      :currentPage="filter.PageNumber"
      @pagechanged="GetAllPlayers"
    />
  </div>
</template>

<script>
import Pagination from "../../components/Pagination.vue";

export default {
  name: "SocialMedia",
  components: {
    Pagination,
  },
  data() {
    return {
      filter: {
        SearchText: "",
        PageNumber: 1,
        OrderBy: "name",
        RoleFilter: "",
      },
      PlayersList: {
        Items: [],
        NumberOfPages: 0,
      },
      DuplicatedId: "",
      Roles: [
        { name: "Centrali" },
        { name: "Pivoti" },
        { name: "Interi" },
        { name: "Extreme" },
        { name: "Portari" },
      ],
    };
  },
  methods: {
    ShowDynamicImage(imagePath) {
      if (!imagePath) {
        return `src/images/user.png`;
      }
      return imagePath;
    },

    GetAllPlayers(page) {
      this.filter.PageNumber = 1;
      if (page) {
        this.filter.PageNumber = page;
      }
      const searchParams = {
        OrderBy: this.filter.OrderBy,
        PageNumber: this.filter.PageNumber,
        PageSize: 1,
        NameSearch: this.filter.SearchText,
        Role: this.filter.RoleFilter,
      };
      this.$axios
        .get(`https://jsonplaceholder.typicode.com/posts/1?${new URLSearchParams(searchParams)}`) ///api/Player/getPlayers
        .then((response) => {
          console.log(searchParams);
          //this.PlayersList = response.data;
          this.PlayersList.Items = [
            {
              Id: 1,
              Lastname: "Ostafe",
              FirstName: "Rares",
              Role: "Administrator",
              Number: 22,
              Height: 1.83,
              Image: "",
            },
            {
              Id: 2,
              Lastname: "Ostafe",
              FirstName: "Rares",
              Role: "Administrator",
              Number: 22,
              Height: 1.83,
              Image: "",
            },
            {
              Id: 3,
              Lastname: "Ostafe",
              FirstName: "Rares",
              Role: "Administrator",
              Number: 22,
              Height: 1.83,
              Image: "",
            },
          ];
        })
        .catch((error) => {
          console.log(error);
        });
    },

    DeletePlayer(id) {
      this.$axios
        .delete(`/api/Player/deletePlayer/${id}`)
        .then((response) => {
          this.GetAllPlayers();
          console.log(`Deleted news with ID ${id}`);
        })
        .catch((error) => {
          console.error(error);
        });
    },

    OrderBy(orderBy) {
      if (this.filter.OrderBy.includes("_desc")) {
        this.filter.OrderBy = orderBy;
      } else {
        this.filter.OrderBy = orderBy + "_desc";
      }

      this.GetAllPlayers();
    },
  },

  created() {
    this.GetAllPlayers();
  },
};
</script>
