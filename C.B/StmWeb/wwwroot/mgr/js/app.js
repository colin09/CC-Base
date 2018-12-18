//var module = angular.module('managerApp', ['ngRoute', 'ui.calendar', 'ui.bootstrap','ngSanitize']);

var module = angular.module('managerApp', []);


module.controller('mgrDevCtl', function ($scope, $http) {

    $scope.Title = "System Users";


    $scope.newUser = {
        AuthType: 2,
        State: 1,
        UserName: "",
        TrueName: "",
        Department: ""
    };
    $scope.AddNewUser = function () {
        var url = "../../Sys/Dev/CreateSysUsers";
        /*
        var request = {
            userName:$scope.newUser.UserName,
            trueName:$scope.newUser.TrueName,
            department:$scope.newUser.Department,
            authType:$scope.newUser.AuthType
        };*/
        $http.post(url, $scope.newUser).success(function (response) {
            $scope.GetUserList();
        });
    }

    $scope.GetUserList = function () {
        var url = "../../Sys/Dev/GetSysUsersByPage";
        $http.get(url).success(function (response) {
            if (response.success) {
                $.each(response.data, function (index, item) {
                    switch (item.authType) {
                        case 1: item.authTypeTxt = "develop"; break;
                        case 2: item.authTypeTxt = "admin"; break;
                        case 3: item.authTypeTxt = "user"; break;
                    }
                });
                $scope.UserList = response.data;
            } else
                alert(response.message);
        });
    }
    $scope.GetUserList();

});

module.controller('mgrFileCheckerCtl', function ($scope, $http) {

    $scope.Title = "CHEditor.Source.Checker";
    $scope.tab = "image";

    $scope.GetResourceList = function (type) {
        var url = "../../Sys/File/Browse?type=" + type;
        $http.get(url).success(function (response) {
            if (response.success) {
                $scope.ResourceList = response.data;

                window.setTimeout(function () {
                    $("select.image-picker").imagepicker({
                        hide_select: false,
                    });
                }, 100);
            }
        });
    };
    $scope.$watch('tab', function (newVal, oldVal) {
        if (newVal != "upload")
            $scope.GetResourceList(newVal);
    });

    $scope.GetResourceList("image");

    $scope.Select = function () {
        var files = $("select.image-picker").val();
        var callback = $("#hdCallback").val();
        //window.opener.CKEDITOR.tools.callFunction(files, callback, "");

        window.location.href = "Select?urls=" + files + "&callback=" + callback;
    }


    /**
     * window.parent.CKEDITOR.tools.callFunction(imgUrl, callback, "");
     */
});


module.controller('mgrEditorCtl', function ($scope, $http) {

    // event , expert , news , notice
    $scope.EditType = $("#hdType").val();
    $scope.Title = "";
    switch($scope.EditType){
        case "event": $scope.Title = "赛事信息编辑"; break;
        case "expert": $scope.Title = "专家简介编辑"; break;
        case "news": $scope.Title = "新闻信息编辑"; break;
        case "notice": $scope.Title = "通知公告编辑"; break;
    }

    var id = $("#hdId").val();
    if(id.length>0){
        $http.get("GetEditorInfo?type="+$scope.EditType+"&Id="+id).success(function(response){
            if(response.success){
                $scope.title = response.data.title;
                $scope.content = response.data.content;
                $scope.pubOrg = response.data.pubOrg;
                $scope.author = response.data.author;
                $scope.isShow = response.data.isShow;
                $scope.isTop = response.data.isTop;
                $scope.isRoll = response.data.isRoll;
            }
        });
    }

    $scope.SubmitForm = function () {
        var form = new FormData(document.getElementById("formEditor"));
        $http({
            url: "SaveEditor",
            method: "post",
            data: form,
            transformRequest: angular.identity,
            headers: {
                'Content-Type': undefined  //angularjs设置文件上传的content-type修改方式
            }
        }).success(function (response) {
            if (response.success) {
                window.location.href = response.data;
            }
            else
                alert(response.data);
        }).error(function (message) {
            console.log(message);
        });
    }


});

