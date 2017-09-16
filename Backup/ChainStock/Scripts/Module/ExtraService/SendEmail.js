$(document).ready(function () {
    $('#txtMemEmail').css("background-color", "#eee");
    $('#txtMemEmail').attr("readonly", true);
    //选择会员按钮响应函数
    $("#btnChoose").bind("click", BtnChoose);
    //发送按钮响应函数
    $("#btnSendEmail").bind("click", BtnSendEmail);
    //重置按钮响应函数
    $("#btnReset").bind("click", BtnReset);
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

    if ($("#isEmail").val() == "1") {
        art.dialog
              ({
                  title: '系统提示',
                  time: 2,
                  content: ("暂不允许发送邮件，请完善邮件设置！"),
                  lock: true
              });
    }
})

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

function BtnSendEmail() {
    $("#btnSendEmail").attr("disabled", "disabled");
    var strErrorMsg = "";
    var strMemEmail = $.trim($("#txtMemEmail").val());
    var customEmail = $("#txtCustomEmail").val();
    var strTitle = $("#txtTitle").val();
    var strMemContent = $.trim($("#txtEmailContent").val());
    if (strMemEmail == "" && customEmail == "") {
        strErrorMsg += "<li>给会员发邮件的地址不能为空,请选择会员电子邮箱或者自定义输入电子邮箱;</li>";
    }
    if (customEmail != "") {
        var number = customEmail.indexOf("，");
        if (number >= 0) {
            strErrorMsg += "<li>自定义发邮件的输入框不能存在中文的逗号,第" + (eval("number") + 1) + "位字符是中文逗号;</li>";
        }
        var arrayEmail = new Array();
        arrayEmail = customEmail.split(",");
        if (arrayEmail.length > 0) {
            for (var i = 0; i < arrayEmail.length; i++) {
                if (!arrayEmail[i].IsEmail()) {
                    strErrorMsg += "<li>自定义发邮件的邮箱格式输入错误;</li>";
                }
            }
        }
    }
    if (strTitle.IsEmpty()) {
        strErrorMsg += "<li>给会员发邮件的标题不能为空,请输入电子邮件的标题;</li>";
    }
    if (strMemContent.IsEmpty()) {
        strErrorMsg += "<li>给会员发邮件的内容不能为空,请输入电子邮件的内容;</li>";
    }
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        $("#btnSendMessage").attr("disabled", "");
        return false;
    }

    doAjax("../",
           "SendEmail",
           {
               "strMemEmail": strMemEmail,
               "customEmail": customEmail,
               "strTitle": strTitle,
               "strMemContent": strMemContent
           },
           "json",
           function (json) {
               $("#btnSendEmail").attr("disabled", "");
               switch (json) {
                   case -1:
                       art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: ("邮件未发送，请重新发送！"),
                                  lock: true
                              });
                       break;
                   case 0:
                       art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: ("邮件未发送，请重新发送！"),
                                  lock: true
                              });
                       break;
                   default:
                       art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: ("邮件已经增加到发送队列，请稍后！"),
                                  lock: true,
                                  close: function () {
                                      BtnReset();
                                  }
                              });

                       break;
               }
           });
}

function BtnReset() {
    $("#txtMemEmail").val("");
    $("#txtCustomEmail").val("");
    $("#txtTitle").val("");
    $("#txtEmailContent").val("");
    $("#txtMemEmail").css("display", "none");
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
                   $.each(json, function (index, item) {
                       if (item.MemEmail != "" && item.MemEmail != null) {
                           var strEmail = $.trim($("#txtEmail").val());
                           strEmail += (item.MemName + ":" + item.MemEmail + ";");
                           $("#txtEmail").val(strEmail);
                           $("#txtEmail").focus();
                       }
                   });
               }
           });
}
/****************************************************************************************
*确定按钮响应函数
****************************************************************************************/
function BtnOK() {
    var strEmail = $.trim($("#txtEmail").val());
    if (strEmail != "") {
        $("#txtMemEmail").val(strEmail);
        $("#txtMemEmail").css("display", "");
    }
    chooseDialog.close();

}

/****************************************************************************************
*取消按钮响应函数
****************************************************************************************/
function BtnCancel() {
    chooseDialog.close();
}