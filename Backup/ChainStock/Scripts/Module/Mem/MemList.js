$(document).ready(function () {
    $("#txtShop").val($("#HDsltshop").val());

    $("#txtQueryMem").focus();
    //会员充值响应事件
    $("#btnMemRecharge").bind("click", btnMemRecharge);

    //会员换卡响应事件
    $("#btnMemChangeCard").bind("click", btnMemChangeCard);

    //会员延期响应事件
    $("#btnMemDelay").bind("click", btnMemDelay);

    //会员锁定响应事件
    $("#btnMemLock").bind("click", btnMemLock);

    //会员修改密码响应事件
    $("#btnMemChangePwd").bind("click", btnMemChangePwd);

    //会员提现响应事件
    $("#btnMemDrawMoney").bind("click", btnMemDrawMoney);

    //会员发送短信响应事件
    $("#btnSenndSMS").bind("click", btnSenndSMS);

    //会员积分清零响应事件
    $("#btnMemPointReset").bind("click", btnMemPointReset);

    //会员积分全部清零响应事件
    $("#btnAllMemPointReset").bind("click", btnAllMemPointReset);

    //给会员发优惠券
    $("#btnCoupon").bind("click", btnCoupon);
    $("#btnSendCoupon").bind("click", btnSendCoupon);
    $("#btnCloseCoupon").bind("click", btnCloseCoupon);

    //高级查询
    $("#btnhigthSearch").bind("click", btnhigthSearch);
    //高级查询中的重置
    $("#btnMemReset").bind("click", btnMemReset);

    //会员登记时间
    $('#txtMemStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $('#txtMemEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $("#SysArea1_sltCity").bind("change", { foo: "SysArea1" }, City);
    $("#SysArea1_sltCounty").bind("change", { foo: "SysArea1" }, County);
    $("#SysArea1_sltProvince").bind("change", { foo: "SysArea1" }, Province);
    $("#SysArea1_sltVillage").bind("change", { foo: "SysArea1" }, Village);

    $("#sltCustomField").bind("change", BindCustom);
});

function BindCustom() {
    var strInfo = new Array();
    var html = "";
    var customField = $("#sltCustomField").val();
    if (customField != "") {
        var customType = $("#sltCustomField").find("option:selected").attr("CustomFieldType");
        var customInfo = $("#sltCustomField").find("option:selected").attr("CustomFieldInfo");
        strInfo = customInfo.toString().split("|");

        $("#txtCustomField").remove();
        switch (customType) {
            case "text":
                html = "<input type='text' id='txtCustomField' name='txtCustomField' class='input_txt border_radius' />";
                break;
            case "select":
                html = "<select id='txtCustomField' name='txtCustomField' class='selectWidth' >";
                html += "<option value='无'>===== 请选择 =====</option>";
                for (var i = 0; i < strInfo.length; i++) {
                    html += "<option value='" + strInfo[i] + "'>" + strInfo[i] + "</option>";
                }
                html += "</select>";
                break;
            case "date":
                html = "<input type='text' id='txtCustomField' name='txtCustomField' class='Wdate border_radius' onfocus=\"WdatePicker({ skin: 'ext', isShowClear: false, readOnly: true });\"/>";
                break;
        }
    }
    else {
        html = "<input type='text' id='txtCustomField' class='input_txt border_radius' />";
    }
    $("#Custom").html(html);
}

//高级查询
function btnhigthSearch() {
    var divSearch = document.getElementById("divHightSearch");
    if (divSearch.style.display == "none") {
        divSearch.style.display = "";
    }
    else {
        divSearch.style.display = "none";
    }
}
function btnMemReset() {
    $("#sltMemLevelID").val("");
    $("#sltShop").val("");
    $("#sltMemState").val("");
    $("#sltMemPoint").val("");
    $("#txtMemPoint").val("");
    $("#sltMemUserID").val("");
    $("#sltMemBirthday").val("");
    $("#sltMemPastTime").val("");
    $("#sltMemMoney").val("");
    $("#txtMemMoney").val("");
    $("#sltCustomField").val("");
    //    $("#txtCustomField").val("");
    var html = "<input type='text' id='text' name='text' class='input_txt border_radius'/>";
    $("#Custom").html(html);
    $("#sltMemConsume").val("");
    $("#sltConsumeMoney").val("");
    $("#txtConsumeMoney").val("");
    $("#txtMemStartTime").val("");
    $("#txtMemEndTime").val("");
    $("#txtMemRecommendCard").val("");
    $("#SysArea1_sltProvince").val("");
    $("#SysArea1_sltCity").val("");
    $("#SysArea1_sltCounty").val("");
    $("#SysArea1_sltVillage").val("");
}


var memcard = "";
var number = 0;
var state = 0;
var memID = "";
var CouponDialog;

/**************************************************************************************
*选择单个会员
**************************************************************************************/
function GetCheckValue() {
    $(".chk").each(function () {
        if ($(this).attr("checked")) {
            number++;
        }
        if ($(this).attr("checked") && $(this).attr("memcard") != "") {
            memcard = $(this).attr("memcard");
            state = parseInt($(this).attr("state"));
        }
    });
}

/**************************************************************************************
*不选会员默认发送筛选的全部
**************************************************************************************/
function GetAllValue() {
    var strSql = " MemID >0 ";
    var strMem = $("#txtQueryMem").val();
    if (strMem != "") {
        strSql += " and (MemCard ='" + strMem + "' or MemName like '%" + strMem + "%' or MemMobile='" + strMem + "') ";
    }

    var strMemLevelID = $("#sltMemLevelID").val();
    if (strMemLevelID != "") {
        strSql += " and MemLevelID=" + strMemLevelID;
    }

    var strMemShopID = $("#sltShop").val();
    if (strMemShopID != "") {
        strSql += " and MemShopID=" + strMemShopID;
    }

    var strMemPastTime = $("#sltMemPastTime").val();
    if (strMemPastTime != "") {
        strSql += " and MemIsPast = 1 ";
        if (strMemPastTime == "0") {
            strSql += " and getdate()>MemPastTime ";
        }
        else if (strMemPastTime == "1") {
            strSql += " and datediff(day,MemPastTime,getdate())=7 ";
        }
        else if (strMemPastTime == "2") {
            strSql += " and datediff(day,MemPastTime,getdate())=30 ";
        }
    }

    var strMemConsume = $("#sltMemConsume").val();
    if (strMemConsume != "") {
        if (strMemConsume == "0") {
            strSql += " and datediff(day,MemConsumeLastTime,getdate())>91 ";
        }
        else if (strMemConsume == "1") {
            strSql += " and datediff(day,MemConsumeLastTime,getdate())>182 ";
        }
        else if (strMemConsume == "2") {
            strSql += " and datediff(day,MemConsumeLastTime,getdate())>365 ";
        }
    }

    var strMemBirthday = $("#sltMemBirthday").val();
    if (strMemBirthday != "") {
        if (strMemBirthday == "0") {
            strSql += " and datediff(week,(convert(varchar(5),getdate(),120)+convert(varchar(5),MemBirthday,110)),getdate())=0 ";
        }
        else if (strMemBirthday == "1") {
            var strNowTime = new Date();
            var strMonth = strNowTime.getMonth() + 1;
            strSql += " and convert(varchar(2),MemBirthday,107)=" + strMonth;
        }
    }

    var strMemCustom = $("#sltCustomField").val();
    var strCustomField = $("#txtCustomField").val();
    if (strMemCustom != "" && strCustomField != "") {
        strSql += " and " + strMemCustom + " = '" + strCustomField + "'";
    }

    var strMemState = $("#sltMemState").val();
    if (strMemState != "") {
        strSql += " and MemState=" + strMemState;
    }

    var strMemUserID = $("#sltMemUserID").val();
    if (strMemUserID != "") {
        strSql += " and MemUserID=" + strMemUserID;
    }

    var strMemStartTime = $("#txtMemStartTime").val();
    if (strMemStartTime != "") {
        strSql += " and MemCreateTime>'" + strMemStartTime + "' ";
    }

    var strMemEndTime = $("#txtMemEndTime").val();
    if (strMemEndTime != "") {
        var dateArray = strMemEndTime.split("-");
        var dtEndTime = new Date(dateArray[0], dateArray[1], dateArray[2]);
        strMemEndTime = dtEndTime.setDate(dtEndTime.getDate() + 1);
        strSql += " and MemCreateTime<'" + strMemEndTime + "' ";
    }

    var strPointSymbols = $("#sltMemPoint").val();
    var strPoint = $("#txtMemPoint").val();
    if (strPoint != "") {
        strSql += " and MemPoint" + strPointSymbols + parseInt(strPoint);
    }

    var strMoneySymbols = $("#sltMemMoney").val();
    var strMemMoney = $("#txtMemMoney").val();
    if (strMemMoney != "") {
        strSql += " and MemMoney" + strMoneySymbols + parseFloat(strMemMoney);
    }

    var strConsumeMoneySymbols = $("#sltConsumeMoney").val();
    var strConsumeMoney = $("#txtConsumeMoney").val();
    if (strConsumeMoney != "") {
        strSql += " and MemConsumeMoney" + strConsumeMoneySymbols + parseFloat(strConsumeMoney);
    }

    return strSql;
}

function GetSomeValue() {
    memID = "";
    number = 0;
    var intChoosedMem = 0;
    var intLockMem = 0;
    var intNoMobile = 0;

    $(".chk").each(function () {
        if ($(this).attr("checked")) {
            memID += $(this).attr("memID") + ",";
            if ($(this).attr("state") != 0) {
                intLockMem++;
            }
            if ($(this).attr("mobiles") == "") {
                intNoMobile++
            }
            if ($(this).attr("state") == 0) {
                number++;
            }
            intChoosedMem++;
        }
    });

    if (intChoosedMem > 0) {
        $("#lblChoosedMem").html(intChoosedMem + '人');
        $("#lblLockMem").html(intLockMem + '人');
        $("#lblNoMobile").html(intNoMobile + '人');
        $("#lblSendNumber").html(number + '人');
    }
    else {
        var strCondition = GetAllValue();
        doAjax("../",
            "GetAllMemList",
            { "Condition": strCondition },
            "json",
            function (json) {
                if (json != "") {
                    $.each(json, function (index, item) {
                        memID += item.MemID + ",";
                        if (item.MemState != 0) {
                            intLockMem++;
                        }
                        if (item.MemMobile == "") {
                            intNoMobile++;
                        }
                        if (item.MemState == 0) {
                            number++;

                        }
                        intChoosedMem++;
                    });
                }
                $("#lblChoosedMem").html(intChoosedMem + '人');
                $("#lblLockMem").html(intLockMem + '人');
                $("#lblNoMobile").html(intNoMobile + '人');
                $("#lblSendNumber").html(number + '人');
            });
    }
}

/**************************************************************************************
*发送电子优惠券  选择多个会员
**************************************************************************************/
function GetSomeCheckValue() {
    memID = "";
    number = 0;
    var intChoosedMem = 0;
    var intLockMem = 0;
    var intNoMobile = 0;

    $(".chk").each(function () {
        if ($(this).attr("checked")) {
            if ($(this).attr("state") != 0) {
                intLockMem++;
            }
            if ($(this).attr("mobiles") == "") {
                intNoMobile++
            }
            if ($(this).attr("mobiles") != "" && $(this).attr("state") == 0) {
                number++;
                memID += $(this).attr("memID") + ",";
            }
            intChoosedMem++;
        }
    });

    if (intChoosedMem > 0) {
        $("#lblChoosedMem").html(intChoosedMem + '人');
        $("#lblLockMem").html(intLockMem + '人');
        $("#lblNoMobile").html(intNoMobile + '人');
        $("#lblSendNumber").html(number + '人');
    }
    else {
        var strCondition = GetAllValue();
        doAjax("../",
            "GetAllMemList",
            { "Condition": strCondition },
            "json",
            function (json) {
                if (json != "") {
                    $.each(json, function (index, item) {
                        if (item.MemState != 0) {
                            intLockMem++;
                        }
                        if (item.MemMobile == "") {
                            intNoMobile++;
                        }
                        if (item.MemMobile != "" && item.MemState == 0) {
                            number++;
                            memID += item.MemID + ",";
                        }
                        intChoosedMem++;
                    });
                }
                $("#lblChoosedMem").html(intChoosedMem + '人');
                $("#lblLockMem").html(intLockMem + '人');
                $("#lblNoMobile").html(intNoMobile + '人');
                $("#lblSendNumber").html(number + '人');
            });
    }
}

///**************************************************************************************
//*发送电子优惠时选择筛选出的所有会员
//**************************************************************************************/
//function GetAllCheckValue() {
//    number = 0;
//    memID = "";
//    var strErrorMsg = "";

//    $(".chk").each(function () {
//        if ($(this).attr("mobiles") != "" && $(this).attr("state") == 0) {
//            number++;
//            memID += $(this).attr("memID") + ",";
//        }
//    });

//    if (number < 1) {
//        title: '系统提示',
//       strErrorMsg += "<li>当前没有查询出来的会员!</li>";
//    }
//    if (strErrorMsg != "") {
//        art.dialog({
//            title: '系统提示',
//            icon: 'error', //图标
//            time: 2,
//            content: strErrorMsg,
//            lock: true
//        });
//        return false;
//    }
//}

/*************************************************************************************
*发优惠券  快捷按钮
*************************************************************************************/
function btnCoupon() {
    CouponChange();
    if ($("#chkSms").attr("checked")) {
        $("#counponType").val("0");
        GetSomeCheckValue();
        CouponDialog = art.dialog({
            title: '发送优惠券',
            content: document.getElementById('divCoupon'),
            width: '600px',
            height: '300px',
            id: 'divCoupon',
            lock: true,
            close: function () {
                $("#counponType").val("");
            }
        });
    }
    else {
        GetSomeValue();
        $("#counponType").val("1");
        CouponDialog = art.dialog({
            title: '发送优惠券',
            content: document.getElementById('divCoupon'),
            width: '600px',
            height: '300px',
            id: 'divCoupon',
            lock: true,
            close: function () {
                $("#counponType").val("");
            }
        });
    }
}

/*************************************************************************************
*关闭按钮
*************************************************************************************/
function btnCloseCoupon() {
    CouponDialog.close();
}

/*************************************************************************************
*发送按钮
*************************************************************************************/
function btnSendCoupon() {
    if (number <= 0) {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: "预发会员为零时不能发电子优惠券，请选择会员！",
            lock: true
        });
        return false;
    }
    else {
        if ($("#counponType").val() == "0") {
            doAjax("../",
            "SendCoupon",
            { "MemID": memID,
                "Number": number,
                "CouponID": $("#sltCoupon").val()
            },
            "json",
            function (json) {
                CouponDialog.close();
                switch (json) {
                    case -1:
                        art.dialog({ title: '系统提示',
                            time: 3,
                            content: ("短信余额少于预发送数量，请充值短信后再发优惠券！"),
                            lock: true
                        });
                        break;
                    case -2:
                        art.dialog({ title: '系统提示',
                            time: 3,
                            content: ("您选择优惠券少于预发送数量，请重新选择优惠券！"),
                            lock: true
                        });
                        break;
                    case 0:
                        art.dialog({ title: '系统提示',
                            time: 3,
                            content: ("系统异常，未能发送！"),
                            lock: true
                        });
                        break;
                    case 1:
                        art.dialog({ title: '系统提示',
                            time: 6,
                            content: ("正在发送，请等待......"),
                            lock: true
                        });
                        break;
                    case -5:
                        art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: ("本店所拥有的短信量不足，发送短信失败，请与总店联系！"),
                                  lock: true
                              });
                        break;
                }
            });
        }
        else {
            art.dialog({
                title: '系统提示',
                content: '未启用短信功能确认为会员绑定优惠券吗？',
                lock: true,
                ok: function () {
                    this.close();

                    doAjax("../",
               "BindCoupon",
            {
                "MemID": memID,
                "Number": number,
                "CouponID": $("#sltCoupon").val()
            },
               "json",
                function (json) {
                    CouponDialog.close();
                    switch (json) {
                        case -2:
                            art.dialog({ title: '系统提示',
                                time: 3,
                                content: ("您选择优惠券少于预发送数量，请重新选择优惠券！"),
                                lock: true
                            });
                            break;
                        case 0:
                            art.dialog({ title: '系统提示',
                                time: 3,
                                content: ("系统异常，未能发送！"),
                                lock: true
                            });
                            break;
                        case 1:
                            art.dialog({ title: '系统提示',
                                time: 6,
                                content: ("正在发送，请等待......"),
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
    }
}

/*************************************************************************************
*选择优惠券  改变事件
*************************************************************************************/
function CouponChange() {
    doAjax("../",
          "GetCoupon",
          { "CouponID": $("#sltCoupon").val() },
          "json",
          function (json) {
              if (json != null) {
                  $("#lblCouponMinMoney").html(Math.round(json[0].CouponMinMoney, 2) + '元');
                  $("#lblCouponDayNum").html(json[0].CouponDayNum + '张');
                  $("#lblCouponYF").html(json[0].CouponYF + '张');
                  $("#lblCouponNu").html(parseInt(json[0].CouponPredictNu) - parseInt(json[0].CouponYF) + '张');

                  if (json[0].CouponEffective == "1") {
                      var strTime = json[0].CouponStart.split(" ")[0] + " 至 " + json[0].CouponEnd.split(" ")[0];
                      $("#lblCouponTime").html(strTime);
                  }
                  else {
                      $("#lblCouponTime").html("永久有效");
                  }
              }
              else {
                  $("#lblCouponMinMoney").html('0元');
                  $("#lblCouponDayNum").html('0张');
                  $("#lblCouponYF").html('0张');
                  $("#lblCouponNu").html('0张');
              }
          });
}

/*************************************************************************************
*会员充值  快捷按钮
*************************************************************************************/
function btnMemRecharge() {
    number = 0;
    state = 0;
    var strErrorMsg = "";
    GetCheckValue();
    if (number > 1) {
        strErrorMsg += "<li>该操作只允许对单个会员进行操作,请重新选择!</li>";
    }
    if (state != 0) {
        strErrorMsg += "<li>该会员状态异常不能进行操作,请重新选择!</li>";
    }
    if (strErrorMsg != "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    window.location.href = "../Member/MemRechargeMoney.aspx?PID=4&MemCard=" + memcard;
}

/*************************************************************************************
*会员换卡  快捷按钮
*************************************************************************************/
function btnMemChangeCard() {
    number = 0;
    state = 0;
    var strErrorMsg = "";
    GetCheckValue();
    if (number > 1) {
        strErrorMsg += "<li>该操作只允许对单个会员进行操作,请重新选择!</li>";
    }
    if (state != 0) {
        strErrorMsg += "<li>该会员状态异常不能进行操作,请重新选择!</li>";
    }
    if (strErrorMsg != "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    window.location.href = "../Member/MemChangeCard.aspx?PID=6&MemCard=" + memcard;
}

/*************************************************************************************
*会员延时  快捷按钮
*************************************************************************************/
function btnMemDelay() {
    number = 0;
    state = 0;
    var strErrorMsg = "";
    GetCheckValue();
    if (number > 1) {
        strErrorMsg += "<li>该操作只允许对单个会员进行操作,请重新选择!</li>";
    }
    if (state != 0) {
        strErrorMsg += "<li>该会员状态异常不能进行操作,请重新选择!</li>";
    }
    if (strErrorMsg != "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    window.location.href = "../Member/MemDelay.aspx?PID=7&MemCard=" + memcard;
}

/*************************************************************************************
*会员锁定  快捷按钮
*************************************************************************************/
function btnMemLock() {
    number = 0;
    state = 0;
    var strErrorMsg = "";
    GetCheckValue();
    if (number > 1) {
        strErrorMsg += "<li>该操作只允许对单个会员进行操作,请重新选择!</li>";
    }
    if (strErrorMsg != "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    window.location.href = "../Member/MemLock.aspx?PID=8&MemCard=" + memcard;
}

/*************************************************************************************
*会员修改密码  快捷按钮
*************************************************************************************/
function btnMemChangePwd() {
    number = 0;
    state = 0;
    var strErrorMsg = "";
    GetCheckValue();
    if (number > 1) {
        strErrorMsg += "<li>该操作只允许对单个会员进行操作,请重新选择!</li>";
    }
    if (state != 0) {
        strErrorMsg += "<li>该会员状态异常不能进行操作,请重新选择!</li>";
    }
    if (strErrorMsg != "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    window.location.href = "../Member/MemChangePwd.aspx?PID=9&MemCard=" + memcard;
}

/*************************************************************************************
*会员提现  快捷按钮
*************************************************************************************/
function btnMemDrawMoney() {
    number = 0;
    state = 0;
    var strErrorMsg = "";
    GetCheckValue();
    if (number > 1) {
        strErrorMsg += "<li>该操作只允许对单个会员进行操作,请重新选择!</li>";
    }
    if (state != 0) {
        strErrorMsg += "<li>该会员状态异常不能进行操作,请重新选择!</li>";
    }
    if (strErrorMsg != "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    window.location.href = "../Member/MemDrawMoney.aspx?PID=10&MemCard=" + memcard;
}

/*************************************************************************************
*会员发短信  快捷按钮
*************************************************************************************/
function btnSenndSMS() {
    MemListSendSMS();
}

/*************************************************************************************
*积分清零 快捷按钮
**************************************************************************************/
function btnMemPointReset() {
    number = 0;
    memID = "";
    var strErrorMsg = "";

    $(".chk").each(function () {
        if ($(this).attr("checked")) {
            number++;
            memID += $(this).attr("memID") + ",";
        }
    });
    if (number < 1) {
        return btnAllMemPointReset();
    }
    if (strErrorMsg != "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            time: 2,
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    art.dialog({
        title: '系统提示',
        content: '您确定要对所选会员进行积分清零吗？此操作不可恢复，请慎重！ ',
        lock: true,
        ok: function () {
            this.close();
            doAjax("../",
          "MemPointReset",
          { "memID": memID },
          "json",
          function (json) {
              switch (json) {
                  case 0:
                      art.dialog({
                          title: '系统提示',
                          time: 2,
                          content: ("系统异常，请重试！"),
                          lock: true
                      });
                      break;
                  case 1:
                      art.dialog({
                          title: '系统提示',
                          time: 2,
                          content: ("积分清零成功！"),
                          close: function () { location.href = location.href; }
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

/*************************************************************************************
*全部积分清零 快捷按钮
**************************************************************************************/
function btnAllMemPointReset() {
    number = 0;
    memID = "";
    var strErrorMsg = "";

    $(".chk").each(function () {
        number++;
        memID += $(this).attr("memID") + ",";
    });

    if (number < 1) {
        title: '系统提示',
       strErrorMsg += "<li>当前没有查询出来的会员!</li>";
    }
    if (strErrorMsg != "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            time: 2,
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    art.dialog({
        title: '系统提示',
        content: '您确定要进行全部会员积分清零吗？此操作不可恢复，请慎重！ ',
        lock: true,
        ok: function () {
            this.close();
            doAjax("../",
          "MemPointReset",
          { "memID": memID },
          "json",
          function (json) {
              switch (json) {
                  case 0:
                      art.dialog({
                          title: '系统提示',
                          time: 2,
                          content: ("系统异常，请重试！"),
                          lock: true
                      });
                      break;
                  case 1:
                      art.dialog({
                          title: '系统提示',
                          time: 2,
                          content: ("积分清零成功！"),
                          close: function () { location.href = location.href; }
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

/*************************************************************************************
*删除会员
*************************************************************************************/
function DeleteMem(name, strMemID) {
    art.dialog({
        title: '系统提示',
        content: '您确定要删除该会员吗？此操作不可恢复，请慎重！ ',
        lock: true,
        ok: function () {
            this.close();
            doAjax("../",
                   "MemDelete",
                   { "strMemID": strMemID },
                   "json",
                   function (json) {
                       switch (json.result.toString()) {
                           case "0":
                               art.dialog({
                                   title: '系统提示',
                                   time: 4,
                                   content: ("系统错误 请与系统管理员联系！"),
                                   lock: true
                               });
                               break;
                           case "1":
                               art.dialog({
                                   title: '系统提示',
                                   time: 2,
                                   content: ("删除成功！"),
                                   close: function () { location.href = "MemList.aspx?PID=3" }
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
        cancel: true
    });
}