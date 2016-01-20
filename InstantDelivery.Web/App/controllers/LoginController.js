app.controller('LoginController', [ '$scope', function ($scope) {
        $scope.loginData = {
            userName: "",
            password: ""
        };

        $scope.login = function () {
            console.log('Tried to login');
        }
    }
]);