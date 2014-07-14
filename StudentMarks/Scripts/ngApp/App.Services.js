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
        setCourseName: function (data) {
            // CREDITS: http://www.jasonwatmore.com/post/2014/04/18/Post-a-simple-string-value-from-AngularJS-to-NET-Web-API.aspx
            // NOTE: For simple string data, be sure to a) wrap in double-quotes as below and b) have only one parameter on the server-side WebAPI
            return $http.post(urlService.url(routePrefix, 'SetCourseName'), '"' + data + '"');
        },

        // GET api/CourseConfig/GetCourseName
        getCourseName: function () {
            return $http.get(urlService.url(routePrefix, 'GetCourseName'));
        },

        // POST api/CourseConfig/SaveEvaluationComponents
        saveEvaluationComponents: function (data) {
            return $http.post(
                urlService.url(routePrefix, 'SaveEvaluationComponents'),
                data
                );
        },
        // GET api/CourseConfig/GetEvaluationComponents
        getEvaluationComponents: function () {
            return $http.get(urlService.url(routePrefix, 'GetEvaluationComponents'));
        }

    };
});

