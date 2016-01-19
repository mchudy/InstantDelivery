angular.module('app').controller("MainController", function ($http) {
    var vm = this;
    vm.title = "ngtitle";

    $http.get('http://instantdelivery.azurewebsites.net/api/packages/1/history/').then(
     function (response) {
         vm.title = response;
     }, function (response) {
         vm.title = response;
     });
});