/*global _comma_separated_list_of_variables_*/
$(document).ready(function () {
    $("#tdbox").css({ height: document.body.scrollHeight - 4 });
});


function getPageSize() {
    var xScroll, yScroll;
    if (window.innerHeight && window.scrollMaxY) {
        xScroll = window.innerWidth + window.scrollMaxX;
        yScroll = window.innerHeight + window.scrollMaxY;
    } else if (document.body.scrollHeight > document.body.offsetHeight) {
        // all but Explorer Mac  
        xScroll = document.body.scrollWidth;
        yScroll = document.body.scrollHeight;
    } else {
        // Explorer Mac...would also work in Explorer 6 Strict, Mozilla and Safari  
        xScroll = document.body.offsetWidth;
        yScroll = document.body.offsetHeight;
    }
    var windowWidth, windowHeight;
    if (self.innerHeight) {
        // all except Explorer  
        if (document.documentElement.clientWidth) {
            windowWidth = document.documentElement.clientWidth;
        } else {
            windowWidth = self.innerWidth;
        }
        windowHeight = self.innerHeight;
    } else if (document.documentElement && document.documentElement.clientHeight) {
        // Explorer 6 Strict Mode  
        windowWidth = document.documentElement.clientWidth;
        windowHeight = document.documentElement.clientHeight;
    } else if (document.body) {
        // other Explorers  
        windowWidth = document.body.clientWidth;
        windowHeight = document.body.clientHeight;
    }
    // for small pages with total height less then height of the viewport  
    if (yScroll < windowHeight) {
        pageHeight = windowHeight;
    } else {
        pageHeight = yScroll;
    }
    // for small pages with total width less then width of the viewport  
    if (xScroll < windowWidth) {
        pageWidth = xScroll;
    } else {
        pageWidth = windowWidth;
    }
    arrayPageSize = new Array(pageWidth, pageHeight, windowWidth, windowHeight);
    return arrayPageSize;
};
window.onresize = function () {

    //  $("#tdbox").css({ height: document.body.clientWidth - 4 });
}

// 检测文本框输入只能是正数Float类型
function isFloatPositive(jQuerySelector) {

    var numFloatPositive = /^\d+(\.\d+)?|[0-9]*[1-9][0-9]*$/; //^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$正浮点数(8位小数)^\d+(\.\d+)?$

    //确保用户输入的是正实数
    if (isNaN($(jQuerySelector).val())) { $(jQuerySelector).val(''); return false; }
    if (!numFloatPositive.test($(jQuerySelector).val()) && $(jQuerySelector).val() != '') {
        document.execCommand('undo'); //确保用户输入的是正实数
        return false;
    }
    return true;
}

//判读是否为正整数 
function isIntPositive(jQuerySelector) {
    var num = /^[0-9]*[1-9][0-9]*$/;
    return num.test(jQuerySelector);
}

//乘法函数，用来得到精确的乘法结果
//说明：javascript的乘法结果会有误差，在两个浮点数相乘的时候会比较明显。这个函数返回较为精确的乘法结果。
//调用：accMul(arg1,arg2)
//返回值：arg1乘以arg2的精确结果
function accMul(arg1, arg2) {
 
    var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
    try { m += s1.split(".")[1].length } catch (e) { }
    try { m += s2.split(".")[1].length } catch (e) { }
    return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m);
}

//除法函数，用来得到精确的除法结果
//说明：javascript的除法结果会有误差，在两个浮点数相除的时候会比较明显。这个函数返回较为精确的除法结果。
//调用：accDiv(arg1,arg2)
//返回值：arg1除以arg2的精确结果
function accDiv(arg1, arg2) {
    var t1 = 0, t2 = 0, r1, r2;
    try { t1 = arg1.toString().split(".")[1].length } catch (e) { }
    try { t2 = arg2.toString().split(".")[1].length } catch (e) { }
    with (Math) {
        r1 = Number(arg1.toString().replace(".", ""));
        r2 = Number(arg2.toString().replace(".", ""));
        var result = (r1 / r2) * pow(10, t2 - t1);
        if (result == Infinity) {
            result = 0;
        }
        return result;
    }
}

