﻿/// <reference path="../../../studentmarks/scripts/angular.js" />
/// <reference path="../../../studentmarks/scripts/angular-mocks.js" />
/// <reference path="../../../studentmarks/scripts/ngapp/myapp.js" />
/// <reference path="../../Scripts/jasmine.js" />
/// <reference path="../../Scripts/ngmidwaytester.js" />

//Test suite
describe('myApp', function () {

    //Setup
    beforeEach(function () {
    });

    //Spec - 1
    it('should exist as angular module', function () {
        expect(angular.module('myApp')).not.toBe(null);
        expect(angular.module('myApp')).not.toBe(undefined);
    });

/*
    it('', function () { 
    });
*/
    describe('dependencies', function () {
        var deps;
        var hasModule = function (m) { return deps.indexOf(m) >= 0; };

        beforeEach(function () {
            deps = angular.module('myApp').value('appName').requires;
        })

        it('should have no dependencies', function () {
            expect(deps.length).toEqual(0);
        });

        /*
        // NOTE: If I add dependencies on my module, then use the sample in this block
        it('should have myApp.mainCtrl as a dependency', function () {
            expect(hasModule('myApp.Controllers')).toEqual(true);
        });
        */
    });


    it('should map routes to controllers', function () {
        angular.module('myApp');
        inject(function ($route) {
            expect($route.routes['/students'].controller).toBe('StudentListCtrl');
            expect($route.routes['/students'].tmeplateUrl).toEqual('partials/student-list.html');

            expect($route.routes['/students/:studentId'].controller).toBe('studentDetailCtrl');
            expect($route.routes['/students/:studentId'].templateUrl).toEqual('partials/student-detail.html');

            // otherwise redirect to
            expect($route.routes[null].redirectTo).toEqual('/students')

        });
    });

    //Teardown
    afterEach(function () {
    });
});

angular.module('App.Services', []);

angular.module('App.Services')
.factory('$appServer', function ($http) {
    return {

    };
});


