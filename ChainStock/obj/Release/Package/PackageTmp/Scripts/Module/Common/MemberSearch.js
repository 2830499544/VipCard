var quickSearch;
var isBeginFind;
var ClearBoxTimeout;
//var quickMemCard = "";
$(document).ready(function () {
    $("#txtFindMember").focus();
    //把文本框设置为只读
    $(".txtWidthFindMember").each(function () {
        $(this).css("background-color", "#eee");
        $(this).attr("readonly", true);
    });

    if ($.trim($("#txtFindMember").val()) != "") {
        BtnFindMember();
    }
    if ($("#chkNoMember").attr("checked")) {       
        NoMemberChoosed(1);
    }

    if ($("#ucMemberSearch_txtIsCanSlotCard").val() == "1") {
        $("#lblFindMember").css("display", "none");
        $("#lblQuickSearch").css("display", "none");
        
    }

    //按“回车键”响应查询
    $("#txtFindMember").keypress(function (e) {
        var keyAscii = window.event ? e.keyCode : e.which;
        if (keyAscii == 13) {
            BtnFindMember();
        }
        if ($("#ucMemberSearch_txtIsCanSlotCard").val() == "1") {
            clearTimeout(ClearBoxTimeout);
            ClearBoxTimeout = setTimeout(clearFindBox, 20);
        }
    });
    //快速查找中的查询按钮回车事件
    $("#txtQueryMem").keypress(function (e) {
        var keyAscii = window.event ? e.keyCode : e.which;
        if (keyAscii == 13) {
            btnQuickSearch();
        }
    });

    //查找按钮响应函数
    $("#btnFindMember").bind("click", BtnFindMember);

    //快速查找按钮响应函数
    $("#btnQuickSearch").bind("click", btnQuickSearch);

    //快速查找中查找按钮响应函数
    $("#btnSearch").bind("click", btnSearch);

    //判断是否显示散客消费选项
    if (location.href.indexOf("Expense.aspx") != -1) {
        $("#lblNoMember").css("display", "");
    }
    else {
        $("#lblNoMember").css("display", "none");
    }




    //读卡：感应式IC卡 按钮响应函数
    $("#ucMemberSearch_btnSenseReadCard").bind("click", btnSenseReadCard);
    //读卡：接触式IC卡 按钮响应函数

    $("#ucMemberSearch_btnContactReadCard").bind("click", btnContactReadCard);

   // $("#[classStyle=btnContactReadCard]").bind("click", btnContactReadCard);

//     var textnum = document.getElementById("<%=btnContactReadCard.ClientID %>");
//     alert(textnum.value);

    //$("#btnContactReadCard").bind("click", btnContactReadCard);

    $("#btnClearCard").bind("click", ClearCard);





    //查看会员已有充次项目
    if (location.href.indexOf("MemRechargeCount.aspx") != -1) {
        $("#btnQueryMemCount").css("display", "");
        $("#btnQueryMemCount").bind("click", BtnQueryMemCount);
    }
    else {
        $("#btnQueryMemCount").css("display", "none");
    }

    if (location.href.indexOf("MemInfo.aspx") != -1) {
        $("#trMemAll").css("display", "none");
    }
});

function ClearCard() {
    CleanAll();
    $("#txtFindMember").focus().attr("readonly", "");
    $("#btnClearCard").css("display", "none");

}


