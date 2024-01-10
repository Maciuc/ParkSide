<template>
  <div>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Adăugare membru</div>
      </div>

      <div class="col-auto">
        <button class="button green" type="submit" form="form-add-member">
          Salvează
        </button>
      </div>
    </div>
  </div>

  <Form
    @submit="AddMember(this.newMember.OrderNumber)"
    :validation-schema="schema"
    v-slot="{ errors }"
    id="form-add-member"
  >
    <div class="new-form">
      <div class="row mt-4">
        <div class="col-4">
          <div class="mb-3">
            <label for="input-name-add-member" class="form-label"
              >Nume membru</label
            >
            <Field
              type="text"
              class="form-control"
              :class="{ 'border-danger': errors.name }"
              id="input-name-add-member"
              name="name"
              placeholder="Nume membru"
              v-model="newMember.Name"
            />
            <ErrorMessage name="name" class="text-danger error-message" />
          </div>

          <div class="mb-3 position-relative">
            <label for="platform" class="form-label"
              >Funcția în organizație</label
            >
            <Field
              v-model="newMember.OrganizationFunction"
              name="function"
              as="select"
              :class="{ 'border-danger': errors.function }"
              class="form-select form-control"
            >
              <option value="" disabled>Funcția în organizație</option>
              <option
                v-for="(plat, index) in platforms"
                :key="index"
                :value="plat.name"
              >
                {{ plat.name }}
              </option>
            </Field>

            <ErrorMessage name="function" class="text-danger error-message" />
          </div>

          <div class="mb-3 position-relative">
            <label for="input-birth-date-member" class="form-label"
              >Dată naștere</label
            >
            <Field
              type="text"
              class="form-control"
              :class="{ 'border-danger': errors.birthdate }"
              id="input-birth-date-member"
              name="birthdate"
              placeholder="Dată naștere"
              v-model="newMember.BirthDate"
            />

            <ErrorMessage name="birthdate" class="text-danger error-message" />
          </div>

          <div class="mb-3">
            <label for="input-name-add-member" class="form-label"
              >Numar ordine</label
            >
            <Field
              type="text"
              class="form-control"
              :class="{ 'border-danger': errors.order }"
              id="input-name-add-member"
              name="order"
              placeholder="Numar ordine"
              v-model="newMember.OrderNumber"
            />
            <ErrorMessage name="order" class="text-danger error-message" />
            <div
              v-if="validOrderNumber === false"
              class="text-danger error-message"
            >
              Numărul de ordine este deja ocupat!
            </div>
          </div>
        </div>
        <div class="col-4">
          <div class="d-flex gap-2">
            <div>
              <label class="form-label">Selectează imagine</label>
              <label
                for="input-upload-member-image"
                class="button blue"
                style="width: 140px"
              >
                Încarcă imagine
                <font-awesome-icon :icon="['fas', 'upload']" />
                <Field
                  type="file"
                  id="input-upload-member-image"
                  name="upload"
                  style="display: none"
                  accept="image/*"
                  ref="uploadInput"
                  @change="UploadImageMember"
                >
                </Field>
              </label>
            </div>

            <div>
              <div
                class="image-container d-flex align-items-center justify-content-center"
              >
                <button
                  type="button"
                  class="button-delete"
                  @click="DeletePhoto"
                >
                  <font-awesome-icon :icon="['fas', 'trash']" />
                </button>
                <div
                  v-if="!newMember.ImageBase64"
                  class="d-flex flex-column justify-content-center align-items-center gap-2"
                >
                  <img src="@/images/NoImageSelected.png" class="no-image" />
                  <div>Nicio imagine selectată</div>
                </div>

                <div v-if="newMember.ImageBase64" class="image">
                  <img
                    :src="newMember.ImageBase64"
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
      </div>
      <div class="row ">
        <div class="col-8">
          <div class="form-check form-switch">
            <input
              class="form-check-input"
              type="checkbox"
              id="flexSwitchCheckDefault"
              :checked="newMember.VizibilitySpeech"
              @click="SelectShowDescription"
            />
            <label class="form-label" for="flexSwitchCheckDefault"
              >Afișare discurs de bun venit</label
            >
          </div>

          <div class="mb-3 position-relative">
            <label for="textarea-description-add-project" class="form-label"
              >Descriere</label
            >
            <Field
              v-slot="{ field }"
              v-model="newMember.Speech"
              name="description"
            >
              <textarea
                v-bind="field"
                name="description"
                class="form-control textarea"
                rows="5"
                placeholder="Descriere"
              />
            </Field>
          </div>
        </div>
      </div>
    </div>
  </Form>
</template>

<script>
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";
export default {
  name: "MembersAddMemberComponent",
  components: {
    Form,
    Field,
    ErrorMessage,
  },
  data() {
    return {
      photoValidation: null,
      validOrderNumber: true,
      newMember: {
        VizibilitySpeech: false,
        Name: "",
        OrganizationFunction: "",
        BirthDate: "",
        Speech: "",
        OrderNumber: "",
        ImageBase64: null,
      },
      platforms: [
        { name: "Director executiv" },
        { name: "Manager general" },
        { name: "Manager departament tehnic" },
        { name: "Administrator" },
        { name: "Contabil" },
      ],
    };
  },

  computed: {
    schema() {
      return yup.object({
        name: yup.string().required("Acest câmp este obligatoriu"),
        function: yup.string().required("Acest câmp este obligatoriu"),
        birthdate: yup.string().required("Acest câmp este obligatoriu"),
        order: yup.string().required("Acest câmp este obligatoriu"),
      });
    },
  },
  methods: {
    SelectShowDescription() {
      this.newMember.VizibilitySpeech =
        !this.newMember.VizibilitySpeech;
      console.log(this.newMember.VizibilitySpeech);
    },
    AddMember(orderNumber) {
      this.$axios
        .get(`/api/Member/getMembers`)
        .then((response) => {
          if (response.data.Items.find((x) => x.OrderNumber == orderNumber)) {
            console.log("order number not ok: " + false);
            this.validOrderNumber = false;
          } else {
            //console.log("order number ok: " + true);
            this.$axios
              .post(`/api/Member/createMember`, this.newMember)
              .then((response) => {
                console.log(response);
                this.$router.push({ name: "members" });
              })
              .catch((error) => {
                console.error(error);
              });
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    UploadImageMember(event) {
      const selectedFile = event.target;
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        if (reader.result.includes("image")) {
          if (file.size / 1024 < 15360) {
            this.photoValidation = null;
            console.log(reader.result);
            this.newMember.ImageBase64 = reader.result;
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
    DeletePhoto() {
      this.$refs.uploadInput.reset();
      this.newMember.ImageBase64 = null;
    },
  },
};
</script>

<style scoped>
.image-container {
  width: 190px;
}
</style>
