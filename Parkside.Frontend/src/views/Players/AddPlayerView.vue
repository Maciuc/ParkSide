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
    @submit="AddPlayer(this.newPlayer.Number)"
    :validation-schema="schema"
    v-slot="{ errors }"
    id="form-add-player"
  >
    <div class="new-form">
      <div class="row mt-4">
        <div class="col-md-7 col-lg-5 col-12">
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
              v-model="newPlayer.LastName"
            />
            <ErrorMessage name="name" class="text-danger error-message" />
          </div>

          <div class="mb-3">
          <label for="input-teamname-add-player" class="form-label"
              >Nume echipa</label
            >
            <Field
              type="text"
              class="form-control"
              :class="{ 'border-danger': errors.name }"
              id="input-teamname-add-player"
              name="teamname"
              placeholder="Nume echipa"
              v-model="newPlayer.TeamName"
            />
            <ErrorMessage name="name" class="text-danger error-message" />
          </div>

          <div class="mb-3 position-relative">
            <label for="role" class="form-label"
              >Rol</label
            >
            <Field
              v-model="newPlayer.Role"
              name="role"
              as="select"
              :class="{ 'border-danger': errors.role }"
              class="form-select form-control"
            >
              <option value="" disabled>Rol jucator</option>
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
            <div
              :class="{ 'invalid-input': errors.birthdate }"
              class="col custom-date-picker"
            >
              <label class="form-label">Dată naștere</label>
              <Field
                v-slot="{ field }"
                name="birthdate"
                id="birthdate-add-player"
              >
                <VueDatePicker
                  v-bind="field"
                  v-model="newPlayer.BirthDate"
                  format="dd/MM/yyyy"
                  auto-apply
                  utc
                  :enable-time-picker="false"
                  placeholder="Dată naștere"
                ></VueDatePicker>
              </Field>
              <ErrorMessage
                name="birthdate"
                class="text-danger error-message"
              />
            </div>
          </div>

          <div class="mb-3">
            <label for="input-number-add-player" class="form-label"
              >Numar</label
            >
            <Field
              type="text"
              class="form-control"
              :class="{ 'border-danger': errors.number }"
              id="input-number-add-player"
              name="number"
              placeholder="Numar"
              v-model="newPlayer.Number"
            />
            <ErrorMessage name="number" class="text-danger error-message" />
            <div
              v-if="validNumber === false"
              class="text-danger error-message"
            >
              Numărul este deja ocupat!
            </div>
          </div>
        </div>

        
        <div class="col-md-7 col-lg-6 col-xl-4 col-12 row gap-1">
          <div class="row mb-5"> 
          <div class="col">
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

          <div class="col">
            <div
              class="image-container d-flex align-items-center justify-content-center"
            >
              <button type="button" class="button-delete" @click="DeletePhoto">
                <font-awesome-icon :icon="['fas', 'trash']" />
              </button>
              <div
                v-if="!newPlayer.ImageBase64"
                class="d-flex flex-column justify-content-center align-items-center gap-2"
              >
                <img src="@/images/NoImageSelected.png" class="no-image" />
                <div class="text-center">Nicio imagine selectată</div>
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
        <!-- <div class="row mt-2"> 
          <div class="mb-5">
            <label for="input-nationality-add-player" class="form-label"
              >Nationalitate</label
            >
            <Field
              type="text"
              class="form-control"
              id="nationality"
              name="nationality"
              placeholder="Nationalitate"
              v-model="newPlayer.Nationality"
            />
          </div>
        </div> -->

        <div class="row"> 
          <div class="mb">
            <label for="input-height-add-player" class="form-label"
              >Inaltime</label
            >
            <Field
              type="text"
              class="form-control"
              id="input-height-add-player"
              name="height"
              placeholder="Inaltime jucator"
              v-model="newPlayer.Height"
            />
          </div>
        </div>


        </div>
      </div>
      <div class="row">
        <div class="col-md-7 col-lg-7 col-12">
          <div class="mb-3 position-relative">
            <label for="textarea-description-add-project" class="form-label"
              >Descriere</label
            >
            <Field
              v-slot="{ field }"
              v-model="newPlayer.Description"
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
        Role: "",
        BirthDate: "",
        Number: "",
        TeamName: "",
        Description: "",
        Nationality: "",
        ImageBase64: null,
      },
      Roles: [
        { name: "Centrali" },
        { name: "Pivoti" },
        { name: "Interi" },
        { name: "Extreme" },
        { name: "Portari" },
      ],
    };
  },

  computed: {
    schema() {
      return yup.object({
        name: yup.string().required("Acest câmp este obligatoriu"),
        number: yup.string().required("Acest câmp este obligatoriu"),
      });
    },
  },
  methods: {
    AddPlayer(number) {
      this.$axios
        .get(`/api/Player/getPlayers`)
        .then((response) => {
          console.log(this.newPlayer);
          if (response.data.Items.find((x) => x.Number == number)) {
            console.log("Number not ok: " + false);
            this.validNumber = false;
          } else {
            this.$axios
              .post(`/api/Player/createPlayer`, this.newPlayer)
              .then((response) => {
                console.log(response);
                this.$router.push({ name: "players" });
                this.$swal.fire({
                  title: "Succes",
                  text: "Jucatorul a fost adăugat",
                  icon: "success",
                  showConfirmButton: false,
                  timer: 1500,
                });
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

<style scoped></style>
