angular.module("app", ['ngRoute'])
.config(function($routeProvider, $locationProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'Index',
                controller: 'MainController'
            })
            .when('/packages', {
                templateUrl: 'MyPackages',
                controller: 'MainController'
            })
            .when('/sendPackage', {
                templateUrl: 'SendPackage',
                controller: 'MainController'
            });

        $locationProvider.html5Mode(true);
    });