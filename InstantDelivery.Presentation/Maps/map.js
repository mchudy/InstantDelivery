var map;

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 10
    });
    var geocoder = new google.maps.Geocoder();
    geocodeAddress(geocoder, "Aleje Jerozolimskie, Warszawa", map);
}

function geocodeAddress(geocoder, address, resultsMap) {
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            resultsMap.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: resultsMap,
                position: results[0].geometry.location
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}