'use strict';

app.controller("SendPackageController", ['$scope', '$location', 'packagesService', 'sendPackageDataService',
    function ($scope, $location, packagesService, sendPackageDataService) {
        $scope.service = sendPackageDataService;
        $scope.package = sendPackageDataService.currentPackage;
        $scope.userData = sendPackageDataService.userData;
        $scope.$watch("service", function () {
            $scope.userData = sendPackageDataService.userData;
        }, true);
        $scope.message = "";

        $scope.moveToDimensions = function (isValid) {
            if (isValid) {
                $location.path('sendPackage/dimensions');
            } else {
                showErrors($scope.addressForm);
            }
        };

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

        $scope.sendPackage = function () {
            packagesService.sendPackage($scope.package).then(function () {
                sendPackageDataService.currentPackage = {};
                location.path('myPackages');
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