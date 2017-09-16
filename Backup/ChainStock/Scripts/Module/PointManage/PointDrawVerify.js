$(document).ready(function () {
   
    //确定按钮响应函数
    $("#btnOK").bind("click", BtnOK);

    //取消按钮响应函数
    $("#btnCancel").bind("click", BtnCancel);
});


/***********************************************************
*通过审核
************************************************************/
function AllowExchange(strID, strAccount) {
    var strtype = $("#txtDrawType").val();


    art.dialog({
        title: '系统提示',
        content: '确定订单' + strAccount + '通过审核吗？',
        lock: true,
        ok: function () {
            this.close();

            //            this.lock();
            doAjax("../",
               "PointDrawVerify",
               {
                   "DrawID": strID
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


                        case 1:
                            art.dialog
                                    ({
                                        title: '系统提示',
                                        time: 2,
                                        content: ("该订单通过审核！"),
                                        lock: true,
                                        close: function () {
                                            if (strtype == "2") {
                                                location.href = "../PointManage/PointDrawVerify.aspx?PID=149";
                                            }
                                            if (strtype == "3") {
                                                location.href = "../PointManage/PointDrawVerify.aspx?PID=150";
                                            }

                                        }
                                    });
                            break;
                    }
                });
            return false;
            alert("2");
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    });
}


/***********************************************************
*不通过审核
************************************************************/
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

/****************************************************************************************
*确定按钮响应函数
****************************************************************************************/
function BtnOK() {
    BackExchange.close();
    doAjax("../",
               "NoExchange",
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
                                    close: function () { location.href = "../PointManage/ExchangeVerify.aspx?PID=92"; }
                                });
                            break;
                    }
                });
}

/****************************************************************************************
*取消按钮响应函数
****************************************************************************************/
function BtnCancel() {
    $("#txtExchangeRemark").val("");
    BackExchange.close();
}