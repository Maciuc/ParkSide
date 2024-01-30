<template>
</template>

<script>
export default {
    name: "ranking",
    methods: {
        UpdateRanking() {
            this.$axios
                .put(`/api/Ranking/updateRankings`)
                .then((response) => {
                    console.log(response);
                    this.$swal.fire({
                        title: "Succes",
                        text: "Clasamentul a fost actualizat",
                        icon: "success",
                        showConfirmButton: false,
                        timer: 1500,
                    });
                    this.$router.push({ name: "playersTrofees" });
                })
                .catch((error) => {
                    console.error(error);
                });
        },
    },
    created() {
        this.UpdateRanking();
    },
}
</script>
<style scoped></style>