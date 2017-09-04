/*************************************************************************************************************
会员刷卡页面JS处理
**************************************************************************************************************/
var exchangeTab;
var pageSize = 5;
var currentPage = 1;
$(document).ready(function () {
    //判断是否启动会员过期
    if (!$("#chkIsPast").attr("checked")) {
        $("#trIsPast").css("display", "none");
    }

    exchangeTab = new CommonTab("ExchangeTab",
       function (index) {
           var strErrorMsg = "";
           if (index != 0 && $.isEmptyObject(global_mem)) {
               strErrorMsg += "<li>请先选择会员,再进行下一步操作!</li>";
               art.dialog({
                   title: '系统提示',
                   icon: 'error', //图标
                   content: strErrorMsg,
                   lock: true
               });
               return false;
           }
           return true;
       }, function (index) {
           if (index == 1) {
               MemInfoExpenseList();
           }
           else if (index == 2) {
               MemInfoRecharge();
           }
           else if (index == 3) {
               MemInfoRechargeCount();
           }
           else if (index == 4) {
               MemInfoExchangeGift();
           } else if (index == 5) {
               MemInfoCoupon();
           }
           else if (index == 6) {
               MemInfoMobile();
           }
       });

    if ($("#MemCard").val() != null) {
        $("#txtFindMember").val($("#MemCard").val());
    }
    $("#btnEditMem").bind("click", btnEditMem);
    $("#btnExpense").bind("click", btnExpense);
    $("#btnRechargeMoney").bind("click", btnRechargeMoney);
    $("#btnRechargCount").bind("click", btnRechargCount);
    $("#btnExchangeGift").bind("click", btnExchangeGift);
    $("#btnSendSMS").bind("click", btnSendSMS);
    $("#btGoodsExpense").bind("click", btGoodsExpense);
    $("#btTimeExpense").bind("click", btTimeExpense);

    $("#btConsumeMemCount").bind("click", btConsumeMemCount);
});

//回调函数
function FindMember_CallBack() {
    exchangeTab.GoTab("0");
    MemInfoBind(global_mem);
    var html = "<tr><td style=\"height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;\" colspan=\"8\" type=\"LoadingBar\">正在加载，请稍候……</td></tr>";
    $("#MemInfoExpense").html(html);
    $("#MemInfoRecharge").html(html);
    $("#MemInfoRechargeCount").html(html);
    $("#MemInfoExchangeGift").html(html);
    $("#MemInfoCoupon").html(html);
    $("#MemInfoMobile").html(html);
    $("#MemCard").val(global_mem.MemCard);
    return true;
}

