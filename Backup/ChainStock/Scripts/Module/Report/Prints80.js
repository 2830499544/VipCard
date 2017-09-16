
var PrintPreview = SysParameter().PrintPreview;

//快速消费
function Print_Expense_Point(UserName, global_mem, print, type, PointNum) {
    var a = $("#lblOrderAccount").html();
    if (print.toString() == "true") {
        var LODOP;
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<table style='width:170px; font-size:12px; overflow:hidden;  text-overflow:ellipsis'>";
        if (!$("#chkNoMember").attr("checked")) {
            htmlPrint += "<tr ><td colspan='4'><strong>"
            + $("#lblPrintTitle").html()
            if (type == "0") {
                htmlPrint += "</strong><br/>快速消费小票"
            }
            else {
                htmlPrint += "</strong><br/>计时消费小票"
            }
            htmlPrint += "<br/>消费单号:" + $("#lblOrderAccount").html()
            + "<br/>会员卡号：" + global_mem.MemCard
            + "<br/>会员名称：" + global_mem.MemName
            + "<br/>----------------------------------------<br/>"
            + "消费金额：" + $("#txtDiscountMoney").val()
            + "<br/>获得积分：" + $("#txtExpPoint").val().toString()
            + "<br/>会员总积分：" + (parseFloat(global_mem.MemPoint) + parseFloat($("#txtExpPoint").val())).toString()
            + "</tr>";
        }
        else {
            htmlPrint += "<tr ><td colspan='4'><strong>"
            + $("#lblPrintTitle").html()
            + "</strong><br/>快速消费小票<br/>消费单号：" + $("#lblOrderAccount").html()
            + "<br/>会员名称：散客<br/>----------------------------------------<br/>消费金额："
            + $("#txtDiscountMoney").val()
            + "<br/>获得积分：0 <br/>会员总积分：0 </tr>";
        }
        if (!$("#chkNoMember").attr("checked")) {

            htmlPrint += "<tr><td colspan='4'>----------------------------------------<br/>应付金额：" + $("#txtDiscountMoney").val()
            + "<br/>" + accountPay($("#txtExpMoney").val(), $("#txtDiscountMoney").val())
            + "<br/>" + PayInfos(parameter)
            + "<br/>卡内余额：" + parseFloat(parseFloat(global_mem.MemMoney) - parseFloat(parameter[0].CardMoney)).toFixed(2)
            + "<br/>操作人员：" + UserName
            + "<br/>消费时间：" + $("#lblOrderCreateTime").html()
            + "<br/>客户签名：<br/><br/><br/>"
            + $("#lblPrintFoot").html() + "</td></tr>";
        }
        else {
            htmlPrint += "<tr><td colspan='4'>----------------------------------------<br/>应付金额：" + $("#txtDiscountMoney").val()
            + "<br/>" + accountPay($("#txtExpMoney").val(), $("#txtDiscountMoney").val())
            + "<br/>" + PayInfos(parameter)
            + "<br/>操作人员：" + UserName
            + "<br/>消费时间：" + $("#lblOrderCreateTime").html()
            + "<br/>客户签名：<br/><br/><br/>"
            + $("#lblPrintFoot").html()
            + "</td></tr>";
        }
        htmlPrint += "</table>"
        LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, ""); //打印方向   宽高
        LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
        LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint); //上左 宽高  
        //LODOP.ADD_PRINT_HTM("5mm", 34, "RightMargin:0.9cm", "BottomMargin:9mm", htmlPrint);
        //LODOP.ADD_PRINT_HTM("", "5%", "5%", "", htmlPrint);// 方向为  上左右下
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES($("#PointNum").val()); //设置打印的份数
        LODOP.PRINT();
        if ($("#txtIsTimeExpense").val() == "1") {
            window.location.href = "../MemExpense/TimeList.aspx?PID=87";
        }
        else {
            window.location.href = "../MemExpense/Expense.aspx?PID=17";
        }
    }
    else {
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
    htmlPrint = "<table style='width:170px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
    htmlPrint += "<tr ><td colspan='4'><strong>"
            + $("#lblPrintTitle").html()
    htmlPrint += "</strong><br/>计时消费小票";
    htmlPrint += "<br/>消费单号:" + OrderAount
            + "<br/>会员卡号：" + MemCard
            + "<br/>会员名称：" + MemName
            + "<br/>----------------------------------------<br/>"
            + "消费服务：" + ProjectName
            + "<br/>消费总时长：" + ExpenseTime + "分钟"
            + "<br/>剩余时长：" + syTime + "分钟"
            + "<br/>会员总积分：" + Point
            + "</tr>";
    htmlPrint += "<tr><td colspan='4'>----------------------------------------<br/>"
            + "<br/>操作人员：" + UserName
            + "<br/>消费时间：" + datetime
            + "<br/>客户签名：<br/><br/><br/>"
            + $("#lblPrintFoot").html() + "</td></tr>";
    htmlPrint += "</table>"
    LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, "");
    LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
    LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
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
        htmlPrint = "<table style='width:170px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
        htmlPrint += "<tr ><td colspan='4'><strong>"
            + $("#lblPrintTitle").html()
        htmlPrint += "</strong><br/>会员充时消费"
        htmlPrint += "<br/>消费单号:" + $("#lblOrderAccount").html()
            + "<br/>会员卡号：" + global_mem.MemCard
            + "<br/>会员名称：" + global_mem.MemName
            + "<br/>----------------------------------------<br/>"
            + "消费金额：" + $("#txtDiscountMoney").val()
            + "<br/>获得积分：" + $("#txtExpPoint").val().toString()
            + "<br/>会员总积分：" + (parseFloat(global_mem.MemPoint) + parseFloat($("#txtExpPoint").val())).toString()
            + "<br/>充值时间：" + time
            + "</tr>";
        htmlPrint += "<tr><td colspan='4'>----------------------------------------<br/>应付金额：" + $("#txtDiscountMoney").val()
            + "<br/>" + accountPay($("#txtExpMoney").val(), $("#txtDiscountMoney").val())
            + "<br/>" + PayInfos(parameter)
            + "<br/>卡内余额：" + parseFloat(parseFloat(global_mem.MemMoney) - parseFloat(parameter[0].CardMoney)).toFixed(2)
            + "<br/>操作人员：" + UserName
            + "<br/>消费时间：" + $("#lblOrderCreateTime").html()
            + "<br/>客户签名：<br/><br/><br/>"
            + $("#lblPrintFoot").html() + "</td></tr>";
        htmlPrint += "</table>"
        LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, "");
        LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
        LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.PRINT();
        location.reload();
    }
    else {
        location.reload();
    }
}
//会员充值
function Print_RechageMoney_Point(UserName, global_mem, print, PrintNum) {
    if (print.toString() == "true") {
        var LODOP;
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<table style='width:170px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
        htmlPrint += "<tr ><td colspan='4'><strong>" + $("#lblPrintTitle").html() + "</strong><br/>会员充值小票"
                  + "<br/>充值单号：" + $("#lblAccount").html()
                  + "<br/>会员卡号：" + global_mem.MemCard
                  + "<br/>会员名称：" + global_mem.MemName + ""
                  + "<br/>----------------------------------------"
                  + "<br/>充值金额：" + parseFloat($.trim($("#txtMoney").val())).toFixed(2)
                  + "<br/>赠送金额：" + parseFloat($.trim($("#txtGiveMoney").val())).toFixed(2)
                  + "<br/>充值总金额：" + (parseFloat($.trim($("#txtMoney").val())) + parseFloat($.trim($("#txtGiveMoney").val()))).toFixed(2)
                  + "<br/>卡内余额：" + (parseFloat(global_mem.MemMoney) + parseFloat($.trim($("#txtMoney").val())) + parseFloat($.trim($("#txtGiveMoney").val()))).toFixed(2)
                  + "</tr>"
                  + "<tr><td colspan='4'>----------------------------------------</td></tr>"
                  + "<tr><td colspan='4'>操作人员：" + UserName
                  + "<br/>充值时间：" + $("#lblRechargeTime").html()
                  + "<br/>客户签名：<br/><br/><br/>"
                  + $("#lblPrintFoot").html() + "</td></tr>";
        htmlPrint += "</table>"


        LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, ""); //设置纸张大小(打印的方向，纸张的宽，纸张的长，纸张的名字)
        LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
        LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint); //增加表格打印项（超文本模式）
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES(PrintNum); //设置打印的份数
        LODOP.PRINT(); //直接打印


        window.location.href = "../Member/MemRechargeMoney.aspx?PID=4";
    }
    else {
        window.location.href = "../Member/MemRechargeMoney.aspx?PID=4";
    }
}