//加法函数，用来得到精确的加法结果
//说明：javascript的加法结果会有误差，在两个浮点数相加的时候会比较明显。这个函数返回较为精确的加法结果。
//调用：accAdd(arg1,arg2)
//返回值：arg1加上arg2的精确结果
function accAdd(arg1, arg2) {
    var r1, r2, m;
    try { r1 = arg1.toString().split(".")[1].length } catch (e) { r1 = 0 }
    try { r2 = arg2.toString().split(".")[1].length } catch (e) { r2 = 0 }
    m = Math.pow(10, Math.max(r1, r2));
    return accDiv(accMul(arg1, m) + accMul(arg2, m), m);
}

// 减法函数
function accSub(arg1, arg2) {
    return accAdd(arg1, accMul(arg2, -1));
}
////將得到的值四捨五入，去小數點后兩位
////arg1：參數
//function accMath(arg1)
//{
//    return accMath(Math.round(arg1,2));
//}

//function GetGoodsClassParentID(ParentID)
//{
//var isParent;
//   doAjax(
//            "../",
//            "GetGoodsClass",
//            {"class": ParentID},
//            "json",
//            function (json) 
//            {
//                isParent=json.IsParent;
//            }
//       ) 
//}

//读取会员等级小心 供js调用
var memLevel;
function GetCommonDate(callback) {
    doAjax(
            "../",
            "GetCommonData",
            "json",
            function (json) {
                memLevel = json;
                if (typeof (callback) == "function")
                    callback();
            }
    )
}

// 取得某等级的打折比例
function LevelIDToPercent(levelID) {
    var lPercent = 1;
    if (typeof (memLevel) != "undefined") {
        $.each(memLevel, function (index, obj) {
            if (obj.LevelID == levelID) {
                lPercent = parseFloat(obj.Percent);
            }
        });
    }
    return lPercent;
}

//取得当前时间
function GetDataTimeNow() {
    var nowData = "";

    var now = new Date();
    var year = now.getFullYear();
    var month = now.getMonth() + 1;
    var day = now.getDate();
    var hour = now.getHours();
    var minute = now.getMinutes();
    var second = now.getSeconds();

    nowdate = year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
    return nowdate;
}

//计算两个时间相差的天数
function DateDiff(sDate1, sDate2) { //sDate1和sDate2是2002-12-18格式 
    var aDate, oDate1, oDate2, iDays
    aDate = sDate1.split("-")
    oDate1 = new Date(aDate[1] + '-' + aDate[2] + '-' + aDate[0] + " 01:00:00") //转换为12-18-2002格式 
    aDate = sDate2.split("-")
    oDate2 = new Date(aDate[1] + '-' + aDate[2] + '-' + aDate[0] + " 00:00:00")
    iDays = parseInt(Math.floor(oDate1 - oDate2) / 1000 / 60 / 60 / 24) //把相差的毫秒数转换为天数 
    return iDays
}

function dateValid(StartDate, EndDate) {
    var beginDate = new Date(StartDate.replace(/-/g, "/"));
    var endDate = new Date(EndDate.replace(/-/g, "/"));
    if (beginDate > endDate) {
        return false;
    }
    else {
        return true;
    }
}

//字符串数组转换为整形数组
function strtoint(arrayObj) {
    var Mydata = new Array(arrayObj.length);
    for (var i = 0; i < arrayObj.length; i++) {
        Mydata[i] = parseInt(arrayObj[i]);
    }
    return Mydata;
}

//字符串数组转换为浮点型数组
function strtofloat(arrayObj) {
    var Mydata = new Array(arrayObj.length);
    for (var i = 0; i < arrayObj.length; i++) {
        Mydata[i] = parseFloat(arrayObj[i]);
    }
    return Mydata;
}

//打印小票算出省的金额
function accountPay(totalMoney, totalDiscount) {
    var sbPay = "";
    var payMoney = (parseFloat(totalMoney) - parseFloat(totalDiscount)).toString();
    if (payMoney <= 0 || totalMoney == "") {
        sbPay = "";
    } else {

        var moneys = parseFloat(totalMoney) - parseFloat(totalDiscount);
        sbPay = "节省金额：" + parseFloat(moneys).toFixed(2) + "";
    }
    return sbPay;
}
//打印时不同支付方式的金额
function PayInfos(parameter) {
    var strPayInfo = "";
    //现金付款 算出找零的金额 
    if (parameter[0].IsCash || parameter[0].IsCard || parameter[0].IsBink) {
        if (parameter[0].IsCash) {
            strPayInfo += "现金付款：" + parseFloat(parameter[0].CashMoney).toFixed(2) + "<br/>";
        }
        if (parameter[0].IsCard) {
            strPayInfo += "余额付款：" + parseFloat(parameter[0].CardMoney).toFixed(2) + "<br/>";
        }

        if (parameter[0].IsBink) {
            strPayInfo += "银联付款：" + parseFloat(parameter[0].BinkMoney).toFixed(2) + "<br/>";
        }
        if (parameter[0].CouponMoney != "0") {
            strPayInfo += "优惠券优惠：" + parseFloat(parameter[0].CouponMoney).toFixed(2) + "<br/>";
        }

    }
    var strChange = parameter[0].ChangeMoney;
    strPayInfo += "实付金额：" + parseFloat(accAdd(accSub(parameter[0].DiscountMoney, parameter[0].CouponMoney), strChange)).toFixed(2) + "<br/>" + "找零金额：" + parseFloat(strChange).toFixed(2) + "";
    return strPayInfo;
}


