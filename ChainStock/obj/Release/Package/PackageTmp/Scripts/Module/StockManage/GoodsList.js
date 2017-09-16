$(document).ready(function () {
    //商品编码获取焦点
    $("#txtGoodsCode").focus();

    //绑定商品类别事件
    $("#sltShop").bind("change", SetGoodsClass);

    //绑定商品类别
    SetGoodsClass();
})

//删除按钮响应事件
function DeleteGoods(goodsID, shopID) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除该商品吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
             'GoodsDel', { "GoodsID": goodsID, "ShopID": shopID }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统异常 记录未能删除，请重试！"),
                                       lock: true
                                   });
                             break;
                         case -3:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统错误 请与系统管理员联系！"),
                                       lock: true
                                   });
                             break;
                         case -2:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("此商品已有变动记录,不能删除！"),
                                       lock: true
                                   });
                             break;
                         default:
                             art.dialog
                                   ({
                                       time: 0.5,
                                       content: '删除成功！',
                                       close: function () { location.href = "GoodsList.aspx?PID=62"; },
                                       lock: true
                                   });
                             break;
                     }
                 });

            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    });
    return false;
}

function SyncGoods(goodsID, shopID) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要同步该商品到其他商家吗? 此操作不可返回',
        ok: function () {
            this.close();
            doAjax("../",
             'GoodsSync', { "GoodsID": goodsID, "ShopID": shopID }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统异常 记录未能删除，请重试！"),
                                       lock: true
                                   });
                             break;
                         case -3:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统错误 请与系统管理员联系！"),
                                       lock: true
                                   });
                             break;
                         case -2:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("此商品已有变动记录,不能删除！"),
                                       lock: true
                                   });
                             break;
                         default:
                             art.dialog
                                   ({
                                       time: 0.5,
                                       content: '同步成功！',
                                       lock: true
                                   });
                             break;
                     }
                 });

            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    });
    return false;
}

function SetGoodsClass() {
    var cid = $("#sltGoodsClass").val();
    doAjax(
        "../",
        "GetGoodsClassList",
        { "ShopID": $("#sltShop").val() },
        "json",
         function (json) {
             switch (json) {
                 case -1:
                     $("#sltGoodsClass").empty();
                     $("#sltGoodsClass").append("<option value=''>===== 请选择 =====</option>");
                     //                     art.dialog
                     //                          ({
                     //                              title: '系统提示',
                     //                              time: 4,
                     //                              content: ("暂无类别信息，请先从商品分类中添加类别！"),
                     //                              close: function () { $("#sltGoodsClass").empty(); $("#sltGoodsClass").append("<option value=''>===== 请选择 =====</option>"); },
                     //                              lock: true
                     //                          });
                     //                     break;
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
                     $("#sltGoodsClass").empty();
                     $("#sltGoodsClass").append("<option value=''>===== 请选择 =====</option>");
                     for (var i = 0; i < json.length; i++) {
                         $("#sltGoodsClass").append("<option value='" + json[i].ClassID + "'>" + json[i].ClassName + "</option>");
                     }
                     var goodsClass = $("#txtGoodsClass").val();
                     $("#sltGoodsClass").val(goodsClass);
                     break;
             }
         })
}