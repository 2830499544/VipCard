$(document).ready(function () {
    //    if (!$("#chkIsOldPwd").attr("checked")) {
    //        $("#frmChangePwd").attr("disabled", "disabled");
    //        $("#txtOldPwd").attr("disabled", "disabled");
    //        $("#txtNewPwd").attr("disabled", "disabled");
    //        $("#txtRePwd").attr("disabled", "disabled");
    //        $('input[name="radCheckOldPwd"]').attr("disabled", "disabled");
    //        $("#txtChangePwdRemark").attr("disabled", "disabled");
    //        $("#txtFindMember").attr("disabled", "disabled");
    //        $("#btnFindMember").attr("disabled", "disabled");
    //        //        var throughBox = art.dialog.through;
    //        art.dialog({
    //            title: '系统提示',
    //            icon: 'error', //图标
    //            content: "未启用会员密码功能，不能进行密码修改！",
    //            lock: true
    //        });
    //        return false;
    //    }

    //没有查询会员时 所有空间禁用
    if ($.isEmptyObject(global_mem)) {
        $("#txtOldPwd").attr("disabled", "disabled");
        $("#txtNewPwd").attr("disabled", "disabled");
        $("#txtRePwd").attr("disabled", "disabled");
//        $('input[name="radCheckOldPwd"]').attr("disabled", "disabled");
        $("#txtChangePwdRemark").attr("disabled", "disabled");
    };
    if ($("#MemCard").val() != null) {
        $("#txtFindMember").val($("#MemCard").val());
    }

//    var radList = $("input[name='radCheckOldPwd']:");
//    for (var i = 0; i < radList.length; i++) {
//        $(radList[i]).click(function () {
//            if ($(this).val() == "0")
//                $("#trOldPwd").css("display", "none");
//            else
//                $("#trOldPwd").css("display", "");
//        });
//    };
    $("#btnChangePwdReset").bind("click", btnChangePwdReset);
    $("#btnChangePwdSave").bind("click", btnChangePwdSave);
})

//重置按钮响应函数
function btnChangePwdReset() {
    window.location.href = '../Member/MemChangePwd.aspx?PID=9';
};

//保存按钮响应函数
function btnChangePwdSave() {
    var strErrorMsg = "";
    var strOldPwd = $.trim($("#txtOldPwd").val());
    var strNewPwd = $.trim($("#txtNewPwd").val());
    var strRePwd = $.trim($("#txtRePwd").val());
    var Name = global_mem.MemName;
    var MemCard = global_mem.MemCard;

    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员！</li>";
    }
    //    if (checkPwd == "1" && strOldPwd == "") {
    //        strErrorMsg += "<li>请输入会员卡旧密码！</li>";
    //    }
    if (strNewPwd != strRePwd) {
        strErrorMsg += "<li>两次密码输入不一致，请重新输入！</li>";
    }
    if (!$("#RegNullPwd").attr("checked")) {
        if (strNewPwd == "") {
            strErrorMsg += "<li>请输入会员密码！</li>";
        }
    }
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
        content: '将为会员 [' + Name + '],修改密码。\n确定操作吗？',
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
               "ChangePwd",
               {
                   "memID": global_mem.MemID,
                   "oldPwd": strOldPwd,
                   "newPwd": strNewPwd,
                   "isOldPwd":$("#hdisOldPwd").val(),
                   "remark": $("#txtChangePwdRemark").val()
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
                                 close: function () { location.href = location.href; },
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

/****************************************************************************************************
*在选择好会员时可以执行回调函数
*****************************************************************************************************/
function FindMember_CallBack() {
    var strErrorMsg;
    if (global_mem.MemState != 0) {
        strErrorMsg = "当前会员卡处于锁定或者挂失状态，暂不允许进行修改密码。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    if (global_mem.MemIsPast == "True") {
        strErrorMsg = "当前会员卡已过期，暂不允许进行修改密码。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    //    if (global_mem.MemPassword == "") {
    //        strErrorMsg = "当前会员没有设置密码不能修改。";
    //        art.dialog.through({
    //            content: strErrorMsg,   //弹出层显示文本
    //            icon: 'error', //图标
    //            lock: true//是否锁定背景
    //        });
    //        return false;
    //    }

    if (global_mem.MemPassword == "") {
        $("#isCheckOldPwd").attr("disabled", "disabled");
    }
    else {
        $("#isCheckOldPwd").attr("disabled", "");
    }
    //查找到会员后 所有控件解禁
    if (global_mem.MemID != "") {
        $("#txtOldPwd").attr("disabled", "");
        $("#txtNewPwd").attr("disabled", "");
        $("#txtRePwd").attr("disabled", "");
//        $('input[name="radCheckOldPwd"]').attr("disabled", "");
        $("#txtChangePwdRemark").attr("disabled", "");
        $("#txtNewPwd").focus();
        if ($("#txtOldPwd").css("display")=="none") {
            $("#txtNewPwd").focus();
        }
        else {
            $("#txtOldPwd").focus();
        }
    }
    return true;
}