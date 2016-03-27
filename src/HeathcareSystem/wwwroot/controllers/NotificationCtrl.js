app.controller("NotificationCtrl", function ($scope, $http, $stateParams) {
    var type = $stateParams.type;
    var id = $stateParams.id;

    var getRequest = function (id) {
        return $http({
            method: 'GET',
            url: '/api/MedicalRecord/GetRequestRecord?id=' + id,
        });
    }

    getRequest(id).success(function (data) { $scope.request = data; console.log(data) })

    var confirm = function (c) {
        return $http({
            method: 'PUT',
            url: '/api/MedicalRecord/ComfirmRequest?id=' + id + '&status=' + c,
        });
    }



    $scope.accept = function () {
        confirm(0);
    }

    $scope.reject = function () {
        confirm(1);
    }
});