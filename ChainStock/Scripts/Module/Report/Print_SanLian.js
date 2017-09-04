var PrintPreview = SysParameter().PrintPreview;


//快速消费
function Print_Expense_Point(UserName, global_mem, print, type, PointNum) {
    if (print.toString() == "true") {
        var LODOP;
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<div style=\"width:200mm; margin-left:10mm;\">";
        htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
        htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
        htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">快速消费</div>";
        htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
        htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">消费单号：" + $("#lblOrderAccount").html() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + global_mem.MemName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
        htmlPrint += "<div style=\"float: left; width: 33%;\">消费时间：" + $("#lblOrderCreateTime").html() + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"height: auto; width: 100%;\">";
        htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: auto; width: 100%;\">";
        htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">会员卡号</td><td style=\"width: 20%; margin-top: 2mm;\">消费金额</td><td style=\"width: 20%; margin-top: 2mm;\">获得积分</td>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">会员总积分</td><td style=\"width: 20%; margin-top: 2mm;\">卡内余额</td></tr>";
        htmlPrint += "<tr>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + global_mem.MemCard + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + $("#txtDiscountMoney").val() + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + $("#txtExpPoint").val().toString() + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + $("#txtExpPoint").val().toString() + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(parseFloat(global_mem.MemMoney) - parseFloat(parameter[0].CardMoney)).toFixed(2) + "</td>"
        htmlPrint += "</tr>";
        htmlPrint += "</table>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 2px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + UserName + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">客户签名：</div>";
        htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
        htmlPrint += "</div></div></div>";
        LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES($("#PointNum").val()); //设置打印的份数
        LODOP.PRINT();
        location.reload();
    }
    else {
        if ($("#txtIsTimeExpense").val() == "1") {
            window.location.href = "../MemExpense/TimeList.aspx?PID=87";
        }
        else {
            window.location.href = "../MemExpense/Expense.aspx?PID=17";
        }
        if ($("#txtIsTimeExpense").val() == "1") {
            window.location.href = "../MemExpense/TimeList.aspx?PID=87";
        }
        else {
            window.location.href = "../MemExpense/Expense.aspx?PID=17";
        }
    }
}

//计时消费
function TimeExpenPrint(UserName, MemCard, MemName, Point, ProjectName, ExpenseTime, syTime, OrderAount, datetime) {
    var LODOP;
    var htmlPrint;
    LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
    htmlPrint = "<div style=\"width:200mm; margin-left:10mm;\">";
    htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
    htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
    htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">计时消费</div>";
    htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
    htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">消费单号：" + OrderAount + "</div>";
    htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
    htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + MemName + "</div>";
    htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
    htmlPrint += "<div style=\"float: left; width: 33%;\">消费时间：" + datetime + "</div>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\"height: auto; width: 100%;\">";
    htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: auto; width: 100%;\">";
    htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">会员卡号</td><td style=\"width: 20%; margin-top: 2mm;\">消费服务</td><td style=\"width: 20%; margin-top: 2mm;\">消费总时长</td>";
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">剩余时长</td><td style=\"width: 20%; margin-top: 2mm;\">会员总积分</td></tr>";
    htmlPrint += "<tr>";
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + MemCard + "</td>"
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + ProjectName + "</td>"
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + ExpenseTime + "分钟" + "</td>"
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + syTime + "分钟" + "</td>"
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + Point + "</td>"
    htmlPrint += "</tr>";
    htmlPrint += "</table>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 2px;\">";
    htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + UserName + "</div>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">客户签名：</div>";
    htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
    htmlPrint += "</div></div></div>";
    LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
    if (PrintPreview == 1) {//等于1表示 启用打印预览
        LODOP.PREVIEW(); //打印预览
    }
    LODOP.PRINT();
    window.location.href = "../MemExpense/TimeList.aspx?PID=87";
}

