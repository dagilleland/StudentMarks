/// <reference path="../../../studentmarks/scripts/angular.js" />
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
    })


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

describe('App.Services', function () {
    describe('using $http', function () {
        it('should retrieve course name', function () { expect('Test Specs').toBe('To Be Detailed'); });
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