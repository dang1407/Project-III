<template>
  <div>
    <w-tooltip bottom v-if="tooltip">
      <template #activator="{ on }">
        <div v-if="label" @click="focusInput" class="w-fit font-bold">
          <span class="flex items-center w-fit" :class="labelStyle">
            {{ label }}
            <div v-if="required" class="text-[red] ml-[2px]">*</div>
          </span>
        </div>
      </template>
      {{ tooltip }}
    </w-tooltip>
    <div v-if="label && !tooltip" @click="focusInput" class="w-fit font-bold">
      <span class="flex items-center w-fit" :class="labelStyle">
        {{ label }}
        <div v-if="required" class="text-[red] ml-[2px]">*</div>
      </span>
    </div>
    <div class="flex">
      <w-tooltip bottom @open="handleOptenErrorTooltip">
        <template #activator="{ on }">
          <div v-on="on"></div>
        </template>
        {{ errorTooltip }}
      </w-tooltip>
      <div class="w-[100%]">
        <flat-pickr
          ref="vFlatPickR"
          v-model="date"
          :config="datePickerConfig"
          :tabIndex="tabIndex"
          @on-change="handleChangedDate"
        />
      </div>

      <div
        class="flex justify-center items-center w-[36px] h-[36px] ml-[-36px] pointer-events-none"
      >
        <w-icon>mdi mdi-calendar-blank</w-icon>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, watch, reactive } from "vue";
import flatPickr from "vue-flatpickr-component";
import "flatpickr/dist/flatpickr.css";
import "flatpickr/dist/plugins/monthSelect/style.css";
const emits = defineEmits(["update:modelValue"]);
const props = defineProps({
  tabIndex: {
    type: String,
  },
  enableTime: {
    type: Boolean,
    default: false,
  },
  label: {
    type: String,
  },
  labelStyle: {
    type: String,
  },
  required: {
    type: Boolean,
  },
  tooltip: {
    type: String,
  },
  errorTooltip: {
    type: String,
  },
  focus: {
    type: Boolean,
  },
  dateFormat: {
    type: String,
    default: "d/m/Y H:i",
  },
});
const propsReactive = reactive(props);
const isShowFlatPickR = ref(true);
const date = defineModel("date");
const dateFormat = defineModel("dateFormat");
const vFlatPickR = ref(null);
const datePickerConfig = ref({
  locale: {
    firstDayOfWeek: 1, // Bắt đầu từ thứ 2 (ngày đầu tiên của tuần)
    weekdays: {
      shorthand: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"],
      longhand: [
        "Chủ Nhật",
        "Thứ Hai",
        "Thứ Ba",
        "Thứ Tư",
        "Thứ Năm",
        "Thứ Sáu",
        "Thứ Bảy",
      ],
    },
    months: {
      shorthand: [
        "Th1",
        "Th2",
        "Th3",
        "Th4",
        "Th5",
        "Th6",
        "Th7",
        "Th8",
        "Th9",
        "Th10",
        "Th11",
        "Th12",
      ],
      longhand: [
        "Tháng Một",
        "Tháng Hai",
        "Tháng Ba",
        "Tháng Tư",
        "Tháng Năm",
        "Tháng Sáu",
        "Tháng Bảy",
        "Tháng Tám",
        "Tháng Chín",
        "Tháng Mười",
        "Tháng Mười Một",
        "Tháng Mười Hai",
      ],
    },
  },
  allowInput: true,
  dateFormat: props.dateFormat,
  enableTime: props.enableTime,
  // time_24hr: true,
  // altFormat: "M Y",
  // plugins: [
  //   new monthSelectPlugin({
  //     shorthand: false,
  //     dateFormat: "M/Y",
  //     altFormat: "M Y",
  //   }),
  // ],
});

function handleChangedDate() {
  // emits("update:modelValue", date.value);
  vFlatPickR.value.fp.close();
}

function handleOptenErrorTooltip() {
  console.log("Xin chào");
}

// watch(date, (newDate) => {
//   // console.log(newDate);
//   emits("update:modelValue", newDate);
// });

// watch(
//   () => propsReactive.dateFormat,
//   (newValue) => {
//     // isShowFlatPickR.value = false;
//     datePickerConfig.value.dateFormat = newValue;
//     // isShowFlatPickR.value = true;
//   }
// );

onMounted(() => {
  // console.log(vFlatPickR.value);
  // if (props.modelValue) {
  //   date.value = props.modelValue;
  // } else {
  //   date.value = new Date().toString();
  // }
});
</script>

<style>
.flatpickr-input {
  width: 100%;
  border: 1px solid #aaa;
  border-radius: var(--border-radius-general);
  padding-left: var(--padding-left-general);
  height: var(--input-height-general);
}
.flatpickr-input:hover,
.flatpickr-input:active,
.flatpickr-input:focus {
  border-color: var(--input-border-hover-color);
}
.flatpickr-day.selected {
  background-color: #2ca012 !important;
  border-color: #2ca012 !important;
}
</style>