function MemInfoBind(global_mem) {
    $("#txtMemCard").html(global_mem.MemCard);
    $("#txtMemName").html(global_mem.MemName ? global_mem.MemName : "无");
    $("#txtMemCardNumber").html(global_mem.MemCardNumber ? global_mem.MemCardNumber : "无");
    $("#txtMemMobile").html(global_mem.MemMobile ? global_mem.MemMobile : "无");
    global_mem.MemState = GetMemState(global_mem.MemState);
    $("#txtMemState").html(global_mem.MemState);
    $("#txtMemLevel").html(global_mem.LevelName);
    $("#txtMemShop").html(global_mem.ShopName);
    //    global_mem.MemBirthday = GetBirthday(global_mem.MemBirthday);
    $("#txtMemBirthday").html(global_mem.MemBirthday);
    global_mem.MemSex = GetMemSex(global_mem.MemSex);
    $("#txtMemSex").html(global_mem.MemSex);
    $("#txtMemPoint").html(global_mem.MemPoint);
    $("#txtMemMoney ").html(parseFloat(global_mem.MemMoney).toFixed(2));
    $("#txtMemEmail").html(global_mem.MemEmail ? global_mem.MemEmail : "无");
    $("#txtTelephone").html(global_mem.MemTelePhone ? global_mem.MemTelePhone : "无");
    $("#txtMemIdentityCard").html(global_mem.MemIdentityCard ? global_mem.MemIdentityCard : "无");
    $("#txtMemCreateTime").html(global_mem.MemCreateTime);
    $("#txtMemUserName").html(global_mem.UserName);
    $("#txtMemRecommendCard").html((global_mem.MemRecommendID != "0") ? GetMemRecommend(global_mem.MemRecommendID) : "无");
    //    global_mem.MemPastTime = GetPastTime(global_mem.MemPastTime);
    $("#txtMemPastTime").html(global_mem.MemPastTime);
    if (global_mem.MemProvinceName != "") {
        $("#txtMemAddress").html(global_mem.MemProvinceName + "省" + global_mem.MemCityName + "市" + global_mem.MemCountyName + global_mem.MemVillageName + global_mem.MemAddress)
    }
    else {
        $("#txtMemAddress").html(global_mem.MemAddress ? global_mem.MemAddress : "无");
    }
    //    $("#txtMemAddress").html(global_mem.MemAddress ? global_mem.MemAddress : "无");
    $("#txtMemRemark").html(global_mem.MemRemark ? global_mem.MemRemark : "无");

    $("span[id^=MemInfo_Custom_]").each(function (index) {
        var field = $(this).attr("id").replace("MemInfo_Custom_", "");
        $(this).html(global_mem[field] ? global_mem[field] : "无");
    });
    if (global_mem.MemPhoto != "") {
        $("#imgMemPhoto").attr("src", global_mem.MemPhoto);
    }
    else {
        $("#imgMemPhoto").attr("src", "../images/member/nophoto.gif");
    }
    //    if (global_mem.MemQRCode != "") {
    //        $("#trTitle").attr("display", "");
    //        $("#trQrCode").attr("display", "");
    //        $("#imgQRCode").attr("src", global_mem.MemQRCode);
    //    }
    //    else {
    //        $("#trTitle").attr("display", "none");
    //        $("#trQrCode").attr("display", "none");
    //    }
};

//会员生日设置
//function GetBirthday(birthday) {
//    var strBirthday;
//    if (birthday == "1900-01-01" || birthday == "") {
//        strBirthday = "未设置";
//    }
//    else {
//        strBirthday = birthday;
//    }
//    return strBirthday;
//}

//会员过期时间设置
//function GetPastTime(pastTime) {
//    var strPastTime;
//    if (pastTime == "2900-01-01" || pastTime == "") {
//        strPastTime = "永久有效";
//    }
//    else {
//        strPastTime = pastTime;
//    }
//    return strPastTime;
//}

//响应会员编辑按钮事件
function btnEditMem() {
    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员,再进行下一步操作!</li>";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    window.location.href = "../Member/MemRegister.aspx?PID=2&MemID=" + global_mem.MemID;

}

//响应快速消费按钮事件
function btnExpense() {
    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员,再进行下一步操作!</li>";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    window.location.href = "../MemExpense/Expense.aspx?PID=17&MemCard=" + global_mem.MemCard;

}

//响应会员充值按钮事件
function btnRechargeMoney() {
    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员,再进行下一步操作!</li>";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    window.location.href = "../Member/MemRechargeMoney.aspx?PID=4&MemCard=" + global_mem.MemCard;
}

//响应会员充次按钮事件
function btnRechargCount() {
    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员,再进行下一步操作!</li>";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    window.location.href = "../Member/MemRechargeCount.aspx?PID=66&MemCard=" + global_mem.MemCard;

}

//响应会员兑换礼品按钮事件
function btnExchangeGift() {
    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员,再进行下一步操作!</li>";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    window.location.href = "../PointManage/GiftExchange.aspx?PID=14&MemCard=" + global_mem.MemCard;

}

//获取会员推荐人卡号
function GetMemRecommend(MemID) {
    doAjax("../",
           "GetMemRecommend",
           { "MemID": MemID },
           "json",
           function (json) {
               $("#txtMemRecommendCard").html(json);
           });
}

//响应发送短信按钮事件
function btnSendSMS() {
    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员,再进行下一步操作!</li>";
    }
    if (global_mem.MemMobile == "") {
        strErrorMsg += "<li>该会员没有手机号,不能发送短信!</li>";
    }
    if (strErrorMsg != "") {
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    location.href = "../ExtraService/SendMessage.aspx?PID=46&modiles=" + global_mem.MemName + ":" + global_mem.MemMobile + ";" + "";
}
//响应商品消费按钮事件
function btGoodsExpense() {
    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员,再进行下一步操作!</li>";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    window.location.href = "../StockManage/GoodsExpense.aspx?PID=67&MemCard=" + global_mem.MemCard;
}
//响应计时消费按钮事件
function btTimeExpense() {
    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员,再进行下一步操作!</li>";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    window.location.href = "../MemExpense/TimeExpense.aspx?PID=87&MemCard=" + global_mem.MemCard;
}

