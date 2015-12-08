var map;

function initMap() {
    map = new google.maps.Map(document.getElementById('map'), {
        zoom: 13
    });
}

function geocodeAddress(geocoder, address) {
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location,
            });
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

function showPackages(packages) {
    packages = JSON.parse(packages);
    var geocoder = new google.maps.Geocoder();
    for (var i = 0; i < packages.length; i++) {
        var address = packages[i].shippingAddress;
        var addressString = address.street + " " + address.number + ", " + address.city;
        geocodeAddress(geocoder, addressString);
    }
}