<template>
  <div>
    <Header></Header>
    <div>
      <router-view></router-view>
    </div>
    <Progress v-show="isLoading"></Progress>
    <Dialog v-show="isShowDialog"></Dialog>
    <ToastMessageGroup></ToastMessageGroup>
  </div>
</template>

<script setup>
import { storeToRefs } from "pinia";
import Header from "../header/Header.vue";
import Progress from "../../base/progress/Progress.vue";
import Dialog from "@/components/base/dialog/Dialog.vue";
import ToastMessageGroup from "@/components/base/toastmessage/ToastMessageGroup.vue";
import { useNotificationStore } from "../../../stores/NotificationStore";

const notificationStore = useNotificationStore();
const { isLoading, isShowDialog } = storeToRefs(notificationStore);
</script>

<style lang="scss" scoped></style>
