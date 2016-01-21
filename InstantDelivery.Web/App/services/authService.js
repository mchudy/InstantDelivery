app.factory('authService', ['$http', '$q', 'localStorageService', 'config', function ($http, $q, localStorageService, config) {
    return {
        isAuth: false,
        userName: "",

        logOut: function() {
            localStorageService.remove('authorizationData');
            this.isAuth = false;
            this.userName = "";
        },

        login: function (loginData) {
            var self = this;
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
            var deferred = $q.defer();
            $http.post(config.baseUri + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .then(function(response) {
                    localStorageService.set('authorizationData', {
                        token: response.access_token,
                        userName: loginData.userName
                    });
                    self.isAuth = true;
                    self.userName = loginData.userName;
                    deferred.resolve(response);
                }, function(err) {
                    self.logOut();
                    deferred.reject(err);
                });
            return deferred.promise;
        },

        fillAuthData: function() {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                this.isAuth = true;
                this.userName = authData.userName;
            }
        },

        register: function () {
            this.logOut();
            //TODO
        }
    };
}]);