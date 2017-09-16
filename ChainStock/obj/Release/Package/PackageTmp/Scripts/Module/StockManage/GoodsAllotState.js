$(document).ready(function () {

    //申请调入
    $("#Allot_In").bind("click", Allot_In);
    //申请调出
    $("#Allot_Out").bind("click", Allot_Out);

    $('#txtStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $('#txtEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });



});


//商品调拨之  编辑
function AllotEdit(AllotID, b) {
    window.location.href = "../StockManage/GoodsAllot.aspx?PID=105&AllotID=" + AllotID;
}

//商品调拨之  发货
function AllotFahuo(AllotID, b) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定发货吗? 请谨慎操作！',
        ok: function () {
            this.close();
            doAjax("../",
             'ConfirmDelivery',
             {
                 "AllotID": AllotID

             }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("发货成功！"),
                                     close: function () { window.location.href = "../StockManage/GoodsAllotState.aspx?PID=139"; },
                                     lock: true
                                 });
                             break;
                         case -1:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("系统异常，未保存数据！"),
                                     lock: true
                                 });
                             break;
                         case -2:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("有商品库存不足，无法进行发货操作！"),
                                     lock: true
                                 });
                             break;
                         case -3:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("数据更新失败，请联系管理员！"),
                                     lock: true
                                 });
                             break;
                         case -4:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("该单已发货！不允许重复发货！"),
                                     close: function () { window.location.href = "../StockManage/GoodsAllotState.aspx?PID=139"; },
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
function AllotShouhuo(AllotID, b) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定收货吗? 请谨慎操作！',
        ok: function () {
            this.close();
            doAjax("../",
             'ConfirmReceipt',
             {
                 "AllotID": AllotID

             }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("成功收货！"),
                                     close: function () { window.location.href = "../StockManage/GoodsAllotState.aspx?PID=139"; },
                                     lock: true
                                 });
                             break;
                         case -1:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("系统错误，请联系管理员！"),
                                     lock: true
                                 });
                             break;
                         case -3:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("数据更新失败，请联系管理员！"),
                                     lock: true
                                 });
                             break;
                         case -4:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("该单已收货，不允许继续收货！"),
                                     close: function () { window.location.href = "../StockManage/GoodsAllotState.aspx?PID=139"; },
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

//商品调拨之  撤单
function AllotCedan(AllotID, b) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定撤销调拨单吗? 请谨慎操作！',
        ok: function () {
            this.close();
            doAjax("../",
             'AllotRevoke',
             {
                 "AllotID": AllotID
             }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("撤单成功！"),
                                     close: function () { window.location.href = "../StockManage/GoodsAllotState.aspx?PID=139"; },
                                     lock: true
                                 });
                             break;
                         case -1:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("系统错误，请联系管理员！"),
                                     lock: true
                                 });
                             break;
                         case -2:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("入库的商家 商品库存不足，无法撤单！"),
                                     lock: true
                                 });
                             break;
                         case -3:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("数据更新失败，请联系管理员！"),
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


//申请调入
function Allot_In() {

    //    $("#Allot_In").css("color", "green");
    //    $("#Allot_Out").css("color", "#000000");

    //    var HiddenShopID = $("#HiddenShopID").val();
    //    $("#sltInShopID").val(HiddenShopID);
    //    $("#sltInShopID").css("color", "red");
    //    $("#sltInShopID").attr("disabled", "disabled");

    //    $("#sltOutShopID").attr("disabled", "");
    //    $("#HiddenType").val(2);

    window.location.href = "../StockManage/GoodsAllot.aspx?PID=105&Type=1";
    //window.location.href = "../SystemManage/SysNotice.aspx?PID=43&NoticeID=" + noticeid;


}
//申请调出
function Allot_Out() {
    window.location.href = "../StockManage/GoodsAllot.aspx?PID=105&Type=2";


    //    $("#Allot_In").css("color", "#000000");
    //    $("#Allot_Out").css("color", "green");

    //    var HiddenShopID = $("#HiddenShopID").val();
    //    $("#sltOutShopID").val(HiddenShopID);
    //    $("#sltOutShopID").attr("disabled", "disabled");

    //    $("#sltInShopID").attr("disabled", "");

    //    $("#HiddenType").val(1);

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
            "GetGoodsLogIsShows",
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
