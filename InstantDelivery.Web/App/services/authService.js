app.factory('authService', ['$http', '$q', 'localStorageService', 'config', function ($http, $q, localStorageService, config) {
    var authServiceFactory = {};

    authServiceFactory.isAuth = false;
    authServiceFactory.userName = "";

    authServiceFactory.logOut = function () {
        localStorageService.remove('authorizationData');
        this.isAuth = false;
        this.userName = "";
    };

    authServiceFactory.login = function (loginData) {
        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
        var deferred = $q.defer();
        $http.post(config.baseUri + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .then(function (response) {
                localStorageService.set('authorizationData', {
                    token: response.access_token,
                    userName: loginData.userName
                });
                this.isAuth = true;
                this.userName = loginData.userName;
                deferred.resolve(response);
            }, function (err) {
                this.logOut();
                deferred.reject(err);
            });
        return deferred.promise;
    };

    authServiceFactory.fillAuthData = function () {
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            this.isAuth = true;
            this.userName = authData.userName;
        }
    };

    return authServiceFactory;
}]);