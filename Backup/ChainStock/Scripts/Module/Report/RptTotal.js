$(document).ready(function () {
    $("#btnSerach").bind("click", btnSerach);
    $("#rdTimeDuan").bind("click", rdTimeDuan);
    //绑定日期控件
    $('#txtStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm', maxDate: '%y-%M-%d %H:%m:%s', isShowClear: false, readOnly: true });
    });

    //绑定日期控件
    $('#txtEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm', maxDate: '%y-%M-%d %H:%m:%s', isShowClear: false, readOnly: true });
    });
});

function btnSerach() {
    var strErrorMsg = "";
    var checkRdo = $('input[name="group"]:checked').val();

    if (checkRdo == "6") {
        if ($("#txtStartTime").val() == "" || $("#txtEndTime").val() == "") {
            strErrorMsg += "<li>时间不能为空！</li>";
            return;
        }
    }
    if (checkRdo != "6") //不是时间段的查询
    {
        $("#txtStartTime").val("");
        $("#txtEndTime").val("");
        rdTimeDuan();
    }
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    doAjax(
        "../",
        "GetRptTotal",
        {
            "checkRadion": checkRdo,
            "txttimeStart": $("#txtStartTime").val(),
            "txttimeEnd": $("#txtEndTime").val(),
            "UserID": $("#sltUserID").val(),
            "ShopID": $("#sltShop").val()
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
                   $("#lblTime").html(json.timeRadion);
                   $("#lblMemNumber").html(json.intNumber);  //新增会员数量
                   $("#lblSRechargeMoney").html("￥" + json.memSRechargeMoney); //初始充值总金额
                   $("#lblFRechargeMoney").html("￥" + json.memFRechargeMoney); //常规充值总金额
                   $("#lblRechargeBank").html("￥" + json.expenseBankSumMoneys); //银联充值总金额
                   $("#lblFRechargeGiveMoney").html("￥" + json.FRechargeGiveMoney); //充值赠送总金额
                   $("#lblExpenseSumMoneys").html("￥" + json.expenseSumMoneys); //现金支付金额
                   $("#lblCardMoney").html("￥" + json.payCard); //卡内支付金额  
                   $("#lblExpenseBinkMoneys").html("￥" + json.payBink); //银联支付金额 
                   $("#lblExpenseCouponMoneys").html("￥" + json.payCoupon); //优惠券支付金额 
                   $("#lblAllMoneys").html("￥" + json.allMoney);
                   $("#lblAllMoneys").css("color", "red");
                   $("#lblMaster").html(json.strUserName); //操作员  
                   $("#lblMemDetial").html(json.MemDetial);
                   $("#lblCountCardMoney").html("￥" + json.countPayCard); //计次余额支付
                   $("#lblCountCashMoney").html("￥" + json.countSumMoneys); //计次先进支付
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

};

function rdTimeDuan() {
    if ($("#rdTimeDuan").attr("checked") == true) {
        $("#txtStartTime").css("background-color", "#FFFFFF");
        $("#txtStartTime").removeAttr("disabled");
        $("#txtEndTime").css("background-color", "#FFFFFF");
        $("#txtEndTime").removeAttr("disabled");

    } else {
        $("#txtStartTime").css("background-color", "#eee");
        $("#txtStartTime").attr("disabled", "disabled");
        $("#txtEndTime").css("background-color", "#eee");
        $("#txtEndTime").attr("disabled", "disabled");
    }
}
 