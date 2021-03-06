﻿app.controller("RegisterTreatmentCtrl", function ($scope, $http) {
    $scope.$on('$viewContentLoaded', function () {
        $('#fromTime').datetimepicker();
        $('#toTime').datetimepicker();
    });

    function getData() {
        $http.get('/api/hospital/GetHospitals').then(function (data) {
            $scope.data = data.data;
            $scope.data.forEach(function (data) {
                data.hospitals.forEach(function (hospital) {
                    hospital.departments.push({ name: ''});
                });
            });
            $scope.data.forEach(function (item) {
                if (item.province == 'Bình Dương') {
                    $scope.provide = item;
                }
            });
            $scope.$watch('provide', function () {
                if ($scope.provide && $scope.provide.hospitals) {
                    $scope.hospital = $scope.provide.hospitals[0];
                }
            });
            $scope.$watch('hospital', function () {
                if ($scope.hospital && $scope.hospital.departments) {
                    $scope.department = $scope.hospital.departments[0];
                }
            });
        });
    };
    getData();

    $scope.model = {};
    $scope.submit = function () {
        $scope.model.hospitalId = $scope.hospital.id;
        $scope.model.departmentId = $scope.department.id;
        if ($scope.doctor) {
            $scope.model.DoctorId = $scope.doctor.id;
        }
        $scope.model.startTime = $('#fromTime').val();
        $scope.model.endTime = $('#toTime').val();
        $http.post('/api/appointment/requestappointment', $scope.model).then(function (data) {
            $scope.isSuccess = true;
        }, function () {
            $scope.isError = true;
        });
    };
});