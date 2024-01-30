<template>
  <section>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Echipe</div>
      </div>

      <div class="col-auto">
        <button class="button green" @click="OpenModalAddTeam">
          <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
          Adaugă
        </button>
        <AddTeamModalComponent
          @get-list="GetAllTeams()"
          ref="addTeamModal"
        ></AddTeamModalComponent>
      </div>
    </div>

    <div class="row mt-3 new-form">
      <div class="col-xxl-3 col-xl-3 col-lg-3 col-md-4 col-sm-6">
        <div class="input-group-custom mb-3 custom-control">
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
            placeholder="Caută echipa"
            aria-label="Username"
            aria-describedby="basic-addon1"
            v-model="filter.SearchText"
            v-on:keyup.enter="GetAllTeams()"
          />
        </div>
      </div>
    </div>
    <div style="overflow-x: auto">
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
              <span>Nume echipa</span>
            </th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(type, index) in teams.Items" :key="index">
            <td>
              <div class="d-flex align-items-center">
                <div class="img-container-avatar me-3">
                  <img
                    :src="ShowDynamicImage(type.ImageBase64)"
                    class="me-2 icon-avatar"
                  />
                </div>

                <span>{{ type.Name }}</span>
              </div>
            </td>
            <td>
              <div class="editButtons">
                <button
                  class="button-edit"
                  data-bs-toggle="modal"
                  type="button"
                  data-bs-target="#team-edit-modal"
                  @click="GetTeamForEdit(type.Id)"
                >
                  <font-awesome-icon :icon="['far', 'pen-to-square']" />
                </button>

                <button
                  type="button"
                  class="button-delete"
                  @click="DeleteTeam(type.Id)"
                >
                  <font-awesome-icon :icon="['fas', 'trash']" />
                </button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>

    <Pagination
      :totalPages="teams.NumberOfPages"
      :currentPage="filter.PageNumber"
      @pagechanged="GetAllTeams"
    />

    <EditTeamModalComponent
      :editedTeam="selectedTeamForEdit"
      @get-list="GetAllTeams"
    />
  </section>
</template>

<script>
import AddTeamModalComponent from "../components/Modals/Teams/AddTeamComponent.vue";
import EditTeamModalComponent from "../components/Modals/Teams/EditTeamComponent.vue";
import Pagination from "../components/Pagination.vue";

export default {
  name: "Teams",
  components: {
    AddTeamModalComponent,
    EditTeamModalComponent,
    Pagination,
  },
  data() {
    return {
      selectedTeamForEdit: {
        Id: 0,
        Name: "",
        ImageBase64: "",
      },

      filter: {
        SearchText: "",
        PageNumber: 1,
        OrderBy: "name",
      },
      teams: {
        Items: [],
        NumberOfPages: 1,
      },
    };
  },
  methods: {
    ShowDynamicImage(imagePath) {
      if (!imagePath) {
        return `src/images/NoImageSelected.png`;
      }
      return imagePath;
    },
    OpenModalAddTeam() {
      $("#team-add-modal").modal("show");
      this.$refs.addTeamModal.ClearModal();
    },

    GetTeamForEdit(id) {
      this.$axios
        .get(`/api/Team/getTeam/${id}`)
        .then((response) => {
          this.selectedTeamForEdit = response.data;
          console.log(this.selectedTeamForEdit);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    GetAllTeams(page) {
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
        .get(`api/Team/getTeams?${new URLSearchParams(searchParams)}`)
        .then((response) => {
          console.log(searchParams);
          this.teams = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    DeleteTeam(id) {
      this.$axios
        .delete(`/api/Team/deleteTeam/${id}`)
        .then((response) => {
          this.GetAllTeams();
          console.log(`Deleted team with ID ${id}`);
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

      this.GetAllTeams();
    },
  },

  created() {
    this.GetAllTeams();
  },
};
</script>
