/*************************************************************************************************************
商品进货页面JS处理
**************************************************************************************************************/
var pageSize = 10;
var currentPage = 1;
var key = "", keyBarCode = "";       //搜索的值
var GoodsList = new Array();
var totalNumber = 0, totalMoney = 0

$(document).ready(function () {
    $("#txtCode").focus().select();
    GoodsProductList();
    //消费时间绑定日期控件
    $('#txtCreteTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
//        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm', maxDate: '%y-%M-%d %H:%m:%s', isShowClear: false, readOnly: true });
    });
    //搜索按钮响应的事件
    $("#btnGoodsSearch").bind("click", btnGoodsSearch);
    //下拉框改变事件
    $("#sltShop").bind("change", changeShop);
    //马上进货按钮响应事件
    $("#btnGoodsSave").bind("click", GoodsIn);

    $("#txtCode").keyup(function (e) {
        var keyAscii = window.event ? e.keyCode : e.which;
        if (keyAscii == 13 && $.trim($("#txtCode").val()) != "") {
            keyBarCode = $("#txtCode").val();
            SelectGoodsByBarCode();
        }
    });
})



function SelectGoodsByBarCode() {
    doAjax(
        "../",
        "GetGoodsByCodeDelServe",
        { "barCode": keyBarCode },
        "json",
        function (json) {
            if (json != null) {
                SelectGoodsOK(json);
            }
            $("#txtCode").select();
            $("#RechargeCount_BarCode").select();
        }
     );
}


/****************************************************************************************************
*输入商品名称或简码后  回车响应事件
*****************************************************************************************************/
function getKey() {
    if (event.keyCode == 13) {
        key = $("#txtGoodsCode").val()
        GoodsProductList();
    }
}

/****************************************************************************************************
*搜索按钮响应事件
*****************************************************************************************************/
function btnGoodsSearch() {
    key = $("#txtGoodsCode").val()
    GoodsProductList();
}
/****************************************************************************************************
*下拉框改变事件
*****************************************************************************************************/
function changeShop() {
    var newGoodsList = new Array();
    GoodsList = newGoodsList;
    $("#txtCode").val("");
    $("#Expense_Text_BarCode").val("");
    GoodsProductList();
    BindGoodsList();

}

/****************************************************************************************************
*获取商品
*****************************************************************************************************/
function GoodsProductList() {
    doAjax(
           "../",
           "GetGoodsList",
           {
               "size": pageSize,
               "index": currentPage,
               "key": key,
               "shopID": $("#sltShop").val(),
               "bolGoodsExpense": false,
               "ClassID": "",
               "IsCheckStock": false//是否检查库存；
           },
           "json",
           function (json) {
               CreateGoodsProductTable(json.List);
               CreateGoodsProductPager(json.RecordCount);
           });
}

/****************************************************************************************************
*创建表格
*****************************************************************************************************/
function CreateGoodsProductTable(obj) {
    var html = ''
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            html += "<tr class=\"td\" onclick=\"javascript:SelectGoods(" + item.GoodsID + ");\">"
                 + '<td style="text-align: left">' + item.Name + '</td>'
                 + '<td>' + item.GoodsCode + '</td>'
                 + '<td style="text-align: right">' + parseFloat(item.GoodsBidPrice).toFixed(2) + '</td>'
               
                 + '</tr>';
        });
    }
    else {
        html += '<tr><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="5">未找到符合此条件的数据！请重试！</td></tr>';
    }

    $("#tbGoodsProduct").html(html);
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
            GoodsProductList();
        }
    );
    page.ShowSimple();
    //    page.Show();
}

// 选择商品,加入到进货清单
function SelectGoods(goodsID) {
    doAjax(
          "../",
          "GetGoodsNumber",
          {
              "goodsID": goodsID,
              "InshopID": $("#sltShop").val()
          },
          "json",
          function (json) {
              SelectGoodsOK(json);
          });
}
// 选择好商品，加入到已选商品列表
function SelectGoodsOK(json) {
    var index = -1;
    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].GoodsID == json[0].GoodsID) {
            index = i;
        }
    }
    if (index == -1) {
        json[0]["Number"] = 1;
        json[0]["InMoney"] = json[0].GoodsBidPrice;
        json[0]["GoodsBuyDiscount"] = json[0].GoodsBuyDiscount;
        json[0]["TotalMoney"] = accMul(json[0].GoodsBidPrice, json[0]["Number"]);
        GoodsList.push(json[0])
    }
    else {
        var currentNum = parseInt(GoodsList[index].Number) + 1;
        //var currentMoney = 0;
        var currentTotalMoney = 0;
        //currentMoney = Math.round(accMul(GoodsList[index].GoodsBidPrice, currentNum), 2);
        currentTotalMoney = parseFloat(accMul(GoodsList[index].GoodsBidPrice, currentNum)).toFixed(2);
        GoodsList[index].Number = currentNum;
        //GoodsList[index].InMoney = currentMoney;
        GoodsList[index].TotalMoney = currentTotalMoney;
    }
    BindGoodsList();
}

