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
})

function btQuery() {
    var strErrorMsg = "";
    if (!$("#txtDrawMoney").val().IsDecimal() && $("#txtDrawMoney").val() != "") {
        strErrorMsg += "<li>输入提现金额格式不正确,请重新输入!</li>";
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
        title: '会员提现统计图',
        width: '800px',
        content: document.getElementById('ChartShow'),
        id: 'ChartOfMemRecharge',
        close: function () { }
    });
}

function bindchart(starttime, endtime) {
    doAjax(
                "../",
                "GetDrawMoney",
                {
                    "StartTime": starttime,
                    "EndTime": endtime,
                    "ShopID": $("#sltShop").val()
                },
                "json",
                function (json) {
                    if (json != "") {
                        var Interval = json.Interval.toString().split(",");
                        var arrayObj1 = json.Mydata.toString().split(",");    //会员扣除余额
                        var arrayObj2 = json.PracticalMoney.toString().split(","); //实际提现金额
                        var Mydata = strtofloat(arrayObj1);
                        var PracticalMoney = strtofloat(arrayObj2);
                        var series = [{ name: '余额扣除', data: Mydata }, { name: "提现金额", data: PracticalMoney}];
                        LodaChartData('areaspline', '会员提现统计图', '提现 (元)', Interval, series);
                    }
                });
}