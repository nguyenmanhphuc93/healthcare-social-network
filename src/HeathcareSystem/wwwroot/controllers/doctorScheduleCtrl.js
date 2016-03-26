app.controller("DoctorScheduleCtrl", function ($scope, doctorScheduleService) {
    doctorScheduleService.getIncommingAppointments()
    .success(function (data) {
        $scope.appointments = data;
    })


});