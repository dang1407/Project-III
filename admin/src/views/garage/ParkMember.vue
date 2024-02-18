<template>
  <div @click="hideParkMemberFunction">
    <BaseView>
      <template #bv-title>
        <h1>Khách hàng gửi xe</h1>
        <DButton @click="toggleParkMemberForm(formModeEnum.Create)"
          >Thêm mới khách hàng</DButton
        >
      </template>

      <template #bv-body>
        <w-toolbar>
          <div class="flex"></div>
          <div class="spacer"></div>
          <DInput
            @keydown.enter="garageStore.goToSearchMode"
            @iconClick="garageStore.goToSearchMode"
            :icon="{
              iconName: 'mdi mdi-magnify',
              iconPosition: 'right',
              iconColor: '#000',
            }"
            v-model="parkMemberProperty"
            :focus="true"
          ></DInput>

          <div class="icons__container ml-[8px]">
            <DIcon
              iconSize="24px"
              iconColor="#000"
              iconName="mdi mdi-reload"
              @click="garageStore.getParkMemberDataAsync"
            ></DIcon>
          </div>
          <div class="icons__container">
            <DIcon
              @click="
                garageStore.exportExcelCurrentPage(
                  page,
                  pageSize,
                  parkMemberProperty,
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
            :items="parkMembersData"
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
                        header.key == 'ParkMemberId' ||
                        header.key == 'functionBox',
                      // 'left-[0]': (header.key = 'ParkMemberId'),
                      // 'right-[0]': header.key == 'functionBox',
                      // 'bg-[#fff]': header.key == 'ParkMemberId',
                    },
                  ]"
                  :style="{
                    right: header.key == 'functionBox' ? '0px' : '',
                  }"
                >
                  <div
                    v-if="header.key == 'ParkMemberId'"
                    class="flex justify-center items-center"
                  >
                    <w-checkbox
                      color="green"
                      @click="
                        garageStore.selectOneParkMember(item['ParkMemberId'])
                      "
                      v-model="
                        garageStore.selectedParkMemberIdsObject[
                          item['ParkMemberId']
                        ]
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

                  <div v-else-if="header.key == 'CreatedDate'" class="pl-[8px]">
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
                        <div
                          class="text-[#0075c0] cursor-pointer"
                          @click="
                            toggleParkMemberForm(
                              formModeEnum.Modified,
                              item.ParkMemberId
                            )
                          "
                        >
                          Sửa
                        </div>

                        <div
                          @click.stop="
                            (event) =>
                              showParkMemberFunction(event, item.ParkMemberId)
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
          :pageCount="numParkMembers"
          @goToNextPage="garageStore.goToNextPageAsync"
          @goToPrevPage="garageStore.goToPrevPageAsync"
          :listPageOptions="listPageOptions"
          @onPageSizeChanged="garageStore.changePageSize"
        ></DPaging>

        <!-- <ParkMemberForm
          v-if="showParkMemberForm"
          @on-close="toggleParkMemberForm"
          :title="parkMemberFormTitle"
        ></ParkMemberForm> -->

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
            @click="garageStore.deleteOneParkMember()"
          >
            Xóa
          </div>
          <div class="hover:bg-[#9CD5B3] px-[8px] rounded-[4px] cursor-pointer">
            Nhân bản
          </div>
        </div>
      </template>
    </BaseView>

    <DForm
      v-if="showParkMemberForm"
      :title="parkMemberFormTitle"
      @onClose="toggleParkMemberForm"
    >
      <template #form_body>
        <div class="w-[900px]">
          <div class="px-[8px] flex">
            <div class="w-[210px]">
              <div class="font-bold">Ảnh đại diện:</div>
              <div class="h-[154px] rounded-[50%]">
                <img :src="avatarUrl" alt="" class="rounded-[50%] h-[154px]" />
              </div>
              <input
                id="park-member-avatar"
                @change="handleImageChange"
                ref="avatarInputFile"
                type="file"
                class="w-[0.1px] h-[0.1px] opacity-0"
              />
              <label for="park-member-avatar" class="file--upload">
                <div>
                  <DIcon
                    iconName="mdi mdi-upload"
                    iconColor="#32475c99"
                  ></DIcon>
                  Chọn ảnh từ máy tính
                  <!-- <w-icon>mdi mdi-upload-boxs</w-icon> -->
                </div>
              </label>
            </div>

            <div class="w-[calc(100%-210px)] pl-[24px]">
              <div class="flex">
                <div class="mr-[8px] w-[200px]">
                  <DInput
                    ref="parkMemberCodeInput"
                    label="Mã khách hàng"
                    :required="true"
                    v-model="parkMemberFormData.ParkMemberCode"
                  ></DInput>
                </div>
                <div class="flex w-[calc(100%-200px)]">
                  <div class="w-[100%]">
                    <DInput
                      label="Họ và tên khách hàng"
                      :required="true"
                      v-model="parkMemberFormData.FullName"
                    ></DInput>
                  </div>

                  <!-- <div class="ml-[8px] w-[calc(100%-210px)]">
                    <DInput
                      v-model="parkMemberFormData.PersonalIdentification"
                      label="Số CCCD"
                      tooltip="Số căn cước công dân"
                      required
                    ></DInput>
                  </div> -->
                </div>
              </div>
              <div class="mt-[8px] flex">
                <div class="w-[200px]">
                  <!-- <DComboBox
                    :listOptions="vehicleComboBoxOptions"
                    label="Loại phương tiện"
                    required
                    v-model="parkMemberFormData.Vehicle"
                  ></DComboBox> -->
                  <DInput
                    v-model="parkMemberFormData.PersonalIdentification"
                    label="Số CCCD"
                    tooltip="Số căn cước công dân"
                    required
                  ></DInput>
                </div>
                <div class="ml-[8px] w-[200px]">
                  <DInput
                    v-show="parkMemberFormData.Vehicle != 0"
                    label="Biển số xe"
                    required
                    v-model="parkMemberFormData.LicensePlate"
                  ></DInput>
                </div>
                <div class="ml-[8px]">
                  <div class="font-bold">
                    <span>Giới tính</span>
                  </div>
                  <div class="flex h-[36px]">
                    <div class="flex items-center mr-[4px]">
                      <DRadio
                        name="Gender"
                        value="1"
                        size="24px"
                        v-model="parkMemberFormData.Gender"
                      ></DRadio>
                      <span class="ml-[4px]">Nam</span>
                    </div>

                    <div class="flex items-center mr-[4px]">
                      <DRadio
                        name="Gender"
                        value="0"
                        size="24px"
                        v-model="parkMemberFormData.Gender"
                      ></DRadio>
                      <span class="ml-[4px]">Nữ</span>
                    </div>
                    <div class="flex items-center mr-[4px]">
                      <DRadio
                        name="Gender"
                        value="2"
                        size="24px"
                        v-model="parkMemberFormData.Gender"
                      ></DRadio>
                      <span class="ml-[4px]">Khác</span>
                    </div>
                  </div>
                </div>
              </div>
              <div class="mt-[8px]">
                <div class="w-[100%]">
                  <DInput
                    label="Địa chỉ"
                    v-model="parkMemberFormData.Address"
                  ></DInput>
                </div>
                <!-- <div class="w-[200px]"></div> -->
              </div>

              <div class="mt-[8px] flex">
                <div class="w-[200px]">
                  <DDatePicker
                    label="Ngày sinh"
                    v-model:date="parkMemberFormData.DateOfBirth"
                    dateFormat="d/m/Y"
                  ></DDatePicker>
                </div>
                <div class="ml-[8px] w-[200px]">
                  <DInput
                    label="Số điện thoại"
                    v-model="parkMemberFormData.Mobile"
                  ></DInput>
                </div>
              </div>
              <!-- <div>
                <div>

                </div>
              </div> -->
            </div>
          </div>
          <div class="flex justify-between mt-[64px]">
            <div>
              <DButton bgColor="white">Hủy</DButton>
            </div>
            <div class="flex">
              <div>
                <DButton ref="acceptButton" @click="clickSave()">{{
                  buttonText.Save
                }}</DButton>
              </div>
              <!-- <div class="ml-[8px]">
                <DButton
                  @click="clickSaveAndDuplicate(formModeEnum.Duplicate)"
                  >{{ buttonText.SaveAndDuplicate }}</DButton
                >
              </div> -->
            </div>
          </div>
        </div>
      </template>
    </DForm>

    <!-- <Dialog></Dialog> -->
  </div>
