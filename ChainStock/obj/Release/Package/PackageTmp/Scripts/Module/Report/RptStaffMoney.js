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
        title: '员工提成统计图',
        width: '800px',
        content: document.getElementById('ChartShow'),
        id: 'ChartOfMemRecharge',
        close: function () { }
    });
}

function bindchart(starttime, endtime) {
    doAjax(
                "../",
                "GetStaffMoneyChart",
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
                        var Mydata = strtofloat(arrayObj);
                        var series = [{name: '提成',data: Mydata}];
                        LodaChartData('areaspline', '员工提成统计图', '提成 (元)', Interval, series);
                    }
                });
}

/***********************************************************
*展开详情
arg2为 是否展开的条件参数
************************************************************/
function ShowStaffDetail(id, arg2) {
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
            "GetStaffIsShow",
            {
                "id": id,
                "startTime": $("#txtStaffStartTime").val(),
                "endTime": $("#txtStaffEndTime").val()
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

