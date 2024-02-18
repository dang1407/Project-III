import { defineStore } from "pinia";
import axios from "@/js/axios";
import { useNotificationStore } from "@/stores/NotificationStore";
import { useHelperStore } from "@/stores/HelperStore";
import { useUserStore } from "@/stores/UserStore";
import { resource } from "./GarageResource";
import.meta.env.VITE_IMAGE_HANDLE_URL;
export const useGarageStore = defineStore("garageStore", {
  state: () => ({
    parkSlotsObject: {
      A1: [],
      A2: [],
      A3: [],
      B1: [],
      B2: [],
      B3: [],
      C1: [],
      C2: [],
      C3: [],
      C4: [],
      D1: [],
      D2: [],
      D3: [],
      D4: [],
      E1: [],
      E2: [],
      E3: [],
      F1: [],
      F2: [],
      G1: [],
      G2: [],
      H1: [],
      H2: [],
    },
    rawData: [],
    processedData: [],
    floor: "B2",
    licensePlate: "",
    imageFile: null,
    vehicleType: "",
    ticketType: 0,
    parkMemberLicensePlateToSearch: "",
    parkMemberInf: {
      ParkMemberCode: "",
      FullName: "Chưa xác định",
      PersonalIdentification: "",
      DateOfBirth: "",
      Address: "Chưa xác định",
      LicensePlate: null,
      AvatarLink: import.meta.env.VITE_ANONYMOUS_AVATAR,
      Mobile: "Chưa xác định",
      Gender: null,
      CreatedDate: "",
      CreatedBy: "",
      ModifiedDate: null,
      ModifiedBy: null,
    },
    outLicensePlate: "",
    parkingHistoryFormData: {
      CreatedDate: "",
      CreatedBy: "nkmdang",
      ModifiedDate: "",
      ModifiedBy: "",
      ParkMemberCode: "",
      Price: 0,
      LicensePlate: null,
      VehicleOutDate: null,
      VehicleType: "",
      Floor: "B2",
    },
    parkingHistoryData: {},
    isExecuteSuccess: false,
    price: {
      2: {
        0: {
          InDayBefore18: 2000,
          InDayAfter18: 3000,
          OutDay: 5000,
        },
        1: {
          InDayBefore18: 3000,
          InDayAfter18: 5000,
          OutDay: 8000,
        },
        2: {
          Hour: 5000,
        },
      },
      1: {
        0: {
          InDayBefore18: 3000,
          InDayAfter18: 4000,
          OutDay: 8000,
        },
        1: {
          InDayBefore18: 4000,
          InDayAfter18: 6000,
          OutDay: 10000,
        },
        2: {
          Hour: 8000,
        },
      },
    },
    billUrl: "",
    isCheckLicensePlateBeforeOut: false,
    // isGetBikcycleImageBeforeIn: false,

    // BEGIN PARKMEMBER STATE
    headers: [
      {
        label: "checkBox",
        key: "ParkMemberId",
        sticky: true,
        width: "50px",
        sortable: false,
      },
      {
        label: "Mã khách hàng gửi xe",
        key: "ParkMemberCode",
        width: "250px",
      },
      { label: "Họ và tên", key: "FullName", width: "200px" },
      { label: "Ngày sinh", key: "DateOfBirth", width: "220px" },
      { label: "Giới tính", key: "Gender", width: "100px" },
      { label: "Biển số xe", key: "LicensePlate", width: "150px" },
      { label: "Ngày đăng ký", key: "CreatedDate", width: "150px" },
      { label: "Địa chỉ", key: "Address", width: "350px" },
      { label: "Số điện thoại", key: "Mobile", width: "150px" },
      {
        label: "Chức năng",
        key: "functionBox",
        sticky: true,
        width: "100px",
        sortable: false,
      },
    ],

    parkMembersData: [],
    numParkMembers: 0,
    page: 1,
    pageSize: 50,
    start: 1,
    end: 50,

    // Các biến phục vụ việc chọn khách hàng gửi xe trên bảng
    selectedParkMemberIdsObject: {},
    selectedParkMemberIds: [],
    isBatchProcess: false,
    isAllPageProcess: false,
    numCurrentPageParkMemberSelected: 0,

    // Thông tin tìm kiếm khách hàng gửi xe
    parkMemberProperty: "",

    // Thông tin form data khách hàng gửi xe
    parkMemberFormData: {},

    // Id của khách hàng gửi xe để xóa
    parkMemberDeleteId: "",
    mode: "getAll",

    // END PARKMEMBER STATE
    helperStore: useHelperStore(),
    notificationStore: useNotificationStore(),
    userStore: useUserStore(),
    resourceLanguage: resource[useHelperStore().languageCode],
  }),
  getters: {},
  actions: {
    /**
     * Hàm lấy thông tin trạng thái vị trí gửi xe theo tầng
     * Created by: nkmdang 12/1/2024
     */
    async getParkSlotByFloorAsync() {
      try {
        this.notificationStore.showLoading();
        const response = await axios.get(`ParkSlots?floor=${this.floor}`, {
          headers: {
            Authorization: `Bearer ${this.userStore.accessToken}`,
          },
        });
        this.rawData = response.data;
        this.rawData = this.rawData.sort((a, b) => {
          a.ParkSlotState > b.ParkSlotState;
        });
        // console.log(this.rawData);
        this.resetParkSlotObject();
        for (let i = 0; i < this.rawData.length; i++) {
          if (this.rawData[i].ParkSlotCode) {
            const parkSlotProccessCode =
              this.rawData[i].ParkSlotCode[0] + this.rawData[i].ParkSlotCode[2];
            // console.log(parkSlotProccessCode);
            this.parkSlotsObject[parkSlotProccessCode].push(this.rawData[i]);
          }
          // if (this.rawData[i].ParkSlotCode.includes("A-1")) {
          //   this.parkSlotsObject.A1.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("A-2")) {
          //   this.parkSlotsObject.A2.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("A-3")) {
          //   this.parkSlotsObject.A3.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("B-1")) {
          //   this.parkSlotsObject.B1.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("B-2")) {
          //   this.parkSlotsObject.B2.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("B-3")) {
          //   this.parkSlotsObject.B3.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("C-1")) {
          //   this.parkSlotsObject.C1.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("C-2")) {
          //   this.parkSlotsObject.C2.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("C-3")) {
          //   this.parkSlotsObject.C3.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("C-4")) {
          //   this.parkSlotsObject.C4.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("D-1")) {
          //   this.parkSlotsObject.D1.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("D-2")) {
          //   this.parkSlotsObject.D2.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("D-3")) {
          //   this.parkSlotsObject.D3.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("D-4")) {
          //   this.parkSlotsObject.D4.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("E-1")) {
          //   this.parkSlotsObject.E1.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("E-2")) {
          //   this.parkSlotsObject.E2.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("F-1")) {
          //   this.parkSlotsObject.F1.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("F-2")) {
          //   this.parkSlotsObject.F2.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("G-1")) {
          //   this.parkSlotsObject.G1.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("G-2")) {
          //   this.parkSlotsObject.G2.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("H-1")) {
          //   this.parkSlotsObject.H1.push(this.rawData[i]);
          // } else if (this.rawData[i].ParkSlotCode.includes("H-2")) {
          //   this.parkSlotsObject.H2.push(this.rawData[i]);
          // }
        }
        // console.log(this.parkSlotsObject);
        this.notificationStore.hideLoading();
      } catch (error) {
        this.notificationStore.hideLoading();
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.CannotGetParkSlots
        );
        console.log(error);
      }
    },

    /**
     * Trả về nội dung trắng cho các thông tin vị trí gửi xe
     * Created by: nkmdang 10/1/2024
     */
    resetParkSlotObject() {
      this.parkSlotsObject = {
        A1: [],
        A2: [],
        A3: [],
        B1: [],
        B2: [],
        B3: [],
        C1: [],
        C2: [],
        C3: [],
        C4: [],
        D1: [],
        D2: [],
        D3: [],
        D4: [],
        E1: [],
        E2: [],
        E3: [],
        F1: [],
        F2: [],
        G1: [],
        G2: [],
        H1: [],
        H2: [],
      };
    },

    /**
     * Lấy thông tin biển số xe từ ảnh
     * @returns licensePlate (string or array[string])
     * Created by: nkmdang 17/1/2024
     */
    async getLicensePlateFromImage() {
      try {
        if (!this.imageFile) {
          console.log("Chưa có nội dung thông tin ảnh!");
          return;
        }
        const formData = new FormData();
        formData.append("image", this.imageFile);
        this.notificationStore.showLoading();
        const response = await axios.post(
          `${import.meta.env.VITE_IMAGE_HANDLE_URL}/predict_license_plate`,
          formData,
          {
            headers: {
              "Content-Type": "multipart/form-data",
            },
          }
        );

        // this.licensePlate = response.data.result;
        if (response.data.result.length == 1) {
          this.parkMemberLicensePlateToSearch = response.data.result[0];
          this.parkingHistoryFormData.LicensePlate = response.data.result[0];
          // this.licensePlate = response.data.result.reduce(
          //   (prevValue, curValue) => {
          //     prevValue += "," + curValue;
          //   },
          //   ""
          // );
        } else if (response.data.result.length > 1) {
          this.notificationStore.hideLoading();
          this.notificationStore.showToastMessage(
            this.resourceLanguage.ToastMessage.EnterImageHasOneLicensePlate
          );
          return;
        } else {
          console.log("Ko có thông tin biển số xe");
          this.notificationStore.showToastMessage(
            this.resourceLanguage.ToastMessage.ImageNotInclueVehicle
          );
          this.parkingHistoryFormData.LicensePlate = response.data.result;
          this.parkMemberLicensePlateToSearch = response.data.result[0];
        }

        await this.getParkMemberByLicensePlateAsync();
        this.isCheckLicensePlateBeforeOut = true;
        this.notificationStore.hideLoading();
        return response;
        // console.log(response.data.result);
      } catch (error) {
        this.notificationStore.hideLoading();
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.CannotGetLicensePlate
        );
        console.log(error);
      }
    },

    // BEGIN: Chức năng vẽ biểu đồ thống kê doanh thu
    async getParkingHistoryByOutDateAsync(time) {
      if (!time) {
        console.log(
          "Truyền thiếu tham số thời gian cho hàm lấy lịch sử gửi xe"
        );
        return;
      }
      try {
        this.notificationStore.showLoading();
        const response = await axios.get(`ParkingHistory?time=${time}`, {
          headers: {
            Authorization: `Bearer ${this.userStore.accessToken}`,
          },
        });
        this.notificationStore.hideLoading();
        return response;
      } catch (error) {
        this.notificationStore.hideLoading();
        console.log(error);
      }
    },
    // END: Chức năng vẽ biểu đồ thống kê doanh thu

    /**
     * Hàm lấy thông tin khách hàng gửi xe hội viên và đọc thông tin loại vé của khách
     * @returns parkMember Thông tin khách hàng hội viên gửi xe
     * Created by: nkmdang 17/1/2024
     */
    async getParkMemberByLicensePlateAsync() {
      // console.log("call get by license plate");
      try {
        if (!this.parkMemberLicensePlateToSearch) {
          console.log("Không có thông tin mã khách hàng gửi xe.");
          return;
        } else console.log(this.parkMemberLicensePlateToSearch);
        console.log(this.parkMemberLicensePlateToSearch);
        this.notificationStore.showLoading();
        const response = await axios.get(
          `ParkMembers?page=1&pageSize=1&parkMemberProperty=${this.parkMemberLicensePlateToSearch}`
        );
        console.log(response);
        this.notificationStore.hideLoading();
        if (response.data.data.length > 0) {
          this.parkMemberInf = response.data.data[0];
          this.ticketType = 2;
          return response;
        } else {
          this.parkMemberInf = {
            ParkMemberCode: "",
            FullName: "",
            PersonalIdentification: "",
            DateOfBirth: "",
            Address: "",
            LicensePlate: "",
            AvatarLink: import.meta.env.VITE_ANONYMOUS_AVATAR,
            Mobile: "",
            Gender: null,
            CreatedDate: "",
            CreatedBy: "",
            ModifiedDate: null,
            ModifiedBy: null,
          };
          // this.parkMemberInf = this.helperStore.processNullDataInObject(
          //   this.parkMemberInf
          // );
          this.ticketType = 1;
          this.parkMemberInf.AvatarLink = import.meta.env.VITE_ANONYMOUS_AVATAR;
          return null;
        }
      } catch (error) {
        this.notificationStore.hideLoading();
        console.log(error);
      }
    },

    /**
     * Hàm kiểm tra biển số xe và xe ra có trùng khớp hay không
     * Biển vào được fetch từ API ParkSlot, biển ra được lấy từ ảnh
     * Created by: nkmdang 23/1/2024
     */
    async checkLicensePlateWhenVehicleOutAsync() {
      const response = await this.getLicensePlateFromImage();
      console.log(response);

      if (
        response.data.result.length == 1 &&
        typeof response.data.result == "object" &&
        response.data.result[0] != this.parkingHistoryData.LicensePlate
      ) {
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.VehicleInFifferenceVehicleOut
        );
      } else {
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.CheckLisensePlateSuccess
        );
        this.isCheckLicensePlateBeforeOut = true;
      }
    },

    /**
     * Đưa xe vào bãi
     * Created by: nkmdang 23/01/2024
     */
    async createParkingHistoryAsync() {
      console.log("call create");
      if (
        !this.parkingHistoryFormData.LicensePlate &&
        this.parkingHistoryFormData.Vehicle > 0
      ) {
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.LicesePlateNotEnter
        );
        return;
      }
      this.isExecuteSuccess = false;
      try {
        this.notificationStore.showLoading();
        // for (let key in this.parkMemberInf) {
        //   this.parkingHistoryFormData[key] = this.parkMemberInf[key];
        // }
        this.parkingHistoryFormData.CreatedDate = this.getCurrentTimeString();
        this.parkingHistoryFormData.VehicleOutDate = null;
        this.parkingHistoryFormData.ParkSlotState = 1;
        this.parkingHistoryFormData.ModifiedDate =
          this.parkingHistoryFormData.CreatedDate;
        this.parkingHistoryFormData.ModifiedBy =
          import.meta.env.VITE_MODIFIED_BY;
        this.parkingHistoryFormData.ParkMemberCode =
          this.parkMemberInf.ParkMemberCode;
        console.log(this.parkingHistoryFormData);
        const response = await axios.post(
          `ParkingHistory/enterVehicle`,
          this.parkingHistoryFormData,
          {
            headers: {
              Authorization: `Bearer ${this.userStore.accessToken}`,
            },
          }
        );
        await this.getParkSlotByFloorAsync();
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.EnterVehicleSuccess
        );
        this.isExecuteSuccess = true;
        this.notificationStore.hideLoading();
      } catch (error) {
        this.notificationStore.hideLoading();
        if (error.response.status == 409) {
          this.notificationStore.showToastMessage(
            this.resourceLanguage.ToastMessage.ExistVehicleInGarage
          );
        } else {
          this.notificationStore.showToastMessage(
            this.resourceLanguage.ToastMessage.CannotCreateParkingHistory
          );
        }
        console.log(error);
      }
    },

    /**
     * Hàm lấy dữ liệu từ hàm caculatePrice để tính giá tiền và xuất mã QR cho khách hàng
     * Created by: nkmdang 24/1/2024
     */
    invoice() {
      if (!this.isCheckLicensePlateBeforeOut) {
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.LicesePlateNotCheck
        );
        return;
      }
      // this.notificationStore.showLoading();
      this.parkingHistoryData.VehicleOutDate = this.convertDateUIToDateDB(
        this.parkingHistoryData.VehicleOutDateUI
      );
      if (this.parkMemberInf?.ParkMemberCode) {
        this.ticketType = 2;
      } else {
        this.ticketType = 1;
      }
      console.log(this.parkingHistoryData.VehicleOutDate);
      this.parkingHistoryData.Price = this.caculatePrice(
        this.parkingHistoryData.CreatedDate,
        this.parkingHistoryData.VehicleOutDate,
        this.parkingHistoryData.Vehicle,
        this.ticketType
      );

      this.parkingHistoryData.ParkSlotState = 0;
      this.parkingHistoryData.Floor = this.floor;
      this.billUrl = `https://api.vietqr.io/image/970407-19036744400010-jsBXndE.jpg?accountName=NGUYEN%20KHANH%20MINH%20DANG&amount=${
        this.parkingHistoryData.Price
      }&addInfo=Chuyen%20khoan%20gui%20${this.convertVehicleTypeToUIText(
        this.parkingHistoryData.Vehicle
      )}`;
    },

    /**
     * Lấy xe ra khỏi bãi
     * Created by: nkmdang 21/1/2024
     */
    async updateParkingHistoryAsync() {
      if (!this.isCheckLicensePlateBeforeOut) {
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.LicesePlateNotCheck
        );
        return;
      }
      try {
        this.isExecuteSuccess = false;
        this.notificationStore.showLoading();

        // this.parkingHistoryData.CreatedDate =
        if (!this.validateParkingHistoryData()) {
          //   this.parkingHistoryData.CreatedDate.substring(0, 19) + "Z";{
          console.log("Lỗi ngày tháng ra ko nhỏ hơn hoặc bằng ngày tháng vào");
          this.notificationStore.showToastMessage(
            this.resourceLanguage.ToastMessage
              .VehicleOutDateNotLessThenCreatedDate
          );
          this.notificationStore.hideLoading();
          return;
        }
        console.log(this.parkingHistoryData);
        const response = await axios.put(
          `ParkingHistory/enterVehicleOut`,
          this.parkingHistoryData,
          {
            headers: {
              Authorization: `Bearer ${this.userStore.accessToken}`,
            },
          }
        );
        await this.getParkSlotByFloorAsync();
        this.isExecuteSuccess = true;
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.VehicleOutSuccess
        );
        this.notificationStore.hideLoading();
      } catch (error) {
        this.notificationStore.hideLoading();
        console.log(error);
      }
    },

    /**
     * Hàm lấy thông tin khách hàng gửi xe bằng mã khách hàng
     * @param {string} parkMemberCode
     * Created by: nkmdang 25/1/2024
     */
    async getParkMemberByParkMemberCodeAsync(parkMemberCode) {
      try {
        this.notificationStore.showLoading();
        const response = await axios.get(
          `ParkMembers/ParkMemberCode?parkMemberCode=${parkMemberCode}`,
          {
            headers: {
              Authorization: `Bearer ${this.userStore.accessToken}`,
            },
          }
        );
        if (response.data) {
          this.parkMemberInf = response.data;
          this.ticketType = 2;
        } else {
          this.ticketType = 1;
        }
        this.notificationStore.hideLoading();
      } catch (error) {
        this.notificationStore.hideLoading();
      }
    },

    async getParkMemberByParkMemberIdAsync(parkMemberId) {
      try {
        this.notificationStore.showLoading();
        const response = await axios.get(`ParkMembers/${parkMemberId}`, {
          headers: {
            Authorization: `Bearer ${this.userStore.accessToken}`,
          },
        });
        this.notificationStore.hideLoading();
        return response;
      } catch (error) {
        console.log(error);
        this.notificationStore.hideLoading();
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.CannotGetParkMemberData
        );
      }
    },

    validateParkingHistoryData() {
      const outDate = new Date(this.parkingHistoryData.VehicleOutDate);
      const inDate = new Date(this.parkingHistoryData.CreatedDate);
      if (outDate.getTime() > inDate.getTime()) {
        return true;
      } else return false;
    },

    /**
     * Hàm lấy thông tin xe đang ở trong bãi theo biển số, đặt trạng thái kiểm tra biển số trước khi xuất xe khỏi bến thành false
     * @param {string} licensePlate
     */
    async getParkingVehicleAsync(licensePlate) {
      // console.log("first");
      try {
        this.isCheckLicensePlateBeforeOut = false;
        this.notificationStore.showLoading();
        const response = await axios.get(
          `ParkingHistory/parking?licensePlate=${licensePlate}`,
          {
            headers: {
              Authorization: `Bearer ${this.userStore.accessToken}`,
            },
          }
        );
        console.log(response);
        this.parkingHistoryData = response.data.result[0];
        if (this.parkingHistoryData.ParkMemberCode) {
          this.ticketType = 2;
        } else {
          this.ticketType = 1;
        }
        if (response.data.parkMember) {
          this.parkMemberInf = response.data.parkMember;
        } else {
          this.resetParkMemberInf();
        }
        this.notificationStore.hideLoading();
      } catch (error) {
        this.notificationStore.hideLoading();
        console.log(error);
      }
    },
    caculatePrice(dateString1, dateString2, vehicle, ticketType) {
      console.log(dateString1, dateString2, vehicle, ticketType);
      if (!dateString2) {
        return;
      }
      const dateObject1 = new Date(dateString1);
      const dateObject2 = new Date(dateString2);
      if (dateObject1 >= dateObject2) {
        console.log(
          "Thời điểm xe vào không thể trùng hoặc lớn hơn thời điểm xe ra. Vui lòng kiểm tra lại cách truyền tham số."
        );
      }

      const date1 = dateObject1.getDate();
      const date2 = dateObject2.getDate();
      // Tính giá tiền cho ô tô
      if (vehicle == 2) {
        const time = dateObject2.getTime() - dateObject1.getTime();
        const hours = Math.ceil(time / (60 * 60 * 1000));
        console.log(this.price[ticketType][2]);
        return this.price[ticketType][2].Hour * hours;
        // console.log(hours);
      } else {
        if (date2 > date1) {
          return this.price[ticketType][vehicle].OutDay * (date2 - date1);
        } else {
          if (dateObject2.getHours() <= 17) {
            return this.price[ticketType][vehicle].InDayBefore18;
          } else if (
            dateObject2.getHours() >= 18 &&
            dateObject2.getHours() <= 23
          ) {
            return this.price[ticketType][vehicle].InDayAfter18;
          }
        }
      }
    },
    setImageFile(file) {
      this.imageFile = file;
      // console.log(this.imageFile);
    },

    /**
     * Hàm thêm xe đạp vào bãi, đặc thù xe đạp ko có biển số nên cần lưu ảnh để đối chiếu
     * Created by: nkmdang 23/1/2024
     */
    async upLoadBikeCycleFormDataAsync() {
      if (this.parkingHistoryFormData.Vehicle == 0 && this.imageFile) {
        this.isCheckLicensePlateBeforeOut = true;
      }
      if (!this.isCheckLicensePlateBeforeOut) {
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.LicesePlateNotEnter
        );
        return;
      }
      try {
        const formData = new FormData();
        console.log(this.imageFile);
        formData.append("VehicleInImage", this.imageFile);
        for (let key in this.parkingHistoryFormData) {
          formData.append(key, this.parkingHistoryFormData[key]);
        }

        formData.set("VehicleOutDate", "");
        formData.set("CreatedDate", this.getCurrentTimeString());
        formData.set("ParkMemberCode", this.parkMemberInf.ParkMemberCode);
        this.notificationStore.showLoading();
        this.isExecuteSuccess = false;
        for (const entry of formData.entries()) {
          const [key, value] = entry;

          console.log(`Field: ${key}, Value: ${value}`);
        }
        const respone = await axios.post(
          `ParkingHistory/enterBikecycleToGarage`,
          formData,
          {
            headers: {
              Authorization: `Bearer ${this.userStore.accessToken}`,
            },
          }
        );
        await this.getParkSlotByFloorAsync();
        this.isExecuteSuccess = true;
        this.notificationStore.hideLoading();
      } catch (error) {
        this.notificationStore.hideLoading();
        if (error.response.status == 400) {
          this.notificationStore.showToastMessage(
            this.resourceLanguage.ToastMessage.LicesePlateNotEnter
          );
        }
        console.log(error);
      }
    },

    async getBikecycleInfInGarage(parkSlotCode) {
      console.log("getBikcycle");
      try {
        this.notificationStore.showLoading();
        this.isExecuteSuccess = false;

        const response = await axios.get(
          `ParkingHistory/parking?parkSlotCode=${parkSlotCode}`,
          {
            headers: {
              Authorization: `Bearer ${this.userStore.accessToken}`,
            },
          }
        );
        this.parkingHistoryData = response.data[0];
        this.parkMemberInf = response.data[0];
        if (this.parkingHistoryData.ParkMemberCode) {
          this.ticketType = 2;
        } else {
          this.ticketType = 1;
        }
        console.log(response);
        // this.parkingHistoryData = response.data[0];
        this.notificationStore.hideLoading();
        this.isExecuteSuccess = true;
      } catch (error) {
        console.log(error);
        this.notificationStore.hideLoading();
        this.showToastMessage();
      }
    },

    setVehicleType(vehicleType) {
      this.parkingHistoryFormData.VehicleType = vehicleType;
    },
    convertVehicleTypeToUIText(vehicleType) {
      const vehicleTypeObject = ["Xe đạp", "Xe máy", "Ô tô"];
      return vehicleTypeObject[vehicleType];
    },
    convertDataToUIText(value) {
      return value ? value : "Chưa xác định";
    },

    getKeepVehiclePriceHTML(vehicle, parkMemberCode) {
      // console.log(vehicle, parkMemberCode);
      const vehicleMemberPrice = {
        0: `<span class='ml-[2px]'><span> Trong ngày, trước 18h: ${this.price[2][0].InDayBefore18}đ/lượt.</span><br /><span class='ml-[2px]'>Trong ngày, 18-24h: ${this.price[2][0].InDayAfter18}đ/lượt.</span><br /><span class='ml-[2px]'>Qua ngày: ${this.price[2][0].OutDay}đ/ngày.</span></span>`,
        1: `<span class='ml-[2px]'><span> Trong ngày, trước 18h: ${this.price[2][1].InDayBefore18}đ/lượt.</span><br /><span class='ml-[2px]'>Trong ngày, 18-24h: ${this.price[2][1].InDayAfter18}đ/lượt.</span><br /><span class='ml-[2px]'>Qua ngày: ${this.price[2][1].OutDay}đ/ngày.</span></span>`,
        2: `<span class='ml-[2px]'>${this.price[2][2].Hour}đ/tiếng</span>`,
      };
      const vehicleNotMemberPrice = {
        0: `<span class='ml-[2px]'><span> Trong ngày, trước 18h: ${this.price[1][0].InDayBefore18}đ/lượt.</span><br /><span class='ml-[2px]'>Trong ngày, 18-24h: ${this.price[1][0].InDayAfter18}đ/lượt.</span><br /><span class='ml-[2px]'>Qua ngày: ${this.price[1][0].OutDay}đ/ngày.</span></span>`,
        1: `<span class='ml-[2px]'><span> Trong ngày, trước 18h: ${this.price[1][1].InDayBefore18}đ/lượt.</span><br /><span class='ml-[2px]'>Trong ngày, 18-24h: ${this.price[1][1].InDayAfter18}đ/lượt.</span><br /><span class='ml-[2px]'>Qua ngày: ${this.price[1][1].OutDay}đ/ngày.</span></span>`,
        2: `<span class='ml-[2px]''>${this.price[1][2].Hour}đ/tiếng</span>`,
      };

      if (this.ticketType == 2) {
        return vehicleMemberPrice[vehicle];
      } else if (this.ticketType == 1) {
        return vehicleNotMemberPrice[vehicle];
      } else {
        return (
          `<div><div class="font-bold">Khách hàng hội viên:</div >  ${vehicleMemberPrice[vehicle]}</div>` +
          `<div><div class="font-bold">Khách vãng lai:</div>  ${vehicleNotMemberPrice[vehicle]}</div>`
        );
      }
    },
    resetParkingHistoryFormData() {
      this.parkingHistoryFormData = {
        CreatedDate: "",
        CreatedBy: "nkmdang",
        ModifiedDate: "",
        ModifiedBy: "",
        ParkMemberCode: "",
        Price: 0,
        LicensePlate: null,
        VehicleOutDate: null,
        VehicleType: "",
        Floor: "B2",
      };
    },
    resetParkMemberInf() {
      this.parkMemberInf = {
        ParkMemberCode: "",
        FullName: "Chưa xác định",
        PersonalIdentification: "",
        DateOfBirth: "",
        Address: "Chưa xác định",
        LicensePlate: null,
        AvatarLink: import.meta.env.VITE_ANONYMOUS_AVATAR,
        Mobile: "Chưa xác định",
        Gender: null,
        CreatedDate: "",
        CreatedBy: "",
        ModifiedDate: null,
        ModifiedBy: null,
      };
    },
    convertDateDBToCustomFormat(dateTimeString) {
      console.log(dateTimeString);
      if (dateTimeString) {
        // Lấy thông tin ngày, tháng, năm, giờ và phút
        const day = dateTimeString.substring(8, 10);
        const month = dateTimeString.substring(5, 7); // Lưu ý: Tháng bắt đầu từ 0
        const year = dateTimeString.substring(0, 4);
        const hours = dateTimeString.substring(11, 13);
        const minutes = dateTimeString.substring(14, 16);

        // Tạo định dạng mới
        const customFormat = `${day}/${month}/${year} ${hours}:${minutes}`;

        return customFormat;
      } else return "";
    },

    /**
     * Hàm chuyển đổi định dạng ngày tháng ở FE sang định dạng ngày tháng gửi cho BE
     * dd/mm/yyyy sang yyyy-mm-ddThh:mm:
     * @param {string} inputString
     * @returns
     * Created by: nkmdang 23/1/2024
     */
    convertDateUIToDateDB(inputString) {
      // Phân tích chuỗi thời gian đầu vào
      var parts = inputString.toString().split(/[\s/]+/);
      let timePart = [];
      // trong trường hợp người dùng ko chọn giờ phút thì cho bằng 00
      if (parts[3]) {
        timePart = parts[3].split(":");
      } else {
        timePart[0] = "00";
        timePart[1] = "00";
      }

      return `${parts[2]}-${parts[1]}-${parts[0]}T${timePart[0]}:${timePart[1]}:00`;
    },
    getCurrentTimeString() {
      const now = new Date();

      const year = now.getFullYear();
      const month = (now.getMonth() + 1).toString().padStart(2, "0"); // Thêm '0' phía trước nếu cần
      const day = now.getDate().toString().padStart(2, "0");
      const hours = now.getHours().toString().padStart(2, "0");
      const minutes = now.getMinutes().toString().padStart(2, "0");
      const seconds = now.getSeconds().toString().padStart(2, "0");

      return `${year}-${month}-${day}T${hours}:${minutes}:${seconds}Z`;
    },

    convertTicketType(ticketType) {
      const ticketTypeObject = {
        0: "Chưa xác định",
        1: "Khách vãng lai",
        2: "Khách hàng hội viên",
      };
      // console.log(ticketType);
      return ticketTypeObject[ticketType];
    },

    convertVehicle(vehicle) {},

    // BEGIN PARKMEMBER ACTION
    async getParkMemberAsync() {
      try {
      } catch (error) {}
    },

    /**
     * Hàm lấy thông tin khách hàng gửi xe
     * Created by: nkmdang 15/1/2024
     */
    async getParkMemberDataAsync() {
      let getUrl = "";
      if (this.mode == "getAll") {
        getUrl = `ParkMembers?page=${this.page}&pageSize=${this.pageSize}`;
      } else if (this.mode == "search") {
        getUrl = `ParkMembers?page=${this.page}&pageSize=${this.pageSize}&parkMemberProperty=${this.parkMemberProperty}`;
      }

      try {
        this.notificationStore.showLoading();
        const response = await axios.get(getUrl, {
          headers: {
            Authorization: `Bearer ${this.userStore.accessToken}`,
          },
        });
        this.parkMembersData = response.data.data;
        this.numParkMembers = response.data.countParkMembers;
        this.start = (this.page - 1) * this.pageSize + 1;
        this.end = this.start + response.data.data.length - 1;
        this.numPages = Math.ceil(this.numParkMembers / this.pageSize);
        this.numCurrentPageParkMemberSelected = 0;
        this.notificationStore.hideLoading();
      } catch (error) {
        this.notificationStore.hideLoading();
        this.start = 0;
        this.end = 0;
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.CannotGetParkMemberData
        );
      }
    },

    goToGetAllMode() {
      this.mode = "getAll";
      this.getParkMemberDataAsync();
    },

    async goToSearchMode() {
      this.mode = "search";
      await this.getParkMemberDataAsync();
    },

    /**
     * Hàm validate thông tin khách hàng để gửi đi
     */
    validateParkMemberFormData() {
      // Validate Empty Input
      let missingInputMessage = "";
      const NotEmptyFieldAndMessage = [
        {
          RequireField: "FullName",
          MissingFieldMessage: "Họ và tên không được để trống.",
        },
        {
          RequireField: "ParkMemberCode",
          MissingFieldMessage: "Mã khách hàng không được để trống.",
        },
        {
          RequireField: "PersonalIdentification",
          MissingFieldMessage: "Số CCCD không được để trống.",
        },
        {
          RequireField: "LicensePlate",
          MissingFieldMessage: "Biển số xe không được để trống.",
        },
      ];

      // Duyệt qua các trường không để trống trong parkMemberFormData
      for (let value of NotEmptyFieldAndMessage) {
        // Nếu trường đang để trống
        if (!this.parkMemberFormData[value.RequireField]) {
          missingInputMessage += value.MissingFieldMessage + "<br/>";
        }
      }

      // Nếu có trường để trống thì mở dialog
      if (missingInputMessage.length > 0) {
        this.notificationStore.showDialog(
          this.resourceLanguage.Dialog.EmptyField(missingInputMessage)
        );
        return false;
      }
      // Validate logic cho các trường dữ liệu
      let logicErrorFieldMessage = "";
      // Mã khách hàng hợp lệ
      const parkMemberCodeRegex = /PMB-00[0-4][0-9]{3}/;
      if (!parkMemberCodeRegex.test(this.parkMemberFormData.ParkMemberCode)) {
        logicErrorFieldMessage +=
          "Mã khách hàng phải có định dạng PMB-00abcd với a, b, c, d là các chữ số.<br/>";
      }
    },

    /**
     * Hàm thêm mới một khách hàng gửi xe
     * Created by: nkmdang 20/1/2024
     */
    async createNewOneParkMember() {
      try {
        this.notificationStore.showLoading();
        this.isExecuteSuccess = false;
        const formData = new FormData();
        console.log(this.parkMemberFormData);
        for (let key in this.parkMemberFormData) {
          formData.append(key, this.parkMemberFormData[key]);
        }
        if (this.parkMemberFormData.Vehicle == 0) {
          formData.set("LicensePlate", "");
        }
        if (this.parkMemberFormData.DateOfBirth) {
          formData.set(
            "DateOfBirth",
            this.convertDateUIToDateDB(this.parkMemberFormData.DateOfBirth) +
              "Z"
          );
        }
        formData.append("AvatarFile", this.imageFile);
        formData.append("CreatedDate", this.getCurrentTimeString());
        formData.append("CreatedBy", this.userStore.loginData.UserName);
        // for (const entry of formData.entries()) {
        //   const [key, value] = entry;

        //   console.log(`Field: ${key}, Value: ${value}`);
        // }

        // if (!this.validateParkMemberFormData()) {
        //   this.notificationStore.hideLoading();
        //   return;
        // }
        // const response = await axios.post("/ParkMembers", formData, {
        //   headers: {
        //     Authorization: `Bearer ${this.userStore.accessToken}`,
        //   },
        // });
        // this.isExecuteSuccess = true;
        this.notificationStore.hideLoading();
      } catch (error) {
        console.log(error);
        this.notificationStore.hideLoading();
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.CannotCreateOne
        );
      }
    },

    async updateOneParkMember() {
      try {
        this.notificationStore.showLoading();
        this.isExecuteSuccess = false;
        const formData = new FormData();
        console.log(this.parkMemberFormData);
        for (let key in this.parkMemberFormData) {
          formData.append(key, this.parkMemberFormData[key]);
        }
        if (this.parkMemberFormData.Vehicle == 0) {
          formData.set("LicensePlate", "");
        }
        if (this.parkMemberFormData.DateOfBirth) {
          formData.set(
            "DateOfBirth",
            this.convertDateUIToDateDB(this.parkMemberFormData.DateOfBirth) +
              "Z"
          );
        }
        if (this.imageFile) {
          formData.append("AvatarFile", this.imageFile);
        }
        formData.append("CreatedDate", this.getCurrentTimeString());
        formData.append("CreatedBy", this.userStore.loginData.UserName);
        // for (const entry of formData.entries()) {
        //   const [key, value] = entry;

        //   console.log(`Field: ${key}, Value: ${value}`);
        // }
        // if (!this.validateParkMemberFormData()) {
        //   return;
        // }
        const response = await axios.put(
          `ParkMembers/${this.parkMemberFormData.ParkMemberId}`,
          formData,
          {
            headers: {
              Authorization: `Bearer ${this.userStore.accessToken}`,
            },
          }
        );
        this.isExecuteSuccess = true;
        this.notificationStore.hideLoading();
      } catch (error) {
        console.log(error);
        this.notificationStore.hideLoading();
        this.notificationStore.showToastMessage();
      }
    },

    /**
     * Hàm lấy mã khách hàng gửi xe mới
     */
    async getNewParkMemberCode() {
      try {
        this.notificationStore.showLoading();
        const response = await axios.get("ParkMembers/NewParkMemberCode", {
          headers: {
            Authorization: `Bearer ${this.userStore.accessToken}`,
          },
        });
        this.parkMemberFormData.ParkMemberCode = response.data;
        this.notificationStore.hideLoading();
        return response;
      } catch (error) {
        this.notificationStore.hideLoading();
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.CannotGetNewParkMemberCode
        );
        console.log(error);
      }
    },

    // async deleteOneParkMember() {
    //   try {
    //     this.notificationStore.showLoading();
    //     const response = await axios.delete(
    //       `ParkMembers/${this.parkMemberDeleteId}`,
    //       {
    //         headers: {
    //           Authorization: `Bearer ${this.userStore.accessToken}`,
    //         },
    //       }
    //     );
    //     this.notificationStore.hideLoading();
    //   } catch (error) {
    //     this.notificationStore.hideLoading();
    //     console.log(error);
    //   }
    // },

    /**
     * Hàm thay đổi số bản ghi trong trang
     * @param {Number} newPageSize
     *
     * Created by: nkdang 2/11/2023
     */
    async changePageSize(newPageSize) {
      this.pageSize = newPageSize;
      if ((this.page - 1) * this.pageSize >= this.numLeaveDaysRequest) {
        this.page = this.page - 1;
      }
      // console.log(this.pageSize);
      await this.getParkMemberDataAsync();
      // console.log(this.processedData);
    },

    /**
     * Hàm chuyển sang trang tiếp theo
     * Created by: nkmdang 14/11/2023
     */
    async goToNextPageAsync() {
      if (this.page < this.numPages) {
        this.page = this.page + 1;
        await this.getParkMemberDataAsync();
      }
    },

    /**
     * Hàm quay về trang trước
     */
    async goToPrevPageAsync() {
      if (this.page > 1) {
        this.page = this.page - 1;
        await this.getParkMemberDataAsync();
      }
    },

    /**
     * Hàm tích chọn khách hàng gửi xe trong bảng
     * @param {Guid} parkMemberId
     * Created by: nkdang 12/12/2023
     */
    selectParkMember(parkMemberId) {
      this.selectedParkMembers.parkMemberId = true;
    },

    /**
     * Hàm nhận file excel từ backend
     * @param {Int} page
     * @param {Int} pageSize
     * @param {String} parkMemberProperty
     *
     * Created By: nkmdang 10/10/2023
     */
    async exportExcelCurrentPage(page, pageSize, parkMemberProperty, aRef) {
      // this.parkMemberPropertyExcel = this.parkMemberProperty;
      try {
        this.notificationStore.showLoading();
        const response = await axios.get(
          `ParkMembers/ParkMembersExcel?page=${page}&pageSize=${pageSize}`,
          {
            headers: {
              Authorization: `Bearer ${this.userStore.accessToken}`,
            },
            responseType: "blob",
          }
        );
        // Tạo một Blob từ dữ liệu trả về từ API
        const blob = new Blob([response.data]);

        // Tạo URL cho Blob
        const url = window.URL.createObjectURL(blob);

        // Lấy thẻ <a> tải xuống và đặt href là URL của Blob
        aRef.href = url;

        // Đặt tên tệp Excel mà bạn muốn khi người dùng tải về
        aRef.download = "Danh_sach_khach_hang_gui_xe.xlsx";

        // Simulate a click to trigger the download
        aRef.click();

        // Giải phóng URL để tránh rò rỉ bộ nhớ
        window.URL.revokeObjectURL(url);
        // console.log(response);
        this.notificationStore.hideLoading();
      } catch (error) {
        this.notificationStore.hideLoading();
        console.log(error);
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.CannotExportExcel
        );
      }
    },

    // Xử lý việc chọn các khách hàng gửi xe để thực hiện hàng loạt
    /**
     * Hàm thêm id của khách hàng gửi xe được chọn vào object các id của các khách hàng gửi xe được chọn
     * @param {Guid (String)} parkMemberId
     */
    selectOneParkMember(parkMemberId) {
      if (!this.selectedParkMemberIdsObject[parkMemberId]) {
        this.selectedParkMemberIdsObject[parkMemberId] = false;
        this.numCurrentPageParkMemberSelected -= 1;
      } else {
        this.numCurrentPageParkMemberSelected += 1;
        this.selectedParkMemberIdsObject[parkMemberId] = true;
      }
      this.handleStateProcess();
    },

    /**
     * Hàm xử lý trạng thái trong bảng là đang xử lý theo lô (không tất cả) hay là đang xử lý tất cả
     * Created by: nkdang 14/12/2023
     */
    handleStateProcess() {
      if (this.numCurrentPageParkMemberSelected < 1) {
        this.isAllPageProcess = false;
        this.isBatchProcess = false;
      } else if (
        this.numCurrentPageParkMemberSelected > 1 &&
        this.numCurrentPageParkMemberSelected < this.pageSize
      ) {
        this.isBatchProcess = true;
      } else {
        this.isBatchProcess = false;
        this.isAllPageProcess = true;
      }
    },

    /**
     * Hàm xóa một khách hàng gửi xe theo Id
     * @param {Guid - String} parkMemberId
     */
    async deleteOneParkMember() {
      try {
        console.log(this.parkMemberDeleteId);
        this.notificationStore.showLoading();
        const response = await axios.delete(
          `/ParkMembers/${this.parkMemberDeleteId}`
        );
        await this.getParkMemberDataAsync();
        this.notificationStore.hideLoading();
      } catch (error) {
        this.notificationStore.hideLoading();
        this.start = 0;
        this.end = 0;
        this.notificationStore.showToastMessage(
          this.resourceLanguage.ToastMessage.DeleteParkMemberFailed
        );
      }
    },
  },
});
