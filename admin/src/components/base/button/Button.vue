<template>
  <w-button
    :bg-color="bgColor"
    height="36px"
    :class="{ '!border-[#ccc]': bgColor == 'white' }"
    :route="route"
  >
    <slot></slot>

    <DIcon
      v-if="iconConfig"
      :iconName="iconConfig.IconName"
      :iconColor="iconConfig.IconColor"
      :iconSize="iconConfig.IconSize"
    ></DIcon>
  </w-button>
</template>

<script setup>
import { ref, onMounted, nextTick } from "vue";
const props = defineProps({
  bgColor: {
    type: String,
    default: "success",
  },
  route: {
    type: String,
  },
  loading: {
    type: Boolean,
  },
  tooltip: {
    type: String,
  },
  focus: {
    type: String,
  },
  iconConfig: {
    type: Object,
  },
});

onMounted(async () => {
  // await nextTick();
  // if (props.focus) {
  // }
});
</script>

<style lang="scss" scoped>
.w-button {
  min-width: 80px;
  border-radius: 4px !important;
}

.button--white {
  border: 1px solid #ccc !important;
}
</style>
