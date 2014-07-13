/// <reference path="../../../studentmarks/scripts/angular.js" />

angular.module('App.Controllers', ['App.Services']);

angular.module('App.Controllers')
.controller('courseConfigCtrl', ['$scope', 'App.Services', function ($scope, myAppServices) {
	myAppServices.getCourseName().then(function (data) { $scope.courseName = data; });
}]);

