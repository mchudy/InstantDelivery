var app = angular.module('app', ['ngRoute', 'LocalStorageModule']);

app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'MyPackages',
                controller: 'MainController'
            })
            .when('/login', {
                templateUrl: 'login',
                controller: 'LoginController'
            })
            .when('/signup', {
                templateUrl: 'Signup',
                controller: 'SignupController'
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

app.constant('config', {
    baseUri: 'https://instantdelivery.azurewebsites.net/api/'
    //baseUri: 'https://localhost:44300/'
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);