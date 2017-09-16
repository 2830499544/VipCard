$(document).ready(function () {

    $('#txtStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $('#txtEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });


});





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
            "GetGoodsLogIsShow",
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

function aGoodsRevoke(ID, ShopID) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定撤销该入库记录吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
             'GoodsRevoke',
             {
                 "ID": ID,
                 "InShopID": ShopID
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
                         case -1:
                             art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 3,
                                     content: ("有商品库存不足，无法进行撤单！"),
                                     lock: true
                                 });
                             break;
                         default:
                             art.dialog
                                ({
                                    title: '系统提示',
                                    time: 2,
                                    content: '商品入库撤单成功！',
                                    close: function () { window.location.href = "../StockManage/GoodsLog.aspx?PID=80"; },
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



function GoodsLogPrint(id, type) {

    //获取打印的份数
    var PointNum = $("#PointNum").val(); 
    doAjax("../",
            "GoodsLogPrinting",
            {
                "id": id
            },
            "json",
            function (json) {
                if (json != "") {
                    var userName = $("#txtUser").val();
                    var printtitle = $("#lblPrintTitle").html();
                    var printfoot = $("#lblPrintFoot").html();
                    PrintGoodsLog(userName, printtitle, printfoot, json.strGoodsLog[0], json.strGoodsLogDetail, PointNum);
                }
            });
}