<template>
  <div>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Știri</div>
      </div>

      <div class="col-auto">
        <router-link :to="{ name: 'add-news' }" class="button green">
          Adaugă
          <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
        </router-link>
      </div>
    </div>

    <div class="row d-flex flex-row mt-3 new-form">
      <div class="col">
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
            placeholder="Caută știre"
            aria-label="Username"
            aria-describedby="basic-addon1"
            v-model="filter.SearchText"
            v-on:keyup.enter="GetAllNews()"
          />
        </div>
      </div>
      <!-- <div class="col custom-control">
        <select
          class="form-select form-control"
          aria-label="Default select example"
          v-model="filter.NewsType"
          @change="GetAllNews()"
        >
          <option value="" selected>Toate tipurile</option>
          <option v-for="(type, index) in functions" :key="index">
            {{ type.name }}
          </option>
        </select>
      </div> -->

    

      <div class="col custom-date-picker">
        <VueDatePicker
          v-model="filter.PublishedDate"
          auto-apply
          utc
          :enable-time-picker="false"
          @update:model-value="GetAllNews()"
          placeholder="Dată publicare"
        ></VueDatePicker>
      </div>

      <div class="col custom-date-picker">
        <VueDatePicker
          v-model="filter.CreatedDate"
          auto-apply
          utc
          :enable-time-picker="false"
          placeholder="Dată adăugare"
          @update:model-value="GetAllNews()"
        ></VueDatePicker>
      </div>







      <div class="col custom-dropdown">
        <div class="custom dropdown">
          <button
            class="btn btn-secondary justify-content-between"
            type="button"
            id="dropdownStateNews"
            aria-expanded="false"
            @click="OpenDropdownState"
            :class="{ 'dropdown-toggle': !stateNews }"
          >
            <div>
              <span v-if="filter.isPublished===true">
                Publicate
              </span>
              <span v-else-if="filter.isPublished===false">
                Schițe
              </span>
              <span v-else>Publicate, schițe </span>
            </div>

            <button
              v-if="stateNews"
              @click="CloseAndResetDropdownState"
              class="button-close justify-content-end"
            >
              <font-awesome-icon :icon="['fas', 'xmark']" />
            </button>
          </button>
          <ul class="dropdown-menu" aria-labelledby="dropdownNewsTypesAddNews">
            <li>
              <a
                class="dropdown-item"
                href="#"
                @click="SelectStateNews('Publicate')"
                >Publicate</a
              >
            </li>
            <li>
              <a
                class="dropdown-item"
                href="#"
                @click="SelectStateNews('Schițe')"
                >Schițe</a
              >
            </li>
          </ul>
        </div>
      </div>






      <div class="col custom-dropdown">
        <div class="custom dropdown">
          <button
            class="btn btn-secondary"
            type="button"
            id="dropdownAuthor"
            aria-expanded="false"
            @click="OpenDropdownAuthor"
            :class="{ 'dropdown-toggle': !selectedUserAuthor.Name }"
          >
            <div>
              <span v-if="selectedUserAuthor.Name">
                {{ selectedUserAuthor.Name }}
              </span>
              <span v-else>Autor</span>
            </div>

            <button
              v-if="selectedUserAuthor.Name"
              @click="CloseAndResetDropdownAuthor"
              class="justify-content-end button-close"
            >
              <font-awesome-icon :icon="['fas', 'xmark']" />
            </button>
          </button>
          <ul class="dropdown-menu">
            <li
              v-for="(user, index) in usersAuthorList"
              :key="index"
              :value="user.Name"
            >
              <a class="dropdown-item" href="#" @click="SelectUser(user)">{{
                user.Name
              }}</a>
            </li>
          </ul>
        </div>
      </div>
    </div>

    <table class="table table-custom">
      <thead>
        <tr>
          <th width="15%" @click="OrderBy('name')" class="cursor-pointer">
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
          <th scope="25" width="15%">Descriere</th>
          <th
            scope="25"
            width="15%"
            @click="OrderBy('publishedDate')"
            class="cursor-pointer"
          >
            <font-awesome-icon
              v-if="filter.OrderBy === 'publishedDate'"
              :icon="['fas', 'arrow-up-wide-short']"
              style="color: #29be00"
              rotation="180"
              size="xl"
              class="me-2"
            />

            <font-awesome-icon
              v-else-if="filter.OrderBy === 'publishedDate_desc'"
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
            Data publicării
          </th>

          <th
            scope="25"
            width="15%"
            @click="OrderBy('createdDate')"
            class="cursor-pointer"
          >
            <font-awesome-icon
              v-if="filter.OrderBy === 'createdDate'"
              :icon="['fas', 'arrow-up-wide-short']"
              style="color: #29be00"
              rotation="180"
              size="xl"
              class="me-2"
            />

            <font-awesome-icon
              v-else-if="filter.OrderBy === 'createdDate_desc'"
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
            Data adăugării
          </th>

          <th
            scope="25"
            width="20%"
            @click="OrderBy('addedBy')"
            class="cursor-pointer"
          >
            <font-awesome-icon
              v-if="filter.OrderBy === 'addedBy'"
              :icon="['fas', 'arrow-up-wide-short']"
              style="color: #29be00"
              rotation="180"
              size="xl"
              class="me-2"
            />

            <font-awesome-icon
              v-else-if="filter.OrderBy === 'addedBy_desc'"
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
            Adăugat de :
          </th>
          <th width="10%"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(news, index) in NewsList.Items" :key="index">
          <td>
            <div class="d-flex align-items-center">
              <div class="img-container-avatar me-2">
                <img
                  :src="ShowDynamicImage(news.ImageBase64)"
                  class="me-2 icon-avatar"
                />
              </div>
              <div class="d-flex flex-column">
                <span>{{ news.Name }}</span>
                <span v-if="news.IsPublished===false" class="draft">Draft</span>
                <span v-if="news.IsPublished===true" class="published">Published</span>
              </div>
              
            </div>
          </td>
          <td>{{ news.Description }}</td>
          <td>{{ news.CategoryName }}</td>

          <td>{{ news.PublishedDate }}</td>
          <td>{{ news.CreatedDate }}</td>
          <td>
            <div class="d-flex align-items-center">
              <div class="img-container-avatar me-2">
                <img
                  :src="ShowDynamicImage(news.CreateByImageBase64)"
                  class="me-2 icon-avatar"
                />
              </div>

              <span>{{ news.CreateByName }}</span>
            </div>
          </td>

          <td>
            <div class="editButtons">
              <router-link
                :to="{ name: 'edit-news', params: { id: news.Id } }"
                class="button-edit"
              >
                <font-awesome-icon :icon="['far', 'pen-to-square']" />
              </router-link>

              <button
                type="button"
                class="button-delete"
                @click="DeleteNews(news.Id)"
              >
                <font-awesome-icon :icon="['fas', 'trash']" />
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>

    <Pagination
      :totalPages="NewsList.NumberOfPages"
      :currentPage="filter.PageNumber"
      @pagechanged="GetAllNews"
    />
  </div>
