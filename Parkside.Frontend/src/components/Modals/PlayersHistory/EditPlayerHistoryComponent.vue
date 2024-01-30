<template>
    <!-- Modal -->
    <div class="modal fade custom-modal" id="playerHistory-edit-modal" tabindex="-1" aria-labelledby="exampleModalLabel"
        :aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title mt-2">
                        Adaugă participare
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <Form @submit="EditPlayerHistory" :validation-schema="schema" v-slot="{ errors }"
                    ref="editPlayerHistoryFormRef">
                    <div class="modal-body new-form">

                         <div class="mb-2 position-relative">
                      <label for="player" class="form-label">Alege jucator</label>
                      <Field
                        v-slot="{ field }"
                        name="player"
                        class="form-control"
                        v-model="editedPlayerHistory.Player"
                      >
                        <div class="custom dropdown custom-dropdown" v-bind="field">
                          <button
                            class="btn btn-secondary dropdown-toggle"
                            :class="{ 'border-danger': errors.player }"
                            type="button"
                            id="dropdownPlayers"
                            data-bs-toggle="dropdown"
                            aria-expanded="false"
                          >
                            <span>
                              {{ editedPlayerHistory.Player?.Name }}
                            </span>
                          </button>
                          <ul
                            class="dropdown-menu"
                            aria-labelledby="dropdownPlayers"
                          >
                            <li v-for="(player, index) in playersList" :key="index">
                              <a
                                class="dropdown-item"
                                @click="editedPlayerHistory.Player = player"
                                >{{ player.Name }}</a
                              >
                            </li>
                          </ul>
                        </div>
                      </Field>

                      <ErrorMessage name="player" class="text-danger error-message" />
                    </div>

                    <div class="mb-2 position-relative">
                  <label for="championship" class="form-label">Alege campionat</label>
                  <Field
                    v-slot="{ field }"
                    name="championship"
                    class="form-control"
                    v-model="editedPlayerHistory.Championship"
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
                          {{ editedPlayerHistory.Championship?.Name }}
                        </span>
                      </button>
                      <ul
                        class="dropdown-menu"
                        aria-labelledby="dropdownChampionships"
                      >
                        <li v-for="(champ, index) in championshipsList" :key="index">
                          <a
                            class="dropdown-item"
                            @click="editedPlayerHistory.Championship = champ"
                            >{{ champ.Name }}</a
                          >
                        </li>
                      </ul>
                    </div>
                  </Field>

                  <ErrorMessage name="championship" class="text-danger error-message" />
                </div>

                        <div class="mb-3 position-relative">
                            <label for="year" class="form-label">Alege anul</label>
                            <Field v-model="editedPlayerHistory.Year" name="year" as="select"
                                :class="{ 'border-danger': errors.year }" class="form-select form-control">
                                <option value="" disabled>An</option>
                                <option v-for="(year, index) in Years" :key="index" :value="year.year">
                                    {{ year.year }}
                                </option>
                            </Field>

                            <ErrorMessage name="year" class="text-danger error-message" />
                        </div>

                        <div class="mb-3 position-relative">
                            <label for="role" class="form-label">Alege rolul</label>
                            <Field v-model="editedPlayerHistory.PlayerRole" name="role" as="select"
                                :class="{ 'border-danger': errors.role }" class="form-select form-control">
                                <option value="" disabled>Roluri</option>
                                <option v-for="(role, index) in Roles" :key="index" :value="role.name">
                                    {{ role.name }}
                                </option>
                            </Field>

                            <ErrorMessage name="role" class="text-danger error-message" />
                        </div>

                        <div class="mb-3 position-relative">
                            <label for="team" class="form-label">Alege echipa</label>
                            <Field v-model="editedPlayerHistory.TeamName" name="team" as="select"
                                :class="{ 'border-danger': errors.team }" class="form-select form-control">
                                <option value="" disabled>Echipe</option>
                                <option v-for="(team, index) in TeamNames" :key="index" :value="team.name">
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
    name: "EditPlayerHistoryComponent",
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
    props: {
        editedPlayerHistory: {
            type: Object,
            default() {
                return {
                    Id: "",
                    PlayerRole: "",
                    Year: "",
                    TeamName: "",
                };
            },
        },
    },
    computed: {
        schema() {
            return yup.object({
                championship: yup.object().required("Acest câmp este obligatoriu"),
                player: yup.object().required("Acest câmp este obligatoriu"),
                year: yup.string().required("Acest câmp este obligatoriu"),
                role: yup.string().required("Acest câmp este obligatoriu"),
                team: yup.string().required("Acest câmp este obligatoriu"),
            });
        },
    },

    methods: {
        EditPlayerHistory() {
            this.$axios
                .put(`/api/PlayerHistory/updatePlayerHistory/${this.editedPlayerHistory.Id}/${this.editedPlayerHistory.Player.Id}/${this.editedPlayerHistory.Championship.Id}`, this.editedPlayerHistory)
                .then((response) => {
                    console.log(response);
                    console.log(this.editedPlayerHistory);
                    this.$emit("get-list");
                    $("#playerHistory-edit-modal").modal("hide");
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
            this.$refs.editPlayerHistoryFormRef.resetForm();
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

  