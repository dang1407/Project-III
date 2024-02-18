<template>
  <div
    class="fixed z-10 top-0 right-0 bottom-0 left-0 bg-slate-100/70 flex items-center justify-center"
  >
    <div>
      <w-progress class="ma1" circle color="green"></w-progress>
      <div>{{ loadingMessage }}</div>
    </div>
  </div>
</template>

<script setup>
import { storeToRefs } from "pinia";
import { useNotificationStore } from "@/stores/NotificationStore";
const notificationStore = useNotificationStore();
const { loadingMessage } = storeToRefs(notificationStore);
</script>

<style lang="scss" scoped></style>
