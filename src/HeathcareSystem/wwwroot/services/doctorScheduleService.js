app.service("doctorScheduleService", function ($http) {
    this.getIncommingAppointments = function () {
        return $http({
            method: 'GET',
            url: '/api/Appointment/GetPattientInCommingAppointment',
        });
    }
})