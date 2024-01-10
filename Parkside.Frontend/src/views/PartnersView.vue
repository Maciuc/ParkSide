<template>
  <div>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Parteneri</div>
      </div>

      <div class="col-auto">
        <button class="button green" @click="OpenModalAddPartner">
          <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
          Adaugă
        </button>
        <AddPartnerComponent
          @get-list="GetAllPartners()"
          ref="addPartnerModal"
        ></AddPartnerComponent>
      </div>
    </div>

    <div class="row d-flex flex-row mt-3 new-form">
      <div class="col-xxl-4 col-xl-4 col-lg-5 col-md-6 col-sm-8">
        <div class="input-group mb-3">
          <span class="input-group-text"
            ><font-awesome-icon
              class="search_icon"
              :icon="['fas', 'magnifying-glass']"
              style="color: #688088"
          /></span>
          <input
            type="text"
            class="form-control"
            placeholder="Caută partener"
            aria-label="Username"
            aria-describedby="basic-addon1"
            v-model="filter.SearchText"
            v-on:keyup.enter="GetAllPartners()"
          />
        </div>
      </div>
    </div>
    <div class="custom-card-partners">
      <div class="row gx-3 gy-3">
        <div
          v-for="(partner, index) in PartnersList.Items"
          :key="index"
          class="col-xxl-2 col-xl-2 col-lg-3 col-md-4 col-sm-6"
        >
          <div class="card h-100">
            <div class="buttons-group d-flex gap-1">
              <button
                class="button-edit"
                data-bs-toggle="modal"
                type="button"
                data-bs-target="#partner-edit-modal"
                @click="GetPartnerForEdit(partner.Id)"
              >
                <font-awesome-icon :icon="['far', 'pen-to-square']" />
              </button>
              <button
                type="button"
                class="button-delete"
                @click="DeletePartner(partner.Id)"
              >
                <font-awesome-icon :icon="['fas', 'trash']" />
              </button>
            </div>

            <div
              class="card-image d-flex justify-content-center align-items-center"
            >
              <img
                :src="ShowDynamicImage(partner.LogoBase64)"
                class="card-img-top"
              />
            </div>

            <div
              class="card-body border-top ps-3 d-flex justify-content-between"
              :class="{ 'partner-no-visibility': partner.IsVisible === false }"
            >
              {{ partner.Name }}
              <button
                class="button-eye"
                @click="EditVisibilityPartner(partner.Id)"
              >
                <font-awesome-icon
                  :icon="['far', 'eye']"
                  v-if="partner.IsVisible === true"
                />
                <font-awesome-icon
                  :icon="['far', 'eye-slash']"
                  v-if="partner.IsVisible === false"
                />
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>

    <Pagination
      :totalPages="PartnersList.NumberOfPages"
      :currentPage="filter.PageNumber"
      @pagechanged="GetAllPartners"
    />

    <EditPartnerComponent
      :editedPartner="selectedPartnerForEdit"
      @get-list="GetAllPartners()"
    />
  </div>
</template>

<script>
import AddPartnerComponent from "./../components/Modals/Partners/AddPartnerComponent.vue";
import EditPartnerComponent from "./../components/Modals/Partners/EditPartnerComponent.vue";
import Pagination from "../components/Pagination.vue";

export default {
  name: "Partners",
  components: {
    AddPartnerComponent,
    EditPartnerComponent,
    Pagination,
  },
  data() {
    return {
      selectedPartnerForEdit: [],

      filter: {
        SearchText: "",
        PageNumber: 1,
        OrderBy: "name",
      },
      PartnersList: {
        Items: [],
        NumberOfPages: 0,
      },

      platforms: [
        { name: "Facebook" },
        { name: "Twitter" },
        { name: "WhatsApp" },
        { name: "Linkedin" },
        { name: "Instagram" },
      ],
    };
  },
  methods: {
    ShowDynamicImage(imagePath) {
      if (!imagePath) {
        return `src/images/partner.png`;
      }
      return imagePath;
    },

    OpenModalAddPartner() {
      $("#partner-add-modal").modal("show");
      this.$refs.addPartnerModal.ClearModal();
    },

    GetPartnerForEdit(id) {
      console.log(id);
      this.$axios
        .get(`/api/Partners/getPartner/${id}`)
        .then((response) => {
          this.selectedPartnerForEdit = response.data;
          console.log(this);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    GetAllPartners(page) {
      this.filter.PageNumber = 1;
      if (page) {
        this.filter.PageNumber = page;
      }
      const searchParams = {
        OrderBy: this.filter.OrderBy,
        PageNumber: this.filter.PageNumber,
        PageSize: 18,
        NameSearch: this.filter.SearchText,
      };
      this.$axios
        .get(`/api/Partners/getPartners?${new URLSearchParams(searchParams)}`)
        .then((response) => {
          console.log(searchParams);
          this.PartnersList = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    DeletePartner(id) {
      this.$axios
        .delete(`/api/Partners/deletePartner/${id}`)
        .then((response) => {
          this.GetAllPartners();
          console.log(`Deleted partner with ID ${id}`);
        })
        .catch((error) => {
          console.error(error);
        });
    },
    EditVisibilityPartner(id) {
      this.PartnersList.Items.find((x) => x.Id == id).IsVisible =
        !this.PartnersList.Items.find((x) => x.Id == id).IsVisible;
      this.$axios
        .put(`/api/Partners/updateVisibilityPartner/${id}`)
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.error(error);
        });
    },
  },

  created() {
    this.GetAllPartners();
  },
};
</script>

<style scoped>
.custom-card-partners .card {
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

.custom-card-partners .card:hover {
  transition: box-shadow 0.3s;
  box-shadow: 0 0 14px -2px rgba(0, 0, 0, 0.2);
}

.custom-card-partners .card-image {
  height: 120px;
  margin: 5px;
}
.custom-card-partners .card-img-top {
  height: 100%;
  width: 100%;
  object-fit: contain;
}

.custom-card-partners .buttons-group {
  position: absolute;
  top: 5px;
  right: 5px;
}
.custom-card-partners .button-delete,
.custom-card-partners .button-edit {
  width: 24px;
  height: 24px;
  background-color: white;
  border-radius: 3px;
  border: 1px solid #d9dee0;
  color: #95a6ac;
  opacity: 90%;
}

.custom-card-partners button:hover {
  background-color: #f0f2f3;
  border: 1px solid #00000033;
  color: #4e4e4e;
}

.custom-card-partners .card-body {
  flex-grow: 1;
  align-items: center;
  padding-top: 10px;
  padding-bottom: 10px;
  color: black;
}
.custom-card-partners .partner-no-visibility {
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
