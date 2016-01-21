app.controller('SignupController', ['$scope', 'authService', function($scope, authService) {
    $scope.signupData = {
        //TODO
        userName: "",
        password: "",
        firstName: "",
        lastName: "",
        gender: "",
        dateOfBirth: "",
        email: ""
    }

    $scope.signup = function() {
        
    }
}]);