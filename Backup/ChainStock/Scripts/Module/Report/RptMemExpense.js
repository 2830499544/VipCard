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

    $("#txtShop").val($("#HDsltshop").val());
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
        title: '会员消费统计图',
        width: '800px',
        content: document.getElementById('ChartShow'),
        id: 'ChartOfMemExpense',
        close: function () { }
    });

}

function bindchart(starttime, endtime) {
    doAjax(
                "../",
                "GetOrderChart",
                {
                    "StartTime": starttime,
                    "EndTime": endtime,
                    "ShopID": $("#sltShop").val(),
                    "MemCard": $("#txtMemInfo").val()
                },
                "json",
                function (json) {
                    if (json != "") {
                        var Interval = json.Interval.toString().split(",");
                        var arrayObj1 = json.TotalMoney.toString().split(",");    //应收总金额
                        var arrayObj2 = json.TotalDiscount.toString().split(",");   //实收总金额
                        var arrayObj3 = json.Coupon.toString().split(","); //优惠券总金额
                        var arrayObj4 = json.Point.toString().split(","); //获得总积分
                        var TotalMoney = strtofloat(arrayObj1);
                        var TotalDiscount = strtofloat(arrayObj2);
                        var Coupon = strtofloat(arrayObj3);
                        var Point = strtoint(arrayObj4);
                        var series = [{ name: "优惠券总金额", data: Coupon }, { name: "获得总积分", data: Point }, { name: '应收总金额', data: TotalMoney }, { name: "实收总金额", data: TotalDiscount}];
                        LodaChartData('spline', '会员消费统计图', '消费 (元)', Interval, series);
                    }
                });
}