(function () {
    'use strict';

    var id = 'myApp';

    // TODO: Inject modules as needed.
    var myApp = angular.module('myApp', [
        // Angular modules 
        'ngAnimate',        // animations
        'ngRoute'           // routing

        // Custom modules 

        // 3rd Party Modules
        
    ]);

    // Execute bootstrapping code and any dependencies.
    // TODO: inject services as needed.
    myApp.run(['$q', '$rootScope',
        function ($q, $rootScope) {

        }]);
})();