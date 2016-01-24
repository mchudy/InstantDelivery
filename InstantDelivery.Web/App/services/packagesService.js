app.factory('packagesService', ['$http', 'config', function ($http, config) {
    return {
        getPage: function (pageIndex, pageSize) {
            var query = "pageSize=" + pageSize + "&pageIndex=" + pageIndex;
            return $http.get(config.baseUri + 'customers/packages/page?' + query);
        }
    };
}]);