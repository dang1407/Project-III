<template>
  <div>
    <VueDatePicker
      :time-picker="timePicker"
      :year-picker="yearPicker"
      :month-picker="monthPicker"
      :format="format"
      select-text="Chọn"
      cancel-text="Hủy"
      locale="vi-VN"
      v-model="time"
      :dayNames="dayNames"
    ></VueDatePicker>
  </div>
</template>

<script setup>
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import { ref } from "vue";
const props = defineProps({
  timePicker: {
    type: Boolean,
  },
  monthPicker: {
    type: Boolean,
  },
  yearPicker: {
    type: Boolean,
  },
  format: {},
  dayNames: {
    type: Array,
    default: ["T2", "T3", "T4", "T5", "T6", "T7", "CN"],
  },
});

const time = defineModel("time");
</script>

<style lang="scss" scoped></style>
