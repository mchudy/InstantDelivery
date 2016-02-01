'use strict';

/**
 * Serwis wykonujący zapytania do API związane z kontem użytkownika
 */
app.factory('accountService', ['$http', 'config', function ($http, config) {
    return {
        getAddressData: function () {
            return $http.get(config.baseUri + 'customers/address');
        },

        getProfileData: function () {
            return $http.get(config.baseUri + 'customers/profile');
        }
    };
}]);