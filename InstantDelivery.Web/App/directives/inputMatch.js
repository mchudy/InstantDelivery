/**
 * Dyrektywa umożliwiająca sprawdzanie, czy pewne właściwości modelu na formularzu
 * są ze sobą zgodne. W przypadku braku zgodności ustawiany jest błąd walidacji 'match'
 */
app.directive('inputMatch', ['$parse', function ($parse) {
    var directiveId = 'inputMatch';
    var directive = {
        link: link,
        restrict: 'A',
        require: '?ngModel'
    };
    return directive;

    function link(scope, elem, attrs, ctrl) {
        if (!ctrl) return;
        if (!attrs[directiveId]) return;

        var firstInput = $parse(attrs[directiveId]);
        var validator = function (value) {
            var temp = firstInput(scope),
            v = value === temp;
            ctrl.$setValidity('match', v);
            return value;
        }

        ctrl.$parsers.unshift(validator);
        ctrl.$formatters.push(validator);
        attrs.$observe(directiveId, function () {
            validator(ctrl.$viewValue);
        });
    }
}]);