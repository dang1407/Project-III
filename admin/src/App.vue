<script setup>
import BaseLayout from "./components/layouts/base/BaseLayout.vue";
import Login from "./views/login/Login.vue";
import { useUserStore } from "@/stores/UserStore";
import { storeToRefs } from "pinia";

const userStore = useUserStore();
const { isLogined } = storeToRefs(userStore);
</script>

<template>
  <BaseLayout v-if="isLogined"></BaseLayout>
  <Login v-else></Login>

  <!-- <BaseLayout></BaseLayout> -->
</template>

<style scoped></style>
