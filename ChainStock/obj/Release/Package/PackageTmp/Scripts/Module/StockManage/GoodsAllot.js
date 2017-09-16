/*************************************************************************************************************
商品调拔页面JS处理
**************************************************************************************************************/
var pageSize = 10;
var currentPage = 1;
var key = "";       //搜索的值
var GoodsList = new Array();
var totalNumber = 0, totalMoney = 0
var isEmptyBillsExpense = false;
$(document).ready(function () {
    $("#txtCode").focus().select();
    GoodsProductList();

    //消费时间绑定日期控件
    $('#txtCreateTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm', maxDate: '%y-%M-%d %H:%m:%s', isShowClear: false, readOnly: true });
    });

    //搜索按钮响应的事件
    $("#btnGoodsSearch").bind("click", btnGoodsSearch);

    //下拉框改变事件
    $("#sltOutShopID").bind("change", changeShop);

    //马上调拔按钮响应事件
    $("#btnGoodsSave").bind("click", GoodsIn);

    $("#tbGoodsProduct").css("disabled", "disabled");

    $("#txtCode").keyup(function (e) {
        var keyAscii = window.event ? e.keyCode : e.which;
        if (keyAscii == 13 && $.trim($("#txtCode").val()) != "") {
            keyBarCode = $("#txtCode").val();
            SelectGoodsByBarCode();
        }
    });
    //为编辑 时执行
    if ($("#HidExit").val() == "2") {
        GetExpenseDetail();
        isEmptyBillsExpense = true;
    }

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
               "shopID": $("#sltOutShopID").val(),
               "bolGoodsExpense": false,
               "ClassID": "",
               "IsCheckStock": true//是否检查库存；库存要大于0
           },
           "json",
           function (json) {
               if (json != null) {
                   CreateGoodsProductTable(json.List);
                   CreateGoodsProductPager(json.RecordCount);
               }
           });
}

/****************************************************************************************************
*尝试读取 被编辑的 调拨信息
*****************************************************************************************************/
function GetExpenseDetail() {
    var AllotID = $("#HidAllotID").val();
    doAjax(
        "../",
        "EixtGetAllotDetail",
        { "AllotID": AllotID },
        "json",
        function (json) {
            if (json != null) {
                $.each(json, function (index, item) {
                    var obj = {}
                    obj["Name"] = item.Name;
                    obj["ExpNum"] = item.AllotDetailNumber;
                    obj["GoodsID"] = item.GoodsID;
                    GoodsList.push(obj);
                });
                BindGoodsList();
            }
            else {
                art.dialog
                    ({
                        title: '系统提示',
                        time: 3,
                        content: ("已发货订单，不允许修改！"),
                        close: function () { window.location.href = "../StockManage/GoodsAllotState.aspx?PID=139"; },
                        lock: true
                    });
            }
        }
     );
}



/****************************************************************************************************
*创建表格
*****************************************************************************************************/
function CreateGoodsProductTable(obj) {
    var html = ''
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            if (item.Number > 0) {
                html += "<tr class=\"td\"  onclick=\"javascript:SelectGoods(" + item.GoodsID + ");\">"
                 + '<td style="text-align: left">' + item.Name + '</td>'
                 + '<td>' + item.GoodsCode + '</td>'
                 + '<td style="text-align: right">' + Math.round(item.GoodsBidPrice, 2) + '</td>'
                 + '<td style="text-align: right">' + item.Number + '</td>'
                 + '</tr>';
            }
        });
    }
    else {
        html += '<td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="5">未找到符合此条件的数据！请重试！</td>';
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
}

/****************************************************************************************************
*选择商品,加入到调入商家
*****************************************************************************************************/
function SelectGoods(goodsID) {
    if ($("#sltInShopID").val() == $("#sltOutShopID").val()) {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: "调出商家和调入商家不可为同一个商家！",
            lock: true
        });
        return false;
    }
    else if ($("#sltInShopID").val() == "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: "调入商家不允许为空！",
            lock: true
        });
        return false;
    }
    doAjax(
          "../",
          "GetGoodsNumber",
          {
              "goodsID": goodsID,
              "OutshopID": $("#sltOutShopID").val(),
              "InshopID": $("#sltInShopID").val()
          },
          "json",
          function (json) {
              if (json != null) {
                  if (Number(json[0]["Number"]) <= 0) {
                      art.dialog({
                          title: '系统提示',
                          icon: 'error', //图标
                          content: "所选商品库存为空！",
                          lock: true
                      });
                      return false;
                  }
                  else {
                      SelectGoodsOK(json);
                  }
              }
              else {

                  art.dialog({
                      title: '系统提示',
                      icon: 'error', //图标
                      time: '2',
                      content: "调入商家没有该商品！",
                      lock: true
                  });
                  return false;

              }
          });
}

/****************************************************************************************************
*选择好商品,加入到调入商家
*****************************************************************************************************/
function SelectGoodsOK(json) {
    var index = -1;
    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].GoodsID == json[0].GoodsID) {
            index = i;
        }
    }
    if (index == -1) {
        if (json[0].Number <= 0) {
            art.dialog({
                title: '系统提示',
                icon: 'error', //图标
                content: "<li>对不起！你选择的商品已经没有库存！</li>",
                lock: true
            });
            return false;
        }
        else {
            if (parseFloat(json[0].Number) < (1.00)) {
                json[0]["ExpNum"] = parseFloat(_sys_check.defval);
            } else {
                json[0]["ExpNum"] = 1;
            }
            GoodsList.push(json[0]);
        }
    }
    else {
        var currentNum = parseFloat(GoodsList[index].ExpNum) + 1;
        if (currentNum > parseFloat(json[0].Number)) {
            art.dialog({
                title: '系统提示',
                icon: 'error', //图标
                content: "<li>对不起！你选择的商品超过该商品库存的最大值！</li>",
                lock: true
            });
            return false;
        }
        else {
            GoodsList[index].ExpNum = currentNum;
        }
    }
    BindGoodsList();

}

