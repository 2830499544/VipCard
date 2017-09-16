$(document).ready(function () {
    $("#lblStartTime").html(GetDataTimeNow());
    $("#txtHandoverUserID").bind("keyup", Qusers);
    $("#txtsjMoneys").bind("keyup", Qsjje);
    $("#btnSave").bind("click", btnSave);
    doAjax(
        "../",
        "GetRptTotal",
        {
            "checkRadion": 6,
            "txttimeStart": $("#hdStartTime").val(),
            "txttimeEnd": $("#hdEndTime").val(),
            "UserID": $("#hdUserID").val(),
            "ShopID": $("#hdShopID").val()
        },
        "json",
       function (json) {
           switch (json) {
               case "0":
                   art.dialog.alert("系统异常，未保存数据，请再次点击保存！");
                   break;
               case "-1":
                   art.dialog.alert("数据异常，请重新输入，然后重试！");
                   break;
               case "-3":
                   art.dialog.alert("系统错误 请与系统管理员联系！");
                   break;
               default:
                   $("#lblMemNumber").html(json.intNumber);  //新增会员数量
                   $("#lblSRechargeMoney").html("￥" + json.memSRechargeMoney); //初始充值总金额
                   $("#lblFRechargeMoney").html("￥" + json.memFRechargeMoney); //常规充值总金额
                   $("#lblRechargeBank").html("￥" + json.expenseBankSumMoneys); //银联充值总金额
                   $("#lblFRechargeGiveMoney").html("￥" + json.FRechargeGiveMoney); //充值赠送总金额
                   $("#lblExpenseSumMoneys").html("￥" + json.expenseSumMoneys); //现金支付金额
                   $("#lblCardMoney").html("￥" + json.payCard); //卡内支付金额  
                   $("#lblExpenseBinkMoneys").html("￥" + json.payBink); //银联支付金额 
                   $("#lblExpenseCouponMoneys").html("￥" + json.payCoupon); //优惠券支付金额 
                   $("#lblAllMoneys").html("￥" + accAdd(json.doWorkallMoney, $("#hdallmoney").val())); //实际现金应收入
                   $("#lblAllMoneys").css("color", "red");
                   $("#hdyjjg").val(accAdd(json.allMoney, $("#hdallmoney").val()));
                   $("#lblCountCardMoney").html("￥" + json.countPayCard); //计次余额支付
                   $("#lblCountCashMoney").html("￥" + json.countSumMoneys); //计次现金支付
                   $("#lblCountBankMoney").html("￥" + json.countPayBink); //计次银联支付
                   $("#lblCountCouponMoney").html("￥" + json.countpayCoupon)//计次优惠券金额
                   $("#lblStorageTimingPayCard").html("￥" + json.StorageTimingPayCard)//充时优惠券金额
                   $("#lblStorageTimingPayCash").html("￥" + json.StorageTimingPayCash)//充时优惠券金额
                   $("#lblStorageTimingPayBink").html("￥" + json.StorageTimingPayBink)//充时优惠券金额
                   $("#lblStorageTimingPayCoupon").html("￥" + json.StorageTimingPayCoupon)//充时优惠券金额
                   $("#lbAllDrawMoney").html("￥" + json.AllDrawMoney); //提现金额
                   $("#lblAllDrawActualMoney").html("￥" + json.AllDrawActualMoney); //扣除余额
                   break;
           }
       });
})
function Qusers() {
    var usernumber = $("#txtHandoverUserID").val();
    if (usernumber != "") {
        doAjax(
                           "../",
                           "GetUserName",
                           { "Number": $.trim($("#txtHandoverUserID").val()) },
                           "json",
                           function (json) {
                               if (json == "") { $("#lblname").html("未找到指定的管理员！"); $("#hdjjid").val(""); return; }
                               if (json[0].UserID == $("#hdUserID").val()) { $("#lblname").html("交接人不能设置为自己"); $("#hdjjid").val(""); return; }
                               $("#lblname").html("交接给：" + json[0].UserName);
                               $("#hdjjid").val(json[0].UserID);

                           });
    }
}
function btnSave() {
    if ($("#txtsjMoneys").val() == "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: "请输入实际收款金额",
            lock: true
        });
        return false;
    }
    if ($("#hdjjid").val() == "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: "请输入接班的员工编号",
            lock: true
        });
        return false;
    }
    doAjax(
                           "../",
                           "AddUserWork",
                           {
                               "hdUserID": $("#hdUserID").val(),
                               "hdStartTime": $("#hdStartTime").val(),
                               "lblMemNumber": $("#lblMemNumber").html(),
                               "lblCardMoney": accAdd($("#lblCardMoney").html().replace("￥", ""), $("#lblCountCardMoney").html().replace("￥", "")),
                               "lblExpenseSumMoneys": accAdd($("#lblExpenseSumMoneys").html().replace("￥", ""), $("#lblCountCashMoney").html().replace("￥", "")),
                               "lblExpenseBinkMoneys": accAdd($("#lblExpenseBinkMoneys").html().replace("￥", ""), $("#lblCountBankMoney").html().replace("￥", "")),
                               "lblExpenseCouponMoneys": accAdd($("#lblExpenseCouponMoneys").html().replace("￥", ""), $("#lblCountCouponMoney").html().replace("￥", "")),
                               "lblSRechargeMoney": $("#lblSRechargeMoney").html(),
                               "lblFRechargeMoney": $("#lblFRechargeMoney").html(),
                               "lblRechargeBank": $("#lblRechargeBank").html(),
                               "lblFRechargeGiveMoney": $("#lblFRechargeGiveMoney").html(),
                               "hdyjjg": $("#hdyjjg").val(),
                               "txtsjMoneys": $("#txtsjMoneys").val(),
                               "hdjjid": $("#hdjjid").val(),
                               "hdye": $("#hdye").val(),
                               "hdShopID": $("#hdShopID").val()
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
                                             content: '保存成功！',
                                             close: function () {
                                                 top.location.href = "../login.aspx?type=loginout";
                                             },
                                             lock: true
                                         });
                                       break;
                               }
                           });
}
function Qsjje() {
    var sjmoney = $("#txtsjMoneys").val().ToFloat();
    var bymoney = $("#hdyjjg").val().ToFloat();
    if (sjmoney < bymoney) {
        ye = accSub(bymoney, sjmoney);
        $("#lblArrearage").html("余额：" + ye + "转入下班");
        $("#hdye").val(ye);
    }
    else {
        $("#lblArrearage").html("");
        $("#hdye").val(0);
    }
} 