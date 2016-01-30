app.controller("SendPackageController", ['$scope', '$location', 'packagesService', function ($scope, $location, packagesService) {
    $scope.package = packagesService.currentPackage;

    $scope.moveToDimensions = function (isValid) {
        if (isValid) {
            packagesService.currentPackage = $scope.package;
            $location.path('sendPackage/dimensions');
        } else {
            showErrors($scope.addressForm);
        }
    }

    $scope.moveToSummary = function() {
        packagesService.currentPackage = $scope.package;
        $location.path('sendPackage/summary');
    }

    function showErrors(form) {
        angular.forEach(form.$error, function (field) {
            angular.forEach(field, function (errorField) {
                errorField.$setTouched();
            });
        });
    }
}]);