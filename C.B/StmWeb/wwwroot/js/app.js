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

    $scope.getEventType = function (id) {
        $http.get("../Event/GetTypeList?parentId=" + id).success(function (response) {
            if (response.success) {
                if (id == 0) {
                    $scope.ParentTypeList = response.data;
                    var typeId = $scope.ParentTypeList[0].id;
                    $scope.getEventType(typeId);
                }
                else {
                    $scope.typeList = response.data;
                    var typeId = $scope.typeList[0].id;
                    $scope.getEventInfo(typeId);

                    window.setTimeout(function(){addListener();},100);
                }
            }
        });
    }

    $scope.getEventInfo = function (id) {
        $http.get("../Event/GetDetail?typeId=" + id).success(function (response) {
            if (response.success) {
                $scope.Model = response.data;
            }
        });
    }


    function addListener() {
        $(".parentTypeList>a").click(function () {
            $(".parentTypeList>a").removeClass("active");
            $(this).addClass("active");
            var id = $(this).data("id");
            $scope.getEventType(id);
        });
        $(".typeList>a").click(function () {
            $(".typeList>a").removeClass("active");
            $(this).addClass("active");
            var id = $(this).data("id");
            $scope.getEventInfo(id);
        });
        
        $(".parentTypeList>a").eq(0).addClass("active");
        $(".typeList>a").eq(0).addClass("active");
    }


    $scope.getEventType(0);
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

    $scope.pager = { pageIndex: 1, pageSize: 9 };

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

    $scope.getData = function (type) {
        $http.get("../Expert/GetList?type=" + type).success(function (response) {
            if (response.success) {
                switch (type) {
                    case 1:
                        $scope.zhjList = response.data;
                        break;
                    case 2:
                        $scope.YuansList = response.data;
                        break;
                }
            }
        });
    }

    $scope.getData(1);
    $scope.getData(2);

});

module.controller('ExpertDetailCtl', function ($scope, $http) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(4).addClass("active");


    $scope.getData = function (id) {
        $http.get("../Expert/GetDetail?id=" + id).success(function (response) {
            if (response.success) {
                $scope.Model = response.data;
            }
        });
    };

    var id = $("#hdId").val();
    $scope.getData(id);
});
