<template>
  <!-- Modal -->
  <div
    class="modal fade custom-modal"
    id="social-link-edit-modal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title mt-2">
            Editare link Social Media
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
            <div class="mb-3 position-relative">
              <label for="platform" class="form-label">Alege platforma</label>
              <Field
              v-model="socialLink.Platform"
                name="platform"
                as="select"
                :class="{ 'border-danger': errors.Platform }"
                class="form-select form-control"
              >
                <option
                  v-for="(plat,index) in platforms"
                  :key="index"
                  :value="plat.name"
                >
                  {{ plat.name }}
                </option>
              </Field>
            
              

              <ErrorMessage name="platform" class="text-danger error-message" />
            </div>

            <div class="mb-3">
              <label for="input-title-edit-link" class="form-label"
                >Nume link</label
              >
              <Field
                type="text"
                class="form-control"
                :class="{ 'border-danger': errors.name }"
                id="input-title-edit-link"
                name="name"
                v-model="socialLink.Name"
              />

              <ErrorMessage name="name" class="text-danger error-message" />
            </div>

            <div class="mb-3">
              <label for="textarea-description-edit-link" class="form-label"
                >Adaugă link</label
              >
              <Field
                type="text"
                class="form-control textarea"
                id="textarea-description-edit-link"
                rows="4"
                name="link"
                placeholder="Descriere"
                v-model="socialLink.Link"
                :class="{ 'border-danger': errors.link }"
                as="textarea"
              />
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
      platform: this.socialLink.Platform,
      platforms: [
        { name: "Facebook" },
        { name: "WhatsApp" },
        { name: "Instagram" },
        { name: "TikTok" },
        { name: "Twitter" },
      ],
      value: [],
    };
  },
  props: {
    socialLink: {
      type: Object,
      default() {
        return { Platform: "", Name: "", Link: "", Id: "" };
      },
    },
  },

  methods: {
    SaveEditedLink() {
      this.$axios
        .put(`/api/SocialMedia/updateSocialMedia/${this.socialLink.Id}`, this.socialLink)
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
        platform: yup.string().required("Acest câmp este obligatoriu"),
        name: yup.string().required("Acest câmp este obligatoriu"),
        link: yup.string().required("Acest câmp este obligatoriu"),
      });
    },
  },
};
</script>
