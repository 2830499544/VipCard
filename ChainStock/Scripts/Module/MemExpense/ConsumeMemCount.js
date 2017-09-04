/*************************************************************************************************************
计次消费页面JS处理
**************************************************************************************************************/
var pageSize = 10;
var currentPage = 1;
var key = "", keyBarCode = "";       //搜索的值
var totalNumber = 0, totalPoint = 0, totalMoney = 0, totalDiscount = 0, totalStaffMoney = 0; //汇总数据初始化
var GoodsList = new Array();
var type = 0;       //判断是计次消费还是普通消费
var staff;      //是否启动员工提成
var staffType;  //提成类型 按商品还是按员工

var isChangeStaff = 0; //是否为整单提成
var staffPercent = 0; //选择整单提成时员工的提成比例
var isEmptyBillsExpense = false;
var order = 0;
$(document).ready(function () {
    //判断是否启用员工提成
    staff = $("#chkStaff").attr("checked");
    if (!staff) {
        $("#tdStaff").css("display", "none");
        $("#tddStaff").css("display", "none");
        $("#thStaff").css("display", "none");
    }
    else {
        $("#tdStaff").css("display", "");
        $("#tddStaff").css("display", "");
        $("#thStaff").css("display", "");
    }
    staffType = $("#txtStaffType").val();
    //根据是否启动员工提成 来显示或隐藏员工下拉框
    if (!staff) {
        $("#sltStaff").attr("disabled", "disabled");
    }
    else {
        $("#sltStaff").attr("disabled", "");
        //下拉框选择响应事件     
        $("#sltStaff").bind("change", changeAllStaff);
    }

    //页签 切换页签方法
    $("#liService").css("display", "none");
    //搜索按钮响应的事件
    $("#btnGoodsSearch").bind("click", btnGoodsSearch);
    //消费时间绑定日期控件
    $('#txtExpenseTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm', maxDate: '%y-%M-%d %H:%m:%s', isShowClear: false, readOnly: true });
    });
    //结算按钮响应的事件
    $("#btnExpenseSave").bind("click", ExpenseSave);

    //输入消费金额得到折后金额和积分
    //  $("#txtExpMoney").bind("keyup", DiscountMoney);

    $("#findTable").removeAttr("style");
    $("#findTable").css("width", "100%");
    $("#tdExpense").css("vertical-align", "top")
    $("#tbTop").css("width", "100%");
    $("#tbBody").css("width", "100%");
    if ($("#MemCard").val() != null && $("#MemCard").val() != "0") {
        $("#txtFindMember").val($("#MemCard").val());
    }
    else {
        $("#chkNoMember").attr("checked", "checked");
        var orderID = $("#txtOrderID").val();
        GetExpenseDetail(orderID);
    }
});

function pageHeight() {
    if ($.browser.msie) {
        return document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight :
document.body.clientHeight;
    } else {
        return self.innerHeight;
    }
};

/****************************************************************************************************
*输入商品名称或简码后  回车响应事件
*****************************************************************************************************/
function getKey() {
    if (event.keyCode == 13) {
        var strErrorMsg = "";
        if ($.isEmptyObject(global_mem)) {
            strErrorMsg += "<li>请先选择会员!</li>";
            art.dialog({
                title: '系统提示',
                content: strErrorMsg,   //弹出层显示文本
                icon: 'error', //图标
                lock: true//是否锁定背景
            });
            return false;
        }
        else {
            key = $("#txtGoodsCode").val()
            ServingProductList();
        }


    }
}


/****************************************************************************************************
*下拉框改变响应事件
*****************************************************************************************************/
function changeAllStaff() {
    if ($("#sltStaff").val() != "") {
        //整单提成
        isChangeStaff = 1;
        staffPercent = $("#sltStaff").find("option:selected").attr("ClassPercent");
        // $("select[name='sltStaff']").find("option[value='" + $("#sltStaff").val() + "']").attr("selected", true);
        $("select[id ^=staff]").each(function () {
            $(this).attr("value", $("#sltStaff").val().toString()).trigger("change");
            //$(this).attr("disabled", "disabled");
        });
    }
    else {
        isChangeStaff = 0;
        $("#lblStaffMoney").val("0");
    }
    //    ExGoodsBindList();
    ExGoodsBindTotal();
}

