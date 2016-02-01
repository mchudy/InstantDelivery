'use strict';

/**
 * Serwis odpowiedzialny za autoryzację
 */
app.factory('authService', ['$http', '$q', 'localStorageService', 'config', function ($http, $q, localStorageService, config) {
    var CUSTOMER_ROLE = 3;

    return {
        isAuth: false,
        userName: "",

        logOut: function () {
            localStorageService.remove('authorizationData');
            this.isAuth = false;
            this.userName = "";
        },

        login: function (loginData) {
            var self = this;
            var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
            var deferred = $q.defer();
            $http.post(config.baseUri + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .then(function (response) {
                    localStorageService.set('authorizationData', {
                        token: response.data.access_token,
                        userName: loginData.userName
                    });
                    return $http.get(config.baseUri + 'account/roles');
                }, function () {
                    self.logOut();
                    return $q.reject();
                })
                .then(function (response) {
                    if (response.data[0] === CUSTOMER_ROLE) {
                        self.isAuth = true;
                        self.userName = loginData.userName;
                        deferred.resolve();
                    } else {
                        self.logOut();
                        deferred.reject();
                    }
                }, function (err) {
                    self.logOut();
                    deferred.reject(err);
                });
            return deferred.promise;
        },

        fillAuthData: function () {
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                this.isAuth = true;
                this.userName = authData.userName;
            }
        },

        signup: function (signupData) {
            this.logOut();
            return $http.post(config.baseUri + 'customers/register', signupData);
        }
    };
}]);