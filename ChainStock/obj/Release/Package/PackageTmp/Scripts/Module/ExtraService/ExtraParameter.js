
$(document).ready(function () {
    IsMoneySMS();
    IsTelNoMember();
    IsMMS();
    IsMarketingSMS();
    var strSmsSerialPwd = $("#lblNotificationSMSPwd").html();
    if (strSmsSerialPwd != "") {
        $("#txtNotificationSMSPwd").val(strSmsSerialPwd);
    }
    var strMarketingSMS = $("#lblMarketingSMSPwd").html();
    if (strMarketingSMS != "") {
        $("#txtMarketingSMSPwd").val(strMarketingSMS);
    }
    var strMMSSerialPwd = $("#lblMMSSerialPwd").html();
    if (strMMSSerialPwd != "") {
        $("#txtMMSSeriesPwd").val(strMMSSerialPwd);
    }
});

function IsMarketingSMS() {
    if ($("#chkMarketingSMS").is(":checked")) {
        $('#txtMarketingSMS').attr("readonly", false);
        $('#txtMarketingSMSPwd').attr("readonly", false);
        $('#txtMarketingSMS').attr("disabled", false);
        $('#txtMarketingSMSPwd').attr("disabled", false);
    }
    else {
        $('#txtMarketingSMS').attr("readonly", true);
        $('#txtMarketingSMSPwd').attr("readonly", true);
        $('#txtMarketingSMS').attr("disabled", true);
        $('#txtMarketingSMSPwd').attr("disabled", true);
        $('#txtMarketingSMS').val("");
        $('#txtMarketingSMSPwd').val("");
    }
}
/******************************************************************
*不选择“启用系统短信功能”时，不能选择“启用系统短信(账户变动信息)自动发送功能”
******************************************************************/
function IsMoneySMS() {
    if ($("#chkSms").is(":checked")) {
        $('#chkMoneySms').attr("disabled", false);
        $('#chkIsSmsShopName').attr("disabled", false);
        $('#txtSmsShopName').attr("disabled", false);
        $('#txtNotificationSMS').attr("disabled", false);
        $('#txtNotificationSMSPwd').attr("disabled", false);

        $('#txtSmsShopName').attr("readonly", false);
        $('#txtNotificationSMS').attr("readonly", false);
        $('#txtNotificationSMSPwd').attr("readonly", false);
    }
    else {
        $('#chkMoneySms').attr("checked", false);
        $('#chkMoneySms').attr("disabled", true);

        $('#chkIsSmsShopName').attr("checked", false);
        $('#chkIsSmsShopName').attr("disabled", true);
        $('#txtSmsShopName').attr("readonly", true);
        $('#txtNotificationSMS').attr("readonly", true);
        $('#txtNotificationSMSPwd').attr("readonly", true);

        $('#txtSmsShopName').val("");
        $('#txtNotificationSMS').val("");
        $('#txtNotificationSMSPwd').val("");

        $('#txtSmsShopName').attr("disabled", true);
        $('#txtNotificationSMS').attr("disabled", true);
        $('#txtNotificationSMSPwd').attr("disabled", true);
    }
}
/*****************************************************************
*不选择“启用来电提醒功能”时，不能选择“启用非会员来电提醒功能”
******************************************************************/
function IsTelNoMember() {
    if ($("#chkTel").is(":checked") == false) {
        $('#chkTelNoMember').attr("checked", false);
        $('#chkTelNoMember').attr("disabled", true);
    }
    else {
        $('#chkTelNoMember').attr("disabled", false);
    }
}

function checkForm() {

    if (document.getElementById("chkSms").checked=="true") {
        if ($('#txtNotificationSMS').val() == "" | $('#txtNotificationSMSPwd').val() == "") {
            art.dialog({
                title: '系统提示',
                icon: 'error', //图标
                content: '请输入完整的短信序列号和密码,否则系统将无法提供短信发送功能！',
                lock: true
            });
            return false;
        }
    }
    if ($("#chkMMS").is(":checked")) {
        if ($('#txtMMSSeries').val() == "" | $('#txtMMSSeriesPwd').val() == "") {
            art.dialog({
                title: '系统提示',
                icon: 'error', //图标
                content: '请输入完整的彩信序列号和密码,否则系统将无法提供二维码发送功能！',
                lock: true
            });
            return false;
        }
    }
    else {
        return true;
    }
}

/******************************************************************
*不选择“启用系统彩信功能”时，不能输入“彩信序列号及彩信密码”
******************************************************************/
function IsMMS() {
    if ($("#chkMMS").is(":checked")) {
        $('#txtMMSSeries').attr("disabled", false);
        $('#txtMMSSeriesPwd').attr("disabled", false);
        $('#txtMMSSeries').attr("readonly", false);
        $('#txtMMSSeriesPwd').attr("readonly", false);
    }
    else {
        $('#txtMMSSeries').attr("readonly", true);
        $('#txtMMSSeriesPwd').attr("readonly", true);
        $('#txtMMSSeries').attr("disabled", true);
        $('#txtMMSSeriesPwd').attr("disabled", true);
        $('#txtMMSSeries').val("");
        $('#txtMMSSeriesPwd').val("");
    }
}
/*****************************************************************
*获取系列号的剩余短信条数
*****************************************************************/
function GetSmsBalance(type) {
    var smsType = type;
    doAjax("../",
           "GetSmsBalance",
           {
               "type": smsType,
               "NotificationSMS": $("#txtNotificationSMS").val(),
               "NotificationSMSPwd": $("#txtNotificationSMSPwd").val(),
               "MarketingSMS": $("#txtMarketingSMS").val(),
               "MarketingSMSPwd": $("#txtMarketingSMSPwd").val()
           },
           "json",
           function (json) {
               if (json.smsType == "False") {
                   $("#lblNotificationSmsOverNumber").html(json.number);
               }
               else {
                   $("#lblMarketingSMSOverNumber").html(json.number);
               }

           });
}
/*****************************************************************
*获取系列号的剩余彩信条数
*****************************************************************/
function GetMMSBalance() {
    if ($("#chkMMS").is(":checked")) {
        if ($('#txtMMSSeries').val() == "" | $('#txtMMSSeriesPwd').val() == "") {
            art.dialog({
                title: '系统提示',
                icon: 'error', //图标
                content: '请输入完整的彩信序列号和密码,否则无法查询彩信的剩余条数！',
                lock: true
            });
            return false;
        }
    }
    doAjax("../",
           "GetMMSBalance",
           {
               "MMSSeries": $("#txtMMSSeries").val(),
               "MMSSerialPwd": $("#txtMMSSeriesPwd").val()
           },
           "json",
           function (json) {
               if (json == "-1") {
                   art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("彩信序列号或者密码错误，请重新输入！"),
                              lock: true
                          });
                   return false;
               }
               $("#lblMMSOverNumber").html(json);
           });
}