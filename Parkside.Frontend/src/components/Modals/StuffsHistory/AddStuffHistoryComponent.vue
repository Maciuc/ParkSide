<template>
    <!-- Modal -->
    <div class="modal fade custom-modal" id="stuffHistory-add-modal" tabindex="-1" aria-labelledby="exampleModalLabel"
        :aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title mt-2">
                        Adaugă participare
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <Form @submit="AddStaffHistory" :validation-schema="schema" v-slot="{ errors }"
                    ref="addStaffHistoryFormRef">
                    <div class="modal-body new-form">

                        <!-- <div :class="{ 'invalid-input': errors.date }" class="col custom-date-picker mb-2">
                            <label class="form-label">Dată meci</label>
                            <Field v-slot="{ field }" name="date" id="date">
                                <VueDatePicker v-bind="field" v-model="newStaffHistory.StaffHistoryDate" auto-apply utc
                                    :enable-time-picker="false" placeholder="Dată meci"></VueDatePicker>
                            </Field>
                            <ErrorMessage name="date" class="text-danger error-message" />
                        </div> -->

                        <div class="mb-2 position-relative">
                            <label for="stuff" class="form-label">Alege staff</label>
                            <Field v-model="selectedStaff.Name" name="stuff" as="select" @change="updateSelectedStaffId"
                                :class="{ 'border-danger': errors.stuff }" class="form-select form-control">
                                <option value="" disabled>Staff</option>
                                <option v-for="(stuff, index) in stuffsList" :key="index" :value="stuff.Id">
                                    {{ stuff.Name }}
                                </option>
                            </Field>
                            <ErrorMessage name="stuff" class="text-danger error-message" />
                        </div>

                        <div class="mb-3 position-relative">
                            <label for="year" class="form-label">Alege anul</label>
                            <Field v-model="newStaffHistory.Year" name="year" as="select"
                                :class="{ 'border-danger': errors.year }" class="form-select form-control">
                                <option value="" disabled>An</option>
                                <option v-for="(year, index) in Years" :key="index" :value="year.year">
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
                                    v-model="newStaffHistory.Role"
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
                            <label for="team" class="form-label">Alege echipa</label>
                            <Field v-model="newStaffHistory.TeamName" name="team" as="select"
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
    name: "AddStaffHistoryComponent",
    components: {
        Form,
        Field,
        ErrorMessage,
        VueDatePicker,
    },
    emits: ["get-list"],
    data() {
        return {
            stuffsList: [],
            selectedStaff: {},
            newStaffHistory: {
                Year: "",
                TeamName: "",
                Role: "",
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
                { name: "Antrenor" },
                { name: "Kinetoterapeut" },
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
                stuff: yup.string().required("Acest câmp este obligatoriu"),
                year: yup.string().required("Acest câmp este obligatoriu"),
                team: yup.string().required("Acest câmp este obligatoriu"),
                role: yup.string().required("Acest câmp este obligatoriu"),
            });
        },
    },

    methods: {
        AddStaffHistory() {
            this.$axios
                .post(`/api/StuffHistory/createStuffHistory/${this.selectedStaff.Id}`, this.newStaffHistory)
                .then((response) => {
                    console.log(response);
                    console.log(this.newStaffHistory);
                    this.$emit("get-list");
                    $("#stuffHistory-add-modal").modal("hide");
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
                    console.log(this.selectedStaff.Id);
                    console.log(this.newStaffHistory);
                });
        },
        ClearModal() {
            this.$refs.addStaffHistoryFormRef.resetForm();
        },
        GetStaffsList() {
            this.$axios
                .get("/api/Stuff/getStuffsDropDown")
                .then((response) => {
                    this.stuffsList = response.data;
                    console.log(response.data);
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        updateSelectedStaffId() {
            const selectedPlay = this.stuffsList.find(
                (champ) => champ.Id === this.selectedStaff.Name
            );
            this.selectedStaff.Id = selectedPlay ? selectedPlay.Id : null;
        },
    },
    created() {
        this.GetStaffsList();
    },
};
</script>

  