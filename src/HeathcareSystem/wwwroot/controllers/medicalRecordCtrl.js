app.controller('MedicalRecordCtrl', function ($scope, $http) {
    var getRecordStr = 'api/MedicalRecord/GetRecords';
    var record = {};
    $scope.call = function () {
        $http.get($scope.input)
            .success(function (data) {
                record = data;
                console.log($scope.input);
                console.log(data);
            })
            .error(function (error) {
                console.log('error of ' + $scope.input);
            })
    }
});