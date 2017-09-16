var index = 0;
var type = ['line', 'spline', 'area', 'areaspline', 'column', 'scatter'];
$(document).ready(function () {
    bindchart($("#txtMemStartTime").val(), $("#txtMemEndTime").val());
//    $('#txtMemStartTime').bind("focus click", function () {
//        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
//    });
//    $('#txtMemEndTime').bind("focus click", function () {
//        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
//    });
    $("#btSerch").bind("click", btSerch);
    $("#btshowchart").bind("click", btshowchart);
});

function btQuery() {
    var strReg = /^[0-9]*[1-9][0-9]*$/; //正则表达式
    var strErrorMsg = "";
    if (!strReg.test($("#txtPoint").val()) && $("#txtPoint").val() != "" && $("#txtPoint").val() != "0") {
        strErrorMsg += "<li>输入变动积分格式不正确,请重新输入!</li>";
    }
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    return true;
}

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
        title: '会员积分变动统计图',
        width: '800px',
        content: document.getElementById('ChartShow'),
        id: 'ChartOfMemRecharge',
        close: function () { }
    });

}

function bindchart(starttime, endtime) {
    doAjax(
                "../",
                "GetPointChart",
                {
                    "StartTime": starttime,
                    "EndTime": endtime,
                    "ShopID": $("#sltShop").val()
                },
                "json",
                function (json) {
                    if (json != "") {
                        var Interval = json.Interval.toString().split(",");
                        var arrayObj1 = json.NewPoint.toString().split(",");    //新增积分
                        var arrayObj2 = json.CutPoint.toString().split(",");   //消费积分
                        var NewPoint = strtoint(arrayObj1);
                        var CutPoint = strtoint(arrayObj2);
                        var series = [{ name: '新增积分', data: NewPoint }, { name: "消费积分", data: CutPoint}];
                        LodaChartData('column', '会员积分变动统计图', '积分 (个)', Interval, series);
                    }
                });
}


//打印
function againPoint(pointID) {

    //获取打印的份数
    var PointNum = $("#PointNum").val(); 

    doAjax("../", "AgainPrintPointID", { "PointID": pointID }, "json", function (json) {
        var row = json[0];
        var lblPrintTitle = $("#lblPrintTitle").html();
        var exchangeType = row.PointChangeType;
        var changeNumber = row.PointNumber;
        var printCount = "2";
        var userName = row.UserName;
        var getDataTimeNow = row.PointCreateTime;
        var lblPrintFoot = $("#lblPrintFoot").html();
        var print = true;
        var PointRemark = row.PointRemark;
        Print_PointChangeAgain(lblPrintTitle, row, exchangeType, changeNumber, printCount, userName, getDataTimeNow, lblPrintFoot, print, PointNum, PointRemark);
    })
}