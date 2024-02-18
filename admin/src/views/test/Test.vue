<template>
  <!-- <Bar :data="data" :options="options" /> -->
  <!-- <DButton @click="notificationStore.showDialog()">Bật thông báo</DButton>
  <div>
    {{ testValue }}
  </div> -->
  <!-- <DDatePicker v-model:date="model" date-format="d/m/Y"></DDatePicker> -->
  <div class="w-[250px]">
    <VueDatePicker
      :time-picker="mode.timePicker"
      :month-picker="mode.monthPicker"
      :year-picker="mode.yearPicker"
      v-model:time="time"
    ></VueDatePicker>
  </div>
  <div class="flex">
    <DButton @click="changeMode('yearPicker')">Một năm</DButton>
    <DButton @click="changeMode()">Ngày / tháng / năm</DButton>
    <DButton @click="changeMode('monthPicker')">Tháng / năm</DButton>
  </div>
</template>

<script setup>
import { ref, watch, reactive, nextTick } from "vue";
import { useNotificationStore } from "@/stores/NotificationStore";
import DDatePicker from "../../components/base/datepicker/DDatePicker.vue";
import VueDatePicker from "../../components/base/datepicker/VueDatePicker.vue";
const notificationStore = useNotificationStore();
const testValue = ref("Hello");
const check = reactive(notificationStore);
const mode = ref({
  timePicker: false,
  monthPicker: false,
  yearPicker: true,
});
const time = ref(new Date().getFullYear());
watch(
  () => check.selectedValue,
  (newValue) => {
    testValue.value = newValue;
  }
);

async function changeMode(newMode) {
  mode.value = {
    timePicker: false,
    monthPicker: false,
    yearPicker: false,
  };
  if (newMode) {
    mode.value[newMode] = true;
  }
  await nextTick();
}
</script>
