'use strict';

app.factory('sendPackageDataService', ['accountService', function (accountService) {
    var data = {
        currentPackage: {},
        userData: {}
    }

    accountService.getAddressData().then(function (response) {
        var fullName = response.data.firstName + " " + response.data.lastName;
        data.userData = {
            name: fullName,
            address: response.data.address
        }
    });
    return data;
}]);