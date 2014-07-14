/// <reference path="../../../studentmarks/scripts/angular.js" />

angular.module('App.Controllers', ['App.Services']);

angular.module('App.Controllers')
.controller('courseConfigCtrl', ['$scope', 'courseConfigService', function ($scope, courseConfigService) {
    $scope.courseConfig = {};

    // Load data from server
    courseConfigService.getCourseName().then(function (data) {
        $scope.courseName = data.data;
        $scope.courseConfig.resetCourseName = angular.copy($scope.courseName);
    });
    courseConfigService.getEvaluationComponents().then(function (data) {
        $scope.courseEval = data.data;
        $scope.courseConfig.resetCourseEval = angular.copy($scope.courseEval);
        console.log(data);
    });

    // Dynamic Values
    $scope.totalWeight = function () {
        var total = 0;
        if ($scope.courseEval) {
            for (var i = 0; i < $scope.courseEval.MarkableItems.length; i++) {
                total = total + $scope.courseEval.MarkableItems[i].Weight;
            }
        }

        return total;
    };

    // User Events
    $scope.saveCourseName = function () {
        alert($scope.courseName);
    };

    $scope.resetCourseName = function () {
        $scope.courseName = $scope.courseConfig.resetCourseName;
    }
    $scope.saveComponents = function () {
        alert($scope.courseEval);
    };

    $scope.resetComponents = function () {
        $scope.courseEval = $scope.courseConfig.resetCourseEval;
    }
}]);

