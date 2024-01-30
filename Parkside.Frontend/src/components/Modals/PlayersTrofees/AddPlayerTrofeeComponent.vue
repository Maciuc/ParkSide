<template>
    <!-- Modal -->
    <div class="modal fade custom-modal" id="playerTrofee-add-modal" tabindex="-1" aria-labelledby="exampleModalLabel"
        :aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title mt-2">
                        Adaugă trofeul jucatorului
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <Form @submit="AddPlayerTrofee" :validation-schema="schema" v-slot="{ errors }"
                    ref="addPlayerTrofeeFormRef">
                    <div class="modal-body new-form">

                        <div class="mb-2 position-relative">
                            <label for="participation" class="form-label">Alege participare</label>
                            <Field v-model="selectedParticipation.PlayerHistoryName" name="participation" as="select" @change="updateSelectedParticipationId"
                                :class="{ 'border-danger': errors.participation }" class="form-select form-control">
                                <option value="" disabled>Participari</option>
                                <option v-for="(part, index) in participationsList" :key="index" :value="part.Id">
                                    {{ part.PlayerHistoryName }}
                                </option>
                            </Field>
                            <ErrorMessage name="participation" class="text-danger error-message" />
                        </div>

                        <div class="mb-2 position-relative">
                                <label for="trofee" class="form-label">Alege trofeul</label>
                                <Field v-model="selectedTrofee.Name" name="player" as="select" @change="updateSelectedTrofeeId"
                                    :class="{ 'border-danger': errors.trofee }" class="form-select form-control">
                                    <option value="" disabled>Trofee</option>
                                    <option v-for="(trofee, index) in trofeesList" :key="index" :value="trofee.Id">
                                        {{ trofee.Name }}
                                    </option>
                                </Field>
                                <ErrorMessage name="trofee" class="text-danger error-message" />
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
    name: "AddPlayerTrofeeComponent",
    components: {
        Form,
        Field,
        ErrorMessage,
        VueDatePicker,
    },
    emits: ["get-list"],
    data() {
        return {
            participationsList: [],
            selectedParticipation: {},
            trofeesList: [],
            selectedTrofee: {},
        };
    },

    computed: {
        schema() {
            return yup.object({
                participation: yup.string().required("Acest câmp este obligatoriu"),
            });
        },
    },

    methods: {
        AddPlayerTrofee() {
            this.$axios
                .post(`/api/PlayerTrofee/createPlayerTrofee/${this.selectedParticipation.Id}/${this.selectedTrofee.Id}`)
                .then((response) => {
                    console.log(response);
                    this.$emit("get-list");
                    $("#playerTrofee-add-modal").modal("hide");
                    this.$swal.fire({
                        title: "Succes",
                        text: "Trofeul jucatorului a fost adăugat",
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
            this.$refs.addPlayerTrofeeFormRef.resetForm();
        },
        GetParticipationsList() {
            this.$axios
                .get("/api/PlayerHistory/getPlayerHistoryDropDown")
                .then((response) => {
                    this.participationsList = response.data;
                    console.log(response.data);
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        GetTrofeesList() {
            this.$axios
                .get("/api/Trofee/getTrofeesDropDown")
                .then((response) => {
                    this.trofeesList = response.data;
                    console.log(response.data);
                })
                .catch((error) => {
                    console.log(error);
                });
        },
        updateSelectedParticipationId() {
            const selectedPart = this.participationsList.find(
                (part) => part.Id === this.selectedParticipation.PlayerHistoryName
            );
            this.selectedParticipation.Id = selectedPart ? selectedPart.Id : null;
        },
        updateSelectedTrofeeId() {
            const selectedTrof = this.trofeesList.find(
                (trof) => trof.Id === this.selectedTrofee.Name
            );
            this.selectedTrofee.Id = selectedTrof ? selectedTrof.Id : null;
        },
    },
    created() {
        this.GetParticipationsList();
        this.GetTrofeesList();
    },
};
</script>

  