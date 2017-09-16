$(document).ready(function () {
    GetGiftList();
    //控制查找会员的样式
    $("#findTable").removeAttr("style");
    $("#findTable").css("width", "100%");
    $("#tbGiftExchange").css("vertical-align", "top")
    $("#tbTop").css("width", "100%");
    $("#tbBody").css("width", "100%");

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

    //搜素按钮响应事件
    $("#btnGiftSearch").bind("click", btnGiftSearch);

    //兑换礼品按钮响应事件
    $("#btnExchangeSave").bind("click", btnExchangeSave);

    //输入礼品名称或简码的回车事件
    $("#txtGiftCode").keyup(function (e) {
        var keyAscii = window.event ? e.keyCode : e.which;
        if (keyAscii == 13) btnGiftSearch();
        return false;
    })
});
var expenseTab;
var pageSize = 10;
var currentPage = 1;
var key = "";
var sumPoint = 0;
var sumNumber = 0;
var GiftList = new Array();

//搜索按钮的响应事件
function btnGiftSearch() {
    key = $("#txtGiftCode").val();
    GetGiftList();
}

//得到左侧的礼品列表
function GetGiftList() {
    doAjax(
           "../",
           "GetGiftList",
           {
               "size": pageSize,
               "index": currentPage,
               "key": key
           },
           "json",
           function (json) {
               CreateGiftTable(json.List);
               CreateGoodsProductPager(json.RecordCount);
           });
}

//创建礼品列表
function CreateGiftTable(obj) {
    var html = ''
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            if (item.GiftStockNumber > 0) {
                html += "<tr class=\"td\"  onclick=\"javascript:SelectGift(" + item.GiftID + ");\">"
                        + '<td style="text-align: left">' + item.GiftName + '</td>'
                        + '<td style="text-align: left">' + item.GiftCode + '</td>'
                        + '<td style="text-align: right">' + item.GiftExchangePoint + '</td>'
                        + '<td style="text-align: right">' + item.GiftStockNumber + '</td>'
                        + '</tr>';
            }
        });
    }
    else {
        html += '<td style="height:25px; line-height:25px;padding-left:20px; background-color:#fff;" colspan="4">未找到符合此条件的数据！</td>';
    }
    $("#GiftList").html(html);
}

/****************************************************************************************************
*分页
*****************************************************************************************************/
function CreateGoodsProductPager(resCount) {
    var page = new CommonPager(
        "GoodsProductPage",
        pageSize,
        resCount,
        currentPage,
        function (p) {
            currentPage = parseInt(p);
            GetGiftList();
        }
    );
    page.ShowSimple();
}

// 选择需要兑换的礼品
function SelectGift(GiftID) {
    doAjax(
           "../",
           "GetGiftModel",
           {
               "GiftID": GiftID
           },
           "json",
            function (json) {
                var has = false;
                for (var i = 0; i < GiftList.length; i++) {
                    if (GiftList[i].GiftID == json.GiftID) {
                        var currentNum = parseInt(GiftList[i].ExcNumber) + 1;
                        if (currentNum > parseInt(json.GiftStockNumber)) {
                            art.dialog({
                                icon: 'error', //图标
                                content: '兑换数量超过礼品库存数量,请重新输入',
                                lock: true
                            });
                            return false;
                        }
                        if (parseInt(GiftList[i].GiftExchangePoint) > parseInt(global_mem.MemPoint)) {
                            art.dialog({
                                title: '系统提示',
                                icon: 'error', //图标
                                content: '兑换礼品的已经超过会员所拥有积分,请重新选择礼品',
                                lock: true
                            });
                            return false;
                        }
                        GiftList[i].ExcNumber = currentNum;
                        has = true;
                    }
                }
                if (parseInt(json.GiftExchangePoint) > parseInt(global_mem.MemPoint)) {
                    art.dialog({
                        title: '系统提示',
                        icon: 'error', //图标
                        content: '兑换礼品的已经超过会员所拥有积分,请重新选择礼品',
                        lock: true
                    });
                    return false;
                }
                else {
                    if (!has) {
                        json["ExcNumber"] = 1;
                        GiftList.push(json);
                    }
                    SelectGiftBindList();
                }
            });
}

