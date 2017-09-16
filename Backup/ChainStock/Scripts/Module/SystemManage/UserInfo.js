// 页面加载时执行
var locationUrl;
$(document).ready(function () {
    //"保存"按键响应函数
    $("#btnUserSave").bind("click", UserSave);

    //"重置"按键响应函数
    $("#btnUserReset").bind("click", UserReset);

    $("#btnSavePwd").bind("click", SavePwd);

    $("#sltShopInfo").bind("change", BindGroupOnShopChange);
    var userType= $("#txtUserType").val();
    if (userType == 1) {
        locationUrl = "MainUserList.aspx?PID=33";
    }
    if (userType == 2) {
        locationUrl = "AllianceUserList.aspx?PID=142";
    }
    if (userType == 3) {
        locationUrl = "ShopUserList.aspx?PID=146";
    }

    BindGroupOnShopChange();
});

//"保存"修改密码
function SavePwd() {
    var strErrorMsg = "";
    var newPwdOne = $("#txtPwdOne").val();
    var newPwdTwo = $("#txtPwdTwo").val();
    if (newPwdOne == "" || newPwdTwo == "") {
        strErrorMsg += "<li>密码不能为空，请重新输入;</li>";
    }
    else {
        if (newPwdOne != newPwdTwo) {
            strErrorMsg += "<li>两次密码输入不同，请重新输入;</li>";
        }
    }
    if ($("input[name='radChooseYesOrNo']:checked").val() == "0") strErrorMsg += "<li>该用户帐号已锁定，无法修改密码;</li>";
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        //var throughBox = art.dialog.through;
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    art.dialog({
        title: '系统提示',
        content: '将为系统用户 [' + $.trim($("#txtUserName").val()) + '],修改密码。\n确定操作吗？',
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
               "ChangeUserPwd",
               {
                   "userID": $("#txtUserID").val(),
                   "userPassword": newPwdOne
               },
               "json",
                function (json) {
                    switch (json) {
                        case 0:
                            art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统异常，未保存数据，请再次点击保存！"),
                                     lock: true
                                 });
                            break;
                        case -1:
                            art.dialog
                                ({
                                    title: '系统提示',
                                    time: 4,
                                    content: ("会员旧密码输入不正确,请重新输入！"),
                                    lock: true
                                });

                            break;
                        case -2:
                            art.dialog
                             ({
                                 title: '系统提示',
                                 time: 4,
                                 content: ("系统错误 请与系统管理员联系！"),
                                 lock: true
                             });
                            break;
                        default:
                            art.dialog
                             ({
                                 title: '系统提示',
                                 time: 0.5,
                                 content: '密码已成功修改，请牢记您的新密码！',
                                 close: function () { location.href = locationUrl; },
                                 lock: true
                             });
                            break;
                    }
                });
            return false;
        },
        cancelVal: '取消',
        cancel: true
    });
}

