/// <reference path="../angular.js" />
'use strict';

angular.module('myApp', [
    // Angular modules 
    //'ngAnimate',        // animations
    //'ngRoute'           // routing

    // Custom modules 
    'App.Services',
    'App.Controllers',
    'App.Routes',
    'App.Directives'
    // 3rd Party Modules
    //,'ui.bootstrap'
]);

angular.module('myApp').controller('WazooCtrl', function ($scope) {
    $scope.whazoo = 5;
});



//// Execute bootstrapping code and any dependencies.
//// TODO: inject services as needed.
//myApp.run(['$q', '$rootScope',
//    function ($q, $rootScope) {

//    }]);
