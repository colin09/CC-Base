var module = angular.module('stmApp', []);

module.controller('StmIndexCtl', function ($scope, $http) {
    $("#divSlides").removeClass("ng-hide");
    // $("#divBanner").show();
    $("#divFooterOne").removeClass("ng-hide");
    
    $(".nav-top>li>a").eq(0).addClass("active");


});

module.controller('EventIndexCtl', function ($scope, $http) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(1).addClass("active");

});

module.controller('EventAwardCtl', function ($scope, $http) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(5).addClass("active");

});

module.controller('EventReviewCtl', function ($scope, $http) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(6).addClass("active");

});

module.controller('EventActiveCtl', function ($scope, $http) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(7).addClass("active");

});

module.controller('EventContactCtl', function ($scope, $http) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(8).addClass("active");

});



module.controller('NoticeIndexCtl', function ($scope, $http) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(2).addClass("active");

});

module.controller('MediaIndexCtl', function ($scope, $http) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(3).addClass("active");

});
module.controller('ExpertIndexCtl', function ($scope, $http) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(4).addClass("active");

});