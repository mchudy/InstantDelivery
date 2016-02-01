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
        },

        changePassword: function(changePasswordData) {
            return $http.post(config.baseUri + 'account/changePassword', changePasswordData);
        },

        updateProfile: function(userData) {
            return $http.post(config.baseUri + 'customers/updateProfile', userData);
        }
    };
}]);