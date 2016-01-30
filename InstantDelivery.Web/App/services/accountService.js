'use strict';

app.factory('accountService', ['$http', 'config', function ($http, config) {
    return {
        getAddressData : function() {
            return $http.get(config.baseUri + 'customers/address');
        }
    };
}]);