app.run(function ($rootScope, $state, $window, $location, cfpLoadingBar, localStorageService, AuthService) {

    $rootScope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
        // scroll top the page on change state
        $window.scrollTo(0, 0);
        // Save the route title
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

    $urlRouterProvider.otherwise("404");

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });


    $stateProvider
    .state('Dashboard', {
        url: '/',
        templateUrl: '/views/dashboard.html',
        controller: '/controllers/dashboardCtrl.js',
        title: 'Dashboard',
        authenticate: false
    })
    .state('DoctorSchedule', {
        url: '/dschedule',
        templateUrl: '/views/doctorschedule.html',
        controller: '/controllers/doctorScheduleCtrl.js',
        title: 'Schedule',
        authenticate: false
    })
    .state('PatientSchedule', {
        url: '/pschedule',
        templateUrl: '/views/patientschedule.html',
        controller: '/controllers/patientScheduleCtrl.js',
        title: 'Schedule',
        authenticate: false
    })
    .state('registerTreatment', {
        url: '/registertreatment',
        templateUrl: '/views/registertreatment.html',
        controller: '/controllers/registerTreatmentCtrl.js',
        title: 'Register treatment',
        authenticate: false
    })
});

