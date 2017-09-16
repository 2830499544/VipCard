var intShopID = "";
var intClassID = "";
$(document).ready(function () {
    var tag = $("#otherStock").val();
    if (tag == "0") {
        $("a[id*=aStock]").css("display", "none");
    } else {
        $("a[id*=aStock]").css("display", "");
    }
})

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
            + '<tbody>'
            + '<tr class=\"th\">'
            + '<th style="width:300px">异店名称</th><th style="width:300px">库存数量</th'
            + '</tr>'
            + '</tbody>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            html += "<tr class=\"td\">"
            html += '<td>' + item.ShopName + '</td>'
                 + '<td>' + item.Number + '</td>'
                 + '</tr>';
        });
    }
    else {
        html += '<td align="center" style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="2">未找到其他商家的数据！请重试！</td>';
    }

    $("#tbAllStock").html(html);
}


/****************************************************************************************************
*输入商品名称或简码后  回车响应事件
*****************************************************************************************************/
function getKey() {
    if (event.keyCode == 13) {
        //alert('ok');
        //document.getElementById("btnUserSearch").onclick();
        //$("#btnUserSearch").click();


//       // $("#a_doClick").click(function () {  
//                $("#<%=btnTest.ClientID%>").click();  
        //           })  
        $('#<%=btnUserSearch.ClientID%>').click();  




    }
}
 

