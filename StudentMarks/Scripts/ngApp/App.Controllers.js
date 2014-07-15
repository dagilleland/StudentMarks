/// <reference path="../../../studentmarks/scripts/angular.js" />

angular.module('App.Controllers', ['App.Services', 'ui.bootstrap']);

//#region courseConfigCtrl
angular.module('App.Controllers')
.controller('courseConfigCtrl',
['$scope', '$timeout', 'courseConfigService', 'modalService',
    function ($scope, $timeout, courseConfigService, modalService) {
    //#region Initial Model values
    $scope.courseConfig = {};
    $scope.newItem = {};
    $scope.newTopic = "";
    $scope.courseConfig.displayPositions = [];
    $scope.webApiStatus = '';
    //#endregion

    //#region Load data from server
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
    //#endregion

    //#region Dynamic Values
    $scope.totalWeight = function () {
        var total = 0;
        if ($scope.courseEval) {
            for (var i = 0; i < $scope.courseEval.MarkableItems.length; i++) {
                total = total + $scope.courseEval.MarkableItems[i].Weight;
            }
        }

        return total;
    };
    //#endregion

    //#region User [View] Event Handlers
    $scope.saveCourseName = function () {
        var modalOptions = {
            closeButtonText: 'Cancel',
            actionButtonText: 'Save Course Name',
            headerText: 'Save ' + $scope.courseName + '?',
            bodyText: 'Are you sure you want to save this as the course name?'
        };

        modalService.showModal({}, modalOptions).then(function (result) {
            $scope.webApiStatus = "Saving Course Name...";
            courseConfigService.setCourseName($scope.courseName).then(function () {
                $scope.webApiStatus = "Course Name Saved.";
                loadCourseName();
                $timeout(clearWebApiStatus, 1500);
            });
        });
    };

    $scope.resetCourseName = function () {
        $scope.courseName = angular.copy($scope.courseConfig.resetCourseName);
    }

    $scope.saveComponents = function () {
        var itemCount = $scope.courseEval.MarkableItems.length;
        var topicCount = $scope.courseEval.BucketTopics.length;

        var modalOptions = {
            closeButtonText: 'Cancel',
            actionButtonText: 'Save Evaluation Components',
            headerText: 'Save Changes to Evaluation Components?',
            bodyText: 'Are you sure you want to save these ' + itemCount + ' Evaluation Components and ' + topicCount + ' Topics? The total weight of all components is ' + $scope.totalWeight() + ' %.'
        };

        modalService.showModal({}, modalOptions).then(function (result) {
            $scope.webApiStatus = "Saving Course Evaluation Components...";
            courseConfigService.saveEvaluationComponents($scope.courseEval).then(function () {
                $scope.webApiStatus = "Course Evaluation Components Saved.";
                loadEvaluationComponents();
                $timeout(clearWebApiStatus, 1500);
            });
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
            // TODO: Clean up DisplayOrder as a result of removing item

            // TODO: Clean up $scope.courseConfig.displayPositions as a result of removing item
        }
    };

    $scope.add = function () {
        console.log($scope.newItem);
        var newItem = {
            ID: 0,
            ItemType: angular.copy($scope.newItem.ItemType),
            Name: angular.copy($scope.newItem.Name),
            DisplayOrder: $scope.courseEval.MarkableItems.length + 1
        };
        if ($scope.newItem.ItemType === "Quiz") {
            newItem.Weight = $scope.newItem.Weight;
            newItem.TotalPossibleMarks = $scope.newItem.TotalPossibleMarks | null;
        } else {
            newItem.Topic = $scope.newItem.Topic;
            newItem.TopicID = $scope.newItem.TopicID | null;
        }
        $scope.courseEval.MarkableItems.push(newItem);
        // TODO: Add to $scope.courseConfig.displayPositions as a result of adding item
        $scope.courseConfig.displayPositions.push(newItem.DisplayOrder);
        $scope.newItem = {};
    };

    $scope.deleteTopic = function (item) {
        var index = $scope.courseEval.BucketTopics.indexOf(item);
        if (index >= 0) {
            $scope.courseEval.BucketTopics.splice(index, 1);
        }
    };

    $scope.addTopic = function () {
        console.log($scope.newTopic);
        var newTopic = { TopicID: 0, Description: angular.copy($scope.newTopic) };
        $scope.courseEval.BucketTopics.push(newTopic);
        $scope.newTopic = "";
    };
    //#endregion
}]);
//#endregion

// TODO: Add other controllers