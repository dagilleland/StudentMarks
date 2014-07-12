/// <reference path="../../../studentmarks/scripts/angular.js" />
/// <reference path="../../../studentmarks/scripts/angular-mocks.js" />
/// <reference path="../../../studentmarks/scripts/ngapp/myapp.js" />
/// <reference path="../../scripts/ngapp/app.services.js" />
/// <reference path="../../Scripts/jasmine.js" />
/// <reference path="../../Scripts/ngmidwaytester.js" />

describe('App.Services', function () {
	// Tests the urlService factory to ensure it correctly generates urls for api calls
	describe('urlService', function () {
		var urlService;

		beforeEach(function () {
			angular.mock.module('App.Services')

			inject(function (_urlService_) {
				urlService = _urlService_;
			})
		});

		it('should have rootUrl', function () {
			expect(urlService.rootUrl).toEqual('http://localhost:58955');
		});

		it('should calculate url', function () {
			expect(urlService.url('api/NotReal', 'GetName')).toEqual('http://localhost:58955/api/NotReal/GetName');
		});

		it('should calculate url without routePrefix', function () {
			expect(urlService.url(null, 'GetName')).toEqual('http://localhost:58955/GetName');
		});

		it('should calculate url without route', function () {
			expect(urlService.url('api/NotReal')).toEqual('http://localhost:58955/api/NotReal');
		});

		it('should calculate url without any parameter as equal to the rootUrl', function () {
			expect(urlService.url()).toEqual(urlService.rootUrl);
		});
	});
});