function clearFindBox() {
    if (!isBeginFind) {
        $("#txtFindMember").val("");
    }
}
/***************************************************************************
*查找按钮响应函数
***************************************************************************/
function BtnFindMember() {
    isBeginFind = true;
    var strErrorMsg;
    if ($("#chkNoMember").attr("checked") == true) {
        strErrorMsg = "您已经选择了散客，不能再查找会员！";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true //是否锁定背景
        });
        return false;
    }
    var strFindMember = $.trim($("#txtFindMember").val());
    if (strFindMember == "") {
        //        strErrorMsg = "请输入查询条件（会员卡号/卡面号码/姓名/手机号码）！";
        //        art.dialog({
        //            title: '系统提示',
        //            icon: 'error', //图标
        //            content: strErrorMsg,
        //            lock: true
        //        });
        isBeginFind = false;
        return false;
    }

    doAjax(
            "../",
            "GetMem",
            { "memCard": strFindMember },
            "json",
            function (json) {
                if (json == "") {
                    //清空全部数据
                    CleanAll();
                    if ($("#ucMemberSearch_txtIsCanSlotCard").val() == "1") {
                        isBeginFind = false;
                        $("#txtFindMember").val("");
                    }
                    else {
                        strErrorMsg = "未找到会员，请重新输入！"
                        art.dialog.through({
                            title: '系统提示',
                            icon: 'error', //图标
                            content: strErrorMsg,
                            lock: true
                        });
                    }
                    return false;
                }
                BindMemberInfo(json);
            });
}

/***************************************************************************
*绑定会员信息
***************************************************************************/
function BindMemberInfo(obj) {
    global_mem = obj[0];

    //处理会员生日的格式
    global_mem.MemBirthday = global_mem.MemBirthday.ToDate();
    //    global_mem.MemBirthday = global_mem.MemBirthday.replace("/", "-");
    //    if ((global_mem.MemBirthday.indexOf("1900") > -1) || (global_mem.MemBirthday.indexOf("0001") > -1)) {
    //        global_mem.MemBirthday = "";
    //    }
    //    else {
    //        var strArray = global_mem.MemBirthday.split(" ");
    //        global_mem.MemBirthday = strArray[0];
    //    }

    //处理会员过期时间的格式
    global_mem.MemPastTime = global_mem.MemPastTime.ToDate();
    //    global_mem.MemPastTime = global_mem.MemPastTime.replace("/", "-");
    //    if ((global_mem.MemPastTime.indexOf("2900") > -1) || (global_mem.MemPastTime.indexOf("0001") > -1)) {
    //        global_mem.MemPastTime = "";
    //    }
    //    else {
    //        var strArray = global_mem.MemPastTime.split(" ");
    //        global_mem.MemPastTime = strArray[0];
    //    }

    //处理会员创建时间的格式
    global_mem.MemCreateTime = global_mem.MemCreateTime.ToDate();
    //    global_mem.MemCreateTime = global_mem.MemCreateTime.replace("/", "-");
    //    if (global_mem.MemCreateTime.indexOf("0001") > -1) {
    //        global_mem.MemCreateTime = "";
    //    }
    //    else {
    //        var strArray = global_mem.MemCreateTime.split(" ");
    //        global_mem.MemCreateTime = strArray[0];
    //    }

    //处理会员余额为两位小数
    global_mem.MemMoney = parseFloat(global_mem.MemMoney).toFixed(2);

  //  if (location.href.indexOf("ConsumeMemCount.aspx") != -1 || location.href.indexOf("GiftExchange.aspx") != -1 || location.href.indexOf("GoodsExpense.aspx") != -1 || location.href.indexOf("MemInfo.aspx") != -1 || location.href.indexOf("MemRechargeCount.aspx") != -1 || location.href.indexOf("Expense.aspx") != -1 || location.href.indexOf("MemStorageTiming.aspx") != -1) {
        $("#spName").html(global_mem.MemName);
        $("#spLevelName").html(global_mem.LevelName);
        $("#spMoney").html(global_mem.MemMoney);
        $("#spPoint").html(global_mem.MemPoint);
    //  }

        debugger;

    // 在选择好会员时可以执行回调函数
    var isOK = true;
    if (typeof (FindMember_CallBack) == "function")
        isOK = FindMember_CallBack();
    if (isOK) {
        
        $("#txtFMemID").val(global_mem.MemID);
        $("#txtFMemCard").val(global_mem.MemCard);
        $("#txtFMemCardNumber").val(global_mem.MemCardNumber);
        $("#txtFMemName").val(global_mem.MemName);
        $("#txtFMemMoney").val(global_mem.MemMoney);
        $("#txtFMemPoint").val(global_mem.MemPoint);
        $("#txtFMemCountNumber").val(global_mem.MemCountNumber);
        if (global_mem.MemConsumeMoney != "") {
            $("#txtFConsumeMoney").val(parseFloat(global_mem.MemConsumeMoney).toFixed(2));
        }
        else {
            $("#txtFConsumeMoney").val("0.00");
        }

        var Pointtitle = ""
        var Leveltitle = "";
        if (global_mem.LevelPointPercent > 0) {
            Pointtitle = global_mem.LevelPointPercent * 100 + "元积1分";
        }
        else {
            Pointtitle = "不积分";
        }

        if (global_mem.LevelDiscountPercent == 1) {
            Leveltitle = "不打折";
        }
        else {
            Leveltitle = global_mem.LevelDiscountPercent * 10 + "折";
        }
        //$("#txtFMemLevelName").val(global_mem.LevelName + "(" + Leveltitle + "/" + Pointtitle + ")").attr('title', "(" + Leveltitle + "/" + Pointtitle + ")"); ;
        $("#txtFMemLevelName").val(global_mem.LevelName);

        //        $("#txtFMemLevelName").val(global_mem.LevelName + "(折扣率：" + global_mem.LevelDiscountPercent + ")");
        $("#txtFMemShopName").val(global_mem.ShopName);
        $("#txtFMemMobile").val(global_mem.MemMobile);
        global_mem.MemState = GetMemState(global_mem.MemState);
        $("#txtFMemState").val(global_mem.MemState);
        $("#txtFMemBirthday").val(global_mem.MemBirthday);
        global_mem.MemSex = GetMemSex(global_mem.MemSex);
        $("#txtFMemSex").val(global_mem.MemSex);
        $("#txtFMemIdentityCard").val(global_mem.MemIdentityCard);
        $("#txtFMemPastTime").val(global_mem.MemPastTime);
        $("#txtFMemEmail").val(global_mem.MemEmail);
        //        $("#txtFMemAddress").val(global_mem.MemAddress);
        $("#txtFMemCreateTime").val(global_mem.MemCreateTime);
        $("#txtFMemUserName").val(global_mem.UserName);

        if ($("#ucMemberSearch_txtIsCanSlotCard").val() == "1") {
            $("#btnClearCard").css("display", "");
            $("#txtFindMember").attr("readonly", "readonly");
        }
    }
    else {
        global_mem = {};
        $("#txtFindMember").val("");
        $("#txtFindMember").focus();
    }
}