//会员充次
function Print_MemCount(strUserName, lblPrintTitle, lblPrintFoot, GoodsList, parameter, bolPrint, PointNum) {
    if (bolPrint != null && bolPrint.toString() == "true") {
        var LODOP;
        var htmlPrint;
        var ss = $("#lblPrintTitle").html();
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<table style='width:170px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
        htmlPrint += "<tr><td colspan='4'><strong>" + lblPrintTitle + "</strong></td></tr>";
        htmlPrint += "<tr><td colspan='4'>充次小票</td></tr>";
        htmlPrint += "<tr><td colspan='4'>充次单号：" + $("#spOrderCode").html() + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员卡号：" + global_mem.MemCard + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员名称：" + global_mem.MemName + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>所得积分：" + totalPoint + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员总积分：" + (parseFloat(global_mem.MemPoint) + parseFloat(totalPoint)) + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>-----------------------------------</td></tr>";
        htmlPrint += "<tr><td>服务项目</td><td>次数</td><td>金额</td><td>积分</td></tr>";
        for (var i = 0; i < GoodsList.length; i++) {
            htmlPrint += "<tr><td>" + GoodsList[i]["Name"] + "</td><td>" + GoodsList[i]["ExpNum"] + "</td><td>" + parseFloat(GoodsList[i]["ExpMoney"]).toFixed(2) + "</td><td>" + GoodsList[i]["ExpPoint"] + "</td></tr>";
        }
        htmlPrint += "<tr><td colspan='4'>-----------------------------------</td></tr>";
        htmlPrint += "<tr><td colspan='4'>应付金额：" + parseFloat(totalDiscount).toFixed(2) + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>" + accountPay(totalMoney, totalDiscount) + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>" + PayInfos(parameter) + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>卡内余额：" + (parseFloat(global_mem.MemMoney) - parseFloat(parameter[0].CardMoney)).toFixed(2) + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>操作人员：" + strUserName + " </td></tr>";
        htmlPrint += "<tr><td colspan='4'>消费时间：" + GetDataTimeNow() + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>客户签名：<br/><br/><br/></td></tr>";
        htmlPrint += "<tr><td colspan='4'><strong>" + lblPrintFoot + "</strong></td></tr>";
        htmlPrint += "</table>";
        LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, "");
        LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
        LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
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
        var LODOP;
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<table style='width:170px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>"
                  + "<tr><td colspan='4'><strong>" + $("#lblPrintTitle").html() + "</strong><br/>会员积分兑换小票"
                  + "<br/>兑换单号："
                  + "<br/>" + $("#lblExchangePrefix").html()
                  + "<br/>会员卡号：" + global_mem.MemCard
                  + "<br/>会员名称：" + global_mem.MemName
                  + "<br/>兑换数量：" + sumNumber
                  + "<br/>扣减积分：" + sumPoint
                  + "<br/>剩余积分：" + (parseInt(global_mem.MemPoint) - parseInt(sumPoint)) + "</tr>"
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>"
        htmlPrint += "<tr><td>产品名称</td><td>数量</td><td>积分</td><td>合计</td></tr>";
        for (var i = 0; i < GiftList.length; i++) {
            htmlPrint += "<tr><td>" + (GiftList[i]["GiftName"].toString()) + "</td><td>" + GiftList[i]["ExcNumber"].toString() + "</td><td>" + GiftList[i]["GiftExchangePoint"] + "</td><td>" + (parseFloat(GiftList[i]["ExcNumber"].toString()) * parseFloat(GiftList[i]["GiftExchangePoint"].toString())) + "</td></tr>";
        }
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>"
                  + "<tr><td colspan='4'>操作人员：" + UserName
                  + "<br/>兑换时间：" + $("#lblExchangeTime").html()
                  + "<br/>客户签名：<br/><br/><br/>"
                  + "<br/>" + $("#lblPrintFoot").html()
                  + "</td></tr>";
        htmlPrint += "</table>"
        LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, "");
        LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
        LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
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
        htmlPrint = "<table style='width:180px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
        htmlPrint += "<tr><td colspan='4'><strong>" + lblPrintTitle + "</strong></td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员积分变动小票</td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员卡号：" + global_mem.MemCard + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员姓名：" + global_mem.MemName + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>";
        htmlPrint += "<tr><td colspan='4'>" + (exchangeType == "0" ? "增加" : "扣减") + "积分：" + Math.abs(parseFloat(changeNumber)) + "</td></tr>";
        if (type == 1) {
            if (exchangeType == "0") {
                htmlPrint += "<tr><td colspan='4'>剩余积分：" + (parseFloat(global_mem.MemPoint) + parseFloat(changeNumber)) + "</td></tr>";
            } else {
                htmlPrint += "<tr><td colspan='4'>剩余积分：" + (parseFloat(global_mem.MemPoint) - parseFloat(changeNumber)) + "</td></tr>";
            }
        }
        else {
            htmlPrint += "<tr><td colspan='4'>剩余积分：" + parseFloat(global_mem.MemPoint) + "</td></tr>";
        }
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>";
        htmlPrint += "<tr><td colspan='4'>操作员：" + userName + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>变动时间：" + pointChangeTime + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>客户签名：<br/><br/><br/></td></tr>";
        htmlPrint += "<tr><td colspan='4'><strong>" + lblPrintFoot + "</strong></td></tr>";
        htmlPrint += "</table>";
        LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, "");
        LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
        LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
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
        //小票打印
        var LODOP; //声明为全局变量
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        //LODOP.ADD_PRINT_TEXT(31,576,144,22,"");
        htmlPrint = "<table style='width:180px;margin: 0 0 0 0; padding:0px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
        if (!$("#chkNoMember").attr("checked")) {
            htmlPrint += "<tr><td colspan='4'><strong>" + $("#lblPrintTitle").html() + "</strong><br/>商品消费小票"
                      + "<br/>消费单号：" + $("#spOrderAccount").html()
                      + "<br/>会员卡号：" + global_mem.MemCard
                      + "<br/>会员名称：" + global_mem.MemName
                      + "<br/>消费金额：" + parseFloat(totalDiscount).toFixed(2)
                      + "<br/>获得积分：" + totalPoint
                      + "<br/>会员总积分：" + (parseFloat(global_mem.MemPoint) + parseFloat(totalPoint))
                      + "</tr>";
        }
        else//散客消费
        {
            htmlPrint += "<tr><td colspan='4'><strong>" + $("#lblPrintTitle").html() + "</strong><br/>商品消费小票"
                      + "<br/>消费单号：" + $("#spOrderAccount").html()
                      + "<br/>会员名称：散客"
                      + "<br/>消费金额：" + parseFloat(totalDiscount).toFixed(2)
                      + "<br/>获得积分：0 "
                      + "<br/>会员总积分：0 "
                      + "</tr>";
        }
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>"
        htmlPrint += "<tr><td>商品名称</td><td>单价</td><td>数量</td><td>合计</td></tr>";
        for (var i = 0; i < GoodsList.length; i++) {
            htmlPrint += "<tr><td colspan='4'>" + GoodsList[i]["Name"] + "</td></tr>";
            htmlPrint += "<tr>";
            htmlPrint += "<td>" + GoodsList[i]["GoodsCode"].toString().substring(0, 6) + "..." + "</td>"
            htmlPrint += "<td>" + parseFloat(GoodsList[i]["Price"]).toFixed(2) + "</td>"
            htmlPrint += "<td>" + GoodsList[i]["ExpNum"] + "</td>"
            htmlPrint += "<td>" + parseFloat(GoodsList[i]["ExpMoney"]).toFixed(2) + "</td>"
            htmlPrint += "</tr>";
        }
        if (!$("#chkNoMember").attr("checked")) {
            htmlPrint += "<tr ><td colspan='4'>----------------------------------------<br/>"
                      + "应付金额：" + parseFloat(totalDiscount).toFixed(2)
                     + "<br/>" + accountPay(totalMoney, totalDiscount)
                      + "<br/>" + PayInfos(parameter)
                      + "<br/>卡内余额：" + parseFloat((parseFloat(global_mem.MemMoney) - parseFloat(parameter[0].CardMoney)).toString()).toFixed(2)
                      + "<br/>操作人员：" + userName
                      + "<br/>消费时间：" + $("#txtExpenseTime").val()
                      + "<br/>客户签名：<br/><br/><br/>"
                      + "<br/>" + $("#lblPrintFoot").html()
                      + "</td></tr>";
        }
        else//散客消费
        {
            htmlPrint += "<tr ><td colspan='4'>----------------------------------------<br/>"
                      + "应付金额：" + parseFloat(totalDiscount).toFixed(2)
                      + "<br/>" + accountPay(totalMoney, totalDiscount)
                      + "<br/>" + PayInfos(parameter)
                      + "<br/>操作人员：" + userName
                      + "<br/>消费时间：" + GetDataTimeNow()
                      + "<br/>客户签名：<br/><br/><br/>"
                      + "<br/>" + $("#lblPrintFoot").html()
                      + "</td></tr>";
        }
        htmlPrint += "</table>"
        LODOP.SET_PRINT_PAGESIZE(1, 600, 1400, "");
        LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
        LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
        //   LODOP.SET_PRINT_STYLEA(2,"FontSize",12);
        //	 LODOP.SET_PRINT_STYLEA(2,"ItemType",4);
        //	 LODOP.SET_PRINT_STYLEA(2,"HOrient",3);
        //	 LODOP.SET_PRINT_STYLEA(2,"VOrient",3);
        LODOP.SET_PRINT_COPIES($("#PointNum").val()); //设置打印的份数
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.PRINT();
        window.location.href = "../StockManage/GoodsExpense.aspx?PID=67";
    }
    else {
        window.location.href = "../StockManage/GoodsExpense.aspx?PID=67";
    }
}


