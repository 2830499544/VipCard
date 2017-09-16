var index = 0;
var type = ['line', 'spline', 'area', 'areaspline', 'column', 'scatter'];
$(document).ready(function () {
    $("#txtShop").val($("#HDsltshop").val());
 
    //绑定商品类别事件
    $("#sltShopList").bind("change", SetGoodsClass);
    //绑定商品类别
    SetGoodsClass();

    bindchart($("#txtMemStartTime").val(), $("#txtMemEndTime").val());
 
    $("#btSerch").bind("click", btSerch);
    $("#btshowchart").bind("click", btshowchart);
})

//绑定商品类别下拉框
function SetGoodsClass() {
    var cid = $("#sltGoodsClass").val();
    doAjax(
        "../",
        "GetGoodsClassList",
        { "ShopID": $("#sltShop").val() },
        "json",
         function (json) {
             switch (json) {
                 case -1:
                     $("#sltGoodsClass").empty();
                     $("#sltGoodsClass").append("<option value=''>===== 请选择 =====</option>");
                     break;
                 case -3:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("系统错误 请与系统管理员联系！"),
                              lock: true
                          });
                     break;
                 default:
                     $("#sltGoodsClass").empty();
                     $("#sltGoodsClass").append("<option value=''>===== 请选择 =====</option>");
                     for (var i = 0; i < json.length; i++) {
                         $("#sltGoodsClass").append("<option value='" + json[i].ClassID + "'>" + json[i].ClassName + "</option>");
                     }
                     var goodsClass = $("#txtGoodsClass").val();
                     $("#sltGoodsClass").val(goodsClass);
                     break;
             }
         })
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
        title: '商品消费统计图',
        width: '800px',
        content: document.getElementById('ChartShow'),
        id: 'ChartOfMemRecharge',
        close: function () { }
    });

}

function bindchart(starttime, endtime) {
    doAjax(
                "../",
                "GetGoodsExpenseChart",
                {
                    "StartTime": starttime,
                    "EndTime": endtime,
                    "GoodsCode": $("#txtgoodcode").val(),
                    "ShopID": $("#sltShop").val()
                },
                "json",
                function (json) {
                    if (json == "0") {
                        art.dialog
                             ({
                                 title: '系统提示',
                                 time: 4,
                                 content: ("输入的商品编码不存在！"),
                                 lock: true
                             });
                        return;
                    }
                    if (json != "") {
                        var Interval = json.Interval.toString().split(",");
                        var arrayObj1 = json.Mydata.toString().split(",");    //总消费
                        var arrayObj2 = json.GeneralMoney.toString().split(",");   //普通商品消费
                        var arrayObj3 = json.ServeMoney.toString().split(","); //服务项目消费
                        var Mydata = strtofloat(arrayObj1);
                        var GeneralMoney = strtofloat(arrayObj2);
                        var ServeMoney = strtofloat(arrayObj3);

                        var series = [{ name: "普通商品", data: GeneralMoney },
                                      { name: "服务项目", data: ServeMoney },
                                      { name: '总消费', data: Mydata }
                                      ];
                        LodaChartData('areaspline', '商品消费统计图', '消费 (元)', Interval, series);

                    }
                });
            }

var intGoodsID = "";
var strName = "";
var pageSize = 10;
var currentPage = 1;

function ShowGoodsDetail(name) {
    strName = name;
    art.dialog({
        lock: true,
        title: '商品消费详情' + '----' + name,
        left: '35%',
        top: "35%",
        width: '700',
        content: document.getElementById('dvDetail'),
        id: 'dvDetail',
        close: function () { $("#tbRptGoodsExpense").html(""); pageSize = 10; currentPage = 1; }
    });
}
function GetGoodsExpense(goodsid,name) {
    doAjax("../",
           "GetGoodsExpenseDetail",
           { 
                "goodsName":$("#txtGoodsName").val(),
                "goodsType":$("#sltGoodsType").val(),
                "shopID":$("#sltShop").val(),
                "goodsClass":$("#sltGoodsClass").val(),
               "goodsID": goodsid,
               "startTime": $("#txtStartTime").val(),
               "endTime": $("#txtEndTime").val(),
               "pageSize": pageSize,
               "currentPage": currentPage
           },
           "json",
           function (json) {
               CreateTable(json.List);
               CreatePager(json.RecordCount,goodsid);
               ShowGoodsDetail(name);
           });
}

function CreateTable(obj) {
    var html = ''
             + '  <thead class=\"thead\">'
             + ' <tr class=\"th\">'
             + ' <th>序号</th><th align="center">订单编号</th><th align="center">会员卡号</th><th align="center">会员姓名</th><th align="center">消费类型</th><th align="center">消费数量</th><th align="center">商品总金额</th><th align="center">消费时间</th>'
             + '</tr></thead>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            html += "<tr class=\"td\">"
            html += '<td>' + (index + 1) + '</td>'
            html += '<td align="center">' + item.OrderAccount + '</td>'
            html += '<td align="left">' + item.MemCard + '</td>'
            html += '<td align="left">' + item.MemName + '</td>'
            html += '<td align="center">' + (parseInt(item.OrderDetailType) == 0 ? "商品消费" : "计次消费") + '</td>'
            html += '<td align="right">' + item.OrderDetailNumber + '</td>'
            html += '<td align="right">' + parseFloat(item.OrderDetailDiscountPrice).toFixed(2) + '</td>'
            html += '<td>' + item.OrderCreateTime + '</td>'
            html += '</tr>'
        });
    }
    else {
        html += '<td align="center" style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="8">未找到符合此条件的数据！请重试！</td>';
    }
    $("#tbRptGoodsExpense").html(html);
}
//详情页点击分页链接无效 —— 商品id 没有传过来
function CreatePager(resCount,goodid) {
    var page = new CommonPager(
        "RptGoodsExpensePage",
        pageSize,
        resCount,
        currentPage,
        function (p) {
            currentPage = parseInt(p);
            GetGoodsExpense(goodid);
        }
    );
    page.ShowSimple();
}