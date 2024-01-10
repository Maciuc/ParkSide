<template>
  <div>
    <div class="row header-section align-items-center">
      <div class="col">
        <div class="title-page">Utilizatori</div>
      </div>

      <div class="col-auto">
        <button class="button green" @click="OpenModalAddUser">
          Adaugă
          <font-awesome-icon :icon="['fas', 'plus']" style="color: #ffffff" />
        </button>
      </div>
    </div>

    <AddUser @get-list="GetAllUsers()" ref="addUserModal"> </AddUser>

    <div class="row d-flex flex-row mt-3 new-form">
      <div class="col-3">
        <div class="input-group mb-3">
          <span class="input-group-text"
            ><font-awesome-icon
              class="search_icon"
              :icon="['fas', 'magnifying-glass']"
              style="color: #688088"
          /></span>
          <input
            type="text"
            class="form-control"
            placeholder="Caută nume"
            aria-label="Username"
            aria-describedby="basic-addon1"
            v-model="filter.SearchText"
            v-on:keyup.enter="GetAllUsers()"
          />
        </div>
      </div>

      <div class="col-3 custom-control">
        <select
          class="form-select form-control"
          aria-label="Default select example"
          v-model="filter.SearchRole"
          @change="GetAllUsers()"
        >
          <option selected>Toate rolurile</option>
          <option
            v-for="(role, index) in roles"
            :key="index"
            :value="role.name"
          >
            {{ role.name }}
          </option>
        </select>
      </div>
    </div>

    <table class="table table-custom">
      <thead>
        <tr>
          <th width="20%" @click="OrderBy('name')" class="cursor-pointer">
            <font-awesome-icon
              v-if="filter.OrderBy === 'name'"
              :icon="['fas', 'arrow-up-wide-short']"
              style="color: #29be00"
              rotation="180"
              size="xl"
              class="me-2"
            />

            <font-awesome-icon
              v-else-if="filter.OrderBy === 'name_desc'"
              :icon="['fas', 'arrow-up-short-wide']"
              rotation="180"
              style="color: #29be00"
              size="xl"
              class="me-2"
            />
            <font-awesome-icon
              v-else
              :icon="['fas', 'arrow-up-wide-short']"
              rotation="180"
              size="xl"
              class="me-2"
            />
            Nume & Avatar
          </th>

          <th scope="25" width="15%">Rol</th>

          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(user, index) in UsersList.Items" :key="index">
          <td>
            <div class="d-flex align-items-center">
              <div class="img-container-avatar me-2">
                <img
                  :src="ShowDynamicImage(user.Image)"
                  class="me-2 icon-avatar"
                />
              </div>

              <span>{{ user.Name }}</span>
            </div>
          </td>
          <td>{{ user.Role }}</td>
          <td>
            <div class="editButtons">
              <button
                type="button"
                class="button-edit"
                @click="GetUserForEdit(user.Id)"
              >
                <font-awesome-icon :icon="['far', 'pen-to-square']" />
              </button>

              <button
                type="button"
                class="button-delete"
                @click="DeleteUser(user.Id)"
              >
                <font-awesome-icon :icon="['fas', 'trash']" />
              </button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>

    <EditUser
      :user="selectedUserForEdit"
      @get-list="GetAllLinks()"
      ref="editUserModal"
    >
    </EditUser>

    <Pagination
      :totalPages="UsersList.NumberOfPages"
      :currentPage="filter.PageNumber"
      @pagechanged="GetAllUsers"
    />
  </div>
</template>

<script>
import Pagination from "../components/Pagination.vue";
import AddUser from "../components/Modals/Users/AddUserComponent.vue";
import EditUser from "../components/Modals/Users/EditUserComponent.vue";