// 取得中文汉字拼音首字母
function getPYCode(str, id) {
    var result = "";
    doAjax(
            "../",
            "GetPinYin",
            { "strpinyin": str },
            "json",
            function (json) {
                $("#" + id).val(json.result.toString());
            });

}
function getPY(s) {
    if (s != "") {
        execScript("tmp=asc(\"" + s + "\")", "vbscript");
        tmp = 65536 + tmp;
        var py = "";
        if (tmp >= 45217 && tmp <= 45252) {
            py = "A"
        } else if (tmp >= 45253 && tmp <= 45760) {
            py = "B"
        } else if (tmp >= 45761 && tmp <= 46317) {
            py = "C"
        } else if (tmp >= 46318 && tmp <= 46825) {
            py = "D"
        } else if (tmp >= 46826 && tmp <= 47009) {
            py = "E"
        } else if (tmp >= 47010 && tmp <= 47296) {
            py = "F"
        } else if ((tmp >= 47297 && tmp <= 47613) || (tmp == 63193)) {
            py = "G"
        } else if (tmp >= 47614 && tmp <= 48118) {
            py = "H"
        } else if (tmp >= 48119 && tmp <= 49061) {
            py = "J"
        } else if (tmp >= 49062 && tmp <= 49323) {
            py = "K"
        } else if (tmp >= 49324 && tmp <= 49895) {
            py = "L"
        } else if (tmp >= 49896 && tmp <= 50370) {
            py = "M"
        } else if (tmp >= 50371 && tmp <= 50613) {
            py = "N"
        } else if (tmp >= 50614 && tmp <= 50621) {
            py = "O"
        } else if (tmp >= 50622 && tmp <= 50905) {
            py = "P"
        } else if (tmp >= 50906 && tmp <= 51386) {
            py = "Q"
        } else if (tmp >= 51387 && tmp <= 51445) {
            py = "R"
        } else if (tmp >= 51446 && tmp <= 52217) {
            py = "S"
        } else if (tmp >= 52218 && tmp <= 52697) {
            py = "T"
        } else if (tmp >= 52698 && tmp <= 52979) {
            py = "W"
        } else if (tmp >= 52980 && tmp <= 53688) {
            py = "X"
        } else if (tmp >= 53689 && tmp <= 54480) {
            py = "Y"
        } else if (tmp >= 54481 && tmp <= 62289) {
            py = "Z"
        } else {
            py = s.charAt(0)
        }
        return py
    }
}

//获取优惠券详情
function GetCouponDetail() {
    art.dialog({
        title: '电子优惠券详情',
        fixed: true,
        left: '90%',
        top: '70%',
        content: document.getElementById('divCouponDetail'),
        id: 'divCouponDetail'
    });
}

///***************************************************************************************
//*一些公用的弹出层（密码验证层、结账付款层,发短信时查找会员）
//***************************************************************************************/
//function CommonCheckPwdPanel(strOrderAccount, strDiscountMoney) {
//    // 弹出密码验证框   
//    if (document.getElementById("divCheckPwd") == null) {
//        var html = ''
//           + '  <div id="divCheckPwd"' + 'style="height:35px; line-height:35px; margin-left:20px; display: none">请输入会员卡密码：'
//           + '  <input type="password" id="txtCheckPwd" style="width:150px; margin-top:5px;" class="input_txt border_radius" onclick=\'this.focus();this.select();\'/>'
//           + '  <input type="button" id="btnCheckPwd" class="buttonColor" value="确定" style="margin-left:10px;"/>'
//           + '  <div style="height:15px; line-height:25px; margin-left:20px;" id="divMsg"></div>'
//           + '</div>';
//        $("body").append(html);

