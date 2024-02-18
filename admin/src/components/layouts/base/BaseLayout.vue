<template>
  <!-- <Header></Header> -->
  <div class="split left">
    <SideBar></SideBar>
  </div>

  <div class="split right"><router-view></router-view></div>
  <Progress v-show="isLoading"></Progress>
  <Dialog v-show="isShowDialog"></Dialog>
  <ToastMessageGroup></ToastMessageGroup>
</template>

<script setup>
import { storeToRefs } from "pinia";
import Header from "../header/Header.vue";
import Progress from "../../base/progress/Progress.vue";
import Dialog from "@/components/base/dialog/Dialog.vue";
import ToastMessageGroup from "@/components/base/toastmessage/ToastMessageGroup.vue";
import { useNotificationStore } from "@/stores/NotificationStore";
import SideBar from "../sidebar/SideBar.vue";

const notificationStore = useNotificationStore();
const { isLoading, isShowDialog } = storeToRefs(notificationStore);
</script>

<style lang="scss" scoped>
.container {
  width: 100%;
  height: 100vh;
}

// .router-view__container {
//   height: 100%;
//   width: calc(100% - 260px);
// }

/* Split the screen in half */
.split {
  height: 100%;
  position: fixed;
  z-index: 1;
  top: 0;
  overflow-x: hidden;
}

/* Control the left side */
.left {
  width: 260px;
  left: 0;
}

/* Control the right side */
.right {
  width: calc(100% - 260px);
  right: 0;
}
</style>
