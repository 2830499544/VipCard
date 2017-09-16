$(document).ready(function () {

    //没有查询会员时 所有空间禁用
    if ($.isEmptyObject(global_mem)) {
        $("#txtDrawMoney").attr("disabled", "disabled");
        $("#txtAccountMoney").attr("disabled", "disabled");
        $("#txtDrawRemark").attr("disabled", "disabled");
    };
    //如果没有启用发送短信功能，隐藏发送短信
    //    if (!$("#chkIsSMS").attr("checked")) {
    //        $("#lblIsSMS").css("display", "none");
    //    }
    if ($("#MemCard").val() != null) {
        $("#txtFindMember").val($("#MemCard").val());
    }
    $("#txtDrawMoney").bind("keyup", AccountMoney);
    $("#btnDrawSave").bind("click", btnDrawSave);
    $("#btnDrawMoneyReset").bind("click", btnDrawMoneyReset);
});

//金额处理
function AccountMoney() {
    var drawMoneyPercent = $("#txtDrawPercent").val();
    if (isFloatPositive($(this))) {
        $("#txtAccountMoney").val(parseFloat(accDiv(parseFloat($(this).val()), drawMoneyPercent)).toFixed(2));
    }
    else {
        $("#txtAccountMoney").val("0");
    }
    if ($("#txtDrawMoney").val() == "") {
        $("#txtAccountMoney").val("0");
    }
}
//重置按钮响应事件
function btnDrawMoneyReset() {
    window.location.href = '../Member/MemDrawMoney.aspx?PID=10';
};

//保存按钮响应事件
function btnDrawSave() {
    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员！</li>";
    }
    var drawMoneyPercent = $("#txtDrawPercent").val();
    var memMoney = (global_mem.MemMoney != "") ? global_mem.MemMoney : 0;
    var maxMoney = accMul(parseFloat(memMoney), drawMoneyPercent);

    if ($.trim($("#txtDrawMoney").val()) == "") {
        strErrorMsg += "<li>请输入要提现的金额！</li>";
    }

    if (parseFloat($.trim($("#txtDrawMoney").val())) <= 0) {
        strErrorMsg += "<li>输入的提现金额必须大于0！</li>";
    }

    if (parseFloat($.trim($("#txtDrawMoney").val())) > maxMoney) {
        strErrorMsg += "<li>提现金额超出会员账户金额，请重新填写！</li>";
    }
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        //var throughBox = art.dialog.through;
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    //    if (!confirm("您确定要执行提现操作么？")) { return false; }

    art.dialog({
        title: '系统提示',
        content: '将为会员 [' + global_mem.MemName + '],提取金额:[' + $.trim($("#txtDrawMoney").val()) + '] ,实际扣除:[' + $('#txtAccountMoney').val() + '] 元。\n确定操作吗？',
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
               "DrawMoney",
               {
                   "account": $("#lblMemDrawMoney").html(),
                   "memID": global_mem.MemID,
                   "money": $.trim($("#txtDrawMoney").val()),
                   "actualmoney": $.trim($("#txtAccountMoney").val()),
                   "remark": $("#txtDrawRemark").val(),
                   "sendSMS": $("#chkSMS").attr("checked")
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
                        case 1:
                            art.dialog
                             ({
                                 title: '系统提示',
                                 time: 0.5,
                                 content: '会员提现成功',
                                 close: function () {
                                      Mem_DRaw_money($("#lblOrderUSers").val(), global_mem, $("#chkPrint").attr("checked"), "1", "1");
                                      location.href = location.href;
                                  },
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
                                  time: 4,
                                  content: ("会员提现成功，短信余额不足，不能发送短信，请充值短信！"),
                                  close: function () { location.href = location.href; },
                                  lock: true
                              });
                            break;
                        case -3:
                            art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("提现金额小于会员余额，不可提现！"),
                                  close: function () { location.href = location.href; },
                                  lock: true
                              });
                              break;
                          case -5:
                              art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("会员提现成功,发送短信失败,本店拥有的短信量不足请与总店联系！"),
                                  close: function () { location.href = location.href; },
                                  lock: true
                              });
                              break;
                    }
                });
        },
        cancelVal: '取消',
        cancel: true
    });

};


/****************************************************************************************************
*在选择好会员时可以执行回调函数
*****************************************************************************************************/
function FindMember_CallBack() {
    var strErrorMsg;
    var drawMoneyPercent = $("#txtDrawPercent").val();
    if (global_mem.MemState != 0) {
        strErrorMsg = "当前会员卡处于锁定或者挂失状态，暂不允许进行提现。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    if (global_mem.MemIsPast == "True") {
        strErrorMsg = "当前会员卡已过期，暂不允许进行提现。";
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
        $("#txtDrawMoney").attr("disabled", "");
        $("#txtAccountMoney").attr("disabled", "");
        $("#txtDrawRemark").attr("disabled", "");
        $("#txtDrawMoney").focus();
    }
    var strMemMoney = (global_mem.MemMoney != "") ? global_mem.MemMoney : 0;
    $("#spanCurrentMoney").html(parseFloat(strMemMoney) + "  (最多可提现：" + GetFloat(strMemMoney * drawMoneyPercent) + "元)");
    return true;
}
function GetFloat(floats) {
    if (parseInt(floats) == floats) {
        return floats;
    }
    else {
        var a = floats.toString();
        var aNew;
        var re = /([0-9]+\.[0-9]{2})[0-9]*/;
        aNew = a.replace(re, "$1");
        return aNew;
    }
}