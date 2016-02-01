'use strict';

/**
 * Serwis wykonujący zapytania do API związane z paczkami
 */
app.factory('packagesService', ['$http', 'config', function ($http, config) {
    return {
        getPage: function (pageIndex, pageSize) {
            var query = "pageSize=" + pageSize + "&pageIndex=" + pageIndex;
            return $http.get(config.baseUri + 'customers/packages/page?' + query);
        },

        getCost: function (packageDto) {
            var query = "weight=" + packageDto.weight + "&height=" + packageDto.height +
                "&width=" + packageDto.width + "&length=" + packageDto.length;
            return $http.get(config.baseUri + 'packages/cost?' + query);
        },

        sendPackage: function (packageDto) {
            return $http.post(config.baseUri + 'customers/sendPackage', packageDto);
        },

        getStatistics: function () {
            return $http.get(config.baseUri + 'customers/packages/statistics');
        }
    };
}]);