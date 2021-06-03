var angle = 0;
function galleryspin(sign) {
  spinner = document.querySelector("#spinner");

  if (!sign) {
    angle = angle + 45;
  } else {
    angle = angle - 45;
  }

  /*Nếu không có kí tự trong hàm thì nó sẽ lấy giá trị angle + || - 45 deg*/

  spinner.setAttribute(
    "style",
    "-webkit-transform: rotateY(" +
      angle +
      "deg); -moz-transform: rotateY(" +
      angle +
      "deg); transform: rotateY(" +
      angle +
      "deg);"
  );
}

/*Các kí hiệu -webkit- và -moz- là để thể hiện có thể chạy trên các trình duyêt 
	 khác , thực chất là để xoay khối spinner lệch theo các góc để hiển thị ảnh tương ứng */

/*---------------- End hết xử lý khối carousel -----------------------*/

$(document).ready(function () {
  $("#nav-icon1,#nav-icon2,#nav-icon3,#nav-icon4").click(function () {
    $(this).toggleClass("open");
  });
});

/* ------------------------  End hieuungiconclose    --------------------*/

document.addEventListener("change", function (event) {
  /*change : Sự kiện thay đổi trong form khi mất tiêu điểm */

  let element = event.target;
  /*event.target : Lấy ra thông tin tag name của phần tử bị thay đổi*/

  if (element && element.matches(".form-element-field")) {
    element.classList[element.value ? "add" : "remove"]("-hasvalue");
  }
  /*element.matches() : Trả về true hoặc false khi chon phần tử
	  Các phần tử có class được chọn , nếu có value thì add class và ngược lại*/
});

/*-------------------------  End form-effect ---------------------------*/

document.addEventListener(
  "DOMContentLoaded",
  function () {
    const button = document.querySelector(".main-circle");
    const circles = document.querySelectorAll(".circles");
    const cross = document.querySelector(".cross");
    const crossbg = document.querySelector(".bg");
    const boxicons = document.querySelectorAll(".icons svg");
    button.addEventListener("click", function () {
      cross.classList.toggle("show");
      crossbg.classList.toggle("show");

      circles.forEach((element) => {
        element.classList.toggle("show");
      });

      /*forEach : Gọi một hàm cho mối phần tử trong một mảng
		  	=> : Có thể hiểu như cái chỉ định sẽ làm việc gì */

      boxicons.forEach((element) => {
        element.classList.toggle("colorchange");
      });
    });
  },
  false
);

/*-------------------  End jellyMenu   ----------------*/