/****************************************************************************************************
*搜索按钮响应事件
*****************************************************************************************************/
function btnGoodsSearch() {
    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员!</li>";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    else {
        key = $("#txtGoodsCode").val()
        ServingProductList();
    }
}

/****************************************************************************************************
*下拉框改变响应事件
*****************************************************************************************************/
function changeAllStaff() {
    if ($("#sltStaff").val() != "") {

        //整单提成
        isChangeStaff = 1;
        staffPercent = $("#sltStaff").find("option:selected").attr("ClassPercent");
        // $("select[name='sltStaff']").find("option[value='" + $("#sltStaff").val() + "']").attr("selected", true);
        $("select[id ^=staff]").each(function () {
            $(this).attr("value", $("#sltStaff").val().toString()).trigger("change");
            //$(this).attr("disabled", "disabled");
        });
    }
    else {
        isChangeStaff = 0;
        $("#lblStaffMoney").val("0");
    }
    //    ExGoodsBindList();

    ExGoodsBindTotal();
}

/****************************************************************************************************
*获取计次项目
*****************************************************************************************************/
function ServingProductList() {
    doAjax(
               "../",
               "GetServingProductList",
               {
                   "size": pageSize,
                   "index": currentPage,
                   "key": key,
                   "memID": global_mem.MemID
               },
               "json",
               function (json) {
                   CreateServingProductTable(json.List);
                   CreateServingProductPager(json.RecordCount)
               });
}
/****************************************************************************************************
*创建计次项目表格
*****************************************************************************************************/
function CreateServingProductTable(obj) {
    var html = '';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            var bg = index % 2 == 0 ? "#eee" : "#fff";
            html += "<tr class=\"td\" onclick=\"javascript:ExSelectGoods(" + item.CountDetailGoodsID + "," + 1 + "," + index + ");\">"
                        + '<td style="text-align: left">' + item.Name + '</td>'
                        + '<td style="text-align: left">' + item.Unit + '</td>'
                        + '<td style="text-align: right"><span id="txtTotalCount' + index + '">' + item.Number + '</span></td>'
                        + '</tr>';
        });
    }
    else {
        html += '<td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="5">未找到符合此条件的数据！请重试！</td>';
    }

    $("#tbServingProduct").html(html);
}
/****************************************************************************************************
*创建分页
*****************************************************************************************************/
function CreateServingProductPager(resCount) {
    var page = new CommonPager(
            "ServingProductPage",
            pageSize,
            resCount,
            currentPage,
            function (p) {
                currentPage = parseInt(p);
                ServingProductList();
            }
        );
    page.ShowSimple();
}

/****************************************************************************************************
*将左边列表中的数据放到右边列表中
*****************************************************************************************************/
var chkNoMember;
//选择商品，加入到消费购物车
function ExSelectGoods(goodsID, type, index) {
    chkNoMember = $("#chkNoMember").attr("checked");
    if (!chkNoMember && $.isEmptyObject(global_mem)) {
        art.dialog({
            icon: 'error', //图标
            content: '请先选择会员!',
            lock: true
        });
        return false;
    }
    doAjax(
               "../",
               "GetGoodsInfo",
               {
                   "goodsID": goodsID,
                   "MemLevelID": global_mem.MemLevelID == undefined ? -1 : global_mem.MemLevelID
               },
               "json",
               function (json) {
                   ExSelectGoodsOK(json, type, index, json[0].PointDiscount);
               });
}

