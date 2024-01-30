<template>
  <section>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Meciuri</div>
      </div>

      <div class="col-auto">
        <button class="button green" @click="OpenModalAddMatch">
          <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
          Adaugă
        </button>
        <AddMatchComponent
          @get-list="GetAllMatches()"
          ref="addMatchModal"
        ></AddMatchComponent>
      </div>
    </div>

    <div class="row mt-3 new-form">
      <div class="col-xxl-4 col-xl-4 col-lg-3 col-md-4 col-sm-6">
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
            placeholder="Caută echipa adversa"
            aria-label="Username"
            aria-describedby="basic-addon1"
            v-model="filter.SearchText"
            v-on:keyup.enter="GetAllMatches()"
          />
        </div>
      </div>

      <div class="col-xl-4 col-md-3 col-sm-4 mb-2 custom-date-picker">
        <VueDatePicker
          v-model="filter.MatchDate"
          format="dd/MM/yyyy"
          auto-apply
          utc
          :enable-time-picker="false"
          @update:model-value="GetAllMatches()"
          placeholder="Dată meci"
        ></VueDatePicker>
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
              <span>Nume echipa adversa</span>
            </th>

            <th scope="20" width="20%">Campionat</th>

            <th
              width="20%"
              @click="OrderBy('matchdate')"
              class="cursor-pointer"
            >
              <font-awesome-icon
                v-if="filter.OrderBy === 'matchdate'"
                :icon="['fas', 'arrow-up-wide-short']"
                style="color: #29be00"
                rotation="180"
                size="xl"
                class="me-2"
              />

              <font-awesome-icon
                v-else-if="filter.OrderBy === 'matchdate_desc'"
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
              <span>Data</span>
            </th>
            <th scope="20" width="20%">Ora</th>
            <th scope="20" width="20%">Locatie</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(match, index) in matches.Items" :key="index">
            <td>
              <div class="d-flex align-items-center">
                <div class="img-container-avatar me-3">
                  <img
                    :src="ShowDynamicImage(match.EnemyTeamImageBase64)"
                    class="me-2 icon-avatar"
                  />
                </div>
                <span>{{ match.EnemyTeamName }}</span>
              </div>
            </td>
            <td>
              <div class="d-flex align-items-center">
                <div class="img-container-avatar me-3">
                  <img
                    :src="ShowDynamicImage(match.ChampionshipImageBase64)"
                    class="me-2 icon-avatar"
                  />
                </div>
                <span>{{ match.ChampionshipName }}</span>
              </div>
            </td>
            <td>{{ match.MatchDate }}</td>
            <td>{{ match.MatchHour }}</td>
            <td>{{ match.Location }}</td>
            <td>
              <div class="editButtons">
                <button
                  class="button-edit"
                  data-bs-toggle="modal"
                  type="button"
                  data-bs-target="#match-edit-modal"
                  @click="GetMatchForEdit(match.Id)"
                >
                  <font-awesome-icon :icon="['far', 'pen-to-square']" />
                </button>

                <button
                  type="button"
                  class="button-delete"
                  @click="DeleteMatch(match.Id)"
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
      :totalPages="matches.NumberOfPages"
      :currentPage="filter.PageNumber"
      @pagechanged="GetAllMatches"
    />

    <EditMatchComponent
      :editedMatch="selectedMatchForEdit"
      @get-list="GetAllMatches"
    />
  </section>
</template>

<script>
import AddMatchComponent from "../components/Modals/Matches/AddMatchComponent.vue";
import EditMatchComponent from "../components/Modals/Matches/EditMatchComponent.vue";
import Pagination from "../components/Pagination.vue";

export default {
  name: "Matches",
  components: {
    AddMatchComponent,
    EditMatchComponent,
    Pagination,
  },
  data() {
    return {
      selectedMatchForEdit: {},

      filter: {
        SearchText: "",
        PageNumber: 1,
        OrderBy: "matchdate_desc",
        MatchDate: "",
      },
      matches: {
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
    OpenModalAddMatch() {
      $("#match-add-modal").modal("show");
      this.$refs.addMatchModal.ClearModal();
    },

    GetMatchForEdit(id) {
      this.$axios
        .get(`/api/Match/getMatch/${id}`)
        .then((response) => {
          this.selectedMatchForEdit = response.data;
          console.log(this.selectedMatchForEdit);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    GetAllMatches(page) {
      this.filter.PageNumber = 1;
      if (page) {
        this.filter.PageNumber = page;
      }
      const searchParams = {
        OrderBy: this.filter.OrderBy,
        PageNumber: this.filter.PageNumber,
        MatchDate: this.filter.MatchDate,
        PageSize: 6,
        NameSearch: this.filter.SearchText,
      };
      this.$axios
        .get(`api/Match/getMatches?${new URLSearchParams(searchParams)}`)
        .then((response) => {
          console.log(searchParams);
          this.matches = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    DeleteMatch(id) {
      this.$axios
        .delete(`/api/Match/deleteMatch/${id}`)
        .then((response) => {
          this.GetAllMatches();
          console.log(`Deleted match with ID ${id}`);
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

      this.GetAllMatches();
    },
  },

  created() {
    this.GetAllMatches();
  },
};
</script>
