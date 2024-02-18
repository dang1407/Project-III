const date = new Date(1, 12, 2023);

function getDate(date) {
  const day = date.getDate();
  if (day < 10) {
    day = "0" + String(day);
  }
  console.log(typeof day);
  const month = date.getMonth() + 1;
  if (month < 10) {
    month = "0" + month;
  }
  const year = date.getFullYear();
  return `${day}/${month}/${year}`;
}

console.log(getDate(date));