function ExSelectGoodsOK(json, type, line, pointDiscount) {
    var index = -1;
    if (type == 0) {
        for (var i = 0; i < GoodsList.length; i++) {
            if (GoodsList[i].GoodsID == json[0].GoodsID && GoodsList[i].ExpNum > 0) {
                index = i;
            }
        }
    }
    else {
        for (var i = 0; i < GoodsList.length; i++) {
            if (GoodsList[i].GoodsID == json[0].GoodsID && GoodsList[i].ExpNum < 0) {
                index = i;
            }
        }
    }
    //这是某件商品第一次被选中销售时
    if (index == -1) {
        //如果为商品消费
        //if (type == 0) {
        //    if (json[0]["GoodsType"] == 0 && json[0]["Number"] == 0) {
        //        var strErrorMsg = "";
        //        strErrorMsg += "<li>你选择的商品超过了该商品的库存最大值！</li>";
        //        art.dialog({
        //            title: '系统提示',
        //            icon: 'error', //图标
        //            content: strErrorMsg,
        //            lock: true
        //        });
        //        return false;
        //    }
        //    json[0]["ExpDiscount"] = line;
        //    json[0]["ExpPointDiscount"] = pointDiscount;
        //    json[0]["ExpNum"] = 1;
        //    if (chkNoMember)//散客消费
        //    {
        //        json[0]["ExpPoint"] = 0; //散客消费不计积分
        //        //json[0]["ExpMoney"] = json[0].Price; //散客消费不计折扣
        //        json[0]["ExpMoney"] = accMul(json[0]["ExpDiscount"], json[0].Price);
        //    }
        //    else {
        //        json[0]["ExpMoney"] = accMul(json[0]["ExpDiscount"], json[0].Price);
        //        json[0]["ExpPoint"] = getPoint(json[0]["ExpPointDiscount"], json[0].Point, json[0]["ExpMoney"], 1);
        //    }
        //}
        ////会员计次项目
        //else {
        json[0]["Price"] = json[0].Price;
        if ($("#txtTotalCount" + line).text() == "0") {
            var strErrorMsg = "";
            strErrorMsg += "<li>你选择的次数超过了剩余次数的最大值！</li>";
            art.dialog({
                title: '系统提示',
                icon: 'error', //图标
                content: strErrorMsg,
                lock: true
            });
            return false;
        }
        json[0]["ExpNum"] = -1;
        json[0]["ExpPoint"] = 0;
        json[0]["ExpMoney"] = json[0].Price;
        json[0]["CommissionType"] = json[0].CommissionType;
        json[0]["CommissionNumber"] = json[0].CommissionNumber;
        json[0]["GoodsDiscount"] = json[0].GoodsDiscount;
        //  }

        //提成金额

        //if (staff && staffType == "False") {
        //    //按商品类型
        //    switch (json[0].CommissionType) {
        //        case "0":
        //            json[0]["ExpStaffMoney"] = 0;
        //            break;
        //        case "1":
        //            json[0]["ExpStaffMoney"] =  json[0].Price * json[0].CommissionNumber  //json[0]["ExpMoney"] * json[0].CommissionNumber;
        //            break;
        //        default:
        //            json[0]["ExpStaffMoney"] = json[0].CommissionNumber;
        //            break;
        //    }           
        //}
        ////按员工提成比例
        //else if (staff && staffType == "True")  {
        //    switch (json[0]["GoodsType"]) {
        //        case "0":
        //            json[0]["ExpStaffMoney"] = 0;
        //            break;
        //        case "1":
        //            //json[0]["ExpStaffMoney"] = json[0].CommissionNumber;
        //            json[0]["ExpStaffMoney"] = json[0].CommissionNumber;
        //            break;
        //    }
        //    json[0]["ExpStaffName"] = 0;
        //    json[0]["ExpStaffPercent"] = 0;
        //    if ($("#sltStaff").val() != "") {
        //        json[0]["ExpStaffName"] = $("#sltStaff").val();
        //        json[0]["ExpStaffPercent"] = $("#sltStaff").find("option:selected").attr("ClassPercent");

        //        json[0]["ExpStaffMoney"] = json[0]["ExpMoney"] * json[0]["ExpStaffPercent"];
        //    }
        //}
        //else if (!staff) {
        //    json[0]["ExpStaffMoney"] = 0;
        //}               
        //json[0]["OrderDetailID"] = 0;

        //alert(json[0]["ExpMoney"]);
        //alert(json[0]["ExpStaffPercent"]);
        //alert(json[0]["ExpStaffMoney"]);
        //if ($("#sltStaff").val() != "") {
        json[0]["ExpStaffName"] = "";
        //}
        GoodsList.push(json[0]);
    }
    else {
        if (type == 0) {
            //当前消费数量
            var currentNum = parseInt(GoodsList[index].ExpNum) + 1;
            var Number;
            if (isEmptyBillsExpense) {
                Number = GoodsList[index].OldNum;
            }
            else {
                Number = parseInt(GoodsList[index].Number);
            }
            if (GoodsList[index].GoodsType == 0 && currentNum > Number) {
                var strErrorMsg = "";
                strErrorMsg += "<li>你选择的商品超过了该商品的库存最大值！</li>";
                art.dialog({
                    title: '系统提示',
                    icon: 'error', //图标
                    content: strErrorMsg,
                    lock: true
                });
                return false;
            }
            //如果是散客
            if (chkNoMember) {
                var currentPoint = 0;

                var currentMoney = accMul(accMul(GoodsList[index]["Price"], line), currentNum);
            }
            else {
                //                var currentMoney = 0;
                if (GoodsList[index]["ExpDiscount"] != "0" && GoodsList[index]["ExpPointDiscount"] != "0") {
                    var currentMoney = accMul(accMul(GoodsList[index]["Price"], GoodsList[index]["ExpDiscount"]), currentNum);
                    var goodsPointDiscount = parseFloat(GoodsList[index]["ExpPointDiscount"]);
                    currentPoint = getPoint(GoodsList[index]["ExpPointDiscount"], GoodsList[index].Point, currentMoney, currentNum);
                }
                else {
                    var currentMoney = accMul(accMul(GoodsList[index]["Price"], line), currentNum);
                    var goodsPointDiscount = parseFloat(pointDiscount);
                    currentPoint = getPoint(pointDiscount, GoodsList[index].Point, currentMoney, currentNum);
                }
            }
            GoodsList[index].ExpPoint = currentPoint;
            GoodsList[index].ExpMoney = currentMoney;
            GoodsList[index].ExpNum = currentNum;

        }
        else {
            var currentNum = parseInt(GoodsList[index].ExpNum) - 1;  //当前数量
            //alert(currentNum);
            if (Math.abs(currentNum) > $("#txtTotalCount" + line).text()) {
                var strErrorMsg = "";
                strErrorMsg += "<li>你输入的次数超过了剩余次数的最大值！</li>";
                art.dialog({
                    title: '系统提示',
                    icon: 'error', //图标
                    content: strErrorMsg,
                    lock: true
                });
                return false;
            }

            var currentPoint = 0;
            var currentMoney =  Math.abs(GoodsList[index]["Price"] * GoodsList[index]["GoodsDiscount"] * currentNum );
            GoodsList[index].ExpPoint = currentPoint;
            GoodsList[index].ExpMoney = currentMoney;
            GoodsList[index].ExpNum = currentNum;
        }
        
    }
    ExGoodsBindList();
}
/****************************************************************************************************
* 切换用户时，刷新已选商品列表（相关价格需要重新计算过）
*****************************************************************************************************/
function ExSelectGoodsRefreshList() {
    var newGoodsList = new Array();
    GoodsList = newGoodsList;
    ExGoodsBindList();
}
/****************************************************************************************************
*将选择好的产品绑定到右侧列表中
*****************************************************************************************************/
function ExGoodsBindList() {
    $("#tbOrderTable").html("");
    for (var i = 0; i < GoodsList.length; i++) {
        var color = GoodsList[i].GoodsType == "0" ? "Black" : "#ff4800";
        var html = '<tr class="td" >'
        if (GoodsList[i].GoodsType == 1) {
            html += '<td style="color:#ff4800;text-align: left">' + GoodsList[i].Name + '</td>'
        }
        else {
            html += '<td style="text-align: left">' + GoodsList[i].Name + '</td>'
        }
        html += '<td><input id="txtServicePrice' + GoodsList[i].GoodsID + '" type="text" disabled="disabled"  class="border_radius common_ServiceButton border_radius2"  value="' + GoodsList[i].Price + '"/></td>'
            + '<td><input id="txtServiceNumber' + GoodsList[i].GoodsID + '"  type="text" disabled="disabled" class="border_radius common_ServiceButton border_radius3"  value="' + GoodsList[i].ExpNum + '" ></td>'
            + '<td><input id="txtServiceDiscountPrice' + GoodsList[i].GoodsID + '" disabled="disabled" type="text" class="border_radius common_ServiceButton border_radius2"  value="' + GoodsList[i].ExpMoney + '" /></td>'
            + '<td><input id="txtServicePoint' + GoodsList[i].GoodsID + '" disabled="disabled" type="text" class="border_radius common_ServiceButton border_radius3"  value="' + GoodsList[i].ExpPoint + '"/></td>'
         //员工下拉框
            + '<td id="td' + i + '"></td>'
        html += '<td class=\"listtd\" style=\"width: 60px;\"><a href="javascript:void(0);" onclick="javascript:JianShaoItem(' + GoodsList[i].GoodsID + ',' + GoodsList[i].ExpNum + ');"><img src=\"../images/Gift/abolish.png\" alt=\"减少数量\" title=\"减少数量\" /></a>  <a href="javascript:void(0);" onclick="javascript:ExDeleteItem(' + GoodsList[i].GoodsID + ',' + GoodsList[i].ExpNum + ');"><img src=\"../images/Gift/del.png\" alt=\"删除此项\" title=\"删除此项\" /></a> </td>'
        html += '</tr>';

        $("#tbOrderTable").append(html);
        //克隆下拉框
        var sltStaff = $("#sltStaff");
        var cSltStaff = sltStaff.clone();
        //改变克隆下拉框的ID 一个控件只能有一个唯一的ID
        cSltStaff.attr("id", "staff" + i);
        //将克隆的下拉框追加到<td>里
        $("#td" + i).append(cSltStaff);
        //克隆的下拉框绑定事件 foo为数据参数
        $("#staff" + i).bind("change", { foo: i }, changeStaff);
        //没有启动提成 下拉框禁用
        if (!staff) {
            //            $("#td" + i).attr("disabled", "disabled");
            $("#td" + i).css("display", "none");
        }
        else {
            //$("#td" + i).attr("disabled", "disabled");
            $("#td" + i).css("display", "");
            if ($("#sltStaff").val().toString() != "") {
                $("select[id ^=staff]").each(function () {
                    $(this).attr("value", $("#sltStaff").val().toString());
                    $(this).attr("disabled", "disabled");
                });
            }
            else {
                $("select[id ^=staff]").each(function () {
                    $(this).attr("disabled", "");
                });
            }
            $("#staff" + i).val(GoodsList[i].ExpStaffName);
        }
    }

    html = '<tr id="tdExpenseTotal"><td colspan="9" style="height:30px; line-height:30px; background-color:#ddeeff;padding:0px 5px 0px 5px;"></td></tr>';

    $("#tbOrderTable").append(html);

    ExGoodsBindTotal();
}

