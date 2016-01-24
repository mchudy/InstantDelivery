app.controller("MyPackagesController", ['$scope', 'packagesService', function ($scope, packagesService) {
    $scope.packages = [];
    $scope.totalPackages = 0;
    $scope.packagesPerPage = 2;
    getResultsPage(1);

    $scope.pagination = {
        current: 1
    };

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