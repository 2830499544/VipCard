/*************************************************************************************************************
商品消费页面JS处理
**************************************************************************************************************/
var expenseTab;
var pageSize = 10;
var currentPage = 1;
var key = "", keyBarCode = "";       //搜索的值
var totalNumber = 0, totalPoint = 0, totalMoney = 0, totalDiscount = 0, totalStaffMoney = 0; //汇总数据初始化
var GoodsList = new Array();
var levelPoint, levelPercent;       //会员消费兑换积分比例 会员等级比例
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

    //普通商品
    GoodsProductList();
    //搜索按钮响应的事件
    $("#btnGoodsSearch").bind("click", btnGoodsSearch);
    //消费时间绑定日期控件
    $('#txtExpenseTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm', maxDate: '%y-%M-%d %H:%m:%s', isShowClear: false, readOnly: true });
    });
    //选择散客响应的事件
    $("#chkNoMember").bind("click", ClkchkNoMember);
    //结算按钮响应的事件
    $("#btnExpenseSave").bind("click", ExpenseSave);
    //挂单按钮响应的事件
    $("#btnEntryOrder").bind("click", btnEntryOrder);

    //根据是否启动员工提成 来显示或隐藏员工下拉框
    if (!staff) {
        $("#sltStaff").attr("disabled", "disabled");
    }
    else {
        $("#sltStaff").attr("disabled", "");
        //下拉框选择响应事件     
        $("#sltStaff").bind("change", changeAllStaff);
    }
    $("#findTable").removeAttr("style");
    $("#findTable").css("width", "100%");
    $("#tbTop").css("width", "100%");
    $("#tbBody").css("width", "100%");

    if ($("#MemCard").val() != null && $("#MemCard").val() != "0") {
        $("#txtFindMember").val($("#MemCard").val());
    }
    else {
        $("#chkNoMember").attr("checked", "checked");
        chkNoMember = true;
        isEmptyBillsExpense = true;
        order = $("#txtOrderID").val();
        GetExpenseDetail(order);
    }

    $("#txtGoodsExpenseCode").keyup(function (e) {
        var keyAscii = window.event ? e.keyCode : e.which;
        if (keyAscii == 13 && $.trim($("#txtGoodsExpenseCode").val()) != "") {
            keyBarCode = $("#txtGoodsExpenseCode").val();
            SelectGoodsByBarCode();
        }
    });
});

//尝试读取挂单信息
function GetExpenseDetail(orderID) {
    doAjax(
        "../",
        "GetOrderEmptyBills",
        { "orderID": orderID },
        "json",
        function (json) {
            if (json != null) {
                $.each(json, function (index, item) {
                    var obj = {}
                    obj["ExpPoint"] = item.OrderDetailPoint;
                    obj["ExpMoney"] = item.OrderDetailDiscountPrice;
                    obj["ExpNum"] = item.OrderDetailNumber;
                    obj["CommissionType"] = item.CommissionType;
                    obj["CommissionNumber"] = item.CommissionNumber;
                    obj["ExpStaffMoney"] = 0;
                    obj["ExpStaffName"] = 0;
                    obj["ExpStaffPercent"] = 0;
                    obj["GoodsType"] = item.GoodsType;
                    obj["Price"] = item.Price;
                    obj["GoodsID"] = item.GoodsID;
                    obj["GoodsCode"] = item.GoodsCode;
                    obj["Name"] = item.Name;
                    obj["Number"] = item.Number;
                    obj["Point"] = item.Point;
                    obj["ExpDiscount"] = item.ClassDiscountPercent == "" ? "1.00" : item.ClassDiscountPercent;
                    obj["ExpPointDiscount"] = item.ClassPointPercent == "" ? "9999999999" : item.ClassPointPercent;
                    obj["OrderDetailID"] = item.OrderDetailID;
                    obj["OldNum"] = parseInt(item.OrderDetailNumber) + parseInt(item.Number);
                    GoodsList.push(obj);
                });
                ExGoodsBindList();
            }
        }
     );
}

//扫描枪快速定位
function SelectGoodsByBarCode() {
    if ($("#chkNoMember").attr("checked")) {
        isShanKe = true;
    }
    else {
        if ($.isEmptyObject(global_mem)) {
            art.dialog({
                icon: 'error', //图标
                content: '请先选择会员!',
                lock: true
            });
            $("#txtFindMember").select();
            $("#txtGoodsExpenseCode").val("");
            return false;
        }
    }
    doAjax(
        "../",
        "GetGoodsList",
            {
                "size": 1,
                "index": 1,
                "key": keyBarCode,
                "shopID": $("#ShopID").val(),
                "bolGoodsExpense": true,
                "ClassID": "",
                "IsCheckStock": false, //是否检查库存；库存要大于0
                "MemLevelID": global_mem.MemLevelID == undefined ? -1 : global_mem.MemLevelID
            },
        "json",
        function (json) {
            if (json != null) {
                //                var goodsDiscount = calculate(json.List[0]); //获得商品的折扣率
                //                var goodsDiscountPrcie = accMul(json.List[0].Price, goodsDiscount).toFixed(2); //计算商品的折后金额
                //                var pointDiscount = pointCalculate(json.List[0]); //计算商品的积分比例
                var goodsDiscount = json.List[0].GoodsDiscount;
                var goodsDiscountPrcie = json.List[0].DiscountPrice;
                var pointDiscount = json.List[0].PointDiscount;
                ExSelectGoods(json.List[0].GoodsID, 0, goodsDiscount, pointDiscount);
            }
            $("#txtGoodsExpenseCode").select();
            $("#RechargeCount_BarCode").select();
        }
     );
}

