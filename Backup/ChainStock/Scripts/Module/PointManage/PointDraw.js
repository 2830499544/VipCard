// 页面加载时执行
var href = "";
$(document).ready(function () {
    $("#txtDrawPoint").bind("keyup", TotalMoney);
        $("#txtDrawPoint").bind("click", MoneySelect);
//    //没有查询会员时 所有空间禁用
//    if ($.isEmptyObject(global_mem)) {
//        $('input[name="radPointType"]').attr("disabled", "disabled");
//        $("#txtChangeNumber").attr("disabled", "disabled");
//        $("#txtChangeRemark").attr("disabled", "disabled");
//    };
    //保存按钮响应函数
    $("#btnChangeSave").bind("click", BtnChangeSave);

    //重置按钮响应函数
    $("#btnChangeReset").bind("click", BtnChangeReset);
    var txtDrawType = $("#txtDrawType").val();
    if (txtDrawType == "2") {
        href = "/PointManage/PointDraw.aspx?PID=147";
    } else if (txtDrawType == "3") {
        href = "/PointManage/PointDraw.aspx?PID=148";
    }
});

/*******************************************************************************
*提现合计处理
*******************************************************************************/
function TotalMoney() {
    var dclDrawPoint = $("#txtDrawPoint").val() != "" ? parseFloat($("#txtDrawPoint").val()) : 0;
    var dclRate = $("#lbl_PointDrawPercent").html()
    $("#txtDrawAmount").val( parseFloat(dclDrawPoint * dclRate).toFixed(2));
        
}
function MoneySelect() {
    if ($("#txtDrawPoint").focus) {
        $("#txtDrawPoint").select();
    }
}
/*******************************************************************************
*保存按钮响应函数
*******************************************************************************/
function BtnChangeSave() {
    var strErrorMsg = "";
//    if ($.isEmptyObject(global_mem)) {
//        strErrorMsg += "<li>请先选择会员！</li>";
//    }

    var point = $.trim($("#txtDrawPoint").val());
    var totalPoint = $("#txtTotalPoint").val();

    
    var strReg = /^[0-9]*[1-9][0-9]*$/; //正则表达式
    if (point == "") {
        strErrorMsg += "<li>请输入提现积分数量！</li>";
    }
    else if (!strReg.test(point)) {
        strErrorMsg += "<li>提现积分数量必须是正整数！</li>";
    }
    else if (point.length > 5) {
        strErrorMsg += "<li>提现积分必须是长度不超过5位的整数！</li>"
    }
    else if (point > totalPoint) {
        strErrorMsg += "<li>超过可提现积分额度！</li>"
    }

//    else if (point > totalPoint) {
//        strErrorMsg += "<li>超出可提现积分额度！</li>"
//    }
//    var type = $('input[name="radPointType"]:checked').val();
//    if (type == "1") {
//        if (parseInt(point) > parseInt(global_mem.MemPoint)) {
//            strErrorMsg += "<li>提现的积分超过此商家拥有的积分，请重新输入！</li>";
//        }
//    }
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
        title: '系统提示',
        content: "将提现积分:[" + $.trim($("#txtDrawPoint").val()) + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();

            doAjax("../",
          "PointDraw",
          {
              "DrawAccount": $("#lblDrawAccount").html(),
              "DrawShopID": $("#txtShopID").val(),
              "DrawPoint": $("#txtDrawPoint").val(),
              "DrawAmount": $("#txtDrawAmount").val(),
              "DrawCreateUserID": $("#txtDrawUserID").val(),
              "DrawCreateTime": $("#lblDrawTime").html(),
              "DrawRemark": $("#txtDrawRemark").val(),
              "DrawStatus":0             

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
                              content: ("会员状态异常，请联系管理员！"),
                              lock: true
                          });
                       break;
                   case -3:
                       art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("系统错误 请与系统管理员联系！"),
                              lock: true
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
                   case -8:
                       art.dialog
                            ({
                                title: '系统提示',
                                time: 4,
                                content: ("该商家剩余可用积分不足，请重新填写！"),
                                lock: true
                            });
                       break;
                   default:
                       if (json.Success != "0") {
                           var showtime = 1;
                           if (json.strUpdateMemLevel != "") {
                               showtime = 4;
                           }
                           art.dialog({
                               title: '系统提示',
                               time: showtime,
                               content: '积分提现成功！',
                               close: function () {
                               $("#txtDrawPoint").val("");
                               $("#txtDrawAmount").val("");
                              
//                                   var lblPrintTitle = $("#lblPrintTitle").html();
//                                   var userName = $("#lblPointChangeUser").html();
//                                   var exchangeType = $("input[name='radPointType']:checked").val();
//                                   var changeNumber = $("#txtChangeNumber").val();
//                                   var lblPrintFoot = $("#lblPrintFoot").html();
//                                   var print = $("#chkPrint").attr("checked");
//                                   var pointChangeTime = $("#lblPointChangeTime").html();

//                                   //     获取打印份数
//                                   var PointNum = $("#PointNum").val();
//                                   Print_PointChange(userName, global_mem, exchangeType, changeNumber, lblPrintTitle, lblPrintFoot, pointChangeTime, print, 1, PointNum);
                               },
                               lock: true
                           });
                           location.href = href;
                       }
                       else {
                           art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("积分提现成功，" + json.strUpdateMemLevel + "短信余额不足，不能发送短信，请充值短信！"),
                                  close: function () {
//                                      var lblPrintTitle = $("#lblPrintTitle").html();
//                                      var userName = $("#lblPointChangeUser").html();
//                                      var exchangeType = $("input[name='radPointType']:checked").val();
//                                      var changeNumber = $("#txtChangeNumber").val();
//                                      var lblPrintFoot = $("#lblPrintFoot").html();
//                                      var print = $("#chkPrint").attr("checked");
//                                      var pointChangeTime = $("#lblPointChangeTime").html();
//                                      //     获取打印份数
//                                      var PointNum = $("#PointNum").val();
//                                      Print_PointChange(userName, global_mem, exchangeType, changeNumber, lblPrintTitle, lblPrintFoot, pointChangeTime, print, 1, PointNum);
                                  },
                                  lock: true
                              });
                           location.href = href;
                       }
               }
           });
        },
        cancelVal: '取消',
        cancel: true
    });

}


/*******************************************************************************
*重置按钮响应函数
*******************************************************************************/
function BtnChangeReset() {
    window.location.href = '../PointManage/PointChange.aspx?PID=15';
}

/****************************************************************************************************
*在选择好会员时可以执行回调函数
*****************************************************************************************************/
function FindMember_CallBack() {
    var strErrorMsg;
    if (global_mem.MemState != 0) {
        strErrorMsg = "当前会员卡处于锁定或者挂失状态，暂不允许进行积分变动。";
        art.dialog({
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    if (global_mem.MemIsPast == "True") {
        strErrorMsg = "当前会员卡已过期，暂不允许进行积分变动。";
        art.dialog({
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    //查找到会员后 所有控件解禁
    if (global_mem.MemID != "") {
        $('input[name="radPointType"]').attr("disabled", "");
        $("#txtChangeNumber").attr("disabled", "");
        $("#txtChangeRemark").attr("disabled", "");
        $("#txtChangeNumber").focus();
    }
    $("#SpanCurrentPoint").html(global_mem.MemPoint);
    return true;
}