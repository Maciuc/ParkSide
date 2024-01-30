<template>
    <section>
        <div class="row header-section align-items-center">
            <div class="col">
                <div class="title-page">Participari staff</div>
            </div>

            <div class="col-auto">
                <button class="button green" @click="OpenModalAddStuffHistory">
                    <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
                    Adaugă
                </button>
                <AddStuffHistoryComponent @get-list="GetAllStuffHistories()" ref="addStuffHistoryModal">
                </AddStuffHistoryComponent>
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
                        v-on:keyup.enter="GetAllStuffHistories()" />
                </div>
            </div>

            <div class="col-xl-4 col-md-3 col-sm-4 mb-2 custom-date-picker">
                <!-- <VueDatePicker v-model="filter.StuffHistoryDate" format="dd/MM/yyyy" auto-apply utc :enable-time-picker="false"
                    @update:model-value="GetAllStuffHistories()" placeholder="Dată meci"></VueDatePicker> -->
                <select class="form-select form-control" aria-label="Default select example" placeholder="An"
                    v-model="filter.Year" @change="GetAllStuffHistories()">
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
                        <th width="22%" @click="OrderBy('stuffName')" class="cursor-pointer">
                            <font-awesome-icon v-if="filter.OrderBy === 'stuffName'" :icon="['fas', 'arrow-up-wide-short']"
                                style="color: #29be00" rotation="180" size="xl" class="me-2" />

                            <font-awesome-icon v-else-if="filter.OrderBy === 'stuffName_desc'"
                                :icon="['fas', 'arrow-up-short-wide']" rotation="180" style="color: #29be00" size="xl"
                                class="me-2" />
                            <font-awesome-icon v-else :icon="['fas', 'arrow-up-wide-short']" rotation="180" size="xl"
                                class="me-2" />
                            <span>Nume staff</span>
                        </th>

                        <th width="20%" @click="OrderBy('year')" class="cursor-pointer">
                            <font-awesome-icon v-if="filter.OrderBy === 'year'" :icon="['fas', 'arrow-up-wide-short']"
                                style="color: #29be00" rotation="180" size="xl" class="me-2" />

                            <font-awesome-icon v-else-if="filter.OrderBy === 'year_desc'"
                                :icon="['fas', 'arrow-up-short-wide']" rotation="180" style="color: #29be00" size="xl"
                                class="me-2" />
                            <font-awesome-icon v-else :icon="['fas', 'arrow-up-wide-short']" rotation="180" size="xl"
                                class="me-2" />
                            <span>An</span>
                        </th>
                        <th scope="20" width="20%">Echipa</th>
                        <th scope="20" width="20%">Rol</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(stuffHistory, index) in stuffHistories.Items" :key="index">

                        <td>
                            <div class="d-flex align-items-center">
                                <div class="img-container-avatar me-3">
                                    <img :src="ShowDynamicImage(stuffHistory.StuffImageBase64)"
                                        class="me-2 icon-avatar" />
                                </div>
                                <span>{{ stuffHistory.StuffLastName + " " + stuffHistory.StuffFirstName }}</span>
                            </div>
                        </td>
                        <td>{{ stuffHistory.Year }}</td>
                        <td>{{ stuffHistory.TeamName }}</td>
                        <td>{{ stuffHistory.Role }}</td>
                        <td>
                            <div class="editButtons">
                                <button class="button-edit" data-bs-toggle="modal" type="button"
                                    data-bs-target="#stuffHistory-edit-modal"
                                    @click="GetStuffHistoryForEdit(stuffHistory.Id)">
                                    <font-awesome-icon :icon="['far', 'pen-to-square']" />
                                </button>

                                <button type="button" class="button-delete" @click="DeleteStuffHistory(stuffHistory.Id)">
                                    <font-awesome-icon :icon="['fas', 'trash']" />
                                </button>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <Pagination :totalPages="stuffHistories.NumberOfPages" :currentPage="filter.PageNumber"
            @pagechanged="GetAllStuffHistories" />

        <!-- <EditStuffHistoryComponent :editedStuffHistory="selectedStuffHistoryForEdit" @get-list="GetAllStuffHistories" /> -->
    </section>
</template>
  
<script>
import AddStuffHistoryComponent from "../components/Modals/StuffsHistory/AddStuffHistoryComponent.vue";
//import EditStuffHistoryComponent from "../components/Modals/StuffsHistory/EditStuffHistoryComponent.vue";
import Pagination from "../components/Pagination.vue";
import { Form, Field, ErrorMessage } from "vee-validate";

export default {
    name: "StuffHistories",
    components: {
        AddStuffHistoryComponent,
        // EditStuffHistoryComponent,
        Pagination,
        Field
    },
    data() {
        return {
            selectedStuffHistoryForEdit: {},

            filter: {
                SearchText: "",
                PageNumber: 1,
                OrderBy: "year_desc",
                Year: "",
                Role: "",
                TeamName: "",

            },
            stuffHistories: {
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
            TeamNames: [
                { name: "CSU Suceava Juniori" },
                { name: "CSU Suceava Seniori" },
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
        OpenModalAddStuffHistory() {
            $("#stuffHistory-add-modal").modal("show");
            this.$refs.addStuffHistoryModal.ClearModal();
        },

        GetStuffHistoryForEdit(id) {
            this.$axios
                .get(`/api/StuffHistory/getStuffHistory/${id}`)
                .then((response) => {
                    this.selectedStuffHistoryForEdit = response.data;
                    console.log(this.selectedStuffHistoryForEdit);
                })
                .catch((error) => {
                    console.log(error);
                });
        },

        GetAllStuffHistories(page) {
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
                Role : this.filter.Role,
                TeamName: this.filter.TeamName,
            }
            this.$axios
                .get(
                    `api/StuffHistory/getStuffHistories?${new URLSearchParams(
                        searchParams
                    )}`
                )
                .then((response) => {
                    console.log(searchParams);
                    this.stuffHistories = response.data;
                    console.log(this.stuffHistories);
                })
                .catch((error) => {
                    console.log(error);
                });
        },

        DeleteStuffHistory(id) {
            this.$axios
                .delete(`/api/StuffHistory/deleteStuffHistory/${id}`)
                .then((response) => {
                    this.GetAllStuffHistories();
                    console.log(`Deleted stuffHistory with ID ${id}`);
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

            this.GetAllStuffHistories();
        },
    },

    created() {
        this.GetAllStuffHistories();
    },
};
</script>
  