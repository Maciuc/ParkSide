<template>
  <div class="row header-section align-items-center">
    <div class="col">
      <div class="title-page">Staff</div>
    </div>

    <div class="col-auto">
      <router-link :to="{ name: 'add-stuff' }" class="button green">
        Adaugă
        <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
      </router-link>
    </div>
  </div>

  <div class="row d-flex flex-row mt-3 new-form">
    <div class="col-xxl-4 col-xl-4 col-lg-5 col-md-6 col-sm-6">
      <div class="input-group-custom mb-3">
        <div class="d-flex">
          <div
            class="d-flex justify-content-center align-items-center search-separator"
          >
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
            placeholder="Caută staff"
            aria-label="Username"
            aria-describedby="basic-addon1"
            v-model="filter.SearchText"
            v-on:keyup.enter="GetAllStuffes()"
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
            Nume
          </th>

          <th scope="20" width="20%">Descriere</th>
          <th scope="20" width="20%">Data nastere</th>
          <th scope="20" width="20%">Inaltime</th>
          <th scope="20" width="20%">Nationalitate</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(stuff, index) in StuffesList.Items" :key="index">
          <td>
            <div class="d-flex align-items-center">
              <div class="img-container-avatar me-2">
                <img
                  :src="ShowDynamicImage(stuff.ImageBase64)"
                  class="me-2 icon-avatar"
                />
              </div>

              <span>{{ stuff.LastName + " " + stuff.FirstName }}</span>
            </div>
          </td>
          <td>{{ stuff.Description }}</td>
          <td>{{ stuff.BirthDate }}</td>
          <td>{{ stuff.Height }}</td>
          <td>{{ stuff.Nationality }}</td>

          <td>
            <div class="editButtons">
              <router-link
                :to="{ name: 'edit-stuff', params: { id: stuff.Id } }"
                class="button-edit"
              >
                <font-awesome-icon :icon="['far', 'pen-to-square']" />
              </router-link>

              <button
                type="button"
                class="button-delete"
                @click="DeleteStuff(stuff.Id)"
              >
                <font-awesome-icon :icon="['fas', 'trash']" />
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>

    <Pagination
      :totalPages="StuffesList.NumberOfPages"
      :currentPage="filter.PageNumber"
      @pagechanged="GetAllStuffes"
    />
  </div>
</template>

<script>
import Pagination from "../../components/Pagination.vue";

export default {
  name: "Stuffes",
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
      StuffesList: {
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

    GetAllStuffes(page) {
      this.filter.PageNumber = 1;
      if (page) {
        this.filter.PageNumber = page;
      }
      const searchParams = {
        OrderBy: this.filter.OrderBy,
        PageNumber: this.filter.PageNumber,
        PageSize: 9,
        NameSearch: this.filter.SearchText,
      };
      this.$axios
        .get(`/api/Stuff/getStuffs/?${new URLSearchParams(searchParams)}`)
        .then((response) => {
          console.log(searchParams);
          this.StuffesList = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    DeleteStuff(id) {
      this.$axios
        .delete(`/api/Stuff/deleteStuff/${id}`)
        .then((response) => {
          this.GetAllStuffes();
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

      this.GetAllStuffes();
    },
  },

  created() {
    this.GetAllStuffes();
  },
};
</script>
