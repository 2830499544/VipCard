$(document).ready(function () {


    $('#txtStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $('#txtEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });



});

//商品调拨之  发货
function BuySend(BuyID, b) {

    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定发货吗? 请谨慎操作！',
        ok: function () {
            this.close();
            doAjax("../",
             'GoodsBuySend',
             {
                 "BuyID": BuyID,"Status":1

             }, "json",
                 function (json) {
                     switch (json) {
                         
                         case 0:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("系统异常，未保存数据！"),
                                     lock: true
                                 });
                             break;
                         case 1:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("发货成功！"),
                                     close: function () { window.location.href = "../GoodsBuy/GoodsBuySureList.aspx?PID=182"; },
                                     lock: true
                                 });
                             break;
                     }
                 });
            return false;
        },
        cancelVal: '取消',
        cancel: true
    });
    return false;
}

//商品调拨之  收货
function BuyGet(BuyID, b) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定收货吗? 请谨慎操作！',
        ok: function () {
            this.close();
            doAjax("../",
             'GoodsBuyGet',
             {
                 "BuyID": BuyID,"Status":2

             }, "json",
                 function (json) {
                     switch (json) {
                        
                         case 0:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("系统错误，请联系管理员！"),
                                     lock: true
                                 });
                             break;
                          default:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("收货成功！"),
                                     close: function () { window.location.href = "../GoodsBuy/GoodsBuySureList.aspx?PID=182"; },
                                     lock: true
                                 });
                             break;
                     }
                 });
            return false;
        },
        cancelVal: '取消',
        cancel: true
    });
    return false;
}




/***********************************************************
*展开详情
arg2为 是否展开的条件参数
************************************************************/
function ShowGoodsLogDetail(id, arg2) {

    if ($("#img" + id).css("display") != "none") {
        if ($("#data" + id).css("display") == "none") {
            $("#data" + id).css("display", "");
            $("#img" + id).attr("src", "../Inc/Style/images/minus.gif")
        }
        else {
            $("#data" + id).css("display", "none");
            $("#img" + id).attr("src", "../Inc/Style/images/plus.gif")
        }
    }
    else {
        $("#data" + id).css("display", "none");
    }
}

function IsShowPic(id, arg2) {
    doAjax("../",
            "GetGoodsBuyDetailIsShows",
            {
                "id": id
            },
            "json",
            function (json) {
                if (json == 1) {
                    $("#img" + id).css("display", "");
                }
                else {
                    $("#img" + id).css("display", "none");
                    $("#a" + id).css("padding-left", "22px");
                }

            });

}
