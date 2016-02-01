/// <reference path="defaultReferences.js"/>
/// <reference path="~/App/controllers/StatisticsController.js"/>
"use strict";

describe("StatisticsController", function () {
    var controller, $scope;

    beforeEach(module("app"));

    beforeEach(inject(function ($controller, $q, $rootScope) {
        $scope = $rootScope.$new();
        var packagesServiceMock = {
            getStatistics: function () {
                var deferred = $q.defer();
                deferred.resolve({
                    data: {
                        monthStatistics: [
                            { month: 1, cost: 10, count: 1 },
                            { month: 2, cost: 20, count: 2 }
                        ],
                        weekStatistics: [
                            { day: 1, cost: 30, count: 3 },
                            { day: 2, cost: 40, count: 4 }
                        ]
                    }
                });
                return deferred.promise;
            }
        }

        controller = $controller('StatisticsController', {
            $scope: $scope,
            packagesService: packagesServiceMock

        });
    }));

    it('should start with empty data', function() {
        expect($scope.monthData).toEqual([[]]);
        expect($scope.weekData).toEqual([[]]);
    });

    it('should transform data to the chart form', function () {
        $scope.$digest();
        expect($scope.monthData).toEqual([[1, 2]]);
        expect($scope.weekData).toEqual([[3, 4]]);
    });
});