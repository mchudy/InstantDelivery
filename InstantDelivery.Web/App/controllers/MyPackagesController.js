'use strict';

/**
 * Kontroler dla widoku z paczkami
 */
app.controller("MyPackagesController", ['$scope', 'packagesService', function ($scope, packagesService) {
    $scope.packages = [];
    $scope.totalPackages = 0;
    $scope.packagesPerPage = 20;
    getResultsPage(1);

    $scope.pagination = {
        current: 1
    };

    /**
     * Funkcja wywoływana podczas zmiany strony
     * @param {number} newPage numer nowej strony
     */
    $scope.pageChanged = function (newPage) {
        getResultsPage(newPage);
    };

    function getResultsPage(pageNumber) {
        packagesService.getPage(pageNumber, $scope.packagesPerPage)
            .then(function (result) {
                $scope.packages = result.data.pageCollection;
                $scope.totalPackages = result.data.pageCount * $scope.packagesPerPage;
            });
    }
}]);