//会员充时消费
function Print_MemStorageExpense(UserName, global_mem, print, time, PointNum) {
    var a = $("#lblOrderAccount").html();
    if (print.toString() == "true") {
        var LODOP;
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<div style=\"width:200mm; margin-left:10mm;\">";
        htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
        htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
        htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">会员充时消费</div>";
        htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
        htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">消费单号：" + $("#lblOrderAccount").html() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + global_mem.MemName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
        htmlPrint += "<div style=\"float: left; width: 33%;\">消费时间：" + $("#lblOrderCreateTime").html() + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"height: auto; width: 100%;\">";
        htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: auto; width: 100%;\">";
        htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">会员卡号</td><td style=\"width: 20%; margin-top: 2mm;\">消费金额</td><td style=\"width: 20%; margin-top: 2mm;\">获得积分</td>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">会员总积分</td><td style=\"width: 20%; margin-top: 2mm;\">卡内余额</td></tr>";
        htmlPrint += "<tr>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + global_mem.MemCard + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + $("#txtDiscountMoney").val() + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + $("#txtExpPoint").val().toString() + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + (parseFloat(global_mem.MemPoint) + parseFloat($("#txtExpPoint").val())).toString() + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(parseFloat(global_mem.MemMoney) - parseFloat(parameter[0].CardMoney)).toFixed(2) + "</td>"
        htmlPrint += "</tr>";
        htmlPrint += "</table>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 2px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + UserName + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">客户签名：</div>";
        htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
        htmlPrint += "</div></div></div>";
        LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        LODOP.PRINT();
        location.reload();
    }
    else {
        location.reload();
    }
}
//会员充值
function Print_RechageMoney_Point(UserName, global_mem, print, PointNum) {
    if (print.toString() == "true") {
        var LODOP;
        var htmlPrint;
        //小票打印
        var LODOP; //声明为全局变量
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<div style=\"width:200mm; margin-left:10mm;\">";
        htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
        htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
        htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">会员充值</div>";
        htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
        htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">充值单号：" + $("#lblAccount").html() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + global_mem.MemName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
        htmlPrint += "<div style=\"float: left; width: 33%;\">充值时间：" + $("#lblRechargeTime").html() + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"height: auto; width: 100%;\">";
        htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: auto; width: 100%;\">";
        htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">会员卡号</td><td style=\"width: 20%; margin-top: 2mm;\">充值金额</td><td style=\"width: 20%; margin-top: 2mm;\">赠送金额</td>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">充值总金额</td><td style=\"width: 20%; margin-top: 2mm;\">卡内余额</td></tr>";
        htmlPrint += "<tr>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + global_mem.MemCard + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat($.trim($("#txtMoney").val())).toFixed(2) + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat($.trim($("#txtGiveMoney").val())).toFixed(2) + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + (parseFloat($.trim($("#txtMoney").val())) + parseFloat($.trim($("#txtGiveMoney").val()))).toFixed(2) + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + (parseFloat(global_mem.MemMoney) + parseFloat($.trim($("#txtMoney").val())) + parseFloat($.trim($("#txtGiveMoney").val()))).toFixed(2) + "</td>"
        htmlPrint += "</tr>";
        htmlPrint += "</table>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 2px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + UserName + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">客户签名：</div>";
        htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
        htmlPrint += "</div></div></div>";
        LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        LODOP.PRINT();
        location.reload();
    }
}