function pageHeight() {
    if ($.browser.msie) {
        return document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight :
document.body.clientHeight;
    } else {
        return self.innerHeight;
    }
};
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
    key = $("#txtGoodsCode").val()
    currentPage = 1;
    GoodsProductList();
}

/****************************************************************************************************
*输入商品名称或简码后  回车响应事件
*****************************************************************************************************/
function getKey() {
    if (event.keyCode == 13) {
        key = $("#txtGoodsCode").val()
        currentPage = 1;
        GoodsProductList();
    }
}

/****************************************************************************************************
*获取商品
*****************************************************************************************************/
function GoodsProductList() {
    doAjax(
           "../",
           "GetGoodsList",
           {
               "size": pageSize,
               "index": currentPage,
               "key": key,
               "shopID": $("#ShopID").val(),
               "bolGoodsExpense": true,
               "ClassID": "",
               "IsCheckStock": true, //是否检查库存；库存要大于0
               "MemLevelID": global_mem.MemLevelID == undefined ? -1 : global_mem.MemLevelID
           },
           "json",
           function (json) {
               CreateGoodsProductTable(json.List);
               CreateGoodsProductPager(json.RecordCount);
           });
}
/****************************************************************************************************
*创建表格
*****************************************************************************************************/
function CreateGoodsProductTable(obj) {
    var html = '';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            var goodsDiscount = item.GoodsDiscount;
            var goodsDiscountPrcie = item.DiscountPrice;
            var pointDiscount = item.PointDiscount;
            //            var goodsDiscount = calculate(item); //获得商品的折扣率
            //            var goodsDiscountPrcie = accMul(item.Price, goodsDiscount).toFixed(2); //计算商品的折后金额
            //            var pointDiscount = pointCalculate(item); //计算商品的积分比例
            html += "<tr class=\"td\" onclick=\"javascript:ExSelectGoods(" + item.GoodsID + "," + 0 + "," + goodsDiscount + "," + pointDiscount + ");\">"
            if (item.GoodsType == 0) {
                html += '<td style="text-align: left">' + item.Name + '</td>'
            }
            else {
                html += '<td style="text-align: left;color:red" >' + item.Name + '</td>'
            }
            html += '<td style="text-align: right"><b>' + parseFloat(item.Price).toFixed(2) + '</b></td>'
                 + '<td style="text-align: right">' + item.Number + '</td>'
            //            if (item.Point != -1) {
            //                html += '<td>' + item.Point + '</td>'
            //            }
            //            else {
            //                html += '<td>不积分</td>'
            //            }
            html += '<td style="text-align: right">' + goodsDiscountPrcie + '</td>';
            html += '</tr>';
        });
    }
    else {
        html += '<td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="5">未找到符合此条件的数据！请重试！</td>';
    }

    $("#tbGoodsProduct").html(html);
}
/****************************************************************************************************
*根据商品类型ID获取名称
*****************************************************************************************************/
function GetGoodsType(goodsType) {
    var strGoorsType;
    if (goodsType == 0) {
        strGoorsType = "普通商品";
    }
    else {
        strGoorsType = "服务商品";
    }
    return strGoorsType;
}

/****************************************************************************************************
*分页
*****************************************************************************************************/
function CreateGoodsProductPager(resCount) {
    var page = new CommonPager(
        "GoodsProductPage",
        pageSize,
        resCount,
        currentPage,
        function (p) {
            currentPage = parseInt(p);
            GoodsProductList();
        }
    );
    page.ShowSimple();
}

