//var module = angular.module('managerApp', ['ngRoute', 'ui.calendar', 'ui.bootstrap','ngSanitize']);

var module = angular.module('managerApp',['ngSanitize']);


module.controller('mgrDevController', function ($scope) {

    $scope.Title= "System Users";

});