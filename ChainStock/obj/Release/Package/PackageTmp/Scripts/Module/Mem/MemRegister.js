$(document).ready(function () {
    $("#txtMemCard").focus();

    //会员生日绑定日期控件
    $('#txtMemBirthday').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });

    if ($('#txtMemID').val() == "") {
        //会员办卡时间
        $('#txtMemCreateTime').bind("focus click", function () {
            WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: false, readOnly: true });
        });
    }
    //如果没有启用发送短信功能，隐藏发送短信
    //    if (!$("#chkIsSMS").attr("checked")) {
    //        $("#lblIsSMS").css("display", "none");
    //    }
    //默认的情况下生成二维码的框框不显示
    $("#trTitle").css("display", "none");
    $("#trQrCode").css("display", "none");

    if ($("#txtMemID").val() != "") {
        //编辑会员时，隐藏发送短信选择框
        $("#lblIsSMS").css("display", "none");
        $('#chkSMS').attr("checked", "");
        $("#lblIsMMS").css("display", "none");
        $('#chkMMS').attr("checked", "");
        $("#btnQRCode").css("display", "none");
        $("#trTitle").css("display", "");
        $("#trQrCode").css("display", "");

        $("#chkRegisterStaff").attr("checked", "");
        $("#tdStaff").css("display", "none");
        $("#tddStaff").css("display", "none");
    }
    else {
        //新增会员时，永久有效选中
        $("#chkMemIsPast").attr("checked", true);
    }

    //是否过期
    ChkMemIsPast();

    //编辑时，会员积分,会员余额和办卡时间不能操作
    if ($('#txtMemID').val() != "") {
        $('#txtMemPoint').css("background-color", "#eee");
        $('#txtMemPoint').attr("readonly", true);
        $('#txtMemMoney').css("background-color", "#eee");
        $('#txtMemMoney').attr("readonly", true);
        $('#txtMemCard').css("background-color", "#eee");

        $('#txtMemCreateTime').css("background-color", "#eee");

    }

    //点击永久有效响应函数
    $("#chkMemIsPast").bind("click", ChkMemIsPast);

    //保存按钮响应函数
    $("#btnMemSave").bind("click", BtnMemSave);

    //重置按钮响应函数
    $("#btnMemReset").bind("click", BtnMemReset);

    //二维码生成
    $("#btnQRCode").bind("click", QRCode);

    //    发卡
    //发卡：感应式IC卡按钮响应函数
    $("#btnSendSenseICCard").bind("click", btnSendSenseICCard);
    //发卡：接触式IC卡按钮响应函数
    $("#btnContactICCard").bind("click", btnContactICCard);



    //查询推荐人信息
    $('#txtMemRecommendCard').keypress(function (e) {
        var keyAscii = window.event ? e.keyCode : e.which;
        var memID = $("#txtMemID").val();
        if (keyAscii == 13) {
            if ($(this).val() != "") {
                doAjax(
                           "../",
                           "GetMem",
                           { "memCard": $.trim($("#txtMemRecommendCard").val()) },
                           "json",
                           function (json) {
                               if (json == "") { $("#txtMemRecommendMsg").html("未找到指定的会员！"); $("#txtMemRecommendName").val(""); $("#txtMemRecommendID").val(""); return; }
                               if (json[0].MemID == memID) { $("#txtMemRecommendMsg").html("推荐人不能设置为自己"); $("#txtMemRecommendCard").val(""); $("#txtMemRecommendID").val(""); $("#txtMemRecommendName").val(""); return; }
                               $("#txtMemRecommendName").val(json[0].MemName);
                               $("#txtMemRecommendID").val(json[0].MemID);
                               $("#txtMemRecommendMsg").html("姓名：" + json[0].MemName);
                           });
            }
            else {
                return false;
            }
        }
    });

    //查询推荐人信息
    $('#txtMemRecommendCard').keyup(function (e) {
        var memID = $("#txtMemID").val();
        if ($(this).val() != "") {
            doAjax(
                           "../",
                           "GetMem",
                           { "memCard": $.trim($("#txtMemRecommendCard").val()) },
                           "json",
                           function (json) {
                               if (json == "") { $("#txtMemRecommendMsg").html("未找到指定的会员！"); $("#txtMemRecommendName").val(""); $("#txtMemRecommendID").val(""); return; }
                               if (json[0].MemID == memID) { $("#txtMemRecommendMsg").html("推荐人不能设置为自己"); $("#txtMemRecommendCard").val(""); $("#txtMemRecommendID").val(""); $("#txtMemRecommendName").val(""); return; }
                               $("#txtMemRecommendName").val(json[0].MemName);
                               $("#txtMemRecommendID").val(json[0].MemID);
                               $("#txtMemRecommendMsg").html("姓名：" + json[0].MemName);
                           });
        }
        else {
            $("#txtMemRecommendMsg").html("");
            $("#txtMemRecommendName").val("");
            $("#txtMemRecommendID").val("");
            return;

        }
    });

    // 图片上传处理
    if (location.href.toLowerCase().indexOf("memregister.aspx") != -1) {
        $("#MemPhoto_Uploadify").uploadify({
            'uploader': "../images/swf/uploadify.swf",
            'script': "../Service/upload.ashx",
            'cancelImg': "../images/member/cancel.png",
            'folder': '../Upload/MemPhoto',
            'queueID': 'MemRegister_fileQueue',
            'buttonImg': "../images/member/selectImg.jpg",
            'height': 22,
            'width': 70,
            'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
            'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
            'auto': false,
            'multi': false,
            'sizeLimit': 512000,
            'onError': function (event, ID, fileObj, errorObj) {
                if (errorObj.type == "File Size")
                    alert("对不起，上传的图片不能超过500K");
                else
                    alert(errorObj.type + ' Error: ' + errorObj.info);
            },
            'onComplete': function (event, ID, fileObj, response, data) {
                if (response != "0") {
                    $("#imgMemPhoto").attr("src", response);
                    $("#txtMemPhoto").val(response);
                }
            }
        });
    }

});
function QRCode() {
    var strErrorMsg = "";
    var memCard = $.trim($("#txtMemCard").val());
    memCard = memCard.replace(/\s/ig, '');

    if (!/\d+/.test(memCard)) {
        strErrorMsg += "<li>会员卡号应该是由数字组成的一个字符串;</li>";
    }
    if (memCard.length < 4) {
        strErrorMsg += "<li>会员卡号必须4~20位;</li>";
    }
    //    if (!$("#chkMMS").attr("checked")) {
    //        strErrorMsg += "<li>如要发送二维码，请开启服务参数中的“启动系统彩信功能”</li>";
    //    }
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
    $("#trTitle").css("display", "");
    $("#trQrCode").css("display", "");
    $("#imgQRCode").attr("src", "../Service/ValidateQRCode.ashx?content=" + memCard + "&" + GetGuid());
    $("#hidImgSrc").val($('#imgQRCode')[0].src);


}
/*************************************************************************************************
*会员过期时间
**************************************************************************************************/
function ChkMemIsPast() {
    if (!$("#chkIsIsPast").attr("checked")) {
        $("#trMemIsPastTime").css("display", "none");
        $("#chkMemIsPast").attr("checked", "true");
        $('#txtMemPastTime').val("");
    }
    else {
        if ($('#chkMemIsPast').attr("checked")) {
            $('#txtMemPastTime').val("");
            $('#txtMemPastTime').css("background-color", "#eee");
            $("#txtMemPastTime").attr("readonly", true);
        }
        else {
            $("#txtMemPastTime").attr("disabled", "");
            $('#txtMemPastTime').css("background-color", "");
            $("#txtMemPastTime").attr("readonly", false);
            $('#txtMemPastTime').bind("focus click", function () {
                WdatePicker({ skin: 'ext', minDate: '%y-%M-{%d+1}', isShowClear: true, readOnly: true });
            });
        }
    }
}

