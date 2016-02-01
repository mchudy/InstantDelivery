'use strict';

/**
 * Interceptor HTTP odpowiedzialny za autoryzację
 */
app.factory('authInterceptorService', ['$q', '$injector', '$location', 'localStorageService', function ($q, $injector, $location, localStorageService) {
    return {
        /**
         * Funkcja wywoływana przy każdym zapytaniu do API. Jeżeli użytkownik jest zalogowany
         * dopisuje nagłówek Authorization typu Bearer z tokenem aktualnie zalogowanego użytkownika
         */
        request: function (config) {
            config.headers = config.headers || {};
            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }
            return config;
        },

        /**
         * Funkcja wywoływana przy każdym zakończonym błędem zapytaniu do API. W przypadku błędu
         * autoryzacji (401) wylogowuje użytkownika i przenosi do strony logowania
         */
        responseError: function (rejection) {
            if (rejection.status === 401) {
                var authService = $injector.get('authService');
                authService.logOut();
                $location.path('/login');
            }
            return $q.reject(rejection);
        }
    };
}]);