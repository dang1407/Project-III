// function convertDateDBToCustomFormat(dateTimeString) {
//   let dateOffset = dateTimeString.toString().split("+");
//   console.log(dateOffset);
//   dateOffset = dateOffset.toString().substring(0, 2);
//   dateOffset = parseInt(dateOffset);
//   // Chuyển đổi chuỗi thời gian thành đối tượng Date
//   const dateObject = new Date(
//     // new Date(dateTimeString) + dateOffset * 60 * 1000
//     Date.parse(dateTimeString)
//   );

//   // Lấy thông tin ngày, tháng, năm, giờ và phút
//   const day = String(dateObject.getDate()).padStart(2, "0");
//   const month = String(dateObject.getMonth() + 1).padStart(2, "0"); // Lưu ý: Tháng bắt đầu từ 0
//   const year = dateObject.getFullYear();
//   const hours = String(dateObject.getHours()).padStart(2, "0");
//   const minutes = String(dateObject.getMinutes()).padStart(2, "0");

//   // Tạo định dạng mới
//   const customFormat = `${day}/${month}/${year} ${hours}:${minutes}`;

//   return customFormat;
// }

// console.log(convertDateDBToCustomFormat("2024-01-18T17:00:00+07:00"));
// const now = new Date();

// const year = now.getFullYear();
// const month = (now.getMonth() + 1).toString().padStart(2, "0"); // Thêm '0' phía trước nếu cần
// const day = now.getDate().toString().padStart(2, "0");
// const hours = now.getHours().toString().padStart(2, "0");
// const minutes = now.getMinutes().toString().padStart(2, "0");
// const seconds = now.getSeconds().toString().padStart(2, "0");

// const formattedDateTime = `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`;

// console.log(formattedDateTime);

function convertDateUIToDateDB(inputString) {
  // Phân tích chuỗi thời gian đầu vào
  var parts = inputString.split(/[\s/]+/);
  var timePart = parts[3].split(":");

  return `${parts[2]}-${parts[1]}-${parts[0]}T${timePart[0]}:${timePart[1]}:00Z`;
}

function caculatePrice(dateString1, dateString2, vehicle, ticketType) {
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
    const hours = time / (60 * 60 * 1000);
    return {
      Vehicle: 2,
      Hours: hours,
    };
    // console.log(hours);
  } else {
    if (date2 > date1) {
      return {
        Vehicle: vehicle,
        Days: date2 - date1,
        Before18: false,
        InDay: false,
        OutDay: true,
      };
    } else {
      if (dateObject2.getHours() <= 17) {
        return {
          Vehicle: vehicle,
          Before18: true,
          InDay: true,
          OutDay: false,
        };
      } else if (dateObject2.getHours() >= 18 && dateObject2.getHours() <= 23) {
        return {
          Vehicle: vehicle,
          Before18: false,
          InDay: true,
          OutDay: false,
        };
      }
    }
  }
}

console.log(
  caculatePrice(
    "2024-01-23T13:07:32+07:00",
    "2024-01-23T17:06:15.970Z",
    0,
    null
  )
);
