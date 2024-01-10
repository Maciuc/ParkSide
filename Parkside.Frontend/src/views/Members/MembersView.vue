<template>
  <div>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Membri</div>
      </div>

      <div class="col-auto">
        <router-link :to="{ name: 'add-member' }" class="button green">
          Adaugă
          <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
        </router-link>
      </div>
    </div>

    <div class="row d-flex flex-row mt-3 new-form">
      <div class="col-xxl-3 col-xl-4 col-lg-5 col-md-6 col-sm-8">
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
            placeholder="Caută nume"
            aria-label="Username"
            aria-describedby="basic-addon1"
            v-model="filter.SearchText"
            v-on:keyup.enter="GetAllMembers()"
          />
        </div>
      </div>
      <div class="col-xxl-3 col-xl-4 col-lg-5 col-md-6 col-sm-8 custom-control">
        <select
          class="form-select form-control"
          aria-label="Default select example"
          v-model="filter.SearchFunction"
          @change="GetAllMembers()"
        >
          <option value="" selected>Toate funcțiile</option>
          <option v-for="(fnct, index) in functions" :key="index">
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

          <th scope="25" width="20%">Funcția în organizație</th>
          <th scope="25" width="30%">Discurs de bun venit</th>
          <th
            scope="25"
            width="20%"
            @click="OrderBy('orderNumber')"
            class="cursor-pointer"
          >
            <font-awesome-icon
              v-if="filter.OrderBy === 'orderNumber'"
              :icon="['fas', 'arrow-up-wide-short']"
              style="color: #29be00"
              rotation="180"
              size="xl"
              class="me-2"
            />

            <font-awesome-icon
              v-else-if="filter.OrderBy === 'orderNumber_desc'"
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
            Număr ordine afișare
          </th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(member, index) in MembersList.Items" :key="index">
          <td>
            <div class="d-flex align-items-center">
              <div class="img-container-avatar me-2">
                <img
                  :src="ShowDynamicImage(member.ImageBase64)"
                  class="me-2 icon-avatar"
                />
              </div>

              <span>{{ member.Name }}</span>
            </div>
          </td>
          <td>{{ member.OrganizationFunction }}</td>
          <td v-if="member.VizibilitySpeech">{{ member.Speech }}</td>
          <td v-else></td>
          <td class="new-form">
            {{ member.OrderNumber }}
          </td>

          <td>
            <div class="editButtons">
              <router-link
                :to="{ name: 'edit-member', params: { id: member.Id } }"
                class="button-edit"
              >
                <font-awesome-icon :icon="['far', 'pen-to-square']" />
              </router-link>

              <button
                type="button"
                class="button-delete"
                @click="DeleteMember(member.Id)"
              >
                <font-awesome-icon :icon="['fas', 'trash']" />
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>

    <Pagination
      :totalPages="MembersList.NumberOfPages"
      :currentPage="filter.PageNumber"
      @pagechanged="GetAllMembers"
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
        SearchFunction: "",
      },
      MembersList: {
        Items: [],
        NumberOfPages: 0,
      },
      validOrderNumber: null,
      DuplicatedId: "",

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
    ShowDynamicImage(imagePath) {
      if (!imagePath) {
        return `src/images/user.png`;
      }
      return imagePath;
    },

    GetAllMembers(page) {
      this.filter.PageNumber = 1;
      if (page) {
        this.filter.PageNumber = page;
      }
      const searchParams = {
        OrderBy: this.filter.OrderBy,
        PageNumber: this.filter.PageNumber,
        PageSize: 4,
        NameSearch: this.filter.SearchText,
        OrganizationFunction: this.filter.SearchFunction,
      };
      this.$axios
        .get(`/api/Member/getMembers?${new URLSearchParams(searchParams)}`)
        .then((response) => {
          console.log(searchParams);
          this.MembersList = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    DeleteMember(id) {
      this.$axios
        .delete(`/api/Member/deleteMember/${id}`)
        .then((response) => {
          this.GetAllMembers();
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

      this.GetAllMembers();
    },
  },

  created() {
    this.GetAllMembers();
  },
};
</script>