/******************************************************************************
*获取会员状态：正常，锁定，挂失
******************************************************************************/
function GetMemState(intMemState) {
    var strMemState;
    if (intMemState == 1) {
        strMemState = "锁定";
    }
    else if (intMemState == 2) {
        strMemState = "挂失";
    }
    else {
        strMemState = "正常";
    }
    return strMemState;
}

/******************************************************************************
*获取会员性别：男，女
******************************************************************************/
function GetMemSex(bolMemSex) {
    var strMemSex;
    if (bolMemSex == "True" || bolMemSex == "男") {
        strMemSex = "男士";
    }
    else {
        strMemSex = "女士";
    }
    return strMemSex;
}
function GetDataTime(time) {

    Date = year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
    return Date;
}

/****************************************************************************
*是否展开所有会员信息
****************************************************************************/
function ShowAll(type) {
    if (type == 1) {
        $("#trCardNumberAndMobile").css("display", "");
        $("#trPastTimeAndShop").css("display", "");
        $("#trBirthdayAndSex").css("display", "");
        $("#trIdentityAndEmai").css("display", "");
        $("#trCreateTimeAndUser").css("display", "");

        //        $("#trCreateTimeAndUser").css("display", "");
        $("#trNoDisplayAll").css("display", "");
        $("#trDisplayAll").css("display", "none");
    }
    else {
        //        $("#trMobileAndState").css("display", "none");
        //        $("#trBirthdayAndSex").css("display", "none");
        //        $("#trIdentityAndPastTime").css("display", "none");
        //        $("#trEmailAndAddress").css("display", "none");
        //        $("#trCreateTimeAndUser").css("display", "none");
        $("#trCardNumberAndMobile").css("display", "none");
        $("#trPastTimeAndShop").css("display", "none");
        $("#trBirthdayAndSex").css("display", "none");
        $("#trIdentityAndEmai").css("display", "none");
        $("#trCreateTimeAndUser").css("display", "none");
        $("#trNoDisplayAll").css("display", "none");
        $("#trDisplayAll").css("display", "");
    }
}

