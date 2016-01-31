/// <reference path="defaultReferences.js"/>
/// <reference path="~/App/controllers/MainController.js"/>
"use strict";

describe("MainController", function () {
    var mainController, location;
    var authServiceMock = {
        isAuth: false
    };

    beforeEach(module("app"));

    beforeEach(inject(function ($controller, _$location_, $rootScope) {
        var scope = $rootScope.$new();
        location = _$location_;
        spyOn(location, 'path');
        mainController = $controller('MainController', {
            $location: location,
            authService: authServiceMock,
            $scope: scope
        });
    }));

    it('should redirect to login page if not authenticated', function () {
        expect(location.path).toHaveBeenCalledWith('/login');
    });
});