</template>

<script setup>
import { ref, onMounted, watch, reactive, nextTick } from "vue";
import { storeToRefs } from "pinia";
import BaseView from "../base/BaseView.vue";
import DPaging from "@/components/base/paging/DPaging.vue";
import { useGarageStore } from "./GarageStore";
import { useHelperStore } from "@/stores/HelperStore";
import { useNotificationStore } from "@/stores/NotificationStore";
import { resource } from "./ParkMemberResource";
import Dialog from "@/components/base/dialog/Dialog.vue";
// import ParkMemberForm from "./ParkMemberForm.vue";
// Store lưu thông tin khách hàng
const garageStore = useGarageStore();
// Helper Store để lấy thông tin ngôn ngữ, các hàm phụ trợ
const helperStore = useHelperStore();
// NotificationStore để đóng mở thông báo
const notificationStore = useNotificationStore();
// Resource theo loại ngôn ngữ được quy định ở store
const resourceLanguage = ref(resource[helperStore.language]);
const {
  parkMembersData,
  headers,
  numParkMembers,
  page,
  pageSize,
  start,
  end,
  parkMemberFormData,
  parkMemberProperty,
} = storeToRefs(garageStore);

const listPageOptions = ref([
  { label: 10 },
  { label: 20 },
  { label: 50 },
  { label: 100 },
]);
const parkMemberDeleteIdRef = ref("");
const isShowEmpFunctionBox = ref(false);
const empFunctionBoxPosition = ref({
  Top: "0px",
  Left: "0px",
});

