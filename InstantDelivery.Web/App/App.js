var app = angular.module('app', ['ngRoute']);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'MyPackages',
                controller: 'MainController'
            })
            .when('/login', {
                templateUrl: 'Login',
                controller: 'LoginController'
            })
            .when('/packages', {
                templateUrl: 'MyPackages',
                controller: 'MainController'
            })
            .when('/sendPackage', {
                templateUrl: 'SendPackage',
                controller: 'MainController'
            })
            .otherwise({ redirectTo: '/' });

    $locationProvider.html5Mode(true);
}]);