//商品消费打印小票
function PrintMemCountExpense(userName, global_mem, GoodsList, parameter, print, PointNum) {
    if (print != null && print.toString() == "true") {
        //小票打印
        var LODOP; //声明为全局变量
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        //LODOP.ADD_PRINT_TEXT(31,576,144,22,"");
        htmlPrint = "<table style='width:180px;margin: 0 0 0 0; padding:0px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
        if (!$("#chkNoMember").attr("checked")) {
            htmlPrint += "<tr><td colspan='4'><strong>" + $("#lblPrintTitle").html() + "</strong><br/>计次消费小票"
                      + "<br/>消费单号：" + $("#spOrderAccount").html()
                      + "<br/>会员卡号：" + global_mem.MemCard
                      + "<br/>会员名称：" + global_mem.MemName
                      + "<br/>消费金额：" + parseFloat(totalDiscount).toFixed(2)
                      + "<br/>获得积分：" + totalPoint
                      + "<br/>会员总积分：" + (parseFloat(global_mem.MemPoint) + parseFloat(totalPoint))
                      + "</tr>";
        }
        else//散客消费
        {
            htmlPrint += "<tr><td colspan='4'><strong>" + $("#lblPrintTitle").html() + "</strong><br/>计次消费小票"
                      + "<br/>消费单号：" + $("#spOrderAccount").html()
                      + "<br/>会员名称：散客"
                      + "<br/>消费金额：" + parseFloat(totalDiscount).toFixed(2)
                      + "<br/>获得积分：0 "
                      + "<br/>会员总积分：0 "
                      + "</tr>";
        }
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>"
        htmlPrint += "<tr><td>商品名称</td><td>单价</td><td>数量</td><td>合计</td></tr>";
        for (var i = 0; i < GoodsList.length; i++) {
            htmlPrint += "<tr><td colspan='4'>" + GoodsList[i]["Name"] + "</td></tr>";
            htmlPrint += "<tr>";
            htmlPrint += "<td>" + GoodsList[i]["GoodsCode"].toString().substring(0, 6) + "..." + "</td>"
            htmlPrint += "<td>" + parseFloat(GoodsList[i]["Price"]).toFixed(2) + "</td>"
            htmlPrint += "<td>" + GoodsList[i]["ExpNum"] + "</td>"
            htmlPrint += "<td>" + parseFloat(GoodsList[i]["ExpMoney"]).toFixed(2) + "</td>"
            htmlPrint += "</tr>";
        }
        if (!$("#chkNoMember").attr("checked")) {
            htmlPrint += "<tr ><td colspan='4'>----------------------------------------<br/>"
                      + "应付金额：" + parseFloat(totalDiscount).toFixed(2)
                     + "<br/>" + accountPay(totalMoney, totalDiscount)
                      + "<br/>" + PayInfos(parameter)
                      + "<br/>卡内余额：" + parseFloat((parseFloat(global_mem.MemMoney) - parseFloat(parameter[0].CardMoney)).toString()).toFixed(2)
                      + "<br/>操作人员：" + userName
                      + "<br/>消费时间：" + $("#txtExpenseTime").val()
                      + "<br/>客户签名：<br/><br/><br/>"
                      + "<br/>" + $("#lblPrintFoot").html()
                      + "</td></tr>";
        }
        else//散客消费
        {
            htmlPrint += "<tr ><td colspan='4'>----------------------------------------<br/>"
                      + "应付金额：" + parseFloat(totalDiscount).toFixed(2)
                      + "<br/>" + accountPay(totalMoney, totalDiscount)
                      + "<br/>" + PayInfos(parameter)
                      + "<br/>操作人员：" + userName
                      + "<br/>消费时间：" + GetDataTimeNow()
                      + "<br/>客户签名：<br/><br/><br/>"
                      + "<br/>" + $("#lblPrintFoot").html()
                      + "</td></tr>";
        }
        htmlPrint += "</table>"
        LODOP.SET_PRINT_PAGESIZE(1, 600, 1400, "");
        LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
        LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
        //   LODOP.SET_PRINT_STYLEA(2,"FontSize",12);
        //	 LODOP.SET_PRINT_STYLEA(2,"ItemType",4);
        //	 LODOP.SET_PRINT_STYLEA(2,"HOrient",3);
        //	 LODOP.SET_PRINT_STYLEA(2,"VOrient",3);
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.PRINT();
        location.reload();
    }
    else {
        location.reload();
    }
}

