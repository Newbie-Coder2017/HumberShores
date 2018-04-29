$(document).ready(function () {

    $("#myCarousel").carousel({
        interval: 8000
    });

    $(".left").click(function () {
        $("#myCarousel").carousel("prev");
    });
    $(".right").click(function () {
        $("#myCarousel").carousel("next");
    });

    $("#childFields").show();

    if (!$("#childSelector").is(':checked')) {
        $('#childFields').hide();
    }

    if (!$("#childSelectorEdit").is(':checked')) {
        $("#childFieldsEdit").hide();
    }

    $('#childSelector').click(function () {
        $("#childFields").toggle(this.checked);
    });

    $('#childSelectorEidt').click(function () {
        $("#childFieldsEdit").toggle(this.checked);
    });

    $(function () {
        $('#date').datepicker({
            daysOfWeekDisabled: [0,6],
            minDate: '+1D',
            format: 'DD/MM/YYYY'
        });
    });
    
    $(function () {
        $('#time').datetimepicker({
            format: 'HH:00',
            disabledHours: [0, 1, 2, 3, 4, 5, 6, 7, 8, 17, 18, 19, 20, 21, 22, 23],
            useCurrent: false,
            
        });
    });

    $(function () {
        $('#dateBirth').datepicker({
            changeYear: true,
            yearRange: '1995:2018'
        });
    });
   
});


function initMap() {
    var mapHumber = new google.maps.Map(document.getElementById('hospitalMap'), {
        zoom: 12
    });
    var addressHumber = "205 Humber College Blvd., Toronto, Ontario, Canada M9W 5L7";
    var gcoder = new google.maps.Geocoder();
    gcoder.geocode(
        { address: addressHumber },
        function (results, status) {
            if (status === 'OK') {
                mapHumber.setCenter(results[0].geometry.location);
                var marker = new google.maps.Marker({
                    position: results[0].geometry.location,
                    map: mapHumber,
                    title: 'Humber Shores Hospital'
                });
            }
        }
    );
}