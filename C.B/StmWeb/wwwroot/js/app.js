var module = angular.module('stmApp', ["ngSanitize"]);

module.controller('StmIndexCtl', function ($scope, $http) {
    $("#divSlides").removeClass("ng-hide");
    // $("#divBanner").show();
    $("#divFooterOne").removeClass("ng-hide");

    $(".nav-top>li>a").eq(0).addClass("active");


    $scope.InitData = function () {
        var pager = { pageIndex: 1, pageSize: 4 };
        $http.post("../Notice/GetList", pager).success(function (response) {
            if (response.success) {
                $scope.NoticeList = response.data;
            }
        });
        $scope.InitMedia(1);
        $scope.InitMedia(2);
        $scope.InitMedia(3);
    }

    $scope.InitMedia = function (newsType) {
        var request = { num1: newsType, pager: { pageIndex: 1, pageSize: 4 } };
        $http.post("../Medias/GetList", request).success(function (response) {
            if (response.success) {
                switch (newsType) {
                    case 1:
                        $scope.NewsEventList = response.data;
                        break;
                    case 2:
                        $scope.NewsImageList = response.data;
                        break;
                    case 3:
                        $scope.NewsVideoList = response.data;
                        break;
                }
            }
        });
    }

    $scope.InitData();
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

    $scope.getData = function () {
        var pager = { pageIndex: 1, pageSize: 10 };
        $http.post("../Notice/GetList", pager).success(function (response) {
            if (response.success) {
                $scope.NoticeList = response.data;
            }
        });
    }
    $scope.getData();
});

module.controller('NoticeDetailCtl', function ($scope, $http) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(2).addClass("active");

    $scope.getData = function (id) {
        $http.get("../Notice/GetDetail?id=" + id).success(function (response) {
            if (response.success) {
                $scope.Model = response.data;
            }
        });
    }
    window.setTimeout(function () {
        var id = $("#hdId").val();
        $scope.getData(id);
    }, 100);
});

module.controller('MediaIndexCtl', function ($scope, $http) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(3).addClass("active");

    $scope.pager = { pageIndex: 1, pageSize: 9};
    
    $scope.InitMedia = function (newsType) {
        var request = { num1: newsType, pager: $scope.pager };
        $http.post("../Medias/GetList", request).success(function (response) {
            if (response.success) {
                switch (newsType) {
                    case 1:
                        $scope.NewsEventList = response.data;
                        break;
                    case 2:
                        $scope.NewsImageList = response.data;
                        break;
                    case 3:
                        $scope.NewsVideoList = response.data;
                        break;
                }
            }
        });
    }

    $scope.InitMedia(1);
    $scope.InitMedia(2);
    $scope.InitMedia(3);

    

});
module.controller('ExpertIndexCtl', function ($scope, $http) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(4).addClass("active");

});