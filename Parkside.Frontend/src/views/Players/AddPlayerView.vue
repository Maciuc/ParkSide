<template>
  <div>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Adăugare jucator</div>
      </div>

      <div class="col-auto">
        <button class="button green" type="submit" form="form-add-player">
          Salvează
        </button>
      </div>
    </div>
  </div>

  <Form
    @submit="AddPlayer(this.newPlayer.OrderNumber)"
    :validation-schema="schema"
    v-slot="{ errors }"
    id="form-add-player"
  >
    <div class="new-form">
      <div class="row mt-4">
        <div class="col-4">
          <div class="mb-3">
            <label for="input-name-add-player" class="form-label"
              >Nume jucator</label
            >
            <Field
              type="text"
              class="form-control"
              :class="{ 'border-danger': errors.name }"
              id="input-name-add-player"
              name="name"
              placeholder="Nume jucator"
              v-model="newPlayer.Name"
            />
            <ErrorMessage name="name" class="text-danger error-message" />
          </div>

          <div class="mb-3 position-relative">
            <label for="platform" class="form-label"
              >Rol jucator</label
            >
            <Field
              v-model="newPlayer.Role"
              name="role"
              as="select"
              :class="{ 'border-danger': errors.role }"
              class="form-select form-control"
            >
              <option value="" disabled>Rol</option>
              <option
                v-for="(plat, index) in roles"
                :key="index"
                :value="plat.name"
              >
                {{ plat.name }}
              </option>
            </Field>

            <ErrorMessage name="role" class="text-danger error-message" />
          </div>

          <div class="mb-3 position-relative">
            <label for="input-number-player" class="form-label"
              >Numar jucator</label
            >
            <Field
              type="text"
              class="form-control"
              :class="{ 'border-danger': errors.Number }"
              id="input-number-player"
              name="Number"
              placeholder="Numar jucator"
              v-model="newPlayer.Number"
            />

            <ErrorMessage name="Number" class="text-danger error-message" />
            <div
              v-if="validNumber === false"
              class="text-danger error-message"
            >
              Numărul este deja ocupat!
            </div>
          </div>

          <div class="mb-3">
            <label for="input-height-add-player" class="form-label"
              >Inaltime jucator</label
            >
            <Field
              type="text"
              class="form-control"
              :class="{ 'border-danger': errors.Height }"
              id="input-height-add-player"
              name="Height"
              placeholder="Inaltime"
              v-model="newPlayer.OrderNumber"
            />
            <ErrorMessage name="Height" class="text-danger error-message" />
            
          </div>
        </div>
        <div class="col-4">
          <div class="d-flex gap-3">
            <div>
              <label class="form-label">Selectează imagine</label>
              <label
                for="input-upload-player-image"
                class="button blue"
                style="width: 140px"
              >
                Încarcă imagine
                <font-awesome-icon :icon="['fas', 'upload']" />
                <Field
                  type="file"
                  id="input-upload-player-image"
                  name="upload"
                  style="display: none"
                  accept="image/*"
                  ref="uploadInput"
                  @change="UploadImagePlayer"
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
                  v-if="!newPlayer.ImageBase64"
                  class="d-flex flex-column justify-content-center align-items-center gap-2"
                >
                  <img src="@/images/NoImageSelected.png" class="no-image" />
                  <div>Nicio imagine selectată</div>
                </div>

                <div v-if="newPlayer.ImageBase64" class="image">
                  <img
                    :src="newPlayer.ImageBase64"
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
    </div>
  </Form>
</template>

<script>
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";
export default {
  name: "PlayersAddPlayerComponent",
  components: {
    Form,
    Field,
    ErrorMessage,
  },
  data() {
    return {
      photoValidation: null,
      validNumber: true,
      newPlayer: {
        FirstName: "",
        LastName: "",
        Number: 0,
        Role: "",
        Height: "",
        ImageBase64: null,
      },
      roles: [
      { name: "Centrali" },
        { name: "Pivoti" },
        { name: "Interi" },
        { name: "Extreme" },
        { name: "Portari" },
    ]
    };
  },

  computed: {
    schema() {
      return yup.object({
        name: yup.string().required("Acest câmp este obligatoriu"),
        role: yup.string().required("Acest câmp este obligatoriu"),
        Number: yup.string().required("Acest câmp este obligatoriu"),
        Height: yup.string().required("Acest câmp este obligatoriu"),
      });
    },
  },
  methods: {
    SelectShowDescription() {
      this.newPlayer.VizibilitySpeech =
        !this.newPlayer.VizibilitySpeech;
      console.log(this.newPlayer.VizibilitySpeech);
    },
    AddPlayer(orderNumber) {
      this.$axios
        .get(`/api/Player/getPlayers`)
        .then((response) => {
          if (response.data.Items.find((x) => x.OrderNumber == orderNumber)) {
            console.log("order number not ok: " + false);
            this.validNumber = false;
          } else {
            //console.log("order number ok: " + true);
            this.$axios
              .post(`/api/Player/createPlayer`, this.newPlayer)
              .then((response) => {
                console.log(response);
                this.$router.push({ name: "players" });
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
    UploadImagePlayer(event) {
      const selectedFile = event.target;
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        if (reader.result.includes("image")) {
          if (file.size / 1024 < 15360) {
            this.photoValidation = null;
            console.log(reader.result);
            this.newPlayer.ImageBase64 = reader.result;
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
      this.newPlayer.ImageBase64 = null;
    },
  },
};
</script>

<style scoped>
.image-container {
  width: 190px;
}
</style>