//选择员工 改变提成
function changeStaff(event) {
    //重新计算提成
    var obj = event.data.foo;

    if (staff && staffType == "False") {
        //按商品类型
        switch (GoodsList[obj].CommissionType) {
            case "0": //不提成
                GoodsList[obj].ExpStaffMoney = 0;
                break;
            case "1": //按商品比例
                GoodsList[obj].ExpStaffMoney = accMul(GoodsList[obj].ExpMoney, GoodsList[obj].CommissionNumber);
                break;
            default: //按商品固定金额
                GoodsList[obj].ExpStaffMoney = accMul(GoodsList[obj].CommissionNumber, GoodsList[obj].ExpNum);
                break;
        }
    }
        //按员工提成比例 
    else if (staff && staffType == "True") {
        var staffPercent = $("#staff" + obj).find("option:selected").attr("ClassPercent");

        GoodsList[obj].ExpStaffMoney = accMul(GoodsList[obj].ExpMoney, staffPercent);
        GoodsList[obj].ExpStaffPercent = staffPercent;
    }
    else if (!staff) {
        GoodsList[obj].ExpStaffMoney = 0;
    }

    GoodsList[obj].ExpStaffName = $("#staff" + obj).val();
    ExGoodsBindTotal();

}
/****************************************************************************************************
*绑定消费合计
*****************************************************************************************************/
function ExGoodsBindTotal() {
    totalNumber = totalPoint = totalMoney = totalDiscount = totalStaffMoney = 0;
    for (var i = 0; i < GoodsList.length; i++) {
        totalNumber = accAdd(totalNumber, Math.abs(GoodsList[i].ExpNum));

        totalMoney = accAdd(totalMoney, accMul(GoodsList[i].Price, GoodsList[i].ExpNum));

        totalDiscount = accAdd(totalDiscount, GoodsList[i].ExpMoney);
        if (chkNoMember) {
            totalPoint = 0;
        }
        else {
            totalPoint = accAdd(totalPoint, GoodsList[i].ExpPoint);
        }
    }
    totalStaffMoney = GetAllStaff();

    totalPoint = Math.floor(totalPoint);
    if (GoodsList.length != 0) {
        var strTotalhtml = "";
        $("#lblTotalNumber").html(totalNumber);
        $("#lblTotalMoney").html(Math.abs(totalMoney));
        $("#lblTotalDiscountMoney").html(totalDiscount);
        $("#lblTotalPoint").html(totalPoint);
        $("#lblStaffMoney").html(totalStaffMoney);
    }
    else {
        $("#lblTotalNumber").html("0");
        $("#lblTotalMoney").html("0");
        $("#lblTotalDiscountMoney").html("0");
        $("#lblTotalPoint").html("0");
        $("#lblStaffMoney").html("0");
        $("#tbOrderTable").html('<td colspan="8" style="height:30px; line-height:30px;padding:0px 5px 0px 5px;">请点击左侧商品列表，选择需要消费的商品！</td>');
    }
}
//得到提成总金额 整单重新计算
function GetAllStaff() {
    var dclTotalStaffMoney = 0;
    for (var i = 0; i < GoodsList.length; i++) {
        //如果启动提成
        if (staff) {
            //按照员工比例
            if (staffType == "True") {
                GoodsList[i].ExpStaffMoney = accMul(GoodsList[i].ExpMoney, GoodsList[i].ExpStaffPercent)
                dclTotalStaffMoney = accAdd(dclTotalStaffMoney, GoodsList[i].ExpStaffMoney);
            }
            //按照商品提成类型
            if ($("#staff" + i).val() != "") {
                if (staffType == "False") {
                    switch (GoodsList[i].CommissionType) {
                        case "0":
                            GoodsList[i].ExpStaffMoney = 0;
                            break;
                        case "1":
                            GoodsList[i].ExpStaffMoney = accMul(GoodsList[i].ExpMoney, GoodsList[i].CommissionNumber);
                            break;
                        default:
                            GoodsList[i].ExpStaffMoney = Math.abs(accMul(GoodsList[i].CommissionNumber, GoodsList[i].ExpNum));
                            break;
                    }
                    dclTotalStaffMoney = accAdd(dclTotalStaffMoney, GoodsList[i].ExpStaffMoney);
                }
                if ($("#sltStaff").val() != "") {
                    GoodsList[i].ExpStaffName = $("#sltStaff").val();
                }
            }
        }
            //不启动提成
        else {
            dclTotalStaffMoney = 0;
        }
    }
    return dclTotalStaffMoney;
}

