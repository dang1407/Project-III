class ParkMemberAPI {
  // BEGIN PARKMEMBER ACTION
  async getParkMemberAsync() {
    try {
    } catch (error) {}
  }

  /**
   * Hàm lấy thông tin khách hàng gửi xe
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
      console.log(getUrl);
      console.log(this.page, this.pageSize);
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
      console.log(response);
      this.notificationStore.hideLoading();
    } catch (error) {
      this.notificationStore.hideLoading();
      this.start = 0;
      this.end = 0;
      this.notificationStore.showToastMessage(
        this.resourceLanguage.ToastMessage.CannotGetData
      );
    }
  }

  goToGetAllMode() {
    this.mode = "getAll";
    this.getParkMemberDataAsync();
  }

  async goToSearchMode() {
    this.mode = "search";
    await this.getParkMemberDataAsync();
  }

  /**
   * Hàm thêm mới một khách hàng gửi xe
   */
  async createNewOneParkMember() {
    try {
      this.notificationStore.showLoading();
      const formData = new FormData();
      const response = await axios.post(
        "/ParkMembers",
        this.parkMemberFormData,
        {
          headers: {
            Authorization: `Bearer ${this.userStore.accessToken}`,
          },
        }
      );
      this.notificationStore.hideLoading();
    } catch (error) {
      console.log(error);
      this.notificationStore.hideLoading();
      this.notificationStore.showToastMessage(
        this.resourceLanguage.ToastMessage.CannotCreateOne
      );
    }
  }

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
  }

  async deleteOneParkMember(parkMemberDeleteId) {
    try {
      this.notificationStore.showLoading();
      const response = await axios.delete(`ParkMembers/${parkMemberDeleteId}`, {
        headers: {
          Authorization: `Bearer ${this.userStore.accessToken}`,
        },
      });
      this.notificationStore.hideLoading();
    } catch (error) {
      this.notificationStore.hideLoading();
      console.log(error);
    }
  }

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
  }

  /**
   * Hàm chuyển sang trang tiếp theo
   * Created by: nkmdang 14/11/2023
   */
  async goToNextPageAsync() {
    if (this.page < this.numPages) {
      this.page = this.page + 1;
      await this.getParkMemberDataAsync();
    }
  }

  /**
   * Hàm quay về trang trước
   */
  async goToPrevPageAsync() {
    if (this.page > 1) {
      this.page = this.page - 1;
      await this.getParkMemberDataAsync();
    }
  }

  /**
   * Hàm tích chọn khách hàng gửi xe trong bảng
   * @param {Guid} parkMemberId
   * Created by: nkdang 12/12/2023
   */
  selectParkMember(parkMemberId) {
    this.selectedParkMembers.parkMemberId = true;
  }

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
  }

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
  }

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
  }

  /**
   * Hàm xóa một khách hàng gửi xe theo Id
   * @param {Guid - String} parkMemberId
   */
  async deleteOneParkMember(parkMemberId) {
    try {
      this.notificationStore.showLoading();
      const response = await axios.delete(`/ParkMembers/${parkMemberId}`);
      this.notificationStore.hideLoading();
    } catch (error) {
      this.notificationStore.hideLoading();
      this.start = 0;
      this.end = 0;
      this.notificationStore.showToastMessage(
        this.resourceLanguage.ToastMessage.DeleteParkMemberFailed
      );
    }
  }
}

export default new ParkMemberAPI();
