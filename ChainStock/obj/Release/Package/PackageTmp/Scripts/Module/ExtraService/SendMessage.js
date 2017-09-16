
$(document).ready(function () {
    $("#txtMemContent").focus();
    $('#txtMemReceiver').css("background-color", "#eee");
    $('#txtMemReceiver').attr("readonly", true);
    //选择会员按钮响应函数
    $("#btnChoose").bind("click", BtnChoose);
    //发送按钮响应函数
    $("#btnSendMessage").bind("click", BtnSendMessage);
    $("#btnMemSendMessage").bind("click", BtnMemSendMessage);
    //重置按钮响应函数
    $("#btnReset").bind("click", BtnReset);
    $("#btnMemReset").bind("click", BtnMemReset);
    //发送短信的回车事件
    $("#txtCustomContent").keypress(function (e) {
        var keyAscii = window.event ? e.keyCode : e.which;
        if (keyAscii == 13) {
            BtnSendMessage();
        }
    });
    var chooseDialog;
    //查找按钮响应函数
    $("#btnChooseMember").bind("click", BtnChooseMember);
    //确定按钮响应函数
    $("#btnOK").bind("click", BtnOK);
    //取消按钮响应函数
    $("#btnCancel").bind("click", BtnCancel);
    //查找会员的回车事件
    $("#txtQueryMem").keypress(function (e) {
        var keyAscii = window.event ? e.keyCode : e.which;
        if (keyAscii == 13) {
            BtnChooseMember();
            return false;
        }
    });
    $("#sltMemSmsTemplate").bind("onchange", checkMsgLen);
    $("#sltCustomSmsTemplate").bind("onchange", checkNotifyLen);
    $("#txtMemContent").bind("onkeyup", checkMsgLen);
    $("#txtCustomContent").bind("onkeyup", checkNotifyLen);
});

//估算短信长度
function checkMsgLen() {
    if ($("#txtMemContent").val().length > 0) {
        $("#txtMessageLen").html("估算长度：" + $("#txtMemContent").val().length);
    } else {
        $("#txtMessageLen").html("");
    }
    //setTimeout("checkMsgLen()", 2000);
}
//估算短信长度
function checkNotifyLen() {
    if ($("#txtCustomContent").val().length > 0) {
        $("#txtNotifyLen").html("估算长度：" + $("#txtCustomContent").val().length);
    } else {
        $("#txtNotifyLen").html("");
    }
    //setTimeout("checkNotifyLen()", 2000);
}
/*******************************************************************************
*选择会员按钮响应函数
*******************************************************************************/
function BtnChoose() {
    chooseDialog = art.dialog({
        title: '批量选择会员',
        content: document.getElementById('divChooseMember'),
        id: 'divChooseMember',
        lock: true
    });
}
/*******************************************************************************
*自定义发短信 发送按钮响应函数
*******************************************************************************/
function BtnSendMessage() {
    $("#btnSendMessage").attr("disabled", "disabled");
    if ($("#chkSms").attr("checked") == false) {
        art.dialog.alert("本系统未启用发送短信功能，如果要启用发送短信功能请先到增值服务参数页面启用发送短信功能！");
        return false;
    }
    var strErrorMsg = "";
    var strReceiver = $.trim($("#txtCustomReceiver").val());
    var strContent = $.trim($("#txtCustomContent").val());

    if (strReceiver.IsEmpty()) {
        strErrorMsg += "<li>自定义发短信的手机号码不能为空;</li>";
    }
    else {
        if (strReceiver.length < 11) {
            strErrorMsg += "<li>自定义发短信的手机号码位数不能少于11位;</li>";
        }
        var number = strReceiver.indexOf("，");
        if (number >= 0) {
            strErrorMsg += "<li>自定义发短信的手机号码输入框不能存在中文的逗号,第" + (eval("number") + 1) + "位字符是中文逗号;</li>";
        }
        var arrayReceiver = new Array();
        arrayReceiver = strReceiver.split(",");
        if (arrayReceiver.length > 0) {
            for (var i = 0; i < arrayReceiver.length; i++) {
                if (!arrayReceiver[i].IsMobile()) {
                    strErrorMsg += "<li>自定义发短信的手机号码格式输入错误;</li>";
                }
            }
        }
    }
    if (strContent.IsEmpty()) {
        strErrorMsg += "<li>自定义发短信的发送内容不能为空;</li>";
    } else if ((/[银銀]行/).test(strContent)) {
        strErrorMsg += "<li>短信模版中不能出现银行关键字(词);</li>";
    }
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true,
            close: function () {
                $('#btnSendMessage').removeAttr("disabled");
            }
        });
        return false;
    }
    doAjax("../",
           "SendMessage",
           {
               "strReceiver": strReceiver,
               "strContent": strContent
           },
           "json",
           function (json) {
               $('#btnSendMessage').removeAttr("disabled");
               switch (json) {
                   case 1:
                       art.dialog.alert("短信已发送！");
                       $("#txtCustomReceiver").val("");
                       $("#txtCustomContent").val("");
                       break;
                   case 2:
                       art.dialog.alert("发送失败,请重新发送！");
                       break;
                   case 3:
                       art.dialog.alert("短信不足，不能发送，请充值短信！");
                       break;
                   case 5:
                       art.dialog.alert("本店所拥有的短信量不足，发送短信失败，请与总店联系！");
                       break;
                   default:
                       art.dialog.alert("系统错误，请重新发送！");
                       break;
               }
           });
}

