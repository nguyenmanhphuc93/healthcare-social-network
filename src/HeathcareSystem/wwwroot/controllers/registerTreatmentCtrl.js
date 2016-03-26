app.controller("RegisterTreatmentCtrl", function ($scope, $http) {
    $scope.$on('$viewContentLoaded', function () {
        $('#fromTime').datetimepicker();
        $('#toTime').datetimepicker();
    });

    $scope.model = {};
    $scope.submit = function () {
        $scope.model.fromTime = $('#fromTime').val();
        $scope.model.toTime = $('#toTime').val();
        $http.post('/api/appointment/requestappointment', $scope.model).then(function (data) {
            $scope.isSuccess = true;
        }, function () {
            $scope.isError = true;
        });
    };
});