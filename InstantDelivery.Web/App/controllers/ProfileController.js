'use strict';

/**
 * Kontroler dla widoku profilu
 */
app.controller('ProfileController', ['$scope', 'accountService', function ($scope, accountService) {
    /**
     * Dane użytkownika
     */
    $scope.userData = {};

    /**
     * Dane potrzebne do zmiany hasła
     */
    $scope.changePasswordData = {};

    $scope.changePasswordErrorMessage = "";
    $scope.updateProfileErrorMessage = "";
    $scope.changePasswordSuccessMessage = "";
    $scope.updateProfileSuccessMessage = "";

    /**
     * Zmienia dane zalogowanego użytkownika
     */
    $scope.updateProfile = function(isValid) {
        if (isValid) {
            accountService.updateProfile($scope.userData).then(function() {
                $scope.updateProfileSuccessMessage = "Dane zostały zmienione";
                $scope.updateProfileErrorMessage = "";
            }, function() {
                $scope.updateProfileErrorMessage = "Niepoprawne dane";
                $scope.updateProfileSuccessMessage = "";
            });
        } else {
            showErrors($scope.updateProfileForm);
        }
    };

    /**
     * Zmienia hasło zalogowanego użytkownika
     */
    $scope.changePassword = function(isValid) {
        if (isValid) {
            accountService.changePassword($scope.changePasswordData).then(function() {
                $scope.changePasswordErrorMessage = "";
                $scope.changePasswordSuccessMessage = "Hasło zostało zmienione";
            }, function() {
                $scope.changePasswordErrorMessage = "Niepoprawne hasło";
                $scope.changePasswordSuccessMessage = "";
            });
        } else {
            showErrors($scope.changePasswordForm);
        }
    }

    accountService.getProfileData().then(function (response) {
        $scope.userData = response.data;
    });

    function showErrors(form) {
        angular.forEach(form.$error, function (field) {
            angular.forEach(field, function (errorField) {
                errorField.$setTouched();
            });
        });
    }
}]);