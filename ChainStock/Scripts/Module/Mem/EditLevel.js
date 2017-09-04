$(document).ready(function () {
    $("#txtLevelDiscountPercent").bind("keyup", SetAllLevelDiscountPercent);
    $("#txtLevelPointPercent").bind("keyup", SetAllLevelPointPercent);
});

function SetAllLevelDiscountPercent() {
    if ($("#txtLevelID").val() == "") {
        $.each($("input[gm='txtMyDiscountPercent']"), function (i, item) {
            item.value = $("#txtLevelDiscountPercent").val();
        });
    }
}
function SetAllLevelPointPercent() {
    if ($("#txtLevelID").val() == "") {
        $.each($("input[gm='txtMyPointPercent']"), function (i, item) {
            item.value = $("#txtLevelPointPercent").val();
        });
    }
}
/******************************************************************************
*保存按钮响应函数
*******************************************************************************/
function BtnSetLevel() {
    var strErrorMsg = "";

    var strLevelName = $.trim($("#txtLevelName").val());
    if (strLevelName.IsEmpty()) {
        strErrorMsg += "<li>必须输入等级名称;</li>";
    }

    var strLevelPoint = $.trim($("#txtLevelPoint").val());
    if (strLevelPoint.IsEmpty()) {
        strErrorMsg += "<li>必须输入等级所需积分;</li>";
    }
    else {
        if (!strLevelPoint.IsNumber()) {
            strErrorMsg += "<li>所需积分输入格式有误，必须输入正确的数字；</li>";
        }
    }
    var strLevelDiscount = $.trim($("#txtLevelDiscountPercent").val());
    if (strLevelDiscount.IsEmpty()) {
        strErrorMsg += "<li>快速消费-必须输入等级折扣率;</li>";
    }
    else {
        if (!strLevelDiscount.IsNumber() || parseInt(strLevelDiscount) > 100 || parseInt(strLevelDiscount) <= 0) {
            strErrorMsg += "<li>快速消费-等级折扣输入错误，必须为1-100之间的正整数</li>";
        }
    }

    var strLevelPointPercent = $.trim($("#txtLevelPointPercent").val());
    if (strLevelPointPercent.IsEmpty()) {
        strErrorMsg += "<li>快速消费-必须输入积分比率;</li>";
    }
    else {
        if (!strLevelPointPercent.IsDecimal() || parsefloat(strLevelPointPercent) <= 0) {
            strErrorMsg += "<li>快速消费-积分比率输入错误，必须为大于等于0的正整数;</li>";
        }
    }
    $.each($("input[gm='txtMyDiscountPercent']"), function (i, item) {
        if (item.value.IsEmpty()) {
            strErrorMsg += "<li>" + item.myname.toString() + "-必须输入等级折扣率;</li>";
        }
        else {
            if (!item.value.IsNumber() || parseInt(item.value) > 100 || parseInt(item.value) <= 0) {
                strErrorMsg += "<li>" + item.myname.toString() + "-等级折扣输入错误，必须为1-100之间的正整数</li>";
            }
        }
    });
    $.each($("input[gm='txtMyPointPercent']"), function (i, item) {
        if (item.value.IsEmpty()) {
            strErrorMsg += "<li>" + item.myname + "-必须输入积分比率;</li>";
        }
        else {
            if (!item.value.IsDecimal() || parsefloat(item.value) <= 0) {
                strErrorMsg += "<li>" + item.myname.toString() + "-积分比率输入错误，必须为大于等于0的正整数;</li>";
            }
        }
    });
    var strLevelRechargePointRate = $.trim($("#txtLevelRechargePointRate").val());
    if (strLevelRechargePointRate.IsEmpty()) {
        strErrorMsg += "<li>会员充值-必须输入积分比率;</li>";
    }
    else {
        if (!strLevelRechargePointRate.IsDecimal() || parsefloat(strLevelRechargePointRate) < 0) {
            strErrorMsg += "<li>会员充值-积分比率输入错误，必须为大于等于0的正整数;</li>";
        }
    }

    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    return true;

}