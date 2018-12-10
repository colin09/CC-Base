//var module = angular.module('managerApp', ['ngRoute', 'ui.calendar', 'ui.bootstrap','ngSanitize']);

var module = angular.module('managerApp',[]);


module.controller('mgrDevCtl', function ($scope,$http) {

    $scope.Title= "System Users";

    
    $scope.newUser = {
        AuthType:2,
        State:1,
        UserName:"",
        TrueName:"",
        Department:""
    };
    $scope.AddNewUser = function(){
        var url = "../../Sys/Dev/CreateSysUsers";
        var request = {
            userName:$scope.newUser.UserName,
            trueName:$scope.newUser.TrueName,
            department:$scope.newUser.Department,
            authType:$scope.newUser.AuthType
        };
        $http.post(url,request).success(function(response){
            $scope.GetUserList();
        });
    }

    $scope.GetUserList = function(){
        var url = "../../Sys/Dev/GetSysUsersByPage";
        $http.get(url).success(function(response){
            $scope.UserList = response;
        });
    }
    $scope.GetUserList();
    
});

