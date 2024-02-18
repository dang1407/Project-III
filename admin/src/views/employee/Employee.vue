<template>
  <div @click="hideEmployeeFunction">
    <BaseView>
      <template #bv-title>
        <h1>Nhân viên</h1>
        <DButton @click="toggleEmployeeForm">Thêm mới nhân viên</DButton>
      </template>

      <template #bv-body>
        <w-toolbar>
          <div class="flex"></div>
          <div class="spacer"></div>
          <DInput
            @iconClick="employeeStore.goToSearchMode"
            :icon="{
              iconName: 'mdi mdi-magnify',
              iconPosition: 'right',
              iconColor: '#000',
            }"
            @keydown.enter=""
            v-model="employeeProperty"
            :focus="true"
          ></DInput>

          <div class="icons__container ml-[8px]">
            <DIcon
              iconSize="24px"
              iconColor="#000"
              iconName="mdi mdi-reload"
              @click="employeeStore.getEmployeeDataAsync"
            ></DIcon>
          </div>
          <div class="icons__container">
            <DIcon
              @click="
                employeeStore.exportExcelCurrentPage(
                  page,
                  pageSize,
                  employeeProperty,
                  aRef
                )
              "
              iconSize="24px"
              iconColor="#000"
              iconName="mdi mdi-microsoft-excel"
            ></DIcon>
            <a ref="aRef"></a>
          </div>
        </w-toolbar>
        <div style="height: calc(100% - 108px)" class="relative">
          <DTable
            :fixedHeaders="true"
            :items="employeesData"
            :headers="headers"
            style="width: 100%; height: calc(100%)"
          >
            <template #header-label="{ label, index }">
              <span
                v-if="label == 'checkBox'"
                class="w-[100%] h-[100%] flex justify-center items-center bg-[#e0e0e0]"
              >
                <DCheckBox
                  @checkCheckBox="checkedCheckbox"
                  :checked="checkBoxProperties.checked"
                  :icon="checkBoxProperties.icon"
                ></DCheckBox>
              </span>
              <span
                v-else-if="label == 'Chức năng'"
                class="flex justify-center w-[100%]pl-[8px]"
                >{{ label }}</span
              >
              <span v-else class="pl-[8px]">{{ label }}</span>
              <!-- {{ label }} -->
            </template>
            <template #item="{ item, index, select, classes }">
              <tr :class="classes" @click="select">
                <!-- <td class="w-[36px] flex justify-center">
                  <input type="checkbox" />
                </td> -->
                <td
                  v-for="(header, i) in headers"
                  :key="i"
                  :class="[
                    `h-[48px] text-${
                      header.align || 'left'
                    } left-[0]  bg-inherit`,
                    {
                      sticky:
                        header.key == 'EmployeeId' ||
                        header.key == 'functionBox',
                      // 'left-[0]': (header.key = 'EmployeeId'),
                      // 'right-[0]': header.key == 'functionBox',
                      // 'bg-[#fff]': header.key == 'EmployeeId',
                    },
                  ]"
                  :style="{
                    right: header.key == 'functionBox' ? '0px' : '',
                  }"
                >
                  <div
                    v-if="header.key == 'EmployeeId'"
                    class="flex justify-center items-center"
                  >
                    <w-checkbox
                      color="green"
                      @click="
                        employeeStore.selectOneEmployee(item['EmployeeId'])
                      "
                    ></w-checkbox>
                  </div>

                  <div v-else-if="header.key == 'Gender'" class="pl-[8px]">
                    {{
                      helperStore.convertGenderDBToGenderUser(item[header.key])
                    }}
                  </div>

                  <div v-else-if="header.key == 'DateOfBirth'" class="pl-[8px]">
                    {{
                      helperStore.covertDateDBToDatePickerDate(
                        item[header.key],
                        "dd/MM/yyyy"
                      )
                    }}
                  </div>

                  <div v-else-if="header.key == 'functionBox'">
                    <div class="flex justify-center items-center">
                      <div class="flex justify-center items-center">
                        <div class="text-[#0075c0]">Sửa</div>

                        <div
                          @click.stop="
                            (event) =>
                              showEmployeeFunction(event, item.EmployeeId)
                          "
                        >
                          <DIcon
                            iconName="mdi mdi-triangle-small-down"
                            iconColor="#0075c0"
                          ></DIcon>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div v-else class="pl-[8px]">
                    {{ item[header.key] || "" }}
                  </div>
                  <!-- <div>{{ item[header.key] || "" }}</div> -->
                </td>
              </tr>
            </template>
          </DTable>

          <div class="w-[50px] h-[36px] absolute top-0 left-0 z-10">
            <span
              class="w-[100%] h-[100%] flex justify-center items-center bg-[#e0e0e0]"
            >
              <DCheckBox
                @checkCheckBox="checkedCheckbox"
                :checked="checkBoxProperties.checked"
                :icon="checkBoxProperties.icon"
              ></DCheckBox>
            </span>
          </div>
        </div>

        <DPaging
          :initOption="pageSize"
          :start="start"
          :end="end"
          :pageCount="numEmployees"
          @goToNextPage="employeeStore.goToNextPageAsync"
          @goToPrevPage="employeeStore.goToPrevPageAsync"
          :listPageOptions="listPageOptions"
          @onPageSizeChanged="employeeStore.changePageSize"
        ></DPaging>

        <EmployeeForm
          v-if="showEmployeeForm"
          @onClose="toggleEmployeeForm"
          :title="employeeFormTitle"
        ></EmployeeForm>

        <div
          class="fixed rounded-[10px] bg-[#fff] shadow-xl p-[12px]"
          :style="{
            top: `${empFunctionBoxPosition.Top}px`,
            left: `${empFunctionBoxPosition.Left}px`,
          }"
          v-show="isShowEmpFunctionBox"
        >
          <div
            class="hover:bg-[#9CD5B3] px-[8px] rounded-[4px] cursor-pointer"
            @click="employeeStore.deleteOneEmployee(employeeDeleteId)"
          >
            Xóa
          </div>
          <div class="hover:bg-[#9CD5B3] px-[8px] rounded-[4px] cursor-pointer">
            Nhân bản
          </div>
        </div>
      </template>
    </BaseView>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { storeToRefs } from "pinia";
