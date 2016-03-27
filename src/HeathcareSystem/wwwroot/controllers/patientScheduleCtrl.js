app.controller("PatientScheduleCtrl", function ($scope, $http) {
    $scope.$on('$viewContentLoaded', function () {
        $('#recheck').datetimepicker();
    });


    var getIncommingPatient = function () {
        return $http({
            method: 'GET',
            url: '/api/Appointment/GetDoctorInCommingAppoitnment',
        });
    }

    var startExamination = function (exam) {
        return $http({
            method: 'POST',
            url: '/api/MedicalRecord/CreateRecord',
            data: exam
        });
    }

    var getDiseases = function () {
        return $http({
            method: 'GET',
            url: '/api/Disease/GetAll',
        });
    }

    var requestRecords = function (data) {
        return $http({
            method: 'POST',
            url: '/api/MedicalRecord/RequestRecord',
            data: data
        });
    }

    getDiseases().success(function (data) {
        $scope.diseases = data;
    })

    getIncommingPatient().success(function (data) {
        $scope.patients = data;
        for (var i = 0; i < data.length; ++i) {
            $scope.patients[i].time = moment($scope.patients[i].time);
        }
    })



    $scope.currentDate = moment().format("DD/MM/YYYY");
    //console.log($scope.currentDate)

    $scope.startExam = function (appointment) {
        $scope.curPatient = appointment;
        $scope.form = { appointmentId: appointment.id };


        startExamination($scope.form).success(function (data) {
            $scope.form.id = data;
            $scope.showPatientForm = true;
        })

    }

    function getDisease() {
        $http.get('/api/Disease/GetAll', function (data) {
            $scope.diseases = data.data;
            console.log($scope.diseases);
        });
    };
    getDisease();
    $scope.current = 0
    $scope.requests = [{ index: 0, id: 0 }];
    $scope.add = function () {
        $scope.current++;
        $scope.requests.push({ index: $scope.current, id: 0 });
    };
    $scope.remove = function (index) {
        $scope.requests.splice(index, 1);
    }

    $scope.request = function () {
        var ids = [];
        for (var i = 0; i < $scope.requests.length; ++i) {
            ids.push($scope.requests[i].id);
        }
        requestRecords({ patientId: $scope.curPatient.patient.id, diseaseIds: ids }).success(function () {
            $scope.requests = [{ index: 0, id: 0 }];
        })
    }


    var getPatientRecordService = function () {
        return $http({
            method: 'Get',
            url: '/api/MedicalRecord/GetRequestedRecordByPatient?id=' + $scope.curPatient.patient.id + '&currentRecordId=' + $scope.form.id,
        });
    }

    var getPatientRecord = function () {
        if (!$scope.form) {
            return;
        }
        getPatientRecordService().success(function (data) { $scope.medicalRecords = data });
    }

    window.setInterval(getPatientRecord, 7000);

    $scope.finish = function () {
        console.log('enter');
        $http.put('/api/MedicalRecord/FinishRecord?id=' + $scope.form.id)
            .success(function () {
                $scope.showPatientForm = false;
                $scope.patients.splice($scope.patients.indexOf($scope.curPatient),1);
            })
            .error(function () {

            });

    }
});