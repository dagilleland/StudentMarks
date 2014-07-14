/// <reference path="../../../studentmarks/scripts/angular.js" />

angular.module('App.Controllers', ['App.Services']);

angular.module('App.Controllers')
.controller('courseConfigCtrl', ['$scope', '$timeout', 'courseConfigService', function ($scope, $timeout, courseConfigService) {
    $scope.courseConfig = {};
    $scope.webApiStatus = '';
    // Load data from server
    var loadCourseName = function () {
        courseConfigService.getCourseName().then(function (data) {
            // Stripping all double-quotes from simple string result
            $scope.courseName = data.data.replace(/"/g, '');
            $scope.courseConfig.resetCourseName = angular.copy($scope.courseName);
        });
    };
    var loadEvaluationComponents = function () {
        courseConfigService.getEvaluationComponents().then(function (data) {
            $scope.courseEval = data.data;
            $scope.courseConfig.resetCourseEval = angular.copy($scope.courseEval);
            console.log(data);
        });
    };
    var clearWebApiStatus = function () { $scope.webApiStatus = ''; };

    loadCourseName();
    loadEvaluationComponents();

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
        //alert($scope.courseName);
        $scope.webApiStatus = "Saving Course Name...";
        courseConfigService.setCourseName($scope.courseName).then(function () {
            $scope.webApiStatus = "Course Name Saved.";
            loadCourseName();
            $timeout(clearWebApiStatus, 1500);
        });
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

