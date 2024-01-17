<template>
    <!-- Modal -->
    <div
      class="modal fade custom-modal"
      id="championship-add-modal"
      tabindex="-1"
      aria-labelledby="exampleModalLabel"
      :aria-hidden="true"
    >
      <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable ">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title mt-2" >
              Adaugă campionat
            </h5>
            <button
              championship="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>
          <Form @submit="AddChampionship()" :validation-schema="schema" v-slot={errors} ref="addChampionshipFormRef" class="new-form">
            <div class="modal-body new-form">
              <div class="mb-3">
                <label for="input-title-championship" class="form-label">Denumire</label>
                <Field
                  championship="text"
                  class="form-control"
                  :class="{'border-danger': errors.name}"
                  id="input-title-championship"
                  name="name"
                  placeholder="Denumire"
                  v-model="newChampionship.Name"
                />
                <ErrorMessage name="name" class="text-danger error-message "  />
              </div>
            </div>
          
          <div class="modal-footer justify-content-between">
            <button championship="button" class="button grey" data-bs-dismiss="modal">
              Anulare
            </button>
            <button championship="submit" class="button green">
              Salvare
            </button>
          </div>
        </Form>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  //import { formToJSON } from "axios";
  import { Form, Field, ErrorMessage } from "vee-validate";
  import * as yup from "yup";
  
  export default {
    name: "AddChampionshipModalComponent",
    components: {
      Form,
      Field,
      ErrorMessage,
    },
    emits: ["get-list"],
    data() {
      return {
        newChampionship: {
          Name: "",
        },
      };
    },
    methods: {
      AddChampionship() {
        this.$axios
          .post(`/api/Championship/createChampionship`, this.newChampionship)
          .then((response) => {
            console.log(response);
            this.$emit("get-list");
            $("#championship-add-modal").modal("hide");
            this.$swal.fire({
              title: "Succes",
              text: "Echipa a fost adăugata",
              icon: "success",
              showConfirmButton: false,
              timer: 1500,
            });
  
          })
          .catch((error) => {
            console.error(error);
          });
      },
      ClearModal(){
        this.$refs.addChampionshipFormRef.resetForm();
      }
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
  
  
  