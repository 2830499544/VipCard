var pageSize = 10;
var currentPage = 1;
var intShopID = "";
var intClassID = "";
$(document).ready(function () {
    $("#txtQuery").focus();

    //绑定部门列表到树形菜单
    mkTree_convertTrees();
    expandTree('treeStock');
    intShopID = $("#shopID").val();
    GetStockList();
    //按“回车键”响应查询
    $("#txtQuery").keypress(function (e) {
        var keyAscii = window.event ? e.keyCode : e.which;
        if (keyAscii == 13) {
            GetStockList();
        }
    });

    //"查询"按钮响应函数
    $("#btnStockQuery").bind("click", GetStockList);
})

/*****************************************************************************************************
*绑定商品列表到树形菜单
*****************************************************************************************************/
function StockListTree(intID, bolIsShop) {
    $("#txtQuery").val("");
    if (intID != undefined) {
        if (bolIsShop) {
            intShopID = intID;
        }
        else {
            intClassID = intID;
        }
    }
    GetStockList();
}

function GetStockList() {
    doAjax(
           "../",
           "GetGoodsList",
           {
               "size": pageSize,
               "index": currentPage,
               "key": $.trim($("#txtQuery").val()),
               "ShopID": $("#shopID").val(),
               "ClassID": intClassID,
               "bolGoodsExpense": false
           },
           "json",
           function (json) {
               CreateGoodsProductTable(json.List);
               // CreateGoodsProductPager(json.RecordCount);
               intShopID = "";
               intClassID = "";
           });
}

function CreateGoodsProductTable(obj) {
    var html = ''
            + '<tr class=\"th\">'
              + ' <td>商品名称</td><td>商品简码</td><td>库存数量</td><td>操作</td>'
            + '</tr>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            var bg = index % 2 == 0 ? "#eee" : "#fff";
            html += "<tr class=\"repeterItems\" style='background-color:" + bg + ";cursor:hand;' onmouseover=\"this.style.background='#ddeeff'\" onmouseout=\"this.style.background='" + bg + "'\">"
            html += '<td>' + item.Name + '</td>'
            html += '<td><b>' + item.NameCode + '</b></td>'
                    + '<td>' + item.Number + '</td>'
                    + '<td align="center"><a id="aStock" runat="server" href="javascript:void(0);" onclick="javascript:GetGoodsAllStock(' + item.GoodsID + ',\'' + item.Name + '\');">查看异店库存</a></td>'
                    + '</tr>';
        });
    }
    else {
        html += '<td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="5">未找到符合此条件的数据！请重试！</td>';
    }

    $("#StockList").html(html);
}

function GetGoodsAllStock(goodsID, name) {
    art.dialog({
        lock: true,
        title: '异店库存' + '----' + name,
        width: '600px',
        content: document.getElementById('tbAllStock'),
        id: 'tbAllStock',
        close: function () { $("#tbAllStock").html(""); }
    });
    doAjax("../",
           "GetAllStock",
           { "goodsID": goodsID },
           "json",
           function (json) {
               CreateAllStockTable(json.List);
           });
}
function CreateAllStockTable(obj) {
    var html = ''
            + '<tr class=\"th\">'
            + '<td style="width:300px">异店名称</td><td style="width:300px">库存数量</td>'
            + '</tr>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            var bg = index % 2 == 0 ? "#eee" : "#fff";
            html += "<tr class=\"repeterItems\" style='background-color:" + bg + ";cursor:hand;' onmouseover=\"this.style.background='#ddeeff'\" onmouseout=\"this.style.background='" + bg + "'\">"
            html += '<td>' + item.ShopName + '</td>'
                 + '<td>' + item.Number + '</td>'
                 + '</tr>';
        });
    }
    else {
        html += '<td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="2">未找到其他商家的数据！请重试！</td>';
    }

    $("#tbAllStock").html(html);
}