/****************************************************************************************************
*将左边列表中的数据放到右边列表中
*****************************************************************************************************/
var chkNoMember;
//选择商品，加入到消费购物车
function ExSelectGoods(goodsID, type, index, pointDiscount) {
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
                   ExSelectGoodsOK(json, type, index, pointDiscount);
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
        if (type == 0) {
            if (json[0]["GoodsType"] == 0 && json[0]["Number"] == 0) {
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
            json[0]["ExpDiscount"] = line;
            json[0]["ExpPointDiscount"] = pointDiscount;
            if (json[0].GoodsType == 0) {
                if (parseFloat(json[0].Number) < (1.00)) {
                    json[0]["ExpNum"] = parseFloat(_sys_check.defval);
                } else {
                    json[0]["ExpNum"] = 1;
                }
            } else {
                json[0]["ExpNum"] = 1;
            }
            if (chkNoMember)//散客消费
            {
                json[0]["ExpPoint"] = 0; //散客消费不计积分
                json[0]["ExpMoney"] = accMul(accMul(json[0]["ExpDiscount"], json[0].Price), json[0]["ExpNum"]);
            }
            else {
                json[0]["ExpMoney"] = accMul(accMul(json[0]["ExpDiscount"], json[0].Price), json[0]["ExpNum"]);
                //如果商品设置了积分，按照商品积分计算
                if (Math.floor(json[0].Point) > 0)
                    json[0]["ExpPoint"] = Math.floor(json[0].Point);
                else
                    json[0]["ExpPoint"] = Math.floor(accDiv(json[0]["ExpMoney"], json[0]["ExpPointDiscount"]));
            }
        }
//        //会员计次项目
//        else {
//            json[0]["Price"] = 0;
//            if ($("#txtTotalCount" + line).text() == "0") {
//                var strErrorMsg = "";
//                strErrorMsg += "<li>你选择的次数超过了剩余次数的最大值！</li>";
//                art.dialog({
//                    title: '系统提示',
//                    icon: 'error', //图标
//                    content: strErrorMsg,
//                    lock: true
//                });
//                return false;
//            }
//            json[0]["ExpNum"] = -1;
//            json[0]["ExpPoint"] = 0;
//            json[0]["ExpMoney"] = 0;
//            json[0]["CommissionType"] = json[0].CommissionType;
//            json[0]["CommissionNumber"] = json[0].CommissionNumber;
//        }
        //提成金额

        if (staff && staffType == "False") {
            //按商品类型
            switch (json[0].CommissionType) {
                case "0":
                    json[0]["ExpStaffMoney"] = 0;
                    break;
                case "1":
                    json[0]["ExpStaffMoney"] = json[0]["ExpMoney"] * json[0].CommissionNumber;
                    break;
                default:
                    json[0]["ExpStaffMoney"] = json[0].CommissionNumber;
                    break;
            }
        }
        //按员工提成比例
        else if (staff && staffType == "True") {
            switch (json[0]["GoodsType"]) {
                case "0":
                    json[0]["ExpStaffMoney"] = 0;
                    break;
                case "1":
                    //json[0]["ExpStaffMoney"] = json[0].CommissionNumber;
                    json[0]["ExpStaffMoney"] = json[0].CommissionNumber;
                    break;
            }
        }
        else if (!staff) {
            json[0]["ExpStaffMoney"] = 0;
        }
        json[0]["ExpStaffName"] = 0;
        json[0]["ExpStaffPercent"] = 0;
        if ($("#sltStaff").val() != "") {
            json[0]["ExpStaffName"] = $("#sltStaff").val();
            json[0]["ExpStaffPercent"] = $("#sltStaff").find("option:selected").attr("ClassPercent");
            json[0]["ExpStaffMoney"] = json[0]["ExpMoney"] * json[0]["ExpStaffPercent"];
        }

        json[0]["OrderDetailID"] = 0;
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
                if (GoodsList[index]["ExpDiscount"] != "0" && GoodsList[index]["ExpPointDiscount"] != "0") {
                    var currentMoney = accMul(accMul(GoodsList[index]["Price"], GoodsList[index]["ExpDiscount"]), currentNum);
                }
                else {
                    var currentMoney = accMul(accMul(GoodsList[index]["Price"], line), currentNum);
                }
                //如果商品设置了积分，按照商品积分计算
                if (Math.floor(GoodsList[index].Point) > 0) {
                    currentPoint = Math.floor(accMul(GoodsList[index].Point, currentNum));
                }
                else {
                    currentPoint = Math.floor(accDiv(currentMoney, GoodsList[index]["ExpPointDiscount"]));
                }
            }
            GoodsList[index].ExpPoint = currentPoint;
            GoodsList[index].ExpMoney = currentMoney;
            GoodsList[index].ExpNum = currentNum;
        }
        else {
            var currentNum = parseInt(GoodsList[index].ExpNum) - 1;
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
            var currentMoney = 0;
            GoodsList[index].ExpPoint = currentPoint;
            GoodsList[index].ExpMoney = currentMoney;
            GoodsList[index].ExpNum = currentNum;
        }
        //重新计算提成
        if (staff && staffType == "False") {
            //按商品类型
            switch (GoodsList[index].CommissionType) {
                case "0":
                    GoodsList[index].ExpStaffMoney = 0;
                    break;
                case "1":
                    GoodsList[index].ExpStaffMoney = accMul(GoodsList[index].ExpMoney, GoodsList[index].CommissionNumber);
                    break;
                default:
                    GoodsList[index].ExpStaffMoney = Math.abs(accMul(GoodsList[index].CommissionNumber, GoodsList[index].ExpNum));
                    break;
            }
        }
        //按员工提成比例
        else if (staff && staffType == "True") {
            switch (GoodsList[index].GoodsType) {
                case "0":
                    GoodsList[index].ExpStaffMoney = 0;
                    break;
                case "1":
                    GoodsList[index].ExpStaffMoney = Math.abs(accMul(GoodsList[index].CommissionNumber, GoodsList[index].ExpNum));
                    break;
            }
        }
        else if (!staff) {
            GoodsList[index].ExpStaffMoney = 0;
        }
        if ($("#sltStaff").val() != "") {
            GoodsList[index].ExpStaffName = $("#sltStaff").val();
            //            GoodsList[index].["ExpStaffPercent"] = $("#sltStaff").find("option:selected").attr("ClassPercent");
            GoodsList[index].ExpStaffMoney = accMul(GoodsList[index].ExpMoney, GoodsList[index].ExpStaffPercent);
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
        if (GoodsList[i].ExpNum > 0) {
            html += '<td><input id="txtPrice' + GoodsList[i].GoodsID + '"  type="text" class="border_radius common_ServiceButton border_radius2" style="text-align: right" value="' + parseFloat(GoodsList[i].Price).toFixed(2) + '" onkeyup="javascript:ExUpdatePrice(' + GoodsList[i].GoodsID + ',' + 0 + ');" onclick=\"javascript:this.select();\"/></td>'
            + '<td><input id="txtNumber' + GoodsList[i].GoodsID + '" type="text" class="border_radius common_ServiceButton border_radius3" style="text-align: right" value="' + GoodsList[i].ExpNum + '" onkeyup="javascript:ExUpdateNumber(' + GoodsList[i].GoodsID + ',' + 0 + ');" onclick=\"javascript:this.select();\"/></td>'
            + '<td><input id="txtDiscountPrice' + GoodsList[i].GoodsID + '" type="text" class="border_radius common_ServiceButton border_radius2" style="text-align: right" value="' + parseFloat(GoodsList[i].ExpMoney).toFixed(2) + '" onkeyup="javascript:ExUpdateDiscountPrice(' + GoodsList[i].GoodsID + ',' + 0 + ');" onclick=\"javascript:this.select();\"/></td>'
            + '<td><input id="txtPoint' + GoodsList[i].GoodsID + '" type="text" class="border_radius common_ServiceButton border_radius3" style="text-align: right" value="' + GoodsList[i].ExpPoint + '" onkeyup="javascript:ExUpdatePoint(' + GoodsList[i].GoodsID + ',' + 0 + ');" onclick=\"javascript:this.select();\"/></td>'
            //员工下拉框
            + '<td id="td' + i + '"></td>'
           
            html += '<td class=\"listtd\" style=\"width: 30px;\"><a href="javascript:void(0);" onclick="javascript:ExDeleteItem(' + GoodsList[i].GoodsID + ',' + GoodsList[i].ExpNum + ');"><img src=\"../images/Gift/del.png\" alt=\"删除此项\" title=\"删除此项\" /></a></td>'
        }
        else {
            //html += '<td style="color:#ff4800">会员计次</td>'
            html += '<td><input id="txtServicePrice' + GoodsList[i].GoodsID + '" type="text" disabled="disabled"  class="border_radius common_ServiceButton border_radius2"  value="' + GoodsList[i].Price + '" onkeyup="javascript:ExUpdatePrice(' + GoodsList[i].GoodsID + ',' + 1 + ');" onclick=\"javascript:this.select();\"/></td>'
            + '<td><input id="txtServiceNumber' + GoodsList[i].GoodsID + '" disabled="disabled" type="text" class="border_radius common_ServiceButton border_radius3"  value="' + GoodsList[i].ExpNum + '" onkeyup="javascript:ExUpdateNumber(' + GoodsList[i].GoodsID + ',' + 1 + ');" onclick=\"javascript:this.select();\"/></td>'
            + '<td><input id="txtServiceDiscountPrice' + GoodsList[i].GoodsID + '" disabled="disabled" type="text" class="border_radius common_ServiceButton border_radius2"  value="' + GoodsList[i].ExpMoney + '" onkeyup="javascript:ExUpdateDiscountPrice(' + GoodsList[i].GoodsID + ',' + 1 + ');" onclick=\"javascript:this.select();\"/></td>'
            + '<td><input id="txtServicePoint' + GoodsList[i].GoodsID + '" disabled="disabled" type="text" class="border_radius common_ServiceButton border_radius3"  value="' + GoodsList[i].ExpPoint + '" onkeyup="javascript:ExUpdatePoint(' + GoodsList[i].GoodsID + ',' + 1 + ');" onclick=\"javascript:this.select();\"/></td>'
            //员工下拉框
            + '<td id="td' + i + '"></td>'
            
            html += '<td class=\"listtd\" style=\"width: 30px;\"><a href="javascript:void(0);" onclick="javascript:ExDeleteItem(' + GoodsList[i].GoodsID + ',' + GoodsList[i].ExpNum + ');"><img src=\"../images/Gift/del.png\" alt=\"删除此项\" title=\"删除此项\" /></a></td>'
        }
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
            //如果选择整单提成 克隆的控件全部禁用
            if (isChangeStaff == 1) {
                $("#td" + i).attr("disabled", "disabled");
                //$("#staff" + i).val(GoodsList[i].ExpStaffName);
                $("#staff" + i).attr("disabled", "disabled");
            }
            else {
                //$("#td" + i).attr("disabled", "disabled");
                $("#td" + i).attr("disabled", "");
                //$("#staff" + i).attr("value", "");
                $("#staff" + i).attr("disabled", "");
            }
        }
    }

    html = '<tr id="tdExpenseTotal"><td colspan="8" style="height:30px; line-height:30px; background-color:#ddeeff;padding:0px 5px 0px 5px;"></td></tr>';

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
            //        switch (GoodsList[obj].GoodsType) {
            //            case "0":
            //                GoodsList[obj].ExpStaffMoney = accMul(GoodsList[obj].ExpMoney, staffPercent);
            //                break;
            //            case "1":
            //                GoodsList[obj].ExpStaffMoney = Math.abs(accMul(GoodsList[obj].CommissionNumber, GoodsList[obj].ExpNum));
            //                break;
            //        }
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

        totalMoney = accAdd(totalMoney, accMul(parseFloat(GoodsList[i].Price), parseFloat(GoodsList[i].ExpNum)));

        totalDiscount = accAdd(parseFloat(totalDiscount), parseFloat(GoodsList[i].ExpMoney));
        if (chkNoMember) {
            totalPoint = 0;
        }
        else {
            totalPoint = accAdd(totalPoint, GoodsList[i].ExpPoint);
        }
        //totalStaffMoney = accAdd(totalStaffMoney, GoodsList[i].ExpStaffMoney);
    }


    //    var temp = 0;
    //    for (var i = 0; i < GoodsList.length; i++) {
    //        if (Math.floor(GoodsList[i].ExpMoney) != GoodsList[i].ExpPoint) {
    //            temp += 1;
    //        }
    //    }
    //    if (temp == 0) {
    //        totalPoint = Math.floor(totalDiscount);
    //    }

    totalStaffMoney = GetAllStaff();
    totalPoint = Math.floor(totalPoint);
    if (GoodsList.length != 0) {
        var strTotalhtml = "";
        $("#lblTotalNumber").html(totalNumber);
        $("#lblTotalMoney").html(totalMoney);
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
//                if ($("#sltStaff").val() == "") {
//                    if (GoodsList[i].GoodsType == "1") {
//                        dclTotalStaffMoney = accAdd(dclTotalStaffMoney, Math.abs(accMul(GoodsList[i].CommissionNumber, GoodsList[i].ExpNum)));
//                    }
//                    else {
//                        dclTotalStaffMoney = accAdd(dclTotalStaffMoney, accMul(GoodsList[i].ExpMoney, GoodsList[i].ExpStaffPercent));
//                    }
//                }
//                else {
//                    if (GoodsList[i].GoodsType == "1") {
//                        dclTotalStaffMoney = accAdd(dclTotalStaffMoney, Math.abs(accMul(GoodsList[i].CommissionNumber, GoodsList[i].ExpNum)));
//                    }
//                    else {
//                        dclTotalStaffMoney = accAdd(dclTotalStaffMoney, accMul(GoodsList[i].ExpMoney, staffPercent));
//                        GoodsList[i].ExpStaffMoney = accMul(GoodsList[i].ExpMoney, staffPercent);
//                    }
                //                }
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
*更改单价 重新计算合计
*****************************************************************************************************/
function ExUpdatePrice(goodsID, type) {
    var decPrice = 0;
    if (type == 0) {
        decPrice = $("#txtPrice" + goodsID).val();
        //    var index = -1;
    }
    else {
        decPrice = $("#txtServicePrice" + goodsID).val();
    }
    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].GoodsID == goodsID && GoodsList[i].ExpNum > 0) {
            //            index = i;
            if (parseInt(decPrice) < 0) {
                art.dialog({
                    icon: 'error', //图标
                    content: '商品单价不能小于零',
                    lock: true
                });
                decPrice = GoodsList[i].Price;
            }
            GoodsList[i].Price = decPrice;
            break;
        }
    }
    if (chkNoMember)//散客消费
    {
        GoodsList[i].ExpPoint = 0;
        //GoodsList[i].ExpMoney = accMul(decPrice, GoodsList[i].ExpNum);
        GoodsList[i].ExpMoney = accMul(accMul(decPrice, GoodsList[i].ExpDiscount), GoodsList[i].ExpNum);
    }
    else {
        GoodsList[i].ExpMoney = parseFloat(accMul(accMul(decPrice, GoodsList[i].ExpDiscount), GoodsList[i].ExpNum)).toFixed(2);
        // GoodsList[i].ExpPoint = Math.floor(accDiv(GoodsList[i].ExpMoney, GoodsList[i]["ExpPointDiscount"]));
        if (Math.floor(GoodsList[i].Point) > 0) {
            GoodsList[i].ExpPoint = Math.floor(accMul(GoodsList[i].Point, GoodsList[i].ExpNum));
        }
        else {
            GoodsList[i].ExpPoint = Math.floor(accDiv(GoodsList[i].ExpMoney, GoodsList[i]["ExpPointDiscount"]));
        }
    }
    //    //重新计算提成
    //    GetStaffMoney(i);
    //    GoodsList[i].ExpStaffMoney = accMul(GoodsList[i].ExpNum, GoodsList[i].StaffMoney);
    if (type == 0) {
        $("#txtPrice" + goodsID).val(decPrice);
        $("#txtDiscountPrice" + goodsID).val(GoodsList[i].ExpMoney);
        $("#txtPoint" + goodsID).val(GoodsList[i].ExpPoint);
        ExUpdatePoint(goodsID, type);
    }
    ExGoodsBindTotal();
}
/****************************************************************************************************
*更改数量 重新计算合计
*****************************************************************************************************/
function ExUpdateNumber(goodsID, type) {
    var intNumber = parseFloat((type == 0) ? sys_num_checked.product_num($.trim($("#txtNumber" + goodsID).val())) : $("#txtServiceNumber" + goodsID).val());
    var oldval = (type == 0) ? $.trim($("#txtNumber" + goodsID).val()) : $("#txtServiceNumber" + goodsID).val();
    if (intNumber == "" || intNumber == "0") {
        intNumber = "0";
    }
    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].GoodsID == goodsID && GoodsList[i].ExpNum >= 0) {
            if (GoodsList[i].GoodsType == 0) {
                var Number;
                if (isEmptyBillsExpense) {
                    Number = GoodsList[i].OldNum;
                }
                else {
                    Number = parseFloat(sys_num_checked.product_num(GoodsList[i].Number));
                }
                if (parseFloat(intNumber) > Number) {
                    var strErrorMsg = "";
                    strErrorMsg += "<li>你选择的商品超过了该商品的库存最大值！</li>";
                    art.dialog({
                        title: '系统提示',
                        icon: 'error', //图标
                        content: strErrorMsg,
                        lock: true,
                        colse: function () { $("#txtNumber" + goodsID).val(Number); }
                    });
                    $("#txtNumber" + goodsID).val(GoodsList[i].ExpNum);
                    return false;
                }
                var tempval = sys_num_checked.product_num($("#txtNumber" + goodsID).val());
                if (!sys_num_checked.product_num_check(tempval)) {
                    art.dialog({
                        title: '系统提示',
                        icon: 'error', //图标
                        content: '购买商品的数量输入错误,请重新输入',
                        lock: true,
                        colse: function () { $("#txtNumber" + goodsID).val(GoodsList[i].ExpNum); }
                    });
                    $("#txtNumber" + goodsID).val(GoodsList[i].ExpNum);
                    return false;
                }
                if (chkNoMember) { //散客消费
                    GoodsList[i].ExpPoint = 0;
                    GoodsList[i].ExpMoney = accMul(accMul(GoodsList[i].Price, GoodsList[i].ExpDiscount), intNumber);
                }
                else {
                    GoodsList[i].ExpMoney = parseFloat(accMul(accMul(GoodsList[i].Price, intNumber), GoodsList[i].ExpDiscount)).toFixed(2);
                    if (Math.floor(GoodsList[i].Point) > 0) {
                        GoodsList[i].ExpPoint = Math.floor(accMul(GoodsList[i].Point, GoodsList[i].ExpNum));
                    }
                    else {
                        GoodsList[i].ExpPoint = Math.floor(accDiv(GoodsList[i].ExpMoney, GoodsList[i]["ExpPointDiscount"]));
                    }
                }
                //重新计算提成
                GetStaffMoney(i);
                if (oldval != GoodsList[i].ExpNum.toString()) {
                    $("#txtNumber" + goodsID).val(oldval);
                } else {
                    $("#txtNumber" + goodsID).val(GoodsList[i].ExpNum);
                }
                $("#txtDiscountPrice" + goodsID).val(GoodsList[i].ExpMoney);
                $("#txtPoint" + goodsID).val(GoodsList[i].ExpPoint);               
            }
            else {
                if (!(/^\d+$/).test(intNumber)) {
                    $("#txtNumber" + goodsID).val(GoodsList[i].ExpNum);
                    art.dialog({
                        title: '系统提示',
                        icon: 'error', //图标
                        content: '购买商品的计次数量输入错误,请重新输入',
                        lock: true
                    });
                    return;
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
        if (Math.floor(GoodsList[i].Point) > 0) {
            GoodsList[i].ExpPoint = Math.floor(accMul(GoodsList[i].Point, GoodsList[i].ExpNum));
        }
        else {
            GoodsList[i].ExpPoint = Math.floor(accDiv(GoodsList[i].ExpMoney, GoodsList[i]["ExpPointDiscount"]));
        }
    }
    //重新计算提成
    GetStaffMoney(i);
    if (oldval != GoodsList[i].ExpNum.toString()) {
        $("#txtNumber" + goodsID).val(oldval);
    } else {
        $("#txtNumber" + goodsID).val(GoodsList[i].ExpNum);
    }
    $("#txtDiscountPrice" + goodsID).val(GoodsList[i].ExpMoney);
    $("#txtPoint" + goodsID).val(GoodsList[i].ExpPoint);
    ExUpdatePoint(goodsID, type);
    ExGoodsBindTotal();
}
/****************************************************************************************************
*更改折后价 重新计算合计
*****************************************************************************************************/
function ExUpdateDiscountPrice(goodsID, type) {
    var decDiscountPrice = (type == 0) ? $("#txtDiscountPrice" + goodsID).val() : $("#txtServiceDiscountPrice" + goodsID).val();
    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].GoodsID == goodsID && GoodsList[i].ExpNum > 0) {
            GoodsList[i].ExpMoney = decDiscountPrice;
            break;
        }
    }
    //散客消费
    if (chkNoMember) {
        GoodsList[i].ExpPoint = 0;
        GoodsList[i].ExpMoney = decDiscountPrice;
    }
    else {
        GoodsList[i].ExpMoney = decDiscountPrice;
        if (Math.floor(GoodsList[i].Point) > 0) {
            GoodsList[i].ExpPoint = Math.floor(accMul(GoodsList[i].Point, GoodsList[i].ExpNum));
        }
        else {
            GoodsList[i].ExpPoint = Math.floor(accDiv(GoodsList[i].ExpMoney, GoodsList[i]["ExpPointDiscount"]));
        }
    }
    //    //重新计算提成
    //    GetStaffMoney(i);
    $("#txtPoint" + goodsID).val(GoodsList[i].ExpPoint);
    $("#txtDiscountPrice" + goodsID).val(GoodsList[i].ExpMoney);
    ExUpdatePoint(goodsID, type);    
    ExGoodsBindTotal();
}
/****************************************************************************************************
*更改积分 重新计算合计
*****************************************************************************************************/
function ExUpdatePoint(goodsID, type) {
    var intPoint = (type == 0) ? $("#txtPoint" + goodsID).val() : $("#txtServicePoint" + goodsID).val();
    chkNoMember = $("#chkNoMember").attr("checked");
    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].GoodsID == goodsID && GoodsList[i].ExpNum > 0) {
            if (chkNoMember) {
                GoodsList[i].ExpPoint = 0;
            }
            else {
                if (levelPoint == 0) {
                    GoodsList[i].ExpPoint = 0;
                }
                else {
                    GoodsList[i].ExpPoint = intPoint;
                }
            }
            break;
        }
    }
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

