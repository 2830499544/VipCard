$(document).ready(function () {
    $("#btnSendCardSave").bind("click", SendCardSave);
    $("#btnSendCardReset").bind("click", SendCardReset);
});

function SendCardSave() {
    var cardstart = $("#txtStart").val();
    var cardend = $("#txtEnd").val();
    var memstate = $("#sltMemNewState").val();
    var remark = $("#txtRemark").val();
    var delcard = $("#txtDelCard").val();
    var pwd1 = $("#txtPwd1").val();
    var pwd2 = $("#txtPwd2").val();
    var memmoney = $("#txtMenMoney").val();
    var mempoint = $("#txtMemPoint").val();
    var YesOrNoPwd=$("#YesOrNoPwd").val();
    var strErrorMsg = "";
    if (cardstart == "") {
        strErrorMsg += "<li>请输入起始卡号！</li>";
    }
    else {
        if (!cardstart.IsNumber()) {
            strErrorMsg += "<li>起始卡号必须是一串数字组成！</li>";
        }
        else {
            if (cardstart.length < 4) {
                strErrorMsg += "<li>起始卡号必须4~20位;</li>";
            }
        }
    }
    if (cardend == "") {
        strErrorMsg += "<li>请输入结束卡号！</li>";
    }
    else {
        if (!cardend.IsNumber()) {
            strErrorMsg += "<li>结束卡号必须是一串数字组成！</li>";
        }
        else {
            if (cardend.length < 4) {
                strErrorMsg += "<li>结束卡号必须4~20位;</li>";
            }
        }
    }
    if (cardstart.IsNumber() && cardend.IsNumber()) {
        if (cardstart >= cardend) {
            strErrorMsg += "<li>结束卡号必须大于开始卡号;</li>";
        }
    }
    if (cardstart.length != cardend.length) {
        strErrorMsg += "<li>开始卡号长度与结束卡号长度必须相同;</li>";
    }
    if (pwd1 == "" && YesOrNoPwd!="1") {
        strErrorMsg += "<li>请输入会员密码;</li>";
    }
    else {
        if (pwd1 != pwd2) strErrorMsg += "<li>两次输入的密码不一样;</li>";
    }
    if (memmoney != "" && !memmoney.IsDecimal()) strErrorMsg += "<li>会员余额格式输入不正确;</li>";
    if (mempoint != "" && !mempoint.IsNumber()) strErrorMsg += "<li>会员积分格式输入不正确;</li>";
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    else {
        $('#btnSendCardSave').unbind('click');
        art.dialog({
            id: 'KDf435',
            title: '正在发卡',
            content: $("#Sendloading").html(),
            lock: true,
            cancel: false
        });
        doAjax("../",
                    "SendCards",
                    {
                        "cardstart": cardstart,
                        "cardend": cardend,
                        "memstate": memstate,
                        "remark": remark,
                        "delcard": delcard,
                        "pwd": pwd1,
                        "memlevel": $("#sltMemLevelID").val(),
                        "mempoint": mempoint,
                        "memmoney": memmoney
                    },
                    "json",
                    function (json) {
                        art.dialog.list['KDf435'].close();
                        switch (json) {
                            case 0:
                                $("#btnSendCardSave").bind("click", SendCardSave);
                                art.dialog({
                                    title: '系统提示',
                                    icon: 'error', //图标
                                    content: "发卡失败！请确认所选卡号在发卡范围内！",
                                    lock: true
                                });
                                break;
                            case 1:
                                $("#btnSendCardSave").bind("click", SendCardSave);
                                art.dialog({
                                    title: '系统提示',
                                    time: 4,
                                    content: '批量发卡成功！',
                                    close: function () { location.href = location.href; },
                                    lock: true
                                });
                                break;
                            default:
                                art.dialog({
                                    title: '系统提示',
                                    time: 4,
                                    content: '系统异常，发卡失败！',
                                    lock: true
                            });
                    }
                });
    }
}

function SendCardReset() {
    $("#sltMemNewState").val("0");
    $("#txtStart").val("");
    $("#txtEnd").val("");
    $("#txtDelCard").val("");
    $("#txtRemark").val("");
}