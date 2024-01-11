<template>
  <div>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Editare jucator</div>
      </div>

      <div class="col-auto">
        <button class="button green" type="submit" form="form-edit-player">
          Salvează
        </button>
      </div>
    </div>
  </div>

  <Form
    @submit="SaveEditedPlayer(this.editedPlayer.OrderNumber, this.editedPlayer.Id)"
    :validation-schema="schema"
    v-slot="{ errors }"
    class="new-form"
    id="form-edit-player"
  >
    <div class="row mt-4">
      <div class="col-4">
        <div class="mb-3">
          <label class="form-label" for="input-name-edit-player"
            >Nume jucator</label
          >
          <Field
            type="text"
            id="input-name-edit-player"
            name="name"
            :class="{ 'border-danger': errors.name }"
            class="form-control"
            placeholder="Nume jucator"
            v-model="editedPlayer.Name"
          />

          <ErrorMessage name="name" class="text-danger error-message" />
        </div>
        <div class="mb-3">
          <label class="form-label" for="input-function-edit-player"
            >Funcția în organizație</label
          >
          <Field
            name="function"
            as="select"
            id="input-function-edit-player"
            :class="{ 'border-danger': errors.function }"
            class="form-select form-control"
            v-model="editedPlayer.OrganizationFunction"
          >
            <option value="" disabled>Funcția în organizație</option>
            <option
              v-for="(fnct, index) in functions"
              :key="index"
              :value="fnct.name"
            >
              {{ fnct.name }}
            </option>
          </Field>
          <ErrorMessage name="function" class="text-danger error-message" />
        </div>
        <div class="mb-3">
          <label class="form-label" for="input-birth-date-edit-player"
            >Data nașterii</label
          >
          <Field
            type="text"
            name="birthdate"
            id="input-birth-date-edit-player"
            class="form-control"
            placeholder="Data nașterii"
            v-model="editedPlayer.BirthDate"
            :class="{ 'border-danger': errors.birthdate }"
          />

          <ErrorMessage name="birthdate" class="text-danger error-message" />
        </div>

        <div class="mb-3">
          <label for="input-order-edit-player" class="form-label"
            >Numar ordine</label
          >
          <Field
            type="text"
            class="form-control"
            :class="{ 'border-danger': errors.order }"
            id="input-order-edit-player"
            name="order"
            placeholder="Numar ordine"
            v-model="editedPlayer.OrderNumber"
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
            <label class="form-label"> Selectează imagine</label>
            <label
              class="button blue"
              style="width: 140px"
              for="input-upload-edit-player-image"
            >
              Încarcă imagine
              <font-awesome-icon :icon="['fas', 'upload']" />
              <Field
                type="file"
                id="input-upload-edit-player-image"
                name="upload"
                style="display: none"
                accept="image/*"
                ref="uploadInput"
                @change="UploadImageEditPlayer"
              >
              </Field>
            </label>
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

          <div>
            <div class="image-container d-flex justify-content-center">
              <button class="button-delete" type="button" @click="DeletePhoto">
                <font-awesome-icon :icon="['fas', 'trash']" />
              </button>
              <div
                v-if="!editedPlayer.ImageBase64"
                class="d-flex flex-column justify-content-center align-items-center gap-2"
              >
                <img src="@/images/NoImageSelected.png" class="no-image" />
                <div>Nicio imagine selectată</div>
              </div>
              <div v-if="editedPlayer.ImageBase64">
                <img
                  :src="editedPlayer.ImageBase64"
                  class="image"
                  alt="Imagine selectată"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="row custom-modal">
      <div class="col-8">
        <div class="form-check form-switch">
          <input
            class="form-check-input"
            type="checkbox"
            id="flexSwitchCheckDefault"
            :checked="editedPlayer.VizibilitySpeech"
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
            v-model="editedPlayer.Speech"
            name="description"
          >
            <textarea
              v-bind="field"
              name="description"
              :class="{ 'border-danger': errors.description }"
              class="form-control textarea"
              rows="5"
              placeholder="Descriere"
            />
          </Field>

          <ErrorMessage name="description" class="text-danger error-message" />
        </div>
      </div>
    </div>
  </Form>
</template>
<script>
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";
export default {
  name: "PlayersEditPlayerView",
  components: {
    Form,
    Field,
    ErrorMessage,
  },
  props: ["event"],
  data() {
    return {
      validOrderNumber: true,
      photoValidation: null,
      editedPlayer: {
        Id: "",
        Name: "",
        OrganizationFunction: "",
        BirthDate: "",
        Speech: "",
        OrderNumber: "",
        ImageBase64: null,
        VizibilitySpeech: false,
      },
      functions: [
        { name: "Director general" },
        { name: "Manager general" },
        { name: "Manager departament tehnic" },
        { name: "Administrator" },
        { name: "Contabil" },
      ],
    };
  },

  methods: {
    GetPlayerForEdit(id) {
      console.log(id);
      this.$axios
        .get(`/api/Player/getPlayer/${id}`)
        .then((response) => {
          this.editedPlayer = response.data;
          console.log(this);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    UploadImageEditPlayer(event) {
      const selectedFile = event.target;
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        if (reader.result.includes("image")) {
          if (file.size / 1024 < 15360) {
            this.photoValidation = null;
            console.log(reader.result);
            this.editedPlayer.ImageBase64 = reader.result;
            selectedFile.value = "";
          } else {
            this.photoValidation = true;
            this.DeletePhoto();
          }
        } else {
          this.photoValidation = false;
          this.DeletePhoto();
        }
      };
      if (file) {
        reader.readAsDataURL(file);
      }
    },
    DeletePhoto() {
      this.$refs.uploadInput.reset();
      this.editedPlayer.ImageBase64 = null;
    },
    SaveEditedPlayer(orderNumber, id) {
      this.$axios
        .get(`/api/Player/getPlayers`)
        .then((response) => {
          if ((response.data.Items.filter((x) => x.OrderNumber == orderNumber && x.Id != id).length)>0) {
            console.log("order number not ok: " + false);
            this.validOrderNumber = false;
          } else {
            console.log("order number ok: " + true);
            this.$axios
              .put(
                `/api/Player/updatePlayer/${this.editedPlayer.Id}`,
                this.editedPlayer
              )
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
    SelectShowDescription() {
      this.editedPlayer.VizibilitySpeech =
        !this.editedPlayer.VizibilitySpeech;
      console.log(this.editedPlayer.VizibilitySpeech);
    },
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

  created() {
    // console.log("1+ "+this.$route)
    // console.log("2+ "+this.$route.params.id)
    this.GetPlayerForEdit(this.$route.params.id);
  },
};
</script>

<style>
.image-container {
  width: 190px;
}
</style>
