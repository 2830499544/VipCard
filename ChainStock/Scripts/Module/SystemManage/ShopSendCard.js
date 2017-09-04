
/*******************************************************************************
*删除按钮响应函数
*******************************************************************************/
function ShopBuyCardDelete(BuyCardID, StartCardNumber, EndCardNumber) {
    var shoptype = $("#txtShopType").val();
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要撤销【卡号' + StartCardNumber + '至' + EndCardNumber + '】吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
                   "ShopBuyCardDelete",
                   { "BuyCardID": BuyCardID },
                   "json",
                   function (json) {
                       switch (json) {
                           
                           case 0:
                               art.dialog({
                                   title: '系统提示',
                                   time: 4,
                                   content: ("系统异常，未撤销成功，请再次点击撤销！"),
                                   lock: true
                               });
                               break;
                           default:
                               art.dialog({
                                   title: '系统提示',
                                   time: 2,
                                   content: '撤销成功！',
                                   close: function () { location.href = "ShopSendCard.aspx?ShopType="+shoptype; }
                               });
                               break;
                       }
                   });
            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    });
}