//        art.dialog({
//            title: '密码验证',
//            content: document.getElementById('divCheckPwd'),
//            id: 'divCheckPwd',
//            lock: true
//            //closeFn: function () { return false; }
//        });

//        // 密码验证按钮事件
//        $("#btnCheckPwd").click(function () {
//            var strPwd = $("#txtCheckPwd").val();
//            if (strPwd == "") {
//                art.dialog({ title: '系统提示', time: 1, content: '请输入会员密码！' });
//                $("#txtCheckPwd").focus();
//                return;
//            }
//            doAjax(
//               "../",
//               "MemCheckPwd",
//               { "memID": global_mem.MemID, "memPassword": strPwd },
//               "json",
//               function (json) {
//                   if (json > 0) {
//                       var list = art.dialog.list;
//                       for (var i in list) {
//                           list[i].close();
//                       };
//                       CommonShowPayChangePanel(chkNoMember, strOrderAccount, strDiscountMoney);
//                   }
//                   else {
//                       art.dialog({ title: '系统提示', time: 1, content: '密码输入错误，请重试！' });
//                       $("#txtCheckPwd").focus();
//                   }
//               });
//        });

//        //回车响应事件
//        $("#txtCheckPwd").keypress(function (e) {
//            var keyAscii = window.event ? e.keyCode : e.which;
//            if (keyAscii == 13)
//                $("#btnCheckPwd").click();
//        });
//    }
//    else {
//        $("#divMsg").html("");
//        $("#txtCheckPwd").val("");
//        $("#txtCheckPwd").focus();
//        art.dialog({
//            title: '密码验证',
//            content: document.getElementById('divCheckPwd'),
//            id: 'divCheckPwd',
//            lock: true
//        });
//    }
//    $(document).scrollTop(0);
//}


////单号/应付金额
//function CommonShowPayChangePanel(chkNoMember, strOrderAccount, strDiscountMoney) {
//    // 弹出付费找零窗口
//    if (document.getElementById("divPayChange") != null) {
//        $("#divPayChange").remove();
//    }
//    //保留两位小数
//    strDiscountMoney = strDiscountMoney.toFixed(2);
//    var dclDiscountMoney = strDiscountMoney;

