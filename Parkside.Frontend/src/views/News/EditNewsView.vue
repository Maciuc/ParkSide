<template>
  <div>
    <Form
      @submit="EditArticle"
      :validation-schema="schema"
      v-slot="{ errors }"
      id="form-edit-news"
    >
      <div
        class="row header-section align-items-center justify-content-between"
      >
        <div class="col-auto">
          <div class="title-page">Editează știre</div>
        </div>

        <div class="col-auto row gap-1 mt-2 mt-lg-0">
          <div class="col-md-auto d-flex justify-content-sm-end">
            <button
              @click="editedNews.IsPublished = false"
              class="button grey"
              type="submit"
              form="form-edit-news"
            >
              Salvează schița
            </button>
          </div>
          <div class="col-md-auto d-flex justify-content-sm-end">
            <button
              @click="editedNews.IsPublished = true"
              class="button green"
              type="submit"
              form="form-edit-news"
            >
              Publică
            </button>
          </div>
        </div>
      </div>

      <div class="new-form mt-4 mb-4">
        <div class="row">
          <div class="col-md-4 col-sm-6">
            <label for="input-edit-news-title" class="form-label"
              >Titlu știre</label
            >
            <Field
              class="form-control"
              id="input-edit-news-title"
              placeholder="Titlu știre"
              :class="{ 'border-danger': errors.title }"
              name="title"
              v-model="editedNews.Name"
            >
            </Field>
            <ErrorMessage name="title" class="text-danger error-message" />
          </div>

          <div
            :class="{ 'invalid-input': errors.datepublish }"
            class="col-md-4 col-sm-6 custom-date-picker"
          >
            <label class="form-label">Dată publicare</label>
            <Field
              v-slot="{ field }"
              name="datepublish"
              id="date-publish-edit-news"
              v-model="editedNews.PublishedDate"
            >
              <VueDatePicker
                v-bind="field"
                v-model="editedNews.PublishedDate"
                format="dd/MM/yyyy"
                auto-apply
                utc
                :enable-time-picker="false"
                placeholder="Dată publicare"
              ></VueDatePicker>
            </Field>
            <ErrorMessage
              name="datepublish"
              class="text-danger error-message"
            />
          </div>

          <div class="col-md-4 col-sm-6 d-flex align-items-end justify-content-start">
          <div class="form-check form-switch">
                  <input
                    class="form-check-input"
                    type="checkbox"
                    id="flexSwitchCheckDefault"
                    :checked="editedNews.IsPrimary"
                    @click="TogglePrimaryNews"
                  />
                  <label class="form-label" for="flexSwitchCheckDefault"
                    >Stire primara</label
                  >
              </div>
              </div>
        </div>

        <div class="row">
          <div class="col-md-5 col-xl-8">
            <label for="input-description-edit-news" class="form-label"
              >Descriere</label
            >
            <Field
              name="descriptionNews"
              id="input-description-edit-news"
              v-model="editedNews.Description"
              v-slot="{ field }"
            >
              <textarea
                v-bind="field"
                class="form-control textarea"
                placeholder="Descriere"
                rows="6"
              >
              </textarea>
            </Field>
          </div>
          <div class="col-md-7 col-xl-4 row justify-content-start mt-4 gap-1">
            <div class="col">
              <label class="form-label">Selectează imagine</label>
              <label
                class="button blue cursor-pointer"
                style="width: 140px"
                for="input-upload-image-edit-news"
              >
                <Field
                  name="uploadImage"
                  type="file"
                  id="input-upload-image-edit-news"
                  style="display: none"
                  accept="image/*"
                  ref="uploadImageEditNews"
                  @change="UploadImageEditNews"
                >
                </Field>
                Încarcă imagine
                <font-awesome-icon :icon="['fas', 'upload']" />
              </label>
            </div>
            <div class="col">
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
                  v-if="!editedNews.ImageBase64"
                  class="d-flex flex-column justify-content-center align-items-center gap-2"
                >
                  <img src="@/images/NoImageSelected.png" class="no-image" />
                  <div class="text-center">Nicio imagine selectată</div>
                </div>

                <div v-if="editedNews.ImageBase64" class="image">
                  <img
                    :src="editedNews.ImageBase64"
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
    </Form>

    <TextEditor v-model="editedNews.Content"></TextEditor>
  </div>
</template>

<script>
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";
import TextEditor from "../../components/TextEditor.vue";
import moment from "moment";

export default {
  name: "MembersEditMemberComponent",
  components: {
    Form,
    Field,
    ErrorMessage,
    VueDatePicker,
    TextEditor,
  },
  props: ["event"],
  data() {
    return {
      photoValidation: null,
      newsTypesList: [],
      editedNews: {},
    };
  },

  computed: {
    schema() {
      return yup.object({
        title: yup.string().required("Acest câmp este obligatoriu"),
        datepublish: yup.string().required("Acest câmp este obligatoriu"),
      });
    },
  },
  methods: {
    GetNewsForEdit(id) {
      console.log(id);
      this.$axios
        .get(`/api/News/getNews/${id}`)
        .then((response) => {
          this.editedNews = response.data;
          console.log(this.editedNews.PublishedDate);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    UploadImageEditNews(event) {
      const selectedFile = event.target;
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        if (reader.result.includes("image")) {
          if (file.size / 1024 < 15360) {
            this.photoValidation = null;
            console.log(reader.result);
            this.editedNews.ImageBase64 = reader.result;
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
      this.$refs.uploadImageEditNews.reset();
      this.editedNews.ImageBase64 = null;
    },

    EditArticle() {
      this.$axios
        .put(`/api/News/updateNews/${this.editedNews.Id}`, this.editedNews)
        .then((response) => {
          console.log(this.editedNews);
          console.log(response);
          if (this.editedNews.IsPublished === false) {
            this.$swal.fire({
              title: "Succes",
              text: "Știrea a fost salvată",
              icon: "success",
              showConfirmButton: false,
              timer: 1500,
            });
          } else {
            this.$swal.fire({
              title: "Succes",
              text: "Știrea a fost publicată",
              icon: "success",
              showConfirmButton: false,
              timer: 1500,
            });
          }
        })
        .catch((error) => {
          console.error(error);
        });
    },

    TogglePrimaryNews() {
      this.editedNews.IsPrimary =
        !this.editedNews.IsPrimary;
      console.log(this.editedNews.IsPrimary);
    },
  },

  created() {
    this.GetNewsForEdit(this.$route.params.id);
  },
};
</script>

<style scoped>
.button {
  width: 155px;
}
</style>
