<template>
    <!-- Modal -->
    <div class="modal fade custom-modal" id="playerHistory-add-modal" tabindex="-1" aria-labelledby="exampleModalLabel"
        :aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title mt-2">
                        Adaugă participare
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <Form @submit="AddPlayerHistory" :validation-schema="schema" v-slot="{ errors }" ref="addPlayerHistoryFormRef">
                    <div class="modal-body new-form">

                        <div class="mb-2 position-relative">
                                <label for="player" class="form-label">Alege jucator</label>
                                <Field v-model="selectedPlayer.Name" name="player" as="select" @change="updateSelectedPlayerId"
                                    :class="{ 'border-danger': errors.player }" class="form-select form-control">
                                    <option value="" disabled>Jucatori</option>
                                    <option v-for="(player, index) in playersList" :key="index" :value="player.Id">
                                        {{ player.Name }}
                                    </option>
                                </Field>
                                <ErrorMessage name="player" class="text-danger error-message" />
                            </div>

                        <div class="mb-2 position-relative">
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
                        </div>

                        <div class="mb-3 position-relative">
                            <label for="year" class="form-label"
                              >Alege anul</label
                            >
                            <Field
                              v-model="newPlayerHistory.Year"
                              name="year"
                              as="select"
                              :class="{ 'border-danger': errors.year }"
                              class="form-select form-control"
                            >
                              <option value="" disabled>An</option>
                              <option
                                v-for="(year, index) in Years"
                                :key="index"
                                :value="year.year"
                              >
                                {{ year.year }}
                              </option>
                            </Field>

                            <ErrorMessage name="year" class="text-danger error-message" />
                          </div>

                          <div class="mb-3 position-relative">
                              <label for="role" class="form-label"
                                >Alege rolul</label
                              >
                              <Field
                                v-model="newPlayerHistory.PlayerRole"
                                name="role"
                                as="select"
                                :class="{ 'border-danger': errors.role }"
                                class="form-select form-control"
                              >
                                <option value="" disabled>Roluri</option>
                                <option
                                  v-for="(role, index) in Roles"
                                  :key="index"
                                  :value="role.name"
                                >
                                  {{ role.name }}
                                </option>
                              </Field>

                              <ErrorMessage name="role" class="text-danger error-message" />
                            </div>

                            <div class="mb-3 position-relative">
                                <label for="team" class="form-label"
                                  >Alege echipa</label
                                >
                                <Field
                                  v-model="newPlayerHistory.TeamName"
                                  name="team"
                                  as="select"
                                  :class="{ 'border-danger': errors.team }"
                                  class="form-select form-control"
                                >
                                  <option value="" disabled>Echipe</option>
                                  <option
                                    v-for="(team, index) in TeamNames"
                                    :key="index"
                                    :value="team.name"
                                  >
                                    {{ team.name }}
                                  </option>
                                </Field>

                                <ErrorMessage name="team" class="text-danger error-message" />
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
    name: "AddPlayerHistoryComponent",
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
            playersList: [],
            selectedChampionship: {},
            selectedPlayer: {},
            newPlayerHistory: {
                PlayerRole: "",
                Year: "",
                TeamName: "",
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
            Roles: [
                { name: "Central" },
                { name: "Pivot" },
                { name: "Portar" },
                { name: "Inter stanga" },
                { name: "Inter dreapta" },
                { name: "Extrema stanga" },
                { name: "Extrema dreapta" },
            ],
            TeamNames: [
                { name: "CSU Suceava Juniori" },
                { name: "CSU Suceava Seniori" },
            ],
        };
    },

    computed: {
        schema() {
            return yup.object({
                championship: yup.string().required("Acest câmp este obligatoriu"),
                player: yup.string().required("Acest câmp este obligatoriu"),
                year: yup.string().required("Acest câmp este obligatoriu"),
                role: yup.string().required("Acest câmp este obligatoriu"),
                team: yup.string().required("Acest câmp este obligatoriu"),
            });
        },
    },

    methods: {
        AddPlayerHistory() {
            this.$axios
                .post(`/api/PlayerHistory/createPlayerHistory/${this.selectedPlayer.Id}/${this.selectedChampionship.Id}`, this.newPlayerHistory)
                .then((response) => {
                    console.log(response);
                    console.log(this.newPlayerHistory);
                    this.$emit("get-list");
                    $("#playerHistory-add-modal").modal("hide");
                    this.$swal.fire({
                        title: "Succes",
                        text: "Participarea a fost adăugata",
                        icon: "success",
                        showConfirmButton: false,
                        timer: 1500,
                    });
                })
                .catch((error) => {
                    console.error(error);
                });
        },
        ClearModal() {
            this.$refs.addPlayerHistoryFormRef.resetForm();
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
        GetPlayersList() {
            this.$axios
                .get("/api/Player/getPlayersDropDown")
                .then((response) => {
                    this.playersList = response.data;
                    console.log(response.data);
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        updateSelectedChampionshipId() {
            const selectedChamp = this.championshipsList.find(
                (champ) => champ.Id === this.selectedChampionship.Name
            );
            this.selectedChampionship.Id = selectedChamp ? selectedChamp.Id : null;
        },
        updateSelectedPlayerId() {
            const selectedPlay = this.playersList.find(
                (champ) => champ.Id === this.selectedPlayer.Name
            );
            this.selectedPlayer.Id = selectedPlay ? selectedPlay.Id : null;
        },
    },
    created() {
        this.GetChampionshipsList();
        this.GetPlayersList();
    },
};
</script>

  