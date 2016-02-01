'use strict';

/**
 * Główny kontroler aplikacji
 */
app.controller("MainController", ['$scope', '$http', 'authService', '$location', function ($scope, $http, authService, $location) {
    if (!authService.isAuth) {
        $location.path('/login');
    }

    $scope.hideSidebar = false;

    /**
     * Wylogowuje aktualnie zalogowanego użytkownika
     */
    $scope.logOut = function () {
        authService.logOut();
        $location.path('/login');
    };

    /**
     * Zwraca wartość oznaczającą, czy zalogowany jest w danej chwili użytkownik
     */
    $scope.loggedIn = function () {
        return authService.isAuth;
    }

}]);