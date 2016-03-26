app.controller('TestCtrl', function ($scope, $http) {

    $scope.post = function () {
        $http.post('api/' + $scope.input, $scope.data)
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
        $http.get('api/' + $scope.input)
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