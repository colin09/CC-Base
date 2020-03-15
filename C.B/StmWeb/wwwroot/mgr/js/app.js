//var module = angular.module('managerApp', ['ngRoute', 'ui.calendar', 'ui.bootstrap','ngSanitize']);

var module = angular.module('managerApp', ['ngSanitize', "ngWaterfall"]);

module.filter('filterKey', function ($filter) {
    return function (items, value) {
        var list = $filter("filter")(items, { "parentId": value }, true);
        return list;
    };
});
module.controller("mgrNavs", function ($scope, $http) {
    $scope.getNavs = function () {
        var url = "../../Sys/Manager/GetUserNavs";
        $http.get(url).success(function (response) {
            if (response.success) {

                // $scope.navList = response.data.filter(function (item) { item.parentId == 0 });

                $scope.navList = response.data;
            } else
                alert(response.message);
        });
    }
    $scope.getNavs();
});

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
module.controller('mgrUserCtl', function ($scope, $http) {
    $scope.Title = "系统用户";

    $scope.newUser = {
        AuthType: 2,
        State: 1,
        UserName: "",
        TrueName: "",
        Department: ""
    };
    $scope.AddNewUser = function () {
        var url = "../../Sys/Manager/CreateSysUsers";
        $http.post(url, $scope.newUser).success(function (response) {
            $scope.GetUserList();
        });
    }

    $scope.GetUserList = function () {
        var url = "../../Sys/Manager/GetUsersByPage";
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
                switch (type) {
                    case "image":
                        $scope.ResourceImgList = response.data;
                        break;
                    case "video":
                        $scope.ResourceVioList = response.data;
                        break;
                    case "doc":
                        $scope.ResourceDocList = response.data;
                        break;
                }

                window.setTimeout(function () {

                    $("select.imgPickerImg").imagepicker({
                        hide_select: false,
                    });
                    $("select.imgPickerVio").imagepicker({
                        hide_select: true,
                    });
                    $("select.imgPickerDoc").imagepicker({
                        hide_select: true,
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
        if ($scope.tab != "image")
            files = $("#chkFileUrl").val();
        var callback = $("#hdCallback").val();
        //window.opener.CKEDITOR.tools.callFunction(files, callback, "");

        window.location.href = "Select?urls=" + files + "&callback=" + callback;
    }


    $scope.setFilCheck = function (file) {
        // file.fileType;
        // file.url;
        $("#chkFileUrl").val(file.url);
    }

    /**
     * window.parent.CKEDITOR.tools.callFunction(imgUrl, callback, "");
     */
});


module.controller('mgrResourceInfoCtl', function ($scope, $http) {
    $scope.tab = "image";
    $scope.GetInfoList = function (type) {
        var url = "../../Sys/File/Browse?type=" + type;
        $http.get(url).success(function (response) {
            if (response.success) {
                if ($scope.tab == "doc")
                    $.each(response.data, function (index, item) {
                        item.url = "../../mgr/images/doc.jpg";
                    });
                else if ($scope.tab == "video")
                    $.each(response.data, function (index, item) {
                        item.url = "../../mgr/images/video2.jpg";
                    });
                $scope.ResourceList = response.data;
            }
        });
    };


    $scope.$watch('tab', function (newVal, oldVal) {
        $scope.GetInfoList(newVal);
    });

    $scope.GetInfoList($scope.tab);


});
module.controller('mgrEditorCtl', function ($scope, $http) {

    // event , expert , news , notice
    $scope.EditType = $("#hdType").val();
    $scope.Title = "";
    switch ($scope.EditType) {
        case "event": $scope.Title = "赛事信息编辑"; break;
        case "expert": $scope.Title = "专家简介编辑"; break;
        case "news": $scope.Title = "新闻信息编辑"; break;
        case "notice": $scope.Title = "通知公告编辑"; break;
    }

    var id = $("#hdId").val();
    var typeId = $("#hdTypeId").val();
    if (Number(id) > 0 || Number(typeId) > 0) {
        $http.get("GetEditorInfo?type=" + $scope.EditType + "&typeId=" + typeId + "&Id=" + id).success(function (response) {
            if (response.success && response.data != null) {
                $("#hdId").val(response.data.id);
                $("#hdTypeId").val(response.data.typeId);

                $scope.title = response.data.title;
                $scope.newsType = response.data.newsType;
                $scope.eventType = response.data.typeId;
                $scope.eventTypeName = response.data.typeName;
                $scope.content = response.data.content;
                CKEDITOR.instances['editor1'].setData(response.data.content);
                $scope.pubOrg = response.data.pubOrg;
                $scope.pubTime = response.data.pubTime;
                $scope.author = response.data.author;
                $scope.isShow = response.data.isShow;
                $scope.isTop = response.data.isTop;
                $scope.isRoll = response.data.isRoll;
            } else {
                $scope.isShow = true;
                $scope.isTop = false;
                $scope.isRoll = false;
            }
        });
    }

    $scope.SubmitForm = function () {
        $scope.content = CKEDITOR.instances['editor1'].getData();
        window.setTimeout(function () { $scope.SubmitData() }, 100);
    }

    $scope.SubmitData = function () {
        console.log($scope.content);
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
                alert(response.message);
        }).error(function (message) {
            console.log(message);
        });
    }



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
    $scope.ShowEventTypeDialog = function () {
        $http.get("GetEventTypes").success(function (response) {
            var zTreeObj1 = $.fn.zTree.init($("#ulEventTypes"), setting, response.data);
            zTreeObj1.expandAll(true);
            $("#myModal").show();
        });

    }
    function zTreeOnClick(event, treeId, treeNode) {

        $scope.chkEventTypeName = treeNode.name;
        $scope.chkEventType = treeNode.id;
    };

    $scope.ChkEventTypeOver = function () {
        $scope.eventTypeName = $scope.chkEventTypeName;
        $scope.eventType = $scope.chkEventType;
        $("#hdTypeId").val($scope.chkEventType);
    }

});

module.controller('mgrEventInfoCtl', function ($scope, $http) {
    $scope.tab = "eventInfo";
    $scope.eventInfoType = "";

    $scope.NewType = {
        Id: 0,
        ParentId: 0,
        Name: "",
        // Level: "",
        Show: true,
        IsShow: 1,
        // Icon: "",
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
            $scope.eventInfoType = treeNode.id;
            $scope.GetEventInfoList(treeNode.id);
            $scope.EventTypeName = treeNode.name;
            $scope.EventTypeId = treeNode.id;
        } else {
            //alert(treeNode.tId + ", " + treeNode.name);
            $scope.NewType.Id = treeNode.id;
            $("#hdEventTypeId").val(treeNode.id);
            $scope.NewType.Id = treeNode.id;
            $scope.NewType.ParentId = treeNode.parentId;
            $scope.NewType.Name = treeNode.name;
            $scope.NewType.Show = treeNode.isShow == 1;
            $scope.NewType.SortNo = treeNode.sortNo;

            $scope.$apply();
            // $scope.NewType = {
            //     Id: treeNode.id,
            //     ParentId: treeNode.ParentId,
            //     Name: treeNode.Name,
            //     Level: treeNode.Level,
            //     Show: treeNode.Show,
            //     IsShow: $scope.NewType.Show ? 1 : 0,
            //     Icon: treeNode.Icon,
            //     SortNo: treeNode.SortNo,
            // };
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
        var url = "EditEventType";
        $http.post(url, $scope.NewType).success(function (response) {
            if (response.success)
                $scope.GetEventTypeList();
            else
                alert(response.message);
        });
    }
    $scope.RemoveEventType = function () {
        var url = "DeleteEventType?id=" + $scope.NewType.Id;
        $http.get(url).success(function (response) {
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
        window.location.href = "../../Sys/Info/Editor?type=1&typeId=" + $scope.EventTypeId;
    };

    $scope.GetEventTypeList();


    $scope.toModify = function (id) {
        window.location.href = "../../Sys/Info/Editor?type=1&typeId=0&Id=" + id;
    }
    $scope.toDelete = function (id) {
        $http.get("../../Sys/Info/DeleteEditorInfo?type=event&Id=" + id).success(function (response) {
            if (response.success)
                $scope.GetEventInfoList($scope.eventInfoType);
            else alert("删除失败。");
        });
    };

    $scope.GetEventArea = function () {
        $http.get("../../Sys/Info/GetEventArea").success(function (response) {
            if (response.success)
                $("#txtEventArea").val(response.data);
        });
    };

    $scope.SaveEventArea = function () {
        var url = "../../Sys/Info/SaveEventArea";
        var request = { key1: "eventArea", key2: $("#txtEventArea").val() };
        $http.post(url, request).success(function (response) {
            if (response.success) {
                alert("保存成功！");
            } else
                alert("保存失败！");
        });
    };

});

module.controller('mgrExpertInfoCtl', function ($scope, $http) {
    $scope.GetInfoList = function () {
        var url = "../../Sys/Info/GetExpertInfoList";
        $http.get(url).success(function (response) {
            if (response.success) {
                $scope.InfoList = response.data;
            }
        });
    };

    $scope.GetInfoList();

    $scope.toModify = function (id) {
        window.location.href = "../../Sys/Info/Editor?type=2&typeId=0&Id=" + id;
    }
    $scope.toDelete = function (id) {
        $http.get("../../Sys/Info/DeleteEditorInfo?type=expert&Id=" + id).success(function (response) {
            if (response.success)
                $scope.GetInfoList();
            else alert("删除失败。");
        });
    };
});


module.controller('mgrMessageInfoCtl', function ($scope, $http) {
    t4rfedsa
    $scope.GetInfoList = function () {
        var url = "../../Sys/Info/GetMessageList";
        $http.get(url).success(function (response) {
            if (response.success) {
                $scope.InfoList = response.data;
            }
        });
    };

    $scope.GetInfoList();

    $scope.toModify = function (id) {
        window.location.href = "../../Sys/Info/Editor?type=5&typeId=0&Id=" + id;
    }

    $scope.toDelete = function (id) {
        $http.get("../../Sys/Info/DeleteEditorInfo?type=message&Id=" + id).success(function (response) {
            if (response.success)
                $scope.GetInfoList();
            else alert("删除失败。");
        });
    };


    $scope.modifyShow = function (id) {
        $http.get("../../Sys/Info/ModifyMessageShow?Id=" + id).success(function (response) {
            if (response.success)
                $scope.GetInfoList();
            else alert("操作失败。");
        });
    };

    $scope.ShowReplyDialog = function (message) {
        $("#modelTitle").text(message.region);
        $("#modelLabel").text(message.name);
        $("#modelContent").val(message.content);
        $("#hdModelId").val(message.id);

        $("#replyModal").show();
    }

    $scope.SaveReply = function () {
        var request = {
            num1: $("#hdModelId").val(),
            key1: $("#modelReply").val(),
            flag1: $('#chkIsShow').prop('checked')
        };
        var url = "../../Sys/Info/SaveMessageReply";
        $http.post(url, request).success(function (response) {
            if (response.success)
                console.log("success");
        });
    }

});
module.controller('mgrNewsInfoCtl', function ($scope, $http) {
    $scope.tab = "eventNews";

    $scope.GetInfoList = function () {
        var type = $scope.tab == "imageNews" ? 2 : $scope.tab == "videoNews" ? 3 : 1;
        var url = "../../Sys/Info/GetNewsInfoList?type=" + type;
        $http.get(url).success(function (response) {
            if (response.success) {
                $scope.InfoList = response.data;
            }
        });
    };

    $scope.GetInfoList();

    $scope.$watch('tab', function (newVal, oldVal) {
        $scope.GetInfoList();
    });

    $scope.toModify = function (id) {
        window.location.href = "../../Sys/Info/Editor?type=3&typeId=0&Id=" + id;
    }
    $scope.toDelete = function (id) {
        $http.get("../../Sys/Info/DeleteEditorInfo?type=news&Id=" + id).success(function (response) {
            if (response.success)
                $scope.GetInfoList();
            else alert("删除失败。");
        });
    };

});
module.controller('mgrNoticeInfoCtl', function ($scope, $http) {
    $scope.GetInfoList = function () {
        var url = "../../Sys/Info/GetNoticeList";
        $http.get(url).success(function (response) {
            if (response.success) {
                $scope.InfoList = response.data;
            }
        });
    };
    $scope.GetInfoList();

    $scope.toModify = function (id) {
        window.location.href = "../../Sys/Info/Editor?type=4&typeId=0&Id=" + id;
    }

    $scope.toDelete = function (id) {
        $http.get("../../Sys/Info/DeleteEditorInfo?type=notice&Id=" + id).success(function (response) {
            if (response.success)
                $scope.GetInfoList();
            else alert("删除失败。");
        });
    };



});


module.controller('mgrPasswordCtl', function ($scope, $http) {

    $scope.Title = "修改密码";

    $scope.ModifyPwd = function () {
        var pwd = $("#txtPassword").val();
        var newPwd = $("#txtNewPassword").val();
        var chkPwd = $("#txtChkPassword").val();
        if (newPwd != chkPwd) {
            alert("密码不匹配。");
        }
        if (newPwd.length < 6)
            alert("密码长度不能少于6位。");

        $http.post("../../Sys/Manager/ModifyPwd", { key1: pwd, key2: newPwd }).success(function (response) {
            if (response.success)
                alert("修改成功");
            else
                alert("修改失败");
        });
    }
});


module.controller('mgrNavCtl', function ($scope, $http) {
    $scope.Title = "System Nav";
    $scope.navigate = {
        Id: 0, ParentId: 0, NavType: 1, Title: "", NavCode: "", Desc: "",
        Icon: "", Url: "", Module: "", Target: "", Sort: 999
    };

    // zTree 的参数配置，深入使用请参考 API 文档（setting 配置详解）
    var setting = {
        check: {
            enable: true,
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
                name: "title",
                title: "url",
                url: ""
            }
        },
        view: {
            showTitle: true
        },
        callback: {
            onClick: zTreeOnClick
        }
    };
    function zTreeOnClick(event, treeId, treeNode) {

        $scope.navigate.Id = treeNode.id;
        // $("#hdNavId").val(treeNode.id);
        // $scope.navigate = treeNode;

        $scope.navigate.ParentId = treeNode.parentId;
        $scope.navigate.Title = treeNode.title;
        $scope.navigate.NavType = treeNode.navType;
        $scope.navigate.NavCode = treeNode.navCode;
        $scope.navigate.Desc = treeNode.desc;
        $scope.navigate.Icon = treeNode.icon;
        $scope.navigate.Url = treeNode.url;
        $scope.navigate.Module = treeNode.module;
        $scope.navigate.Target = treeNode.target;
        $scope.navigate.Sort = treeNode.sort;

        $scope.$apply();
    }

    $scope.getAuthNavs = function () {
        $http.get("GetAuthNavs").success(function (response) {
            var zTreeObj1 = $.fn.zTree.init($("#ulNavs"), setting, response.data);
            zTreeObj1.expandAll(true);
        });
    }
    $scope.getAuthNavs();

    $scope.SaveNav = function (root) {
        if (root === 0) {
            $scope.navigate.ParentId = 0;
            $scope.navigate.Id = 0;
        }
        else if (root === 999) {
            $scope.navigate.Id = 0;
        } else {
            $scope.navigate.ParentId = $scope.navigate.Id;
            $scope.navigate.Id = 0;
        }
        $http.post("SaveAuthNav", $scope.navigate).success(function (response) {
            if (response.success) {
                alert("保存成功！");
                $scope.getAuthNavs();
            } else
                alert("保存失败！");
        });
    }
    $scope.modifyNav = function () {
        $http.post("SaveAuthNav", $scope.navigate).success(function (response) {
            if (response.success) {
                alert("保存成功！");
                $scope.getAuthNavs();
            } else
                alert("保存失败！");
        });
    }

});


module.controller('mgrRoleNavCtl', function ($scope, $http) {
    $scope.Title = "Role Nav Manage";
    $scope.newRole = {
        id: 0,
        AuthRoleType: 2,
        AuthRoleName: "",
        AuthRoleCode: ""
    };
    $scope.pager = { pageIndex: 1, pageSize: 20, pageCount: 1, totalCount: 0 };

    $scope.addAuthRole = function () {
        var url = "SaveAuthRole";
        $http.post(url, $scope.newRole).success(function (response) {
            $scope.getAuthRoles();
            if (response.success)
                alert("成功！");
            else
                alert(response.message);
        });
    }

    $scope.getAuthRoles = function () {
        var url = "GetAuthRoleByPage";
        var request = {};
        $http.get(url).success(function (response) {
            if (response.success) {
                $.each(response.data, function (index, item) {

                });
                $scope.authRoleList = response.data;
            } else
                alert(response.message);
        });
    }


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

    $scope.getAllNavs = function (roleId) {
        $http.get("GetAuthNavs").success(function (response) {
            if (response.success) {
                var zTreeObj1 = $.fn.zTree.init($("#ulRoleNavs"), setting, response.data);
                zTreeObj1.expandAll(true);
            } else
                alert(response.message);
        });
    }

    $scope.getAuthRoleNavs = function (roleId) {
        $http.get("GetAuthRoleNavs?authRoleId=" + roleId).success(function (response) {
            if (response.success) {
                var zTreeObj1 = $.fn.zTree.init($("#ulRoleNavs"), setting, response.data);
                zTreeObj1.expandAll(true);
            } else
                alert(response.message);
        });
    }

    $scope.setAuthRoleNavs = function () {
        var request = [];
        $http.post("SaveAuthRoleNavs", $scope.newRole).success(function (response) {
            if (response.success)
                alert("成功！");
            else
                alert(response.message);
        });
    }
});


module.controller('mgrUserNavCtl', function ($scope, $http) {
    $scope.Title = "User Nav Manage";

    $scope.newUser = {
        UserName: "",
        TrueName: "",
        MobileNo: 2,
        EMail: 2,
        AuthRoleType: 2,
        AuthRoleId: 1,
    };
    $scope.AddNewUser = function () {
        var url = "SaveAuthUser";
        $http.post(url, $scope.newUser).success(function (response) {
            $scope.GetUserList();
        });
    }

    $scope.getAuthUsers = function () {
        var url = "GetAuthUserByPage";
        $http.get(url).success(function (response) {
            if (response.success) {
                $scope.AuthUserList = response.data;
            } else
                alert(response.message);
        });
    }
    $scope.GetUserList();
});


module.controller('mgrWorksCtl', function ($scope, $http){

    $scope.getWorkList = function(){
        var url = "GetWorkList";
        $http.get(url).success(function (response) {
            if (response.success) {
                $scope.workList = response.data;
            } else
                alert(response.message);
        });


        
    }
});