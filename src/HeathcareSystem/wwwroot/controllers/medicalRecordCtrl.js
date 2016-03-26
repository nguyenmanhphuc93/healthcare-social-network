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
});