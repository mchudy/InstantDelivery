app.controller('LoginController', ['$scope', 'authService', '$location', function ($scope, authService, $location) {
    $scope.message = "";
    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.login = function () {
        authService.login($scope.loginData).then(function () {
            $location.path('/packages');
        },
         function () {
             $scope.message = "Nieprawidłowa nazwa użytkownika lub hasło";
         });
    };
}]);