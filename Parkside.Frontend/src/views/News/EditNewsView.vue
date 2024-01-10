<template>
    <div>
      <div class="row header-section align-items-center">
        <div class="col">
          <div class="title-page">Editează știre</div>
        </div>
  
        <div class="col d-flex gap-3 justify-content-end">
          <div class="col-auto">
            <button class="button grey" type="submit" form="form-edit-news" @click="SaveSketch">
              Salvează schița
            </button>
          </div>
          <div class="col-auto">
            <button class="button blue" type="submit">
              Previzualizează
            </button>
          </div>
          <div class="col-auto">
            <button class="button green" type="submit" form="form-edit-news">
              Publică
            </button>
          </div>
        </div>
      </div>
    </div>
  
    <Form
      @submit="EditArticle()"
      :validation-schema="schema"
      v-slot="{ errors }"
      id="form-edit-news"
    >
      <div class="new-form mt-4">
        <div class="row">
          <div class="col">
            <label for="input-edit-news-title" class="form-label"
              >Titlu știre</label
            >
            <Field
              class="form-control"
              id="input-edit-news-title"
              placeholder="Titlu știre"
              :class="{ 'border-danger': errors.title }"
              name="title"
              v-model="newNews.Name"
            >
            </Field>
            <ErrorMessage name="title" class="text-danger error-message" />
          </div>
  
          <div
            :class="{ 'invalid-input': errors.datepublish }"
            class="col custom-date-picker"
          >
            <label class="form-label">Dată publicare</label>
            <Field
              v-slot="{ field }"
              name="datepublish"
              id="date-publish-edit-news"
            >
              <VueDatePicker
                v-bind="field"
                v-model="newNews.PublishedDate"
                auto-apply
                utc
                :enable-time-picker="false"
                placeholder="Dată publicare"
              ></VueDatePicker>
            </Field>
            <ErrorMessage name="datepublish" class="text-danger error-message" />
          </div>
  
          <div class="col">
            <label for="platform" class="form-label">Tip știre</label>
            <Field
              v-slot="{ field }"
              name="newsType"
              class="form-control"
              v-model="selectedNewsCategory.Name"
            >
              <div class="custom dropdown custom-dropdown" v-bind="field">
                <button
                  class="btn btn-secondary dropdown-toggle"
                  :class="{ 'border-danger': errors.newsType }"
                  type="button"
                  id="dropdownNewsTypesEditNews"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  <span v-if="selectedNewsCategory.Name">
                    {{ selectedNewsCategory.Name }}
                  </span>
                  <span v-else>Tip știre</span>
                </button>
                <ul
                  class="dropdown-menu"
                  aria-labelledby="dropdownNewsTypesEditNews"
                >
                  <li
                    v-for="(news, index) in newsTypesList"
                    :key="index"
                    :value="news.Name"
                  >
                    <a
                      class="dropdown-item"
                      href="#"
                      @click="selectedNewsCategory = news"
                      >{{ news.Name }}</a
                    >
                  </li>
                </ul>
              </div>
            </Field>
  
            <ErrorMessage name="newsType" class="text-danger error-message" />
          </div>
        </div>
  
        <div class="row">
          <div class="col-8">
            <label for="input-description-edit-news" class="form-label"
              >Descriere</label
            >
            <Field
              name="descriptionNews"
              id="input-description-edit-news"
              v-model="newNews.Description"
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
          <div class="col-4 d-flex justify-content-between mt-4">
            <div>
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
            <div>
              <div
                class="image-container d-flex align-items-center justify-content-center"
              >
                <button type="button" class="button-delete" @click="DeletePhoto">
                  <font-awesome-icon :icon="['fas', 'trash']" />
                </button>
                <div
                  v-if="!newNews.ImageBase64"
                  class="d-flex flex-column justify-content-center align-items-center gap-2"
                >
                  <img src="@/images/NoImageSelected.png" class="no-image" />
                  <div>Nicio imagine selectată</div>
                </div>
  
                <div v-if="newNews.ImageBase64" class="image">
                  <img
                    :src="newNews.ImageBase64"
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
  
    <div
      class="mt-5 d-flex align-items-top justify-content-center editor-border-top gap-5"
    >
      <TextEditor v-model="newNews.Content"></TextEditor>
    </div>
  </template>
  
  <script>
  import VueDatePicker from "@vuepic/vue-datepicker";
  import "@vuepic/vue-datepicker/dist/main.css";
  import { Form, Field, ErrorMessage } from "vee-validate";
  import * as yup from "yup";
  import TextEditor from "../../components/TextEditor.vue";
  
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
        selectedNewsCategory: {},
        newNews: {
          Name: "",
          Description: "",
          Content: "Scrie un articol",
          ImageBase64: null,
          PublishedDate: "",
          IsPublished: true,
        },
      };
    },
  
    computed: {
      schema() {
        return yup.object({
          title: yup.string().required("Acest câmp este obligatoriu"),
          datepublish: yup.string().required("Acest câmp este obligatoriu"),
          newsType: yup.string().required("Acest câmp este obligatoriu"),
        });
      },
    },
    methods: {
      GetNewsTypeEditNews() {
        this.$axios
          .get("/api/NewsCategories/getNewsCategoryDropDown")
          .then((response) => {
            this.newsTypesList = response.data;
            console.log(response.data);
          })
          .catch((error) => {
            console.log(error);
          });
      },
  
      OpenDropdown() {
        $("#dropdownNewsTypeEditNews").dropdown("toggle");
      },
  
      OpenDropdownAuthor() {
        $("#dropdownAuthor").dropdown("toggle");
      },
  
      SelectCategory(newsType) {
        this.selectedNewsCategory = newsType;
        $("#dropdownNewsTypesEditNews").dropdown("hide");
        this.GetAllNews();
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
              this.newNews.ImageBase64 = reader.result;
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
      UploadImagesForNews(event) {
        const selectedFile = event.target;
        const file = event.target.files[0];
        const reader = new FileReader();
        reader.onload = () => {
          if (reader.result.includes("image")) {
            if (file.size / 1024 < 15360) {
              this.photoValidation = null;
              console.log(reader.result);
              this.newNews.ImageBase64 = reader.result;
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
        this.newNews.ImageBase64 = null;
        // this.$swal.fire({
        //   title: "The Internet?",
        //   text: "That thing is still around?",
        //   icon: "error",
        // });
      },
  
      EditArticle() {
        
        this.$axios
          .post(
            `/api/News/createNews/${this.selectedNewsCategory.Id}`,
            this.newNews
          )
          .then((response) => {
            console.log(this.newNews);
            console.log(response);
            this.$router.push({ name: "news" });
          })
          .catch((error) => {
            console.error(error);
          });
      },
  
      SaveSketch(){
        this.newNews.IsPublished=false;
        this.EditArticle();
      },
    },
  
    created() {
      this.GetNewsTypeEditNews();

    console.log("1+ "+this.$route)
    console.log("2+ "+this.$route.params.id)
    //this.GetMemberForEdit(this.$route.params.id);
    },
  };
  </script>
  
  <style scoped>
  .image-container {
    width: 190px;
  }
  
  .editor-border-top {
    border-top: 1px solid #0000001a;
    padding-top: 15px;
  }
  
  
  </style>
  