function ShowMemDetail(type) {
    if (type == 1) {
        $("#trMemAll").css("display", "none");
        $("#Panel_MemAllMsg").css("display", "");
    }
    else {
        $("#Panel_MemAllMsg").css("display", "none");
        $("#trMemAll").css("display", "");
    }
}

/****************************************************************************
*散客选择时响应函数
****************************************************************************/
function NoMemberChoosed(type) {
    if ($("#chkNoMember").attr("checked") == true) {
        $("#txtFindMember").attr("disabled", "disabled");
        $("#btnClearCard").css("display", "none");
        //清空全部数据
        CleanAll();
    }
    else {
        $("#txtFindMember").val("").removeAttr("readonly").removeAttr("disabled");      
    }
    var isOK = true;
    if (typeof (NoMember_CallBack) == "function") {
        isOK = NoMember_CallBack(type);
    }

}

/*****************************************************************************
*清空全部数据
*****************************************************************************/
function CleanAll() {
    global_mem = {};
    $("#txtFindMember").val("");
    $("#txtFMemID").val("");
    $("#txtFMemCard").val("");
    $("#txtFMemName").val("");
    $("#txtFMemMoney").val("");
    $("#txtFMemPoint").val("");
    $("#txtFMemLevelName").val("");
    $("#txtFMemShopName").val("");
    $("#txtFMemMobile").val("");
    $("#txtFMemState").val("");
    $("#txtFMemBirthday").val("");
    $("#txtFMemSex").val("");
    $("#txtFMemIdentityCard").val("");
    $("#txtFMemPastTime").val("");
    $("#txtFMemEmail").val("");
    //    $("#txtFMemAddress").val("");
    $("#txtFMemCreateTime").val("");
    $("#txtFMemUserName").val("");

    $("#txtFMemCountNumber").val("");
    $("#txtFConsumeMoney").val("");

    $("#spName").html("");
    $("#spLevelName").html("");
    $("#spMoney").html("");
    $("#spPoint").html("");
}

var MypageSize = 8;
var MycurrentPage = 1;
//快速查找按钮响应事件
function btnQuickSearch() {
    $("#txtQueryMem").focus();
    quickSearch = art.dialog({
        title: '快速查找',
        content: document.getElementById('divQuickSearch'),
        id: 'divQuickSearch',
        lock: true
    });

    doAjax("../",
           "GetQueryMem",
           {
               "size": MypageSize,
               "index": MycurrentPage,
               "memCard": $("#txtQueryMem").val()
           },
           "json",
           function (json) {
               CreateMenQuickSearchList(json.List);
               MyCreateGoodsProductPager(json.RecordCount);
           }

    );
}

/****************************************************************************************************
*分页
*****************************************************************************************************/
function MyCreateGoodsProductPager(resCount) {
    var page = new CommonPager(
        "MemPageList",
        MypageSize,
        resCount,
        MycurrentPage,
        function (p) {
            MycurrentPage = parseInt(p);
            btnQuickSearch();
        }
    );
    page.ShowSimple();
}
//创建表格
function CreateMenQuickSearchList(obj) {
    var html = ''
    //            + '<tr class=\"th\">'
    //            + ' <td><b>会员卡号</b></td><td><b>会员姓名</b></td><td><b>会员等级</b></td><td><b>账户余额</b></td><td><b>积分数量</b></td>'
    //            + '</tr>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            var bg = index % 2 == 0 ? "#eee" : "#fff";
            html += "<tr class=\"td\" ondblclick=\"javascript:QuickSelectMem(\'" + item.MemCard + "\');\">"
                 + '<td style=" width:120px;word-break: break-all">' + item.MemCard + '</td>'
                 + '<td style=" width:120px;text-align: left">' + item.MemName + '</td>'
                 + '<td style=" width:120px">' + item.LevelName + '</td>'
                 + '<td style=" width:120px;text-align: right">' + parseFloat(item.MemMoney).toFixed(2) + '</td>'
                 + '<td style="text-align: right">' + Math.floor(item.MemPoint) + '</td>'
                 + '</tr>';
        });
    }
    else {
        html += '<td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="5">未找到符合此条件的数据！请重试！</td>';
    }
    $("#tbQuickSearch").html(html);
}
function QuickSelectMem(memCard) {
    $("#txtFindMember").val(memCard);
    BtnFindMember();
    quickSearch.close();
}
function btnSearch() {
    //quickMemCard = $("#txtQueryMem").val();
    btnQuickSearch();
}