/****************************************************************************************************
*更改数量 重新计算合计
*****************************************************************************************************/
function ExUpdateNumber(goodsID, type) {
    var intNumber = (type == 0) ? $("#txtNumber" + goodsID).val() : $("#txtServiceNumber" + goodsID).val();
    if (intNumber == "" || intNumber == "0") {
        intNumber = "1";
    }

    //    var index = -1;
    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].GoodsID == goodsID && GoodsList[i].ExpNum > 0) {
            //            index = i;
            if (GoodsList[i].GoodsType == 0) {
                var Number;
                if (isEmptyBillsExpense) {
                    Number = GoodsList[i].OldNum;
                }
                else {
                    Number = parseInt(GoodsList[i].Number);
                }
                if (parseInt(intNumber) > Number) {
                    var strErrorMsg = "";
                    strErrorMsg += "<li>你选择的商品超过了该商品的库存最大值！</li>";
                    art.dialog({
                        title: '系统提示',
                        icon: 'error', //图标
                        content: strErrorMsg,
                        lock: true
                    });
                    $("#txtNumber" + goodsID).val(GoodsList[i].ExpNum);
                    return false;
                }
                if (!intNumber.IsNumber()) {
                    art.dialog({
                        title: '系统提示',
                        icon: 'error', //图标
                        content: '购买商品的数量只能为数字且大于0,请重新输入',
                        lock: true
                    });
                    intNumber = 1;
                    GoodsList[i].ExpNum = 1;
                    if (chkNoMember) { //散客消费
                        GoodsList[i].ExpPoint = 0;
                        //GoodsList[i].ExpMoney = accMul(GoodsList[i].Price, intNumber);
                        GoodsList[i].ExpMoney = accMul(accMul(GoodsList[i].Price, GoodsList[i].ExpDiscount), intNumber);
                    }
                    else {
                        GoodsList[i].ExpMoney = parseFloat(accMul(accMul(GoodsList[i].Price, intNumber), GoodsList[i].ExpDiscount)).toFixed(2);
                        GoodsList[i].ExpPoint = getPoint(GoodsList[i]["ExpPointDiscount"], GoodsList[i].Point, GoodsList[i].ExpMoney, intNumber);
                    }
                    $("#txtNumber" + goodsID).val(intNumber);
                    $("#txtDiscountPrice" + goodsID).val(GoodsList[i].ExpMoney);
                    $("#txtPoint" + goodsID).val(GoodsList[i].ExpPoint);
                    ExGoodsBindTotal();
                    return false;

                }
            }
            GoodsList[i].ExpNum = intNumber;
            break;
        }
    }
    if (chkNoMember) { //散客消费
        GoodsList[i].ExpPoint = 0;
        //GoodsList[i].ExpMoney = accMul(GoodsList[i].Price, intNumber);
        GoodsList[i].ExpMoney = accMul(accMul(GoodsList[i].Price, GoodsList[i].ExpDiscount), intNumber);
    }
    else {
        GoodsList[i].ExpMoney = parseFloat(accMul(accMul(GoodsList[i].Price, intNumber), GoodsList[i].ExpDiscount)).toFixed(2);
        GoodsList[i].ExpPoint = getPoint(GoodsList[i]["ExpPointDiscount"], GoodsList[i].Point, GoodsList[i].ExpMoney, intNumber);
    }
    $("#txtNumber" + goodsID).val(intNumber);
    $("#txtDiscountPrice" + goodsID).val(GoodsList[i].ExpMoney);
    $("#txtPoint" + goodsID).val(GoodsList[i].ExpPoint);
    ExGoodsBindTotal();
}
/****************************************************************************************************
*刪除单个产品
*****************************************************************************************************/
function ExDeleteItem(goodsID, goodsNumber) {
    var newGoodsList = new Array();
    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].GoodsID != goodsID) {
            newGoodsList.push(GoodsList[i]);
        }
        else {
            if (GoodsList[i].ExpNum != goodsNumber) {
                newGoodsList.push(GoodsList[i]);
            }
        }
    }
    GoodsList = newGoodsList;
    ExGoodsBindList();
}

