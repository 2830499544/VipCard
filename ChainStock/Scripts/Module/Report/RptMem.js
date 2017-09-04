var index = 0;
var type = ['line', 'spline', 'area', 'areaspline', 'column', 'scatter'];
$(document).ready(function () {
    $("#txtShop").val($("#HDsltshop").val());


    $("#tdheard").append($("#rptMem"))
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

//function btdetail() {
//    $("#ChartSerch").css("display", "none");
//    $("#chart").css("display", "none");
//    $("#ReportSerch").css("display", "block");
//    $("#report").css("display", "block");
//}

function btshowchart() {
    art.dialog({
        lock: true,
        title: '新增会员数统计图',
        width: '800px',
        content: document.getElementById('ChartShow'),
        id: 'ChartOfMem',
        close: function () { }
    });
}

function bindchart(starttime, endtime) {
    doAjax(
                "../",
                "GetMemChart",
                {
                    "StartTime": starttime,
                    "EndTime": endtime,
                    "ShopID": $("#sltShop").val()
                },
                "json",
                function (json) {
                    if (json != "") {
                        var Interval = json.Interval.toString().split(",");
                        var arrayObj = json.Mydata.toString().split(",");
                        var Mydata = strtoint(arrayObj);
                        var series = [{ name: '新增人数', data: Mydata}];
                        LodaChartData('areaspline', '新增会员数统计图', '人数 (个)', Interval, series);
                    }
                });
}