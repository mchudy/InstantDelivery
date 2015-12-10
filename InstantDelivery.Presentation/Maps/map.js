'use strict';

var map;
var infoWindow;
var geocoder;
var markers = [];
var directionsDisplay;
var directionsService;

/**
 * Inicjalizuje mapę
 */
function initMap() {
    var warsawCenter = {
        lat: 52.2333,
        lng: 21.0167
    };
    map = new google.maps.Map(document.getElementById('map'), {
        center: warsawCenter,
        zoom: 13
    });
    infoWindow = new google.maps.InfoWindow();
    geocoder = new google.maps.Geocoder();
    directionsDisplay = new google.maps.DirectionsRenderer();
    directionsService = new google.maps.DirectionsService();
}

/**
 * Wyświetla paczki na mapie
 * @param {string} packages - Tekst zawierający dane paczek w formacie JSON
 */
function showPackages(packages) {
    packages = JSON.parse(packages);
    cleanMarkers();
    directionsDisplay.setPanel(null);
    directionsDisplay.setMap(null);
    document.getElementById('directionsPanel').style.display = 'none';
    for (var i = 0; i < packages.length; i++) {
        geocodeAddress(getAddressString(packages[i]), packages[i]);
    }
}

/**
 * Wyświetla najkrótszą trasę do dostarczenia wszystkich paczek
 * @param {string} packages - Tekst zawierający dane paczek w formacie JSON
 */
function showRoute(packages) {
    packages = JSON.parse(packages);
    var waypoints = [];
    var firstPackageAddress = getAddressString(packages[0]);
    for (var i = 1; i < packages.length; i++) {
        waypoints.push({
            location: getAddressString(packages[i]),
            stopover: true
        });
    }
    document.getElementById('directionsPanel').style.display = 'block';
    directionsDisplay.setPanel(document.getElementById('directionsPanel'));
    directionsDisplay.setMap(map);
    directionsService.route({
        origin: firstPackageAddress,
        destination: firstPackageAddress,
        travelMode: google.maps.TravelMode.DRIVING,
        waypoints: waypoints,
        optimizeWaypoints: true
    }, function (response, status) {
        if (status === google.maps.DirectionsStatus.OK) {
            var route = response.routes[0];
            var order = route.waypoint_order;
            cleanMarkers();
            route.legs[0].start_address = 'Paczka do dostarczenia: ' +
                    packages[0].id + '\n' + endAddress;
            for (var i = 0; i < route.legs.length - 1; i++) {
                var currentPackage = packages[order[i]];
                var endAddress = route.legs[i].end_address;
                route.legs[i].end_address = 'Paczka do dostarczenia: ' + 
                    currentPackage.id + '\n' + endAddress;
            }
            directionsDisplay.setDirections(response);
        } else {
            window.alert('Directions request failed due to ' + status);
        }
    });
}

function geocodeAddress(address, packageData) {
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === google.maps.GeocoderStatus.OK) {
            var location = results[0].geometry.location;
            map.setCenter(location);
            showMarker(location, packageData);
        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

function showMarker(location, packageData) {
    var marker = new MarkerWithLabel({
        map: map,
        position: location,
        labelContent: packageData.id.toString(),
        labelAnchor: new google.maps.Point(22, 0),
        labelClass: 'label'
    });
    marker.addListener('click', function () {
        infoWindow.close();
        var address = packageData.shippingAddress;
        var addressString = address.street + ' ' + address.number + '<br>' + address.postalCode + ', ' + address.city;
        infoWindow.setContent('<strong>Numer paczki: ' + packageData.id.toString() + '</strong><br><br>' + addressString);
        infoWindow.open(map, marker);
    });
    markers.push(marker);
}

function getAddressString(packageData) {
    var address = packageData.shippingAddress;
    return address.street + " " + address.number + ", " + address.city;
}

function cleanMarkers() {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(null);
    }
    markers.length = 0;
}