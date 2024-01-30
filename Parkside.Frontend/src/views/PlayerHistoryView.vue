<template>
  <section>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Participari jucatori</div>
      </div>

      <div class="col-auto">
        <button class="button green" @click="OpenModalAddPlayerHistory">
          <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
          Adaugă
        </button>
        <AddPlayerHistoryComponent
          @get-list="GetAllPlayerHistories()"
          ref="addPlayerHistoryModal"
        ></AddPlayerHistoryComponent>
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
            placeholder="Caută"
            aria-label="Username"
            aria-describedby="basic-addon1"
            v-model="filter.SearchText"
            v-on:keyup.enter="GetAllPlayerHistories()"
          />
        </div>
      </div>

      <div class="col-xl-4 col-md-3 col-sm-4 mb-2 custom-date-picker">
        <select
          class="form-select form-control"
          aria-label="Default select example"
          placeholder="An"
          v-model="filter.Year"
          @change="GetAllPlayerHistories()"
        >
          <option selected value="">An</option>
          <option v-for="(year, index) in Years" :key="index">
            {{ year.year }}
          </option>
        </select>
      </div>
    </div>

    <div style="overflow-x: auto">
      <table class="table table-custom">
        <thead>
          <tr>
            <th
              width="22%"
              @click="OrderBy('playerName')"
              class="cursor-pointer"
            >
              <font-awesome-icon
                v-if="filter.OrderBy === 'playerName'"
                :icon="['fas', 'arrow-up-wide-short']"
                style="color: #29be00"
                rotation="180"
                size="xl"
                class="me-2"
              />

              <font-awesome-icon
                v-else-if="filter.OrderBy === 'playerName_desc'"
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
              <span>Nume jucator</span>
            </th>

            <th scope="20" width="22%">Campionat</th>

            <th width="15%" @click="OrderBy('year')" class="cursor-pointer">
              <font-awesome-icon
                v-if="filter.OrderBy === 'year'"
                :icon="['fas', 'arrow-up-wide-short']"
                style="color: #29be00"
                rotation="180"
                size="xl"
                class="me-2"
              />

              <font-awesome-icon
                v-else-if="filter.OrderBy === 'year_desc'"
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
              <span>An</span>
            </th>
            <th scope="20" width="20%">Echipa</th>
            <th scope="20" width="20%">Rol</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(playerHistory, index) in playerHistories.Items"
            :key="index"
          >
            <td>
              <div class="d-flex align-items-center">
                <div class="img-container-avatar me-3">
                  <img
                    :src="ShowDynamicImage(playerHistory.PlayerImageBase64)"
                    class="me-2 icon-avatar"
                  />
                </div>
                <span>{{
                  playerHistory.PlayerLastName +
                  " " +
                  playerHistory.PlayerFirstName
                }}</span>
              </div>
            </td>
            <td>
              <div class="d-flex align-items-center">
                <div class="img-container-avatar me-3">
                  <img
                    :src="
                      ShowDynamicImage(playerHistory.ChampionshipImageBase64)
                    "
                    class="me-2 icon-avatar"
                  />
                </div>
                <span>{{ playerHistory.ChampionshipName }}</span>
              </div>
            </td>
            <td>{{ playerHistory.Year }}</td>
            <td>{{ playerHistory.TeamName }}</td>
            <td>{{ playerHistory.PlayerRole }}</td>
            <td>
              <div class="editButtons">
                <button
                  class="button-edit"
                  data-bs-toggle="modal"
                  type="button"
                  data-bs-target="#playerHistory-edit-modal"
                  @click="GetPlayerHistoryForEdit(playerHistory.Id)"
                >
                  <font-awesome-icon :icon="['far', 'pen-to-square']" />
                </button>

                <button
                  type="button"
                  class="button-delete"
                  @click="DeletePlayerHistory(playerHistory.Id)"
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
      :totalPages="playerHistories.NumberOfPages"
      :currentPage="filter.PageNumber"
      @pagechanged="GetAllPlayerHistories"
    />

    <EditPlayerHistoryComponent
      :editedPlayerHistory="selectedPlayerHistoryForEdit"
      @get-list="GetAllPlayerHistories"
    />
  </section>
</template>

<script>
import AddPlayerHistoryComponent from "../components/Modals/PlayersHistory/AddPlayerHistoryComponent.vue";
import EditPlayerHistoryComponent from "../components/Modals/PlayersHistory/EditPlayerHistoryComponent.vue";
import Pagination from "../components/Pagination.vue";

export default {
  name: "PlayerHistories",
  components: {
    AddPlayerHistoryComponent,
    EditPlayerHistoryComponent,
    Pagination,
  },
  data() {
    return {
      selectedPlayerHistoryForEdit: {},

      filter: {
        SearchText: "",
        PageNumber: 1,
        OrderBy: "year_desc",
        Year: "",
        Role: "",
        TeamName: "",
      },
      playerHistories: {
        Items: [],
        NumberOfPages: 1,
      },

      Years: [
        { year: "2024" },
        { year: "2023" },
        { year: "2022" },
        { year: "2021" },
        { year: "2020" },
        { year: "2019" },
        { year: "2018" },
        { year: "2017" },
        { year: "2016" },
      ],
    };
  },
  methods: {
    ShowDynamicImage(imagePath) {
      if (!imagePath) {
        return `src/images/NoImageSelected.png`;
      }
      return imagePath;
    },
    OpenModalAddPlayerHistory() {
      $("#playerHistory-add-modal").modal("show");
      this.$refs.addPlayerHistoryModal.ClearModal();
    },

    GetPlayerHistoryForEdit(id) {
      this.$axios
        .get(`/api/PlayerHistory/getPlayerHistory/${id}`)
        .then((response) => {
          this.selectedPlayerHistoryForEdit = response.data;
          console.log(this.selectedPlayerHistoryForEdit);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    GetAllPlayerHistories(page) {
      this.filter.PageNumber = 1;
      if (page) {
        this.filter.PageNumber = page;
      }
      const searchParams = {
        OrderBy: this.filter.OrderBy,
        PageNumber: this.filter.PageNumber,
        PageSize: 9,
        NameSearch: this.filter.SearchText,
        Year: this.filter.Year,
        PlayerRole: this.filter.Role,
        TeamName: this.filter.TeamName,
      };
      this.$axios
        .get(
          `api/PlayerHistory/getPlayerHistories?${new URLSearchParams(
            searchParams
          )}`
        )
        .then((response) => {
          console.log(searchParams);
          this.playerHistories = response.data;
        })
        .catch((error) => {
          console.log(error);
        });
    },

    DeletePlayerHistory(id) {
      this.$axios
        .delete(`/api/PlayerHistory/deletePlayerHistory/${id}`)
        .then((response) => {
          this.GetAllPlayerHistories();
          console.log(`Deleted playerHistory with ID ${id}`);
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

      this.GetAllPlayerHistories();
    },
  },

  created() {
    this.GetAllPlayerHistories();
  },
};
</script>
