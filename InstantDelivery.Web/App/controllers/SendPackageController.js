'use strict';

/**
 * Kontroler dla widoków nadawania paczki
 */
app.controller("SendPackageController", ['$scope', '$location', 'packagesService', 'sendPackageDataService',
    function ($scope, $location, packagesService, dataService) {
        $scope.service = dataService;
        $scope.package = dataService.currentPackage;
        $scope.userData = dataService.userData;
        $scope.$watch("service", function () {
            $scope.userData = dataService.userData;
            $scope.package.sender = dataService.userData.name;
        }, true);
        $scope.message = "";

        /**
         * Przenosi do formularza z wymiarami paczki
         * @param {} isValid flaga informująca, czy formularz został poprawnie zwalidowany
         */
        $scope.moveToDimensions = function (isValid) {
            if (isValid) {
                $location.path('sendPackage/dimensions');
            } else {
                showErrors($scope.addressForm);
            }
        };

        /**
         * Przenosi do widoku podsumowania nadania paczki
         * @param {} isValid flaga informująca, czy formularz został poprawnie zwalidowany
         */
        $scope.moveToSummary = function (isValid) {
            if (isValid) {
                packagesService.getCost($scope.package).then(function (response) {
                    $scope.package.cost = response.data;
                    $location.path('sendPackage/summary');
                }, function (error) {
                    console.error(error);
                });
            } else {
                showErrors($scope.dimensionsForm);
            }
        };

        /**
         * Nadaje paczkę
         */
        $scope.sendPackage = function () {
            $scope.package.shippingAddress = $scope.package.address;
            packagesService.sendPackage($scope.package).then(function () {
                dataService.currentPackage = {};
                $location.path('myPackages');
            }, function () {
                $scope.message = "Wystąpił błąd podczas nadawania paczki";
            });
        };

        function showErrors(form) {
            angular.forEach(form.$error, function (field) {
                angular.forEach(field, function (errorField) {
                    errorField.$setTouched();
                });
            });
        }
    }]);