module.controller('mgrEventInfoCtl', function ($scope, $http) {
    $scope.tab = "eventInfo";

    $scope.NewType = {
        Id: 0,
        ParentId: 0,
        Name: "",
        Level: "",
        Show: true,
        IsShow: 1,
        Icon: "",
        SortNo: 999
    };
    // zTree 的参数配置，深入使用请参考 API 文档（setting 配置详解）
    var setting = {
        check: {
            enable: false,
            chkStyle: "checkbox", //默认值
            nocheckInherit: true,  //新加入子节点时，自动继承父节点 nocheck = true 的属性。
            chkboxType: { "Y": "ps", "N": "ps" } //勾选 checkbox 对于父子节点的关联影响
        },
        data: {
            simpleData: {
                enable: true,
                idKey: "id",
                pIdKey: "parentId",
                rootPId: 0
            },
            key: {
                name: "name",
                title: ""
            }
        },
        view: {
            showTitle: false
        },
        callback: {
            onClick: zTreeOnClick
        }
    };
    function zTreeOnClick(event, treeId, treeNode) {

        if ($scope.tab == "eventInfo") {
            $scope.GetEventInfoList(treeNode.id);
        } else {
            //alert(treeNode.tId + ", " + treeNode.name);
            $scope.NewType.Id = treeNode.id;
            $("#hdEventTypeId").val(treeNode.id);
            $scope.NewType = {
                Id: treeNode.id,
                ParentId: treeNode.ParentId,
                Name: treeNode.Name,
                Level: treeNode.Level,
                Show: treeNode.Show,
                IsShow: $scope.NewType.Show ? 1 : 0,
                Icon: treeNode.Icon,
                SortNo: treeNode.SortNo,
            };
        }
    };


    $scope.SaveEventType = function () {
        $scope.NewType.ParentId = $("#hdEventTypeId").val();
        if ($scope.NewType.ParentId == "0") {
            alert("请选择一个赛事类别。");
            return false;
        }
        $scope.NewType.Id = 0;
        $scope.NewType.IsShow = $scope.NewType.Show ? 1 : 0;

        var url = "SaveEventType";
        $http.post(url, $scope.NewType).success(function (response) {
            if (response.success)
                $scope.GetEventTypeList();
            else
                alert(response.message);
        });
    }
    $scope.ModifyEventType = function () {
        $scope.NewType.IsShow = $scope.NewType.Show ? 1 : 0;
        var url = "ModifyEventType";
        $http.post(url, $scope.NewType).success(function (response) {
            if (response.success)
                $scope.GetEventTypeList();
            else
                alert(response.message);
        });
    }
    $scope.RemoveEventType = function () {
        var url = "RemoveEventType";
        $http.post(url, { id: $scope.NewType.Id }).success(function (response) {
            if (response.success)
                $scope.GetEventTypeList();
            else
                alert(response.message);
        });
    }

    $scope.GetEventTypeList = function () {
        $http.get("GetEventTypes").success(function (response) {
            /*
            if ($scope.tab == "eventType") {
                var zTreeObj = $.fn.zTree.init($("#ulEventTypes"), setting, response.data);
                zTreeObj.expandAll(true);
            }
            else {
                var zTreeObj = $.fn.zTree.init($("#ulTypeShower"), setting, response.data);
                zTreeObj.expandAll(true);
            }*/

            var zTreeObj1 = $.fn.zTree.init($("#ulEventTypes"), setting, response.data);
            zTreeObj1.expandAll(true);
            var zTreeObj2 = $.fn.zTree.init($("#ulTypeShower"), setting, response.data);
            zTreeObj2.expandAll(true);

        });
    }


    $scope.GetEventInfoList = function (typeId) {
        var url = "GetEventInfoList?type=" + typeId;
        $http.get(url).success(function (response) {
            $scope.EventInfoList = response.data;
        });
    }

    $scope.AddEventInfo = function () {

    };

    $scope.GetEventTypeList();


});

module.controller('mgrExpertInfoCtl', function ($scope, $http) {
    
});
module.controller('mgrMessageInfoCtl', function ($scope, $http) {
    
});
module.controller('mgrNewsInfoCtl', function ($scope, $http) {
    
});
module.controller('mgrNoticeInfoCtl', function ($scope, $http) {
    
});