//会员充次
function Print_MemCount(strUserName, lblPrintTitle, lblPrintFoot, GoodsList, parameter, bolPrint, PointNum) {
    if (bolPrint != null && bolPrint.toString() == "true") {
        //小票打印
        var LODOP; //声明为全局变量
        var htmlPrint;
        var DivHeight;
        DivHeight = (GoodsList.length) * 5 + 20;
        var MainHeight = Number(DivHeight) + 80;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<div style=\"width:200mm; margin-left:10mm;    hight:" + MainHeight + "mm; margin-left:10mm; overflow:hidden;  \" >";
        htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
        htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
        htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">会员充次</div>";
        htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
        htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">充次单号：" + $("#spOrderCode").html() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + global_mem.MemName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
        htmlPrint += "<div style=\"float: left; width: 33%;\">消费时间：" + GetDataTimeNow() + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\" height:" + DivHeight + "mm; overflow:hidden; width: 100%;\">";
        htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: " + DivHeight + "; width: 100%;\">";
        htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">项目名称</td><td style=\"width: 20%; margin-top: 2mm;\">项目编码</td><td style=\"width: 20%; margin-top: 2mm;\">次数</td>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">消费金额</td><td style=\"width: 20%; margin-top: 2mm;\">获得积分</td></tr>";
        for (var i = 0; i < GoodsList.length; i++) {
            htmlPrint += "<tr>";
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GoodsList[i]["Name"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GoodsList[i]["GoodsCode"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GoodsList[i]["ExpNum"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(GoodsList[i]["ExpMoney"]).toFixed(2) + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GoodsList[i]["ExpPoint"] + "</td>"
            htmlPrint += "</tr>";
        }
        htmlPrint += "</table>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 10px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + strUserName + "</div>";
        //        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">消费总金额：" + parseFloat(totalDiscount).toFixed(2) + "</div>";
        //        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">获得积分：" + totalPoint + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">备注：" + $("#tdRemark").val() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
        htmlPrint += "</div></div></div>";
        LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        LODOP.PRINT();
        location.reload();
    }
    else {
        location.reload();
    }
}


//会员兑换礼品
function Print_GiftExchange_Point(UserName, global_mem, GiftList, sumPoint, sumNumber, print, PointNum) {
    if (print.toString() == "true") {
        //小票打印
        var LODOP; //声明为全局变量
        var htmlPrint;
        var DivHeight;
        DivHeight = (GiftList.length) * 5 +20;
        var MainHeight = Number(DivHeight) + 80;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<div style=\"width:200mm; margin-left:10mm;    hight:" + MainHeight + "mm; margin-left:10mm; overflow:hidden;  \" >";
        htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
        htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
        htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">会员积分兑换</div>";
        htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
        htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">兑换单号：" + $("#lblExchangePrefix").html() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + global_mem.MemName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员卡号：" + global_mem.MemCard + "</div>"
        htmlPrint += "<div style=\"float: left; width: 33%;\">兑换时间：" + $("#lblExchangeTime").html() + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\" height:'" + DivHeight + "'mm; overflow:hidden; width: 100%;\">";
        htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: " + DivHeight + "; width: 100%;\">";
        htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">礼品名称</td><td style=\"width: 20%; margin-top: 2mm;\">礼品简码</td><td style=\"width: 20%; margin-top: 2mm;\">数量</td><td style=\"width: 20%; margin-top: 2mm;\">积分</td>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">合计</td></tr>";
        for (var i = 0; i < GiftList.length; i++) {
            htmlPrint += "<tr>";
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GiftList[i]["GiftName"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GiftList[i]["GiftCode"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GiftList[i]["ExcNumber"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GiftList[i]["GiftExchangePoint"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + (parseFloat(GiftList[i]["ExcNumber"].toString()) * parseFloat(GiftList[i]["GiftExchangePoint"].toString())) + "</td>"
            htmlPrint += "</tr>";
        }
        htmlPrint += "</table>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 10px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + UserName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">兑换数量：" + sumNumber + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">扣减积分：" + sumPoint + "</div>";
        htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "</div></div></div>";
        LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        LODOP.PRINT();
        location.reload();
    }
    else {
        location.reload();
    }
}

//积分变动打印小票
function Print_PointChange(userName, global_mem, exchangeType, changeNumber, lblPrintTitle, lblPrintFoot, pointChangeTime, print, type, PointNum) {
    if (print) {
        var LODOP;
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<div style=\"width:200mm; margin-left:10mm;\">";
        htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
        htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
        htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">会员积分变动</div>";
        htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + global_mem.MemName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
        htmlPrint += "<div style=\"float: left; width: 33%;\">变动时间：" + pointChangeTime + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"height: auto; width: 100%;\">";
        htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: auto; width: 100%;\">";
        htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">会员卡号</td><td style=\"width: 20%; margin-top: 2mm;\">操作员</td><td style=\"width: 20%; margin-top: 2mm;\">异动积分</td>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">剩余总积分</td></tr>";
        htmlPrint += "<tr>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + global_mem.MemCard + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + userName + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + (exchangeType == "0" ? "增加" : "扣减") + "积分：" + Math.abs(parseFloat(changeNumber)) + "</td>"
        if (type == 1) {
            if (exchangeType == "0") {
                htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + (parseFloat(global_mem.MemPoint) + parseFloat(changeNumber)) + "</td>";
            } else {
                htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + (parseFloat(global_mem.MemPoint) - parseFloat(changeNumber)) + "</td>";
            }
        }
        else {
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(global_mem.MemPoint) + "</td>";
        }
        htmlPrint += "</tr>";

        htmlPrint += "</table>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 2px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + userName + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">客户签名：</div>";
        htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
        htmlPrint += "</div></div></div>";
        LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        LODOP.PRINT();
        location.reload();
    }
    else {
        location.reload();
    }
}

//商品消费打印小票
function PrintGoodsExpense(userName, global_mem, GoodsList, parameter, print, PointNum) {
    if (print != null && print.toString() == "true") {
        var isVIP = !$("#chkNoMember").attr("checked");
        //小票打印
        var LODOP; //声明为全局变量
        var htmlPrint;
        var DivHeight;
        DivHeight = (GoodsList.length) * 5 +20;
        var MainHeight = Number(DivHeight) + 80;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<div style=\"width:200mm; margin-left:10mm;    hight:" + MainHeight + "mm; margin-left:10mm; overflow:hidden;  \" >";
        htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
        htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
        htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">商品消费</div>";
        htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
        htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">消费单号：" + $("#spOrderAccount").html() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + (isVIP ? global_mem.MemName : "散客") + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
        htmlPrint += "<div style=\"float: left; width: 33%;\">消费时间：" + $("#txtExpenseTime").val() + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\" height:" + DivHeight + "mm; overflow:hidden; width: 100%;\">";
        htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: " + DivHeight + "; width: 100%;\">";
        htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">商品名称</td><td style=\"width: 20%; margin-top: 2mm;\">商品编码</td>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">单价</td><td style=\"width: 20%; margin-top: 2mm;\">数量</td><td style=\"width: 20%; margin-top: 2mm;\">合计</td></tr>";
        for (var i = 0; i < GoodsList.length; i++) {
            htmlPrint += "<tr>";
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GoodsList[i]["Name"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GoodsList[i]["GoodsCode"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(GoodsList[i]["Price"]).toFixed(2) + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GoodsList[i]["ExpNum"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(GoodsList[i]["ExpMoney"]).toFixed(2) + "</td>"
            htmlPrint += "</tr>";
        }
        htmlPrint += "</table>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 2px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + userName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">消费总金额：" + parseFloat(totalDiscount).toFixed(2) + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">获得积分：" + (isVIP ? totalPoint : "0") + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">备注：" + $("#txtExRemark").val() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
        htmlPrint += "</div></div></div>";
        LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES($("#PointNum").val()); //设置打印的份数
        LODOP.PRINT();
        window.location.href = "../StockManage/GoodsExpense.aspx?PID=67";
    }
    else {
        window.location.href = "../StockManage/GoodsExpense.aspx?PID=67";
    }
}


//计次消费打印小票
function PrintMemCountExpense(userName, global_mem, GoodsList, parameter, print, PointNum) {
    if (print != null && print.toString() == "true") {
        var isVIP = !$("#chkNoMember").attr("checked");
        //小票打印
        var LODOP; //声明为全局变量
        var htmlPrint;
        var DivHeight;
        DivHeight = (GoodsList.length) * 5 +20;
        var MainHeight = Number(DivHeight) + 80;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<div style=\"width:200mm; margin-left:10mm;    hight:" + MainHeight + "mm; margin-left:10mm; overflow:hidden;  \" >";
        htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
        htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
        htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">计次消费</div>";
        htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
        htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">消费单号：" + $("#spOrderAccount").html() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + (isVIP ? global_mem.MemName : "散客") + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
        htmlPrint += "<div style=\"float: left; width: 33%;\">消费时间：" + $("#txtExpenseTime").val() + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\" height:" + DivHeight + "mm; overflow:hidden; width: 100%;\">";
        htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: " + DivHeight + "; width: 100%;\">";
        htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">项目名称</td><td style=\"width: 20%; margin-top: 2mm;\">项目编码</td>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">单价</td><td style=\"width: 20%; margin-top: 2mm;\">次数</td><td style=\"width: 20%; margin-top: 2mm;\">合计</td></tr>";
        for (var i = 0; i < GoodsList.length; i++) {
            htmlPrint += "<tr>";
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GoodsList[i]["Name"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GoodsList[i]["GoodsCode"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(GoodsList[i]["Price"]).toFixed(2) + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GoodsList[i]["ExpNum"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(GoodsList[i]["ExpMoney"]).toFixed(2) + "</td>"
            htmlPrint += "</tr>";
        }
        htmlPrint += "</table>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 10px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + userName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">消费总金额：" + parseFloat(totalDiscount).toFixed(2) + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">获得积分：" + (isVIP ? totalPoint : "0") + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">备注：" + $("#txtExRemark").val() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
        htmlPrint += "</div></div></div>";
        LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        LODOP.PRINT();
        location.reload();
    }
    else {
        location.reload();
    }
}

//二次打印快速消费
function SecondExpensePrint(UserName, mem, expense) {
    var LODOP;
    var htmlPrint;
    LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
    htmlPrint = "<div style=\"width:200mm; margin-left:10mm;\">";
    htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
    htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
    htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">快速消费</div>";
    htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
    htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">消费单号：" + expense.OrderAccount + "</div>";
    htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
    htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + mem.MemName + "</div>";
    htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
    htmlPrint += "<div style=\"float: left; width: 33%;\">消费时间：" + expense.OrderCreateTime + "</div>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\"height: auto; width: 100%;\">";
    htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: auto; width: 100%;\">";
    htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">会员卡号</td><td style=\"width: 20%; margin-top: 2mm;\">消费金额</td><td style=\"width: 20%; margin-top: 2mm;\">获得积分</td>";
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">会员总积分</td><td style=\"width: 20%; margin-top: 2mm;\">卡内余额</td></tr>";
    htmlPrint += "<tr>";
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + mem.MemCard + "</td>"
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + expense.OrderDiscountMoney.ToNumber().toFixed(2) + "</td>"
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + expense.OrderPoint + "</td>"
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + (parseFloat(expense.OrderCardPoint)).toString() + "</td>"
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + (parseFloat(expense.OrderCardBalance)).toFixed(2) + "</td>"
    htmlPrint += "</tr>";
    htmlPrint += "</table>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 2px;\">";
    htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + UserName + "</div>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">客户签名：</div>";
    htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
    htmlPrint += "</div></div></div>";
    LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
    if (PrintPreview == 1) {//等于1表示 启用打印预览
        LODOP.PREVIEW(); //打印预览
    }
    LODOP.PRINT();
    window.location.href = "../MemExpense/ExpenseHistory.aspx?PID=18";
}
//商品消费二次打印小票
function SecondGoodsExpensePrint(userName, mem, expense, detail) {
    var isVIP = (mem.MemID != 0 ? true : false);
    //小票打印
    var LODOP; //声明为全局变量
    var htmlPrint;
    var DivHeight;
    DivHeight = (detail.length) * 5 +  20;
    var MainHeight = Number(DivHeight) + 80;
    alert(MainHeight);
    LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
    htmlPrint = "<div id='PrintDiv' style=\"width:200mm; margin-left:10mm;    hight:" + MainHeight + "mm; margin-left:10mm; overflow:hidden;  \" >";
    htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
    htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
    htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">商品消费</div>";
    htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
    htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">消费单号：" + expense.OrderAccount + "</div>";
    htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
    htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + (isVIP ? mem.MemName : "散客") + "</div>";
    htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
    htmlPrint += "<div style=\"float: left; width: 33%;\">消费时间：" + expense.OrderCreateTime + "</div>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\" height:" + DivHeight + "mm; overflow:hidden; width: 100%;\">";
    htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: " + DivHeight + "; width: 100%;\">";
    htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">商品名称</td><td style=\"width: 20%; margin-top: 2mm;\">商品编码</td>";
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">单价</td><td style=\"width: 20%; margin-top: 2mm;\">数量</td><td style=\"width: 20%; margin-top: 2mm;\">合计</td></tr>";
    for (var i = 0; i < detail.length; i++) {
        htmlPrint += "<tr>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + detail[i]["Name"] + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + detail[i]["GoodsCode"].toString() + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(detail[i]["OrderDetailPrice"]).toFixed(2) + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + detail[i]["OrderDetailNumber"] + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(detail[i]["OrderDetailDiscountPrice"]).toFixed(2) + "</td>"
        htmlPrint += "</tr>";
    }
    htmlPrint += "</table>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 10px;\">";
    htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + userName + "</div>";
    htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">消费总金额：" + expense.OrderDiscountMoney.ToNumber().toFixed(2) + "</div>";
    htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">获得积分：" + (isVIP ? expense.OrderPoint : "0") + "</div>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">备注：" + detail[0].OrderRemark + "</div>";
    htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
    htmlPrint += "</div></div></div>";

   
    LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
    if (PrintPreview == 1) {//等于1表示 启用打印预览
        LODOP.PREVIEW(); //打印预览
    }
    LODOP.PRINT();
    location.reload();
}

//会员计次消费
function SecondMemCountExpensePrint(userName, mem, expense, detail, PointNum) {
    var isVIP = (mem.MemID != 0 ? true : false);
    //小票打印
    var LODOP; //声明为全局变量
    var htmlPrint;
    var DivHeight;
    DivHeight = (detail.length) * 5 + 20;
    var MainHeight = Number(DivHeight) + 80;
    LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
    htmlPrint = "<div style=\"width:200mm; margin-left:10mm;    hight:" + MainHeight + "mm; margin-left:10mm; overflow:hidden;  \" >";
    htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
    htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
    htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">会员计次消费</div>";
    htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
    htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">消费单号：" + expense.OrderAccount + "</div>";
    htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
    htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + (isVIP ? mem.MemName : "散客") + "</div>";
    htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
    htmlPrint += "<div style=\"float: left; width: 33%;\">消费时间：" + expense.OrderCreateTime + "</div>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\" height:" + DivHeight + "mm; overflow:hidden; width: 100%;\">";
    htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: " + DivHeight + "; width: 100%;\">";
    htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">项目名称</td><td style=\"width: 20%; margin-top: 2mm;\">项目编码</td>";
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">单价</td><td style=\"width: 20%; margin-top: 2mm;\">次数</td><td style=\"width: 20%; margin-top: 2mm;\">合计</td></tr>";
    for (var i = 0; i < detail.length; i++) {
        htmlPrint += "<tr>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + detail[i]["Name"] + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + detail[i]["GoodsCode"].toString() + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(detail[i]["OrderDetailPrice"]).toFixed(2) + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + detail[i]["OrderDetailNumber"] + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(detail[i]["OrderDetailDiscountPrice"]).toFixed(2) + "</td>"
        htmlPrint += "</tr>";
    }
    htmlPrint += "</table>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 10px;\">";
    htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + userName + "</div>";
    htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">消费总金额：" + expense.OrderDiscountMoney.ToNumber().toFixed(2) + "</div>";
    htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">获得积分：" + (isVIP ? expense.OrderPoint : "0") + "</div>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">备注：" + detail[0].OrderRemark + "</div>";
    htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
    htmlPrint += "</div></div></div>";
    LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
    if (PrintPreview == 1) {//等于1表示 启用打印预览
        LODOP.PREVIEW(); //打印预览
    }
    LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
    LODOP.PRINT();
    location.reload();
}


function PayType(parameter) {
    var strPayInfo = "";
    //现金付款 算出找零的金额 
    if (parameter.OrderIsCash || parameter.OrderIsCard || parameter.OrderIsBink) {
        if (parameter.OrderIsCard == "True") {
            strPayInfo += "余额付款：" + parseFloat(parameter.OrderPayCard).toFixed(2) + "<br>";
        }
        if (parameter.OrderIsCash == "True") {
            strPayInfo += "现金付款：" + parseFloat(parameter.OrderPayCash).toFixed(2) + "<br>";
        }
        if (parameter.OrderIsBink == "True") {
            strPayInfo += "银联付款：" + parseFloat(parameter.OrderPayBink).toFixed(2) + "<br>"
        }
        if (parameter.OrderPayCoupon != "0.0000" && parameter.OrderPayCoupon != "undefined") {
            strPayInfo += "优惠券优惠：" + parseFloat(parameter.OrderPayCoupon).toFixed(2) + "<br>";
        }
    }
    return strPayInfo;
}
//入库打印小票
function PrintGoodsIn(userName, shopName, GoodsList, print, PointNum) {
    if (print != null && print.toString() == "true") {
        //小票打印
        var LODOP; //声明为全局变量
        var htmlPrint;
        var DivHeight;
        DivHeight = (GoodsList.length+1) * 5+20;
        var MainHeight = Number(DivHeight) + 80;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<div style=\"width:200mm; margin-left:10mm;    hight:" + MainHeight + "mm; margin-left:10mm; overflow:hidden;  \" >";
        htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
        htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
        htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">商品入库</div>";
        htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
        htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">入库单号：" + $("#spGoodsAccounte").html() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%;\">入库商家：" + shopName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
        htmlPrint += "<div style=\"float: left; width: 33%;\">入库时间：" + $("#txtCreteTime").val() + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\" height:" + DivHeight + "mm; overflow:hidden; width: 100%;\">";
        htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: " + DivHeight + "; width: 100%;\">";
        htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">商品名称</td><td style=\"width: 20%; margin-top: 2mm;\">商品编码</td>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">单价</td><td style=\"width: 20%; margin-top: 2mm;\">数量</td><td style=\"width: 20%; margin-top: 2mm;\">合计</td></tr>";
        for (var i = 0; i < GoodsList.length; i++) {
            htmlPrint += "<tr>";
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GoodsList[i]["Name"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GoodsList[i]["GoodsCode"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(GoodsList[i]["InMoney"]).toFixed(2) + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GoodsList[i]["Number"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(GoodsList[i]["TotalMoney"]).toFixed(2) + "</td>"
            htmlPrint += "</tr>";
        }
        htmlPrint += "</table>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 10px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">入库数量：" + totalNumber + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">入库总额：" + parseFloat(totalMoney).toFixed(2) + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">操作员：" + userName + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">备注：" + $("#txtExRemark").val() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
        htmlPrint += "</div></div></div>";
        LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        LODOP.PRINT();
        location.reload();
    }
    else {
        location.reload();
    }
}
//商品出入库明细打印小票
function PrintGoodsLog(userName, printtitle, printfoot, goodslog, logdetail, PointNum) {
    //小票打印
    var LODOP; //声明为全局变量
    var htmlPrint;
    var DivHeight;
    DivHeight = (logdetail.length+1) * 5 + 20;
    var MainHeight = Number(DivHeight) + 80;
    LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
    htmlPrint = "<div style=\"width:200mm; margin-left:10mm;    hight:" + MainHeight + "mm; margin-left:10mm; overflow:hidden;  \" >";
    htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
    htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
    htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">商品出入库小票</div>";
    htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
    htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">出入库单号：" + goodslog.GoodsAccount + "</div>";
    htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
    htmlPrint += "<div style=\"float: left; width: 33%;\">出入库方式：" + GetGoodsLgotype(goodslog.Type) + "</div>";
    htmlPrint += "<div style=\"float: left; width: 33%;\"></div>";
    htmlPrint += "<div style=\"float: left; width: 33%;\">异动商家：" + goodslog.ChangeShopName + "</div>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\" height:" + DivHeight + "mm; overflow:hidden; width: 100%;\">";
    htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: " + DivHeight + "; width: 100%;\">";
    htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">商品名称</td><td style=\"width: 20%; margin-top: 2mm;\">商品编码</td>";
    htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">入库价</td><td style=\"width: 20%; margin-top: 2mm;\">出库价</td><td style=\"width: 20%; margin-top: 2mm;\">数量</td></tr>";
    for (var i = 0; i < logdetail.length; i++) {
        htmlPrint += "<tr>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + logdetail[i]["Name"] + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + logdetail[i]["GoodsCode"] + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(logdetail[i]["GoodsInPrice"]).toFixed(2) + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(logdetail[i]["GoodsOutPrice"]).toFixed(2) + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + Math.abs(logdetail[i]["GoodsNumber"]) + "</td>"
        htmlPrint += "</tr>";
    }
    htmlPrint += "</table>";
    htmlPrint += "</div>";
    htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 10px;\">";
    htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作人员：" + userName + "</div>";
    htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">总金额：" + parseFloat(goodslog.TotalPrice).toFixed(2) + "</div>";
    htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">出入库时间：" + goodslog.CreateTime + "</div>";
    htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
    htmlPrint += "</div>";
    htmlPrint += "</div></div></div>";
    LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
    if (PrintPreview == 1) {//等于1表示 启用打印预览
        LODOP.PREVIEW(); //打印预览
    }
    LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
    LODOP.PRINT();
    location.reload();
}

//会员充值 txtMoney(充值金额) txtGiveMoney（赠送金额） printCount（第几次打印，大于1代表再次打印）
//会员充值 txtMoney(充值金额) txtGiveMoney（赠送金额） printCount（第几次打印，大于1代表再次打印）rechargeCardBalance（再次打印时的卡内余额）
function Print_RechageMoneyAgain_Point(lblPrintTitle, lblAccount, global_mem, txtMoney, txtGiveMoney, userName, getDataTimeNow, lblPrintFoot, print, printCount, rechargeCardBalance, PointNum) {
    if (print) {
        var LODOP;
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<div style=\"width:200mm; margin-left:10mm;\">";
        htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
        htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
        htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">会员充值</div>";
        htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
        htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">充值单号：" + lblAccount + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + global_mem.MemName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
        htmlPrint += "<div style=\"float: left; width: 33%;\">充值时间：" + getDataTimeNow + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"height: auto; width: 100%;\">";
        htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: auto; width: 100%;\">";
        htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">会员卡号</td><td style=\"width: 20%; margin-top: 2mm;\">充值金额</td><td style=\"width: 20%; margin-top: 2mm;\">赠送金额</td>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">充值总金额</td><td style=\"width: 20%; margin-top: 2mm;\">卡内余额</td></tr>";
        if (printCount == "1") {
            htmlPrint += "<tr>";
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + global_mem.MemCard + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(txtMoney).toFixed(2) + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(txtGiveMoney).toFixed(2) + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(parseFloat(txtMoney) + parseFloat(txtGiveMoney)).toFixed(2) + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(parseFloat(global_mem.MemMoney) + parseFloat(txtMoney) + parseFloat(txtGiveMoney)).toFixed(2) + "</td>"
            htmlPrint += "</tr>";
        }
        else {
            htmlPrint += "<tr>";
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + global_mem.MemCard + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(parseFloat(txtMoney) - parseFloat(txtGiveMoney)).toFixed(2) + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(txtGiveMoney).toFixed(2) + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(txtMoney).toFixed(2) + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(rechargeCardBalance).toFixed(2) + "</td>"
            htmlPrint += "</tr>";
        }
        htmlPrint += "</table>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 2px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + userName + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">客户签名：</div>";
        htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
        htmlPrint += "</div></div></div>";
        LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        LODOP.PRINT();
        location.reload();
    }
    location.reload();
}

//积分变动 exchangeType（0：增加积分 1：扣减积分）printCount（第几次打印，大于1代表再次打印）
function Print_PointChangeAgain(lblPrintTitle, global_mem, exchangeType, changeNumber, printCount, userName, getDataTimeNow, lblPrintFoot, print, PointNum, PointRemark) {
    if (print) {
        var LODOP;
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<div style=\"width:200mm; margin-left:10mm;\">";
        htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
        htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
        htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">会员积分变动</div>";
        htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + global_mem.MemName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
        htmlPrint += "<div style=\"float: left; width: 33%;\">变动时间：" + getDataTimeNow + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"height: auto; width: 100%;\">";
        htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: auto; width: 100%;\">";
        htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">会员卡号</td><td style=\"width: 20%; margin-top: 2mm;\">操作员</td><td style=\"width: 20%; margin-top: 2mm;\">异动积分</td>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">剩余总积分</td></tr>";
        htmlPrint += "<tr>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + global_mem.MemCard + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + userName + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + (changeNumber.substr(0, 1) == "-" ? "扣减" : "增加") + "积分：" + Math.abs(parseFloat(changeNumber)) + "</td>"
        if (type == 1) {
            if (exchangeType == "0") {
                htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + (parseFloat(global_mem.MemPoint) + parseFloat(changeNumber)) + "</td>";
            } else {
                htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + (parseFloat(global_mem.MemPoint) - parseFloat(changeNumber)) + "</td>";
            }
        }
        else {
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(global_mem.MemPoint) + "</td>";
        }
        htmlPrint += "</tr>";
        htmlPrint += "</table>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 2px;\">";
        htmlPrint += "<div style=\"float: left; width: 100%; line-height: 25px;\">异动详情：" + PointRemark + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + userName + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">客户签名：</div>";
        htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
        htmlPrint += "</div></div></div>";
        LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        LODOP.PRINT();
    }
    else {
        location.reload();
    }
}

//礼品兑换 sumNumber（兑换总量） GiftList（兑换礼品信息列表）
function Print_GiftExchangeAgain_Point(lblPrintTitle, global_mem, sumNumber, sumPoint, GiftList, userName, getDataTimeNow, lblPrintFoot, print, printCount, exchangeAccount, PointNum) {
    if (print) {
        //小票打印
        var LODOP; //声明为全局变量
        var htmlPrint;
        var DivHeight;
        DivHeight = (GiftList.length) * 7 +20;
        var MainHeight = Number(DivHeight) + 80;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<div style=\"width:200mm; margin-left:10mm;    hight:" + MainHeight + "mm; margin-left:10mm; overflow:hidden;  \" >";
        htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
        htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
        htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">会员积分兑换</div>";
        htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
        htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">兑换单号：" + exchangeAccount + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + global_mem.MemName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员卡号：" + global_mem.MemCard + "</div>"
        htmlPrint += "<div style=\"float: left; width: 33%;\">兑换时间：" + getDataTimeNow + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\" height:" + DivHeight + "mm; overflow:hidden; width: 100%;\">";
        htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: " + DivHeight + "; width: 100%;\">";
        htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">礼品名称</td><td style=\"width: 20%; margin-top: 2mm;\">礼品简码</td><td style=\"width: 20%; margin-top: 2mm;\">数量</td><td style=\"width: 20%; margin-top: 2mm;\">积分</td>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">合计</td></tr>";
        for (var i = 0; i < GiftList.length; i++) {
            htmlPrint += "<tr>";
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GiftList[i]["GiftName"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GiftList[i]["GiftCode"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GiftList[i]["ExchangeNumber"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GiftList[i]["GiftExchangePoint"] + "</td>"
            htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + GiftList[i]["ExchangePoint"] + "</td>"
            htmlPrint += "</tr>";
        }
        htmlPrint += "</table>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 10px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + userName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">兑换数量：" + sumNumber + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">扣减积分：" + sumPoint + "</div>";
        htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "</div></div></div>";
        LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        LODOP.PRINT();
    }
    else {
        location.reload();
    }
}
function GetGoodsLgotype(type) {
    var strType = "";
    switch (type) {
        case "0":
            strType = "初始化商品数据";
            break;
        case "1":
            strType = "商品入库";
            break;
        case "2":
            strType = "商品销售出库";
            break;
        case "3":
            strType = "商品挂单出库";
            break;
        case "4":
            strType = "撤销订单入库";
            break;
        case "5":
            strType = "商品撤单入库";
            break;
        case "6":
            strType = "商品调拔";
            break;
        case "7":
            strType = "商品退货入库";
            break;
    }
    return strType;
}


//  会员提现
function Mem_DRaw_money(UserName, global_mem, print, type, PointNum) {


    if (print.toString() == "true") {

        var LODOP;
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<div style=\"width:200mm; margin-left:10mm;\">";
        htmlPrint += "<div style=\"width:190mm; margin-top: 3mm; border: solid #A6A6A5 2px;color: #60605e; font-size: 13px;\">";
        htmlPrint += "<div style=\"width: 170mm;  margin-left: 10mm; margin-top: 1mm;\">";
        htmlPrint += "<div style=\"margin-bottom: 2mm; text-align: center; font-size: 25px; color: Gray;letter-spacing: 15px; font-family: 宋体;\">账户提现</div>";
        htmlPrint += "<div style=\"font-size: 15px; width: 100%; line-height: 25px;text-align: left;\" >" + $("#lblPrintTitle").html() + "</div>";
        htmlPrint += "<div style=\"font-size: 16px; width: 100%; line-height: 25px;\">提现单号：" + $("#lblMemDrawMoney").html() + "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; border-bottom: dashed #A6A6A5 1px; margin-bottom: 5px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%;\">会员名称：" + global_mem.MemName + "</div>";
        htmlPrint += "<div style=\"float: left; width: 33%;\">&nbsp;</div>"
        htmlPrint += "<div style=\"float: left; width: 33%;\">消费时间：" + $("#lblDrawMoneyTime").html() + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"height: auto; width: 100%;\">";
        htmlPrint += "<table style=\"font-size: 13px; color: #60605E; height: auto; width: 100%;\">";
        htmlPrint += "<tr><td style=\"width: 20%; margin-top: 2mm;\">会员卡号</td><td style=\"width: 20%; margin-top: 2mm;\">提现金额</td>";   // ;<td style=\"width: 20%; margin-top: 2mm;\">获得积分</td>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">会员总积分</td><td style=\"width: 20%; margin-top: 2mm;\">卡内余额</td></tr>";
        htmlPrint += "<tr>";
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + global_mem.MemCard + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + $("#txtAccountMoney").val() + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + global_mem.MemCard + "</td>"
        htmlPrint += "<td style=\"width: 20%; margin-top: 2mm;\">" + parseFloat(parseFloat(global_mem.MemMoney) - parseFloat($("#txtAccountMoney").val())) + "</td>"

        htmlPrint += "</tr>";
        htmlPrint += "</table>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"border-top: dashed #A6A6A5 1px; line-height: 25px; height: 25px; margin-top: 2px;\">";
        htmlPrint += "<div style=\"float: left; width: 33%; line-height: 25px;\">操作员：" + UserName + "</div>";
        htmlPrint += "</div>";
        htmlPrint += "<div style=\"line-height: 25px; height: 25px; text-align: left; clear: both; margin-bottom: 2px;\">客户签名：</div>";
        htmlPrint += "<div style=\"line-height: 25px;text-align:left;clear: both;margin-bottom: 2px;\">" + $("#lblPrintFoot").html() + "</div>";
        htmlPrint += "</div></div></div>";
        LODOP.ADD_PRINT_HTML(0, 0, "210mm", "297mm", htmlPrint);
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW();   //打印预览
        }
        LODOP.SET_PRINT_COPIES($("#PointNum").val()); //设置打印的份数
        LODOP.PRINT();
        location.reload();
    }
    else {
        window.location.href = "../Member/MemDrawMoney.aspx?PID=10";
    }
}
