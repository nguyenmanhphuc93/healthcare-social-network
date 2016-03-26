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
            method: 'GET',
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
        $scope.showPatientForm = true;

    }

    function getDisease() {
        $http.get('/api/Disease/GetAll', function (data) {
            $scope.diseases = data.data;
            console.log($scope.diseases);   
        });
    };
    getDisease();
    $scope.requests = [];
    $scope.add = function () {
        $scope.requests.push($scope.disease);
        console.log($scope.requests);
    };

});