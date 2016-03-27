app.controller('MedicalRecordCtrl', function ($scope, $http) {
    var getRecordStr = 'api/MedicalRecord/GetRecords';
    var record = {};
    $scope.post = function () {
        $http.post('api/MedicalRecord/' + $scope.input, $scope.data)
            .success(function (data) {
                record = data;
                console.log($scope.input);
                console.log(data);
            })
            .error(function (error) {
                console.log('error of ' + $scope.input);
            })
    }
    $scope.get = function () {
        $http.get('api/MedicalRecord/' + $scope.input)
            .success(function (data) {
                record = data;
                console.log($scope.input);
                console.log(data);
            })
            .error(function (error) {
                console.log('error of ' + $scope.input);
            })
    }

    var getRecord = function () {
        return $http({
            method: 'GET',
            url: '/api/MedicalRecord/GetRecords',
        });
    }

    getRecord().success(function (data) {
        $scope.records = data;
        for (var i = 0; i < $scope.records.length; ++i) {
            var datetime = new Date($scope.records[i].appointment.time);
            
            $scope.records[i].appointment.time = moment($scope.records[i].appointment.time).format('DD/MM/YYYY HH:ss:mm');
            if (datetime.getFullYear >= 1990) {
                $scope.records[i].appointment.time += '';
            }
        }
        console.log(data);
    })
});