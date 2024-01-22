<template>
  <!-- Modal -->
  <div class="modal fade custom-modal" id="match-edit-modal" tabindex="-1" aria-labelledby="exampleModalLabel"
    :aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title mt-2">
            Editeaza meci
          </h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>

        <Form @submit="SaveMatch" :validation-schema="schema" v-slot="{ errors }" ref="editMatchFormRef">
          <div class="modal-body new-form">

            <div :class="{ 'invalid-input': errors.date }" class="col custom-date-picker mb-2">
              <label class="form-label">Dată meci</label>
              <Field v-slot="{ field }" name="date" id="date">
                <VueDatePicker v-bind="field" v-model="editedMatch.MatchDate" auto-apply utc :enable-time-picker="false"
                  placeholder="Dată meci"></VueDatePicker>
              </Field>
              <ErrorMessage name="date" class="text-danger error-message" />
            </div>

            <!-- <div class="mb-2 position-relative">
              <label for="championship" class="form-label">Alege campionat</label>
              <Field v-model="selectedChampionship.Name" name="championship" as="select"
                @change="updateSelectedChampionshipId" :class="{ 'border-danger': errors.championship }"
                class="form-select form-control">
                <option value="" disabled>Campionate</option>
                <option v-for="(champ, index) in championshipsList" :key="index" :value="champ.Id">
                  {{ champ.Name }}
                </option>
              </Field>
              <ErrorMessage name="championship" class="text-danger error-message" />
            </div> -->

            <div class="mb-2 position-relative">
              <label for="championship" class="form-label">Alege campionat</label>
              <Field
                v-slot="{ field }"
                name="championship"
                class="form-control"
                v-model="editedMatch.Championship"
              >
                <div class="custom dropdown custom-dropdown" v-bind="field">
                  <button
                    class="btn btn-secondary dropdown-toggle"
                    :class="{ 'border-danger': errors.championship }"
                    type="button"
                    id="dropdownChampionships"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                  >
                    <span>
                      {{ editedMatch.Championship?.Name }}
                    </span>
                  </button>
                  <ul
                    class="dropdown-menu"
                    aria-labelledby="dropdownChampionships"
                  >
                    <li v-for="(champ, index) in championshipsList" :key="index">
                      <a
                        class="dropdown-item"
                        @click="editedMatch.Championship = champ"
                        >{{ champ.Name }}</a
                      >
                    </li>
                  </ul>
                </div>
              </Field>

              <ErrorMessage name="championship" class="text-danger error-message" />
            </div>


            <!-- <div class="mb-2 position-relative">
              <label for="team" class="form-label">Alege echipa adversa</label>
              <Field v-model="selectedTeam.Name" name="team" as="select" @change="updateSelectedTeamId"
                :class="{ 'border-danger': errors.team }" class="form-select form-control">
                <option value="" disabled>Echipe</option>
                <option v-for="(team, index) in teamsList" :key="index" :value="team.Id">
                  {{ team.Name }}
                </option>
              </Field>
              <ErrorMessage name="team" class="text-danger error-message" />
            </div> -->

            <div class="mb-2 position-relative">
                <label for="team" class="form-label">Alege echipa adversa</label>
                <Field
                  v-slot="{ field }"
                  name="team"
                  class="form-control"
                  v-model="editedMatch.EnemyTeam"
                >
                  <div class="custom dropdown custom-dropdown" v-bind="field">
                    <button
                      class="btn btn-secondary dropdown-toggle"
                      :class="{ 'border-danger': errors.team }"
                      type="button"
                      id="dropdownTeams"
                      data-bs-toggle="dropdown"
                      aria-expanded="false"
                    >
                      <span>
                        {{ editedMatch.EnemyTeam?.Name }}
                      </span>
                    </button>
                    <ul
                      class="dropdown-menu"
                      aria-labelledby="dropdownTeams"
                    >
                      <li v-for="(team, index) in teamsList" :key="index">
                        <a
                          class="dropdown-item"
                          @click="editedMatch.EnemyTeam = team"
                          >{{ team.Name }}</a
                        >
                      </li>
                    </ul>
                  </div>
                </Field>

                <ErrorMessage name="team" class="text-danger error-message" />
              </div>



            <div class="mb-3">
              <label for="input-location" class="form-label">Locatie</label>
              <Field type="text" class="form-control" id="input-location" name="location" placeholder="Locatie"
                v-model="editedMatch.Location" />
            </div>

            <div class="form-check form-switch">
              <input class="form-check-input" type="checkbox" id="flexSwitchCheckDefault" :checked="editedMatch.PlayingHome"
                @click="ToggleHomePlay" />
              <label class="form-label" for="flexSwitchCheckDefault">Joaca acasa</label>
            </div>
            <div class="col">
              <label for="input-hour" class="form-label">Ora</label>
              <Field class="form-control" id="input-hour" type="time" placeholder="Ora"
                :class="{ 'border-danger': errors.hour }" name="hour" v-model="editedMatch.MatchHour">
              </Field>
              <ErrorMessage name="hour" class="text-danger error-message" />
            </div>

            <div class="mb-3">
                <label for="input-mainpoints" class="form-label">Puncte CSU</label>
                <Field type="text" class="form-control" id="input-mainpoints" name="mainpoints" placeholder="Puncte CSU"
                  v-model="editedMatch.MainTeamPoints" />
              </div>

               <div class="mb-3">
                  <label for="input-enemypoints" class="form-label">Puncte adversari</label>
                  <Field type="text" class="form-control" id="input-enemypoints" name="enemypoints" placeholder="Puncte adversari"
                    v-model="editedMatch.EnemyTeamPoints" />
                </div>
          </div>




          <div class="modal-footer justify-content-between">
            <button type="button" class="button grey" data-bs-dismiss="modal">
              Anulare
            </button>
            <button type="submit" class="button green">Salvare</button>
          </div>
        </Form>
      </div>
    </div>
  </div>
