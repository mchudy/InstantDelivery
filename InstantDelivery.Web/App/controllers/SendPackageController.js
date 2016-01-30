app.controller("SendPackageController", ['$scope', '$location', 'packagesService', function ($scope, $location, packagesService) {
    $scope.package = {};

    $scope.moveToDimensions = function() {
        $location.path('sendPackage/dimensions');
    }
}]);