export default {
  name: "SocialMedia",
  components: {
    AddUser,
    EditUser,
    Pagination,
  },
  data() {
    return {
      selectedUserForEdit: {
        Id: "",
        Name: "",
        Role: "",
        Image: "",
      },

      filter: {
        SearchText: "",
        PageNumber: 1,
        OrderBy: "name",
        SearchRole: "Toate rolurile",
      },
      UsersList: {
        Items: [],
        NumberOfPages: 0,
      },

      roles: [],
    };
  },
  methods: {
    ShowDynamicImage(imagePath) {
      if (imagePath === "") {
        return `src/images/user.png`;
      }
      return imagePath;
    },

    OpenModalAddUser() {
      $("#user-add-modal").modal("show");
      this.$refs.addUserModal.ClearModal();
    },

    OpenModalEditUser() {
      $("#user-edit-modal").modal("show");
    },

    GetUserForEdit(id) {
      console.log(id);
      this.$axios
        .get("https://jsonplaceholder.typicode.com/posts/1")
        .then((response) => {
          this.selectedUserForEdit = this.UsersList.Items.find(
            (x) => x.Id == id
          );
          console.log(this);
          this.OpenModalEditUser();
        })
        .catch((error) => {
          console.log(error);
        });
    },

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

    GetAllUsers(page) {
      this.filter.PageNumber = 1;
      if (page) {
        this.filter.PageNumber = page;
      }
      const searchParams = {
        OrderBy: this.filter.OrderBy,
        PageNumber: this.filter.PageNumber,
        //PageSize: 4,
        NameSearch: this.filter.SearchText,
        RoleSearch: this.filter.SearchRole,
      };
      this.$axios
        .get(
          `https://jsonplaceholder.typicode.com/posts/1?${new URLSearchParams(
            searchParams
          )}`
        )
        .then((response) => {
          this.UsersList.NumberOfPages = 4;

          console.log(searchParams);
          this.UsersList.Items = [
            {
              Id: 111,
              Name: "Ion Popescu",
              Role: "Administrator",
              Image: "",
            },
            {
              Id: 122,
              Name: "Maria Popescu",
              Role: "Editor general",
              Image: " data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAFoAAABkCAYAAAAG2CffAAAACXBIWXMAAAsTAAALEwEAmpwYAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAABbASURBVHgB7Z17jFTXfcfPLMsC5rEbEju7NTizWFXrR1X8Rxzjti6WsE3zULFx/EhUebFs2ZVaA3LsSG0DOMT9I3HEbps/nLry4kZJC4JiJ1b8AAmoakz8R9kqgCtVNQu7FDCGneXNPubmfM6d3+yZM+feuXdmFi+Br3TZmbmv3/nd3+/7e5wzQ6ZvXlugrmLc0Vh8MWeuuor6Y6S/z/w1ikbJrTt/pa6i/jj6p7drZferBnUVlwSNqkoEp0+poX371OjhvuJnk66fq5puuUVlZs7ynnPqH36oznS/oqbes1g1L/+WPn6O+jSA7CN9fSp/6pRqmDVLNc6dGykzGP5wnxp4foWa0fGkmnLHnVXJnSEYpqGOi7vf1wp7ydwcQX2Y8qUFatbyZ/XfO4ufXdj6tvrk6ceL7xlgy999V12z9CF1KYByT7/6irr4q116e79sf9PNt6jGm24pMwAM6dhX7ymOFV199uVX1WR9bBJURR1Y5PFvLjWCRikZsJ8HImCQue+tKjmG809qKznV9UM13kCWY19dZOT3KRkM7d+nzm3eqM5u2lDy+cnnVpSMleCG4s/qY9MgMXUgpE8pWObURX+mn3RoBVj68P69Jcec1nTBU426LtYEndjg4QCfS48e7k/svq4n2cAqJ990q77XoFEgsts4t3lD5IOBShiz7bVxSEQduM+Ru75U9nmj5uTP/WxzUck2EHxUK/f8treNwHEewMP6/JvbjPJQ8GCn5vL1r6jpmlY+8/3OsuNR3JCmAB7OVD1Qo7Cbb/HKbbu9fT/c31WSpGKTmpvV+XffNhQZZSCu3IBxYjjIBS0CoY5EFo37+AC/+pQsPB5lDS4MjTy3XM3+Qac6/o2lxcEN799XdizKw0rDgW00m43WnR8UZRrUHuh7wNO0InyWiIIH1q4y148zDJ/c6EjGe+Hdd1Sgud72xoqKZmBJFQZya1cba0wL7mEruUlbKd7igmPiMKl5bHDyQMqOub68OINzB3UcSaJg5chdEiwLXu5SXkVFx7kOEVypZ4vv4S07SOBacCBu3aBvbB7a7l1q5HB/7L0QdvbL3eZ8G7hlnDycJwOMC9iu3Hb84Z7XLH1YW/19RnbeE3Pgb47xyW7fB0ryeXlFRbuBrVTg941i4VIsWZSMcDOWPWnyTldZgFw6973Vkdf9zA/WlQkbFYxtTLljQfH1aP8hFSc3MiCjfV3ez3rm2TKZJ998q9maNN2c0PHBDZoCzvXFClBR0ZVcCSsmMImS4T6eqk/BAgbE/pPPryzbx0Nz+dOlI84lSEINtgfNfOZbieXmQXMMio4Kji5IGq7VtPCxThVdy8abqB2iUDGPHrEqvyjIYFHStT/bFKtkAe453VOs4AUupt17X8l7FIT7o+zZ319X9CDbC+IoRoCSwWx9naRpmtzLRZySzXkZpXrjDhgyfJYMuOTo4coDFLi5M1bhcz2U4D68M93/bFyYB4Y14rY2Lmx7SyUF6WKawsmVO+T1+Ao31qIrBR8X5KFH7ro9sdDnnaxgUkyrdpozOEAhBNwHkVZuOefjr92TyFDcYxpmNVc8x6toUzJrXqy2PEZot+T2wfWW0Ria8vUW4GipIAXQWLVyU4aTPlZS9tnNpWV6/tRg7PGwRpmiEZK+AMGHwcFHNInSAtcmUEYBt3etDo+4GEFVPt6Hq+3eBJmEfc9q5EaGOGWf9RRJEjPi0BgeOPZEUGxY8YVuHNeIqQSEkoflgnzahwGdiXz+za1lCf+kOTd4jz/37xtNBmCX+TwUghMWqqqQXZQNFzdYckR1/sCgzmKu+8XWyGtm+trbtmvbXjjn//6/ZEfaMjoKDLrtPz4oUxwBKKpym7HsiWKvANj9j0r3IjiSuVzY+lZsrj4ecOUG/Tf+jqGOsjwayzit3V64jyBEkGq6+WbtphtTKx4row/spj9BDK9BO4bZlCpWZVF5MZkKHki2IkHRLohMY18fMxRRZNQTyO3rowRBoBXdoHLKmgfHIthKDjwdNk+qtW635AWVCooz3aUFCiXx+a3vlB1HtcZDlBkf8ULJd6ctuk+d+MvHVTUI77s4tm3gAurzdUIb8/lgsCGTCVt5VsIvgke1ORGC5gypzfCHe2MV5ytZ6QFXAgWNsVbdc+AaPkUXr4exZAKjdOQixpDF2I2qaiBt2iQtACABHauWtisoow6sAcWed1qF0iCin0A0d92Diw/oVmGlpkvxxvpalRRAIE1TsblTZ6SY9j3YD8VAgzwM5HLL+Ci5KYjwjuPffLCiN9LetWXRdqypIxNWhiFnzikocYFq1hfDUoFYSBy4MB03kv4kYFYlKhgK0rQsxQN54BiJeBFy4xXXPPCQdwxQA/uPf+PBivegsXTtTzepY1+7VyWR204RixaNm9nlr2shSWD3gm00eYqNqYsWV3TFNIt6yGomaeqbfFpPss4NKbDp9jsTXUOCqPtgGz19a5QNPaUpikaVOtiYyWuLbqhsPWEv+f3Imh4LOvH0Mu++pjvKH1iTyRIWxAbYSXPSTeujVFexWDpji5pjlNTRN3677WqD9JFgHbkKoHDeWKs2k2tUkzIm64grIxFGggq8JxxNIOQ8XDVKYViFryMHsIwol+W8tB7lg+TfyAxNEGdIV0cLQYt9UQqz2642JKPxWbUtt1w3kwlyjQ2jDb1BQz42fTljzWJzMtE/LgOw0fKdF7wzDmBKYWLVl5VEWVMamAU7hSLHLIFIkZ4S/BpjPCqqvLc9XnSWGdW9jmE1muNNfjCaOmbqm9KKJN1K0msGPFl601M9XTcb0+5d7D03yprSgACIzI0pVxah5Er9ZTzDhdv8l17+iNZxY3vv0d6+eW1q5MO9sRfG7aZqroUGQqrYVSwM7FU8YUq2wBQ9SR5K6GalLjjj8SdirSkpkEfyYOSlsh36YJeR11cXmMmIBx6KnI5yj3eB99oYq36HwxKcWlxrP6siQBpG79fnegjMgJq0cqc/8HBql3fn9shQojg9LQjgtBPoe0Tl7FI9TrkjXTxwdYFxud5boMRce28uZxQdZFRPcOpUlqDnNn+SVERUQGy0D0XpSRYxho2r0mvTk5DKqhYkXT5A/GFDUdOXPpx4EeOprpdK3qN4W39FrwmCHt6HbVIpw/tKc2mz8iZlE91WOikcvYhpOmeWVaZuT8IHt7JKCyw5rhfugx0sJatic1fHxnU16Y1LS1gCfKAyhj/CgiWTQeuPsdNWdNqFfC7oB7O5DaJKVja0f7+qBYM1LpwcU3p4HYkXPm63Ycst6XKQsSyaWpxcGre9xrkhkZR1FhJlKcux1jQdLRtJyupaA6GU9uHS4TBm4CWkpBdTTDYLkjalbLllNW0+nx9T9Oho0NOo58PtxTLSeZqsGzC2G4tLAVlYnnYZVSW4bdo0EKujrWrPjCMzbs24Tmu5a/VWH+xEYLiYxQW9/GsUXUjxcpo6WuRAghpuLksIfAGCgZCrVis4g0chDbpHQh4v6WItFi1pV+REQSHlI0MYXLuqKq+0lyrLMjdyN9sgxzKO42MWbZBRO7RwS+y+NDmxLD4kk5AmS9gqvbNEcFw0am2aT1DfyiAsLq13+B6K9FDoJGIIQns8UJlNl5kQ8cqkiFo2ZsuN8u2Mw8hZPDBQO3VvaQlLABrnhGWk9CIkkxBce8emMgFwdzKMStaNgNf9Yluk1SYpcmQB+fQHv+7NTlAGig6D8di84eecr0SEX+94wXhVVC/dBgY1PaKpZss9VFhunM+oncX9xRf5UPv23NqUwjSRe8Go1Eusm2VaUWUvk5e1BjsqsNk6QEfJgbW6CjHuHtEO4DrXvbkttuzGiqcn/L6NBELdQtpRvL+8mNt7lA9zbjOeG9iK89X4LrDuVj3z7SpcJlIvBXjg9jKHSnKbJQp6rMz3+R7SzMeTV6uS2RR0alA6laV5WlPEErdClAnbtKmRfR7pVb2zk0qAFmZ0PFFYEZVJdI54JY20ITNb845RdNzX42zAzyYQBsGOyIMOzmtbwXdadIUTTDRc2L0rQLbD838vGNq/N5ioOLvp34ych25s7bB1W7IkLJ+fsp6/F3bXtmjGBusrJOHHU8hM+L7HuZTpoOmu6abVtT/dnPg7fja4t6z3sOfyaDPw2bk65dUXCqX56OgYP3vByiWsplboEjQY+O4q83Qv7H7PfMb7I39ye3D61X8q+fxS4MRzy4NjX1kUnNR/GR/yYX3IM9j5Ut3kOXLXFwOtwz2uXssWOeqU5I0ki/YqIaeLAd/ajSadg09/MFmv2ocdO3aYLS2wWLIK+FcWRzYVvp1AoEOe0RrWfwBSSrxX6/A1d1+5ogv0kXSqKgoEE/c7gqzcZ8CH5/9+qjUbgq6uLnX33XebbeXKlaq3tzfxuRQxF5iskB5EoY9OEw15zKqkexerWiDLeXU39PVEJ9SLPoDtkh8/+oBx3eG+Q8bFkgbdPXv2BAsXLmQtUsmWzWaD9evXJ7oG9/z40aXBJ08tM9QBXdhAtoG1q4JaEEUbwLsQvV704YLURxbjhCuF4peF5XI5Y7m33XaboYus7gDs2aHUAT0UXmPRHR0dxsIrWTf1wUxWe+pih7HRKycwy2LIjJ7Rj/smVyUIbQQNQVfikw5ksy3aEgd4yrXCtmgJgjx5gtBwX5/3nIGBgWDNmjVBS0uLsdwWPV+w+tsqGPhIZ6cnxjY+U5aFa6UHBw4c8F5T7o0141VgaN+vtRxfNPKYtHFf9WkjwZbrH8i2ZlUa9N3YtkWicz0xOjho3NgHV8FsC/9IBdqCSxRsb+zreDSZwrmve2/kQeH8rRYj/YfC3Lm9LRk3lyg627qQk10uGw9s3749WLFiRZmCt/88WsE+hS/5cqnC4fWkHF4LpEg5kL1uiaoGEHu9gqILAhzWS0CzlYOyohSMMuOsO8rCuQdWzj3HA1BP/7y2A6paSElej0QeWtiyZYuxXFe5828N+fZAjwo6X1RB94/KlYeFw9VRit7+Rnguf+UcrqODZpnSkQEvqgeiSu5UqDYowo8MpLOz01iSq1gz4LmhcsV6UVJLc/i5rUxeZ28Iz+F4nwXP/4Oxa7oBk23PThWseDp8oLYMUBX0gpzIizGkBbrBmisFwYotrYPzWtc0qMxqlndRYPT09JhUitRrcHDQ/JX3/JXXLrQ1qiVfUeoPbw3/Zq1Fn+v/VallfxW+1laotOsXwefsNw+etM75clb7bTrNOxS9n31dP9apos4klz+l5WjW1eV7Sr3xS6V6fq33O19t1MpX8+fPV9o4zMb75uZm89o9hi4dPyGRUcH6OR8d9S+lTQrXquFV5Vin8lgrXIsV4b5QQhyvisVyXuS+G/x0oazg6bN2vEQVuN93b6x9y09CWbmGSzW+rbu72+ii6pQuCli1cDXuJdkBrrjm+VCZCItCfa4bt9nK8ilD9qEwdx/3lf0EQHe/BEWXjpJsPABobd2L4RiFdqBBICld/7zW7iQ6TPQrYfn8VJoWOSop3Gb16tXmc9xu+dOhq2slGTpoqfy16EjkPIWiXI99uHwUeg9Ff6Y5vIxSKkErVmkLV9rSlY4NevDh5zJ2WaQznFcvJLleIkW39/bm8iroClfw7FI6ahvOYvBdL6uaYCsARbrKXPHU2OuVf1Pg1UPhZj9UznOVbT+kWkCM4NqMmZI//ELVRsPNLNVQ9YTL1URpVXDpSnSB20IRUe4LNyqLIoQrhV/TbpK92OdDI5L+paE3O+sRbpa8uW7c7EK4Wrpu0lEjkEQJavOoDLj7H8NB8zpJ8BmPTR5oJe4Wnl+yZElJ3tyndaFSINmMpQWeZGbWrCzf7z544qRqb283n+vgYXjNxo7/1OnZX/v50wc4Hj7F5Xmtsyrz2t2MG0dwLjRhUwXH8n5wMORZeW/op5Da6WBneNgFx5A+Al0bqC98drb55Qc9QdA756Mj7Wo8IT2QgbXfKUn3fOmVuJyKSAHxBMlW3EwES3JLcd0iNffxFS54DtdLm/VwPPfh2u4+yTR0ADRjHex6qfYqMJWy9cSAne5J5Qcd2IK6bUx5ICg3auDrf+RP1yQNdHNtFCUc76MB9qdVvk151aZzdQFBgMCo3agsMLqD5T2KZUsSNCWAudYs3uHm2jorifQo9vmKoDQBUNqtZvaknsVJUvQXGk7SRqVREzXgpJtdvLj9Dvkc5fnc2/UACg0+dxtUSTYJgC5lpA2AdYNQCAta4igk6fb6T8YU6lq0WLr7IL8w1/+50EkcTfm2zhejKKOGNqjGJFUDnmmZsbMhk+kY7vmvqbO//ohaqGe5N2zYoIuHC+qR+9NXia3XKfXj9Xp+76JSO98LKzNeE/0P6gzhf/43zBTIII4eC4sXPu8tbJJCcfx/7w2Pv+l39XX+ONn9Oe/RJ8N76t61qYJZ+ssc40g+uK0rdyanPi0IhUgWsm7dumITKIqTi/3l5rCPsOVfxvLqaouUuA164drcJy5OCC8zBpsyGKOqEanzaB+0MFv0nyWsP2ZpLGXqa6+9ZvoEmitLQA5L6/P1X1a+Lq1Vk1PfMJZDF/c1h/sFdrvTzqXtfFlAb2b186W5OOV9p26nIruuAIs/8F2XFqiqk6Ipzyc3XNxDIcMPV5+ePsMsEaA3rS3VNJ4EMiAX0MSffzkcvGkC1fm/hTFFSt9YsQL9yP3o16z427CXAWXMPHs6/Ek2XZgM54O7697PqAX92evn42akfMyckxZJO5WWo52f2mV3kklY3B2K6Xhk7DMpXtb9ffmxlPhJgx/XUYXg56ZyjElNRLh8Lfk13Gena1KNJSkkJKe1e8qSb7s5MsUOn7vKr3RtZGQ+E8jCzE8tlUsKLWQngrJgJWlwjNukILHzZFlWYOfUkmsnLVCQpTgXWZYvt3WqOqPu/7PQSH7KGi1+D0utpHdNsxxevP8vVGr0FL6uZ3O2r7/8RiG4Jm3wE5CRCdl0v8YEPyY2zA8Q5M+vUZcDKFNJ8FkTMtIfLvt67LHHvBVcpU2s1y5I4GulSucRpTdBKpfUS5BJihJkHdce83ihEBwHjtx1e1HZeubYDBBF+VyZIGbnuiYIfru0l02Hzl6RxFIDHoK9lEByZl9HTh4SsoiSC8Fv4LJTskBaqijbfANAl+lRyi5bXRQzIcBCGvaziZLNjMosTzvWCsS2kpHFUnJwKNtW3XKuiQLJRCTts3siomxxZRVR0a0rrF4iTUwSUDnGnsWm31Gkm0IahyzIVK/Kb0KgrzAFZufYtrKl06ZS5NZpNlfJQJQ84dO4tBBly9pkV9lmAvfn1aWAaZV8srDw5bdOyYJKyq6ngl1OvmKULHCVbQfItKlf3Ea5rqzAd0UpWeByNopgOl8VAmAt9GHPIbIcgmtzj0+e6hi3qm9Cw1W2PR3m9kaSbnbvQooRO7u4YizZhSjbLmroO6gqlE1x4vYuyJOveCUL4pRNJy7J/KN07ZQ1O2IXI1e8kgW2suWXC2hbSj87LiORzIJj5esSXOOqkiNwKNvaId8DPP/uW2XpH/0Nt6ctQc/Okc9s2mCucUlXFF1uoBFFB81eL4ICJSMR3rb5WDILp588MGFnRyYKpMVqz9TYvK3UGB9L0LNnRi7LVuenBbPkrL1tj6R/EiRZnwxNwMcy9VSSWehzriq5Csi0mB0koRLhYzvoHWpvXX8g29KirqI6SEbifl1avkR/ubQ567KuY7zBBEKmIdOtiTnLz7khdOFHDHMqH9xv/+zZRMVloWgA905uyLB+IWs+0BPAI0Fw/4Ra3BKDy0bRAmkKMVPNT7qrywS/AcgT5oeyzcHyAAAAAElFTkSuQmCC",
            },
            {
              Id: 123,
              Name: "Andrei Radu",
              Role: "Administrator",
              Image: "",
            },
            {
              Id: 444,
              Name: "Elena Vasile",
              Role: "Administrator",
              Image: "",
            },
          ];
        })
        .catch((error) => {
          console.log(error);
        });
    },

    DeleteUser(id) {
      console.log(id + "asta e id-ul din deleteNews");
      this.$axios
        .delete(`https://jsonplaceholder.typicode.com/posts/${id}`)
        .then((response) => {
          this.GetAllUsers();
          console.log(`Deleted news with ID ${id}`);
        })
        .catch((error) => {
          console.error(error);
        });
    },

    OrderBy(orderBy) {
      if (this.filter.OrderBy.includes("_desc")) {
        this.filter.OrderBy = orderBy;
      } else {
        this.filter.OrderBy = orderBy + "_desc";
      }

      this.GetAllUsers();
    },
  },

  created() {
    this.GetAllRoles();
    this.GetAllUsers();
  },
};
</script>
