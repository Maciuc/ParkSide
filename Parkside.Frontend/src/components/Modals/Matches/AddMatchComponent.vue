<template>
    <!-- Modal -->
    <div
      class="modal fade custom-modal"
      id="match-add-modal"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      :aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title mt-2" >
              Adaugă meci
            </h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
  
          <Form
            @submit="AddMatch"
            :validation-schema="schema"
            v-slot="{ errors }"
            ref="addMatchFormRef"
          >
            <div class="modal-body new-form">

              <div
              :class="{ 'invalid-input': errors.date }"
              class="col custom-date-picker mb-2"
            >
              <label class="form-label">Dată meci</label>
              <Field
                v-slot="{ field }"
                name="date"
                id="date"
              >
                <VueDatePicker
                  v-bind="field"
                  v-model="newMatch.Date"
                  auto-apply
                  utc
                  :enable-time-picker="false"
                  placeholder="Dată meci"
                ></VueDatePicker>
              </Field>
              <ErrorMessage name="date" class="text-danger error-message" />
            </div>

              <div class="mb-2 position-relative">
                <label for="championship" class="form-label"
                  >Alege campionat</label
                >
                <Field
                  v-model="selectedChampionship.Name"
                  name="championship"
                  as="select"
                  @change="updateSelectedChampionshipId"
                  :class="{ 'border-danger': errors.championship }"
                  class="form-select form-control"
                >
                  <option value="" disabled>Campionate</option>
                  <option
                    v-for="(champ, index) in championshipsList"
                    :key="index"
                    :value="champ.Id"
                  >
                    {{ champ.Name }}
                  </option>
                </Field>
                <ErrorMessage name="championship" class="text-danger error-message" />
              </div>


              <div class="mb-2 position-relative">
                <label for="team" class="form-label"
                  >Alege echipa adversa</label
                >
                <Field
                  v-model="selectedTeam.Name"
                  name="team"
                  as="select"
                  @change="updateSelectedTeamId"
                  :class="{ 'border-danger': errors.team }"
                  class="form-select form-control"
                >
                  <option value="" disabled>Echipe</option>
                  <option
                    v-for="(team, index) in teamsList"
                    :key="index"
                    :value="team.Id"
                  >
                    {{ team.Name }}
                  </option>
                </Field>
                <ErrorMessage name="team" class="text-danger error-message" />
              </div>

              

              <div class="mb-3">
                <label for="input-location" class="form-label"
                  >Locatie</label
                >
                <Field
                  type="text"
                  class="form-control"
                  id="input-location"
                  name="location"
                  placeholder="Locatie"
                  v-model="newMatch.Location"
                />
              </div>

              <div class="form-check form-switch">
              <input
                class="form-check-input"
                type="checkbox"
                id="flexSwitchCheckDefault"
                :checked="newMatch.PlayingHome"
                @click="ToggleHomePlay"
              />
              <label class="form-label" for="flexSwitchCheckDefault"
                >Joaca acasa</label
              >
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
      championshipsList:[],
      teamsList:[],
      selectedChampionship:{},
      selectedTeam:{},
      newMatch: {
        Location: "",
        Date: "",
        PlayingHome: true,
        EnemyTeamPoints: "",
        MainTeamPoints: "",
      },
    };
  },

  computed: {
    schema() {
      return yup.object({
        championship: yup.string().required("Acest câmp este obligatoriu"),
        team: yup.string().required("Acest câmp este obligatoriu"),
      });
    },
  },
  
  methods: {
    AddMatch() {
      this.$axios
        .post(`/api/Match/createMatch/${this.selectedTeam.Id}/${this.selectedChampionship.Id}`, this.newMatch)
        .then((response) => {
          console.log(response);
          console.log(this.newMatch);
          this.$emit("get-list");
          $("#match-add-modal").modal("hide");
          this.$swal.fire({
            title: "Succes",
            text: "Meciul a fost adăugat",
            icon: "success",
            showConfirmButton: false,
            timer: 1500,
          });
        })
        .catch((error) => {
          console.error(error);
        });
    },
    SelectShowDescription() {
      this.newMatch.PlayingHome =
        !this.newMatch.PlayingHome;
      console.log(this.newMatch.PlayingHome);
    },
    ClearModal() {
      this.$refs.addMatchFormRef.resetForm();
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

  