//"保存"按键响应函数
function UserSave() {
    var strErrorMsg = "";
    var type = ($("#txtUserID").val() == "") ? "UserAdd" : "UserEdit";
    var pwd = $("#txtPassword").val();
    if ($.trim($("#txtUserAccount").val()) == "") strErrorMsg += "<li>用户账号不能为空;</li>";
    if ($.trim($("#txtUserName").val()) == "") strErrorMsg += "<li>用户名称不能为空;</li>";
    if ($("#txtPwd").val() == "" && type == "UserAdd") {
        strErrorMsg += "<li>基于安全性的考虑，请输入系统用户密码;</li>"
    }
    if (($("#txtPwd").val() != $("#txtRepwd").val()) && type == "UserAdd") strErrorMsg += "<li>两次密码输入不一致，请重新输入;</li>";

    if ($("#txtUserNumber").val() == "") {
        strErrorMsg += "<li>用户编号不能为空;</li>";
    }
    else {
        if (!/\d+/.test($("#txtUserNumber").val())) {
            strErrorMsg += "<li>用户编号是由数字组成的一个字符串;</li>";
        }
    }
    if ($("#txtUserNumber").val().length < 3) {
        strErrorMsg += "<li>用户编号最小长度为3位最长为20位;</li>";
    }

    var tel = $.trim($("#txtUserTel").val());
    if (tel != "" && !tel.IsNumber()) strErrorMsg += "<li>联系电话只能为数字且不能为空;</li>"
    if ($("#sltShopInfo").val() == "") strErrorMsg += "<li>请选择商家;</li>";
    if ($("#sltGroupID").val() == "") strErrorMsg += "<li>请选择管理权限;</li>";
    if ($("input[name='radChooseYesOrNo']:checked").val() == "") strErrorMsg += "<li>请选择是否锁定;</li>";

    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        //var throughBox = art.dialog.through;
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }

    art.dialog({
        title: '系统提示',
        content: "将" + (type == "UserAdd" ? "增加" : "修改") + "用户 [" + $.trim($("#txtUserName").val()) + "]。\n确定操作吗？" + (type == "UserAdd" ? "(用户增加成功，则不可删除，请仔细核对信息)" : "") + "",
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
         type,
        {
            "userID": $("#txtUserID").val(),
            "userName": $.trim($("#txtUserName").val()),
            "userAccount": $.trim($("#txtUserAccount").val()),
            "userTel": $.trim($("#txtUserTel").val()),
            "userPassword": $("#txtPwd").val(),
            "userShopID": $("#sltShopInfo").val(),
            "authority": $("#sltGroupID").val(),
            "isChoose": $("input[name='radChooseYesOrNo']:checked").val(),
            "userRemark": $("#txtUserRemark").val(),
            "userNumber": $("#txtUserNumber").val()
        },
        "json",
         function (json) {
             switch (json) {
                 case 0:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("系统异常，未保存数据，请再次点击保存！"),
                              lock: true
                          });
                     break;
                 case -3:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("系统错误 请与系统管理员联系！"),
                              lock: true
                          });
                     break;
                 case -2:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("操作员不允许锁定自己！"),
                              lock: true
                          });
                     break;
                 case -1:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("此用户账号已经在系统中存在，请重新输入用户账号，然后重试！"),
                              lock: true
                          });
                     break;
                 case -4:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("此用户编号已经在系统中存在，请重新输入用户编号，然后重试！"),
                              lock: true
                          });
                     break;
                 default:
                     art.dialog
                            ({
                                time: 0.5,
                                content: '保存成功！',
                                close: function () { location.href = locationUrl; },
                                lock: true
                            });
                     break;
             }
         });
        },
        cancelVal: '取消',
        cancel: true
    });


}

//商家变化时重新绑定权限组
function BindGroupOnShopChange() {
    var shopID = $("#sltShopInfo").val();
    var groupID = $("#txtGroupID").val();
    var groupType = $("#txtUserType").val();
    $("#sltGroupID").empty();
    doAjax("../",
             'GetGroupListByShopAndGroupType', { "shopID": shopID, "groupID": groupID, "GroupType": groupType }, "json",
                 function (json) {
                     if (json != null) {
                         for (var i = 0; i < json.length; i++) {
                             $("#sltGroupID").append("<option value=" + json[i]["GroupID"] + ">" + json[i]["GroupName"] + "</option>");
                         }
                     }
                     BindGroupWithEdit();
                 })
}

//编辑用户时，选择用户后的回调方法，为了修改编辑用户框中的所属角色
//只有在选择用户时才会触发
function BindGroupWithEdit() {
    if ($("#txtEditGroupID").val() != "") {
        $("#sltGroupID").val($("#txtEditGroupID").val());
        $("#txtEditGroupID").val("");
    }
}

//重置按钮响应事件
function UserReset() {
    $('#txtUserNumber').attr("readonly", false);
    $('#txtUserAccount').attr("readonly", false);
    if ($("#UserAddOrEdit").val() == 0) {
        $("#txtUserAccount").val("");
        $("#txtUserNumber").val("");
        $("#txtUserName").val("");
        $("#txtUserID").val("");
        $("#txtPwd").val("");
        $("#txtPassword").val("");
        $("#txtRepwd").val("");
        $("#txtUserRemark").val("");
        $("#txtUserTel").val("");
        $("#radChooseNo").attr("checked", true);
       
        $("#sltGroupID").val($("#txtGroupID").val());
    }
    else if ($("#UserAddOrEdit").val() == 1) {
        UserEdit($("#txtUserName").val(), $("#txtUserID").val());
    }
}