// The App.Services module contains all XHR calls and corresponding injection of data into the angular $scope.
describe('App.Services', function () {

    //Setup
    beforeEach(function () {

    });

    // NOTE: 
    it('should exist as angular module', function () {
        expect(angular.module('App.Services')).not.toBe(null);
        expect(angular.module('App.Services')).not.toBe(undefined);
    });

    describe('has angular dependencies', function () {
        var deps;
        var hasModule = function (m) { return deps.indexOf(m) >= 0; };

        beforeEach(function () {
            deps = angular.module('App.Services').value('appName').requires;
        })

        it('should have $http as a dependency', function () {
            expect(hasModule('$http')).toEqual(true);
        });

        /*
        // NOTE: If I add dependencies on my module, then use the sample in this block
        it('should have no dependencies', function () {
            expect(deps.length).toEqual(0);
        });

        it('should have myApp.mainCtrl as a dependency', function () {
            expect(hasModule('myApp.Controllers')).toEqual(true);
        });
        */
    })

    describe('has services', function () {
        // Approach based on http://www.benlesh.com/2013/06/angular-js-unit-testing-services.html
        // and http://busypeoples.github.io/post/writing-unit-tests-for-service-in-angular-js/
        var $appServer, httpBackend;

        // executed before each "it" is run.
        beforeEach(function () {
            // load the module
            angular.mock.module('App.Services')

            // Inject the service for testing.
            // Also get $httpBackend, which is a mock provided by angular-mocks.js
            // The _underscores_ are a convenience thing
            // so you can have your variable name be the 
            // same as your injected service.
            inject(function (_$appServer_, _$httpBackend_) {
                $appServer = _$appServer_;
                httpBackend = _$httpBackend_;
            })
        });

        // make sure no expectations were missed in your tests.
        // (e.g. expectGET or expectPOST)
        afterEach(function () {
            httpBackend.verifyNoOutstandingExpectation();
            httpBackend.verifyNoOutstandingRequest();
        });

        //beforeEach(inject(function ($injector) {
        //    $appServer = $injector.get('$appServer');
        //   // $http = $injector.get('$http');
        //}));

        it('should contain an $appServer service', function () {
            expect($appServer).toBeDefined();
        });

    });

    describe('using $http', function () {
        //var tester;
        //beforeEach(function () {
        //    if (tester) {
        //        tester.destroy();
        //    };
        //    tester = ngMidwayTester('App');
        //});

        // local variable copies of injected objects
        var $scope, $rootScope, $httpBackend, $timeout;

        // Ensure the module exists
        beforeEach(module('App.Services'));

        // Ensure the DI objects
        // http://nathanleclaire.com/blog/2013/12/13/how-to-unit-test-controllers-in-angularjs-without-setting-your-hair-on-fire/
        beforeEach(inject(function ($injector) {
            $timeout = $injector.get('$timeout');
            $httpBackend = $injector.get('$httpBackend');
            $rootScope = $injector.get('$rootScope');
            $scope = $rootScope.$new();
        }));

        // Cleanup - verification
        afterEach(function () {
            $httpBackend.verifyNoOutstandingExpectation();
            $httpBackend.verifyNoOutstandingRequest();
        });

        it('should retrieve course name', inject(['$http', function () {
            expect('Test Specs').toBe('To Be Detailed');
        }]));

        it('should retrieve evaluation components', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should retrieve bucket weight and topics', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should maintain a copy of course name original value', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should maintain a copy of evaluation components original values', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should maintain a copy of bucket weight and topics original values', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should save course name to server', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should save evaluation components to server as a single batch job', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should prevent saving evaluation components if validation fails', function () { expect('Test Specs').toBe('To Be Detailed'); });

    });

    describe('performing validation', function () {
        it('should validate component name as required', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should validate quiz weight as required', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should validate bucket topic as required', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should validate topic name as required', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should validate individual bucket weights as required', function () { expect('Test Specs').toBe('To Be Detailed'); });
    });

    describe('manipulating data', function () {
        it('should add bucket topics', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should add quiz component', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should add bucket component', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should tally total weight of evaluation components', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should allow ordering of evaluation components', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should reset course name to original value when cancelling course name changes', function () { expect('Test Specs').toBe('To Be Detailed'); });
        it('should reset evaluation components and bucket weight and topics when cancelling evaluation component changes', function () { expect('Test Specs').toBe('To Be Detailed'); });
    });

    //describe('', function () {
    //    it('', function () { expect('Test Specs').toBe('To Be Detailed'); });
    //});
});


/* NOT in queue
 * describe myApp
 *      - should exist as an angular module
 * 
 * describe myApp.mainCtrl
 *      - should determine if myApp requires setup of evaluation components
 *      - should determine if myApp requires setup of class list
 */

/* ADDED TO QUEUE...
 * describe App.Services
 *   describe using $http
 *      - should retrieve course name
 *      - should retrieve evaluation components
 *      - should retrieve bucket weight and topics
 *      - should maintain a copy of course name original value
 *      - should maintain a copy of evaluation components original values
 *      - should maintain a copy of bucket weight and topics original values
 *      - should save course name to server
 *      - should save evaluation components to server as a single batch job
 *      - should prevent saving evaluation components if validation fails
 *   describe performing validation
 *      - should validate component name as required
 *      - should validate quiz weight as required
 *      - should validate bucket topic as required
 *      - should validate topic name as required
 *      - should validate individual bucket weights as required
 *   describe manipulating data
 *      - should add bucket topics
 *      - should add quiz component
 *      - should add bucket component
 *      - should tally total weight of evaluation components
 *      - should allow ordering of evaluation components
 *      - should reset course name to original value when cancelling course name changes
 *      - should reset evaluation components and bucket weight and topics when cancelling evaluation component changes
 */