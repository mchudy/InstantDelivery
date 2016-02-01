'use strict';

/**
 * Kontroler dla widoku statystyk
 */
app.controller('StatisticsController', ['$scope', 'packagesService', function ($scope, packagesService) {
    $scope.monthLabels = ["styczeń", "luty", "marzec", "kwiecień", "maj", "czerwiec", "lipiec",
        "sierpień", "wrzesień", "październik", "listopad", "grudzień"];
    $scope.weekLabels = ["poniedziałek", "wtorek", "środa", "czwartek", "piątek", "sobota", "niedziela"];
    $scope.series = ['Ilość paczek'];
    $scope.monthData = [[]];
    $scope.weekData = [[]];

    packagesService.getStatistics().then(function(response) {
        transformData(response.data);
    });

    function transformData(data) {
        for (var i = 0; i < data.monthStatistics.length; i++) {
            var month = data.monthStatistics[i];
            var monthNum = month.month;
            $scope.monthData[0][monthNum - 1] = month.count;
        }
        for (var j = 0; j < data.weekStatistics.length; j++) {
            var day = data.weekStatistics[j];
            var dayNum = day.day;
            $scope.weekData[0][dayNum - 1] = day.count;
        }
    }
}]);