// 绑定已选中的礼品列表
function SelectGiftBindList() {
    $("#GiftExchangeTable").html('');
    for (var i = 0; i < GiftList.length; i++) {
        var html = '<tr class=\"td\">'
         + '<td style="border-top-style:none;text-align: left">' + GiftList[i].GiftName + '</td>'
         + '<td style="border-top-style:none;text-align: left">' + GiftList[i].GiftCode + '</td>'
         + '<td style="border-top-style:none;text-align: right">' + GiftList[i].GiftExchangePoint + '</td>'
         + '<td style="border-top-style:none;"><input id="txtGiftNumber_' + GiftList[i].GiftID + '" type="text" class="border_radius common_ServiceButton border_radius2" style="text-align: right" value="' + GiftList[i].ExcNumber + '" onkeyup="javascript:UpdateNumber(' + GiftList[i].GiftID + ');" onclick=\"javascript:this.select();\"/></td>'
         + '<td style="border-top-style:none;text-align: right"><span id="spGiftPoint_' + GiftList[i].GiftID + '" class="integral">' + (GiftList[i].GiftExchangePoint * GiftList[i].ExcNumber) + '</span></td>'
         + '<td class=\"listtd\" style=\"width: 30px;border-top-style:none\"><a href="javascript:void(0);" onclick="javascript:DeleteGiftItem(' + GiftList[i].GiftID + ');"> <img src=\"../images/Gift/del.png\" alt=\"删除此项\" title=\"删除此项\" /></a></td>'
         + '<tr>'
        $("#GiftExchangeTable").append(html);
    }
    //    html = '<tr><td colspan="6" style="height:30px; line-height:30px;text-align: left;" class="th" id="GiftTotal"></td></tr>';
    BindGiftListTotal();
}

// 绑定以选中的礼品列表合计
function BindGiftListTotal() {
    sumNumber = sumPoint = 0;
    for (var i = 0; i < GiftList.length; i++) {
        sumNumber += GiftList[i].ExcNumber;
        sumPoint += GiftList[i].GiftExchangePoint * GiftList[i].ExcNumber;
    }
    if (parseInt(sumPoint) > parseInt(global_mem.MemPoint)) {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: '兑换礼品的已经超过会员所拥有积分,请重新选择礼品',
            lock: true
        });
        return false;
    }
    $("#lblTotalNumber").html(sumNumber);
    $("#lblTotalPoint").html(sumPoint);
    //    $("#GiftTotal").html('&nbsp;&nbsp;兑换总数量：<b>' + sumNumber + '</b>&nbsp;&nbsp;积分总量：<font style="font-weight:bold;color:red;">' + sumPoint + '</font>');
}

// 更新绑定的选中礼品数量
function UpdateNumber(GiftID) {
    var number = $("#txtGiftNumber_" + GiftID).val();
    if (number == "")
        number = "0";
    for (var i = 0; i < GiftList.length; i++) {
        if (GiftList[i].GiftID == GiftID) {
            if (parseInt(number) > GiftList[i].GiftStockNumber) {
                art.dialog({
                    title: '系统提示',
                    icon: 'error', //图标
                    content: '兑换数量超过礼品库存数量,请重新输入',
                    lock: true
                });
                $("#txtGiftNumber_" + GiftID).val(GiftList[i].ExcNumber);
                $("#spGiftPoint_" + GiftID).html(GiftList[i].GiftExchangePoint * GiftList[i].ExcNumber);
                //                GiftList[i].ExcNumber = 1;
                return false;
            }
            if (!number.IsNumber()) {
                art.dialog({
                    title: '系统提示',
                    icon: 'error', //图标
                    content: '兑换礼品数量只能为数字且大于0,请重新输入',
                    lock: true
                });
                $("#txtGiftNumber_" + GiftID).val("1");
                GiftList[i].ExcNumber = 1;
                number = "1";
                BindGiftListTotal();
                $("#spGiftPoint_" + GiftID).html(GiftList[i].GiftExchangePoint * parseInt(number));
                return false;
            }
            $("#spGiftPoint_" + GiftID).html(GiftList[i].GiftExchangePoint * parseInt(number));
            GiftList[i].ExcNumber = parseInt(number);
            break;
        }
    }
    BindGiftListTotal();
}

