app.controller("MainController", ['$scope', '$http', 'authService', '$location', function ($scope, $http, authService, $location) {
    if (!authService.isAuth) {
        $location.path('/login');
    }

    $scope.hideSidebar = false;
    $scope.title = "title";

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/login');
    };

    $scope.loggedIn = function () {
        return authService.isAuth;
    }

    $http.get('https://instantdelivery.azurewebsites.net/api/packages/1/history/').then(
     function (response) {
         $scope.title = response;
     }, function (response) {
         $scope.title = response;
     });
}]);