//响应计次消费按钮事件
function btConsumeMemCount() {
    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员,再进行下一步操作!</li>";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    window.location.href = "../MemExpense/ConsumeMemCount.aspx?PID=118&MemCard=" + global_mem.MemCard;
}





/*************************************************************************************************************
会员刷卡页面 会员消费记录
**************************************************************************************************************/

//会员消费记录
function MemInfoExpenseList() {
    var html = $("#MemInfoExpense").html();
    if (html.indexOf("LoadingBar") != -1) {
        MemInfoExpenseListPage();
    }
}
//分页获取会员消费记录
function MemInfoExpenseListPage() {
    doAjax("../",
           "MemInfoExpense",
           {
               "key": $("#txtFindMember").val(),
               "size": pageSize,
               "index": currentPage
           },
           "json",
           function (json) {
               MemInfoExpenseCreateTable(json.MemInfoExpense);
               MemInfoExpenseCreateTablePage(json.RecordCount);
           });
}
//创建列表项
function MemInfoExpenseCreateTable(obj) {
    var html = ''
               + '<tr class=\"th\">'
               + '<th>订单号</th><th>消费金额</th><th>实际金额</th><th>积分</th><th>消费时间</th><th>消费商家</th><th>操作员</th>'
               + '</td>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            html += "<tr class=\"td\" >"
            if (item.OrderType == 2 || item.OrderType == 7) {
                html += '<td style="width: 120px;"><a href="javascript:void(0);" onclick="javascript:ExpenseShowDetail(' + item.OrderID + ');" title="查看订单详细">' + item.OrderAccount + '</a></td>'
            }
            else if (item.OrderType == 0 || item.OrderType == 1) {
                html += '<td style="width: 120px;">' + item.OrderAccount + '</td>'
            }
            html += '<td align="left">' + item.OrderTotalMoney.ToFloat().toFixed(2) + '</td>'
                    + '<td align="left">' + item.OrderDiscountMoney.ToFloat().toFixed(2) + '</td>'
                    + '<td align="left">' + item.OrderPoint + '</td>'
                    + '<td align="left">' + item.OrderCreateTime + '</td>'
                    + '<td align="left">' + item.ShopName + '</td>'
                    + '<td align="left">' + item.UserName + '</td>'
                    + '</tr>'
                    + '<tr id="trOrderDetail' + item.OrderID + '" style="display:none;background-color:#ffffff"><td colspan="7" style="padding:20px;">正在读取数据，请稍候……</td></tr>';
        })
    }
    else {
        html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="9">未找到符合此条件的数据！</td></tr>';
    }
    $("#MemInfoExpense").html(html);
}
/*************************************************************************************************************
创建分页按钮
**************************************************************************************************************/
function MemInfoExpenseCreateTablePage(resCount) {
    var page = new CommonPager(
        "MemInfoExpenseListPage",
        pageSize,
        resCount,
        currentPage,
        function (p) {
            currentPage = parseInt(p);
            MemInfoExpenseListPage();
        }
    );
    page.ShowSimple();
}
//获取消费详情
function ExpenseShowDetail(orderID) {
    if ($("#trOrderDetail" + orderID).css("display") == "none") {
        $("#trOrderDetail" + orderID).css("display", "");
    }
    else {
        $("#trOrderDetail" + orderID).css("display", "none");
    }
    doAjax("../",
           "GetExpenseDetail",
           { "orderID": orderID },
           "json",
           function (json) {
               MemInfoExpenseDeatilCreateTable(orderID, json.MemInfoExpenseDeatil)
           }
    )
}
//消费详情
function MemInfoExpenseDeatilCreateTable(orderID, obj) {
    var html = '';
    if (obj.length > 0) {
        html += '<div style="padding-left: 55px;">'
                + '<table class="tableStyle" cellspacing="0" cellpadding="0" style="width: 80%;">'
                + '<tr class=\"th\">'
                + '<th>商品编码</th><th>商品名称</th><th>商品类型</th><th>商品单价</th><th>消费数量</th><th>积分数量</th><th>折后总价</th>'
                + '</tr>';
        $.each(obj, function (index, item) {

            html += "<tr class=\"td\" >"

                        + '<td>' + item.GoodsCode + '</td>'

            if (item.GoodsType == 0) {
                html += '<td>' + item.Name + '</td>'
                     + '<td>' + GetGoodsType(item.GoodsType) + '</td>'
            }
            else {
                html += '<td style="color:red">' + item.Name + '</td>'
                     + '<td style="color:red">' + GetGoodsType(item.GoodsType) + '</td>'
            }
            html += '<td align="left">' + item.OrderDetailPrice + '</td>'
                        + '<td align="left">' + item.OrderDetailNumber + '</td>'
                        + '<td align="left">' + item.OrderDetailPoint + '</td>'
                        + '<td align="left">' + item.OrderDetailDiscountPrice + '</td>'
                        + '</tr>';
        });
        html += '</table></div>';
    }
    $("#trOrderDetail" + orderID).html("<td colspan='7' style='padding:1px;'>" + html + "</td>");
}
function GetGoodsType(goodsType) {
    var strGoodsType = "";
    switch (goodsType) {
        case "0":
            strGoodsType = "普通商品";
            break;
        case "1":
            strGoodsType = "服务商品";
            break;
    }
    return strGoodsType;
}
/*************************************************************************************************************
会员刷卡页面 会员充值记录
**************************************************************************************************************/
//会员充值记录
function MemInfoRecharge() {
    var html = $("#MemInfoRecharge").html();
    if (html.indexOf("LoadingBar") != -1) {
        MemInfoRechargeListPage();
    }
}
//分页获取会员充值记录
function MemInfoRechargeListPage() {
    doAjax("../",
           "MemInfoRecharge",
           {
               "key": $("#txtFindMember").val()
           },
           "json",
           function (json) {
               MemInfoRechargeCreateTable(json.MemInfoRecharge);
           });
}
//创建列表项
function MemInfoRechargeCreateTable(obj) {
    var html = ''
               + '<tr class=\"th\">'
               + '<th>订单号</th><th>充值金额</th><th>备注</th><th>充值时间</th><th>充值商家</th><th>操作员</th>'
               + '</td>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {

            html += "<tr class=\"td\" >"
                    + '<td align="left">' + item.RechargeAccount + '</td>'
                    + '<td align="left">' + item.RechargeMoney.ToNumber().toFixed(2) + '</td>'
                    + '<td align="left">' + item.RechargeRemark + '</td>'
                    + '<td align="left">' + item.RechargeCreateTime + '</td>'
                    + '<td align="left">' + item.ShopName + '</td>'
                    + '<td align="left">' + item.UserName + '</td>'
                    + '</tr>'
        })
    }
    else {
        html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="8">未找到符合此条件的数据！</td><tr>';
    }
    $("#MemInfoRecharge").html(html);
}

