<template>
  <div>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Editare stuff</div>
      </div>

      <div class="col-auto">
        <button class="button green" type="submit" form="form-edit-stuff">
          Salvează
        </button>
      </div>
    </div>
  </div>

  <Form @submit="EditStuff(this.editedStuff.Number)" :validation-schema="schema" v-slot="{ errors }" id="form-edit-stuff">
    <div class="new-form">
      <div class="row mt-4">
        <div class="col-md-7 col-lg-5 col-12">
          <div class="mb-3">
            <label for="input-lastname-edit-stuff" class="form-label">Nume</label>
            <Field type="text" class="form-control" :class="{ 'border-danger': errors.lastname }"
              id="input-lastname-edit-stuff" name="lastname" placeholder="Nume " v-model="editedStuff.LastName" />
            <ErrorMessage name="lastname" class="text-danger error-message" />
          </div>

          <div class="mb-3">
            <label for="input-firstname-edit-stuff" class="form-label">Prenume</label>
            <Field type="text" class="form-control" :class="{ 'border-danger': errors.firstname }"
              id="input-firstname-edit-stuff" name="firstname" placeholder="Nume stuff" v-model="editedStuff.FirstName" />
            <ErrorMessage name="firstname" class="text-danger error-message" />
          </div>

          <!-- <div class="mb-3">
            <label for="input-teamname-edit-stuff" class="form-label">Nume echipa</label>
            <Field type="text" class="form-control" :class="{ 'border-danger': errors.teamname }"
              id="input-teamname-edit-stuff" name="teamname" placeholder="Nume echipa" v-model="editedStuff.TeamName" />
            <ErrorMessage name="teamname" class="text-danger error-message" />
          </div> -->

          <div class="mb-3 position-relative">
            <div :class="{ 'invalid-input': errors.birthdate }" class="col custom-date-picker">
              <label class="form-label">Dată naștere</label>
              <Field v-slot="{ field }" name="birthdate" id="birthdate-edit-stuff">
                <VueDatePicker v-bind="field" v-model="editedStuff.BirthDate" format="dd/MM/yyyy" auto-apply utc
                  :enable-time-picker="false" placeholder="Dată naștere"></VueDatePicker>
              </Field>
              <ErrorMessage name="birthdate" class="text-danger error-message" />
            </div>
          </div>

          <div class="mb-3 position-relative">
                <label for="input-nationality-edit-stuff" class="form-label">Nationalitate</label>
                <Field type="text" class="form-control" id="nationality" name="nationality" placeholder="Nationalitate"
                  v-model="editedStuff.Nationality" />
              </div>

              <div class="mb-3 position-relative">
                <label for="input-height-edit-stuff" class="form-label">Inaltime</label>
                <Field type="text" class="form-control" id="input-height-edit-stuff" name="height"
                  placeholder="Inaltime stuff" v-model="editedStuff.Height" />
              </div>
        </div>


        <div class="col-md-7 col-lg-6 col-xl-4 col-12 row gap-1">
          <div class="row mb-4">
            <div class="col">
              <label class="form-label">Selectează imagine</label>
              <label for="input-upload-stuff-image" class="button blue" style="width: 140px">
                Încarcă imagine
                <font-awesome-icon :icon="['fas', 'upload']" />
                <Field type="file" id="input-upload-stuff-image" name="upload" style="display: none" accept="image/*"
                  ref="uploadInput" @change="UploadImageStuff">
                </Field>
              </label>
            </div>

            <div class="col">
              <div class="image-container d-flex align-items-center justify-content-center">
                <button type="button" class="button-delete" @click="DeletePhoto">
                  <font-awesome-icon :icon="['fas', 'trash']" />
                </button>
                <div v-if="!editedStuff.ImageBase64"
                  class="d-flex flex-column justify-content-center align-items-center gap-2">
                  <img src="@/images/NoImageSelected.png" class="no-image" />
                  <div class="text-center">Nicio imagine selectată</div>
                </div>

                <div v-if="editedStuff.ImageBase64" class="image">
                  <img :src="editedStuff.ImageBase64" alt="Imagine selectată" class="image" />
                </div>
              </div>

              <div v-if="photoValidation === false" ref="validation-img-type" class="text-danger error-message">
                Tipul imaginii selectate nu este valid
              </div>
              <div v-else-if="photoValidation === true" ref="validation-img-type" class="text-danger error-message">
                Imaginea selectată este prea mare
              </div>
              <div v-else></div>
            </div>
          </div>


        </div>
      </div>
      <div class="row">
        <div class="col-md-7 col-lg-7 col-12">
          <div class="mb-3 position-relative">
            <label for="textarea-description-edit-project" class="form-label">Descriere</label>
            <Field v-slot="{ field }" v-model="editedStuff.Description" name="description">
              <textarea v-bind="field" name="description" class="form-control textarea" rows="5"
                placeholder="Descriere" ></textarea>
            </Field>
          </div>
        </div>
      </div>
    </div>
  </Form>
</template>
  
<script>
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";
export default {
  name: "StuffesEditStuffComponent",
  components: {
    Form,
    Field,
    ErrorMessage,
    VueDatePicker,
  },
  data() {
    return {
      photoValidation: null,
      validNumber: true,
      editedStuff: {},
    };
  },

  computed: {
    schema() {
      return yup.object({
        firstname: yup.string().required("Acest câmp este obligatoriu"),
        lastname: yup.string().required("Acest câmp este obligatoriu"),
      });
    },
  },
  methods: {
    GetStuffForEdit(id) {
      console.log(id);
      this.$axios
        .get(`/api/Stuff/getStuff/${id}`)
        .then((response) => {
          this.editedStuff = response.data;
          console.log(this.editedStuff);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    EditStuff() {
      this.$axios
        .put(`/api/Stuff/updateStuff/${this.editedStuff.Id}`, this.editedStuff)
        .then((response) => {
          console.log(response);
          this.$router.push({ name: "stuff" });
          this.$swal.fire({
            title: "Succes",
            text: "Antrenorul a fost editat",
            icon: "success",
            showConfirmButton: false,
            timer: 1500,
          });
        })
        .catch((error) => {
          console.error(error);
        });
    },


    UploadImageStuff(event) {
      const selectedFile = event.target;
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        if (reader.result.includes("image")) {
          if (file.size / 1024 < 15360) {
            this.photoValidation = null;
            console.log(reader.result);
            this.editedStuff.ImageBase64 = reader.result;
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
      this.editedStuff.ImageBase64 = null;
      this.photoValidation = null;
    },
  },
  created() {
    this.GetStuffForEdit(this.$route.params.id);
  }
};
</script>
  
<style scoped></style>
  