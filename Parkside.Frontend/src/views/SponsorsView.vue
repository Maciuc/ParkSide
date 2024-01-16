<template>
  <div>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Sponsori</div>
      </div>

      <div class="col-auto">
        <button class="button green" @click="OpenModalAddSponsor">
          Adaugă
          <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
        </button>
        <AddSponsorComponent
          @get-list="GetAllSponsors()"
          ref="addSponsorModal"
        ></AddSponsorComponent>
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
            placeholder="Caută sponsor"
            aria-label="Username"
            aria-describedby="basic-addon1"
            v-model="filter.SearchText"
            v-on:keyup.enter="GetAllSponsors()"
          />
          </div>
        </div>
      </div>
    </div>

    <div class="custom-card-sponsors">
      <div class="row gx-3 gy-3">
        <div
          v-for="(sponsor, index) in SponsorsList.Items"
          :key="index"
          class="col-xxl-2 col-xl-2 col-lg-3 col-md-4 col-sm-6"
        >
          <div class="card h-100">
            <div class="buttons-group d-flex gap-1">
              <button
                class="button-edit"
                data-bs-toggle="modal"
                type="button"
                data-bs-target="#sponsor-edit-modal"
                @click="GetSponsorForEdit(sponsor.Id)"
              >
                <font-awesome-icon :icon="['far', 'pen-to-square']" />
              </button>
              <button
                type="button"
                class="button-delete"
                @click="DeleteSponsor(sponsor.Id)"
              >
                <font-awesome-icon :icon="['fas', 'trash']" />
              </button>
            </div>

            <div
              class="card-image d-flex justify-content-center align-items-center"
            >
              <img
                :src="ShowDynamicImage(sponsor.ImageBase64)"
                class="card-img-top"
              />
            </div>
            <div class="card-body border-top ps-3 d-flex justify-content-between">
              {{ sponsor.Name.length>50 ? sponsor.Name.substring(0, 50)+ "..." : sponsor.Name}}
            </div>
          </div>
        </div>
      </div>
    </div>

    <Pagination
      :totalPages="SponsorsList.NumberOfPages"
      :currentPage="filter.PageNumber"
      @pagechanged="GetAllSponsors"
    />

    <EditSponsorComponent
      :editedSponsor="selectedSponsorForEdit"
      @get-list="GetAllSponsors()"
    />
  </div>
</template>

<script>
import AddSponsorComponent from "./../components/Modals/Sponsors/AddSponsorComponent.vue";
import EditSponsorComponent from "./../components/Modals/Sponsors/EditSponsorComponent.vue";
import Pagination from "../components/Pagination.vue";

export default {
  name: "Sponsors",
  components: {
    AddSponsorComponent,
    EditSponsorComponent,
    Pagination,
  },
  data() {
    return {
      selectedSponsorForEdit: [],

      filter: {
        SearchText: "",
        PageNumber: 1,
      },
      SponsorsList: {
        Items: [],
        NumberOfPages: 0,
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

    OpenModalAddSponsor() {
      $("#sponsor-add-modal").modal("show");
      this.$refs.addSponsorModal.ClearModal();
    },

    GetSponsorForEdit(id) {
      console.log(id);
      this.$axios
        .get(`/api/Sponsor/getSponsor/${id}`)
        .then((response) => {
          this.selectedSponsorForEdit = response.data;
          console.log(this);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    GetAllSponsors(page) {
      this.filter.PageNumber = 1;
      if (page) {
        this.filter.PageNumber = page;
      }
      const searchParams = {
        PageNumber: this.filter.PageNumber,
        PageSize: 18,
        NameSearch: this.filter.SearchText,
      };
      this.$axios
        .get(`/api/Sponsor/getSponsors?${new URLSearchParams(searchParams)}`)
        .then((response) => {
          console.log(searchParams);
          this.SponsorsList = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    DeleteSponsor(id) {
      this.$axios
        .delete(`/api/Sponsor/deleteSponsor/${id}`)
        .then((response) => {
          this.GetAllSponsors();
          console.log(`Deleted sponsor with ID ${id}`);
        })
        .catch((error) => {
          console.error(error);
        });
    },
  },

  created() {
    this.GetAllSponsors();
  },
};
</script>

<style scoped>
.custom-card-sponsors .card {
  border-color: #e5e5e5;
  border-radius: 10px;
  position: relative;
  max-width: 220px;

  font-family: "Raleway";
  font-size: 11px;
  font-weight: 600;
  letter-spacing: 0.4px;
  line-height: 12px;
}

.custom-card-sponsors .card:hover {
  transition: box-shadow 0.3s;
  box-shadow: 0 0 14px -2px rgba(0, 0, 0, 0.2);
}

.custom-card-sponsors .card-image {
  height: 120px;
  margin: 5px;
}
.custom-card-sponsors .card-img-top {
  height: 100%;
  width: 100%;
  object-fit: contain;
}

.custom-card-sponsors .buttons-group {
  position: absolute;
  top: 5px;
  right: 5px;
}
.custom-card-sponsors .button-delete,
.custom-card-sponsors .button-edit {
  width: 24px;
  height: 24px;
  background-color: white;
  border-radius: 3px;
  border: 1px solid #d9dee0;
  color: #95a6ac;
  opacity: 90%;
}

.custom-card-sponsors button:hover {
  background-color: #f0f2f3;
  border: 1px solid #00000033;
  color: #4e4e4e;
}

.custom-card-sponsors .card-body {
  flex-grow: 1;
  align-items: center;
  padding-top: 10px;
  padding-bottom: 10px;
  color: black;
}
.custom-card-sponsors .sponsor-no-visibility {
  color: #688088;
}

.button-eye {
  display: flex;
  justify-content: center;
  align-items: center;
  width: 20px;
  height: 20px;
  background-color: white;
  border-radius: 3px;
  color: inherit;
  opacity: 90%;
}
</style>
