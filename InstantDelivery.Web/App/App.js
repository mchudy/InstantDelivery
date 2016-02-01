/**
 * Główny moduł aplikacji
 */
var app = angular.module('app', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar',
    'ngAnimate', 'ngMessages', 'angularUtils.directives.dirPagination', 'chart.js']);

/**
 * Konfiguracja routingu po stronie klienta
 */
app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '../App/views/myPackages.html',
            controller: 'MyPackagesController'
        })
        .when('/login', {
            templateUrl: '../App/views/login.html',
            controller: 'LoginController'
        })
        .when('/signup', {
            templateUrl: '../App/views/signup.html',
            controller: 'SignupController'
        })
        .when('/accountCreated', {
            templateUrl: '../App/views/accountCreated.html'
        })
        .when('/packages', {
            templateUrl: '../App/views/myPackages.html',
            controller: 'MyPackagesController'
        })
        .when('/profile', {
            templateUrl: '../App/views/profile.html',
            controller: 'ProfileController'
        })
        .when('/statistics', {
            templateUrl: '../App/views/statistics.html',
            controller: 'StatisticsController'
        })
        .when('/sendPackage/address', {
            templateUrl: '../App/views/sendPackageAddress.html',
            controller: 'SendPackageController'
        })
        .when('/sendPackage/dimensions', {
            templateUrl: '../App/views/sendPackageDimensions.html',
            controller: 'SendPackageController'
        })
        .when('/sendPackage/summary', {
            templateUrl: '../App/views/sendPackageSummary.html',
            controller: 'SendPackageController'
        })
        .otherwise({ redirectTo: '/' });

    $locationProvider.html5Mode(true);
}]);

/**
 * Globalne stałe aplikacji
 */
app.constant('config', {
    baseUri: 'https://instantdelivery.azurewebsites.net/api/'
    //baseUri: 'https://localhost:44300/'
});

/**
 * Wymusza aktualizację danych zalogowanego użytkownika
 */
app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

/**
 * Konfiguracja paska ładowania na górze strony
 */
app.config(['cfpLoadingBarProvider', function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = false;
    cfpLoadingBarProvider.includeBar = true;
}]);

/**
 * Konfiguracja $httpProvider, dodaje odpowiednie interceptory
 */
app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
}]);

/**
 * Konfiguracja modułu do paginacji
 */
app.config(['paginationTemplateProvider', function (paginationTemplateProvider) {
    paginationTemplateProvider.setPath('../App/templates/dirPagination.tpl.html');
}]);

/**
 * Konfiguracja Chart.js do rysowania wykresów
 */
app.config(['ChartJsProvider', function (ChartJsProvider) {
    ChartJsProvider.setOptions({
        responsive: false
    });
}])