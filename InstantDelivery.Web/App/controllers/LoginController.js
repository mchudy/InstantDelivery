app.controller('LoginController', ['$scope', 'authService', function ($scope, authService) {
    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.login = function () {
        authService.login($scope.loginData).then(function () {
            $location.path('/packages');
        },
         function (err) {
             $scope.message = err.error_description;
         });
    };
}]);