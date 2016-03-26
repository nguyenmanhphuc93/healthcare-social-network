app.controller("DoctorScheduleCtrl", function ($scope, doctorScheduleService) {
    doctorScheduleService.getIncommingAppointments()
    .success(function (data) {
        $scope.loaded = true;
        $scope.appointments = data;
        for (var i = 0; i < data.length; ++i) {
            $scope.appointments[i].time = moment($scope.appointments[i].time);
        }
    })
});