/*************************************************************************************************************
会员刷卡页面 会员充次记录
**************************************************************************************************************/
//会员充次记录
function MemInfoRechargeCount() {
    var html = $("#MemInfoRechargeCount").html();
    if (html.indexOf("LoadingBar") != -1) {
        GetRechargeCountList();
    }
}
//分页获取会员充次记录
function GetRechargeCountList() {
    doAjax("../",
           "MemInfoRechargeCount",
           {
               "key": $("#txtFindMember").val(),
               "size": pageSize,
               "index": currentPage
           },
           "json",
           function (json) {
               MemInfoRechargeCountTable(json.MemInfoRechargeCount);
               MemInfoRechargeCountPage(json.RecordCount);
           });
}

//创建列表项
function MemInfoRechargeCountTable(obj) {
    var html = ''
         + '<tr class=\"th\">'
         + '<th>帐单号</th><th>会员卡号</th><th>会员姓名</th><th>应付金额</th><th>实付金额</th><th>所得积分</th><th>充次商家</th><th>充次时间</th><th>操作员</th>'
         + '</td>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {

            html += "<tr class=\"td\" >"
                  + '<td style="width: 120px;"><a href="javascript:void(0);" onclick="javascript:RechargeCountShowDetail(' + item.CountID + ');" title="查看订单详细">' + item.CountAccount + '</a></td>'
                  + '<td align="left">' + item.MemCard + '</td>'
                  + '<td align="left">' + item.MemName + '</td>'
                  + '<td align="right">' + item.CountTotalMoney.ToNumber().toFixed(2) + '</td>'
                  + '<td align="right">' + item.CountDiscountMoney.ToNumber().toFixed(2) + '</td>'
                  + '<td align="right">' + item.CountPoint + '</td>'
                  + '<td align="left">' + item.ShopName + '</td>'
                  + '<td align="center">' + item.CountCreateTime + '</td>'
                  + '<td align="left">' + item.UserName + '</td>'
                  + '</tr>'
                  + '<tr id="trRechargeCountDetail' + item.CountID + '" style="display:none;background-color:#ffffff"><td colspan="7" style="padding:20px;">正在读取数据，请稍候……</td></tr>';
        })
    }
    else {
        html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="9">未找到符合此条件的数据！</td></tr>';
    }
    $("#MemInfoRechargeCount").html(html);
}

