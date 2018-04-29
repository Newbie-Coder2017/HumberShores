$(document).ready(pageReady);
var container;
var mouseX;
var mouseY;


function pageReady() {
    container = $("#information"); //Grab container to output results
    $("#departments").on("change", function () {
        $("#changeDept").submit();
    });

    $("#HumberShores > g > polygon, #HumberShores > g > rect, #ParkingLot").on("click", function (event) { displaySection(event); });
    

    ////Mouseover
    //$("#HeliPad > rect, [id^='Parking'] > rect, #Entrance > polygon").on("mouseover", function (event) { floatDiv(event); });
    ////Mouseout 
    //$("#HeliPad > rect, [id^='Parking'] > rect, #Entrance > polygon").on("mouseout", removeFloatDiv);
}

function getMousePos(e) {

    mouseX = e.clientX;
    mouseY = e.clientY;
    console.log("tracking mouse");
}


////Create the contents for the Floating Div
//function floatDiv(obj) {
//    $("#HumberShores").on("mousemove", function (event) { getMousePos(event); });
//    var targetName = $(obj.target).parent().attr("id");
//    console.log(targetName);
//    switch (targetName) {
//        case "Entrance":
//            sectionId = 1;
//            break;
//        case "HeliPad":
//            sectionId = 6;
//            break;
//    }

//    if (targetName === "ParkingLot-2" || targetName === "ParkingLot-3" || targetName === "ParkingLot-4") {
//        sectionId = 2;
//    }

//    $("#temp").toggle(true);
//    $("#temp").css({
//        "top": mouseY,
//        "left": mouseX,
//        "z-index": 20
//    });
//    $.ajax({
//        url: "/departments/SectionDescription",
//        method: "POST",
//        async: true,
//        //processData: false,
//        data: "section=" + sectionId,
//        complete: function (response) {
//            $("#temp").html(response);
//            console.log("success");
//            return false;
//        }
//    });
//}

////Remove the Floating DIV from view
//function removeFloatDiv() {
//    $("#temp").toggle(false);
//    console.log("inside remove float div");
//}


function displaySection(obj) {
    console.log("Inside new function");

    var parentId = $(obj.target).parent().attr("id");
    console.log(parentId);

    //List of Property Sections on Map
    var sections = ["Entrance", "ParkingLot", "Administration", "RadiologyResearch", "Emergency", "HeliPad", "ShippingRecieving", "Surgery", "InPatients", "LongTermCare"];
    var section_id = sections.indexOf(parentId) + 1;
    //Use the order of the sections Array to provide the ID number for the search in the DB

    //Return Section Definition and Any departments within that section

    //Call DB for section 
    $.ajax({
        url: "/departments/SectionDepartments",
        method: "POST",
        async: true,
        data: { "section" : section_id },
        success: function (response) {
            container.html(response);
            console.log("success");
            return false;
        }
    });


}