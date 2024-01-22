<template>
  <div>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Adăugare antrenor</div>
      </div>

      <div class="col-auto">
        <button class="button green" type="submit" form="form-edit-coach">
          Salvează
        </button>
      </div>
    </div>
  </div>

  <Form @submit="EditCoach(this.editedCoach.Number)" :validation-schema="schema" v-slot="{ errors }" id="form-edit-coach">
    <div class="new-form">
      <div class="row mt-4">
        <div class="col-md-7 col-lg-5 col-12">
          <div class="mb-3">
            <label for="input-lastname-edit-coach" class="form-label">Nume antrenor</label>
            <Field type="text" class="form-control" :class="{ 'border-danger': errors.lastname }"
              id="input-lastname-edit-coach" name="lastname" placeholder="Nume " v-model="editedCoach.LastName" />
            <ErrorMessage name="lastname" class="text-danger error-message" />
          </div>

          <div class="mb-3">
            <label for="input-firstname-edit-coach" class="form-label">Prenume antrenor</label>
            <Field type="text" class="form-control" :class="{ 'border-danger': errors.firstname }"
              id="input-firstname-edit-coach" name="firstname" placeholder="Nume antrenor" v-model="editedCoach.FirstName" />
            <ErrorMessage name="firstname" class="text-danger error-message" />
          </div>

          <div class="mb-3">
            <label for="input-teamname-edit-coach" class="form-label">Nume echipa</label>
            <Field type="text" class="form-control" :class="{ 'border-danger': errors.teamname }"
              id="input-teamname-edit-coach" name="teamname" placeholder="Nume echipa" v-model="editedCoach.TeamName" />
            <ErrorMessage name="teamname" class="text-danger error-message" />
          </div>

          <div class="mb-3 position-relative">
            <div :class="{ 'invalid-input': errors.birthdate }" class="col custom-date-picker">
              <label class="form-label">Dată naștere</label>
              <Field v-slot="{ field }" name="birthdate" id="birthdate-edit-coach">
                <VueDatePicker v-bind="field" v-model="editedCoach.BirthDate" format="dd/MM/yyyy" auto-apply utc
                  :enable-time-picker="false" placeholder="Dată naștere"></VueDatePicker>
              </Field>
              <ErrorMessage name="birthdate" class="text-danger error-message" />
            </div>
          </div>
        </div>


        <div class="col-md-7 col-lg-6 col-xl-4 col-12 row gap-1">
          <div class="row mb-4">
            <div class="col">
              <label class="form-label">Selectează imagine</label>
              <label for="input-upload-coach-image" class="button blue" style="width: 140px">
                Încarcă imagine
                <font-awesome-icon :icon="['fas', 'upload']" />
                <Field type="file" id="input-upload-coach-image" name="upload" style="display: none" accept="image/*"
                  ref="uploadInput" @change="UploadImageCoach">
                </Field>
              </label>
            </div>

            <div class="col">
              <div class="image-container d-flex align-items-center justify-content-center">
                <button type="button" class="button-delete" @click="DeletePhoto">
                  <font-awesome-icon :icon="['fas', 'trash']" />
                </button>
                <div v-if="!editedCoach.ImageBase64"
                  class="d-flex flex-column justify-content-center align-items-center gap-2">
                  <img src="@/images/NoImageSelected.png" class="no-image" />
                  <div class="text-center">Nicio imagine selectată</div>
                </div>

                <div v-if="editedCoach.ImageBase64" class="image">
                  <img :src="editedCoach.ImageBase64" alt="Imagine selectată" class="image" />
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

          <div class="row">
            <div>
              <label for="input-nationality-edit-coach" class="form-label">Nationalitate</label>
              <Field type="text" class="form-control" id="nationality" name="nationality" placeholder="Nationalitate"
                v-model="editedCoach.Nationality" />
            </div>
          </div>

          <div class="row">
            <div class="mb">
              <label for="input-height-edit-coach" class="form-label">Inaltime</label>
              <Field type="text" class="form-control" id="input-height-edit-coach" name="height"
                placeholder="Inaltime antrenor" v-model="editedCoach.Height" />
            </div>
          </div>


        </div>
      </div>
      <div class="row">
        <div class="col-md-7 col-lg-7 col-12">
          <div class="mb-3 position-relative">
            <label for="textarea-description-edit-project" class="form-label">Descriere</label>
            <Field v-slot="{ field }" v-model="editedCoach.Description" name="description">
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
  name: "CoachesEditCoachComponent",
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
      editedCoach: {},
    };
  },

  computed: {
    schema() {
      return yup.object({
        firstname: yup.string().required("Acest câmp este obligatoriu"),
        lastname: yup.string().required("Acest câmp este obligatoriu"),
        teamname: yup.string().required("Acest câmp este obligatoriu"),
      });
    },
  },
  methods: {
    GetCoachForEdit(id) {
      console.log(id);
      this.$axios
        .get(`/api/Coach/getCoach/${id}`)
        .then((response) => {
          this.editedCoach = response.data;
          console.log(this.editedCoach);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    EditCoach() {
      this.$axios
        .put(`/api/Coach/updateCoach/${this.editedCoach.Id}`, this.editedCoach)
        .then((response) => {
          console.log(response);
          this.$router.push({ name: "coaches" });
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


    UploadImageCoach(event) {
      const selectedFile = event.target;
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        if (reader.result.includes("image")) {
          if (file.size / 1024 < 15360) {
            this.photoValidation = null;
            console.log(reader.result);
            this.editedCoach.ImageBase64 = reader.result;
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
      this.editedCoach.ImageBase64 = null;
      this.photoValidation = null;
    },
  },
  created() {
    this.GetCoachForEdit(this.$route.params.id);
  }
};
</script>
  
<style scoped></style>
  