//    var html = ''
//         + '<div id="divPayChange" style="display: none;" >'
//         + '    <div class="radioPayChange">订单编号： ' + strOrderAccount + '</div>';
//    if (!chkNoMember) {
//        html += '    <div class="radioPayChange">账户余额：<font color="red"> ￥' + global_mem.MemMoney + '</font></div>';
//    }
//    html += '    <div class="radioPayChange"  >应付金额：'
//         + '       <input type="text" style="font-size:20px;" disabled="disabled"  class="input_txt border_radius"  id="txtTotalMoney" />'
//         + '    </div>'
//    if (!chkNoMember) {
//        html += '<div  class="radioPayChange">优惠金额：'
//         + '       <input type="text" style="font-size:20px;" disabled="disabled" class="input_txt border_radius" id="txtCouponMoney" />'
//         + '     </div>';
//    }
//    html += '    <div id="divByCard">'
//         + '         <div class="radioPayChange">实际支付：'
//         + '            <input type="text" id="txtByCardMoney" style="font-size:20px;color:red" class="input_txt border_radius"/><font color="red">'
//         + '         </font></div>'
//         + '    </div>'
//         + '    <div id="divByCash">'
//         + '      <div class="radioPayChange">实际支付：'
//         + '         <input type="text" id="txtByCashMoney" style="font-size:20px" class="input_txt border_radius"/>'
//         + '      </div>'
//         + '      <div class="radioPayChange">找零金额：'
//         + '         <input type="text" id="txtByCardChangeMoney" disabled="disabled" style="font-size:20px" class="input_txt border_radius"/>'
//         + '      </div>'
//         + '    </div>'
//         + '    <div id="divByUnion">'
//         + '         <div class="radioPayChange">余额支付：'
//         + '            <input type="text" style="font-size:20px" id="txtByUnionMoney" class="input_txt border_radius" />&nbsp;&nbsp;<font color="red">'
//         + '         </font></div>'
//         + '         <div class="radioPayChange">现金支付：'
//         + '            <input type="text" style="font-size:20px" id="txtByUnionCashMoney" class="input_txt border_radius" />'
//         + '         </div>'
//         + '         <div class="radioPayChange">找零金额：'
//         + '            <input type="text" style="font-size:20px" id="txtByUnionChangeMoney" disabled="disabled" class="input_txt border_radius"/>'
//         + '         </div>'
//         + '    </div>'
//         + '    <div class="radioPayChange">'
//         + '       <label><input type="radio"  id="radCard" name="RadPayMoneyType" value="card"/>余额支付</label>&nbsp;&nbsp;'
//         + '       <label><input type="radio"  id="radCash" name="RadPayMoneyType" value="cash"/>现金支付</label>&nbsp;&nbsp;'
//         + '       <label><input type="radio"  id="radUnion" name="RadPayMoneyType" value="union"/>联合支付</label>'
//         + '    </div>'
//    if (!chkNoMember) {
//        html += '<div class="radioPayChange">优惠券号：'
//         + '       <input type="text" style="font-size:15px;" class="input_txt border_radius" id="txtCoupon" />'
//         + '    </div>'
//         + '    <div id="divCouponResult" class="radioPayChange">'
//         + '       <span id="spCoupanResult" style="font-size:15px"></span>'
//         + '    </div>';
//        html += '<div id="divCouponDetail" style="display: none">'
//            + '<table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 300px; margin: auto">'
//            + '    <tr>'
//            + '      <td class="tableAlignRight">优惠券号：</td>'
//            + '      <td><span id="spCouPon"></span></td>'
//            + '    </tr>'
//            + '    <tr>'
//            + '      <td class="tableAlignRight">优惠券名称：</td>'
//            + '      <td><span id="spCouponTitle"></span></td>'
//            + '    </tr>'
//            + '    <tr>'
//            + '      <td class="tableAlignRight">优惠类型：</td>'
//            + '      <td><span id="spCouponType"></span></td>'
//            + '    </tr>'
//            + '    <tr>'
//            + '      <td class="tableAlignRight"><span id="spCouponNumName">优惠金额：</span></td>'
//            + '      <td><span id="spCouponNumber"></span></td>'
//            + '    </tr>'
//            + '    <tr>'
//            + '      <td class="tableAlignRight">单日限用：</td>'
//            + '      <td><span id="spCouponDayNum"></span></td>'
//            + '    </tr>'
//            + '    <tr>'
//            + '      <td class="tableAlignRight">最低消费：</td>'
//            + '      <td><span id="spCouponMinMoney"></span></td>'
//            + '    </tr>'
//            + '    <tr>'
//            + '      <td class="tableAlignRight">已发时间：</td>'
//            + '      <td><span id="spConponSendTime"></span></td>'
//            + '    </tr>'
//            + '    <tr>'
//            + '      <td class="tableAlignRight">发送对象：</td>'
//            + '      <td><span id="spConponSendMem"></span></td>'
//            + '    </tr>'
//            + '    <tr>'
//            + '      <td class="tableAlignRight">有效期：</td>'
//            + '      <td><span id="spCouponTime"></td>'
//            + '    </tr>'
//            + '    <tr>'
//            + '      <td class="tableAlignRight">已用时间：</td>'
//            + '      <td><span id="spCouponUseTime"></span></td>'
//            + '    </tr>'
//            + '    <tr>'
//            + '      <td class="tableAlignRight">使用订单号：</td>'
//            + '      <td><span id="spCouponAccount"></span></td>'
//            + '    </tr>'
//            + '   </table>'
//            + ' </div>';
//    }
//    html += '    <div style="text-align:center; padding:10px;">'
//         + '      <input type="button" value="马上结算" class="buttonColor" id="btnPayChange"/>'
//         + '    </div>'
//         + '</div>';
//    $("body").append(html);

//    art.dialog({
//        left: '100%',
//        top: '100%',
//        height: 400,
//        width: 270,
//        title: '付费找零',
//        content: document.getElementById('divPayChange'),
//        id: 'divPayChange',
//        lock: true
//    });

//    if (chkNoMember) {
//        $("#divByCard").css("display", "none");
//        $("#divByCash").css("display", "");
//        $("#divByUnion").css("display", "none");
//        $("#radCard").attr("disabled", "disabled");
//        $("#radUnion").attr("disabled", "disabled");
//    }
//    else {
//        $("#divByCard").css("display", "");
//        $("#divByCash").css("display", "none");
//        $("#divByUnion").css("display", "none");
//    }

//    //初始化数据
//    InitData();

