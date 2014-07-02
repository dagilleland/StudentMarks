/// <reference path="../../../studentmarks/scripts/angular.js" />
/// <reference path="../../../studentmarks/scripts/ngapp/myapp.js" />

//Test suite
describe('Angular Application is configured correctly', function () {

    //Setup
    beforeEach(function () {
    });

    //Spec - 1
    it('Should exist as angular module', function () {
        expect(angular.module('myApp')).not.toBe(null);
    });


    //Teardown
    afterEach(function () {
    });
});
