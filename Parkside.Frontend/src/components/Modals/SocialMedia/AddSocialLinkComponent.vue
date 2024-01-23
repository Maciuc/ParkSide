<template>
  <!-- Modal -->
  <div
    class="modal fade custom-modal"
    id="social-link-add-modal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    :aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title mt-2" >
            Adaugă link Social Media
          </h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <Form
          @submit="AddSocialLink"
          :validation-schema="schema"
          v-slot="{ errors }"
          ref="addLinkFormRef"
        >
          <div class="modal-body new-form">
    
            <div class="mb-3 position-relative">
              <label for="platform" class="form-label"
                >Alege platformă din lista</label
              >
              <Field
                v-model="newLink.Platform"
                name="platform"
                as="select"
                :class="{ 'border-danger': errors.Platform }"
                class="form-select form-control"
              >
                <option value="" disabled>Toate platformele</option>
                <option
                  v-for="(plat, index) in platforms"
                  :key="index"
                  :value="plat.name"
                >
                  {{ plat.name }}
                </option>
              </Field>

              <ErrorMessage name="platform" class="text-danger error-message" />
            </div>

            <div class="mb-3">
              <label for="input-title-add-link" class="form-label"
                >Nume pagina</label
              >
              <Field
                type="text"
                class="form-control"
                :class="{ 'border-danger': errors.name }"
                id="input-title-add-link"
                name="name"
                placeholder="Nume pagina"
                v-model="newLink.Name"
              />
              <ErrorMessage name="name" class="text-danger error-message" />
            </div>

            <div class="mb-3 position-relative">
              <label for="link" class="form-label">Adaugă link</label>
              <Field
                type="text"
                class="form-control"
                :class="{ 'border-danger': errors.link }"
                id="input-title-add-link"
                name="link"
                placeholder="Link"
                v-model="newLink.Link"
              />

              <ErrorMessage name="link" class="text-danger error-message" />
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
  name: "SocialMediaAddLinkComponent",
  components: {
    Form,
    Field,
    ErrorMessage,
  },
  emits: ["get-list"],
  data() {
    return {
      newLink: {
        Platform: "",
        Name: "",
        Link: "",
      },
      platforms: [
        { name: "Facebook" },
        { name: "WhatsApp" },
        { name: "Instagram" },
        { name: "TikTok" },
        { name: "Twitter" },
      ],
    };
  },
  methods: {
    AddSocialLink() {
      this.$axios
        .post(`/api/SocialMedia/createSocialMedia`, this.newLink)
        .then((response) => {
          console.log(response);
          this.$emit("get-list");
          $("#social-link-add-modal").modal("hide");
          this.$swal.fire({
            title: "Succes",
            text: "Link-ul a fost adăugat",
            icon: "success",
            showConfirmButton: false,
            timer: 1500,
          });
        })
        .catch((error) => {
          console.error(error);
          this.$swal.fire({
            title: "Eroare",
            text: "Link-ul nu corespunde cu platforma",
            icon: "error",
            showConfirmButton: false,
            timer: 1500,
          });
        });
    },
    ClearModal() {
      this.$refs.addLinkFormRef.resetForm();
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
