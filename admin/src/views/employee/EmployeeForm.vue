<template>
  <DForm :title="title" @onClose="emits('onClose')">
    <template #form_body>
      <!-- Form Header -->
      <div class="w-[800px]">
        <!-- <div class="flex justify-between items-center mb-[16px]">
          <h1 class="text-[24px]">{{ title }}</h1>
          <div class="flex">
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
        </div> -->

        <!-- Form Body -->
        <div>
          <div class="flex justify-between">
            <div class="px-[8px] h-[200px] flex flex-col items-center w-[40%]">
              <div>
                <img
                  src="https://images.viblo.asia/avatar-retina/aa1cd22b-4c11-4375-92ff-16c2c18c092d.png"
                  alt=""
                  class="rounded-[4px] h-[154px]"
                />
              </div>

              <input type="file" class="mt-[16px] block w-[210px]" />
            </div>

            <div class="px-[8px] w-[calc(60%)]">
              <div>
                <div class="flex">
                  <div class="pr-[4px] w-[30%]">
                    <DInput
                      ref="employeeCodeInput"
                      label="Mã nhân viên"
                      :required="true"
                      v-model="employeeFormData.EmployeeCode"
                      :focus="true"
                    ></DInput>
                  </div>
                  <div class="pl-[4px] w-[70%]">
                    <DInput
                      label="Họ và tên"
                      :required="true"
                      v-model="employeeFormData.FullName"
                    ></DInput>
                  </div>
                </div>
                <div class="pt-[8px]">
                  <DComboBox
                    label="Đơn vị"
                    :required="true"
                    v-model="employeeFormData.DepartmentName"
                  ></DComboBox>
                </div>
                <div class="pt-[8px]">
                  <DInput
                    label="Chức danh"
                    v-model="employeeFormData.PositionName"
                  ></DInput>
                </div>
              </div>
            </div>
          </div>

          <div class="mt-[32px]">
            <div class="flex">
              <div class="flex justify-between px-[8px] w-[50%]">
                <div class="pr-[4px]">
                  <DDatePicker
                    label="Ngày sinh"
                    v-model="employeeFormData.DateOfBirth"
                  ></DDatePicker>
                </div>
                <div class="pl-[4px]">
                  <div class="font-bold">
                    <span>Giới tính</span>
                  </div>
                  <div class="flex h-[36px]">
                    <div class="flex items-center mr-[4px]">
                      <DRadio
                        name="Gender"
                        v-model="employeeFormData.Gender"
                        value="1"
                        size="24px"
                      ></DRadio>
                      <span class="ml-[4px]">Nam</span>
                    </div>

                    <div class="flex items-center mr-[4px]">
                      <DRadio
                        name="Gender"
                        v-model="employeeFormData.Gender"
                        value="0"
                        size="24px"
                      ></DRadio>
                      <span class="ml-[4px]">Nữ</span>
                    </div>
                    <div class="flex items-center mr-[4px]">
                      <DRadio
                        name="Gender"
                        v-model="employeeFormData.Gender"
                        value="2"
                        size="24px"
                      ></DRadio>
                      <span class="ml-[4px]">Khác</span>
                    </div>
                  </div>
                </div>
              </div>
              <div class="flex justify-between px-[8px] w-[50%]">
                <div class="pr-[4px]">
                  <DInput
                    label="Số CMND"
                    tooltip="Số chứng minh nhân dân"
                    v-model="employeeFormData.PersonalIdentification"
                  ></DInput>
                </div>
                <div class="pl-[4px]">
                  <DDatePicker
                    label="Ngày cấp"
                    v-model="employeeFormData.PICreatedDate"
                  ></DDatePicker>
                </div>
              </div>
            </div>
            <div class="px-[8px]">
              <div class="mt-[16px]">
                <DInput
                  label="Địa chỉ"
                  v-model="employeeFormData.Address"
                ></DInput>
              </div>
              <div class="flex mt-[16px]">
                <div class="w-[25%] pr-[4px]">
                  <DInput
                    label="ĐT cố định"
                    v-model="employeeFormData.LandLinePhone"
                  ></DInput>
                </div>
                <div class="w-[25%] px-[4px]">
                  <DInput
                    label="ĐT di động"
                    v-model="employeeFormData.Mobile"
                  ></DInput>
                </div>
                <div class="w-[25%] pl-[4px]">
                  <DInput
                    label="Email"
                    v-model="employeeFormData.Email"
                  ></DInput>
                </div>
              </div>

              <div class="flex mt-[16px]">
                <div class="w-[25%] pr-[4px]">
                  <DInput
                    label="Tài khoản ngân hàng"
                    v-model="employeeFormData.BankAccount"
                  ></DInput>
                </div>
                <div class="w-[25%] px-[4px]">
                  <DInput
                    label="Tên ngân hàng"
                    v-model="employeeFormData.BankName"
                  ></DInput>
                </div>
                <div class="w-[25%] pl-[4px]">
                  <DInput
                    label="Chi nhánh"
                    v-model="employeeFormData.BankBranch"
                  ></DInput>
                </div>
              </div>

              <div class="flex justify-between mt-[32px]">
                <div>
                  <w-tooltip right>
                    <template #activator="{ on }">
                      <DButton bg-color="white" v-on="on">Hủy</DButton>
                    </template>
                    Hủy ()
                  </w-tooltip>
                </div>

                <div class="flex">
                  <div class="mr-[8px]">
                    <DButton @click="employeeStore.createNewOneEmployee"
                      >Tạo</DButton
                    >
                  </div>
                  <div class="ml-[8px]">
                    <DButton>Tạo và thêm mới</DButton>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </template>
  </DForm>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { storeToRefs } from "pinia";
import { useEmployeeStore } from "./EmployeeStore";
import { useNotificationStore } from "@/stores/NotificationStore";
import DForm from "@/components/base/form/DForm.vue";
const emits = defineEmits(["onClose"]);
const props = defineProps({
  title: {
    type: String,
  },
});

const employeeStore = useEmployeeStore();
const { employeeFormData } = storeToRefs(employeeStore);

const notificationStore = useNotificationStore();
const { isShowDialog } = storeToRefs(notificationStore);

const formTitle = ref("Thông tin nhân viên");
// const employeeCode = ref("");
// const employeeFormData = ref({
//   EmployeeCode: false,
//   FullName: false,
//   Gender: false,
//   GenderName: false,
//   DateOfBirth: false,
//   PositionName: false,
//   PhoneNumber: false,
//   Email: false,
//   Address: false,
//   DepartmentName: false,
//   PersonalIdentification: false,
//   PIDateCreated: false,
//   PIPlaceCreated: false,
//   LandLinePhone: false,
//   Mobile: false,
//   BankName: false,
//   BankBranchName: false,
//   BankAccountNumber: false,
//   // departmentId: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
// });

const employeeCodeInput = ref();

onMounted(async () => {
  const response = await employeeStore.getNewEmployeeCode();
  employeeCodeInput.value?.setInputValue(response.data);
});
</script>

<style lang="scss" scoped></style>
