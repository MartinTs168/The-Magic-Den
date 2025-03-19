const clearButton = document.querySelector(".controls > button:nth-child(2)");
const selectedInputs = 
    document.querySelectorAll(".offcanvas-body input[type=checkbox]:checked");
clearButton.addEventListener("click", function (e) {
    selectedInputs.forEach((selectedInput) => selectedInput.checked = false);
    e.target.closest(form).submit();
});

document.querySelector("button#prev-page-btn").addEventListener("click", function () {
    let currentPageVal = parseInt(document.querySelector("#CurrentPage").value);
    document.querySelector('#CurrentPage').value = currentPageVal - 1;
});

document.querySelector("button#next-page-btn").addEventListener("click", function () {
    let currentPageVal = parseInt(document.querySelector("#CurrentPage").value);
    document.querySelector('#CurrentPage').value = currentPageVal + 1;
});