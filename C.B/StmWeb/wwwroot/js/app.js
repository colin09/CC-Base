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

module.controller('EventIndexCtl', function ($scope, $http, $sce) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(1).addClass("active");

    $scope.getEventType = function (id) {
        $http.get("../Event/GetTypeList?level=1&parentId=" + id).success(function (response) {
            if (response.success) {
                if (id == 0) {
                    $scope.ParentTypeList = response.data;
                    var typeId = -1;
                    if (response.data.length > 0) {
                        typeId = $scope.ParentTypeList[0].id;
                        $scope.getEventType(typeId);
                        window.setTimeout(function () { addParentTypeListener(); }, 100);
                    }
                }
                else {
                    $scope.typeList = response.data;
                    var typeId = -1;
                    if (response.data.length > 0) {
                        typeId = $scope.typeList[0].id;
                        $scope.getEventInfo(typeId);
                        window.setTimeout(function () { addTypeListener(); }, 100);
                    } else {
                        $scope.getEventInfo(id);
                    }
                }
            }
        });
    }

    $scope.getEventInfo = function (id) {
        $http.get("../Event/GetDetail?typeId=" + id).success(function (response) {
            if (response.success) {
                $scope.Model = response.data;
                $scope.Model.content = $sce.trustAsHtml(response.data.content);
            }
        });
    }


    function addParentTypeListener() {
        $(".parentTypeList>a").click(function () {
            $(".parentTypeList>a").removeClass("active");
            $(this).addClass("active");
            var id = $(this).data("id");
            $scope.getEventType(id);
        });
        $(".parentTypeList>a").eq(0).addClass("active");
    }

    function addTypeListener() {
        $(".typeList>a").click(function () {
            $(".typeList>a").removeClass("active");
            $(this).addClass("active");
            var id = $(this).data("id");
            $scope.getEventInfo(id);
        });

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

    $scope.GetAreaList = function () {
        $http.get("../Event/GetAreaList").success(function (response) {
            if (response.success) {
                $scope.areaList = response.data;
            }
        });
    };

    function getVerifyCode() {
        $.get("../Common/GetVerifyCodeImg").done(function (response) {
            if (response.success)
                $("#imgVerifyCode").attr("src", "data:image/jpeg;base64," + response.data);
        });
    }

    $("#imgVerifyCode").click(function () {
        getVerifyCode();
    });
    getVerifyCode();

    $scope.SubmitAsk = function () {
        var url = '../Event/SubmitAsk';
        if ($scope.askArea == "0" || $scope.askArea == undefined) {
            alert("请选择赛区");
            return;
        }
        if ($scope.askContent.length < 6) {
            alert("请输入您的问题");
            return;
        }
        var request = {
            key1: $scope.askName,
            key2: $scope.askArea,
            key3: $scope.askContent,
            key4: $scope.askCode
        };
        $http.post(url, request).success(function (response) {
            if (response.success) {
                $scope.askContent = "";
                getVerifyCode();

                $scope.GetDataList();
            } else
                alert(response.message);
        });
    };

    $scope.GetDataList = function () {
        var pager = { pageIndex: 1, pageSize: 20 };
        $http.post("../Event/GetAskList", pager).success(function (response) {
            if (response.success) {
                $scope.AskList = response.data;
            }
        });
    }


    $scope.GetAreaList();
    $scope.GetDataList();

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

module.controller('NoticeDetailCtl', function ($scope, $http, $sce) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(2).addClass("active");

    $scope.getData = function (id) {
        $http.get("../Notice/GetDetail?id=" + id).success(function (response) {
            if (response.success) {
                $scope.Model = response.data;
                $scope.Model.content = $sce.trustAsHtml(response.data.content);

                // window.setTimeout(function () {
                //     var iframe = document.createElementById('frameDoc');
                //     var iwindow = iframe.contentWindow;
                //     var idoc = iwindow.document;
                //     iframe.height = idoc.body.offsetHeight;
                // }, 1000);
            }
        });
    }
    window.setTimeout(function () {
        var id = $("#hdId").val();
        $scope.getData(id);
    }, 100);

    function setIframeHeight() {
        // var iframe = document.getElementById('frameDoc');
        var iframe = document.getElementsByTagName('iframe');
        if (iframe) {
            var iframeWin = iframe.contentWindow || iframe.contentDocument.parentWindow;
            if (iframeWin.document.body) {
                console.log(iframeWin.document.documentElement.scrollHeight || iframeWin.document.body.scrollHeight);
                iframe.height = iframeWin.document.documentElement.scrollHeight || iframeWin.document.body.scrollHeight;
            }
        }
    };
    window.οnresize = function () {
        setIframeHeight();
    }
});

module.controller('MediaIndexCtl', function ($scope, $http) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(3).addClass("active");

    $scope.pagerEvent = { pageIndex: 1, pageSize: 9, pageCount: 1 };
    $scope.pagerImage = { pageIndex: 1, pageSize: 9, pageCount: 1 };
    $scope.pagerVideo = { pageIndex: 1, pageSize: 9, pageCount: 1 };

    $scope.InitMedia = function (newsType) {
        var page = $scope.pagerEvent;
        if (newsType == 2) page = $scope.pagerImage;
        if (newsType == 3) page = $scope.pagerVideo;

        var request = { num1: newsType, pager: page };
        $http.post("../Medias/GetList", request).success(function (response) {
            if (response.success) {
                switch (newsType) {
                    case 1:
                        $scope.NewsEventList = response.data;
                        $scope.pagerEvent.pageCount = response.pager.pageCount;
                        break;
                    case 2:
                        $scope.NewsImageList = response.data;
                        $scope.pagerImage.pageCount = response.pager.pageCount;
                        break;
                    case 3:
                        $scope.NewsVideoList = response.data;
                        $scope.pagerVideo.pageCount = response.pager.pageCount;
                        break;
                }
            }
        });
    }

    $scope.Pager = function (tab, opt) {
        switch (tab) {
            case 1:
                $scope.pagerEvent.pageIndex += opt;
                if ($scope.pagerEvent.pageIndex > $scope.pagerEvent.pageCount)
                    $scope.pagerEvent.pageIndex = $scope.pagerEvent.pageCount;
                if ($scope.pagerEvent.pageIndex < 1)
                    $scope.pagerEvent.pageIndex = 1;
                $scope.InitMedia(tab);
                break;
            case 2:
                $scope.pagerImage.pageIndex += opt;
                if ($scope.pagerImage.pageIndex > $scope.pagerImage.pageCount)
                    $scope.pagerImage.pageIndex = $scope.pagerImage.pageCount;
                if ($scope.pagerImage.pageIndex < 1)
                    $scope.pagerImage.pageIndex = 1;
                $scope.InitMedia(tab);
                break;
            case 3:
                $scope.pagerVideo.pageIndex += opt;
                if ($scope.pagerVideo.pageIndex > $scope.pagerVideo.pageCount)
                    $scope.pagerVideo.pageIndex = $scope.pagerVideo.pageCount;
                if ($scope.pagerVideo.pageIndex < 1)
                    $scope.pagerVideo.pageIndex = 1;
                $scope.InitMedia(tab);
                break;
        }
    }

    $scope.InitMedia(1);
    $scope.InitMedia(2);
    $scope.InitMedia(3);
});

