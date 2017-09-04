

$(document).ready(function () {
    $("#chkAll").bind("click", chkAll);
    $("#Invert").bind("click", Invert);
})

function chkAll() {
    var inputList = $("#tbChick input[type='checkbox']");
    if ($("#chkAll").attr("checked") == true) {
        inputList.attr("checked", true);
    }
    else {
        inputList.attr("checked", false);
    }
}

function Invert() {
    $("#tbChick input[type='checkbox']").each(function () {
        $(this).attr("checked", !this.checked);
    })
}

function btnGroupDel(groupName, groupID, groupType) {
    var locationUrl;
    if (groupType ==1) {
        locationUrl = "MainGroupList.aspx?PID=32";
    }
    if (groupType == 2) {
        locationUrl = "AllianceGroupList.aspx?PID=141";
    }
    if (groupType == 3) {
        locationUrl = "ShopGroupList.aspx?PID=145";
    }
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除权限组【' + groupName + '】吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
             'GroupdDel', { "groupID": groupID }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统异常 权限组未能删除，请重试！"),
                                       lock: true
                                   });
                             break;
                         case -3:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统错误 请与系统管理员联系！"),
                                       lock: true
                                   });
                             break;
                         case -2:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("此权限组已经有用户在使用,不能删除！"),
                                       lock: true
                                   });
                             break;
                         default:
                             art.dialog
                                   ({
                                       time: 1,
                                       content: '删除成功！',
                                       close: function () { location.href = locationUrl; }
                                   });
                             break;
                     }
                 });

            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    });
    return false;
}

function changeParentGroupp() {
    var parentGroupId = $("#sltParentGroup").val();
    var groupType = $("#txtGroupType").val();
    window.location.href = "../SystemManage/GroupAuthority.aspx?PID=36&Gid=0&GroupType=" + groupType + "&ParentGroupID=" + parentGroupId;
}