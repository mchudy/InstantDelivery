'use strict';

app.factory('packagesService', ['$http', 'config', function ($http, config) {
    return {
        /**
         * Used for keeping the state between sendPackage views
         */
        currentPackage: {},

        getPage: function (pageIndex, pageSize) {
            var query = "pageSize=" + pageSize + "&pageIndex=" + pageIndex;
            return $http.get(config.baseUri + 'customers/packages/page?' + query);
        },

        getCost: function(packageDto) {
            var query = "weight=" + packageDto.weight + "&height=" + packageDto.height +
                "&width=" + packageDto.width + "&length=" + packageDto.length;
            return $http.get(config.baseUri + 'packages/cost?' + query);
        }
    };
}]);