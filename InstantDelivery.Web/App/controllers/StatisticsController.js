'use strict';

app.controller('StatisticsController', ['$scope', 'packagesService', function($scope, packagesService) {
    $scope.monthLabels = ["styczeń", "luty", "marzec", "kwiecień", "maj", "czerwiec", "lipiec",
        "sierpień", "wrzesień", "październik", "listopad", "grudzień"];
    $scope.series = ['Ilość paczek', 'Łączna wartość paczek'];
    $scope.data = [
      [65, 59, 80, 81, 56, 55, 40],
      [28, 48, 40, 19, 86, 27, 90]
    ];

    packagesService.getStatistics().then(function(response) {
        console.log(response);
    });
}]);