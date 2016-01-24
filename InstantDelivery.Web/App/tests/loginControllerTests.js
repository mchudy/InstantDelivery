///<reference path="~/Scripts/angular.js"/>
///<reference path="~/Scripts/angular-mocks.js"/>
///<reference path="~/Scripts/jasmine/jasmine.js"/>
///<reference path="~/Scripts/jasmine/jasmine-html.js"/>
///<reference path="~/Scripts/jasmine/boot.js"/>
///<reference path="~/App/app.js"/>
///<reference path="~/App/services/authInterceptorService.js"/>
'use strict';

describe('Login Controller', function () {
    var authServiceMock = {};
    beforeEach(function () {
        //module('app', function ($provide) {
        //    $provide.value('authService', authServiceMock);
        //});

        //inject(function($q) {
        //    authServiceMock.login = function() {
        //        var defer = $q.defer();
        
        //        defer.resolve(this.data);
        
        //        return defer.promise;
        //    };
      
        //    authServiceMock.create = function(name) {
        //        var defer = $q.defer();
        
        //        var id = this.data.length;
        
        //        var item = {
        //            id: id,
        //            name: name
        //        };
        
        //        this.data.push(item);
        //        defer.resolve(item);
        
        //        return defer.promise;
        //    };
        //});
    });

    it('should redirect to my packages page on successful login', function () {
        spyOn($location, 'path');
        scope.login();
        expect($location.path).toHaveBeenCalledWith('/login');
    });
});