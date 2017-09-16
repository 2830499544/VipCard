$(document).ready(function () {
    //是否散客
    var chkNoMember;
    //没有查询会员时 所有控件禁用
    if ($.isEmptyObject(global_mem)) {
        $("#txtExpMoney").attr("disabled", "disabled");
        $("#txtDiscountMoney").attr("disabled", "disabled");
        $("#txtExpPoint").attr("disabled", "disabled");
        $("#txtExpRemark").attr("disabled", "disabled");
    };

    if ($("#MemCard").val() != null) {
        if ($("#MemCard").val() != "0") {
            $("#txtFindMember").val($("#MemCard").val());
        }
        else {
            $("#chkNoMember").attr("checked", "checked");

        }
    }

    //如果没有启用发送短信，隐藏发送短信选择框
    //    if (!$("#chkIsSMS").attr("checked")) {
    //        $("#lblIsSMS").css("display", "none");
    //    }

    //如果没有启用打印小票，隐藏打印小票选择框
    if (!$("#chkPrint").attr("checked")) {
        $("#lblIsPrint").css("display", "none");
    }

    //重置按钮响应函数
    $("#btnExpenseReset").bind("click", BtnExpenseReset);

    //保存按钮响应函数
    $("#btnExpSave").bind("click", btnExpSave);

    //输入消费金额得到折后金额和积分
    $("#txtExpMoney").bind("keyup", DiscountMoney);

    //只输入折后金额得到积分
    $("#txtDiscountMoney").bind("keyup", DiscountPoint);
    //输入积分触发的事件
    $("#txtExpPoint").bind("keyup", ExpPoint);
});

/*************************************************************************
*输入消费金额得到折后金额和积分
*************************************************************************/
function DiscountMoney() {

    if ($("#JSXF").val() == "1") {
        var zonge = $("#txtExpMoney").val();
        $("#txtDiscountMoney").val(zonge);
        $("#txtExpPoint").val(0);
    } else {

        var dclExpMoney = $("#txtExpMoney").val();
        var sssa = dclExpMoney.substring(dclExpMoney.length - 1, dclExpMoney.length);

        if (dclExpMoney.IsDecimal() || sssa == ".") {
            if ($("#chkNoMember").attr("checked")) {
                levelPercent = 1;
                pointPercent = 1;
            }
            else {
                var levelPercent = (global_mem.ClassDiscountPercent != 0) ? global_mem.ClassDiscountPercent : 0;
                var pointPercent = (global_mem.ClassPointPercent != 0 || global_mem.ClassPointPercent != undefined) ? global_mem.ClassPointPercent : 0;
                //                var levelPercent = (global_mem.LevelDiscountPercent != 0) ? global_mem.LevelDiscountPercent : 0;
                //                var pointPercent = (global_mem.LevelPointPercent != 0 || global_mem.LevelPointPercent != undefined) ? global_mem.LevelPointPercent : 0;
            }

            if (levelPercent != 0) {
                $("#txtDiscountMoney").val(parseFloat(accMul(parseFloat($(this).val()), levelPercent)).toFixed(2));
            }
            else {
                $("#txtDiscountMoney").val($(this).val());
            }

            if (pointPercent != 0) {
                if (!$("#chkNoMember").attr("checked")) {
                    $("#txtExpPoint").val(Math.floor(accDiv(parseFloat($("#txtDiscountMoney").val()), pointPercent)));
                }
            }
            if ($.trim($("#txtExpMoney").val()) == "") {
                $("#txtExpMoney").val("0");
                $("#txtDiscountMoney").val("0");
                $("#txtExpPoint").val("0");
            }
            //        if (dclExpMoney.indexOf('-') > -1) {
            //            $("#txtExpMoney").val("");
            //            $("#txtDiscountMoney").val("0");
            //            $("#txtExpPoint").val("0");
            //        }
        }
        else {
            $("#txtExpMoney").val("");
            $("#txtDiscountMoney").val("0");
            $("#txtExpPoint").val("0");
        }
    }
}