// Xử lý form mode
const formModeEnum = ref({
  Modified: 0,
  Create: 1,
  CreateAndDuplicate: 2,
  Duplicate: 3,
});
const formMode = ref();

// Nội dung nút bấm khi sửa thông tin và tạo mới khách hàng
const buttonTextResource = ref({
  Create: "Tạo",
  CreateAndDuplicate: "Tạo và thêm mới",
  Modified: "Sửa",
  ModifiedAndDuplicate: "Sửa và thêm mới",
});
const buttonText = ref({
  Save: "",
  SaveAndDuplicate: "",
});

// Xử lý lựa chọn người dùng trên dialog
const notificationStoreRef = reactive(notificationStore);
const userSelectedEnum = notificationStore.selectedEnum;
// Watch biến lựa chọn, kiểm tra form mode là tạo hay tạo và thêm mới, đóng form khi
// tạo mà ko cần thêm mới
watch(
  () => notificationStoreRef.selectedValue,
  async (newValue) => {
    notificationStore.selectedValue = -1;
    if (
      (formMode.value == formModeEnum.value.Create ||
        formMode.value == formModeEnum.value.CreateAndDuplicate) &&
      newValue == userSelectedEnum.Accept
    ) {
      await garageStore.createNewOneParkMember();
      // Nếu đang ở trạng thái tạo mới một khách hàng thì đóng form
      if (garageStore.isExecuteSuccess) {
        if (formMode.value == formModeEnum.value.Create) {
          toggleParkMemberForm();
          await garageStore.getParkMemberDataAsync();
        }
        // Nếu ở chế độ tạo và tạo mới thì sẽ reset formData và lấy mã khách hàng mới
        else if (formMode.value == formModeEnum.value.CreateAndDuplicate) {
          await garageStore.getNewParkMemberCode();
        }
      }
    } else if (
      (formMode.value == formModeEnum.value.Modified ||
        formMode.value == formModeEnum.value.ModifiedAndDuplicate) &&
      newValue == userSelectedEnum.Accept
    ) {
      await garageStore.updateOneParkMember();
      // Nếu đang ở trạng thái sửa một khách hàng thì đóng form
      if (garageStore.isExecuteSuccess) {
        if (formMode.value == formModeEnum.value.Modified) {
          toggleParkMemberForm();
          await garageStore.getParkMemberDataAsync();
        }
        // Nếu ở chế độ sửa và tạo mới thì sẽ reset formData và lấy mã khách hàng mới
        else if (formMode.value == formModeEnum.value.ModifiedAndDuplicate) {
          await garageStore.getNewParkMemberCode();
        }
      }
    }

    if (formMode.value == formModeEnum.value.Duplicate) {
      await garageStore.getNewParkMemberCode();
      // toggleParkMemberForm();
    }
  }
);