//数据更改 重新计算提成
function GetStaffMoney(i) {
    //重新计算提成
    if (staff && staffType == "False") {
        //按商品类型
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
    }
    //按员工提成比例
    else if (staff && staffType == "True") {
        switch (GoodsList[i].GoodsType) {
            case "0":
                GoodsList[i].ExpStaffMoney = accMul(GoodsList[i].ExpMoney, GoodsList[i].ExpStaffPercent);
                break;
            case "1":
                GoodsList[i].ExpStaffMoney = Math.abs(accMul(GoodsList[i].CommissionNumber, GoodsList[i].ExpNum));
        }
        //        GoodsList[i].ExpStaffMoney = accMul(GoodsList[i].ExpMoney, GoodsList[i].ExpStaffPercent);
    }
    else if (!staff) {
        GoodsList[i].ExpStaffMoney = 0;
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
    GoodsProductList();

    ExGoodsBindList();
    $("#sltStaff").val("");
    //    if ($.isEmptyObject(global_mem)) {
    //        art.dialog({
    //            icon: 'error', //图标
    //            content: '请先选择会员!',
    //            lock: true
    //        });
    //    }

    //    if ($("#chkNoMember").attr("checked")) {
    //        $("#txtFindMember").attr("disabled", "disabled");
    //    }
    //    else {
    //        $("#txtFindMember").attr("disabled", "");
    //        $("#txtFindMember").focus();
    //    }

}
/****************************************************************************************************
*挂单按钮响应事件
*****************************************************************************************************/
function btnEntryOrder() {
    var strErrorMsg = "";
    var strOrderCode = $.trim($("#spOrderAccount").html());
    staff = $("#chkStaff").attr("checked");
    if (GoodsList.length == 0) {
        strErrorMsg += "<li>请在左侧列表中选择需要消费的商品！</li>";
    }
    var num = 0;
    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].ExpNum < 0) {
            num = parseFloat(_sys_check.defval);
        }
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
            parameter.push({ "payType": "EmptyBills", "IsCard": false, "IsCash": false, "IsBink": false, "CardMoney": 0, "CashMoney": 0, "CouponMoney": 0, "BinkMoney": 0, "DiscountMoney": parseFloat(totalDiscount), "UsePoint": 0, "UsePointAmount": 0  });
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
    $("#tbOrderTable").find(".td").find("input[type=text]").each(function (i) { if (i == 1) { $(this).val(sys_num_checked.product_num($.trim($(this).val()))) } });
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
        parameter.push({ "payType": "GoodsExpense", "IsCard": false, "IsCash": false, "IsBink": false, "CardMoney": 0, "CashMoney": 0, "CouponMoney": 0, "BinkMoney": 0, "DiscountMoney": 0, "ChangeMoney": 0 , "UsePoint": 0, "UsePointAmount": 0 });
        ExpenseOK(parameter);
        return;
    }
    ShowPay("GoodsExpense", global_mem.MemMoney,global_mem.MemPoint, totalDiscount, strOrderCode, chkAllowPwd);
}
function ExpenseOK(parameter) {


    //获取打印的份数
    var PointNum = $("#PointNum").val();
    var mid= chkNoMember ? "0" : global_mem.MemID;
    doAjax("../",
            "GoodsExpense",
            {
                "memID": mid,
                //                "payType": payType,
                //                "cardPayMoney": cardPayMoney,
                //                "cashPayMoney": cashPayMoney,
                //                "couponPayMoney": couponPayMoney,
                "parameter": parameter,
                "totalMoney": totalMoney,
                //                "discountMoney": strDiscountMoney,
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
                "isMemCountExpense": 0
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
                                  close: function () { PrintGoodsExpense($("#lblExpenseUSer").html(), global_mem, GoodsList, parameter, $("#chkPrint").attr("checked"), PointNum); }
                              });
                        break;
                    case -3:
                        art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: '挂单成功！',
                                  close: function () { location.href = location.href; }
                              });
                        break;
                    case -4:
                        art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: '请勿重复提交订单！',
                                  close: function () { location.href = location.href; }
                              });
                        break;
                    case -5:
                        art.dialog
                            ({
                                title: '系统提示',
                                time: 4,
                                content: ("发送短信失败,本店拥有的短信量不足请与总店联系！"),
                                lock: true
                            });
                        break;
                    case -6:
                        art.dialog
                              ({
                                  title: '系统提示',
                                  time: 2,
                                  content: ("本店积分不足无法消费，请与总店联系！"),
                                  close: function () { location.href = location.href; }
                              });
                        break;
                    default:
                        art.dialog
                                ({
                                    title: '系统提示',
                                    time: 2,
                                    content: '消费成功！' + json.strUpdateMemLevel,
                                    close: function () {
                                        PrintGoodsExpense($("#lblExpenseUSer").html(), global_mem, GoodsList, parameter, $("#chkPrint").attr("checked"), json.point);

                                    }
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
    $("#txtGoodsExpenseCode").focus().select();
    $("#liService").css("display", "");
    if (location.href.toLowerCase().indexOf("goodsexpense.aspx") != -1) {
        levelPercent = (global_mem.ClassDiscountPercent != 0) ? global_mem.ClassDiscountPercent : 0;
        levelPoint = (global_mem.ClassPointPercent != 0 || global_mem.ClassPointPercent != undefined) ? global_mem.ClassPointPercent : 0;

        //        levelPercent = (global_mem.LevelDiscountPercent != 0) ? global_mem.LevelDiscountPercent : 0;
        //        levelPoint = (global_mem.LevelPointPercent != 0 || global_mem.LevelPointPercent != "undefined") ? accMul(global_mem.LevelPointPercent, 100) : 0;
        ExSelectGoodsRefreshList();
        ExGoodsBindList();
        $("#sltStaff").val("");
    }
    GoodsProductList();
    var orderID = $("#txtOrderID").val();
    if (orderID != "") {
        isEmptyBillsExpense = true;
        GetExpenseDetail(orderID);
        order = orderID;
        $("#txtOrderID").val("");
    }
    return true;
}

//计算商品的折扣
function calculate(item) {
    var minPercent = parseFloat(item.MinPercent).toFixed(2);
    var salePercet = parseFloat(item.SalePercet).toFixed(2);
    var goodsDiscount = 1;
    //判断是否是会员 global_mem.MemLevelID==undefined表示非会员
    if (!global_mem.MemLevelID) {
        if (minPercent == 0 && salePercet == 0) {
            goodsDiscount = 1;
        }
        else if (salePercet > 0) {
            goodsDiscount = salePercet;
        }
        else {//散客没有最低折扣
            goodsDiscount = 1;
        }
    } else {
        var classDiscountPercent = parseFloat(item.ClassDiscountPercent).toFixed(2);
        if (minPercent == 0 && salePercet == 0) {
            //按照商品分类折扣打折
            goodsDiscount = classDiscountPercent;
        } else if (minPercent > 0 && salePercet == 0) {
            //最低折扣与商品分类折扣对比 按照折扣高的打折
            goodsDiscount = minPercent > classDiscountPercent ? minPercent : classDiscountPercent;
        } else if (minPercent == 0 && salePercet > 0) {
            //特价折扣与商品分类折扣对比 按照折扣低的打折
            goodsDiscount = salePercet > classDiscountPercent ? classDiscountPercent : salePercet;
        } else {
            //当程序出现bug时让默认折扣为1
            goodsDiscount = 1;
        }
    }
    return goodsDiscount;
}
//计算商品的积分折扣
function pointCalculate(item) {
    var pointDiscount = 0;
    //判断是否是会员 global_mem.MemLevelID==undefined表示非会员
    if (!global_mem.MemLevelID) {
        pointDiscount = 0;
    } else {
        var parms = parseInt(item.Point);
        if (parms == -1) {
            //表示不积分
            pointDiscount = 0;
        } else if (parms == 0) {
            //按照商品分类积分比例积分
            pointDiscount = parseFloat(item.ClassPointPercent);
        } else if (parms > 0) {
            //按照商品拥有的积分计算
            pointDiscount = 2;
        } else {
            pointDiscount = 0;
        }
    }
    return pointDiscount;
}

//计算购买某种商品获得的积分
function getPoint(goodsPointDiscount, point, expMoney, expNum) {//积分比例 商品积分 折后总金额  购买数量
    var currentPoint = 0;
    var pointCalculate = parseFloat(goodsPointDiscount);
    if (pointCalculate == 2) {
        currentPoint = parseInt(accMul(point, expNum));
    } else if (pointCalculate == -1) {
        currentPoint = 0;
    } else {
        currentPoint = parseInt(accMul(expMoney, pointCalculate));
    }
    return currentPoint;
}