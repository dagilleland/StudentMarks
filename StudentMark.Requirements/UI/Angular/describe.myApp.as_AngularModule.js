/// <reference path="../../../studentmarks/scripts/angular.js" />
/// <reference path="../../../studentmarks/scripts/angular-mocks.js" />
/// <reference path="../../../studentmarks/scripts/ngapp/myapp.js" />
/// <reference path="../../../studentmarks/scripts/ngapp/app.services.js" />
/// <reference path="../../scripts/ngapp/app.services.js" />
/// <reference path="../../Scripts/jasmine.js" />
/// <reference path="../../Scripts/ngmidwaytester.js" />

describe('myApp', function () {

    //Setup
    beforeEach(function () {
    });

    //Spec - 1
    it('should exist as angular module', function () {
        expect(angular.module('myApp')).not.toBe(null);
        expect(angular.module('myApp')).not.toBe(undefined);
    });

    describe('dependencies', function () {
        // TODO: Decide how much my tests need to determine about the wiring up of the module and it's pieces. At present, I can find the dependencies on a module, but not on a module's factory, controller, etc.
        var deps;
        var hasModule = function (m) { return deps.indexOf(m) >= 0; };

        beforeEach(function () {
            deps = angular.module('myApp').value('appName').requires;
        });

        it('should have App.Services as a dependency', function () {
            expect(hasModule('App.Services')).toEqual(true);
        });

        it('should have App.Controllers as a dependency', function () {
            expect(hasModule('App.Controllers')).toEqual(true);
        });

        it('should have App.Routes as a dependency', function () {
            expect(hasModule('App.Routes')).toEqual(true);
        });

        it('should have App.Directives as a dependency', function () {
            expect(hasModule('App.Directives')).toEqual(true);
        });

        /*
        it('should have no dependencies', function () {
            expect(deps.length).toEqual(0);
        });

        it('should have $http as a dependency', function () {
            expect(hasModule('$http')).toEqual(true);
        });
        */
    });
});
