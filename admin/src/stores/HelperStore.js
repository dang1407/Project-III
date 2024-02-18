import { defineStore } from "pinia";
import { baseEnum } from "@/js/enum";
export const useHelperStore = defineStore("helperStore", {
  state: () => ({
    language: "VN",
    languageDictionary: ["VN", "EN"],
  }),
  getters: {
    languageCode: (state) => state.language,
  },
  actions: {
    setLanguage(language) {
      if (this.languageDictionary.includes(language)) {
        this.language = language;
      }
    },

    convertGenderDBToGenderUser(gender) {
      // console.log(gender);
      return baseEnum[this.language].Gender[gender];
    },

    /**
     * Chuyển đổi định dạng ngày từ DB yyyy-mm-dd sang dd/mm/yyyy để hiện thị ở datepicker
     * @param {String} dateDB Chuỗi nhận từ CSDL có dạng yyyy-mm-dd
     * @returns Ngày định dạng dd/mm/yyyy
     * Created by: nkmdang (03/10/2023)
     */
    covertDateDBToDatePickerDate(dateDB, datePattern) {
      if (!datePattern) {
        datePattern = "dd/MM/yyyy";
      }
      if (dateDB && dateDB !== "") {
        const year = dateDB.substring(0, 4);
        const month = dateDB.substring(5, 7);
        const date = dateDB.substring(8, 10);
        let result = datePattern;
        result = result.replace("dd", date);
        result = result.replace("yyyy", year);
        result = result.replace("MM", month);
        // return `${date}/${month}/${year}`;
        return result;
      } else {
        return "";
      }
    },

    processNullDataInObject(obj) {
      for (let key in obj) {
        if (
          !obj[key] &&
          !key.toString().includes("Date") &&
          !key.toString().includes("By")
        ) {
          obj[key] = "Không xác định";
        }
      }
      if (!obj.CreatedDate) {
        obj.CreatedDate = new Date().toISOString().split("T")[0];
      }

      if (!obj.CreatedBy) {
        obj.CreatedBy = import.meta.env.VITE_CREATED_BY;
      }
      return obj;
    },
  },
});
