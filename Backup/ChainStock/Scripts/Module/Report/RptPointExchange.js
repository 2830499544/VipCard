var index = 0;
var type = ['line', 'spline', 'area', 'areaspline', 'column', 'scatter'];
$(document).ready(function () {
    bindchart($("#txtMemStartTime").val(), $("#txtMemEndTime").val());
    $('#txtMemStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $('#txtMemEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $("#btSerch").bind("click", btSerch);
    $("#btshowchart").bind("click", btshowchart);
});


function btSerch() {
    var starttime = $("#txtMemStartTime").val();
    var endtime = $("#txtMemEndTime").val();
    if (starttime == "" || endtime == "") {
        art.dialog
            ({
                title: '系统提示',
                time: 4,
                content: ("请选择开始时间和结束时间！"),
                lock: true
            });
        return;
    }
    if (!dateValid(starttime, endtime)) {
        art.dialog
            ({
                title: '系统提示',
                time: 4,
                content: ("结束时间必须大于开始时间！"),
                lock: true
            });
        return;
    }
    if (DateDiff(starttime, endtime) > 30 || DateDiff(starttime, endtime) < -30) {
        art.dialog
            ({
                title: '系统提示',
                time: 4,
                content: ("选择的时间间隔必须是31天之内！"),
                lock: true
            });
        return;
    }
    bindchart(starttime, endtime);
}

function btshowchart() {
    art.dialog({
        lock: true,
        title: '会员兑换统计图',
        width: '800px',
        content: document.getElementById('ChartShow'),
        id: 'ChartOfMemRecharge',
        close: function () { }
    });
}

function bindchart(starttime, endtime) {
    doAjax(
                "../",
                "GetGiftExchangeChart",
                {
                    "StartTime": starttime,
                    "EndTime": endtime,
                    "ShopID": $("#sltShop").val()
                },
                "json",
                function (json) {
                    if (json != "") {
                        var Interval = json.Interval.toString().split(",");
                        var arrayObj1 = json.AllPoint.toString().split(",");    //兑换的总积分
                        var arrayObj2 = json.MainPoint.toString().split(",");   //主系统兑换总积分
                        var arrayObj3 = json.SelfPoint.toString().split(","); //自助平台兑换总积分
                        var AllPoint = strtoint(arrayObj1);
                        var MainPoint = strtoint(arrayObj2);
                        var SelfPoint = strtoint(arrayObj3);
                        var series = [{ name: "主系统兑换积分", data: MainPoint }, { name: "自助平台兑换积分", data: SelfPoint }, { name: '兑换总积分', data: AllPoint}];
                        LodaChartData('spline', '会员兑换统计图', '积分 (个)', Interval, series);
                    }
                });
}


//打印
function againPrint(rechargeID) {

    //获取打印的份数
    var PointNum = $("#PointNum").val(); 

    doAjax("../", "AgainPrintGiftExchange", { "RechargeID": rechargeID, "GiftExchange": "GiftExchange" }, "json", function (json) {
        giftExchange = json;
        doAjax("../", "AgainPrintGiftExchange", { "RechargeID": rechargeID, "GiftExchangeDetail": "GiftExchangeDetail" }, "json", function (jsonDetail) {
            var row = giftExchange[0];
            var lblPrintTitle = $("#lblPrintTitle").html();
            var sumNumber = row.ExchangeAllNumber;
            var sumPoint = row.ExchangeAllPoint;
            giftExchangeDetail = jsonDetail;
            var userName = row.UserName;
            var getDataTimeNow = row.ExchangeTime;
            var lblPrintFoot = $("#lblPrintFoot").html();
            var print = true;
            var printCount = 2;
            var exchangeAccount = row.ExchangeAccount;
            Print_GiftExchangeAgain_Point(lblPrintTitle, row, sumNumber, sumPoint, giftExchangeDetail, userName, getDataTimeNow, lblPrintFoot, print, printCount, exchangeAccount, PointNum);
        })
    })
}