/// <reference path="../angular.js" />
'use strict';

angular.module('myApp', [
    // Angular modules 
    //'ngAnimate',        // animations
    //'ngRoute'           // routing

    // Custom modules 

    // 3rd Party Modules
        
]);

angular.module('myApp').controller('Controllers', function ($scope) {
    $scope.whazoo = 5;
});



//// Execute bootstrapping code and any dependencies.
//// TODO: inject services as needed.
//myApp.run(['$q', '$rootScope',
//    function ($q, $rootScope) {

//    }]);
