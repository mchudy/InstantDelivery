/// <reference path="defaultReferences.js"/>
/// <reference path="~/App/services/sendPackageDataService.js"/>
/// <reference path="~/App/controllers/SendPackageController.js"/>
"use strict";

describe("SendPackagesController", function () {
    var controller;
    var location;
    var scope;
    var $rootScope;

    beforeEach(module("app"));

    beforeEach(inject(function ($controller, _$location_, _$rootScope_, $q) {
        var packagesServiceMock = {
            getCost: function () {
                var deferred = $q.defer();
                deferred.resolve({data: 1});
                return deferred.promise;
            }
        };

        var dataServiceMock = {
            currentPackage: {},
            userData: {name: "John", address: {} }
        };

        $rootScope = _$rootScope_;
        scope = $rootScope.$new();
        scope.addressForm = {};

        location = _$location_;
        spyOn(location, 'path');

        controller = $controller('SendPackageController', {
            $location: location,
            packagesService: packagesServiceMock,
            sendPackageDataService: dataServiceMock,
            $scope: scope
        });
    }));

    it('given valid data on the address form should redirect to the dimensions form', function () {
        scope.moveToDimensions(true);
        expect(location.path).toHaveBeenCalledWith('sendPackage/dimensions');
    });

    it('given invalid data on the address form should stay on the same page', function () {
        scope.moveToDimensions(false);
        expect(location.path).not.toHaveBeenCalled();
    });

    it('gived valid data on the dimensions form should update the package cost', function () {
        scope.moveToSummary(true);
        $rootScope.$digest();
        expect(scope.package.cost).toEqual(1);
    });

});