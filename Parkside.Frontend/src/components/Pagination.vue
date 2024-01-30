<template>
  <ul class="pagination">
    <li class="pagination-item">
      <button type="button" @click="OnClickFirstPage" :disabled="IsInFirstPage">
        <font-awesome-icon :icon="['fas', 'angles-left']" />
      </button>
    </li>

    <li class="pagination-item">
      <button
        type="button"
        @click="OnClickPreviousPage"
        :disabled="IsInFirstPage"
      >
        <font-awesome-icon :icon="['fas', 'chevron-right']" rotation="180" />
      </button>
    </li>

    <!-- Visible Buttons Start -->

    <li v-for="page in pages" :key="page.name" class="pagination-item">
      <button
        type="button"
        @click="OnClickPage(page.name)"
        :disabled="page.isDisabled"
        :class="{ active: IsPageActive(page.name) }"
      >
        {{ page.name }}
      </button>
    </li>

    <!-- Visible Buttons End -->

    <li class="pagination-item">
      <button type="button" @click="OnClickNextPage" :disabled="IsInLastPage">
        <font-awesome-icon :icon="['fas', 'chevron-right']" />
      </button>
    </li>

    <li class="pagination-item">
      <button type="button" @click="OnClickLastPage" :disabled="IsInLastPage">
        <font-awesome-icon :icon="['fas', 'angles-left']" rotation="180" />
      </button>
    </li>
  </ul>
</template>

<script>
export default {
  props: {
    totalPages: {
      type: Number,
      required: true,
    },
    currentPage: {
      type: Number,
      required: true,
    },
  },
  data() {
    return {
      perPage: 4,
    };
  },
  methods: {
    OnClickFirstPage() {
      this.$emit("pagechanged", 1);
    },

    OnClickPreviousPage() {
      this.$emit("pagechanged", this.currentPage - 1);
    },
    OnClickPage(page) {
      this.$emit("pagechanged", page);
    },
    OnClickNextPage() {
      this.$emit("pagechanged", this.currentPage + 1);
    },

    OnClickLastPage() {
      this.$emit("pagechanged", this.totalPages);
    },

    IsPageActive(page) {
      return this.currentPage === page;
    },
  },
  computed: {
    maxVisibleButtons() {
      if (this.totalPages === 2) {
        return 2;
      } else {
        return 3;
      }
    },

    startPage() {
      // When on the first page
      if (this.currentPage === 1) {
        return 1;
      }

      // When on the last page
      if (this.currentPage === this.totalPages) {
        return this.totalPages - (this.maxVisibleButtons - 1);
      }

      // When inbetween
      return this.currentPage - 1;
    },
    pages() {
      const range = [];

      for (
        let i = this.startPage;
        i <=
        Math.min(this.startPage + this.maxVisibleButtons - 1, this.totalPages);
        i++
      ) {
        range.push({
          name: i,
          isDisabled: i === this.currentPage,
        });
      }

      return range;
    },
    IsInFirstPage() {
      return this.currentPage === 1;
    },
    IsInLastPage() {
      return this.currentPage === this.totalPages;
    },
  },
};
</script>

<style>
.pagination {
  list-style-type: none;
  padding-top: 50px;
  justify-content: center;
}

.pagination-item {
  display: inline-block;
}

.active {
  background-color: black;
  color: #ffffff;
}

button {
  border-radius: 5px;
  width: 30px;
  height: 30px;
  justify-content: center;
  align-items: center;
  border: none;
  background-color: #ffffff;
  font-size: 12px;
  font-weight: 600;
  font-family: "Raleway";
}
</style>
