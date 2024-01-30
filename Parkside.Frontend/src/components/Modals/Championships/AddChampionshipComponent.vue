<template>
  <!-- Modal -->
  <div
    class="modal fade custom-modal"
    id="championship-add-modal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    :aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title mt-2">Adaugă campionat</h5>
          <button
            championship="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <Form
          @submit="AddChampionship()"
          :validation-schema="schema"
          v-slot="{ errors }"
          ref="addChampionshipFormRef"
          class="new-form"
        >
          <div class="modal-body new-form">
            <div class="mb-3">
              <label for="input-title-championship" class="form-label"
                >Denumire</label
              >
              <Field
                championship="text"
                class="form-control"
                :class="{ 'border-danger': errors.name }"
                id="input-title-championship"
                name="name"
                placeholder="Denumire"
                v-model="newChampionship.Name"
              />
              <ErrorMessage name="name" class="text-danger error-message" />
            </div>

            <div class="row">
              <div class="col-6 d-flex flex-column justify-content-center">
                <label class="form-label">Selectează imagine</label>
                <label
                  for="input-upload-add-championship"
                  class="button blue"
                  style="width: 140px"
                >
                  Încarcă imagine
                  <font-awesome-icon :icon="['fas', 'upload']" />
                  <Field
                    type="file"
                    id="input-upload-add-championship"
                    name="upload"
                    style="display: none"
                    accept="image/*"
                    ref="uploadInputAddChampionship"
                    @change="UploadImageChampionship"
                  >
                  </Field>
                </label>
              </div>

              <div class="col-6">
                <div
                  class="image-container d-flex align-items-center justify-content-center"
                >
                  <div v-if="!newChampionship.ImageBase64">
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

                  <div v-if="newChampionship.ImageBase64" class="image">
                    <button
                      type="button"
                      class="button-delete"
                      @click="DeletePhoto"
                    >
                      <font-awesome-icon :icon="['fas', 'trash']" />
                    </button>
                    <img
                      :src="newChampionship.ImageBase64"
                      alt="Imagine selectată"
                      class="image"
                    />
                  </div>
                </div>

                <div
                  v-if="photoValidation === false"
                  ref="validation-img-type"
                  class="text-danger error-message"
                >
                  Tipul imaginii selectate nu este valid
                </div>
                <div
                  v-else-if="photoValidation === true"
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
            <button
              championship="button"
              class="button grey"
              data-bs-dismiss="modal"
            >
              Anulare
            </button>
            <button championship="submit" class="button green">Salvare</button>
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
  name: "AddChampionshipModalComponent",
  components: {
    Form,
    Field,
    ErrorMessage,
  },
  emits: ["get-list"],
  data() {
    return {
      photoValidation: null,
      newChampionship: {
        Name: "",
      },
    };
  },
  methods: {
    AddChampionship() {
      this.$axios
        .post(`/api/Championship/createChampionship`, this.newChampionship)
        .then((response) => {
          console.log(response);
          this.$emit("get-list");
          $("#championship-add-modal").modal("hide");
          this.$swal.fire({
            title: "Succes",
            text: "Echipa a fost adăugata",
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
      this.$refs.addChampionshipFormRef.resetForm();
    },
    UploadImageChampionship(event) {
      const selectedFile = event.target;
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        if (reader.result.includes("image")) {
          if (file.size / 1024 < 15360) {
            this.photoValidation = null;
            console.log(reader.result);
            this.newChampionship.ImageBase64 = reader.result;
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
      this.$refs.uploadInputAddChampionship.reset();
      this.newChampionship.ImageBase64 = null;
    },
  },

  computed: {
    schema() {
      return yup.object({
        name: yup
          .string()
          .required("Acest câmp este obligatoriu")
          .min(3, "Minim 3 caractere!")
          .max(200, "Maxim 200 de caractere!"),
      });
    },
  },
};
</script>
