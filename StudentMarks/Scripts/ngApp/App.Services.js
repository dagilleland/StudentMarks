/// <reference path="../../../studentmarks/scripts/angular.js" />

angular.module('App.Services', []);

angular.module('App.Services')
.factory('urlService', function () {
    var rootUrl = 'http://localhost:58955';
    return {
        rootUrl: rootUrl,
        url: function (routePrefix, route) {
            // CREDITS: for .filter(Boolean) to remove null, empty, NaN, and undefined http://www.devign.me/javascript-tip-remove-falsy-items-out-of-an-array
            return [rootUrl, routePrefix, route].filter(Boolean).join('/');
        }
    };
});

angular.module('App.Services')
.factory('courseConfigService', function ($http, urlService) {
    var routePrefix = 'api/CourseConfig';
    return {
        // POST api/CourseConfig/SetCourseName
        setCourseName: function(data) {
            return $http.post(urlService.url(routePrefix, 'SetCourseName'));
        },

        // GET api/CourseConfig/GetCourseName
        getCourseName: function () {
            return $http.get(urlService.url(routePrefix, 'GetCourseName'));
        },

        // POST api/CourseConfig/SaveEvaluationComponents
        saveEvaluationComponents: function (data) {
            return $http.post(urlService.url(routePrefix, 'SaveEvaluationComponents'));
        },
        // GET api/CourseConfig/GetEvaluationComponents
        getEvaluationComponents: function () {
            return $http.get(urlService.url(routePrefix, 'GetEvaluationComponents'));
        }

    };
});

