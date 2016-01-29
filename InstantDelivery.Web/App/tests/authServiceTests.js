/// <reference path="defaultReferences.js"/>
/// <reference path="~/App/services/authService.js"/>
"use strict";

describe("authService", function () {
    var authService;
    var httpBackend;
    var store = {};
    var localStorageMock;

    beforeEach(module("app"));

    beforeEach(function () {
        localStorageMock = {
            get: function (key) { return store[key]; },
            set: function (key, val) { store[key] = val; },
            remove: function (key) { store[key] = undefined; }
        };

        module(function ($provide) {
            $provide.value('localStorageService', localStorageMock);
        });
    });

    beforeEach(inject(function (_authService_, $httpBackend) {
        httpBackend = $httpBackend;
        authService = _authService_;
    }));

    afterEach(function () {
        httpBackend.verifyNoOutstandingExpectation();
        httpBackend.verifyNoOutstandingRequest();
        store = {};
    });

    it("given the correct credentials should save token to local storage", function () {
        httpBackend.whenPOST("https://instantdelivery.azurewebsites.net/api/token").respond({
            access_token: "aaa"
        });
        httpBackend.whenGET("https://instantdelivery.azurewebsites.net/api/account/roles").respond([3]);

        authService.login({ userName: "user", password: "password" }).then(function () {
            expect(store["authorizationData"]).toEqual({ token: "aaa", userName: "user" });
        }, function () {
            expect(error).toBeUndefined();
        });
        httpBackend.flush();
    });

    it("given the correct credentials should save token to local storage", function () {
        httpBackend.whenPOST("https://instantdelivery.azurewebsites.net/api/token").respond({
            access_token: "aaa"
        });
        httpBackend.whenGET("https://instantdelivery.azurewebsites.net/api/account/roles").respond([3]);

        authService.login({ userName: "user2", password: "password" }).then(function () {
            expect(authService.isAuth).toEqual(true);
            expect(authService.userName).toEqual("user2");
        }, function () {
            expect(error).toBeUndefined();
        });
        httpBackend.flush();
    });

    it("given the incorrect credentials should log out the user", function () {
        httpBackend.whenPOST("https://instantdelivery.azurewebsites.net/api/token").respond(400, "");
        spyOn(authService, "logOut");
        authService.login({ userName: "user", password: "password" }).then(function () {
            expect(error).toBeUndefined();
        }, function () {
            expect(authService.logOut).toHaveBeenCalled();
        });
        httpBackend.flush();
    });

    it("when user in not a customer should log out the user", function () {
        httpBackend.whenPOST("https://instantdelivery.azurewebsites.net/api/token").respond({
            access_token: "aaa"
        });
        httpBackend.whenGET("https://instantdelivery.azurewebsites.net/api/account/roles").respond([2]);
        spyOn(authService, "logOut");
        authService.login({ userName: "user", password: "password" }).then(function () {
            expect(error).toBeUndefined();
        }, function () {
            expect(authService.logOut).toHaveBeenCalled();
        });
        httpBackend.flush();
    });

    it("on log out should reset its properties", function() {
        authService.isAuth = true;
        authService.userName = "newUser";

        authService.logOut();

        expect(authService.isAuth).toEqual(false);
        expect(authService.userName).toEqual("");
    });

    it("on log out should remove token from local storage", function() {
        store["authorizationData"] = { token: "abc", userName: "user" };
        authService.logOut();
        expect(store["authorizationData"]).toBeUndefined();
    });
});