//创建分页按钮
function MemInfoRechargeCountPage(resCount) {
    var page = new CommonPager(
          "MemInfoRechargeCountListPage",
          pageSize,
          resCount,
          currentPage,
          function (p) {
              currentPage = parseInt(p);
              GetRechargeCountList();
          });
    page.ShowSimple();
}

//获取消费详情
function RechargeCountShowDetail(countID) {
    if ($("#trRechargeCountDetail" + countID).css("display") == "none") {
        $("#trRechargeCountDetail" + countID).css("display", "");
    }
    else {
        $("#trRechargeCountDetail" + countID).css("display", "none");
    }
    doAjax("../",
             "GetRechargeCountDetail",
             { "CountID": countID },
             "json",
             function (json) {
                 MemInfoRechargeCountDeatilTable(countID, json.MemInfoRechargeCountDeatil)
             });
}

//消费详情
function MemInfoRechargeCountDeatilTable(countID, obj) {
    var html = '';
    if (obj.length > 0) {
        html += '<div style="padding-left: 55px;">'
          + '<table class="tableStyle" cellspacing="0" cellpadding="0" style="width: 40%;">'
          + '<tr class=\"th\">'
          + '<th>服务项目</th><th>充次次数</th><th>剩余次数</th><th>实付金额</th><th>所得积分</th>'
          + '</tr>';
        $.each(obj, function (index, item) {

            html += "<tr class=\"td\" >"
                  + '<td>' + item.Name + '</td>'
                  + '<td align="left">' + item.CountDetailTotalNumber + '</td>'
                  + '<td align="left">' + item.CountDetailNumber + '</td>'
                  + '<td align="left">' + item.CountDetailDiscountMoney.ToNumber().toFixed(2) + '</td>'
                  + '<td align="left">' + item.CountDetailPoint + '</td>'
                  + '</tr>';
        });
        html += '</table></div>';
    }
    $("#trRechargeCountDetail" + countID).html("<td colspan='9' style='padding:1px;'>" + html + "</td>");
}
/*************************************************************************************************************
会员刷卡页面 会员兑换礼品记录
**************************************************************************************************************/
//会员兑换礼品信息
function MemInfoExchangeGift() {
    var html = $("#MemInfoExchangeGift").html();
    if (html.indexOf("LoadingBar") != -1) {
        MemInfoExchangeGiftListPage();
    }
}
//分页获取会员兑换礼品记录
function MemInfoExchangeGiftListPage() {
    doAjax("../",
           "MemInfoExchangeGift",
           {
               "key": $("#txtFindMember").val()
           },
           "json",
           function (json) {
               MemInfoExchangeCreateTable(json.MemInfoExchangeGift);
           });
}
//创建列表项
function MemInfoExchangeCreateTable(obj) {
    var html = ''
               + '<tr class=\"th\">'
               + '<th>兑换单号</b><th>兑换数量</th><th>消费积分</th><th>兑换类型</th><th>申请时间</th><th>审核时间</th><th>兑换商家</th><th>审核员</th>'
               + '</tr>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {

            html += "<tr class=\"td\" >"
                    + '<td align="left">' + item.ExchangeAccount + '</td>'
                    + '<td align="left">' + item.ExchangeAllNumber + '</td>'
                    + '<td align="left">' + item.ExchangeAllPoint + '</td>'
                    + '<td align="left">' + ExchangeType(item.ExchangeType) + '</td>'
                    + '<td align="left">' + item.ApplicationTime + '</td>'
                    + '<td align="left">' + item.ExchangeTime + '</td>'
                    + '<td align="left">' + item.ShopName + '</td>'
                    + '<td align="left">' + item.UserName + '</td>'
                    + '</tr>'
        })
    }
    else {
        html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="8">未找到符合此条件的数据！</td></tr>';
    }
    $("#MemInfoExchangeGift").html(html);
}

