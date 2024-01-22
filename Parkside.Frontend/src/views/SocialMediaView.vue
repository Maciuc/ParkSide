<template>
    <div>
      <div class="row header-section align-items-center">
        <div class="col">
          <div class="title-page">Linkuri social media</div>
        </div>
  
        <div class="col-auto">
          <button class="button green" @click="OpenModalAddLinkSocial">
            <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
            Adaugă
          </button>
          <AddSocialLinkComponent
            @get-list="GetAllLinks()"
            ref="addSocialLinkModal"
          ></AddSocialLinkComponent>
        </div>
      </div>
  
      <div class="row d-flex flex-row mt-3 new-form">
        <div class="col-xxl-3 col-xl-3 col-lg-3 col-md-4 col-sm-6 custom-control">
          <div class="input-group-custom mb-3">
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
        placeholder="Caută link"
              aria-label="Username"
              aria-describedby="basic-addon1"
              v-model="filter.SearchText"
              v-on:keyup.enter="GetAllLinks()"
            />
          </div>
        </div>
        <div class="col-xxl-3 col-xl-3 col-lg-3 col-md-4 col-sm-6 custom-control">
          <select
            class="form-select form-control"
            aria-label="Default select example"
            placeholder="Toate platformele"
            v-model="filter.SearchPlatform"
            @change="GetAllLinks()"
          >
            <option selected value="">Toate platformele</option>
            <option v-for="(plat, index) in platforms" :key="index">
              {{ plat.name }}
            </option>
          </select>
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
                <font-awesome-icon
                  v-else
                  :icon="['fas', 'arrow-up-wide-short']"
                  rotation="180"
                  size="xl"
                  class="me-2"
                />
                <span> Nume pagina</span>
               
              </th>
  
              <th scope="25" class="description" width="50%">Link</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(socialLink, index) in socialLinksList.Items" :key="index">
              <td>
                <div class="d-flex align-items-center">
                  <img
                    :src="`src/images/${ShowDynamicImage(
                      socialLink.Platform
                    )}.png`"
                    class="me-3 icon" 
                  />
                  <span>{{ socialLink.Name }}</span>
                </div>
              </td>
              <td>{{ socialLink.Link }}</td>
  
              <td>
                <div class="editButtons">
                  <button
                    class="button-edit"
                    data-bs-toggle="modal"
                    type="button"
                    data-bs-target="#social-link-edit-modal"
                    @click="GetLinkForEdit(socialLink.Id)"
                  >
                    <font-awesome-icon :icon="['far', 'pen-to-square']" />
                  </button>
  
                  <button
                    type="button"
                    class="button-delete"
                    @click="DeleteSocialLink(socialLink.Id)"
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
        :totalPages="socialLinksList.NumberOfPages"
        :currentPage="filter.PageNumber"
        @pagechanged="GetAllLinks"
      />
  
      <EditSocialLinkComponent
        :socialLink="selectedLinkForEdit"
        @get-list="GetAllLinks()"
      />
    </div>
  </template>
  
  <script>
  import AddSocialLinkComponent from "./../components/Modals/SocialMedia/AddSocialLinkComponent.vue";
  import EditSocialLinkComponent from "./../components/Modals/SocialMedia/EditSocialLinkComponent.vue";
  import Pagination from "../components/Pagination.vue";
  
  export default {
    name: "SocialMedia",
    components: {
      AddSocialLinkComponent,
      EditSocialLinkComponent,
      Pagination,
    },
    data() {
      return {
        selectedLinkForEdit: {
          Id: 0,
          Name: "",
          Description: "",
        },
  
        filter: {
          SearchText: "",
          PageNumber: 1,
          OrderBy: "name",
          SearchPlatform: "",
        },
        socialLinksList: {
          Items: [],
          NumberOfPages: 0,
        },
  
        platforms: [
          { name: "Facebook" },
          { name: "WhatsApp" },
          { name: "Instagram" },
          { name: "TikTok" },
          { name: "Twitter" },
        ],
      };
    },
    methods: {
      ShowDynamicImage(imagePath) {
        if (!imagePath) {
          return new URL("@/images/NoImageSelected.png", import.meta.url).href;
        }
        return `${imagePath}`;
      },
  
      OpenModalAddLinkSocial() {
        $("#social-link-add-modal").modal("show");
        this.$refs.addSocialLinkModal.ClearModal();
      },
  
      GetLinkForEdit(id) {
        console.log(id);
        this.$axios
          .get("/api/SocialMedia/getSocialMedias")
          .then((response) => {
            this.selectedLinkForEdit = this.socialLinksList.Items.find(
              (x) => x.Id == id
            );
            console.log(this);
          })
          .catch((error) => {
            console.log(error);
          });
      },
  
      GetAllLinks(page) {
        this.filter.PageNumber = 1;
        if (page) {
          this.filter.PageNumber = page;
        }
        const searchParams = {
          OrderBy: this.filter.OrderBy,
          PageNumber: this.filter.PageNumber,
          PageSize: 6,
          NameSearch: this.filter.SearchText,
          Platform: this.filter.SearchPlatform,
        };
        this.$axios
          .get(
            `/api/SocialMedia/getSocialMedias?${new URLSearchParams(
              searchParams
            )}`
          )
          .then((response) => {
            console.log(searchParams);
            this.socialLinksList = response.data;
          })
          .catch((error) => {
            console.log(error);
          });
      },
  
      DeleteSocialLink(id) {
        console.log(id + "asta e id-ul din deleteNews");
        this.$axios
          .delete(`/api/SocialMedia/deleteSocialMedia/${id}`)
          .then((response) => {
            this.GetAllLinks();
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
  
        this.GetAllLinks();
      },
    },
  
    created() {
      this.GetAllLinks();
    },
  };
  </script>
  
  <style scoped>
  .icon {
    width: 30px;
  }
  </style>
  