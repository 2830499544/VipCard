$(document).ready(function () {
    //没有查询会员时 所有空间禁用
    if ($.isEmptyObject(global_mem)) {
        $("#txtMoney").attr("disabled", "disabled");
        $("#txtGiveMoney").attr("disabled", "disabled");
        $("#txtTotal").attr("disabled", "disabled");
        $("#txtRechangeRemark").attr("disabled", "disabled");
        $("#txtPoint").attr("disabled", "disabled");
        $("#chkBank").attr("disabled", "disabled");
        //$("#sltSpecial").attr("disabled", "disabled");
    };

    if ($("#MemCard").val() != null) {
        $("#txtFindMember").val($("#MemCard").val());
    }
    //如果没有启用发送短信，隐藏发送短信选择框
    //    if (!$("#chkIsSMS").attr("checked")) {
    //        $("#lblIsSMS").css("display", "none");
    //    }

    //如果没有启用打印小票，隐藏打印小票选择框
    if (!$("#chkPrint").attr("checked")) {
        $("#lblIsPrint").css("display", "none");
    }

    //保存按钮响应函数
    $("#btnRechageSave").bind("click", BtnRechargeSave);

    //重置按钮响应函数
    $("#btnRechargeReset").bind("click", BtnRechargeReset);

    //充值合计处理
    $("#txtMoney").bind("keyup", TotalMoney);
    $("#txtGiveMoney").bind("keyup", TotalMoney);
    $("#txtMoney").bind("click", MoneySelect);
    $("#txtGiveMoney").bind("click", GiveMoneySelect);
    $('#txtTotal').css("background-color", "#eee");
    $('#txtTotal').attr("readonly", true);
  //  $("#sltSpecial").bind("change", Specialselect);
});

