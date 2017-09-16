$(function () {
    $("#btnCancel").bind("click", BtnCancel);

    $("#btnOK").bind("click", BtnOK);
})

function BtnCancel() {
    $("#txtExchangeRemark").val("");
    BackExchange.close();
}

function BtnOK() {
    BackExchange.close();
    doAjax("../",
               "MicroWebsiteGoodsNoExchange",
               {
                   "MicroOrderID": intExchangeID,
                   "MicroOrderMark": $("#txtExchangeRemark").val(),
                   "sendSMS": $("#chkSMS").attr("checked")
               },
               "text",
                function (text) {
                    switch (text) {
                        case "0":
                            art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统错误，请再次点击退回！")
                                 });
                            break;
                        case "1":
                            art.dialog
                                ({
                                    title: '系统提示',
                                    time: 1,
                                    content: ("退回成功！"),
                                    lock: true,
                                    close: function () { location.href = location.href; }
                                });
                            break;
                    }
                });
}

function AllowExchange(MicroOrderID, MicroOrderAccount) {
    art.dialog({
        title: '系统提示',
        content: '确定订单 [' + MicroOrderAccount + '] 通过审核吗？',
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
                           "MicroWebsiteGoodsAllowExchange",
                           {
                               "MicroOrderID": MicroOrderID,
                               "sendSMS": $("#chkSMS").attr("checked")
                           },
                           "text",
                            function (text) {
                                switch (text) {
                                    case "0":
                                        art.dialog
                                             ({
                                                 title: '系统提示',
                                                 time: 4,
                                                 content: ("系统繁忙，请稍后重试！"),
                                                 lock: true
                                             });
                                        break;
                                    case "-3":
                                        art.dialog
                                             ({
                                                 title: '系统提示',
                                                 time: 4,
                                                 content: ("会员处于锁定状态，不能审核通过该商品订单！"),
                                                 lock: true
                                             });
                                        break;
                                    case "-4":
                                        art.dialog
                                             ({
                                                 title: '系统提示',
                                                 time: 4,
                                                 content: ("会员处于挂失状态，不能审核通过该商品订单！"),
                                                 lock: true
                                             });
                                        break;
                                    case "1":
                                        art.dialog
                                        ({
                                            title: '系统提示',
                                            time: 1,
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

var intExchangeID = 0;
function NoExchange(MicroOrderID) {
    intExchangeID = MicroOrderID;
    BackExchange = art.dialog({
        title: '退回商品申请',
        content: document.getElementById('divBackExchange'),
        id: 'divBackExchange',
        lock: true
    });
}