/*************************************************************************
*只输入折后金额得到积分
*************************************************************************/
function DiscountPoint() {

    if ($("#JSXF").val() == "1") {
        var zonge = $("#txtExpMoney").val();
        $("#txtDiscountMoney").val(zonge);
        $("#txtExpPoint").val(0);
    }
    else {
        var dclDiscount = $("#txtDiscountMoney").val();
        if (dclDiscount.IsDecimal()) {
            if (!$("#chkNoMember").attr("checked")) {
                //                var pointPercent = (global_mem.LevelPointPercent != 0 || global_mem.LevelPointPercent != undefined) ? global_mem.LevelPointPercent : 0;
                //                var levelPercent = (global_mem.LevelDiscountPercent != 0) ? global_mem.LevelDiscountPercent : 0;

                var pointPercent = (global_mem.ClassPointPercent != 0 || global_mem.ClassPointPercent != undefined) ? global_mem.ClassPointPercent : 0;
                var levelPercent = (global_mem.ClassDiscountPercent != 0) ? global_mem.ClassDiscountPercent : 0;



                if (pointPercent != 0) {
                    $("#txtExpPoint").val(Math.floor(accDiv(parseFloat($("#txtDiscountMoney").val()), pointPercent)));
                }
                if (levelPercent != 0) {
                    $('#txtExpMoney').val(parseFloat(accDiv($('#txtDiscountMoney').val(), levelPercent)).toFixed(2));
                }
                if ($.trim($("#txtDiscountMoney").val()) == "") {
                    $("#txtExpPoint").val("0");
                }
            }
        }
        else {
            $("#txtExpMoney").val("");
            //        $("#txtDiscountMoney").val("0");
            $("#txtExpPoint").val("0");
        }
    }
}

function ExpPoint() {
    var point = $("#txtExpPoint").val();
    if (!point.IsNumber()) {
        $("#txtExpPoint").val("0");
    }
}

/*************************************************************************
*保存按钮响应函数
*************************************************************************/
function btnExpSave() {
    chkNoMember = $("#chkNoMember").attr("checked");
    var chkAllowPwd = $("#chkAllowPwd").attr("checked");
    var strOrderAccount = $.trim($("#lblOrderAccount").html());
    var strExpMoney = $.trim($("#txtExpMoney").val()).ToFloat().toFixed(2);
    var strDiscountMoney = $.trim($("#txtDiscountMoney").val()).ToFloat().toFixed(2);

    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem) && (chkNoMember == false)) {
        strErrorMsg += "<li>请先选择会员！</li>";
    }

    if (!strExpMoney.IsDecimal()) {
        strErrorMsg += "<li>输入金额格式不正确,请重新输入!</li>";
    }

    if ((strExpMoney == '' || strExpMoney <= 0)
             || (strDiscountMoney == '' || strDiscountMoney <= 0)) {
        strErrorMsg += "<li>请必须输入‘消费金额(原商品金额)’ 和 ‘折后金额(实际付款金额)’</li>";
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
    ShowPay("Expense", global_mem.MemMoney, global_mem.MemPoint, strDiscountMoney, strOrderAccount, chkAllowPwd);
}