/****************************************************************************************************
*减少单个产品的数量
*****************************************************************************************************/
function JianShaoItem(goodsID, goodsNumber) {
    //只有一个商品的时候  不可以减少
    if (goodsNumber != -1) {
        for (var i = 0; i < GoodsList.length; i++) {
            if (GoodsList[i].GoodsID == goodsID) {
                if (GoodsList[i].ExpNum = goodsNumber) {
                    GoodsList[i].ExpNum += 1;
                }
            }
        }
        ExGoodsBindList();
    }
    else {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: "产品数量不可少于一件！",
            time: 3,
            lock: true
        });
    }
}

/****************************************************************************************************
*选择散客是响应事件
*****************************************************************************************************/
function ClkchkNoMember() {
    var newGoodsList = new Array();
    GoodsList = newGoodsList;
    var clearglobal_mem = new Array();
    global_mem = clearglobal_mem;

    $("#tab0").click();
    ExGoodsBindList();
    $("#sltStaff").val("");

}
/****************************************************************************************************
*挂单按钮响应事件
*****************************************************************************************************/
function btnEntryOrder() {
    var strErrorMsg = "";
    var strOrderCode = $.trim($("#spOrderAccount").html());
    staff = $("#chkStaff").attr("checked");
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>散客不允许挂单，请先选择会员再进行挂单操作！</li>";
    }
    if (GoodsList.length == 0) {
        strErrorMsg += "<li>请在左侧列表中选择需要消费的商品！</li>";
    }
    var num = 0;
    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].ExpNum < 0) {
            num = 1;
        }
    }
    if (num != 0) {
        strErrorMsg += "<li>消费商品中含有计次的服务类商品，不可挂单！</li>";
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

    art.dialog({
        content: '正在进行挂单操作，是否挂单？',
        ok: function () {
            var parameter = new Array();
            parameter.push({ "payType": "EmptyBills", "IsCard": false, "IsCash": false, "IsBink": false, "CardMoney": 0, "CashMoney": 0, "CouponMoney": 0, "BinkMoney": 0, "DiscountMoney": parseFloat(totalDiscount) });
            ExpenseOK(parameter);
        },
        cancelVal: '取消',
        cancel: true
    });
}
/****************************************************************************************************
*结算按钮响应事件
*****************************************************************************************************/
function ExpenseSave() {
    var strErrorMsg = "";
    var chkAllowPwd = $("#chkAllowPwd").attr("checked");
    chkNoMember = $("#chkNoMember").attr("checked");
    staff = $("#chkStaff").attr("checked");
    var strOrderCode = $.trim($("#spOrderAccount").html());
    if (GoodsList.length == 0) {
        strErrorMsg += "<li>请在左侧列表中选择需要消费的商品！</li>";
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

    if (GoodsList.length > 0 && totalDiscount == 0) {
        var parameter = new Array();
        parameter.push({ "payType": "CountExpense", "IsCard": false, "IsCash": false, "IsBink": false, "CardMoney": 0, "CashMoney": 0, "CouponMoney": 0, "BinkMoney": 0, "DiscountMoney": 0, "ChangeMoney": 0 });
        ExpenseOK(parameter);
        return;
    }
    ShowPay("CountExpense", global_mem.MemMoney, global_mem.MemPoint, totalDiscount, strOrderCode, chkAllowPwd);
}

function ExpenseOK(parameter) {
    //获取打印份数
    var PointNum = $("#PointNum").val();

    doAjax("../",
            "CountExpense",
            {
                "memID": chkNoMember ? "0" : global_mem.MemID,
                "parameter": parameter,
                "totalMoney": totalMoney,
                "staffMoney": totalStaffMoney,
                "point": totalPoint,
                "number": totalNumber,
                "orderCode": $.trim($("#spOrderAccount").html()),
                "remark": $("#txtExRemark").val(),
                "print": $("#chkPrint").attr("checked"),
                "sendSMS": $("#chkSMS").attr("checked"),
                "staff": $("#chkStaff").attr("checked"),
                "data": GoodsList,
                "count": GoodsList.length,
                "expensetime": $("#txtExpenseTime").val(),
                "staffName": $("#sltStaff").val(),
                "isEmptyBillsExpense": isEmptyBillsExpense,
                "orderID": order,
                "isMemCountExpense": 1
            },
            "json",
            function (json) {
                switch (json) {
                    case 0:
                        art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统异常，未保存数据，请再次点击保存！"),
                                     lock: true
                                 });
                        break;
                    case -1:
                        art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统错误 请与系统管理员联系！"),
                                     lock: true
                                 });
                        break;
                    case -2:
                        art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: ("消费成功！短信余额不足，不能发送短信，请充值短信！"),
                                  close: function () { PrintMemCountExpense($("#lblExpenseUSer").html(), global_mem, GoodsList, parameter, $("#chkPrint").attr("checked"), PointNum); }
                              });
                        break;
                    case -3:
                        art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: '会员挂单成功！',
                                  close: function () { location.href = location.href; }
                              });
                        break;
                    default:
                        art.dialog
                                ({
                                    title: '系统提示',
                                    time: 2,
                                    content: '消费成功！' + json.strUpdateMemLevel,
                                    close: function () { PrintMemCountExpense($("#lblExpenseUSer").html(), global_mem, GoodsList, parameter, $("#chkPrint").attr("checked"), PointNum); }
                                });
                        break;
                }
            });
}

/****************************************************************************************************
*在选择好会员时可以执行回调函数
*****************************************************************************************************/
function FindMember_CallBack() {
    var strErrorMsg;
    if (global_mem.MemState != 0) {
        strErrorMsg = "当前会员卡处于锁定或者挂失状态，暂不允许进行消费。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    if (global_mem.MemIsPast == "True") {
        strErrorMsg = "当前会员卡已过期，暂不允许进行消费。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    ServingProductList();
    return true;
}