function my97dp() {
    WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm' });
}

function ShowOnlinePhoto() {
    art.dialog.open('../Member/OnlinePhoto.htm', { title: '在线拍照', lock: true
    }, false);

}

/*******************************************************************************
*保存按钮响应函数
*******************************************************************************/
function BtnMemSave() {
    var strErrorMsg = "";
    var type = ($("#txtMemID").val() == "") ? "MemAdd" : "MemEdit";
    var memCard = $.trim($("#txtMemCard").val());
    var memName = $("#txtMemName").val();
    var memPassword = $("#txtMemPassword").val();
    var memCardNumber = $.trim($("#txtCardNumber").val());
    if (!memCard.IsNumber()) {
        strErrorMsg += "<li>会员卡号应该是由数字组成的一个字符串;</li>";
    }
    if (memCard.length < 4) {
        strErrorMsg += "<li>会员卡号必须4~20位;</li>";
    }
    if (type == "MemAdd" && memCardNumber != "" && !memCardNumber.IsNumber()) {
        strErrorMsg += "<li>会员卡面号码应该是由数字组成的一个字符串;</li>";
    }
    if (type == "MemAdd" && memCardNumber != "" && memCardNumber.length < 4) {
        strErrorMsg += "<li>会员卡面号码必须4~20位;</li>";
    }
    if (!$("#RegNullPwd").attr("checked")) {
        if (memPassword == "") strErrorMsg += "<li>必须输入会员密码;</li>";
        if (memPassword != $("#txtMemPasswordCheck").val()) strErrorMsg += "<li>两次输入密码不相同;</li>";
    }
    else {
        //密码可以为空
        if ($.trim($("#txtMemPassword").val()) != $.trim($("#txtMemPasswordCheck").val())) {
            strErrorMsg += "<li>两次输入密码不相同;</li>";
        }
    }

    var memMobile = $.trim($("#txtMemMobile").val());
    var memTelePhone = $.trim($("#txtTelephone").val());
    if ($('#chkSMS').attr("checked")) {
        if (memMobile == "") {
            strErrorMsg += "<li>请输入手机号,否则无法发送短信</li>";
        }
    }
    if ($('#chkMMS').attr("checked")) {
        if (memMobile == "") {
            strErrorMsg += "<li>请输入手机号,否则无法发送二维码</li>";
        }
    }
    if (memMobile != "" && !memMobile.IsMobile()) {
        strErrorMsg += "<li>手机号码格式输入错误;</li>";
    }
    if (memTelePhone != "" && !memTelePhone.IsTelePhone()) {
        strErrorMsg += "<li>固定电话格式输入错误;</li>"
    }

    var identityCardLength = $.trim($("#txtMemIdentityCard").val()).length;
    if ((identityCardLength > 0) && (identityCardLength != 18)) {
        strErrorMsg += "<li>身份证号码必须是18位;</li>";
    }

    var memBirthday = $.trim($("#txtMemBirthday").val());
    if (memBirthday != "") {
        memBirthday = memBirthday.replace(/-/g, '/');
        memBirthday = new Date(memBirthday);
        var nowDate = new Date();
        if (memBirthday == null) {
            strErrorMsg += "<li>输入的生日格式不正确;</li>";
        }
        else {
            if (memBirthday > nowDate) {
                strErrorMsg += "<li>输入的生日不可大于当期日期;</li>";
            }
        }
    }

    if (!$("#txtMemPoint").val().IsNumber()) {
        strErrorMsg += "<li>请输入整数表示“初始积分”;</li>";
    }

    if (!$("#txtMemMoney").val().IsDecimal()) {
        strErrorMsg += "<li>请输入正确的数字，表示“初始余额”;</li>";
    }

    if (!$("#txtMemEmail").val().IsEmpty() && !$("#txtMemEmail").val().IsEmail()) {
        strErrorMsg += "<li>邮箱格式错误;</li>";
    }

    if ($("#sltMemLevelID").val() == "") {
        strErrorMsg += "<li>必须选择会员等级;</li>";
    }

    if ($("#sltShop").val() == "") {
        strErrorMsg += "<li>必须选择商家;</li>";
    }

    var memCreateTime = $.trim($("#txtMemCreateTime").val());
    if (memCreateTime == "") {
        strErrorMsg += "<li>必须填写办卡时间;</li>";
    }
    else {
        memCreateTime = new Date(memCreateTime.replace(/-/g, '/'));
        var nowDate = new Date();
        if (memCreateTime == null) {
            strErrorMsg += "<li>输入的办卡日期格式不正确;</li>";
        }
        else if (memCreateTime > nowDate) {
            strErrorMsg += "<li>输入的办卡日期不可大于当前日期;</li>";
        }
    }

    var trPastTime = $("#trMemIsPastTime").css("display");
    var memPastTime = $.trim($("#txtMemPastTime").val());
    var IsPaseTime = $("#chkMemIsPast").attr("checked");
    if (memPastTime != "") {
        memPastTime = memPastTime.replace(/-/g, '/');
        memPastTime = new Date(memPastTime);
        var nowDate = new Date();
        if (memPastTime == null) {
            strErrorMsg += "<li>输入的过期日期格式不正确;</li>";
        }
        else {
            if (memPastTime < nowDate) {
                strErrorMsg += "<li>输入的过期日期不可小于或等于当前日期;</li>";
            }
        }
    }

    if ((trPastTime != "none") && (memPastTime == "") && (IsPaseTime == "")) {
        strErrorMsg += "<li>请输入过期日期或者选择永久有效;</li>";
    }
    if ($("#ucSysArea_sltProvince").val() != "" && ($("#ucSysArea_sltCity").val() == "" || $("#ucSysArea_sltCounty").val() == "")) {
        strErrorMsg += "<li>请输入完整联系地址;</li>";
    }
    var RecommendCard = $("#txtMemRecommendCard").val();
    var RecommendCardID = $("#txtMemRecommendID").val();
    if (RecommendCard != "" && RecommendCardID == "") {
        strErrorMsg += "<li>输入的推荐人不存在，请重新输入;</li>";
    }

    if ($("#sltStaff").val() == "" && $("#txtRegisterStaffMoney").val() != "") {
        strErrorMsg += "<li>请选择提成员工;</li>";
    }
    if ($("#sltStaff").val() != "" && $("#txtRegisterStaffMoney").val() == "") {
        strErrorMsg += "<li>请输入提成金额;</li>";
    }
    if (typeof ($("#txtRegisterStaffMoney").val()) != "undefined") {
        if ($("#txtRegisterStaffMoney").val() != "") {
            if (!$("#txtRegisterStaffMoney").val().IsDecimal()) {
                strErrorMsg += "<li>请输入正确的数字，表示“提成金额”;</li>";
            }
        }
    }

    //检测自定义属性是否允许为空
    $("*[id^=Mem_Custom_]").each(function (index) {
        var field = $(this).attr("id").replace("Mem_Custom_", "");
        var txt = $("#Mem_Custom_T_" + field).html();
        if ($(this).attr("isNull") == "False" && $(this).val() == "") {
            strErrorMsg += "<li>自定义属性“" + txt + "”为必填字段,不能留空;</li>";
        }
        if ($(this).attr("isNull") == "False" && $(this).attr("isSelect") == "true" && $(this).val() == "无") {
            strErrorMsg += "<li>自定义属性“" + txt + "”请选择;</li>";
        }
    });

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
    $("#sltMemUserID").attr("disabled", false);
    $("#sltShop").attr("disabled", false);
    art.dialog({
        title: '系统提示',
        content: "将" + (type == "MemAdd" ? "增加" : "编辑") + "会员 [" + (memName != "" ? memName : "无姓名") + "], 卡号[" + memCard + "]。\n确定操作吗？ " + (type == "MemAdd" ? "(如有数据变动，则会员不可删除，请仔细核对信息)" : "") + "",
        lock: true,
        ok: function () {
            this.close();
            //this.lock();
            doAjax(
        "../",
        type,
        $("#frmMemRegister").serialize(),
        "json",
         function (json) {
             $("#sltMemUserID").attr("disabled", "disabled");
             $("#sltShop").attr("disabled", "disabled");
             switch (json) {
                 case -1:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("此卡号已经在系统中存在，请重新输入卡号，然后重试！"),
                              lock: true
                          });
                     break;
                 case -2:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("此手机号码已经在系统中存在，请重新输入手机号，然后重试！"),
                              lock: true
                          });
                     break;
                 case -6:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("此卡面号码已经在系统中存在，请重新输入卡面号码，然后重试！"),
                              lock: true
                          });
                     break;
                 case -3:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("保存成功，短信余额不足，不能发送短信，请充值短信！"),
                              close: function () {
                                  if ($("#txtMemID").val() == "") {
                                      location.href = location.href;
                                  } else {
                                      location.href = "../Member/MemList.aspx?PID=3";
                                  }
                              },
                              lock: true
                          });
                     break;
                 case 0:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("系统异常，未保存数据，请再次点击保存！"),
                              lock: true
                          });
                     break;
                 case -4:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("该会员已经存在其他数据异动，不能修改创建时间！"),
                              lock: true
                          });
                     break;
                 case -5:
                     art.dialog
                                ({
                                    title: '系统提示',
                                    time: 4,
                                    content: ("系统错误 请与系统管理员联系！"),
                                    lock: true
                                });
                     break;

                 case -7:
                     art.dialog
                            ({
                                title: '系统提示',
                                time: 4,
                                content: ("该卡不在发卡范围内，请与总店联系！"),
                                lock: true
                            });
                     break;
                 case -8:
                     art.dialog
                            ({
                                title: '系统提示',
                                time: 4,
                                content: ("该商家剩余可用积分不足，请重新填写！"),
                                lock: true
                            });
                     break;
                 default:
                     art.dialog
                            ({
                                title: '系统提示',
                                time: 0.5,
                                content: '保存成功！',
                                close: function () {
                                    if ($("#txtMemID").val() == "") {
                                        location.href = location.href;
                                    } else {
                                        location.href = "../Member/MemList.aspx?PID=3";
                                    }
                                },
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
}


/*******************************************************************************
*重置按钮响应函数
*******************************************************************************/
function BtnMemReset() {
    var strMemID = $('#txtMemID').val();
    if (strMemID != "") {
        window.location.href = '../../Member/MemRegister.aspx?PID=2&MemID=' + strMemID;
    }
    else {
        window.location.href = '../../Member/MemRegister.aspx?PID=2';
    }
}


/*******************************************************************************
*发卡：感应式IC卡按钮响应函数
*******************************************************************************/
function btnSendSenseICCard() {
    var strErrorMsg = "";
    var memCard = $.trim($("#txtMemCard").val());
    memCard = memCard.replace(/\s/ig, '');

    if (!/\d+/.test(memCard)) {
        strErrorMsg += "<li>会员卡号应该是由数字组成的一个字符串;</li>";
    }
    if (memCard.length < 4 || memCard.length > 20) {
        strErrorMsg += "<li>会员卡号必须4~20位;</li>";
    }
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
    doAjax(
        "../",
        "GetModelByMemCard",
        { "memCard": memCard },
        "json",
        function (json) {
            if (json == "1") {
                art.dialog({
                    title: '系统提示',
                    content: '此卡号在系统中已存在，请换个卡号',
                    lock: true,
                    time: 3
                })
                $("#txtMemCard").val("").focus();
            }
            else {
                CardReader.GiveCard(memCard);  //发卡  
                setTimeout("showMsg()", 200)
            }
        }
    )
}
function showMsg() {
    var msg = CardReader.errMsg;
    art.dialog({
        title: "系统提示",
        content: msg,
        time: 3
    });
    if (msg == "发卡成功！！") {
        $("#txtMemCard").attr("readOnly", true);
    }
    else {
        alert('发卡失败');
    }
}




/*******************************************************************************
*发卡：接触式IC卡按钮响应函数
*******************************************************************************/
function btnContactICCard() {
    var strErrorMsg = "";
    var memCard = $.trim($("#txtMemCard").val());
    memCard = memCard.replace(/\s/ig, '');
    if (!/\d+/.test(memCard)) {
        strErrorMsg += "<li>会员卡号应该是由数字组成的一个字符串;</li>";
    }
    if (memCard.length < 4 || memCard.length > 20) {
        strErrorMsg += "<li>会员卡号必须4~20位;</li>";
    }
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

    doAjax("../", "GetModelByMemCard", { "memCard": memCard }, "json", function (json) {
        if (json == "1") {
            art.dialog({
                title: '系统提示',
                content: '此卡号在系统中已存在，请换个卡号',
                lock: true,
                time: 3
            })
            $("#txtMemCard").val("").focus();
        }
        else {
            sendContactICCard(memCard);
        }
    })
}
function sendContactICCard(memCard) {
    var status = null;
    var getValue = null;
    MWReaderCtl.MWic_init(0, 9600);
    status = MWReaderCtl.LastRet;
    if (status == 0) {
        MWReaderCtl.MWcsc_4442(6, "ffffff");
        status = MWReaderCtl.LastRet;
        if (status != 0) {
            art.dialog({
                title: '系统提示',
                content: '密码验证错误，请检查设备的LED灯是否处于绿色状态，若不是请将IC卡插好，否则请与管理员联系！！',
                lock: true,
                time: 3
            })
            MWReaderCtl.MWic_exit();
            return;
        }
        getValue = MWReaderCtl.MWsrd_4442(60, 20);
        status = MWReaderCtl.LastRet;
        if (status == 0) {
            if (!/^[F,f]{20,40}$/.test(getValue)) {
                art.dialog({
                    title: '系统提示',
                    content: '此卡未初始化，请初始化后再发卡！！',
                    lock: true,
                    time: 3
                })
                MWReaderCtl.MWic_exit();
                return;
            }
        }
        else {
            art.dialog({
                title: '系统提示',
                content: '检查此卡是否初始化时失败，请与管理员联系！！',
                lock: true,
                time: 3
            })
            MWReaderCtl.MWic_exit();
            return;
        }
        if (memCard.length < 20) {
            var end = 20 - memCard.length;
            for (var i = 0; i < end; i++) {
                memCard += "F";
            }
        }
        MWReaderCtl.MWswr_4442(60, 20, memCard);
        status = MWReaderCtl.LastRet;
        if (status == 0) {
            art.dialog({
                title: '系统提示',
                content: '发卡成功！！',
                time: 3
            });
            $("#txtMemCard").attr("readOnly", true);
        }
        else {
            art.dialog({
                title: '系统提示',
                content: '发卡失败,请重试！！',
                lock: true,
                time: 3
            });
            MWReaderCtl.MWic_exit();
            return;
        }
        MWReaderCtl.MWic_exit();
    }
    else {
        art.dialog({
            title: '系统提示',
            content: '接触式IC卡设备打开失败，请检查设备连接！',
            lock: true,
            time: 3
        });
    }
}
