/// <reference path="../../../studentmarks/scripts/angular.js" />
/// <reference path="../../../studentmarks/scripts/angular-mocks.js" />
/// <reference path="../../../studentmarks/scripts/ngapp/myapp.js" />
/// <reference path="../../../studentmarks/scripts/ngapp/app.controllers.js" />
/// <reference path="../../scripts/ngapp/app.Controllers.js" />
/// <reference path="../../Scripts/jasmine.js" />
/// <reference path="../../Scripts/ngmidwaytester.js" />

describe('App.Controllers', function () {
    // Ensure that the App.Controllers module exists
    it('should exist as angular module', function () {
        expect(angular.module('App.Controllers')).not.toBe(null);
        expect(angular.module('App.Controllers')).not.toBe(undefined);
    });

    // Ensure that the App.Controllers module has the appropriate dependencies on other modules (both $angular and app-specific)
    describe('dependencies', function () {
        var deps;
        var hasModule = function (m) { return deps.indexOf(m) >= 0; };

        beforeEach(function () {
            deps = angular.module('App.Controllers').value('appName').requires;
        })

        it('should have no module dependencies', function () {
            expect(deps.length).toEqual(0);
        });

        //it('should have $scope as a dependency', function () {
        //    expect(hasModule('$scope')).toEqual(true);
        //});

        //it('should have App.Services as a dependency', function () {
        //    expect(hasModule('App.Services')).toEqual(true);
        //});
    });

    // Ensure that the App.Controllers module has the Controllers/factories it needs to do allow data to flow between the app and the web server
    describe('has Controllers', function () {
        var courseConfigCtrl, studentMarksCtrl, classConfigCtrl, classListCtrl;
        var App;
        // executed before each "it" is run.
        beforeEach(function () {
            // load the module
            App = angular.mock.module('myApp');

            // Get the Controllers associated with the module
            // The _underscores_ are a convenience thing allowing the closure variable name to be the same as the injected service.
            //inject(function (_courseConfigCtrl_, _studentMarksCtrl_, _classConfigCtrl_, _classListCtrl_) {
            //    courseConfigCtrl = _courseConfigCtrl_;
            //    studentMarksCtrl = _studentMarksCtrl_;
            //    classConfigCtrl = _classConfigCtrl_;
            //    classListCtrl = _classListCtrl_;
            //});
        });

        it('should contain a CourseConfigCtrl controller', function () {
            expect(App.CourseConfigCtrl).not.toEqual(null);
            //expect(courseConfigCtrl).toBeDefined();
        });

        it('should contain a studentMarksCtrl controller', function () {
            expect(studentMarksCtrl).toBeDefined();
        });

        it('should contain a classConfigCtrl controller', function () {
            expect(classConfigCtrl).toBeDefined();
        });

        it('should contain a classListCtrl controller', function () {
            expect(classListCtrl).toBeDefined();
        });

    });
});
