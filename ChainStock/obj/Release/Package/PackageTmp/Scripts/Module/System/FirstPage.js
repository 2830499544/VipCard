$(document).ready(function () {
   
    bindchart($("#txtMemStartTime").val(), $("#txtMemEndTime").val());

});



function bindchart(starttime, endtime) {
    


    $.get("../../../Service/AjaxService.ashx?Method=GetMemChartIndex",
    {
        "StartTime": starttime,
        "EndTime": endtime,
        "ShopID": $("#sltShop").val()
    }
    , function (text) {
        var json = JSON.parse(text);

        if (json != "") {
          
            var Interval = json.Interval.toString().split(",");
            var arrayObj = json.Mydata.toString().split(",");
            var Mydata = strtoint(arrayObj);
            var series = [{ name: '新增人数', data: Mydata}];
            ChartData('line', '', '', Interval, series);
        }
    }, "text")



}

function ChartData(SeriesType, mytitle, myunit, myInterval, mySeries) {
    $('#divMemContainer').highcharts = new Highcharts.Chart({
        chart: {
            renderTo: 'divMemContainer',
            plotBackgroundColor: "#fff",
            plotBorderWidth: null,
            defaultSeriesType: SeriesType,
            borderWidth: 0
        },
        title: {
            text: mytitle

        },
        legend: {
            enabled: false,
            borderWidth: 0
        },
        xAxis: {//X轴数据
            labels: {
                align: 'right',
                style: { font: 'normal 13px 宋体' }
            },
            categories: myInterval
        },
        yAxis: {//Y轴显示文字
            title: {
                text: myunit
            }
        },
        series: mySeries
    });
}