﻿
app.controller('IndexCtrl', function ($scope, $http) {
    $scope.state = 1;
    $scope.logout = function () {
        var url = '/Account/LogOff';
        $http.post(url)
            .success(function () {
                window.location = '/';
            })
            .error(function () {
                console.log('can not log out');
            });
        
    };
});