//    $("#radCard").click(function () {
//        $("#divByCard").css("display", "");
//        $("#divByCash").css("display", "none");
//        $("#divByUnion").css("display", "none");
//        $("#txtByCardMoney").select();
//    });

//    $("#radCash").click(function () {
//        $("#divByCard").css("display", "none");
//        $("#divByCash").css("display", "");
//        $("#divByUnion").css("display", "none");
//        $("#txtByCashMoney").select();
//    });

//    $("#radUnion").click(function () {
//        $("#divByCard").css("display", "none");
//        $("#divByCash").css("display", "none");
//        $("#divByUnion").css("display", "");
//        $("#txtByUnionMoney").select();
//    });

//    if (chkNoMember) {
//        $("#radCash").click();
//    }
//    else {
//        $("#radCard").click();
//    }

//    //初始化数据
//    function InitData() {
//        $("#txtTotalMoney").val(strDiscountMoney);
//        $("#txtByCardMoney").val(strDiscountMoney);
//        $("#txtByCashMoney").val(strDiscountMoney);
//        if (chkNoMember) {
//            $("#txtByUnionMoney").val(strDiscountMoney);
//            $("#txtByUnionCashMoney").val("0.00");
//        }
//        else {
//            if (strDiscountMoney.ToFloat() <= global_mem.MemMoney.ToFloat()) {
//                $("#txtByUnionMoney").val(strDiscountMoney);
//                $("#txtByUnionCashMoney").val("0.00");
//            }
//            else {
//                $("#txtByUnionMoney").val(global_mem.MemMoney);
//                var dclCashMoney = Number(strDiscountMoney) - Number(global_mem.MemMoney);
//                $("#txtByUnionCashMoney").val(dclCashMoney.toFixed(2));
//            }
//        }
//        $("#txtCouponMoney").val("0.00");
//        $("#txtByCardChangeMoney").val("0.00");
//        $("#txtByUnionChangeMoney").val("0.00");

//        //优惠券详情
//        $("#spCouPon").html("");
//        $("#spCouponTitle").html("");
//        $("#spCouponType").html("");
//        $("#spCouponNumber").html("");
//        $("#spCouponDayNum").html("");
//        $("#spCouponMinMoney").html("");
//        $("#spConponSendTime").html("");
//        $("#spConponSendMem").html("");
//        $("#spCouponUseTime").html("");
//        $("#spCouponAccount").html("");
//        $("#spCouponTime").html("");
//    }

//    // 数据输入验证，并计算找零
//    $("#txtByCardMoney").keyup(function () { isFloatPositive(this); });
//    $("#txtByCashMoney").keyup(function () {
//        if (isFloatPositive(this)) {
//            var dclChangeMoney = accSub(accAdd($("#txtByCashMoney").val().ToFloat(), $("#txtCouponMoney").val().ToFloat()), strDiscountMoney);
//            $("#txtByCardChangeMoney").val(dclChangeMoney.toFixed(2));
//        }
//        else {
//            $("#txtByCardChangeMoney").val("0.00");
//        }
//    });

//    $("#txtByUnionMoney").keyup(function () {
//        if (isFloatPositive(this)) {
//            var dclChangeMoney = accSub(accAdd(accAdd($("#txtByUnionMoney").val().ToFloat(), $("#txtByUnionCashMoney").val().ToFloat()), $("#txtCouponMoney").val()), strDiscountMoney);
//            $("#txtByUnionChangeMoney").val(dclChangeMoney.toFixed(2));
//        }
//        else {
//            $("#txtByUnionChangeMoney").val("0.00");
//        }
//    });

//    $("#txtByUnionCashMoney").keyup(function () {
//        if (isFloatPositive(this)) {
//            var dclChangeMoney = accSub(accAdd(accAdd($("#txtByUnionMoney").val().ToFloat(), $("#txtByUnionCashMoney").val().ToFloat()), $("#txtCouponMoney").val()), strDiscountMoney);
//            $("#txtByUnionChangeMoney").val(dclChangeMoney.toFixed(2));
//        }
//        else {
//            $("#txtByUnionChangeMoney").val("0.00");
//        }
//    });

