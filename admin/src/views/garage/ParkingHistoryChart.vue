<template>
  <div>
    <BaseView>
      <template #bv-title>
        <h1 class="flex items-center">
          Thống kê doanh thu
          <!-- <div class="w-[150px] pl-[12px]">
            <DDatePicker v-model="time" :dateFormat="dateFormat"></DDatePicker>
          </div> -->
        </h1>
        <!-- <DButton @click="toggleParkMemberForm">Thêm mới khách hàng</DButton> -->
      </template>
      <template #bv-body>
        <div class="pl-[16px] pt-[16px] flex flex-col items-center">
          <div class="w-[100%]">
            <h1>Biểu đồ thống kê doanh thu</h1>
            <div class="w-[100%] h-[400px] pr-[24px]">
              <Bar v-if="isShowChart" :data="chartData" :options="options" />
            </div>
          </div>
          <div class="flex w-[100%]">
            <!-- Begin: Chọn loại thống kê theo thời gian -->
            <div
              class="mr-[12px]"
              v-if="statisticalType == statisticalTypeEnum.Time"
            >
              <!-- <div>Kiểu thống kê theo thời gian:</div> -->
              <DComboBox
                label="Kiểu thống kê theo thời gian:"
                menuPosition="bottom-[40px]"
                :listOptions="timeStatisticalTypeOptions"
                v-model="timeStatisticalType"
                @onOptionChanged="handleChangeStatisticalType"
              ></DComboBox>
            </div>
            <!-- End: Chọn loại thống kê theo thời gian -->

            <!-- Begin: Chọn hoặc nhập thời gian -->
            <div v-if="dateFormat == 'dd/MM/yyyy' || dateFormat == 'yyyy'">
              <div class="font-bold">Nhập thời gian:</div>
              <VueDatePicker
                :time-picker="mode.timePicker"
                :month-picker="mode.monthPicker"
                :year-picker="mode.yearPicker"
                :format="dateFormat"
                v-model:time="time"
              ></VueDatePicker>
              <!-- <DDatePicker
                :dateFormat="timeStatisticalType"
                label="Nhập thời gian:"
                v-model:date="timeToSearch"
              ></DDatePicker> -->
            </div>
            <!-- End: Chọn hoặc nhập thời gian -->

            <div v-else></div>
          </div>
          <div class="flex w-[100%] my-[12px] justify-start">
            <div class="mr-[12px]"><DButton>Đồng ý</DButton></div>
            <div>
              <DButton bgColor="white">Hủy các lựa chọn</DButton>
            </div>
          </div>
        </div>
      </template>
    </BaseView>
  </div>
</template>

<script setup>
import { useGarageStore } from "./GarageStore";
import BaseView from "../base/BaseView.vue";
import axios from "axios";
import VueDatePicker from "../../components/base/datepicker/VueDatePicker.vue";
import { ref, onMounted, watch, nextTick } from "vue";
import { storeToRefs } from "pinia";
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  BarElement,
  CategoryScale,
  LinearScale,
} from "chart.js";
import { Bar } from "vue-chartjs";
ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);
const garageStore = useGarageStore();
const {} = storeToRefs(garageStore);
const dateStatical = [{}];
const monthStatical = [
  {
    Month: "Tháng 1",
    MonthValue: 1,
    Revenue: 0,
  },
  {
    Month: "Tháng 2",
    MonthValue: 2,
    Revenue: 0,
  },
  {
    Month: "Tháng 3",
    MonthValue: 3,
    Revenue: 0,
  },
  {
    Month: "Tháng 4",
    MonthValue: 4,
    Revenue: 0,
  },
  {
    Month: "Tháng 5",
    MonthValue: 5,
    Revenue: 0,
  },
  {
    Month: "Tháng 6",
    MonthValue: 6,
    Revenue: 0,
  },
  {
    Month: "Tháng 7",
    MonthValue: 7,
    Revenue: 0,
  },
  {
    Month: "Tháng 8",
    MonthValue: 8,
    Revenue: 0,
  },
  {
    Month: "Tháng 9",
    MonthValue: 9,
    Revenue: 0,
  },
  {
    Month: "Tháng 10",
    MonthValue: 10,
    Revenue: 0,
  },
  {
    Month: "Tháng 11",
    MonthValue: 11,
    Revenue: 0,
  },
  {
    Month: "Tháng 12",
    MonthValue: 12,
    Revenue: 0,
  },
];
const processedParkingHistoryData = ref(monthStatical);
const rawParkingHistoryData = ref([]);
const isShowChart = ref(false);
const statisticalTypeEnum = ref({
  Time: 1,
  ParkMemberCode: 2,
  LicensePlate: 3,
});
// Hộp combobox chọn loại thống kê
const listOptions = ref([
  { label: "Thời gian", value: statisticalTypeEnum.value.Time },
  { label: "Mã khách hàng", value: statisticalTypeEnum.value.ParkMemberCode },
  { label: "Biển số xe", value: statisticalTypeEnum.value.LicensePlate },
]);

