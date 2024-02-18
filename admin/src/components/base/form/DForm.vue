<template>
  <div class="fixed__container">
    <!-- <w-overlay v-model="showOverlay"> -->
    <div class="form__container max-h-[600px] overflow-y-scroll rounded-[4px]">
      <div class="sticky top-0 right-0 bg-[#fff]">
        <div class="flex justify-end pt-[8px]">
          <w-tooltip bottom>
            <template #activator="{ on }">
              <DIcon
                iconName="mdi mdi-help-circle-outline"
                iconSize="28px"
                v-on="on"
              ></DIcon>
            </template>
            Trợ giúp (F1)
          </w-tooltip>
          <w-tooltip bottom>
            <template #activator="{ on }">
              <DIcon
                iconName="mdi mdi-window-close"
                iconSize="28px"
                @click="emits('onClose')"
                v-on="on"
              ></DIcon>
            </template>
            Đóng (ESC)
          </w-tooltip>
        </div>
        <div class="flex justify-between items-center mb-[16px]">
          <h1 class="text-[24px]">{{ title }}</h1>
        </div>
      </div>
      <slot name="form_body"></slot>
    </div>
    <!-- </w-overlay> -->
  </div>
</template>

<script setup>
import { ref } from "vue";
const emits = defineEmits(["onClose"]);
const props = defineProps({
  title: {
    type: String,
  },
});
const showOverlay = ref(true);
</script>

<style lang="scss" scoped>
.fixed__container {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: rgba(0, 0, 0, 0.3);
  z-index: 100;
}

.form__container {
  background-color: #fff;
  padding: 0 24px 24px 24px;
  border-radius: 4px;
  min-width: 500px;
}
::-webkit-scrollbar {
  width: 8px;
}
::-webkit-scrollbar-thumb {
  border-radius: 4px;
  background-color: #999;
}
</style>
