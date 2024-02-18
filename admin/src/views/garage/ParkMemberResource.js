export const resource = {
  VN: {
    ToastMessage: {
      CannotGetParkMemberData: {},
    },
    Dialog: {
      ConfirmCreateNewParkMember: {
        Title: "Tạo mới một khách hàng",
        Icon: "",
        IconColor: "",
        Message: "Bạn có chắc chắn muốn tạo mới một khách hàng không?",
      },
      ConfirmUpdateParkMember: (parkMemberCode) => ({
        Title: "Cập nhật thông tin khách hàng",
        Icon: "",
        IconColor: "",
        Message: `Bạn có chắc chắn muốn cập nhật thông tin khách hàng ${parkMemberCode} không?`,
      }),
    },
  },
  EN: {},
};
