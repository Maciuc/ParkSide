<template>
    <section>
      <div class="row header-section align-items-center">
        <div class="col">
          <div class="title-page">Campionate</div>
        </div>
  
        <div class="col-auto">
          <button class="button green" @click="OpenModalAddChampionship">
            <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
            Adaugă
          </button>
          <AddChampionshipModalComponent
            @get-list="GetAllChampionships()"
            ref="addChampionshipModal"
          ></AddChampionshipModalComponent>
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
              placeholder="Caută campionat"
              aria-label="Username"
              aria-describedby="basic-addon1"
              v-model="filter.SearchText"
              v-on:keyup.enter="GetAllChampionships()"
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
                <span >Nume campionat</span>
              </th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(type, index) in championships.Items" :key="index">
              <td>
                <div class="d-flex align-items-center">
                  <div class="img-container-avatar me-3">
                    <img :src="ShowDynamicImage(type.ImageBase64)" class="me-2 icon-avatar" />
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
                    data-bs-target="#championship-edit-modal"
                    @click="GetChampionshipForEdit(type.Id)"
                  >
                    <font-awesome-icon :icon="['far', 'pen-to-square']" />
                  </button>
  
                  <button
                    type="button"
                    class="button-delete"
                    @click="DeleteChampionship(type.Id)"
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
        :totalPages="championships.NumberOfPages"
        :currentPage="filter.PageNumber"
        @pagechanged="GetAllChampionships"
      />
  
      <EditChampionshipModalComponent
        :editedChampionship="selectedChampionshipForEdit"
        @get-list="GetAllChampionships"
      /> 
    </section>
  </template>
  
  <script>
  import AddChampionshipModalComponent from "../components/Modals/Championships/AddChampionshipComponent.vue";
  import EditChampionshipModalComponent from "../components/Modals/Championships/EditChampionshipComponent.vue";
  import Pagination from "../components/Pagination.vue";
  
  export default {
    name: "Championships",
    components: {
      AddChampionshipModalComponent,
      EditChampionshipModalComponent,
      Pagination,
    },
    data() {
      return {
        selectedChampionshipForEdit: {
          Id: 0,
          Name: "",
          ImageBase64: "",
        },
  
        filter: {
          SearchText: "",
          PageNumber: 1,
          OrderBy: "name",
        },
        championships: {
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
      OpenModalAddChampionship() {
        $("#championship-add-modal").modal("show");
        this.$refs.addChampionshipModal.ClearModal();
      },
  
      GetChampionshipForEdit(id) {
        console.log(id);
        this.$axios
          .get(`/api/Championship/getChampionships`)
          .then((response) => {
            this.selectedChampionshipForEdit = JSON.parse(
              JSON.stringify(this.championships.Items.find((x) => x.Id == id))
            );
            console.log(this);
          })
          .catch((error) => {
            console.log(error);
          });
      },
  
      GetAllChampionships(page) {
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
          .get(
            `api/Championship/getChampionships?${new URLSearchParams(
              searchParams
            )}`
          )
          .then((response) => {
            console.log(searchParams);
            this.championships = response.data;
          })
          .catch((error) => {
            console.log(error);
          });
      },
  
      DeleteChampionship(id) {
        this.$axios
          .delete(`/api/Championship/deleteChampionship/${id}`)
          .then((response) => {
            this.GetAllChampionships();
            console.log(`Deleted championship with ID ${id}`);
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
  
        this.GetAllChampionships();
      },
    },
  
    created() {
      this.GetAllChampionships();
    },
  };
  </script>
  