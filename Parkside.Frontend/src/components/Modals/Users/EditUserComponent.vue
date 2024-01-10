<template>
  <!-- Modal -->
  <div
    class="modal fade custom-modal"
    id="user-edit-modal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title mt-2">
            Editare utilizator
          </h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>

        <Form
          @submit="SaveEditedLink"
          :validation-schema="schema"
          v-slot="{ errors }"
          ref="editLinkFormRef"
        >
          <div class="modal-body new-form">
            <div class="mb-3">
              <label for="input-title-edit-user" class="form-label"
                >Nume utilizator</label
              >
              <Field
                type="text"
                class="form-control"
                :class="{ 'border-danger': errors.name }"
                id="input-title-edit-user"
                name="name"
                v-model="user.Name"
              />

              <ErrorMessage name="name" class="text-danger error-message" />
            </div>

            <div class="mb-3">
              <label class="form-label">Rolul</label>
              <Field
                name="role"
                as="select"
                v-model="user.Role"
                :class="{ 'border-danger': errors.role }"
                class="form-select form-control"
              >
                <option value="" disabled>Toate rolurile</option>
                <option
                  v-for="(role, index) in roles"
                  :key="index"
                  :value="role.name"
                >
                  {{ role.name }}
                </option>
              </Field>
              <ErrorMessage name="role" class="text-danger error-message" />
            </div>
            <div class="row">
              <div class="col-6 d-flex flex-column justify-content-center">
                <label class="form-label">Selectează imagine</label>
                <label
                  for="input-upload-edit-user"
                  class="button blue"
                  style="width: 140px"
                >
                  Încarcă imagine
                  <font-awesome-icon :icon="['fas', 'upload']" />
                  <Field
                    type="file"
                    :class="{ 'border-danger': errors.upload }"
                    id="input-upload-edit-user"
                    name="upload"
                    style="display: none"
                    accept="image/*"
                    ref="uploadInput"
                    @change="UploadImage"
                  >
                  </Field>
                </label>

                <ErrorMessage name="upload" class="text-danger error-message" />
              </div>

              <div class="col-6">
                <div
                  class="image-container d-flex align-items-center justify-content-center"
                >
                  <div v-if="!user.Image">
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

                  <div v-if="user.Image" class="image">
                    <button
                      type="button"
                      class="button-delete"
                      @click="DeletePhoto"
                    >
                      <font-awesome-icon :icon="['fas', 'trash']" />
                    </button>
                    <img
                      :src="user.Image"
                      alt="Imagine selectată"
                      class="image"
                    />
                  </div>
                </div>

                <div
                  v-if="PhotoValidation === false"
                  ref="validation-img-type"
                  class="text-danger error-message"
                >
                  Tipul imaginii selectate nu este valid
                </div>
                <div
                  v-else-if="PhotoValidation === true"
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
  name: "SocialMediaEditLinkComponent",
  components: {
    Form,
    Field,
    ErrorMessage,
  },
  data() {
    return {
      platforms: [
        { name: "Facebook" },
        { name: "Twitter" },
        { name: "WhatsApp" },
        { name: "Linkedin" },
        { name: "Instagram" },
      ],
      roles: [],
      PhotoValidation: null,
    };
  },
  props: {
    user: {
      type: Object,
      default() {
        return { Id: "", Name: "", Role: "", Image: "" };
      },
    },
  },

  methods: {
    GetAllRoles() {
      this.$axios
        .get(`https://jsonplaceholder.typicode.com/posts/`)
        .then((response) => {
          console.log(response);
          this.roles = [
            { name: "Administrator" },
            { name: "Editor general" },
            { name: "Editor știri din get" },
          ];
        })
        .catch((error) => {
          console.error(error);
        });
    },
    UploadImage(event) {
      const selectedFile = event.target;
      const file = event.target.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        if (reader.result.includes("image")) {
          if (file.size / 1024 < 15360) {
            this.PhotoValidation = null;
            console.log(reader.result);
            this.user.Image = reader.result;
            selectedFile.value = "";
          } else {
            this.PhotoValidation = true;
          }
        } else {
          this.PhotoValidation = false;
        }
      };
      if (file) {
        reader.readAsDataURL(file);
      }
    },
    DeletePhoto(selectedFile) {
      this.$refs.uploadInput.reset();
      this.user.Image = null;
    },
    SaveEditedLink() {
      this.$axios
        .put("https://httpbin.org/put", this.user)
        .then((response) => {
          console.log(response);
          this.$emit("get-list");
          $("#social-link-edit-modal").modal("hide");
        })
        .catch((error) => {
          console.error(error);
        });
    },
  },
  computed: {
    schema() {
      return yup.object({
        name: yup.string().required("Acest câmp este obligatoriu"),
        role: yup.string().required("Selectați un rol"),
      });
    },
  },
  created() {
    this.GetAllRoles();
  },
};
</script>