module.controller('MediaDetailCtl', function ($scope, $http, $sce) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(3).addClass("active");


    $scope.InitMedia = function () {
        var id = $("#hdId").val();
        $http.get("../Medias/GetDetail?id=" + id).success(function (response) {
            if (response.success && response.data != null) {
                $scope.Model = response.data;
                $scope.Model.content = $sce.trustAsHtml(response.data.content);
                switch (response.data.type) {
                    case 1: // event
                        break;
                    case 2: // image
                        break;
                    case 3: // video
                        InitVideoPlayer(response.data.url, response.data.video);
                        break;
                }
            }
        });
    }
    $scope.InitMedia();


    function InitVideoPlayer(imgUrl, vioUrl) {
        var option = {
            poster: imgUrl,
            sources: [{
                src: vioUrl,
                type: 'video/mp4'
            }],
            autoplay: false
        };
        var player = videojs('my-video', option, function () {
            console.log('Good to go!');
            //this.play(); // if you don't trust autoplay for some reason
        });
        player.on('play', function () {
            console.log('开始/恢复播放');
        });
        player.on('pause', function () {
            console.log('暂停播放');
        });
        player.on('ended', function () {
            console.log('结束播放');
        });
    }

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

module.controller('ExpertDetailCtl', function ($scope, $http, $sce) {
    $("#divBanner").removeClass("ng-hide");
    $(".nav-top>li>a").eq(4).addClass("active");


    $scope.getData = function (id) {
        $http.get("../Expert/GetDetail?id=" + id).success(function (response) {
            if (response.success) {
                $scope.Model = response.data;
                $scope.Model.content = $sce.trustAsHtml(response.data.content);
            }
        });
    };

    var id = $("#hdId").val();
    $scope.getData(id);
});
