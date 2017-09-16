
$(document).ready(function () {
    $('#txtStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $('#txtEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
});




var dclCouponMoney;
/*************************************************************************************************************
商品退货JS处理
**************************************************************************************************************/
$(document).ready(function () {
    //高级查询
    $("#btnhigthSearch").bind("click", btnhigthSearch);
    //高级查询中的重置
    $("#btnMemReset").bind("click", btnMemReset);
})
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
    $("#txtOrderAccount").val("");
    //    $("#txtStartTime").val("");
    //    $("#txtEndTime").val("");
    $("#sltShop").val("");
    $("#sltConsumptionShop").val("");
    //    $("#sltDiscountMoney").val("&gt;=");
    $("#txtDiscountMoney").val("0");
    $("#sltMemLevelID").val("");
    $("#sltOrderType").val("");
    //    $("#sltTotalMoney").val("&gt;=");
    $("#txtTotalMoney").val("0");
    $("#txtRemark").val("");
}
/*************************************************************************************************************
整单撤销
**************************************************************************************************************/
function Revoke(orderID, memID, shopid) {
    if ($("#txtShopid").val() == shopid) {
        art.dialog
    ({
        content: '您确定要撤销整单吗？此操作不可恢复，请慎重！',
        ok: function () {
            doAjax("../",
            "Revoke",
            {
                "orderID": orderID,
                "memID": memID
            },
            "json",
            function (json) {
                switch (json.Success) {
                    case "0":
                        art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统异常，未保存数据，请再次点击保存！"),
                                     lock: true
                                 });
                        break;
                    case "-1":
                        art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统错误 请与系统管理员联系！"),
                                     lock: true
                                 });
                        break;
                    case "-2":
                        art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("短信余额不足，不能发送短信，请充值短信！"),
                                  lock: true
                              });
                        break;
                    case "-3":
                        art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("会员积分小于撤单应该撤销的积分，不能撤销此单"),
                                  lock: true
                              });
                        break;
                    case "-4":
                        art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("散客不允许撤单！"),
                                  lock: true
                              });
                        break;
                    default:
                        art.dialog
                                ({
                                    title: '系统提示',
                                    time: 2,
                                    content: '撤单成功！' + (json.strUpdateMemLevel == undefined ? "" : json.strUpdateMemLevel),
                                    close: function () { location.href = location.href; }
                                });
                        break;
                }
            });
        },
        cancelVal: '取消',
        cancel: true
    });
    }
    else {
        art.dialog
            ({
                title: '系统提示',
                time: 2,
                content: ("只能对本商家消费记录进行撤单操作！"),
                close: function () { location.href = location.href; },
                lock: true
            });
    }
}

function btQuery() {
    var strErrorMsg = "";
    if (!$("#txtDiscountMoney").val().IsDecimal() && $("#txtDiscountMoney").val() != "") {
        strErrorMsg += "<li>输入实收金额格式不正确,请重新输入!</li>";
    }
    if (!$("#txtTotalMoney").val().IsDecimal() && $("#txtTotalMoney").val() != "") {
        strErrorMsg += "<li>输入应收金额格式不正确,请重新输入!</li>";
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
    return true;
}

function GetCouponMoney() {
    doAjax("../",
           "GetCouponMoney",
           { "orderID": $("#ordrID").val() },
           "json",
           function (json) {
               dclCouponMoney = json.couponMoney;
           }
    )
}

function Print(orderID, orderType, memID) {

    //获取打印的份数
    var PointNum = $("#PointNum").val(); 

    doAjax("../",
            "SecondPrinting",
            {
                "orderID": orderID,
                "orderType": orderType,
                "memID": memID
            },
            "json",
            function (json) {
                if (json != "") {
                    var userName = $("#txtUser").val();
                    if (json.orderType == 0 || json.orderType == 1) {//快速消费
                        SecondExpensePrint(userName, json.mem[0], json.expense[0], PointNum);
                    }
                    else if (json.orderType == 7) {//记次消费
                        SecondMemCountExpensePrint(userName, json.mem[0], json.expense[0], json.expenseDetail, PointNum);
                    }
                    else {//商品消费
                        SecondGoodsExpensePrint(userName, json.mem[0], json.expense[0], json.expenseDetail, PointNum);
                    }
                }
            });
}


/***********************************************************
*展开详情
arg2为 是否展开的条件参数
************************************************************/
function ShowExpenseDetail(id, arg2) {
    if (arg2 > 0) {
        if ($("#data" + id).css("display") == "none") {
            $("#data" + id).css("display", "");
            $("#img" + id).attr("src", "../Inc/Style/images/minus.gif")
        }
        else {
            $("#data" + id).css("display", "none");
            $("#img" + id).attr("src", "../Inc/Style/images/plus.gif")
        }
    }
    else {
        $("#data" + id).css("display", "none");
    }
}

function IsShowPic(id, arg2) {
    if (arg2 > 0) {
        $("#img" + id).css("display", "");
    }
    else {
        $("#img" + id).css("display", "none");
        $("#a" + id).css("padding-left", "22px");
    }
}
