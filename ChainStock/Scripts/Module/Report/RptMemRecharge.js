var exchangeTab;
var pageSize = 10;
var currentPage = 1;
var index = 0;
var type = ['line', 'spline', 'area', 'areaspline', 'column', 'scatter'];
$(document).ready(function () {
    $("#txtShop").val($("#HDsltshop").val());

//跳转到页面时  默认获得充值列表数据
    $("#MainContent0").css("display", "");
    $("#Export").val("R");
    RechargeList();
    //切换标签时执行的方法
    exchangeTab = new CommonTab("ExchangeTab", "",
        function (index) {
            if (index == 0) {
                RechargeList();
                $("#Export").val("R");
            }
            else
                if (index == 1) {
                    MemRechargeList();
                    $("#Export").val("MR");
                }
                else if (index == 2) {
                    ShopRechargeList();
                    $("#Export").val("SR");
                }
        });

    $("#btnResert").click(function () {
        bindchart($("#txtMemStartTime").val(), $("#txtMemEndTime").val());
    });
    $('#txtMemStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext',dateFmt:'yyyy-MM-dd HH:mm:ss', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $('#txtMemEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm:ss', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $("#btSerch").bind("click", btSerch);
    $("#btshowchart").bind("click", btshowchart);

    //会员充值报表查询按钮事件绑定
    $("#btnMRQuery").bind("click", GetMemRechargeList);
    //商家充值报表查询按钮事件绑定
    $("#btnSRQuery").bind("click", GetShopRechargeList);

    $('#txtMRStart').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm:ss', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $('#txtMREnd').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm:ss', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $('#txtSRStart').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm:ss', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $('#txtSREnd').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm:ss', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });

})
//充值列表(分页)
function RechargeList() {
    var html = $("#RechargeList").html();
    if (html.indexOf("LoadingBar") != -1) {
        RechargeTable();
    }
}
//获取充值列表数据 
function RechargeTable() {
    doAjax("../",
           "GetRechargeListByPage",
           {
               "memInfo": $("#txtQueryMem").val(),
               "memLevel": $("#sltMemLevelID").val(),
               "rechargeAccount": $("#txtRechargeAccount").val(),
               "remark": $("#txtRemark").val(),
               "rechargeType": $("#sltRecharge").val(),
               "memShopID": $("#sltShop").val(),
               "rechargeMoney": $("#txtMoney").val(),
               "moneySymbols": $("#sltMoney").val(),
               "startTime": $("#txtStartTime").val(),
               "endTime": $("#txtEndTime").val(),
               "size": pageSize,
               "index": currentPage
           },
           "json",
           function (json) {
               //根据返回的json来拼接充值记录列表
               var html = '<tr class=\"th\"><th>充值单号</th><th>会员卡号</th><th>会员姓名</th><th>充值类型</th><th>充值总额</th><th>充值金额 </th><th>赠送金额</th><th>账户余额</th><th>操作详情</th><th>商家</th><th>日期</th><th>操作员</th><th>操作</th></tr>';
               if (json.RechargeList.length > 0) {
                   $.each(json.RechargeList, function (index, item) {
                       html += "<tr class=\"td\" >"
                       html += '<td style="width: 120px;">' + item.RechargeAccount + '</td>'
                       html += '<td align="left">' + item.MemCard + '</td>'
                       html += '<td align="left">' + item.MemName + '</td>'
                       html += '<td align="left">' + GetRechargeType(item.RechargeType) + '</td>'
                       html += '<td align="left" style="color:red;">' + item.RechargeMoney.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left">' + item.RechargeOrdMoney.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left">' + item.RechargeGive.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left" style="color:red;">' + item.RechargeCardBalance.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left">' + item.RechargeRemark + '</td>'
                       html += '<td align="left">' + item.ShopName + '</td>'
                       html += '<td align="left">' + item.RechargeCreateTime + '</td>'
                       html += '<td align="left">' + item.UserName + '</td>'
                       html += '<td align="left">'
                       html += '<a href=" javascript:againPoint(' + item.RechargeID + ')">'
                             + '<img src="../images/Gift/print.png" alt="打印小票" title="打印小票" /></a>'
                       if ($("#Revokable").val()) {
                           html += '<a id="gvRptMemRecharge_hyRevoke" class="arevoke" runat="server" href="#" onclick="return RechargeRevoke(' + item.RechargeID + ',' + item.RechargeMemID + ')">'
                             + '<img src="../images/Gift/revoke.png" alt="撤单" title="撤单" /></a>'
                       }
                       html += '</td></tr>'
                   })
               }
               else {
                   html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="9">未找到符合此条件的数据！</td></tr>';
               }
               $("#RechargeList").html(html);

               var page = new CommonPager("divRechargeListPage", pageSize, json.RecordCount, currentPage,
                                            function (p) {
                                                currentPage = parseInt(p);
                                                RechargeTable();
                                            });
               page.ShowSimple();
           });
}

//会员充值记录列表
function MemRechargeList() {
    var html = $("#MemRechargeList").html();
    if (html.indexOf("LoadingBar") != -1) {
        GetMemRechargeList();
    }
}
//获取数据
function GetMemRechargeList() {
    doAjax("../",
           "GetMemRechargeListByPage",
           {
               "memInfo": $("#txtMRMem").val(),
               "type": $("#sltMRType").val(),
               "memShopID": $("#sltMRShop").val(),
               "startTime": $("#txtMRStart").val(),
               "endTime": $("#txtMREnd").val(),
               "orderBy": $("#sltOrderBy").val(),
               "size": pageSize,
               "index": currentPage
           },
           "json",
           function (json) {
               //根据返回的json来拼接充值记录列表               
               var html = '<tr class=\"th\"><th>会员卡号</th><th>会员姓名</th><th>充值总额（含赠送金额）</th><th>充值总金额 </th><th>赠送总金额</th><th style="display:none;">账户余额</th><th>总充值次数</th><th>最后充值时间</th><th>会员所属商家</th></tr>';
               if (json.MemRechargeList.length > 0) {
                   $.each(json.MemRechargeList, function (index, item) {
                       html += "<tr class=\"td\" >"
                       html += '<td align="left">' + item.MemCard + '</td>'
                       html += '<td align="left">' + item.MemName + '</td>'
                       html += '<td align="left" style="color:red;">' + item.RechargeTotalMoney.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left">' + item.TotalMoney.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left">' + item.RechargeTotalGive.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left" style="color:red;display:none;">' + item.MemMoney.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left" style="color:red;">' + item.RechargeCount + '</td>'
                       html += '<td align="left">' + item.LastRechargeTime + '</td>'
                       html += '<td align="left">' + item.ShopName + '</td>'
                       html += '</tr>'
                   });
               }
               else {
                   html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="9">未找到符合此条件的数据！</td></tr>';
               }
               $("#MemRechargeList").html(html);

               var page = new CommonPager("divMemRechargeListPage", pageSize, json.RecordCount, currentPage,
                                            function (p) {
                                                currentPage = parseInt(p);
                                                GetMemRechargeList();
                                            });
               page.ShowSimple();
           });
}

//商家充值记录列表
function ShopRechargeList() {
    var html = $("#ShopRechargeList").html();
    if (html.indexOf("LoadingBar") != -1) {
        GetShopRechargeList();
    }
}
//获取数据
function GetShopRechargeList() {
    doAjax("../",
           "GetShopRechargeListByPage",
           {
               "type": $("#sltSRType").val(),
               "shopID": $("#sltSRShop").val(),
               "startTime": $("#txtSRStart").val(),
               "endTime": $("#txtSREnd").val(),
               "orderBy": $("#sltSROrderBy").val(),
               "size": pageSize,
               "index": currentPage
           },
           "json",
           function (json) {
               //根据返回的json来拼接充值记录列表               
               var html = '<tr class=\"th\"><th>商家名称</th><th>充值总额（含赠送金额）</th><th>充值总金额</th><th>赠送总金额</th><th>总充值次数</th></tr>';
               if (json.ShopRechargeList.length > 0) {
                   $.each(json.ShopRechargeList, function (index, item) {
                       html += "<tr class=\"td\" >"
                       html += '<td align="left">' + item.ShopName + '</td>'
                       html += '<td align="left"  style="color:red;">' + item.RechargeTotalMoney.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left">' + item.TotalMoney.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left">' + item.RechargeTotalGive.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left" style="color:red;">' + item.TotalCount + '</td>'
                       html += '</tr>'
                   });
               }
               else {
                   html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="9">未找到符合此条件的数据！</td></tr>';
               }

               $("#ShopRechargeList").html(html);
               var page = new CommonPager("divShopRechargeListPage", pageSize, json.RecordCount, currentPage,
                                            function (p) {
                                                currentPage = parseInt(p);
                                                GetShopRechargeList();
                                            });
               page.ShowSimple();
           });
}

//充值类型
function GetRechargeType(strType) {
    var strRechargeType = "现金充值";
    switch (strType) {
        case "1":
            strRechargeType = "初始充值";
            break;
        case "2":
            strRechargeType = "现金充值";
            break;
        case "3":
            strRechargeType = "银联充值";
            break;
        case "4":
            strRechargeType = "网上充值";
            break;
        case "5":
            strRechargeType = "撤单退货转充值";
            break;
    }
    return strRechargeType;
}

//会员充值查询按钮
function BtnMRQuery() {
    doAjax("../",
           "GetMemRechargeListByPage",
           {
               "memInfo": $("#txtMRMem").val(),
               "type": $("#sltMRType").val(),
               "memShopID": $("#sltMRShop").val(),
               "startTime": $("#txtMRStart").val(),
               "endTime": $("#txtMREnd").val(),
               "orderBy": $("#sltOrderBy").val(),
               "size": pageSize,
               "index": currentPage
           },
           "json",
           function (json) {
               //根据返回的json来拼接充值记录列表               
               var html = '<tr class=\"th\"><th>会员卡号</th><th>会员姓名</th><th>充值总额（含赠送金额）</th><th>充值总金额 </th><th>赠送总金额</th><th>账户余额</th><th>总充值次数</th><th>最后充值时间</th><th>会员所属商家</th></tr>';
               if (json.MemRechargeList.length > 0) {
                   $.each(json.MemRechargeList, function (index, item) {
                       html += "<tr class=\"td\" >"
                       html += '<td align="left">' + item.MemCard + '</td>'
                       html += '<td align="left">' + item.MemName + '</td>'
                       html += '<td align="left" style="color:red;">' + item.RechargeTotalMoney.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left">' + item.TotalMoney.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left">' + item.RechargeTotalGive.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left" style="color:red;">' + item.MemMoney.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left" style="color:red;">' + item.RechargeCount + '</td>'
                       html += '<td align="left">' + item.LastRechargeTime + '</td>'
                       html += '<td align="left">' + item.ShopName + '</td>'
                       html += '</tr>'
                   });
               }
               else {
                   html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="9">未找到符合此条件的数据！</td></tr>';
               }
               $("#MemRechargeList").html(html);
           });
}

//商家充值查询按钮
function BtnSRQuery() {
    doAjax("../",
           "GetShopRechargeListByPage",
           {
               "type": $("#sltSRType").val(),
               "shopID": $("#sltSRShop").val(),
               "startTime": $("#txtSRStart").val(),
               "endTime": $("#txtSREnd").val(),
               "orderBy": $("#sltSROrderBy").val(),
               "size": pageSize,
               "index": currentPage
           },
           "json",
           function (json) {
               //根据返回的json来拼接充值记录列表               
               var html = '<tr class=\"th\"><th>商家名称</th><th>充值总额（含赠送金额）</th><th>充值总金额</th><th>赠送总金额</th><th>总充值次数</th></tr>';
               if (json.ShopRechargeList.length > 0) {
                   $.each(json.ShopRechargeList, function (index, item) {
                       html += "<tr class=\"td\" >"
                       html += '<td align="left">' + item.ShopName + '</td>'
                       html += '<td align="left"  style="color:red;">' + item.RechargeTotalMoney.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left">' + item.TotalMoney.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left">' + item.RechargeTotalGive.ToFloat().toFixed(2) + '</td>'
                       html += '<td align="left" style="color:red;">' + item.TotalCount + '</td>'
                       html += '</tr>'
                   });
               }
               else {
                   html += '<tr class=\"td\"><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="9">未找到符合此条件的数据！</td></tr>';
               }

               $("#ShopRechargeList").html(html);
           });
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
    $("#btSerch").click();
    art.dialog({
        lock: true,
        title: '会员充值统计图',
        width: '800px',
        content: document.getElementById('ChartShow'),
        id: 'ChartOfMemRecharge',
        close: function () { }
    });
}
function btQuery() {
    var strErrorMsg = "";
    if (!$("#txtMoney").val().IsDecimal() && $("#txtMoney").val() != "") {
        strErrorMsg += "<li>输入充值金额格式不正确,请重新输入!</li>";
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
function bindchart(starttime, endtime) {
    doAjax(
                "../",
                "GetRecharge",
                {
                    "StartTime": starttime,
                    "EndTime": endtime,
                    "ShopID": $("#sltShop").val()
                },
                "json",
                function (json) {
                    if (json != "") {
                        var Interval = json.Interval.toString().split(",");
                        var arrayObj1 = json.Mydata.toString().split(",");    //总共充值
                        var arrayObj2 = json.MyCshcz.toString().split(",");   //初始化充值
                        var arrayObj3 = json.MyPayBink.toString().split(","); //银联充值
                        var arrayObj4 = json.MyPayCash.toString().split(","); //现金充值
                        var Mydata = strtofloat(arrayObj1);
                        var MyCshcz = strtofloat(arrayObj2);
                        var MyPayBink = strtofloat(arrayObj3);
                        var MyPayCash = strtofloat(arrayObj4);
                        var series = [{ name: "初始充值", data: MyCshcz }, { name: "现金充值", data: MyPayCash }, { name: "银联充值", data: MyPayBink }, { name: '总共充值', data: Mydata}];
                        LodaChartData('spline', '会员充值统计图', '充值 (元)', Interval, series);
                    }
                });
}
//打印
function againPoint(rechargeID) {

    //获取打印的份数
    var PointNum = $("#PointNum").val();

    doAjax("../", "AgainPrintMemRecharge", { "rechargeID": rechargeID }, "json", function (json) {
        var row = json[0];
        var lblPrintTitle = $("#lblPrintTitle").html();
        var lblAccount = row.RechargeAccount;
        var txtMoney = row.RechargeMoney;
        var txtGiveMoney = row.RechargeGive;
        var userName = row.UserName;
        var getDataTimeNow = row.RechargeCreateTime;
        var lblPrintFoot = $("#lblPrintFoot").html();
        var print = true;
        var printCount = 2;
        var rechargeCardBalance = row.RechargeCardBalance;
        Print_RechageMoneyAgain_Point(lblPrintTitle, lblAccount, row, txtMoney, txtGiveMoney, userName, getDataTimeNow, lblPrintFoot, print, printCount, rechargeCardBalance, PointNum);
    })
}