/*************************************************************************
*保存消费数据响应函数
*************************************************************************/
//function ExpenseOK(IsPayByCard, IsPayByCash, IsPayByBink, cardPayMoney, cashPayMoney, couponPayMoney, binkPayMoney, strDiscountMoney) {
function ExpenseOK(parameter) {
    //获取打印份数
    var PointNum = $("#PointNum").val();

    doAjax("../",
               "Expense",
               {
                   "memID": global_mem.MemID,
                   "chkNoMember": chkNoMember,
                   "money": $.trim($("#txtExpMoney").val()),
                   "parameter": parameter,
                   "point": $.trim($("#txtExpPoint").val()),
                   "orderAccount": $.trim($("#lblOrderAccount").html()),
                   "remark": $("#txtExpRemark").val(),
                   "sendSMS": $("#chkSMS").attr("checked"),
                   "isTimeExpense": $("#txtIsTimeExpense").val(),
                   "myremark": $("#spTimeInfo").html(),
                   "project": $("#txtProject").val(),
                   "endtime": $("#txtEndTime").val()
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
                                     content: ("系统错误 请与系统管理员联系！"),
                                     lock: true
                                 });
                           break;
                       case -2:
                           art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: ("消费成功！,短信余额不足，不能发送短信，请充值短信！"),
                                  close: function () { Print_Expense_Point($("#lblOrderUSer").html(), global_mem, $("#chkPrint").attr("checked"), $("#txtIsTimeExpense").val(), json.point); }
                              });
                           break;
                       case -6:
                           art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("本店积分不足无法消费，请与总店联系！"),
                                  lock: true
                              });
                           break;
                       default:
                           art.dialog
                                ({
                                    title: '系统提示',
                                    time: 2,
                                    content: '消费成功！' + (json.strUpdateMemLevel == undefined ? "" : json.strUpdateMemLevel),
                                    close: function () { Print_Expense_Point($("#lblOrderUSer").html(), global_mem, $("#chkPrint").attr("checked"), $("#txtIsTimeExpense").val(), json.point); }
                                });
                           break;
                   }
               });
}

/*******************************************************************************
*重置按钮响应函数
*******************************************************************************/
function BtnExpenseReset() {
    window.location.href = '../MemExpense/Expense.aspx?PID=17';
}

/****************************************************************************************************
*在选择好会员时可以执行回调函数
*****************************************************************************************************/
function FindMember_CallBack() {
    var strErrorMsg;
    if (global_mem.MemState != 0) {
        strErrorMsg = "当前会员卡处于锁定或者挂失状态，暂不允许进行消费。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    if (global_mem.MemIsPast == "True") {
        strErrorMsg = "当前会员卡已过期，暂不允许进行消费。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    //查找到会员后 所有控件解禁
    if (global_mem.MemID != "") {
        if ($("#txtIsTimeExpense").val() == "0") {
            $("#txtExpMoney").attr("disabled", "");
            $("#txtDiscountMoney").attr("disabled", "");
            $("#txtExpPoint").attr("disabled", "");
            $("#txtExpMoney").val("");
            $("#txtDiscountMoney").val("");
            $("#txtExpPoint").val("");
            $("#txtExpMoney").focus();
        }
        else {
            $("#txtExpMoney").attr("disabled", "");
            $("#txtDiscountMoney").attr("disabled", "");
            $("#txtExpPoint").attr("disabled", "");
            $("#txtExpMoney").focus();
        }
    }
    return true;
}

/****************************************************************************************************
*点散客选择框执行回调函数
*****************************************************************************************************/
function NoMember_CallBack(type) {
    if (type == 0) {

        $("#txtExpMoney").val("");
        $("#txtDiscountMoney").val("");
        $("#txtExpPoint").val("");
        $("#txtExpRemark").val("");
        $("#txtExpMoney").attr("disabled", "");
        $("#txtDiscountMoney").attr("disabled", "");
        $("#txtExpPoint").attr("disabled", "disabled");
        $("#txtExpRemark").attr("disabled", "");
        $("#txtFindMember").focus();
    }
    else {
        $("#txtExpMoney").attr("disabled", "").focus();
        $("#txtDiscountMoney").attr("disabled", "");
        $("#txtExpPoint").attr("disabled", "disabled");
        $("#txtExpRemark").attr("disabled", "");
        $("#txtExpPoint").val("");
    }
    if ($("#chkNoMember").attr("checked") == true)
    {
        $("#txtExpMoney").focus();
    }
}