</template>

<script>
import Pagination from "../../components/Pagination.vue";
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import { date } from "yup";
import moment from 'moment';

export default {
  name: "NewsView",
  components: {
    VueDatePicker,
    Pagination,
  },
  data() {
    return {
      publishDate: "",
      addDate: "",
      filter: {
        SearchText: "",
        PageNumber: 1,
        OrderBy: "name",
        NewsType: "",
        PublishedDate: "",
        CreatedDate: "",
        UserId: "",
        isPublished:"",
      },
      NewsList: {
        Items: [],
        NumberOfPages: 0,
      },

      selectedNewsCategory: { Id: 0, Name: "" },
      selectedUserAuthor: { UserId: "", Name: "" },
      stateNews:null,
      newsTypesList: [],
      usersAuthorList: [],
      functions: [
        { name: "Director executiv" },
        { name: "Manager General" },
        { name: "Manager Departament tehnic" },
        { name: "Administrator" },
        { name: "Contabil" },
      ],
    };
  },
  methods: {
    GetNewsTypes() {
      this.$axios
        .get(`/api/NewsCategories/getNewsCategoryDropDown`)
        .then((response) => {
          this.newsTypesList = response.data;
          console.log(response.data);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    GetUsersAuthor() {
      this.$axios
        .get(`/api/UserExtend/getUsersExtendDropDown`)
        .then((response) => {
          this.usersAuthorList = response.data;
          console.log(response.data);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    ShowDynamicImage(imagePath) {
      if (!imagePath) {
        return `src/images/user.png`;
      }
      return imagePath;
    },

    GetAllNews(page) {
      this.filter.PageNumber = 1;
      if (page) {
        this.filter.PageNumber = page;
      }
      console.log(this.selectedNewsCategory);
      const searchParams = {
        OrderBy: this.filter.OrderBy,
        PageNumber: this.filter.PageNumber,
        CategoryId: this.selectedNewsCategory.Id || "",
        ...(this.filter.PublishedDate ? {  PublishedDate: moment(this.filter.PublishedDate).format('DD/MM/YYYY') } : ''),
        ...(this.filter.CreatedDate ? {  CreatedDate: moment(this.filter.CreatedDate).format('DD/MM/YYYY') } : ''),
        // PulishedDate: moment(this.filter.PublishedDate).format('MM/DD/YYYY'),
        UserId: this.selectedUserAuthor.UserId || "",
        PageSize: 4,
        NameSearch: this.filter.SearchText,
        isPublished: this.filter.isPublished,
      };
      this.$axios
        .get(`/api/News/getNewses?${new URLSearchParams(searchParams)}`)
        .then((response) => {
          console.log(searchParams);
          this.NewsList = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    DeletePlayer(id) {
      this.$axios
        .delete(`/api/Player/deletePlayer/${id}`)
        .then((response) => {
          this.GetAllNews();
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

      this.GetAllNews();
    },

    OpenDropdown() {
      $("#dropdownNewsTypesAddNews").dropdown("toggle");
    },

    OpenDropdownAuthor() {
      $("#dropdownAuthor").dropdown("toggle");
    },
    
    OpenDropdownState() {
      $("#dropdownStateNews").dropdown("toggle");
    },
    SelectCategory(newsType) {
      this.selectedNewsCategory = newsType;
      $("#dropdownNewsTypesAddNews").dropdown("hide");
      this.GetAllNews();
    },

    SelectUser(user) {
      this.selectedUserAuthor = user;
      console.log("aici e userulll: " + this.selectedUserAuthor.UserId);
      $("#dropdownAuthor").dropdown("hide");
      this.GetAllNews();
    },
    SelectStateNews(state) {
      this.stateNews=state;
      if(this.stateNews==="Publicate"){
        this.filter.isPublished=true;
      }else{
        this.filter.isPublished=false;
      }
      
      console.log("aici e userulll: " + this.selectedUserAuthor.UserId);
      $("#dropdownStateNews").dropdown("hide");
      this.GetAllNews();

    },

    CloseAndResetDropdown() {
      (this.selectedNewsCategory = { Id: "", Name: "" }),
        $("#dropdownNewsTypesAddNews").dropdown("toggle");
      this.GetAllNews();
    },

    CloseAndResetDropdownAuthor() {
      (this.selectedUserAuthor = { UserId: "", Name: "" }),
        $("#dropdownAuthor").dropdown("toggle");
      this.GetAllNews();
    },

    CloseAndResetDropdownState() {
      this.stateNews=null,
      this.filter.isPublished="",
      $("#dropdownStateNews").dropdown("toggle");
      this.GetAllNews();
    },

    DeleteNews(id) {
      this.$axios
        .delete(`/api/News/deleteNews/${id}`)
        .then((response) => {
          this.GetAllNews();
          console.log(`Deleted news with ID ${id}`);
        })
        .catch((error) => {
          console.error(error);
        });
    },
  },

  created() {
    this.GetAllNews();
    this.GetUsersAuthor();
    this.GetNewsTypes();
  },
};
</script>

<style>
.table-custom .draft{
  color: #3F53EE;
  font-size: 10px;
  font-family: 'Raleway';
}

.table-custom .published{
  color: #2d9b17;
  font-size: 10px;
  font-family: 'Raleway';
}


</style>