function UserAdd() {
    $('#txtUserNumber').attr("readonly", false);
    $('#txtUserAccount').attr("readonly", false);
    $('#txtUserNumber').css("background-color", "#ffffff");
    $('#txtUserAccount').css("background-color", "#ffffff");
    $("#radChooseNo").attr("checked", true);
    $("#userInfoPwd").css("display", "");
   
    art.dialog({
        lock: true,
        title: '用户新增',
        content: document.getElementById('UserInfo'),
        id: 'UserInfo',
        close: function () { UserReset(); }
    });
    $("#UserAddOrEdit").val(0);
    $("#txtUserAccount").focus();
}

function UserEditPwd(username, userid) {
    $("#txtPwdOne").val("");
    $("#txtPwdTwo").val("");
    art.dialog({
        lock: true,
        title: '用户修改密码',
        content: document.getElementById('EditPwd'),
        id: 'EditPwd',
        close: function () { $("#UserAddOrEdit").val(0); UserReset(); }
    });
    doAjax("../",
             'GetUserInfo', { "userID": userid }, "json",
                 function (json) {
                     $("#txtUserAccount").val(json.UserAccount);
                     $("#txtUserName").val(json.UserName);
                     $("#txtUserNumber").val(json.UserNumber);
                     $("#txtUserID").val(json.UserID);
                     $("#txtUserTel").val(json.UserTelephone);
                     ("#sltShopInfo").val(json.UserShopID);
                     $("#sltGroupID").val(json.UserGroupID);
                     if (json.UserLock == "True") {
                         $("#radChooseYes").attr("checked", true);
                     }
                     else {
                         $("#radChooseNo").attr("checked", true);
                     }
                 })
}

function UserEdit(username, userid) {
    $('#txtUserNumber').attr("readonly", true);
    $('#txtUserAccount').attr("readonly", true);
    $('#txtUserNumber').css("background-color", "#eee");
    $('#txtUserAccount').css("background-color", "#eee");
    $("#UserAddOrEdit").val(1);
    $("#userInfoPwd").css("display", "none");
    art.dialog({
        lock: true,
        title: '用户编辑',
        content: document.getElementById('UserInfo'),
        id: 'UserInfo',
        close: function () { $("#UserAddOrEdit").val(0); UserReset(); }
    });
    doAjax("../",
             'GetUserInfo', { "userID": userid }, "json",
                 function (json) {
                     $("#txtUserAccount").val(json.UserAccount);
                     $("#txtUserName").val(json.UserName);
                     $("#txtUserNumber").val(json.UserNumber);
                     $("#txtUserID").val(json.UserID);
                     $("#txtUserTel").val(json.UserTelephone);
                     $("#sltShopInfo").val(json.UserShopID);
                     $("#txtEditGroupID").val(json.UserGroupID);
                     BindGroupOnShopChange();
                     $("#txtUserRemark").val(json.UserRemark);
                     if (json.UserLock == "True") {
                         $("#radChooseYes").attr("checked", true);
                     }
                     else {
                         $("#radChooseNo").attr("checked", true);
                     }
                 })
}

function UserDel(username, userid) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除用户【' + username + '】吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
             'UserDel', { "UserID": userid }, "json",
                 function (json) {
                     switch (json.result.toString()) {
                         case "0":
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统异常 记录未能删除，请重试！"),
                                       lock: true
                                   });
                             break;
                         case "-3":
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统错误 请与系统管理员联系！"),
                                       lock: true
                                   });
                             break;
                         case "1":
                             art.dialog
                                   ({
                                       time: 2,
                                       content: '删除成功！',
                                       close: function () { location.href = locationUrl; }
                                   });
                             break;
                         default:
                             strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + json.result.toString() + "</ul>";
                             art.dialog({
                                 title: '系统提示',
                                 icon: 'error', //图标
                                 content: strErrorMsg,
                                 lock: true
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