//    //查询优惠券
//    $('#txtCoupon').keypress(function (e) {
//        var keyAscii = window.event ? e.keyCode : e.which;
//        if (keyAscii == 13) {
//            if ($.trim($("#txtCoupon").val()) == "") {
//                $("#spCoupanResult").html("");
//                //初始化数据
//                InitData();
//                return;
//            }
//            doAjax("../",
//                   "GetCouponDetail",
//                   { "CouPon": $.trim($("#txtCoupon").val()),
//                       "MemID": global_mem.MemID
//                   },
//                   "json",
//                   function (json) {
//                       if (json == null) {
//                           $("#spCoupanResult").html("没有该优惠券号,请输入正确优惠券号！");

//                           //初始化数据
//                           InitData();
//                           return;
//                       }
//                       else {
//                           //优惠券详情
//                           $("#spCouPon").html(json.msgData[0].CouPon);
//                           $("#spCouponTitle").html(json.msgData[0].CouponTitle);
//                           if (json.msgData[0].CouponType == "0") {
//                               $("#spCouponType").html("代金券");
//                               $("#spCouponNumName").html("优惠金额：");
//                           }
//                           else {
//                               $("#spCouponType").html("折扣券");
//                               $("#spCouponNumName").html("折扣比例：");
//                           }
//                           $("#spCouponNumber").html(json.msgData[0].CouponNumber);
//                           $("#spCouponDayNum").html(json.msgData[0].CouponDayNum);
//                           $("#spCouponMinMoney").html(parseFloat(json.msgData[0].CouponMinMoney).toFixed(2));
//                           $("#spConponSendTime").html(json.msgData[0].ConPonSendTime);
//                           $("#spConponSendMem").html(json.msgData[0].MemName);
//                           $("#spCouponUseTime").html(json.msgData[0].ConPonUseTime);
//                           $("#spCouponAccount").html(json.msgData[0].CouPonOrderAccount);
//                           if (json.msgData[0].CouponEffective == "0") {
//                               $("#spCouponTime").html("永久有效！");
//                           }
//                           else {
//                               $("#spCouponTime").html(json.msgData[0].CouponStart.split(" ")[0] + "至" + json.msgData[0].CouponEnd.split(" ")[0]);
//                           }
//                       }
//                       if (global_mem.MemID != json.msgData[0].CouPonMID) {
//                           $("#spCoupanResult").html("优惠券只能本人使用" + ", <a href='javascript:GetCouponDetail()'>查看优惠券详情</a>");
//                           return;
//                       }

//                       if (json.msgData[0].CouPonSY == "True") {
//                           $("#spCoupanResult").html("该优惠券已经使用了" + ", <a href='javascript:GetCouponDetail()'>查看优惠券详情</a>");
//                           return;
//                       }

//                       if (strDiscountMoney < parseFloat(json.msgData[0].CouponMinMoney)) {
//                           $("#spCoupanResult").html("不能达到最低消费金额" + parseFloat(json.msgData[0].CouponMinMoney).toFixed(2) + "元" + ", <a href='javascript:GetCouponDetail()'>查看优惠券详情</a>");
//                           return;
//                       }
//                       if (json.msgResult == "1") {
//                           $("#spCoupanResult").html("该类型优惠券单日限用" + json.msgData[0].CouponDayNum + "张" + ", <a href='javascript:GetCouponDetail()'>查看优惠券详情</a>");
//                           return;
//                       }

//                       var dtNow = new Date();
//                       var dtCouponStart = json.msgData[0].CouponStart.replace(/-/g, '/');
//                       dtCouponStart = new Date(dtCouponStart);
//                       var dtCouponEnd = json.msgData[0].CouponEnd.replace(/-/g, '/');
//                       dtCouponEnd = new Date(dtCouponEnd);

