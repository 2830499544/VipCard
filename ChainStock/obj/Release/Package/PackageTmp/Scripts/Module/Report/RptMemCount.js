

$(document).ready(function () {
    $("#txtShop").val($("#HDsltshop").val());
     });

function btQuery() {
    var strErrorMsg = "";
    if (!$("#txtMoney").val().IsDecimal() && $("#txtMoney").val() != "") {
        strErrorMsg += "<li>输入冲次金额格式不正确,请重新输入!</li>";
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