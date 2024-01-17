<template>
  <!-- Modal -->
  <div
    class="modal fade custom-modal"
    id="sponsor-add-modal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    :aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title mt-2" >
            Adaugă sponsor
          </h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>

        <Form
          @submit="AddSponsor"
          :validation-schema="schema"
          v-slot="{ errors }"
          ref="addSponsorFormRef"
        >
          <div class="modal-body new-form">
            <div class="mb-3">
              <label for="input-title-sponsor-add" class="form-label"
                >Nume sponsor</label
              >
              <Field
                type="text"
                class="form-control"
                :class="{ 'border-danger': errors.name }"
                id="input-title-sponsor-add"
                name="name"
                placeholder="Nume sponsor"
                v-model="newSponsor.Name"
              />
              <ErrorMessage name="name" class="text-danger error-message" />
            </div>

            <div class="mb-3">
              <label for="link" class="form-label">Adaugă link</label>
              <Field
                type="text"
                class="form-control"
                id="input-link-add-link"
                name="link"
                placeholder="Link"
                v-model="newSponsor.Link"
              />
            </div>

            <div class="row">
              <div class="col-6 d-flex flex-column justify-content-center">
                <label class="form-label">Selectează imagine</label>
                <label
                  for="input-upload-add-sponsor"
                  class="button blue"
                  style="width: 140px"
                >
                  Încarcă imagine
                  <font-awesome-icon :icon="['fas', 'upload']" />
                  <Field
                    type="file"
                    id="input-upload-add-sponsor"
                    name="upload"
                    style="display: none"
                    accept="image/*"
                    ref="uploadInputAddSponsor"
                    @change="UploadImageSponsor"
                  >
                  </Field>
                </label>

              </div>

              <div class="col-6">
                <div
                  class="image-container d-flex align-items-center justify-content-center"
                >
                  <div v-if="!newSponsor.ImageBase64">
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

                  <div v-if="newSponsor.ImageBase64" class="image">
                    <button
                      type="button"
                      class="button-delete"
                      @click="DeletePhoto"
                    >
                      <font-awesome-icon :icon="['fas', 'trash']" />
                    </button>
                    <img
                      :src="newSponsor.ImageBase64"
                      alt="Imagine selectată"
                      class="image"
                    />
                  </div>
                </div>

                <div
                  v-if="photoValidation===false"
                  ref="validation-img-type"
                  class="text-danger error-message"
                >
                  Tipul imaginii selectate nu este valid
                </div>
                <div
                  v-else-if="photoValidation===true"
                  ref="validation-img-type"
                  class="text-danger error-message"
                >
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
  name: "SponsorsAddSponsorComponent",
  components: {
    Form,
    Field,
    ErrorMessage,
  },
  emits: ["get-list"],
  data() {
    return {
      photoValidation: null,
      newSponsor: {
        Name: "",
        Link: "",
        ImageBase64: null,
      },
    };
  },
  methods: {
    AddSponsor() {
      this.$axios
        .post(`/api/Sponsor/createSponsor`, this.newSponsor)
        .then((response) => {
          console.log(response);
          console.log(this.newSponsor);
          this.$emit("get-list");
          $("#sponsor-add-modal").modal("hide");
          this.$swal.fire({
            title: "Succes",
            text: "Sponsorul a fost adăugat",
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
      this.$refs.addSponsorFormRef.resetForm();
      this.newSponsor.ImageBase64 = null;
    },
    UploadImageSponsor(event) {
      const selectedFile = event.target;
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        if (reader.result.includes("image")) {
          if (file.size / 1024 < 15360) {
            this.photoValidation = null;
            console.log(reader.result);
            this.newSponsor.ImageBase64 = reader.result;
            selectedFile.value = "";
          }else{
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
      this.$refs.uploadInputAddSponsor.reset();
      this.newSponsor.ImageBase64 = null;
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