</template>
  
<script>
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";

export default {
  name: "AddMatchComponent",
  components: {
    Form,
    Field,
    ErrorMessage,
    VueDatePicker,
  },
  emits: ["get-list"],
  data() {
    return {
      championshipsList: [],
      teamsList: [],
      selectedChampionship: {},
      selectedTeam: {},
    };
  },

  props:{
    editedMatch: {
      type: Object,
      default() {
        return { 
          Id: "",
          Location: "",
          Date: "",
          PlayingHome: true,
          EnemyTeamPoints: "",
          MainTeamPoints: "",
          MatchHour: "", };
      },
    },
  },

  computed: {
    schema() {
      return yup.object({
        championship: yup.object().required("Acest câmp este obligatoriu"),
        team: yup.object().required("Acest câmp este obligatoriu"),
      });
    },
  },

  methods: {
    SaveMatch() {
      this.$axios
        .put(`/api/Match/updateMatch/${this.editedMatch.Id}/${this.editedMatch.EnemyTeam.Id}/${this.editedMatch.Championship.Id}`, this.editedMatch)
        .then((response) => {
          console.log(response);
          console.log(this.editedMatch);
          this.$emit("get-list");
          $("#match-edit-modal").modal("hide");
          this.$swal.fire({
            title: "Succes",
            text: "Meciul a fost editat",
            icon: "success",
            showConfirmButton: false,
            timer: 1500,
          });
        })
        .catch((error) => {
          console.error(error);
        });
    },

    ToggleHomePlay() {
      this.editedMatch.PlayingHome =
        !this.editedMatch.PlayingHome;
      console.log(this.editedMatch);
    },
    ClearModal() {
      this.$refs.editMatchFormRef.resetForm();
    },
    GetChampionshipsList() {
      this.$axios
        .get("/api/Championship/getChampionshipsDropDown")
        .then((response) => {
          this.championshipsList = response.data;
          console.log(response.data);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    GetTeamsList() {
      this.$axios
        .get("/api/Team/getTeamsDropDown")
        .then((response) => {
          this.teamsList = response.data;
          console.log(response.data);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    OpenDropdownChampionships() {
      $("#dropdownChampionships").dropdown("toggle");
    },
    OpenDropdownTeams() {
      $("#dropdownTeams").dropdown("toggle");
    },

    SelectChampionship(championship) {
      this.selectedChampionship = championship;
      $("#dropdownChampionships").dropdown("hide");
    },
    SelectCategory(team) {
      this.selectedTeam = team;
      $("#dropdownTeams").dropdown("hide");
    },
    updateSelectedChampionshipId() {
      const selectedChamp = this.championshipsList.find(
        (champ) => champ.Id === this.selectedChampionship.Name
      );
      this.selectedChampionship.Id = selectedChamp ? selectedChamp.Id : null;
    },
    updateSelectedTeamId() {
      const selectedChamp = this.teamsList.find(
        (champ) => champ.Id === this.selectedTeam.Name
      );
      this.selectedTeam.Id = selectedChamp ? selectedChamp.Id : null;
    },
  },
  created() {
    this.GetChampionshipsList();
    this.GetTeamsList();
  },
};
</script>

  