/*******************************************************************************
*充值合计处理
*******************************************************************************/
function TotalMoney() {
    var dclMoney = $("#txtMoney").val() != "" ? parseFloat($("#txtMoney").val()) : 0;
    var dclGiveMoney = $("#txtGiveMoney").val() != "" ? parseFloat($("#txtGiveMoney").val()) : 0;
    if (isFloatPositive($(this))) {

        doAjax("../", "GetGiveMoney", { "RechargeMoney": dclMoney }, "json",
             function (json) {
                 if (json != "") {
                     $("#txtGiveMoney").val(json);
                     var dclGiveMoney = $("#txtGiveMoney").val() != "" ? parseFloat($("#txtGiveMoney").val()) : 0;
                     $("#txtTotal").val((dclMoney + dclGiveMoney).toFixed(2));
                     if (parseFloat(global_mem.ClassRechargePointRate) > 0) {
                         $("#txtPoint").val(Math.floor(Math.floor(dclMoney) / parseFloat(global_mem.ClassRechargePointRate)));
                     }
                 }
                 else {
                     $("#txtGiveMoney").val("0");
                     var dclGiveMoney = $("#txtGiveMoney").val() != "" ? parseFloat($("#txtGiveMoney").val()) : 0;
                     $("#txtTotal").val((dclMoney + dclGiveMoney).toFixed(2));
                     if (parseFloat(global_mem.ClassRechargePointRate) > 0) {
                         $("#txtPoint").val(Math.floor(Math.floor(dclMoney) / parseFloat(global_mem.ClassRechargePointRate)));
                     }
                 }
             });
    }
    
}
function GiveMoneySelect() {
    if ($("#txtGiveMoney").focus) {
        $("#txtGiveMoney").select();
    }
}
function MoneySelect() {
    if ($("#txtMoney").focus) {
        $("#txtMoney").select();
    }
}
/*******************************************************************************
*保存按钮响应函数
*******************************************************************************/
function BtnRechargeSave() {
    var strErrorMsg = "";

    //获取打印的份数
    var PointNum = $("#PointNum").val(); 

    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员！</li>";
    }

    var Name = global_mem.MemName;
    var MemCard = global_mem.MemCard;
    var MemID = global_mem.MemID;
    if ($.trim($("#txtMoney").val()) == "") {
        strErrorMsg += "<li>请输入要充值的金额！</li>";
    }
    if (parseInt($.trim($("#txtPoint").val())) < 0) {
        strErrorMsg += "<li>请确保获得积分不小于0！</li>";
    }
    if (parseFloat($.trim($("#txtTotal").val())) <= 0) {
        strErrorMsg += "<li>请确保充值总金额大于0！</li>";
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

    art.dialog({
        title: '系统提示',
        content: '将为会员 [' + Name + '] ,卡号:[' + MemCard + '] ,充值:[' + $('#txtMoney').val() + '] ,赠送:[' + $('#txtGiveMoney').val() + '] 元。\n确定操作吗？',
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
               "RechargeMoney",
               {
                   "memID": MemID,
                   "rechargeAccount": $("#lblAccount").html(),
                   "money": $.trim($("#txtMoney").val()),
                   "giveMoney": $.trim($("#txtGiveMoney").val()),
                   "remark": $("#txtRechangeRemark").val(),
                   "createTime": $("#lblRechargeTime").html(),
                   "sendSMS": $("#chkSMS").attr("checked"),
                   "isbank": $("#chkBank").attr("checked"),
                   "point":$("#txtPoint").val()
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
                                  time: 4,
                                  content: ("会员充值成功，但短信余额不足，不能发送短信，请充值短信！"),
                                  close: function () { Print_RechageMoney_Point($("#lblRechargeUSer").html(), global_mem, $("#chkPrint").attr("checked"), PointNum); },
                                  lock: true
                              });
                            break;
                        case -5:
                            art.dialog
                            ({
                                title: '系统提示',
                                time: 4,
                                content: ("发送短信失败,本店拥有的短信量不足请与总店联系！"),
                                lock: true
                            });
                            break;
                        case -6:
                            art.dialog
                            ({
                                title: '系统提示',
                                time: 4,
                                content: ("该店铺没有充值权限，请联系运营商！"),
                                lock: true
                            });
                            break;
                        case -7:
                            art.dialog
                            ({
                                title: '系统提示',
                                time: 4,
                                content: ("该店铺在结算周期内的充值金额已达到上线，不能再充值！"),
                                lock: true
                            });
                            break;
                        default:
                            art.dialog
                            ({
                                title: '系统提示',
                                time: 0.5,
                                content: '会员充值成功',
                                close:
                                function () {
                                    Print_RechageMoney_Point($("#lblRechargeUSer").html(), global_mem, $("#chkPrint").attr("checked"), PointNum);

                                    window.location.href = "../Member/MemRechargeMoney.aspx?PID=4";
                                },
                                lock: true
                            });
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
function BtnRechargeReset() {
    window.location.href = '../Member/MemRechargeMoney.aspx?PID=4';
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
        strErrorMsg = "当前会员卡已过期，暂不允许进行充值。";
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
        $("#txtGiveMoney").attr("disabled", "");
        $("#txtTotal").attr("disabled", "");
        $("#txtRechangeRemark").attr("disabled", "");
        $("#txtPoint").attr("disabled", "");
        $("#txtMoney").focus().select();
        $("#chkBank").attr("disabled", "");
        $("#sltSpecial").attr("disabled", "");
    }
    AddSpecial();
    return true;
}

function AddSpecial() {
    var selectObj = document.getElementById("sltSpecial");
    selectObj.options.length = 0;
    doAjax(
           "../",
           "BindSpecialselect",
           {
               "MemID": global_mem.MemID
           },
           "json",
           function (json) {
               selectObj.options[selectObj.length] = new Option("======请选择======", "");
               $.each(json, function (index, item) {
                   //alert(selectObj.length);
                   selectObj.options[selectObj.length] = new Option(item.Name, item.ID);
                   //var goodsDiscount = item.GoodsDiscount;
               });
           });
}


function Specialselect() {
    var id = $("#sltSpecial").val();
    if (id != "") {
        $("#txtMoney").attr("disabled", "disabled");
        $("#txtGiveMoney").attr("disabled", "disabled");
        $("#txtTotal").attr("disabled", "disabled");
        $("#txtPoint").attr("disabled", "disabled");
        $("#chkBank").attr("disabled", "disabled");
        doAjax("../",
               "SpecialList",

                  { "SpecialID": id },

               "json",
                function (json) {

                    $("#txtMoney").val(parseFloat(json[0].SpecialRecharge).toFixed(2));
                    $("#txtGiveMoney").val(parseFloat(json[0].SpecialGive).toFixed(2));
                    $("#txtIsmhCoins").val(json[0].Sremark);
                    TotalMoney();

                }

               )
    } else {
        $("#txtMoney").attr("disabled", "");
        $("#txtMoney").val("");
        $("#txtGiveMoney").attr("disabled", "");
        $("#txtGiveMoney").val("");
        $("#txtTotal").attr("disabled", "");
        $("#txtTotal").val("");
        $("#txtRechangeRemark").attr("disabled", "");
        $("#txtRechangeRemark").val();
        $("#txtPoint").attr("disabled", "");
        $("#txtPoint").val("");
        $("#txtMoney").focus().select();
        $("#chkBank").attr("disabled", "");
       // $("#sltSpecial").attr("disabled", "");
    }

}