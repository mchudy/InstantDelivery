'use strict';

/**
 * Kontroler dla widoku profilu
 */
app.controller('ProfileController', ['$scope', 'accountService', function ($scope, accountService) {
    $scope.userData = {};
    accountService.getProfileData().then(function(response) {
        $scope.userData = response.data;
    });

}]);