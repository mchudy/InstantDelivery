app.controller('SignupController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
    $scope.message = "";
    $scope.signupData = {
        userName: "",
        password: "",
        confirmPassword: "",
        firstName: "",
        lastName: "",
        gender: "male",
        dateOfBirth: "",
        phoneNumber: "",
        email: "",
        address: {
            postalCode: "",
            street: "",
            name: "",
            state: "",
            country: "",
            number: ""
        }
    }

    $scope.signup = function (isValid) {
        if (isValid) {
            authService.signup($scope.signupData).then(function() {
                $location.path('/accountCreated');
            }, function(err) {
                $scope.message = err.data.message;
            });
        } else {
            angular.forEach($scope.signupForm.$error, function (field) {
                angular.forEach(field, function (errorField) {
                    errorField.$setTouched();
                });
            });
        }
    }
}]);