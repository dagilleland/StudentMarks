/// <reference path="../../../studentmarks/scripts/angular.js" />
/// <reference path="../../../studentmarks/scripts/angular-mocks.js" />
/// <reference path="../../../studentmarks/scripts/ngapp/myapp.js" />
/// <reference path="../../scripts/ngapp/app.services.js" />
/// <reference path="../../Scripts/jasmine.js" />
/// <reference path="../../Scripts/ngmidwaytester.js" />

describe('App.Services', function () {
    // Ensure that the App.Services module exists
    it('should exist as angular module', function () {
        expect(angular.module('App.Services')).not.toBe(null);
        expect(angular.module('App.Services')).not.toBe(undefined);
    });

    // Ensure that the App.Services module has the appropriate dependencies on other modules (both $angular and app-specific)
    describe('dependencies', function () {
        var deps;

        beforeEach(function () {
            deps = angular.module('App.Services').value('appName').requires;
        })

        it('should have no module dependencies', function () {
            expect(deps.length).toEqual(0);
        });
    });

    // Ensure that the App.Services module has the services/factories it needs to do allow data to flow between the app and the web server
    describe('has services', function () {
        // CREDITS: Approach based on http://www.benlesh.com/2013/06/angular-js-unit-testing-services.html and http://busypeoples.github.io/post/writing-unit-tests-for-service-in-angular-js/
        var urlService, courseConfigService, httpBackend;

        // executed before each "it" is run.
        beforeEach(function () {
            // load the module
            angular.mock.module('App.Services')

            // Get the services associated with the module
            // The _underscores_ are a convenience thing allowing the closure variable name to be the same as the injected service.
            inject(function (_urlService_, _courseConfigService_, _$httpBackend_) {
                urlService = _urlService_;
                courseConfigService = _courseConfigService_;
                httpBackend = _$httpBackend_;
            })
        });

        // make sure no expectations were missed in your tests.
        // (e.g. expectGET or expectPOST)
        afterEach(function () {
            httpBackend.verifyNoOutstandingExpectation();
            httpBackend.verifyNoOutstandingRequest();
        });

        it('should contain a courseConfigService service', function () {
            expect(courseConfigService).toBeDefined();
        });

        it('should contain a urlService service', function () {
            expect(urlService).toBeDefined();
        });

    });
});
