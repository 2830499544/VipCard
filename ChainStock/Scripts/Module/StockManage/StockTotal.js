var pageSize = 10;
var currentPage = 1;
var intShopID = "";
var intClassID = "";
$(document).ready(function () {
    $("#txtQuery").focus();

    //绑定部门列表到树形菜单
    mkTree_convertTrees();
    expandTree('treeStock');
    intShopID = $("#sltShop").val();
    GetStockTotal();
    //按“回车键”响应查询
    $("#txtQuery").keypress(function (e) {
        var keyAscii = window.event ? e.keyCode : e.which;
        if (keyAscii == 13) {
            GetStockList();
        }
    });

    //"查询"按钮响应函数
    $("#btnStockQuery").bind("click", GetStockTotal);


    //绑定商品类别事件
    $("#sltShop").bind("change", SetGoodsClass);

    //绑定商品类别
    SetGoodsClass();
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
    GetStockTotal();
}

function GetStockTotal() {
    doAjax(
           "../",
           "GetGoodsStockTotal",
           {
               "size": pageSize,
               "index": currentPage,
               "key": $.trim($("#txtQuery").val()),
               "ClassID": $.trim($("#sltGoodsClass").val()),
               "ShopID": $.trim($("#sltShop").val()),
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
              + ' <td>商品名称</td><td>商品简码</td><td>库存数量</td><td>所属商家</td>'
            + '</tr>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            var bg = index % 2 == 0 ? "#eee" : "#fff";
            html += "<tr class=\"repeterItems\" style='background-color:" + bg + ";cursor:hand;' onmouseover=\"this.style.background='#ddeeff'\" onmouseout=\"this.style.background='" + bg + "'\">"
            html += '<td>' + item.Name + '</td>'
            html += '<td><b>' + item.NameCode + '</b></td>'
                    + '<td>' + item.Number + '</td>'
                    + '<td align="center">' + item.ShopName + '</td>'
                    + '</tr>';
        });
    }
    else {
        html += '<td  style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="5">未找到符合此条件的数据！请重试！</td>';
    }

    $("#StockTotalList").html(html);
}



function SetGoodsClass() {
    var cid = $("#shopID").val();
    if (cid != 0)
        cid = $("#sltShop").val();
    $("#sltGoodsClass").empty();
    $("#sltGoodsClass").append("<option value=''>===== 请选择 =====</option>");
     if (cid == "") {return;}
    doAjax(
        "../",
        "GetGoodsClassList",
        { "ShopID": cid },
        "json",
         function (json) {
             switch (json) {
                 case -1:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("暂无商品类别，请先从商品分类中添加类别！"),
                              lock: true
                          });
                     break;
                 case -3:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("系统错误 请与系统管理员联系！"),
                              lock: true
                          });
                     break;
                 default:
                     for (var i = 0; i < json.length; i++) {
                         $("#sltGoodsClass").append("<option value='" + json[i].ClassID + "'>" + json[i].ClassName + "</option>");
                     }
                     break;
             }
         })
}