import BaseView from "../base/BaseView.vue";
import DPaging from "@/components/base/paging/DPaging.vue";
import { useEmployeeStore } from "./EmployeeStore";
import { useHelperStore } from "@/stores/HelperStore";
import EmployeeForm from "./EmployeeForm.vue";
const employeeStore = useEmployeeStore();
const helperStore = useHelperStore();
const {
  employeesData,
  headers,
  numEmployees,
  page,
  pageSize,
  start,
  end,
  employeeFormData,
  employeeProperty,
} = storeToRefs(employeeStore);

const listPageOptions = ref([
  { label: 10 },
  { label: 20 },
  { label: 50 },
  { label: 100 },
]);
const employeeDeleteIdRef = ref("");
const isShowEmpFunctionBox = ref(false);
const empFunctionBoxPosition = ref({
  Top: "0px",
  Left: "0px",
});

// Ref đến thẻ a để tải file excel
const aRef = ref(null);

// Xử lý thẻ checkbox
const checkBoxValue = ref(false);

// Check thẻ checkbox trên cùng
const checkBoxProperties = ref({
  checked: false,
  icon: "mdi mdi-check",
});
// EmployeeForm
const showEmployeeForm = ref(false);
const employeeFormTitle = ref("Thêm mới nhân viên");

// Loading state in Table
const tableLoading = ref(false);

/**
 * Hàm đảo ngược giá trị ô checkbox
 * Created by: nkdang 24/12/2023
 */
function checkedCheckbox() {
  checkBoxProperties.value.checked = !checkBoxProperties.value.checked;
}

/**
 * Hàm đóng mở EmployeeForm
 * Created by: nkmdang 27/12/2023
 */
function toggleEmployeeForm() {
  console.log("toggle employee form");
  showEmployeeForm.value = !showEmployeeForm.value;
}

/**
 * Hàm đóng hộp thoại chọn thao tác nhân viên
 * Created by: nkmdang 27/12/2023
 */
function hideEmployeeFunction() {
  console.log("hide");
  if (isShowEmpFunctionBox.value) {
    isShowEmpFunctionBox.value = false;
  }
}

/**
 * Hàm mở ra hộp chọn thao tác với nhân viên
 * @param {Event} event Lấy ra tọa độ của nút Sửa được bấm
 * @param {*} employeeDeleteId Id của nhân viên cần xóa
 * Created by: nkmdang 27/12/2023
 */
function showEmployeeFunction(event, employeeDeleteId) {
  event.preventDefault();
  console.log("show");
  employeeDeleteIdRef.value = employeeDeleteId;
  // console.log(employeeDeleteId);
  employeeStore.employeeDeleteId = employeeDeleteId;
  isShowEmpFunctionBox.value = true;
  empFunctionBoxPosition.value.Top =
    event.target.getBoundingClientRect().y + 20;
  empFunctionBoxPosition.value.Left =
    event.target.getBoundingClientRect().x - 78;
}

onMounted(async () => {
  tableLoading.value = true;
  await employeeStore.getEmployeeDataAsync();
  tableLoading.value = false;

  const firstTr = document.querySelectorAll(".w-table__header--sticky");
  console.log(firstTr);
  for (let i = 0; i < firstTr.length; i++) {
    firstTr[i].classList.remove("w-table__header--resizable");
  }
});
</script>

<style lang="scss" scoped></style>