function ExchangeType(type) {
    var exchangeType = "";
    if (type == 1) {
        exchangeType = "主系统兑换";
    }
    else {
        exchangeType = "自助平台兑换";
    }
    return exchangeType;
}

/*************************************************************************************************************
会员刷卡页面 会员优惠券记录
**************************************************************************************************************/
function MemInfoCoupon() {
    var html = $("#MemInfoCoupon").html();
    if (html.indexOf("LoadingBar") != -1) {
        MemInfoCouponListPage();
    }
}
//分页获取会员兑换礼品记录
function MemInfoCouponListPage() {
    doAjax("../",
           "MemInfoCoupon",
           {
               "key": $("#txtFindMember").val()
           },
           "json",
           function (json) {
               MemInfoCouponTable(json);
           });
}
//创建列表项
function MemInfoCouponTable(obj) {
    var html = ''
               + '<tr class=\"th\">'
               + '<th>优惠券号</th><th>发送状态</th><th>使用状态</th><th>发送时间</th><th>使用时间</th><th>使用优惠券帐单号</th>'
               + '</td>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {

            html += "<tr class=\"td\" >"
                 + '<td align="center">' + item.CouPon + '</td>';
            if (item.CouPonYF == "True") {
                html += '<td align="center">已发送</td>';
            }
            else {
                html += '<td align="center">未发送</td>';
            }
            if (item.CouPonSY == "True") {
                html += '<td align="center">已使用</td>';
            }
            else {
                html += '<td align="center">未使用</td>';
            }
            html += '<td align="center">' + item.ConPonSendTime + '</td>'
                 + '<td align="center">' + item.ConPonUseTime + '</td>'
                 + '<td align="center">' + item.CouPonOrderAccount + '</td>'
                 + '</tr>';
        })
    }
    else {
        html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="6">未找到符合此条件的数据！</td></tr>';
    }
    $("#MemInfoCoupon").html(html);
}

/*************************************************************************************************************
会员刷卡页面 会员来电记录
**************************************************************************************************************/
function MemInfoMobile() {
    var html = $("#MemInfoMobile").html();
    if (html.indexOf("LoadingBar") != -1) {
        MemInfoMobileListPage();
    }
}

//分页获取会员来电 记录
function MemInfoMobileListPage() {
    doAjax("../",
           "MemInfoMobile",
           {
               "key": $("#txtFindMember").val()
           },
           "json",
           function (json) {
               MemInfoMobileTable(json);
           });
}
//创建列表项
function MemInfoMobileTable(obj) {
    var html = '<tr class=\"th\">'
               + '<th>手机号码</th><th>来电状态</th><th>来电时间</th><th>来电时长</th><th>来电备注</th><th>接电操作员</th><th>接电商家</th>'
               + '</td>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {

            html += "<tr class=\"td\" >"
                 + '<td align="left">' + item.CallerMobile + '</td>';
            if (item.CallerState == "2") {
                html += '<td align="center">已接来电</td>';
            }
            else {
                html += '<td align="center">未接来电</td>';
            }
            html += '<td align="center">' + item.CallerCreateTime + '</td>'
                 + '<td align="center">' + item.CallerDuration + '</td>'
                 + '<td align="left">' + item.CallerRemark + '</td>'
                 + '<td align="left">' + item.UserName + '</td>'
                 + '<td align="left">' + item.ShopName + '</td>'
                 + '</tr>';
        })
    }
    else {
        html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="7">未找到符合此条件的数据！</td></tr>';
    }
    $("#MemInfoMobile").html(html);
}