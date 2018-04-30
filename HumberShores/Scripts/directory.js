$(document).ready(pageReady);
function pageReady() {
    console.log("fire")
    $("#depts").on("change", function () {
        //console.log("IT CHANGED");
        $("#filter_department").submit();
    });
}