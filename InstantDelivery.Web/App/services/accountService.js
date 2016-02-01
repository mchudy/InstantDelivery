'use strict';

/**
 * Serwis wykonujący zapytania do API związane z kontem użytkownika
 */
app.factory('accountService', ['$http', 'config', function ($http, config) {
    return {
        /**
         * Zwraca dane adresowe zalogowanego klienta
         * @returns {Promise} 
         */
        getAddressData: function () {
            return $http.get(config.baseUri + 'customers/address');
        },

        /**
         * Zwraca dane profilowe zalogowanego klienta
         * @returns {Promise} 
         */
        getProfileData: function () {
            return $http.get(config.baseUri + 'customers/profile');
        },

        /**
         * Zmienia hasło zalogowanego klienta
         * @param {object} changePasswordData dane do zmiany hasła
         * @returns {Promise} 
         */
        changePassword: function(changePasswordData) {
            return $http.post(config.baseUri + 'account/changePassword', changePasswordData);
        },

        /**
         * Zmienia dane profilowe klienta
         * @param {object} userData nowe dane klienta
         * @returns {Promise} 
         */
        updateProfile: function(userData) {
            return $http.post(config.baseUri + 'customers/updateProfile', userData);
        }
    };
}]);