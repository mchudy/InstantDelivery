app.controller('SignupController', ['$scope', 'authService', function($scope, authService) {
    $scope.signupData = {
        //TODO
        userName: "",
        password: "",
        confirmPassword: "",
        firstName: "",
        lastName: "",
        gender: "",
        dateOfBirth: "",
        email: "",
        address: {
            
        }
    }

    $scope.signup = function() {
        
    }
}]);