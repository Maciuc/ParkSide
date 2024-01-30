<template>
  <!-- Modal -->
  <div
    class="modal fade custom-modal"
    id="stuffHistory-edit-modal"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    :aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title mt-2">Actualizeaza participare</h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>

        <Form
          @submit="EditStaffHistory"
          :validation-schema="schema"
          v-slot="{ errors }"
          ref="editStaffHistoryFormRef"
        >
          <div class="modal-body new-form">
            <div class="mb-2 position-relative">
              <label for="staff" class="form-label">Alege staff</label>
              <Field
                v-slot="{ field }"
                name="staff"
                class="form-control"
                v-model="editedStaffHistory.Stuff"
              >
                <div class="custom dropdown custom-dropdown" v-bind="field">
                  <button
                    class="btn btn-secondary dropdown-toggle"
                    :class="{ 'border-danger': errors.staff }"
                    type="button"
                    id="dropdownPlayers"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                  >
                    <span>
                      {{ editedStaffHistory.Stuff?.Name }}
                    </span>
                  </button>
                  <ul class="dropdown-menu" aria-labelledby="dropdownPlayers">
                    <li v-for="(staff, index) in stuffsList" :key="index">
                      <a
                        class="dropdown-item"
                        @click="editedStaffHistory.Stuff = staff"
                        >{{ staff.Name }}</a
                      >
                    </li>
                  </ul>
                </div>
              </Field>

              <ErrorMessage name="staff" class="text-danger error-message" />
            </div>

            <div class="mb-3 position-relative">
              <label for="year" class="form-label">Alege anul</label>
              <Field
                v-model="editedStaffHistory.Year"
                name="year"
                as="select"
                :class="{ 'border-danger': errors.year }"
                class="form-select form-control"
              >
                <option value="" disabled>An</option>
                <option
                  v-for="(year, index) in Years"
                  :key="index"
                  :value="year.year"
                >
                  {{ year.year }}
                </option>
              </Field>

              <ErrorMessage name="year" class="text-danger error-message" />
            </div>

            <div class="mb-3 position-relative">
              <label for="role" class="form-label">Alege rolul</label>
              <Field
                v-model="editedStaffHistory.Role"
                name="role"
                as="select"
                :class="{ 'border-danger': errors.role }"
                class="form-select form-control"
              >
                <option value="" disabled>Roluri</option>
                <option
                  v-for="(role, index) in Roles"
                  :key="index"
                  :value="role.name"
                >
                  {{ role.name }}
                </option>
              </Field>

              <ErrorMessage name="role" class="text-danger error-message" />
            </div>

            <div class="mb-3 position-relative">
              <label for="team" class="form-label">Alege echipa</label>
              <Field
                v-model="editedStaffHistory.TeamName"
                name="team"
                as="select"
                :class="{ 'border-danger': errors.team }"
                class="form-select form-control"
              >
                <option value="" disabled>Echipe</option>
                <option
                  v-for="(team, index) in TeamNames"
                  :key="index"
                  :value="team.name"
                >
                  {{ team.name }}
                </option>
              </Field>

              <ErrorMessage name="team" class="text-danger error-message" />
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
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";

export default {
  name: "EditStaffHistoryComponent",
  components: {
    Form,
    Field,
    ErrorMessage,
    VueDatePicker,
  },
  emits: ["get-list"],
  data() {
    return {
      stuffsList: [],
      selectedStaff: {},
      Years: [
        { year: "2024" },
        { year: "2023" },
        { year: "2022" },
        { year: "2021" },
        { year: "2020" },
        { year: "2019" },
        { year: "2018" },
        { year: "2017" },
        { year: "2016" },
      ],
      Roles: [{ name: "Antrenor" }, { name: "Kinetoterapeut" }],
      TeamNames: [
        { name: "CSU Suceava Juniori" },
        { name: "CSU Suceava Seniori" },
      ],
    };
  },

  props: {
    editedStaffHistory: {
      type: Object,
      default() {
        return {
          Id: "",
          Role: "",
          Year: "",
          TeamName: "",
        };
      },
    },
  },

  computed: {
    schema() {
      return yup.object({
        staff: yup.object().required("Acest câmp este obligatoriu"),
        year: yup.string().required("Acest câmp este obligatoriu"),
        team: yup.string().required("Acest câmp este obligatoriu"),
        role: yup.string().required("Acest câmp este obligatoriu"),
      });
    },
  },

  methods: {
    EditStaffHistory() {
      this.$axios
        .put(
          `/api/StuffHistory/updateStuffHistory/${this.editedStaffHistory.Id}/${this.editedStaffHistory.Stuff.Id}`,
          this.editedStaffHistory
        )
        .then((response) => {
          console.log(response);
          console.log(this.editedStaffHistory);
          this.$emit("get-list");
          $("#stuffHistory-edit-modal").modal("hide");
          this.$swal.fire({
            title: "Succes",
            text: "Participarea a fost actualizata",
            icon: "success",
            showConfirmButton: false,
            timer: 1500,
          });
        })
        .catch((error) => {
          console.error(error);
          console.log(this.selectedStaff.Id);
          console.log(this.editedStaffHistory);
        });
    },
    ClearModal() {
      this.$refs.editStaffHistoryFormRef.resetForm();
    },
    GetStaffsList() {
      this.$axios
        .get("/api/Stuff/getStuffsDropDown")
        .then((response) => {
          this.stuffsList = response.data;
          console.log(response.data);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    updateSelectedStaffId() {
      const selectedPlay = this.stuffsList.find(
        (champ) => champ.Id === this.selectedStaff.Name
      );
      this.selectedStaff.Id = selectedPlay ? selectedPlay.Id : null;
    },
  },
  created() {
    this.GetStaffsList();
  },
};
</script>