// Ref đến thẻ a để tải file excel
const aRef = ref(null);

// Xử lý thẻ checkbox
const checkBoxValue = ref(false);

// Check thẻ checkbox trên cùng
const checkBoxProperties = ref({
  checked: false,
  icon: "mdi mdi-check",
});
// ParkMemberForm
const showParkMemberForm = ref(false);
const parkMemberFormTitle = ref("Thêm mới khách hàng");
const employeeStore = ref({});
const avatarInputFile = ref(null);
const avatarUrl = ref(
  "https://images.viblo.asia/avatar-retina/aa1cd22b-4c11-4375-92ff-16c2c18c092d.png"
);
// const parkMemberFormData = ref({});
// Loading state in Table
const tableLoading = ref(false);
// Ref đến các component cần focus
const parkMemberCodeInput = ref(null);
const acceptButton = ref(null);
const vehicleComboBoxOptions = ref([
  { label: "Xe đạp", value: 0 },
  { label: "Xe máy", value: 1 },
  { label: "Ô tô", value: 2 },
]);
/**
 * Hàm đảo ngược giá trị ô checkbox
 * Created by: nkdang 24/12/2023
 */
function checkedCheckbox() {
  checkBoxProperties.value.checked = !checkBoxProperties.value.checked;
}

/**
 * Hàm đóng mở ParkMemberForm
 * Created by: nkmdang 27/12/2023
 */
async function toggleParkMemberForm(mode, parkMemberId) {
  // Khi đóng form, gán hết các giá trị của thẻ input, form Data về rỗng
  if (showParkMemberForm.value) {
    showParkMemberForm.value = false;
    garageStore.setImageFile("");
    garageStore.parkMemberFormData = {};
    avatarInputFile.value = "";
  }
  // Khi mở form, focus vào thẻ input đầu tiên, lấy mã nhân viên mới
  else {
    formMode.value = mode;
    // Lấy mã nhân viên mới, đưa vào form nhập thông tin và focus vào thẻ Input
    if (mode == formModeEnum.value.Create) {
      buttonText.value.Save = buttonTextResource.value.Create;
      buttonText.value.SaveAndDuplicate =
        buttonTextResource.value.CreateAndDuplicate;
      garageStore.parkMemberFormData = {};
      const response = await garageStore.getNewParkMemberCode();
      showParkMemberForm.value = true;
      // reset form data về một form rỗng
      focusParkMemberCodeInput(response.data);
    }
    // Ở chế độ tạo và tạo mới thì sẽ không reset form data về rỗng
    else if (mode == formModeEnum.value.CreateAndDuplicate) {
      buttonText.value.Save = buttonTextResource.value.Create;
      buttonText.value.SaveAndDuplicate =
        buttonTextResource.value.CreateAndDuplicate;
      showParkMemberForm.value = true;
      const response = await garageStore.getNewParkMemberCode();
      // garageStore.parkMemberFormData = {};
      focusParkMemberCodeInput(response.data);
    } else if (mode == formModeEnum.value.Modified) {
      parkMemberFormTitle.value = "Cập nhật thông tin khách hàng";
      buttonText.value.Save = buttonTextResource.value.Modified;
      buttonText.value.SaveAndDuplicate =
        buttonTextResource.value.ModifiedAndDuplicate;
      const response = await garageStore.getParkMemberByParkMemberIdAsync(
        parkMemberId
      );
      if (response.data.AvatarLink) {
        avatarUrl.value = response.data.AvatarLink;
      }
      // console.log(response.data);
      garageStore.parkMemberFormData = response.data;
      if (response.data.DateOfBirth) {
        garageStore.parkMemberFormData.DateOfBirth = new Date(
          response.data.DateOfBirth
        );
      }
      showParkMemberForm.value = true;
      focusParkMemberCodeInput(garageStore.parkMemberFormData.ParkMemberCode);
    } else if (mode == formModeEnum.value.Duplicate) {
    }
  }
}

/**
 * Hàm đóng hộp thoại chọn thao tác khách hàng gửi xe
 * Created by: nkmdang 27/12/2023
 */
function hideParkMemberFunction() {
  // console.log("hide");
  if (isShowEmpFunctionBox.value) {
    isShowEmpFunctionBox.value = false;
  }
}

