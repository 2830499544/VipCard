
$(document).ready(function () {
    if ($("#txtQueryMem")) {
        $("#txtQueryMem").focus();
    }
    $("#txtShop").val($("#HDsltshop").val());

//    绑定日期控件
    $('#txtStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
//        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm', maxDate: '%y-%M-%d %H:%m:%s', isShowClear: false, readOnly: true });
    });

//    绑定日期控件
    $('#txtEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
        //WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm', maxDate: '%y-%M-%d %H:%m:%s', isShowClear: false, readOnly: true });
    });

    //绑定日期控件
    $('#txtStaffStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });

    });
    //绑定日期控件
    $('#txtStaffEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });

    });

    //绑定空列表
    //    BindNullList("gvGoodsList");
    //    BindNullList("gvRptExpense");
    //    BindNullList("gvRptMem");
    //    BindNullList("gvMemExpense");
    //    BindNullList("gvRptMemRecharge");
    //    BindNullList("gvRptPointChange");
    //    BindNullList("gvRptPointExchange");
    //    BindNullList("gvPointRankList");
    //    BindNullList("gvRptShop");
    //    BindNullList("tbMemCount");
    //    BindNullList("tbStaff");
    //    BindNullList("rptEmptyBillsList");
    //    BindNullList("gvGoodsLog");

    //    BindNullList("gvRptStaffMoney");
});

/***********************************************************
*会员充次 展开详情
************************************************************/
function ShowMemCountDetail(id) {
    if ($("#data" + id).css("display") == "none") {
        $("#data" + id).css("display", "");
    }
    else {
        $("#data" + id).css("display", "none");
    }
}
/***********************************************************
*展开详情
arg2为 是否展开的条件参数
************************************************************/
function ShowDetail(id, arg2) {
    if (arg2 > 0) {
        if ($("#data" + id).css("display") == "none") {
            $("#data" + id).css("display", "");
            $("#img" + id).attr("src", "../Inc/Style/images/minus.gif")
        }
        else {
            $("#data" + id).css("display", "none");
            $("#img" + id).attr("src", "../Inc/Style/images/plus.gif")
        }
    }
    else {
        $("#data" + id).css("display", "none");
    }
}

function IsShow(id, arg2) {
    if (arg2 > 0) {
        $("#img" + id).css("display", "");
    }
    else {
        $("#img" + id).css("display", "none");
        $("#a" + id).css("padding-left", "22px");
    }
}

function RechargeRevoke(rechargeID, memID) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要撤销此充值吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
             'RechargeRevoke', { "rechargeID": rechargeID, "memID": memID }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统异常 记录未能删除，请重试！"),
                                       lock: true
                                   });
                             break;
                         case -1:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统错误 请与系统管理员联系！"),
                                       lock: true
                                   });
                             break;
                         case -2:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("会员充值之后已经使用余额进行过消费，不可撤销此次充值！"),
                                       lock: true
                                   });
                             break;
                         case -3:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("会员充值之后已经使用余额进行过充次，不可撤销此次充值！"),
                                       lock: true
                                   });
                             break;
                         default:
                             art.dialog
                                   ({
                                       time: 1,
                                       content: '会员充值撤单成功！',
                                       close: function () { location.href = "RptMemRecharge.aspx?PID=21"; }
                                   });
                             break;
                     }
                 });

            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    });
    return false;
}