/*******************************************************************************
*给会员发短信 发送按钮响应函数
*******************************************************************************/
function BtnMemSendMessage() {
    $("#btnMemSendMessage").attr("disabled", "disabled");
    if ($("#chkSms").attr("checked") == false) {
        art.dialog.alert("本系统未启用发送短信功能，如果要启用发送短信功能请先到增值服务参数页面启用发送短信功能！");
        return false;
    }
    var strErrorMsg = "";
    var strMemReceiver = $.trim($("#txtMemReceiver").val());
    var strMemContent = $.trim($("#txtMemContent").val());
    if (strMemReceiver.IsEmpty()) {
        strErrorMsg += "<li>给会员发短信的手机号码不能为空,请选择会员手机号码;</li>";
    }
    if (strMemContent.IsEmpty()) {
        strErrorMsg += "<li>给会员发短信的发送内容不能为空;</li>";
    } else if ((/[银銀]行/).test(strMemContent)) {
        strErrorMsg += "<li>短信模版中不能出现银行关键字(词);</li>";
    }
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true,
            close: function () {
                $('#btnMemSendMessage').removeAttr("disabled");
            }
        });
        return false;
    }
    doAjax("../",
           "MemSendMessage",
           {
               "strMemReceiver": strMemReceiver,
               "strMemContent": strMemContent
           },
           "json",
           function (json) {
               $('#btnMemSendMessage').removeAttr("disabled");
               switch (json) {
                   case 1:
                       art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: ("短信已发送！"),
                                  lock: true
                              });
                       $("#txtMemReceiver").val("");
                       $("#txtMemContent").val("");
                       break;
                   case 2:
                       art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: ("发送失败,请重新发送！"),
                                  lock: true
                              });
                       break;
                   case 5:
                       art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: ("本店所拥有的短信量不足，发送短信失败，请与总店联系！"),
                                  lock: true
                              });
                       break;
                   default:
                       art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: ("发送失败,请重新发送！"),
                                  lock: true
                              });
                       break;
               }
           });
}


/*******************************************************************************
* 自定义发短信 重置按钮响应函数
*******************************************************************************/
function BtnReset() {
    $("#txtCustomReceiver").val("");
    $("#sltCustomSmsTemplate").val("");
    $("#txtCustomContent").val("");
}
/*******************************************************************************
* 给会员发短信 重置按钮响应函数
*******************************************************************************/
function BtnMemReset() {
    $("#txtMemReceiver").val("");
    $("#sltMemSmsTemplate").val("");
    $("#txtMemContent").val("");
}
/*******************************************************************************
*自定义发短信 选择模板
*******************************************************************************/
function CustomSmsTemplate() {
    $("#txtCustomContent").val($("#sltCustomSmsTemplate").val());
    checkNotifyLen();
}
/*******************************************************************************
* 给会员发短信 选择模板
*******************************************************************************/
function MemSmsTemplate() {
    $("#txtMemContent").val($("#sltMemSmsTemplate").val());
    checkMsgLen();
}
/****************************************************************************************
*查找按钮响应函数
****************************************************************************************/
function BtnChooseMember() {
    doAjax("../",
           "GetMemList",
           { "memCard": $("#txtQueryMem").val(),
               "memLevelID": $("#sltMemLevelID").val(),
               "memShopID": $("#sltShop").val()
           },
           "json",
           function (json) {
               if (json.length > 0) {
                   var strMobile = $.trim($("#txtMobile").val());
                   $.each(json, function (index, item) {
                       if (item.MemMobile != "" && item.MemMobile != null && strMobile.indexOf(item.MemMobile) < 0) {
                           strMobile += (item.MemName + ":" + item.MemMobile + ";");                       }
                   });
                   $("#txtMobile").val(strMobile);
                   $("#txtMobile").focus();
               }
           });
}
/****************************************************************************************
*确定按钮响应函数
****************************************************************************************/
function BtnOK() {
    var strMobile = $.trim($("#txtMobile").val());
    $("#txtMemReceiver").val(strMobile);
    chooseDialog.close();
    $("#txtMemReceiver").css("display", "");
}
/****************************************************************************************
*取消按钮响应函数
****************************************************************************************/
function BtnCancel() {
    chooseDialog.close();
}