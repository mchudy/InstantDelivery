'use strict';

app.controller("MainController", ['$scope', '$http', 'authService', '$location', function ($scope, $http, authService, $location) {
    if (!authService.isAuth) {
        $location.path('/login');
    }

    $scope.hideSidebar = false;

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/login');
    };

    $scope.loggedIn = function () {
        return authService.isAuth;
    }

}]);