/// <reference path="../../../studentmarks/scripts/angular.js" />

angular.module('App.Controllers', ['App.Services']);

angular.module('App.Controllers')
.controller('courseConfigCtrl', ['$scope', '$timeout', 'courseConfigService', function ($scope, $timeout, courseConfigService) {
    $scope.courseConfig = {};
    $scope.courseConfig.displayPositions = [];
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
            for (var i = 0; i < $scope.courseEval.MarkableItems.length; i++) {
                $scope.courseConfig.displayPositions.push($scope.courseEval.MarkableItems[i].DisplayOrder);
            };
            $scope.courseConfig.resetCourseEval = angular.copy($scope.courseEval);
            //console.log(data);
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
        $scope.courseName = angular.copy($scope.courseConfig.resetCourseName);
    }
    $scope.saveComponents = function () {
        //alert($scope.courseEval);
        $scope.webApiStatus = "Saving Course Evaluation Components...";
        courseConfigService.saveEvaluationComponents($scope.courseEval).then(function () {
            $scope.webApiStatus = "Course Evaluation Components Saved.";
            loadEvaluationComponents();
            $timeout(clearWebApiStatus, 1500);
        });
    };

    $scope.resetComponents = function () {
        $scope.courseEval = angular.copy($scope.courseConfig.resetCourseEval);
    }

    $scope.reorder = function (item) {
        var itemIndex, otherIndex = -1;
        var otherItem;
        for (var i = 0; i < $scope.courseEval.MarkableItems.length; i++) {
            if ($scope.courseEval.MarkableItems[i].DisplayOrder === item.DisplayOrder)
                if ($scope.courseEval.MarkableItems[i].Name !== item.Name) {
                    otherIndex = i;
                } else {
                    itemIndex = i;
                }
        }
        if (otherIndex > -1) {
            otherItem = $scope.courseEval.MarkableItems[otherIndex];

            if (itemIndex < otherIndex) {
                while (itemIndex < otherIndex) {
                    $scope.courseEval.MarkableItems[itemIndex] = $scope.courseEval.MarkableItems[itemIndex + 1];
                    $scope.courseEval.MarkableItems[itemIndex].DisplayOrder--;
                    itemIndex++;
                }
                $scope.courseEval.MarkableItems[otherIndex] = item;
            } else {
                while (itemIndex > otherIndex) {
                    $scope.courseEval.MarkableItems[itemIndex] = $scope.courseEval.MarkableItems[itemIndex - 1];
                    $scope.courseEval.MarkableItems[itemIndex].DisplayOrder++;
                    itemIndex--;
                }
                $scope.courseEval.MarkableItems[otherIndex] = item;
            }
        }
    }

    $scope.delete = function (item) {
        var index = $scope.courseEval.MarkableItems.indexOf(item);
        if (index >= 0) {
            $scope.courseEval.MarkableItems.splice(index, 1);
        }
    };
}]);