/****************************************************************************************************
*将选择好的产品绑定到右侧列表中
*****************************************************************************************************/
function BindGoodsList() {
    $("#tbOrderTable").html("");
    for (var i = 0; i < GoodsList.length; i++) {
        var bg = i % 2 == 0 ? "#eee" : "#fff";
        var html = '<tr class="td" >'
                 + '<td style="text-align: left">' + GoodsList[i].Name + '</td>'
                 + '<td><input id="txtNumber' + GoodsList[i].GoodsID + '" type="text" class="border_radius common_ServiceButton border_radius2" style="text-align: right" value="' + GoodsList[i].ExpNum + '" onkeyup="javascript:GoodsUpdate(' + GoodsList[i].GoodsID + ');" onclick=\"javascript:this.select();\"/></td>'
                 + '  <td class=\"listtd\" style=\"width: 30px;\"><a href="javascript:void(0);" onclick="javascript:GoodsDeleteItem(' + GoodsList[i].GoodsID + ');"><img src=\"../images/Gift/del.png\" alt=\"删除此项\" title=\"删除此项\" /></a></td>'
                 + '</tr>';
        $("#tbOrderTable").append(html);
        GoodsBindTotal();
    }
}

/****************************************************************************************************
*绑定调拔合计
*****************************************************************************************************/
function GoodsBindTotal() {
    totalMoney = totalNumber = 0;
    for (var i = 0; i < GoodsList.length; i++) {
        totalNumber = accAdd(totalNumber, GoodsList[i].ExpNum);
    }
    if (GoodsList.length != 0) {
        //        var strTotalhtml = "";
        //        strTotalhtml += '<td style="text-align:right;" colspan="2">总数量:</td><td style="text-align:left;color:red;font-weight:bold;font-size:20px;" colspan="2"><span id="spTotalNumber">' + totalNumber + '</span></td>';
        //        $("#tdTotal").html(strTotalhtml);
        $("#lblTotalNumber").html(totalNumber);
    }
    else {
        $("#lblTotalNumber").html("0");
        $("#tbOrderTable").html('<tr><td colspan="5" style="height:30px; line-height:30px;padding:0px 5px 0px 5px;">请点击左侧商品列表，选择需要入库的商品！</td></tr>');
    }
}

/****************************************************************************************************
*更改调拔数量 重新计算合计
*****************************************************************************************************/
function GoodsUpdate(goodsID) {
    var intNumber = sys_num_checked.product_num($.trim($("#txtNumber" + goodsID).val()));
    if (!sys_num_checked.product_num_check(intNumber)) {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: "<li>对不起！商品数量输入错误！</li>",
            lock: true,
            close: function () {
                for (var i = 0; i < GoodsList.length; i++) {
                    if (GoodsList[i].GoodsID == goodsID) {
                        $("#txtNumber" + goodsID).val(GoodsList[i].ExpNum)
                    } 
                }
            }
        });
        return false;
    }
    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].GoodsID == goodsID) {
            if (intNumber > parseFloat(GoodsList[i].Number)) {
                art.dialog({
                    title: '系统提示',
                    icon: 'error', //图标
                    content: "<li>对不起！你选择的商品超过该商品库存的最大值！</li>",
                    lock: true,
                    close: function () {
                        for (var i = 0; i < GoodsList.length; i++) {
                            if (GoodsList[i].GoodsID == goodsID) {
                                $("#txtNumber" + goodsID).val(GoodsList[i].ExpNum)
                            }
                        }
                    }
                });
                return false;
            }
            else {
                GoodsList[i].ExpNum = intNumber;
                break;
            }
        }
    }
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
*马上调拨按钮响应事件
*****************************************************************************************************/
function GoodsIn() {
    var strErrorMsg = "";
    if ($("#sltInShopID").val() == "") {
        strErrorMsg += "<li>请选择调入商家;</li>";
    }

    if (GoodsList.length == 0) {
        strErrorMsg += "<li>请在左侧列表中选择要调拨的商品！</li>";
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

    doAjax("../",
            "GoodsAllot",
            { "goodsAccount": $("#spGoodsAccounte").html(),
                "data": GoodsList,
                "count": GoodsList.length,
                "totalNumber": $("#lblTotalNumber").html(),
                "outShopID": $("#sltOutShopID").val(),
                "inShopID": $("#sltInShopID").val(),
                "remark": $.trim($("#txtExRemark").val()),
                "time": $("#txtCreateTime").val(),
                "isEmptyBillsExpense": isEmptyBillsExpense,
                "HidAllotID": $("#HidAllotID").val()
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
                                     content: ("调拨单生成成功！"),
                                     close: function () { window.location.href = "../StockManage/GoodsAllotState.aspx?PID=139"; },
                                     lock: true
                                 });
                        break;
                    case -3:
                        art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("调拨单修改成功！"),
                                     close: function () { window.location.href = "../StockManage/GoodsAllotState.aspx?PID=139"; },
                                     lock: true
                                 });
                        break;
                    default:
                        ////                        art.dialog
                        ////                                ({
                        ////                                    title: '系统提示',
                        ////                                    time: 0.5,
                        ////                                    content: '商品调拨成功！',
                        ////                                    close: function () { window.location.href = "../StockManage/GoodsAllotState.aspx?PID=139"; },
                        ////                                    lock: true
                        ////                                });
                        ////                        break;
                }
            });
}
