app.controller('StatisticCtrl', function ($scope, $http) {

    google.charts.load('current', { 'packages': ['corechart'] });
    google.charts.setOnLoadCallback(drawChart);
    function drawChart() {

        $http.get('/api/statistic/GetProvinces').then(function (res) {
            var diseases = [];
            if (res.data) {
                $scope.diseases = res.data.diseases;
                res.data.diseases.forEach(function (disease) {
                    diseases.push([disease.name, disease.ammount]);
                });
            }
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Diseases');
            data.addColumn('number', 'Amount');
            data.addRows(diseases);
            var options = {
                'title': 'Statistics diseases: ' + res.data.name,
                'width': 800,
                'height': 400
            };

            draw(data, options, 'chart_div1');

            var chart1 = new google.visualization.PieChart(document.getElementById('chart_div1'));
            chart1.draw(data, options);

        }, function () {

        });

        $http.get('/api/statistic/GetDiceases').then(function (res) {
            var districts = [];
            if (res.data) {
                $scope.districts = res.data.provinces;
                res.data.provinces.forEach(function (district) {
                    districts.push([district.name, district.ammount]);
                });
            }
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Districts');
            data.addColumn('number', 'Amount');
            data.addRows(districts);
            var options = {
                'title': 'Statistics diseases: ' + res.data.name,
                'width': 800,
                'height': 400
            };

            draw(data, options, 'chart_div2');
            // Instantiate and draw our chart, passing in some options.
            var chart2 = new google.visualization.ColumnChart(document.getElementById('chart_div2'));
            chart2.draw(data, options);
        }, function () {

        });

    }

    function draw(data, options, ele) {
        var chart = new google.visualization.PieChart(document.getElementById(ele));
        chart.draw(data, options);
    }
});