//删除礼品
function DeleteGiftItem(GiftID) {
    var newGiftList = new Array();
    for (var i = 0; i < GiftList.length; i++) {
        if (GiftList[i].GiftID != GiftID) {
            newGiftList.push(GiftList[i]);
        }
    }
    GiftList = newGiftList;
    SelectGiftBindList();
}

//提交
function btnExchangeSave() {
    var strErrorMsg = "";
    //获取打印份数
    var PointNum = $("#PointNum").val(); 

    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员！</li>";
    }
    if (sumPoint > global_mem.MemPoint) {
        strErrorMsg += "<li>兑换礼品的已经超过会员所拥有积分,请重新选择礼品！</li>";
    }
    if (GiftList.length <= 0) {
        strErrorMsg += "<li>请先选择礼品再进行兑换！</li>";
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

    var test = art.dialog({
        title: '系统提示',
        content: "将要兑换 [" + sumNumber + "] 件礼品，共 [" + sumPoint + "] 积分。\n确定操作吗?",
        lock: true,
        ok: function () {
            //this.close();
            //            this.lock();
            doAjax("../",
                "GiftExchange",
                {
                    "memID": global_mem.MemID,
                    "giftList": GiftList,
                    "giftcount": GiftList.length,
                    "sumPoint": sumPoint,
                    "sumNumber": sumNumber,
                    "sendSMS": $("#chkSMS").attr("checked"),
                    "Account": $("#lblExchangePrefix").html()
                },
                "json",
                function (json) {

                    setTimeout('test.close()', 4000);
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
                                    content: ("会员状态异常，请联系管理员！"),
                                    lock: true
                                });

                            break;
                        case -2:
                            art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("会员积分兑换礼品成功，短信余额不足，不能发送短信，请充值短信！"),
                                  close: function () { Print_GiftExchange_Point($("#lblExchangeUSer").html(), global_mem, GiftList, sumPoint, sumNumber, $("#chkPrint").attr("checked"), PointNum); },
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
                        default:
                            art.dialog
                            ({
                                title: '系统提示',
                                time: 0.5,
                                content: '会员积分兑换礼品成功,' + json.strUpdateMemLevel,
                                close: function () { Print_GiftExchange_Point($("#lblExchangeUSer").html(), global_mem, GiftList, sumPoint, sumNumber, $("#chkPrint").attr("checked"), PointNum); },
                                lock: true
                            });
                            break;
                    }
                });
        },
        cancelVal: '关闭',
        cancel: true //为true等价于function(){}
    });
}

//function GetGiftList() {
//    //获取要兑换的礼品
//    strErrorMsg = "";
//    var inputlist = $("#gvwExchangeGiftList input");
//    for (var i = 0; i < inputlist.length; i++) {
//        var dnumber = parseInt($(inputlist[i]).val());
//        var number = parseInt($(inputlist[i]).attr("number"));
//        if (dnumber > 0) {
//            if (number >= dnumber) {
//                GiftList.push({ "ID": parseInt($(inputlist[i]).attr("lid")), "Point": parseInt($(inputlist[i]).attr("ponit")), "ExchangeNumber": parseInt($(inputlist[i]).val()), "GiftName": $(inputlist[i]).attr("gname") });
//                 
//                sumPoint += parseInt($(inputlist[i]).attr("ponit")) * parseInt($(inputlist[i]).val());
//                sumNumber += parseInt($(inputlist[i]).val());                 
//            }
//            else {
//                strErrorMsg += "<li>礼品" + $(inputlist[i]).attr("gname") + "兑换数量超过库存，请重新选择；</li>";
//            }
//        }
//    }
//    if (sumPoint > global_mem.MemPoint) {
//        strErrorMsg += "<li>当前兑换礼品所需积分超过会员所拥有的积分，请重新选择！</li>";
//    }
//    if (sumNumber < 1) {
//        strErrorMsg += "<li>请先选择礼品再进行兑换</li>";
//    }

//}

/****************************************************************************************************
*在选择好会员时可以执行回调函数
*****************************************************************************************************/
function FindMember_CallBack() {
    var strErrorMsg;
    if (global_mem.MemState != 0) {
        strErrorMsg = "当前会员卡处于锁定或者挂失状态，暂不允许进行消费。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    if (global_mem.MemIsPast == "True") {
        strErrorMsg = "当前会员卡已过期，暂不允许进行消费。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    return true;
}