app.controller("MainController", ['$scope', '$http', function ($scope, $http) {
        $scope.title = "title";

        $http.get('https://instantdelivery.azurewebsites.net/api/packages/1/history/').then(
         function (response) {
             $scope.title = response;
         }, function (response) {
             $scope.title = response;
         });
    }]);