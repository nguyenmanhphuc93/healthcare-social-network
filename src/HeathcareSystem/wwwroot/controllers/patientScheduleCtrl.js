app.controller("PatientScheduleCtrl", function ($scope) {
    $scope.$on('$viewContentLoaded', function () {
        $('#recheck').datetimepicker();
    });
});