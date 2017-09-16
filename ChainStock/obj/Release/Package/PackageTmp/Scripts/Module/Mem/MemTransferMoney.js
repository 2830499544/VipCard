$(document).ready(function () {
    //没有查询会员时 所有空间禁用
    if ($.isEmptyObject(global_mem)) {
        $("#txtMoney").attr("disabled", "disabled");
        $("#txtTransferMemCard").attr("disabled", "disabled");
        $("#txtPassword").attr("disabled", "disabled");
        $("#txtTransferRemark").attr("disabled", "disabled");       
    };

    if ($("#MemCard").val() != null) {
        $("#txtFindMember").val($("#MemCard").val());
    }
 
    //保存按钮响应函数
    $("#btnTransferSave").bind("click", BtnTransferSave);

    //重置按钮响应函数
    $("#btnTransferReset").bind("click", BtnTransferReset);
});

/*******************************************************************************
*保存按钮响应函数
*******************************************************************************/

function BtnTransferSave() {
    var strErrorMsg = "";
    var transferToMemCard = $("#txtTransferMemCard").val();
    var money = $("#txtMoney").val();
    var password = $("#txtPassword").val();
    // if (oppositeMemCard != $("#txtOppositeMemCardok").val()) strErrorMsg += "<li>两次输入的卡号不正确;</li>";
    if (money <= 0) strErrorMsg += "<li>输入金额必须大于0;</li>";
    if (transferToMemCard == "") strErrorMsg += "<li>必须输入对方卡号;</li>";
    if (!money.IsDecimal()) strErrorMsg += "<li>请输入有效金额;</li>";
    if (global_mem.MemMoney < parseFloat(money)) strErrorMsg += "<li>卡上余额不足;</li>";
    if (password == "") strErrorMsg += "<li>请输入会员密码;</li>";
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提示',
            icon: 'error',
            //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    art.dialog({
        title: '系统提示',
        content: '将为卡号:[' + transferToMemCard + '] ,转账:[' + money + '] 元。\n确定操作吗？',
        lock: true,
        ok: function () {
            this.close();
            doAjax("../",
           "TransferMoney",
           {
               "transferToMemCard": transferToMemCard,
               "money": money,
               "password": password,
               "transferFromMemID": global_mem.MemID,
               "transferRemark": $("#txtTransferRemark").val(),
               "transferTime": $("#lblTransferTime").html(),
               "account": $("#lblAccount").html()
           },
           "json",
           function (json) {
               switch (json) {
                   case -1:
                       art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("密码错误，请重试！"),
                              lock: true
                          });
                       break;
                   case -2:
                       art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("卡号无效，未找到收款会员！"),
                              lock: true
                          });
                       break;
                   case -3:
                       art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("系统操作失败，请稍后重试！"),
                              lock: true
                          });
                       break;     
                   default:
                       art.dialog
                            ({
                                title: '系统提示',
                                lock: true,
                                time: 0.5,
                                content: '转账成功！',
                                close: function () {
                                    window.location.href = "../Member/MemTransferMoney.aspx";
                                }
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
function BtnTransferReset() {
    window.location.href = '../Member/MemTransferMoney.aspx?PID=163';
}

/****************************************************************************************************
*在选择好会员时可以执行回调函数
*****************************************************************************************************/
function FindMember_CallBack() {
    var strErrorMsg;
    if (global_mem.MemState != 0) {
        strErrorMsg = "当前会员卡处于锁定或者挂失状态，暂不允许进行充值。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;

    }
    if (global_mem.MemIsPast == "True") {
        strErrorMsg = "当前会员卡已过期，暂不允许进行转账。";
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
        $("#txtMoney").attr("disabled", "");       
        $("#txtTransferMemCard").attr("disabled", "");
        $("#txtPassword").attr("disabled", "");
        $("#txtTransferRemark").attr("disabled", "");              
        $("#txtMoney").focus().select();     
    }  
    return true;
}

