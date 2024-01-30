<template>
    <section>
        <div class="row header-section align-items-center">
            <div class="col">
                <div class="title-page">Trofee jucatori</div>
            </div>

            <div class="col-auto">
                <button class="button green" @click="OpenModalAddPlayerTrofee">
                    <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
                    Adaugă
                </button>
                <AddPlayerTrofeeComponent @get-list="GetAllPlayerTrofees()" ref="addPlayerTrofeeModal">
                </AddPlayerTrofeeComponent>
            </div>
        </div>

        <div class="row mt-3 new-form">
            <div class="col-xxl-4 col-xl-4 col-lg-3 col-md-4 col-sm-6">
                <div class="input-group-custom mb-3 custom-control">
                    <div class="d-flex justify-content-center align-items-center search-separator">
                        <font-awesome-icon class="search_icon" :icon="['fas', 'magnifying-glass']" style="color: #688088" />
                        <div class="separator"></div>
                    </div>
                    <input type="text" class="form-control search" placeholder="Caută" aria-label="Username"
                        aria-describedby="basic-addon1" v-model="filter.SearchText"
                        v-on:keyup.enter="GetAllPlayerTrofees()" />
                </div>
            </div>

            <div class="col-xl-4 col-md-3 col-sm-4 mb-2 custom-date-picker">
                <select class="form-select form-control" aria-label="Default select example" placeholder="An"
                    v-model="filter.Year" @change="GetAllPlayerTrofees()">
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
                        <th width="20%" @click="OrderBy('playerName')" class="cursor-pointer">
                            <font-awesome-icon v-if="filter.OrderBy === 'playerName'" :icon="['fas', 'arrow-up-wide-short']"
                                style="color: #29be00" rotation="180" size="xl" class="me-2" />

                            <font-awesome-icon v-else-if="filter.OrderBy === 'playerName_desc'"
                                :icon="['fas', 'arrow-up-short-wide']" rotation="180" style="color: #29be00" size="xl"
                                class="me-2" />
                            <font-awesome-icon v-else :icon="['fas', 'arrow-up-wide-short']" rotation="180" size="xl"
                                class="me-2" />
                            <span>Nume jucator</span>
                        </th>

                        <th scope="20" width="20%">Campionat</th>
                        <th scope="20" width="20%">Trofeu</th>

                        <th width="10%" @click="OrderBy('year')" class="cursor-pointer">
                            <font-awesome-icon v-if="filter.OrderBy === 'year'" :icon="['fas', 'arrow-up-wide-short']"
                                style="color: #29be00" rotation="180" size="xl" class="me-2" />

                            <font-awesome-icon v-else-if="filter.OrderBy === 'year_desc'"
                                :icon="['fas', 'arrow-up-short-wide']" rotation="180" style="color: #29be00" size="xl"
                                class="me-2" />
                            <font-awesome-icon v-else :icon="['fas', 'arrow-up-wide-short']" rotation="180" size="xl"
                                class="me-2" />
                            <span>An</span>
                        </th>
                        <th scope="20" width="15%">Echipa</th>
                        <th scope="20" width="15%">Rol</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(playerTrofee, index) in playerTrofees.Items" :key="index">

                        <td>
                            <div class="d-flex align-items-center">
                                <div class="img-container-avatar me-3">
                                    <img :src="ShowDynamicImage(playerTrofee.PlayerImageBase64)"
                                        class="me-2 icon-avatar" />
                                </div>
                                <span>{{ playerTrofee.PlayerLastName + " " + playerTrofee.PlayerFirstName }}</span>
                            </div>
                        </td>
                        <td>
                            <div class="d-flex align-items-center">
                                <div class="img-container-avatar me-3">
                                    <img :src="ShowDynamicImage(playerTrofee.ChampionshipImageBase64)"
                                        class="me-2 icon-avatar" />
                                </div>
                                <span>{{ playerTrofee.ChampionshipName }}</span>
                            </div>
                        </td>
                        <td>
                                <div class="d-flex align-items-center">
                                    <div class="img-container-avatar me-3">
                                        <img :src="ShowDynamicImage(playerTrofee.TrofeeImageBase64)"
                                            class="me-2 icon-avatar" />
                                    </div>
                                    <span>{{ playerTrofee.TrofeeName }}</span>
                                </div>
                            </td>
                        <td>{{ playerTrofee.Year }}</td>
                        <td>{{ playerTrofee.TeamName }}</td>
                        <td>{{ playerTrofee.PlayerRole }}</td>
                        <td>
                                <div class="editButtons">
                                    <button type="button" class="button-delete" @click="DeletePlayerTrofee(playerTrofee.Id)">
                                        <font-awesome-icon :icon="['fas', 'trash']" />
                                    </button>
                                </div>
                            </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <Pagination :totalPages="playerTrofees.NumberOfPages" :currentPage="filter.PageNumber"
            @pagechanged="GetAllPlayerTrofees" />

    </section>
</template>
  
<script>
import AddPlayerTrofeeComponent from "../components/Modals/PlayersTrofees/AddPlayerTrofeeComponent.vue";
import Pagination from "../components/Pagination.vue";
import { Form, Field, ErrorMessage } from "vee-validate";

export default {
    name: "PlayerTrofees",
    components: {
        AddPlayerTrofeeComponent,
        Pagination,
        Field
    },
    data() {
        return {
            filter: {
                SearchText: "",
                PageNumber: 1,
                OrderBy: "year_desc",
                Year: ""
            },
            playerTrofees: {
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
        OpenModalAddPlayerTrofee() {
            $("#playerTrofee-add-modal").modal("show");
            this.$refs.addPlayerTrofeeModal.ClearModal();
        },

        GetAllPlayerTrofees(page) {
            this.filter.PageNumber = 1;
            if (page) {
                this.filter.PageNumber = page;
            }
            const searchParams = {
                OrderBy: this.filter.OrderBy,
                PageNumber: this.filter.PageNumber,
                PageSize: 6,
                NameSearch: this.filter.SearchText,
                Year: this.filter.Year,
            };
            this.$axios
                .get(
                    `api/PlayerTrofee/getPlayerTrofees?${new URLSearchParams(
                        searchParams
                    )}`
                )
                .then((response) => {
                    console.log(searchParams);
                    this.playerTrofees = response.data;
                })
                .catch((error) => {
                    console.log(error);
                });
        },

        DeletePlayerTrofee(id) {
            this.$axios
                .delete(`/api/PlayerTrofee/deletePlayerTrofee/${id}`)
                .then((response) => {
                    this.GetAllPlayerTrofees();
                    console.log(`Deleted playerTrofee with ID ${id}`);
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

            this.GetAllPlayerTrofees();
        },
    },

    created() {
        this.GetAllPlayerTrofees();
    },
};
</script>
  