const timeStatisticalTypeOptions = ref([
  { label: "Ngày / tháng / năm", value: "datePicker" },
  { label: "Tháng trong năm", value: "monthPicker" },
  { label: "Một năm", value: "yearPicker" },
  { label: "Nhiều năm", value: "yearsPicker" },
]);
// Chọn kiểu thống kê
const statisticalType = ref(statisticalTypeEnum.value.Time);
const mode = ref({
  timePicker: false,
  monthPicker: false,
  yearPicker: true,
});
const time = ref(new Date().getFullYear());
// Kiểu thống kê theo thời gian
const timeStatisticalType = ref("yearPicker");
const timeToSearch = ref("2024");
const chartData = ref({
  labels: [],
  datasets: [
    {
      data: [],
      label: `Biểu đồ doanh thu theo tháng`,
      backgroundColor: "#f87979",
    },
  ],
});
const options = ref({
  responsive: true,
  height: "70%",
});

//
const dateFormat = ref("yyyy");

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

function resetProccessParkingHistoryData() {
  if (dateFormat.value == "yyyy") {
    processedParkingHistoryData.value = monthStatical;
  }
}

async function proccessParkingHistoryDataAsync() {
  // console.log(time.value);
  resetProccessParkingHistoryData();
  isShowChart.value = false;
  const response = await garageStore.getParkingHistoryByOutDateAsync(
    time.value
  );
  rawParkingHistoryData.value = response.data;
  // console.log(rawParkingHistoryData.value);

  if (dateFormat.value == "yyyy") {
  }
  for (let i = 0; i < rawParkingHistoryData.value.length; i++) {
    const month = rawParkingHistoryData.value[i].VehicleOutDate.substring(5, 7);
    processedParkingHistoryData.value[parseInt(month) - 1].Revenue +=
      rawParkingHistoryData.value[i].Price;
  }
  console.log(processedParkingHistoryData.value);
  chartData.value.labels = processedParkingHistoryData.value.map(function (
    obj
  ) {
    return obj.Month;
  });
  chartData.value.datasets[0].data = processedParkingHistoryData.value.map(
    function (obj) {
      return obj.Revenue;
    }
  );
  chartData.value.datasets[0].label = `Tổng doanh thu tháng (Việt Nam đồng)`;
  isShowChart.value = true;
}

function convertSatisticalTypeToUIText(type) {
  const satisticalTypeText = {
    1: "",
  };
}

async function handleChangeStatisticalType(newOption) {
  mode.value = {
    timePicker: false,
    monthPicker: false,
    yearPicker: false,
  };
  mode.value[newOption] = true;
  if (newOption == "datePicker") {
    dateFormat.value = "dd/MM/yyyy";
    time.value = new Date();
  } else if (newOption == "monthPicker") {
    dateFormat.value = "MM/yyyy";
    time.value = {
      month: new Date().getMonth(),
      year: new Date().getFullYear(),
    };
  } else if (newOption == "yearPicker") {
    dateFormat.value = "yyyy";
    time.value = new Date().getFullYear();
  } else if (newOption == "yearsPicker") {
    dateFormat.value == "yyyys";
  }
  await nextTick();
}

// watch(time, async (newValue) => {
//   time.value = newValue;
//   await proccessParkingHistoryDataAsync();
// });

onMounted(async () => {
  await proccessParkingHistoryDataAsync();
});
</script>

<style lang="scss" scoped></style>