//                       if (dtNow < dtCouponStart || dtNow > dtCouponEnd) {
//                           $("#spCoupanResult").html("该优惠券已过期" + ", <a href='javascript:GetCouponDetail()'>查看优惠券详情</a>");
//                           return;
//                       }
//                       $("#spCoupanResult").html("优惠券名称：" + json.msgData[0].CouponTitle + ", <a id='a' href='javascript:GetCouponDetail()'>查看优惠券详情</a>");
//                       if (json.msgData[0].CouponType == "0") {
//                           $("#txtCouponMoney").val(json.msgData[0].CouponNumber);
//                           dclDiscountMoney = strDiscountMoney - parseFloat(json.msgData[0].CouponNumber);
//                           $("#txtByCardMoney").val(dclDiscountMoney);
//                           $("#txtByCashMoney").val(dclDiscountMoney);
//                           if (dclDiscountMoney <= global_mem.MemMoney.ToFloat()) {
//                               $("#txtByUnionMoney").val(dclDiscountMoney);
//                               $("#txtByUnionCashMoney").val("0.00");
//                           }
//                           else {
//                               $("#txtByUnionMoney").val(global_mem.MemMoney);
//                               var dclCashMoney = Number(dclDiscountMoney) - Number(global_mem.MemMoney);
//                               $("#txtByUnionCashMoney").val(dclCashMoney.toFixed(2));
//                           }
//                           $("#txtByCardChangeMoney").val("0.00");
//                           $("#txtByUnionChangeMoney").val("0.00");
//                       }
//                       else if (json.msgData[0].CouponType == "1") {
//                           dclDiscountMoney = Math.round(accMul(strDiscountMoney, json.msgData[0].CouponNumber), 2);
//                           $("#txtCouponMoney").val(strDiscountMoney - parseFloat(dclDiscountMoney));
//                           $("#txtByCardMoney").val(dclDiscountMoney);
//                           $("#txtByCashMoney").val(dclDiscountMoney);
//                           if (dclDiscountMoney <= global_mem.MemMoney.ToFloat()) {
//                               $("#txtByUnionMoney").val(dclDiscountMoney);
//                               $("#txtByUnionCashMoney").val("0.00");
//                           }
//                           else {
//                               $("#txtByUnionMoney").val(global_mem.MemMoney);
//                               var dclCashMoney = Number(dclDiscountMoney) - Number(global_mem.MemMoney);
//                               $("#txtByUnionCashMoney").val(dclCashMoney.toFixed(2));
//                           }
//                           $("#txtByCardChangeMoney").val("0.00");
//                           $("#txtByUnionChangeMoney").val("0.00");
//                       }
//                   });
//        }
//    });

//    // 数据确认
//    $("#btnPayChange").click(function () {
//        var payType = $('input[name="RadPayMoneyType"]:checked').val();
//        var cardPayMoney = 0, cashPayMoney = 0; couponPayMoney = 0;
//        if (payType == "card") {
//            cardPayMoney = $("#txtByCardMoney").val().ToFloat();
//            if (cardPayMoney > global_mem.MemMoney) {
//                art.dialog({
//                    title: '系统提示',
//                    content: ("账户余额不足，无法完成支付！"),
//                    lock: true
//                });
//                $("#txtByCardMoney").select();
//                return;
//            }
//            if (cardPayMoney != dclDiscountMoney) {
//                art.dialog({
//                    title: '系统提示',
//                    content: ("支付金额错误，请重新填写！"),
//                    lock: true
//                });
//                $("#txtByCardMoney").select();
//                return;
//            }
//        }
//        else if (payType == "cash") {
//            cashPayMoney = $("#txtByCashMoney").val().ToFloat();

//            if (cashPayMoney < dclDiscountMoney) {
//                art.dialog({
//                    title: '系统提示',
//                    content: ("支付金额不足，请重新填写！"),
//                    lock: true
//                });
//                $("#txtByCashMoney").select();
//                return;
//            }
//        }
//        else {
//            cardPayMoney = $("#txtByUnionMoney").val().ToFloat();
//            cashPayMoney = $("#txtByUnionCashMoney").val().ToFloat();
//            if (cardPayMoney > global_mem.MemMoney) {
//                art.dialog({
//                    title: '系统提示',
//                    content: ("账户余额不足，无法完成支付！"),
//                    lock: true
//                });
//                $("#txtByUnionMoney").select();
//                return;
//            }
//            if (cardPayMoney + cashPayMoney < dclDiscountMoney) {
//                art.dialog({
//                    title: '系统提示',
//                    content: ("支付金额不足，请重新填写！"),
//                    lock: true
//                });
//                $("#txtByUnionCashMoney").select();
//                return;
//            }
//        }
//        if ($("#txtCouponMoney").val() != undefined) {
//            couponPayMoney = $("#txtCouponMoney").val().ToFloat();
//            if (couponPayMoney != 0) {
//                doAjax(
//                   "../",
//                   "UpdateCouponList",
//                   {
//                       "Coupon": $("#txtCoupon").val(),
//                       "OrderAccount": strOrderAccount
//                   },
//                   "json",
//                   function (json) {
//                   });
//            }
//        }
//        ExpenseOK(payType, cardPayMoney, cashPayMoney, couponPayMoney, parseFloat(dclDiscountMoney));
//    });
//    $(document).scrollTop(0);
//}