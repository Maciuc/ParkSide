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

    <div class="row mt-3 new-form">
      <div class="col-xxl col-xl-4 col-md-3 col-sm-4 col-xs-6 mb-2">
        <div class="input-group-custom">
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
              placeholder="Caută știre"
              aria-label="Username"
              aria-describedby="basic-addon1"
              v-model="filter.SearchText"
              v-on:keyup.enter="GetAllNews()"
            />
          </div>
        </div>
      </div>

      <div class="col-xxl col-xl-4 col-md-3 col-sm-4 mb-2 custom-date-picker">
        <VueDatePicker
          v-model="filter.PublishedDate"
          format="dd/MM/yyyy"
          auto-apply
          utc
          :enable-time-picker="false"
          @update:model-value="GetAllNews()"
          placeholder="Dată publicare"
        ></VueDatePicker>
      </div>

      <div class="col-xxl col-xl-4 col-md-3 col-sm-4 mb-2">
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
              <span v-if="filter.IsPublished === true"> Publicate </span>
              <span v-else-if="filter.IsPublished === false"> Schițe </span>
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

    <div style="overflow-x: auto">
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
              <span v-if="filter.OrderBy === 'name' || filter.OrderBy === 'name_desc'">Titlu știre</span>
              <span v-else class="span-inactive">Titlu știre</span>
            </th>
            <th scope="25" width="15%">Descriere</th>
            <th
              scope="25"
              width="15%"
              @click="OrderBy('PublishedDate')"
              class="cursor-pointer"
            >
              <font-awesome-icon
                v-if="filter.OrderBy === 'PublishedDate'"
                :icon="['fas', 'arrow-up-wide-short']"
                style="color: #29be00"
                rotation="180"
                size="xl"
                class="me-2"
              />

              <font-awesome-icon
                v-else-if="filter.OrderBy === 'PublishedDate_desc'"
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
              <span v-if="filter.OrderBy === 'PublishedDate' || filter.OrderBy === 'PublishedDate_desc'">Data publicării</span>
              <span v-else class="span-inactive">Data publicării</span>
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
                  <span>{{
                    news.Name.length > 30
                      ? news.Name.substring(0, 20) + "..."
                      : news.Name
                  }}</span>
                  <span v-if="news.IsPublished === false" class="draft"
                    >Draft</span
                  >
                  <span v-if="news.IsPublished === true" class="published"
                    >Published</span
                  >
                </div>
              </div>
            </td>
            <td>
              {{
                news.Description.length > 30
                  ? news.Description.substring(0, 30) + "..."
                  : news.Description
              }}
            </td>

            <td>{{ news.PublishedDate }}</td>

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
    </div>
    </div>

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
import moment from "moment";

export default {
  name: "News",
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
        OrderBy: "PublishedDate_desc",
        PublishedDate: "",
        IsPublished: "",
      },
      NewsList: {
        Items: [],
        NumberOfPages: 0,
      },
      stateNews: null,
    };
  },
  methods: {
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
      const searchParams = {
        OrderBy: this.filter.OrderBy,
        PageNumber: this.filter.PageNumber,
        ...(this.filter.PublishedDate
          ? {
              PublishedDate: moment(this.filter.PublishedDate).format(
                "DD/MM/YYYY"
              ),
            }
          : ""),
        PageSize: 4,
        NameSearch: this.filter.SearchText,
        IsPublished: this.filter.IsPublished,
      };
      this.$axios
        .get(`/api/News/getNewses?${new URLSearchParams(searchParams)}`)
        .then((response) => {
          console.log(searchParams);
          console.log(this.filter.PublishedDate)
          this.NewsList = response.data;
        })
        .catch((error) => {
          console.log(error);
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

    OpenDropdownState() {
      $("#dropdownStateNews").dropdown("toggle");
    },

    SelectStateNews(state) {
      this.stateNews = state;
      if (this.stateNews === "Publicate") {
        this.filter.IsPublished = true;
      } else {
        this.filter.IsPublished = false;
      }
      $("#dropdownStateNews").dropdown("hide");
      this.GetAllNews();
    },

    CloseAndResetDropdownState() {
      (this.stateNews = null),
        (this.filter.IsPublished = ""),
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
  },
};
</script>

<style>




</style>