function handleImageChange(e) {
  avatarInputFile.value = e.target.files[0];
  avatarUrl.value = URL.createObjectURL(e.target.files[0]);
  garageStore.setImageFile(e.target.files[0]);
}

/**
 * Hàm mở ra hộp chọn thao tác với khách hàng gửi xe
 * @param {Event} event Lấy ra tọa độ của nút Sửa được bấm
 * @param {*} parkMemberDeleteId Id của khách hàng gửi xe cần xóa
 * Created by: nkmdang 27/12/2023
 */
function showParkMemberFunction(event, parkMemberDeleteId) {
  event.preventDefault();
  console.log("show");
  parkMemberDeleteIdRef.value = parkMemberDeleteId;
  // console.log(parkMemberDeleteId);
  garageStore.parkMemberDeleteId = parkMemberDeleteId;
  isShowEmpFunctionBox.value = true;
  empFunctionBoxPosition.value.Top =
    event.target.getBoundingClientRect().y + 20;
  empFunctionBoxPosition.value.Left =
    event.target.getBoundingClientRect().x - 78;
}

function createNewOneParkMember() {
  // formMode.value = mode;
  notificationStore.showDialog(
    resourceLanguage.value.Dialog.ConfirmCreateNewParkMember
  );
  // await garageStore.createNewOneParkMember();
  // // Nếu đang ở trạng thái tạo mới một khách hàng thì đóng form
  // if (garageStore.isExecuteSuccess) {
  //   if (formMode.value == formModeEnum.value.Create) {
  //     toggleParkMemberForm();
  //   }
  //   // Nếu ở chế độ tạo và tạo mới thì sẽ reset formData và lấy mã khách hàng mới
  //   else if (formMode.value == formModeEnum.value.CreateAndDuplicate) {
  //     garageStore.parkMemberFormData = {};
  //     await garageStore.getNewParkMemberCode();
  //   }
  // }
}

function clickSave(mode) {
  if (formMode.value == formModeEnum.value.Create) {
    notificationStore.showDialog(
      resourceLanguage.value.Dialog.ConfirmCreateNewParkMember
    );
  } else if (formMode.value == formModeEnum.value.Modified) {
    notificationStore.showDialog(
      resourceLanguage.value.Dialog.ConfirmUpdateParkMember(
        garageStore.parkMemberFormData.ParkMemberCode
      )
    );
  }
}

function clickSaveAndDuplicate(mode) {
  if (formMode.value == formModeEnum.value.CreateAndDuplicate) {
    notificationStore.showDialog(
      resourceLanguage.value.Dialog.ConfirmCreateNewParkMember
    );
  } else if (formMode.value == formModeEnum.value.ModifiedAndDuplicate) {
    notificationStore.showDialog(
      resourceLanguage.value.Dialog.ConfirmUpdateParkMember(
        garageStore.parkMemberFormData.ParkMemberCode
      )
    );
  }
  formMode.value = mode;
}

/**
 * Hàm focus vào thẻ input nhập ParkMemberCode
 * Cần phải nextTick để xử lý tình trạng ref bị null khi dùng v-if
 * @param {string} data
 * Created by: nkmdang 01/02/2024
 */
async function focusParkMemberCodeInput(data) {
  await nextTick();
  parkMemberCodeInput.value.setInputValue(data);
  parkMemberCodeInput.value.inputTextRef.select();
  parkMemberCodeInput.value.inputTextRef.focus();
}

/**
 * Gọi hàm lấy thông tin data của khách hàng gửi xe trong onMounted
 * Created by: nkmdang 17/01/2024
 */
onMounted(async () => {
  tableLoading.value = true;
  await garageStore.getParkMemberDataAsync();
  tableLoading.value = false;

  // Truy vấn các thẻ th được chỉ định có sticky, remove các class resizable
  // để tránh lỗi không ghim được sticky
  const firstTr = document.querySelectorAll(".w-table__header--sticky");
  // console.log(firstTr);
  for (let i = 0; i < firstTr.length; i++) {
    firstTr[i].classList.remove("w-table__header--resizable");
  }
});
</script>

<style lang="scss" scoped>
.file--upload {
  min-width: 150px;
  height: 36px;
  border: 2px dashed #ccc;
  background: #ddd;
  display: flex;
  align-items: center;
  padding-left: 8px;
  padding-right: 8px;
  border-radius: 10px;
}
</style>