//二次打印快速消费
function SecondExpensePrint(UserName, mem, expense, PointNum) {
    var LODOP;
    var htmlPrint;
    LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
    htmlPrint = "<table style='width:170px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
    htmlPrint += "<tr ><td colspan='4'><strong>" + $("#lblPrintTitle").html() + "</strong><br/>累计积分消费小票"
              + "<br/>消费单号：" + expense.OrderAccount
              + "<br/>会员卡号：" + mem.MemCard
              + "<br/>会员名称：" + mem.MemName
              + "<br/>----------------------------------------<br/>"
              + "<br/>消费金额：" + expense.OrderDiscountMoney.ToNumber().toFixed(2)
              + "<br/>获得积分：" + expense.OrderPoint
    //              + "<br/>会员总积分：" + (parseFloat(mem.MemPoint) + parseFloat(expense.OrderPoint)).toString()
            + "<br/>会员总积分：" + (parseFloat(expense.OrderCardPoint)).toString()
              + "</tr>";
    htmlPrint += "<tr><td colspan='4'>----------------------------------------<br/>应付金额：" + expense.OrderDiscountMoney.ToNumber().toFixed(2)
              + "<br/>" + accountPay(expense.OrderTotalMoney, expense.OrderDiscountMoney)
              + PayType(expense)
    //+ "<br/>卡内余额：" + (parseFloat(mem.MemMoney)).toFixed(2)
              + "<br/>卡内余额：" + (parseFloat(expense.OrderCardBalance)).toFixed(2)
              + "<br/>操作人员：" + UserName
              + "<br/>消费时间：" + expense.OrderCreateTime
              + "<br/>客户签名：<br/><br/><br/>"
              + $("#lblPrintFoot").html()
              + "</td></tr>";
    htmlPrint += "</table>"
    LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, "");
    LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
    LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
    LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
    if (PrintPreview == 1) {//等于1表示 启用打印预览
        LODOP.PREVIEW(); //打印预览
    }
    LODOP.PRINT();
    //window.location.href = "../MemExpense/ExpenseHistory.aspx?PID=18";
    //        location.reload();
}
//商品消费二次打印小票
function SecondGoodsExpensePrint(userName, mem, expense, detail, PointNum) {
    //小票打印
    var LODOP; //声明为全局变量
    var htmlPrint;
    LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
    //LODOP.ADD_PRINT_TEXT(31,576,144,22,"");
    htmlPrint = "<table style='width:180px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
    if (mem.MemID != 0) {
        htmlPrint += "<tr ><td colspan='4'><strong>" + $("#lblPrintTitle").html() + "</strong><br/>商品消费小票"
                  + "<br/>消费单号：" + expense.OrderAccount
                  + "<br/>会员卡号：" + mem.MemCard
                  + "<br/>会员名称：" + mem.MemName
                  + "<br/>消费金额：" + expense.OrderDiscountMoney.ToNumber().toFixed(2)
                  + "<br/>获得积分：" + expense.OrderPoint
        //+ "<br/>会员总积分：" + (parseFloat(mem.MemPoint) + parseFloat(expense.OrderPoint)) + "</tr>";
                  + "<br/>会员总积分：" + (parseFloat(expense.OrderCardPoint)) + "</tr>";
    }
    else//散客消费
    {
        htmlPrint += "<tr ><td colspan='4'><strong>" + $("#lblPrintTitle").html() + "</strong><br/>商品消费小票"
                  + "<br/>消费单号：" + expense.OrderAccount
                  + "<br/>会员名称：散客"
                  + "<br/>消费金额：" + expense.OrderDiscountMoney.ToNumber().toFixed(2)
                  + "<br/>获得积分：0 "
                  + "<br/>会员总积分：0 "
                  + "</tr>";
    }

    htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>"
    htmlPrint += "<tr><td>商品名称</td><td>单价</td><td>数量</td><td>合计</td></tr>";
    for (var i = 0; i < detail.length; i++) {
        htmlPrint += "<tr><td colspan='4'>" + detail[i]["Name"] + "</td></tr>";
        htmlPrint += "<tr>";
        htmlPrint += "<td>" + detail[i]["GoodsCode"].toString().substring(0, 6) + "..." + "</td>"
        htmlPrint += "<td>" + parseFloat(detail[i]["OrderDetailPrice"]).toFixed(2) + "</td>"
        htmlPrint += "<td>" + detail[i]["OrderDetailNumber"] + "</td>"
        htmlPrint += "<td>" + parseFloat(detail[i]["OrderDetailDiscountPrice"]).toFixed(2) + "</td>"
        htmlPrint += "</tr>";
    }
    if (mem.MemID != 0) {
        htmlPrint += "<tr ><td colspan='4'>----------------------------------------"
                  + "<br/>应付金额：" + parseFloat(expense.OrderDiscountMoney).toFixed(2)
                  + "<br>" + accountPay(expense.OrderTotalMoney, expense.OrderDiscountMoney)
                  + PayType(expense)
        //+ "<br/>卡内余额：" + parseFloat(mem.MemMoney).toFixed(2)
                  + "<br/>卡内余额：" + parseFloat(expense.OrderCardBalance).toFixed(2)
                  + "<br/>操作人员：" + userName
                  + "<br/>消费时间：" + expense.OrderCreateTime
                  + "<br/>客户签名：<br/><br/><br/>"
                  + "<br/>" + $("#lblPrintFoot").html()
                  + "</td></tr>";
    }
    else//散客消费
    {
        htmlPrint += "<tr ><td colspan='4'>----------------------------------------"
                  + "<br/>应付金额：" + parseFloat(expense.OrderDiscountMoney).toFixed(2)
                  + "<br/>" + accountPay(expense.OrderTotalMoney, expense.OrderDiscountMoney)
                  + PayType(expense)
                  + "<br/>操作人员：" + userName
                  + "<br/>消费时间：" + expense.OrderCreateTime
                  + "<br/>客户签名：<br/><br/><br/>"
                  + "<br/>" + $("#lblPrintFoot").html()
                  + "</td></tr>";
    }
    htmlPrint += "</table>"

    LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, "");
    LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
    LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
    //   LODOP.SET_PRINT_STYLEA(2,"FontSize",12);
    //	 LODOP.SET_PRINT_STYLEA(2,"ItemType",4);
    //	 LODOP.SET_PRINT_STYLEA(2,"HOrient",3);
    //	 LODOP.SET_PRINT_STYLEA(2,"VOrient",3);
    LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
    if (PrintPreview == 1) {//等于1表示 启用打印预览
        LODOP.PREVIEW(); //打印预览
    }
    LODOP.PRINT();
    location.reload();
}

