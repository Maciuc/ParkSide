<template>
      <div class="row header-section align-items-center">
        <div class="col">
          <div class="title-page">Antrenori</div>
        </div>
  
        <div class="col-auto">
          <router-link :to="{ name: 'add-coach' }" class="button green">
            Adaugă
            <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
          </router-link>
        </div>
      </div>
  
      <div class="row d-flex flex-row mt-3 new-form">
        <div class="col-xxl-4 col-xl-4 col-lg-5 col-md-6  col-sm-6 ">
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
              placeholder="Caută antrenor"
              aria-label="Username"
              aria-describedby="basic-addon1"
              v-model="filter.SearchText"
              v-on:keyup.enter="GetAllCoaches()"
            />
            </div>
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
  
            <th scope="20" width="20%">Echipa</th>
            <th scope="20" width="20%">Data nastere</th>
            <th scope="20" width="20%">Inaltime</th>
            <th scope="20" width="20%">Nationalitate</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(coach, index) in CoachesList.Items" :key="index">
            <td>
              <div class="d-flex align-items-center">
                <div class="img-container-avatar me-2">
                  <img
                    :src="ShowDynamicImage(coach.ImageBase64)"
                    class="me-2 icon-avatar"
                  />
                </div>
  
                <span>{{ coach.LastName + " " + coach.FirstName}}</span>
              </div>
            </td>
            <td>{{ coach.TeamName }}</td>
            <td>{{ coach.BirthDate }}</td>
            <td>{{ coach.Height }}</td>
            <td>{{ coach.Nationality }}</td>
  
            <td>
              <div class="editButtons">
                <router-link
                  :to="{ name: 'edit-coach', params: { id: coach.Id } }"
                  class="button-edit"
                >
                  <font-awesome-icon :icon="['far', 'pen-to-square']" />
                </router-link>
  
                <button
                  type="button"
                  class="button-delete"
                  @click="DeleteCoach(coach.Id)"
                >
                  <font-awesome-icon :icon="['fas', 'trash']" />
                </button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
  
      <Pagination
        :totalPages="CoachesList.NumberOfPages"
        :currentPage="filter.PageNumber"
        @pagechanged="GetAllCoaches"
      />
    </div>
  </template>
  
  <script>
  import Pagination from "../../components/Pagination.vue";
  
  export default {
    name: "Coaches",
    components: {
      Pagination,
    },
    data() {
      return {
        filter: {
          SearchText: "",
          PageNumber: 1,
          OrderBy: "name",
        },
        CoachesList: {
          Items: [],
          NumberOfPages: 0,
        },
        DuplicatedId: "",
      };
    },
    methods: {
      ShowDynamicImage(imagePath) {
        if (!imagePath) {
          return `src/images/user.png`;
        }
        return imagePath;
      },
  
      GetAllCoaches(page) {
        this.filter.PageNumber = 1;
        if (page) {
          this.filter.PageNumber = page;
        }
        const searchParams = {
          OrderBy: this.filter.OrderBy,
          PageNumber: this.filter.PageNumber,
          PageSize: 6,
          NameSearch: this.filter.SearchText,
        };
        this.$axios
          .get(`/api/Coach/getCoaches/?${new URLSearchParams(searchParams)}`)
          .then((response) => {
            console.log(searchParams);
            this.CoachesList = response.data;
          })
          .catch((error) => {
            console.log(error);
          });
      },
  
      DeleteCoach(id) {
        this.$axios
          .delete(`/api/Coach/deleteCoach/${id}`)
          .then((response) => {
            this.GetAllCoaches();
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
  
        this.GetAllCoaches();
      },
    },
  
    created() {
      this.GetAllCoaches();
    },
  };
  </script>
  