/*******************************************************************************
*读卡：感应式IC卡 按钮响应函数
*******************************************************************************/
function btnSenseReadCard() {
    if ($("#chkNoMember").attr("checked") == true) {
        art.dialog({
            title: '系统提示',
            content: "您已经选择了散客，不能再查找会员！",   //弹出层显示文本
            time: 3,
            lock: true //是否锁定背景
        });
        return;
    }
    CardReader.ReadCard();  //读卡         
    setTimeout("setValue()", 200); //延时，等待刷卡机读取卡号
}
function setValue() {
    if (CardReader.errMsg != "") {
        art.dialog({
            lock: true,
            title: "系统提示",
            content: CardReader.errMsg,
            time: 3
        });
    }
    else {
        if (CardReader.CardID != "" && CardReader.CardID != null) {
            $("#txtFindMember").val(CardReader.CardID);
            $("#btnFindMember").click();
        }
        else {
            $("#txtFindMember").val("");
        }
    }

}
/*******************************************************************************
*读卡：接触式IC卡 按钮响应函数
*******************************************************************************/
function btnContactReadCard() {


    if ($("#chkNoMember").attr("checked") == true) {
        art.dialog({
            title: '系统提示',
            content: "您已经选择了散客，不能再查找会员！",   //弹出层显示文本
            time: 3,
            lock: true //是否锁定背景
        });
        return;
    }
    var status = null;
    var getValue = null;
    MWReaderCtl.MWic_init(0, 9600);
    status = MWReaderCtl.LastRet;
    if (status == 0) {
        MWReaderCtl.MWcsc_4442(6, "ffffff");
        status = MWReaderCtl.LastRet;
        if (status != 0) {
            art.dialog({
                title: '系统提示',
                content: '密码验证错误，请检查设备的LED灯是否处于绿色状态，若不是请将IC卡插好，否则请与管理员联系！！',
                lock: true,
                time: 3
            })
            MWReaderCtl.MWic_exit();
            return;
        }
        getValue = MWReaderCtl.MWsrd_4442(60, 20);
        status = MWReaderCtl.LastRet;
        if (status == 0) {
            if (/^[F,f]{20,40}$/.test(getValue)) {
                art.dialog({
                    title: '系统提示',
                    content: '此卡处于初始化状态，读取卡号失败！！',
                    lock: true,
                    time: 3
                })
                MWReaderCtl.MWic_exit();
                return;
            }
            else {
                var reg = /(\d{4,20})([F,f]*)/;
                if (reg.test(getValue)) {
                    getValue = RegExp.$1;
                    $("#txtFindMember").val(getValue);
                    $("#btnFindMember").click();
                }
            }
        }
        else {
            art.dialog({
                title: '系统提示',
                content: '检查此卡是否初始化时失败，请与管理员联系或重试！！',
                lock: true,
                time: 3
            })
            MWReaderCtl.MWic_exit();
            return;
        }
        MWReaderCtl.MWic_exit();
    }
    else {
        art.dialog({
            title: '系统提示',
            content: '接触式IC卡设备打开失败，请检查设备连接！',
            lock: true,
            time: 3
        });
    }
}