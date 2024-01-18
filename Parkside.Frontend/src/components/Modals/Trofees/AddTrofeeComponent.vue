<template>
    <!-- Modal -->
    <div class="modal fade custom-modal" id="trofee-add-modal" tabindex="-1" aria-labelledby="exampleModalLabel"
        :aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title mt-2">
                        Adaugă trofeu
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <Form @submit="AddTrofee" :validation-schema="schema" v-slot="{ errors }" ref="addTrofeeFormRef">
                    <div class="modal-body new-form">
                        <div class="mb-3">
                            <label for="input-title-trofee-add" class="form-label">Nume trofeu</label>
                            <Field type="text" class="form-control" :class="{ 'border-danger': errors.name }"
                                id="input-title-trofee-add" name="name" placeholder="Nume trofeu" v-model="newTrofee.Name" />
                            <ErrorMessage name="name" class="text-danger error-message" />
                        </div>

                        <div class="row">
                            <div class="col-6 d-flex flex-column justify-content-center">
                                <label class="form-label">Selectează imagine</label>
                                <label for="input-upload-add-trofee" class="button blue" style="width: 140px">
                                    Încarcă imagine
                                    <font-awesome-icon :icon="['fas', 'upload']" />
                                    <Field type="file" id="input-upload-add-trofee" name="upload" style="display: none"
                                        accept="image/*" ref="uploadInputAddTrofee" @change="UploadImageTrofee">
                                    </Field>
                                </label>

                            </div>

                            <div class="col-6">
                                <div class="image-container d-flex align-items-center justify-content-center">
                                    <div v-if="!newTrofee.ImageBase64">
                                        <div class="d-flex flex-column justify-content-center align-items-center gap-2">
                                            <button type="button" class="button-delete">
                                                <font-awesome-icon :icon="['fas', 'trash']" />
                                            </button>
                                            <img src="@/images/NoImageSelected.png" class="no-image" />
                                            <div>Nicio imagine selectată</div>
                                        </div>
                                    </div>

                                    <div v-if="newTrofee.ImageBase64" class="image">
                                        <button type="button" class="button-delete" @click="DeletePhoto">
                                            <font-awesome-icon :icon="['fas', 'trash']" />
                                        </button>
                                        <img :src="newTrofee.ImageBase64" alt="Imagine selectată" class="image" />
                                    </div>
                                </div>

                                <div v-if="photoValidation === false" ref="validation-img-type"
                                    class="text-danger error-message">
                                    Tipul imaginii selectate nu este valid
                                </div>
                                <div v-else-if="photoValidation === true" ref="validation-img-type"
                                    class="text-danger error-message">
                                    Imaginea selectată este prea mare
                                </div>
                                <div v-else>
                                </div>
                            </div>
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
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";

export default {
    name: "AddTrofeeComponent",
    components: {
        Form,
        Field,
        ErrorMessage,
    },
    emits: ["get-list"],
    data() {
        return {
            photoValidation: null,
            newTrofee: {
                Name: "",
                ImageBase64: null,
            },
        };
    },
    methods: {
        AddTrofee() {
            this.$axios
                .post(`/api/Trofee/createTrofee`, this.newTrofee)
                .then((response) => {
                    console.log(response);
                    console.log(this.newTrofee);
                    this.$emit("get-list");
                    $("#trofee-add-modal").modal("hide");
                    this.$swal.fire({
                        title: "Succes",
                        text: "Trofeeul a fost adăugat",
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
            this.$refs.addTrofeeFormRef.resetForm();
            this.newTrofee.ImageBase64 = null;
        },
        UploadImageTrofee(event) {
            const selectedFile = event.target;
            const file = event.target.files[0];
            const reader = new FileReader();
            reader.onload = () => {
                if (reader.result.includes("image")) {
                    if (file.size / 1024 < 15360) {
                        this.photoValidation = null;
                        console.log(reader.result);
                        this.newTrofee.ImageBase64 = reader.result;
                        selectedFile.value = "";
                    } else {
                        this.photoValidation = true;
                    }
                } else {
                    this.photoValidation = false;
                }
            };
            if (file) {
                reader.readAsDataURL(file);
            }
        },
        DeletePhoto(selectedFile) {
            this.$refs.uploadInputAddTrofee.reset();
            this.newTrofee.ImageBase64 = null;
        },
    },

    computed: {
        schema() {
            return yup.object({
                name: yup.string().required("Denumirea nu este validă"),
            });
        },
    },
};
</script>
  
  