function SecondMemCountExpensePrint(userName, mem, expense, detail, PointNum) {
    //小票打印
    var LODOP; //声明为全局变量
    var htmlPrint;
    LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
    //LODOP.ADD_PRINT_TEXT(31,576,144,22,"");
    htmlPrint = "<table style='width:180px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
    if (mem.MemID != 0) {
        htmlPrint += "<tr ><td colspan='4'><strong>" + $("#lblPrintTitle").html() + "</strong><br/>计次消费小票"
                  + "<br/>消费单号：" + expense.OrderAccount
                  + "<br/>会员卡号：" + mem.MemCard
                  + "<br/>会员名称：" + mem.MemName
                  + "<br/>消费金额：" + expense.OrderDiscountMoney.ToNumber().toFixed(2)
                  + "<br/>获得积分：" + expense.OrderPoint
                  + "<br/>会员总积分：" + (parseFloat(expense.OrderCardPoint)) + "</tr>";
    }
    else//散客消费
    {
        htmlPrint += "<tr ><td colspan='4'><strong>" + $("#lblPrintTitle").html() + "</strong><br/>计次消费小票"
                  + "<br/>消费单号：" + expense.OrderAccount
                  + "<br/>会员名称：散客"
                  + "<br/>消费金额：" + expense.OrderDiscountMoney.ToNumber().toFixed(2)
                  + "<br/>获得积分：0 "
                  + "<br/>会员总积分：0 "
                  + "</tr>";
    }

    htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>"
    htmlPrint += "<tr><td>商品名称</td><td>单价</td><td>数量</td><td>合计</td></tr>";
    for (var i = 0; i < detail.length; i++) {
        htmlPrint += "<tr><td colspan='4'>" + detail[i]["Name"] + "</td></tr>";
        htmlPrint += "<tr>";
        htmlPrint += "<td>" + detail[i]["GoodsCode"].toString().substring(0, 6) + "..." + "</td>"
        htmlPrint += "<td>" + parseFloat(detail[i]["OrderDetailPrice"]).toFixed(2) + "</td>"
        htmlPrint += "<td>" + detail[i]["OrderDetailNumber"] + "</td>"
        htmlPrint += "<td>" + parseFloat(detail[i]["OrderDetailDiscountPrice"]).toFixed(2) + "</td>"
        htmlPrint += "</tr>";
    }
    if (mem.MemID != 0) {
        htmlPrint += "<tr ><td colspan='4'>----------------------------------------"
                  + "<br/>应付金额：" + parseFloat(expense.OrderDiscountMoney).toFixed(2)
                  + "<br>" + accountPay(expense.OrderTotalMoney, expense.OrderDiscountMoney)
                  + PayType(expense)
        //+ "<br/>卡内余额：" + parseFloat(mem.MemMoney).toFixed(2)
                  + "<br/>卡内余额：" + parseFloat(expense.OrderCardBalance).toFixed(2)
                  + "<br/>操作人员：" + userName
                  + "<br/>消费时间：" + expense.OrderCreateTime
                  + "<br/>客户签名：<br/><br/><br/>"
                  + "<br/>" + $("#lblPrintFoot").html()
                  + "</td></tr>";
    }
    else//散客消费
    {
        htmlPrint += "<tr ><td colspan='4'>----------------------------------------"
                  + "<br/>应付金额：" + parseFloat(expense.OrderDiscountMoney).toFixed(2)
                  + "<br/>" + accountPay(expense.OrderTotalMoney, expense.OrderDiscountMoney)
                  + PayType(expense)
                  + "<br/>操作人员：" + userName
                  + "<br/>消费时间：" + expense.OrderCreateTime
                  + "<br/>客户签名：<br/><br/><br/>"
                  + "<br/>" + $("#lblPrintFoot").html()
                  + "</td></tr>";
    }
    htmlPrint += "</table>"

    LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, "");
    LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
    LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
    //   LODOP.SET_PRINT_STYLEA(2,"FontSize",12);
    //	 LODOP.SET_PRINT_STYLEA(2,"ItemType",4);
    //	 LODOP.SET_PRINT_STYLEA(2,"HOrient",3);
    //	 LODOP.SET_PRINT_STYLEA(2,"VOrient",3);
    LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
    if (PrintPreview == 1) {//等于1表示 启用打印预览
        LODOP.PREVIEW(); //打印预览
    }
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
        //        var strChange = accSub(accAdd(accAdd(parameter.OrderPayCard, parameter.OrderPayCash), parameter.OrderPayBink), accSub(parameter.OrderDiscountMoney, parameter.OrderPayCoupon));
        //        strPayInfo += "找零金额：" + parseFloat(strChange).toFixed(2) + "";
    }
    return strPayInfo;
}
//入库打印小票
function PrintGoodsIn(userName, shopName, GoodsList, print, PointNum) {
    if (print != null && print.toString() == "true") {
        //小票打印
        var LODOP; //声明为全局变量
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        //LODOP.ADD_PRINT_TEXT(31,576,144,22,"");
        htmlPrint = "<table style='width:180px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
        htmlPrint += "<tr ><td colspan='4'><strong>" + $("#lblPrintTitle").html() + "</strong><br/>入库小票"
                  + "<br/>入库单号：" + $("#spGoodsAccounte").html()
                  + "<br/>入库商家：" + shopName
                  + "</tr>";
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>"
        htmlPrint += "<tr><td>商品名称</td><td>单价</td><td>数量</td><td>合计</td></tr>";
        for (var i = 0; i < GoodsList.length; i++) {
            htmlPrint += "<tr><td colspan='4'>" + GoodsList[i]["Name"] + "</td></tr>";
            htmlPrint += "<tr>";
            htmlPrint += "<td>" + GoodsList[i]["GoodsCode"].toString().substring(0, 6) + "..." + "</td>"
            htmlPrint += "<td>" + parseFloat(GoodsList[i]["InMoney"]).toFixed(2) + "</td>"
            htmlPrint += "<td>" + GoodsList[i]["Number"] + "</td>"
            htmlPrint += "<td>" + parseFloat(GoodsList[i]["TotalMoney"]).toFixed(2) + "</td>"
            htmlPrint += "</tr>";
        }

        htmlPrint += "<tr ><td colspan='4'>----------------------------------------"
                  + "<br/>入库数量：" + totalNumber
                  + "<br/>入库总额：" + parseFloat(totalMoney).toFixed(2)
                  + "<br/>入库时间：" + $("#txtCreteTime").val()
                  + "<br/>签名：<br/><br/><br/>"
                  + '<br/>' + $("#lblPrintFoot").html()
                  + "</td></tr>";
        htmlPrint += "</table>"
        LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, "");
        LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
        LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
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
    LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
    //LODOP.ADD_PRINT_TEXT(31,576,144,22,"");
    htmlPrint = "<table style='width:180px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
    htmlPrint += "<tr ><td colspan='4'><strong>" + printtitle + "</strong><br/>商品出入库小票"
                  + "<br/>出入库单号：" + goodslog.GoodsAccount
                  + "<br/>出入库方式：" + GetGoodsLgotype(goodslog.Type)
    //+ "<br/>入库商家：" + goodslog.InShopName
                  + "<br/>异动商家：" + goodslog.ChangeShopName
                  + "</tr>";

    if (logdetail != "") {
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>"
        htmlPrint += "<tr><td>名称</td><td>入库价</td><td>出库价</td><td>数量</td></tr>";
        for (var i = 0; i < logdetail.length; i++) {
            //            htmlPrint += "<tr><td>" + logdetail[i]["Name"]
            //            + "</td><td>" + parseFloat(logdetail[i]["GoodsInPrice"]).toFixed(2)
            //            + "</td><td>" + parseFloat(logdetail[i]["GoodsOutPrice"]).toFixed(2)
            //            + "</td><td>" + Math.abs(logdetail[i]["GoodsNumber"]) + "</td></tr>";
            htmlPrint += "<tr><td colspan='4'>" + logdetail[i]["Name"] + "</td></tr>";
            htmlPrint += "<tr>";
            htmlPrint += "<td>" + logdetail[i]["GoodsCode"].toString().substring(0, 4) + "..." + "</td>"
            htmlPrint += "<td>" + parseFloat(logdetail[i]["GoodsInPrice"]).toFixed(2) + "</td>"
            htmlPrint += "<td>" + parseFloat(logdetail[i]["GoodsOutPrice"]).toFixed(2) + "</td>"
            htmlPrint += "<td>" + Math.abs(logdetail[i]["GoodsNumber"]) + "</td>"
            htmlPrint += "</tr>";
        }
    }
    htmlPrint += "<tr ><td colspan='4'>----------------------------------------"
                  + "<br/>总金额：" + parseFloat(goodslog.TotalPrice).toFixed(2)
                  + "<br/>操作人员：" + userName
                  + "<br/>出入库时间：" + goodslog.CreateTime
                  + "<br/>签名：<br/><br/><br/>"
                  + "<br/>" + printfoot
                  + "</td></tr>";


    htmlPrint += "</table>"

    LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, "");
    LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
    LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
    //   LODOP.SET_PRINT_STYLEA(2,"FontSize",12);
    //	 LODOP.SET_PRINT_STYLEA(2,"ItemType",4);
    //	 LODOP.SET_PRINT_STYLEA(2,"HOrient",3);
    //	 LODOP.SET_PRINT_STYLEA(2,"VOrient",3);

    LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
    if (PrintPreview == 1) {//等于1表示 启用打印预览
        LODOP.PREVIEW(); //打印预览
    }
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
        htmlPrint = "<table style='width:180px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
        htmlPrint += "<tr><td colspan='4'><strong>" + lblPrintTitle + "</strong></td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员充值小票</td></tr>";
        htmlPrint += "<tr><td colspan='4'>充值单号：" + lblAccount + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员卡号：" + global_mem.MemCard + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员姓名：" + global_mem.MemName + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>";
        if (printCount == "1") {
            htmlPrint += "<tr><td colspan='4'>充值金额：" + parseFloat(txtMoney).toFixed(2) + "</td></tr>";
            htmlPrint += "<tr><td colspan='4'>赠送金额：" + parseFloat(txtGiveMoney).toFixed(2) + "</td></tr>";
            htmlPrint += "<tr><td colspan='4'>充值总金额：" + parseFloat(parseFloat(txtMoney) + parseFloat(txtGiveMoney)).toFixed(2) + "</td></tr>";
            htmlPrint += "<tr><td colspan='4'>卡内余额：" + parseFloat(parseFloat(global_mem.MemMoney) + parseFloat(txtMoney) + parseFloat(txtGiveMoney)).toFixed(2) + "</td></tr>";
        }
        else {
            htmlPrint += "<tr><td colspan='4'>充值金额：" + parseFloat(parseFloat(txtMoney) - parseFloat(txtGiveMoney)).toFixed(2) + "</td></tr>";
            htmlPrint += "<tr><td colspan='4'>赠送金额：" + parseFloat(txtGiveMoney).toFixed(2) + "</td></tr>";
            htmlPrint += "<tr><td colspan='4'>充值总金额：" + parseFloat(txtMoney).toFixed(2) + "</td></tr>";
            htmlPrint += "<tr><td colspan='4'>卡内余额：" + parseFloat(rechargeCardBalance).toFixed(2) + "</td></tr>";
        }
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>";
        htmlPrint += "<tr><td colspan='4'>操作员：" + userName + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>充值时间：" + getDataTimeNow + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>客户签名：<br/><br/><br/></td></tr>";
        htmlPrint += "<tr><td colspan='4'><strong>" + lblPrintFoot + "</strong></td></tr>";
        htmlPrint += "</table>"
        LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, "");
        LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
        LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.PRINT();
        location.reload();
    }
    location.reload();
}

