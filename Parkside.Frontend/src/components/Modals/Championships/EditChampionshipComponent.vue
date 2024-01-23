<template>
    <!-- Modal -->
    <div
      class="modal fade custom-modal"
      id="championship-edit-modal"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title mt-2" >
              Editare campionat
            </h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>


          <Form
            @submit="SaveEditedChampionship"
            :validation-schema="schema"
            v-slot="{ errors }"
            ref="editChampionshipFormRef"
          >
            <div class="modal-body new-form">
              <div class="mb-3">
                <label for="input-title-edit-championship" class="form-label"
                  >Nume campionat</label
                >
                <Field
                  type="text"
                  class="form-control"
                  :class="{ 'border-danger': errors.name }"
                  id="input-title-edit-championship"
                  name="name"
                  placeholder="Nume campionat"
                  v-model="editedChampionship.Name"
                />
                <ErrorMessage name="name" class="text-danger error-message" />
              </div>

              <div class="row">
                <div class="col-6 d-flex flex-column justify-content-center">
                  <label class="form-label">Selectează imagine</label>
                  <label
                    for="input-upload-edit"
                    class="button blue"
                    style="width: 140px"
                  >
                    Încarcă imagine
                    <font-awesome-icon :icon="['fas', 'upload']" />
                    <Field
                      type="file"
                      id="input-upload-edit"
                      name="upload"
                      style="display: none"
                      accept="image/*"
                      ref="uploadInputEditChampionship"
                      @change="UploadImageChampionshipEdit"
                    >
                    </Field>
                  </label>
                </div>

                <div class="col-6">
                  <div
                    class="image-container d-flex align-items-center justify-content-center"
                  >
                    <div v-if="!editedChampionship.ImageBase64">
                      <div
                        class="d-flex flex-column justify-content-center align-items-center gap-2"
                      >
                        <button type="button" class="button-delete">
                          <font-awesome-icon :icon="['fas', 'trash']" />
                        </button>
                        <img
                          src="@/images/NoImageSelected.png"
                          class="no-image"
                        />
                        <div>Nicio imagine selectată</div>
                      </div>
                    </div>

                    <div v-if="editedChampionship.ImageBase64" class="image">
                      <button
                        type="button"
                        class="button-delete"
                        @click="DeletePhoto"
                      >
                        <font-awesome-icon :icon="['fas', 'trash']" />
                      </button>
                      <img
                        :src="editedChampionship.ImageBase64"
                        alt="Imagine selectată"
                        class="image"
                      />
                    </div>
                  </div>
                  <div
                    v-if="PhotoValidation === false"
                    ref="validation-img-type"
                    class="text-danger error-message"
                  >
                    Tipul imaginii selectate nu este valid
                  </div>
                  <div
                    v-else-if="PhotoValidation === true"
                    ref="validation-img-type"
                    class="text-danger error-message"
                  >
                    Imaginea selectată este prea mare
                  </div>
                  <div v-else></div>
                </div>
              </div>
            </div>


            <div class="modal-footer justify-content-between">
              <button type="button" class="button grey" data-bs-dismiss="modal">
                Anulare
              </button>
              <button
                type="submit"
                class="button green"
              >
                Salvare
              </button>
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
    name: "EditChampionshipComponent",
    components: {
      Form,
      Field,
      ErrorMessage,
    },
    data() {
      return {
        PhotoValidation: null,
      };
    },
    props: {
      editedChampionship: {
        type: Object,
        default() {
          return { Name: "", Id: "", ImageBase64: "" };
        },
      },
    },
  
    methods: {
      SaveEditedChampionship() {
        this.$axios
          .put(`/api/Championship/updateChampionship/${this.editedChampionship.Id}`, this.editedChampionship)
          .then((response) => {
            console.log(response);
            this.$emit("get-list");
            $("#championship-edit-modal").modal("hide");
            this.$swal.fire({
              title: "Succes",
              text: "Campionatul a fost editat",
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
        this.$refs.editChampionshipFormRef.resetForm();
      },
      UploadImageChampionshipEdit(event) {
        const selectedFile = event.target;
        const file = event.target.files[0];
        const reader = new FileReader();
        reader.onload = () => {
          if (reader.result.includes("image")) {
            if (file.size / 1024 < 15360) {
              this.PhotoValidation = null;
              console.log(reader.result);
              this.editedChampionship.ImageBase64 = reader.result;
              selectedFile.value = "";
            } else {
              this.PhotoValidation = true;
            }
          } else {
            this.PhotoValidation = false;
          }
        };
        if (file) {
          reader.readAsDataURL(file);
        }
      },
      DeletePhoto() {
        this.$refs.uploadInputEditChampionship.reset();
        this.editedChampionship.ImageBase64 = null;
        this.PhotoValidation = null;
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
  