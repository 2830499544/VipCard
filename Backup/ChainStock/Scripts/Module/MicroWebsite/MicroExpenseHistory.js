$(document).ready(function () {
    //BindNullList("tbExpenseHistory");
})
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
/***********************************************************
*展开详情
arg2为 是否展开的条件参数
************************************************************/
function ShowExpenseDetail(id, arg2) {
    if (arg2 > 0) {
        if ($("#data" + id).css("display") == "none") {
            $("#data" + id).css("display", "");
            $("#msg" + id).css("display", "");
            $("#detail" + id).css("display", "");
            $("#img" + id).attr("src", "../Inc/Style/images/minus.gif")
        }
        else {
            $("#data" + id).css("display", "none");
            $("#msg" + id).css("display", "none");
            $("#detail" + id).css("display", "none");
            $("#img" + id).attr("src", "../Inc/Style/images/plus.gif")
        }
    }
    else {
        $("#data" + id).css("display", "none");
        $("#msg" + id).css("display", "none");
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