//积分变动 exchangeType（0：增加积分 1：扣减积分）printCount（第几次打印，大于1代表再次打印）
function Print_PointChangeAgain(lblPrintTitle, global_mem, exchangeType, changeNumber, printCount, userName, getDataTimeNow, lblPrintFoot, print, PointNum) {
    if (print) {
        var LODOP;
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<table style='width:180px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
        htmlPrint += "<tr><td colspan='4'><strong>" + lblPrintTitle + "</strong></td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员积分变动小票</td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员卡号：" + global_mem.MemCard + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员姓名：" + global_mem.MemName + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>";
        htmlPrint += "<tr><td colspan='4'>" + (changeNumber.substr(0, 1) == "-" ? "扣减" : "增加") + Math.abs(parseFloat(changeNumber)) + "</td></tr>";
        if (printCount == 1) {//changeNumber.substr(0, 1) == "-"? "扣减" : "增加"
            if (exchangeType == "0") {
                htmlPrint += "<tr><td colspan='4'>剩余积分：" + (parseFloat(global_mem.MemPoint) + parseFloat(changeNumber)) + "</td></tr>";
            } else {
                htmlPrint += "<tr><td colspan='4'>剩余积分：" + (parseFloat(global_mem.MemPoint) - parseFloat(changeNumber)) + "</td></tr>";
            }
        }
        else {
            htmlPrint += "<tr><td colspan='4'>剩余积分：" + parseFloat(global_mem.MemPoint) + "</td></tr>";
        }
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>";
        htmlPrint += "<tr><td colspan='4'>操作员：" + userName + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>变动时间：" + getDataTimeNow + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>客户签名：<br/><br/><br/></td></tr>";
        htmlPrint += "<tr><td colspan='4'><strong>" + lblPrintFoot + "</strong></td></tr>";
        htmlPrint += "</table>";
        LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, "");
        LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
        LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.PRINT();
        location.reload();
    }
    else {
        location.reload();
    }
}

