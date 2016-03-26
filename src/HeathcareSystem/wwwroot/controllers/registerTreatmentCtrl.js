app.controller("RegisterTreatmentCtrl", function ($scope) {
    $scope.$on('$viewContentLoaded', function () {
        console.log("GFDG");
        $('#fromTime').datetimepicker();
        $('#toTime').datetimepicker();
    });
});