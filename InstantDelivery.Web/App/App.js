angular.module("app", ['ngRoute'])
.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'MyPackages',
                controller: 'MainController'
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