import { defineStore } from "pinia";

export const useNotificationStore = defineStore("notificationStore", {
  state: () => ({
    isShowDialog: false,
    dialogTypeEnum: {},
    dialogType: {},
    isLoading: false,
    loadingMessage: "",
    selectedValue: -1,
    selectedEnum: {
      Cancel: 0,
      Not: 1,
      Accept: 2,
    },
    dialogContent: {
      Title: "",
      Message: "",
      Icon: "",
    },
    acceptButtonThemeClass: "",
    toastContents: [],
    cancelCallBack: "",
  }),
  getters: {
    getSelection() {
      return this.selectedValue;
    },
  },
  actions: {
    /**
     * Hàm mở loading, có thể truyền một message vào cùng, message này sẽ được giải
     * phóng ở hàm hideLoading
     * @param {string} message
     */
    showLoading(message) {
      this.isLoading = true;
      this.loadingMessage = message;
    },

    hideLoading() {
      this.isLoading = false;
      this.loadingMessage = "";
    },

    /**
     * Hàm truyền nội dung vào thông báo và mở thông báo Dialog
     * @param {Object} dialogContent
     * Created by: nkmdang 22/11/2023
     */
    showDialog(dialogContent) {
      this.dialogContent = dialogContent;
      this.isShowDialog = true;
    },

    /**
     * Hàm đóng thông báo Dialog
     * @param {Object} dialogContent
     * Created by: nkmdang 22/11/2023
     */
    hideDialog() {
      this.isShowDialog = false;
    },
    /**
     * Hàm xử lý khi nhấn nút cancel trên dialog
     * Created by: nkmdang 22/11/2023
     */
    clickCancel() {
      this.isShowDialog = false;
      this.selectedValue = 0;
      // this.cancelCallBack();
    },

    /**
     * Hàm xử lý khi nhấn nút not trên dialog
     * Created by: nkmdang 22/11/2023
     */
    clickNot() {
      this.isShowDialog = false;
      this.selectedValue = 1;
    },
    /**
     * Hàm xử lý khi nhấn accept trên dialog
     * Created by: nkmdang 22/11/2023
     */
    clickAccept() {
      this.isShowDialog = false;
      this.selectedValue = 2;
    },

    /**
     * Hàm thêm ToastMessageContent vào ToastMessageGroup để mở ToastMessage
     * @param {Object} toastMessageContent
     * Created by: 22/11/2023
     */
    showToastMessage(toastMessageContent) {
      this.toastContents.push(toastMessageContent);
      // console.log(this.toastContents);
      setTimeout(() => this.clearFirstToastMessage(), 5000);
    },

    /**
     * Hàm xóa đi ToastMessage đầu tiên
     * Created by: nkdang 22/11/2023
     */
    clearFirstToastMessage() {
      if (this.toastContents.length > 0) {
        this.toastContents.shift();
      }
    },

    setCancelCallBack(callBack) {
      this.cancelCallBack = callBack;
    },
  },
});
