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
        // api/CourseConfig/SetCourseName

        // api/CourseConfig/GetCourseName
        getCourseName: function () {
            return $http.get(urlService.url(routePrefix, 'GetCourseName'));
        }
    };
});

