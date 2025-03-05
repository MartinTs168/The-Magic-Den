const clearButton = document.querySelector(".controls > button:nth-child(2)");
const selectedInputs = document.querySelectorAll(".offcanvas-body input[type=checkbox]:checked");
clearButton.addEventListener("click", function (e) {
    selectedInputs.forEach((selectedInput) => selectedInput.checked = false);
    e.target.closest(form).submit();
});