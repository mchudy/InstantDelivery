'use strict';

/**
 * Serwis wykonujący zapytania do API związane z paczkami
 */
app.factory('packagesService', ['$http', 'config', function ($http, config) {
    return {
        /**
         * Zwraca stronę paczek zalogowanego klienta
         * @param {number} pageIndex 
         * @param {number} pageSize 
         * @returns {Promise} 
         */
        getPage: function (pageIndex, pageSize) {
            var query = "pageSize=" + pageSize + "&pageIndex=" + pageIndex;
            return $http.get(config.baseUri + 'customers/packages/page?' + query);
        },

        /**
         * Zwraca koszt paczki o podanej wadze i wymiarach
         * @param {} packageDto 
         * @returns {Promise} 
         */
        getCost: function (packageDto) {
            var query = "weight=" + packageDto.weight + "&height=" + packageDto.height +
                "&width=" + packageDto.width + "&length=" + packageDto.length;
            return $http.get(config.baseUri + 'packages/cost?' + query);
        },

        /**
         * Nadaje paczkę
         * @param {object} packageDto 
         * @returns {Promise} 
         */
        sendPackage: function (packageDto) {
            return $http.post(config.baseUri + 'customers/sendPackage', packageDto);
        },

        /**
         * Zwraca dane statystyczne o paczkach zalogowanego klienta
         * @returns {Promise} 
         */
        getStatistics: function () {
            return $http.get(config.baseUri + 'customers/packages/statistics');
        }
    };
}]);