//礼品兑换 sumNumber（兑换总量） GiftList（兑换礼品信息列表）
function Print_GiftExchangeAgain_Point(lblPrintTitle, global_mem, sumNumber, sumPoint, GiftList, userName, getDataTimeNow, lblPrintFoot, print, printCount, exchangeAccount, PointNum) {
    if (print) {
        var LODOP;
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<table style='width:180px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
        htmlPrint += "<tr><td colspan='4'><strong>" + lblPrintTitle + "</strong></td></tr> ";
        htmlPrint += "<tr><td colspan='4'>会员积分兑换小票</td></tr>";
        htmlPrint += "<tr><td colspan='4'>兑换单号：<br />" + exchangeAccount + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员卡号：" + global_mem.MemCard + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>会员姓名：" + global_mem.MemName + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>兑换数量：" + parseInt(sumNumber) + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>扣减积分：" + parseInt(sumPoint) + "</td></tr>";
        if (printCount == 1) {
            htmlPrint += "<tr><td colspan='4'>剩余积分：" + (parseInt(global_mem.MemPoint) - parseInt(sumPoint)) + "</td></tr>";
        } else {
            htmlPrint += "<tr><td colspan='4'>剩余总积分：" + parseInt(global_mem.MemPoint) + "</td></tr>";
        }
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>";
        htmlPrint += "<tr><td>名称</td><td>数量</td><td>积分</td><td>合计</td></tr>";
        for (var i = 0; i < GiftList.length; i++) {
            htmlPrint += "<tr><td>" + GiftList[i]["GiftName"].toString() + "</td><td>" + parseInt(GiftList[i]["ExchangeNumber"].toString()) + "</td><td>" + parseInt(GiftList[i]["GiftExchangePoint"]) + "</td><td>" + parseInt(GiftList[i]["ExchangePoint"]) + "</td></tr>";
        }
        htmlPrint += "<tr><td colspan='4'>----------------------------------------</td></tr>";
        htmlPrint += "<tr><td colspan='4'>操作员：" + userName + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>兑换时间：" + getDataTimeNow + "</td></tr>";
        htmlPrint += "<tr><td colspan='4'>客户签名：<br/><br/><br/></td></tr>";
        htmlPrint += "<tr><td colspan='4'><strong>" + lblPrintFoot + "</strong></td></tr>";
        htmlPrint += "</table>"
        LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, "");
        LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
        LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint);
        LODOP.PRINT();
        LODOP.SET_PRINT_COPIES(PointNum); //设置打印的份数
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.PRINT();
        location.reload();
    }
    location.reload();

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