/****************************************************************************************************
*将选择好的产品绑定到右侧列表中
*****************************************************************************************************/
function BindGoodsList() {
  
    $("#tbOrderTable").html("");
    for (var i = 0; i < GoodsList.length; i++) {
        var discountPrice = parseFloat(GoodsList[i].GoodsBuyDiscount) * parseFloat(GoodsList[i].InMoney);
        var html = '<tr class="td" >'
                 + '<td style="text-align: left">' + GoodsList[i].Name + '</td>'
                    + '<td style="text-align: left">' +  parseFloat(GoodsList[i].InMoney).toFixed(2)  + '</td>'
                 + '<td><input id="txtBidPrice' + GoodsList[i].GoodsID + '"  type="text" disabled="disabled"   class="border_radius common_ServiceButton border_radius2" style="text-align: right" value="' + parseFloat(discountPrice).toFixed(2) + '" onkeyup="javascript:GoodsUpdate(' + GoodsList[i].GoodsID + ');" onclick=\"javascript:this.select();\"/></td>'
             
                 + '<td><input id="txtNumber' + GoodsList[i].GoodsID + '" type="text" class="border_radius common_ServiceButton border_radius2" style="text-align: right" value="' + GoodsList[i].Number + '" onkeyup="javascript:GoodsUpdate(' + GoodsList[i].GoodsID + ');" onclick=\"javascript:this.select();\"/></td>'
                 + '<td style="text-align: right"><span id="spPrice' + GoodsList[i].GoodsID + '" class="integral" >' + parseFloat(GoodsList[i].TotalMoney).toFixed(2) + '</span></td>'
        html += '<td class=\"listtd\" style=\"width: 30px;\"><a href="javascript:void(0);" onclick="javascript:GoodsDeleteItem(' + GoodsList[i].GoodsID + ');"><img src=\"../images/Gift/del.png\" alt=\"删除此项\" title=\"删除此项\" /></a></td>'
        html += '</tr>';
        $("#tbOrderTable").append(html);
    }
    // html = '<tr id="tdTotal"><td colspan="5" style="height:30px; line-height:30px; background-color:#ddeeff;padding:0px 5px 0px 5px;"></td></tr>';
    //    $("#tbOrderTable").append(html);
    GoodsBindTotal();
}
/****************************************************************************************************
*绑定进货合计
*****************************************************************************************************/
function GoodsBindTotal() {
    totalMoney = totalNumber = 0;
    for (var i = 0; i < GoodsList.length; i++) {
        totalNumber = accAdd(totalNumber, GoodsList[i].Number);
        totalMoney = accAdd(totalMoney, parseFloat(accMul(GoodsList[i].InMoney, GoodsList[i].Number)).toFixed(2));
    }
    if (GoodsList.length != 0) {
        var strTotalhtml = "";
        $("#lblTotalNumber").html(totalNumber);
        $("#lblTotalMoney").html(totalMoney);
        //        strTotalhtml += '<td><span style="width:70px;color:red;" >进货合计</span></td><td colspan="2">商品总数量:<span style="width:70px;color:red;font-weight:bold;font-size:20px;" >' + totalNumber + '</span></td><td colspan="2">进货总额:￥<span style="width:70px;color:red;font-weight:bold;font-size:20px;" >' + totalMoney + '</span></td>';
        //        $("#tdTotal").html(strTotalhtml);
    }
    else {
        $("#lblTotalNumber").html("0");
        $("#lblTotalMoney").html("0");
        $("#tbOrderTable").html('<tr><td colspan="5" style="height:30px; line-height:30px;padding:0px 5px 0px 5px;">请点击左侧商品列表，选择需要进货的商品！</td></tr>');
    }
}
/****************************************************************************************************
*更改进货价格和数量 重新计算合计
*****************************************************************************************************/
function GoodsUpdate(goodsID) {
    var dclPrice = $("#txtBidPrice" + goodsID).val();
    var intNumber = sys_num_checked.product_num($.trim($("#txtNumber" + goodsID).val()));   
    if (!sys_num_checked.product_num_check(intNumber)) {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: '进货数量输入有误！',
            lock: true
        });
        intNumber = 1;
        $("#txtNumber" + goodsID).val(1);
        $("#spPrice" + goodsID).html(accMul(dclPrice, 1));
    }
    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].GoodsID == goodsID) {
            GoodsList[i].InMoney = dclPrice;
            GoodsList[i].Number = intNumber;
            break;
        }
    }
    GoodsList[i].TotalMoney = parseFloat(accMul(GoodsList[i].InMoney, intNumber)).toFixed(2);
    $("#spPrice" + goodsID).html(GoodsList[i].TotalMoney);
    GoodsBindTotal();
}
/****************************************************************************************************
*删除商品 重新计算合计
*****************************************************************************************************/
function GoodsDeleteItem(goodsID) {
    var newGoodsList = new Array();
    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].GoodsID != goodsID) {
            newGoodsList.push(GoodsList[i]);
        }
    }
    GoodsList = newGoodsList;
    BindGoodsList();
}
/****************************************************************************************************
*马上进货按钮响应事件
*****************************************************************************************************/
function GoodsIn() {
    if (GoodsList.length == 0) {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: '请在左侧列表中选择要进货的商品！',
            lock: true
        });
        return false;
    }


    //获取打印的份数
    var PointNum = $("#PointNum").val();

    var strAccount = $("#spGoodsAccounte").html();
    doAjax(
            "../",
            "GoodsBuyApply",
            {
                "goodsAccount": strAccount,
                "data": GoodsList,
                "count": GoodsList.length,
                "shopID": $("#sltShop").val(),
                "remark": $("#txtExRemark").val(),
                "time": $("#txtCreteTime").val(),
                "print": $("#chkPrint").attr("checked"),
                "totalMoney": totalMoney,
                "totalNumber": totalNumber
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
                      default:
                          art.dialog
                                ({
                                    title: '系统提示',
                                    time: 0.5,
                                    content: '商品进货成功！',
                                    close: function () {
                                        //PrintGoodsIn($("#lblUSer").html(), $("#sltShop").find("option:selected").text(), GoodsList, $("#chkPrint").attr("checked"), PointNum);
                                     },
                                    lock: true
                                });
                          break;
                  }
              });
}