console.log("Hello");
app.run(function ($rootScope, $state, $window, $location) {

    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        $window.scrollTo(0, 0);
        $rootScope.currTitle = $state.current.title;
    });

    $rootScope.pageTitle = function () {
        return 'Healthcare - ' + ($rootScope.currTitle || '');
    };
});

app.config(function ($stateProvider, $urlRouterProvider, $locationProvider, $controllerProvider, $compileProvider,
    $filterProvider, $provide) {

    app.controller = $controllerProvider.register;
    app.directive = $compileProvider.directive;
    app.filter = $filterProvider.register;
    app.factory = $provide.factory;
    app.service = $provide.service;
    app.constant = $provide.constant;
    app.value = $provide.value;

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });


    $stateProvider
    .state('Dashboard', {
        url: '/',
        templateUrl: '/views/dashboard.html',
        controller: 'DashboardCtrl',
        title: 'Dashboard'
    })
    //.state('DoctorSchedule', {
    //    url: '/dschedule',
    //    templateUrl: '/views/doctorschedule.html',
    //    controller: 'DoctorScheduleCtrl',
    //    title: 'Schedule'
    //})
    .state('PatientSchedule', {
        url: '/plan',
        templateUrl: '/views/doctorschedule.html',
        controller: 'PatientScheduleCtrl',
        title: 'Plan'
    })
    .state('DoctorSchedule', {
        url: '/appointment',
        templateUrl: '/views/patientschedule.html',
        controller: 'DoctorScheduleCtrl',
        title: 'Appointment'
    })
    .state('Schedule', {
        url: '/schedule',
        templateUrl: '/views/schedule.html',
        title: 'Schedule'
    })
    .state('RegisterTreatment', {
        url: '/registertreatment',
        templateUrl: '/views/registertreatment.html',
        controller: 'RegisterTreatmentCtrl',
        title: 'Register treatment'
    })
     .state('Statistic', {
         url: '/statistic',
         templateUrl: '/views/statistic.html',
         controller: 'StatisticCtrl',
         title: 'Statistic'
     })
      .state('MedicalRecord', {
          url: '/medicalrecord',
          templateUrl: '/views/medicalrecord.html',
          controller: 'MedicalRecordCtrl',
          title: 'Medical Record'
      })
        .state('Test', {
            url: '/test',
            templateUrl: '/views/testpage.html',
            controller: 'TestCtrl',
            title: 'Test Page'
        })
});

