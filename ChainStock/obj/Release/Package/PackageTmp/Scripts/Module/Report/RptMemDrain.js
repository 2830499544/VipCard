$(document).ready(function () {
});


function btQuery() {
    var strReg = /^[0-9]*[1-9][0-9]*$/; //正则表达式
    var strErrorMsg = "";
    if (!strReg.test($("#txtDay").val()) && $("#txtDay").val() != "" && $("#txtDay").val() != "0") {
        strErrorMsg += "<li>输入天数格式不正确,请重新输入!</li>";
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