//账户提现
function Mem_DRaw_money(UserName, global_mem, print, PrintNum) {
    if (print.toString() == "true") {
        var LODOP;
        var htmlPrint;
        LODOP = getLodop(document.getElementById('LODOP'), document.getElementById('LODOP_EM'));
        htmlPrint = "<table style='width:170px; font-size:12px; overflow:hidden; text-overflow:ellipsis'>";
        htmlPrint += "<tr ><td colspan='4'><br/>账户提现小票"  // <strong>" + $("#lblPrintTitle").html() + "<>
                  + "<br/>提现单号：" + $("#lblMemDrawMoney").html()
                  + "<br/>会员卡号：" + global_mem.MemCard
                  + "<br/>会员名称：" + global_mem.MemName + ""
                  + "<br/>----------------------------------------"
                  + "<br/>提现金额：" + parseFloat($.trim($("#txtDrawMoney").val())).toFixed(2)
                  + "<br/>实际金额：" + parseFloat($.trim($("#txtAccountMoney").val())).toFixed(2)
                  + "<br/>卡内余额：" + parseFloat(parseFloat(global_mem.MemMoney) - parseFloat($("#txtAccountMoney").val())).toFixed(2)
                  + "</tr>"
                  + "<tr><td colspan='4'>----------------------------------------</td></tr>"
                  + "<tr><td colspan='4'>操作人员：" + UserName
                  + "<br/>客户签名：<br/><br/><br/>"
                  + "</td></tr>";
        htmlPrint += "</table>"


        LODOP.SET_PRINT_PAGESIZE(1, 1000, 1400, ""); //设置纸张大小(打印的方向，纸张的宽，纸张的长，纸张的名字)
        LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 900, 540, "");
        LODOP.ADD_PRINT_TABLE(0, 50, 1000, 1400, htmlPrint); //增加表格打印项（超文本模式）
        if (PrintPreview == 1) {//等于1表示 启用打印预览
            LODOP.PREVIEW(); //打印预览
        }
        LODOP.SET_PRINT_COPIES(PrintNum); //设置打印的份数
        LODOP.PRINT(); //直接打印


        window.location.href = "../Member/MemDrawMoney.aspx?PID=10";
    }
    else {
        window.location.href = "../Member/MemDrawMoney.aspx?PID=10";
    }
}
