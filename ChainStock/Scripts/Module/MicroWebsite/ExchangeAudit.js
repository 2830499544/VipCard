$(function () {
    //BindNullList("tbExchangeVerify");

    //确定按钮响应函数
    $("#btnOK").bind("click", BtnOK);

    //取消按钮响应函数
    $("#btnCancel").bind("click", BtnCancel);
})

function IsShow(id, arg2) {
    if (arg2 > 0) {
        $("#img" + id).css("display", "");
    }
    else {
        $("#img" + id).css("display", "none");
        $("#a" + id).css("padding-left", "22px");
    }
}

function ShowDetail(id, arg2) {
    if (arg2 > 0) {
        if ($("#data" + id).css("display") == "none") {
            $("#data" + id).css("display", "");
            $("#detail" + id).css("display", "");
            $("#img" + id).attr("src", "../Inc/Style/images/minus.gif")
        }
        else {
            $("#data" + id).css("display", "none");
            $("#detail" + id).css("display", "none");
            $("#img" + id).attr("src", "../Inc/Style/images/plus.gif")
        }
    }
    else {
        $("#data" + id).css("display", "none");
        $("#detail" + id).css("display", "none");
    }
}

var intExchangeID = 0;
function NoExchange(strID) {
    intExchangeID = strID;
    BackExchange = art.dialog({
        title: '退回兑换礼品申请',
        content: document.getElementById('divBackExchange'),
        id: 'divBackExchange',
        lock: true
    });
}

function BtnOK() {
    BackExchange.close();
    doAjax("../",
               "MicroWebsiteNoExchange",
               {
                   "ID": intExchangeID,
                   "ExchangeRemark": $("#txtExchangeRemark").val()
               },
               "json",
                function (json) {
                    switch (json) {
                        case 0:
                            art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统错误，请再次点击退回！")
                                 });
                            break;
                        case 1:
                            art.dialog
                                ({
                                    title: '系统提示',
                                    time: 2,
                                    content: ("退回成功！"),
                                    lock: true,
                                    close: function () { location.href = location.href; }
                                });
                            break;
                    }
                });
}

function BtnCancel() {
    $("#txtExchangeRemark").val("");
    BackExchange.close();
}

/***********************************************************
*通过审核
************************************************************/
function AllowExchange(strID, strAccount) {
    art.dialog({
        title: '系统提示',
        content: '确定订单 [' + strAccount + '] 通过审核吗？',
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
               "MicroWebsiteAllowExchange",
               {
                   "ID": strID
               },
               "json",
                function (json) {
                    switch (json) {
                        case -1:
                            art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("会员积分不足，不能兑换该订单礼品！"),
                                     lock: true
                                 });
                            break;
                        case -2:
                            art.dialog
                            ({
                                title: '系统提示',
                                time: 4,
                                content: ("礼品库存不足，不能兑换该订单礼品！"),
                                lock: true
                            });
                            break;
                        case -3:
                            art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("会员处于锁定状态，不能兑换该订单礼品！"),
                                     lock: true
                                 });
                            break;
                        case -4:
                            art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("会员处于挂失状态，不能兑换该订单礼品！"),
                                     lock: true
                                 });
                            break;
                        case 1:
                            art.dialog
                                    ({
                                        title: '系统提示',
                                        time: 2,
                                        content: ("该订单通过审核！"),
                                        lock: true,
                                        close: function () { location.href = location.href; }
                                    });
                            break;
                    }
                });
            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    });
}