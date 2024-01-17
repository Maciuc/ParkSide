<template>
    <!-- Modal -->
    <div
      class="modal fade custom-modal"
      id="championship-edit-modal"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title mt-2" >
              Editare campionat
            </h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <Form
            @submit="SaveEditedChampionship"
            :validation-schema="schema"
            v-slot="{ errors }"
          >
            <div class="modal-body new-form">
              <div class="mb-3">
                <label for="input-title-edit-news" class="form-label"
                  >Denumire</label
                >
                <Field
                  type="text"
                  class="form-control"
                  :class="{'border-danger': errors.name}"
                  id="input-title-edit-news"
                  name="name"
                  v-model="event.Name"
                />
                <ErrorMessage name="name" class="text-danger error-message "  />
              </div>
            </div>
            <div class="modal-footer justify-content-between">
              <button type="button" class="button grey" data-bs-dismiss="modal">
                Anulare
              </button>
              <button
                type="submit"
                class="button green"
              >
                Salvare
              </button>
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
    name: "EditChampionshipComponent",
    components: {
      Form,
      Field,
      ErrorMessage,
    },
    props: {
      event: {
        type: Object,
        default() {
          return { Name: "", Id: "" };
        },
      },
    },
  
    methods: {
      SaveEditedChampionship() {
        this.$axios
          .put(`/api/Championship/updateChampionship/${this.event.Id}`, this.event)
          .then((response) => {
            console.log(response);
            this.$emit("get-list");
            $("#championship-edit-modal").modal("hide");
            this.$swal.fire({
              title: "Succes",
              text: "Campionatul a fost editat",
              icon: "success",
              showConfirmButton: false,
              timer: 1500,
            });
          })
          .catch((error) => {
            console.error(error);
          });
      },
    },
    computed: {
      schema() {
        return yup.object({
          name: yup.string().required("Denumirea nu este validă"),
        });
      },
    },
  };
  </script>
  