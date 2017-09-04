$(document).ready(function () {

    if (!$("#chkIsPast").attr("checked")) {
        $("#frmMemDelay").attr("disabled", "disabled");
        $("#txtFindMember").attr("disabled", "disabled");
        $("#btnFindMember").attr("disabled", "disabled");
        $("#txtDelayTime").attr("disabled", "disabled");
        $("#txDelayRemark").attr("disabled", "disabled");

        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: "未启用会员有效期功能,不能进行会员延期!",
            lock: true
        });
        return false;
    }

    //没有查询会员时 所有空间禁用
    if ($.isEmptyObject(global_mem)) {
        $("#txtDelayTime").attr("disabled", "disabled");
        $("#txDelayRemark").attr("disabled", "disabled");
    };
    $("#txtDelayTime").bind("keyup", DelayTime);
    $("#btnDelaySave").bind("click", btnDelaySave);
    $("#btnDelayReset").bind("click", btnDelayReset);

    if ($("#MemCard").val() != null) {
        $("#txtFindMember").val($("#MemCard").val());
    }
});

//输入延期时间 响应事件
function DelayTime() {

//判断输入是否为空  为空就赋值为0
    if ($("#txtDelayTime").val() == "") {
        $("#txtDelayTime").val("0");
    }
    var strErrorMsg = "";
    $("#spNewPastTime").html("");
    var pastTime = "2900-1-1 00:00:00";
    var time = $.trim($("#txtDelayTime").val());
    var year = 0;

    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员,再对会员进行延期操作!</li>";
    }
    if (!time.IsNumber()) {
        $("#txtDelayTime").val("0");
//        strErrorMsg += "<li>延期的时间只能为数字;</li>";
        $("#spNewPastTime").html("");
    }
    if (parseInt(time) > 36) {
        $("#lblError").attr("style", "font-size: 14px; font-weight: bold;color:Red")
    }
    else {
        var Ptime = $("#txtMemPastTime").html();
        time = parseInt(time);
        if ((nowPastTime.getMonth() + time) > 12) {
            year = parseInt((nowPastTime.getMonth() + time) / 12);
            time = parseInt((nowPastTime.getMonth() + time) % 12) - nowPastTime.getMonth();
        }
        if ((nowPastTime.getMonth() + time) == 12) {
            time = -nowPastTime.getMonth();
            year++;
        }
        newPastTime = nowPastTime.Add("month", (parseInt(time)));
        newPastTime = newPastTime.Add("year", (parseInt(year)));
        $("#spNewPastTime").html("延期后,有效期至:" + newPastTime.Format(1));
        $("#lblError").attr("style", "font-size: 14px; font-weight: bold;");
    }
    //错误提示
    if (strErrorMsg != "") {
        $("#spNewPastTime").html("");
        strErrorMsg = "<div>操作出现以下错误,请核查后重试!</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
}

//重置按钮响应函数
function btnDelayReset() {
    window.location.href = '../Member/MemDelay.aspx?PID=7';
};


//保存按钮响应函数
function btnDelaySave() {
    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员!</li>";
    }
    if ($.trim($("#txtDelayTime").val()) == "") {
        strErrorMsg += "<li>请输入此会员要延期的时间;</li>"
    }
    if ($("#spNewPastTime").html() == "") {
        strErrorMsg += "<li>延期时间不正确,请重新输入;</li>"
    }
    //错误提示
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误,请核查后重试!</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    //newPastTime.Format(1)   newPastTime.MyFormat(0)
    art.dialog({
        title: '系统提示',
        content: '将为会员 [' + global_mem.MemName + '],延长有效期至:[' + newPastTime.Format(1) + ']。\n确定操作吗？',
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
                   "DelayTime",
                   {
                       "memID": global_mem.MemID,
                       "newPastTime": newPastTime.Format(1),
                       "remark": $("#txDelayRemark").val()
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
                            default:
                                art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 0.5,
                                     content: '会员延期成功！',
                                     close: function () { location.href = location.href; },
                                     lock: true
                                 });
                                break;
                        }
                    });
            return false;
        },
        cancelVal: '取消',
        cancel: true
    });


}
/****************************************************************************************************
*在选择好会员时可以执行回调函数
*****************************************************************************************************/
function FindMember_CallBack() {
    var strErrorMsg;

    ///## 选择会员时，清除延期数据
    $("#spNewPastTime").html("");
    $("#IsYongJiu").val("");
    $("#txtDelayTime").val("");
    //    var nowPastTime = new Date();
    //    var newPastTime;
    if (global_mem.MemState != 0) {
        strErrorMsg = "当前会员卡处于锁定或者挂失状态，暂不允许进行延期。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    if (global_mem.MemIsPast == "False") {
        //nowPastTime = global_mem.MemPastTime.ToDate();
        nowPastTime = global_mem.MemPastTime;
        if (nowPastTime != "永久有效") {
            $("#txtMemPastTime").html("<font color='red'>" + nowPastTime + "<font>");
            $("#spNewPastTime").html("");
            $("#txtDelayTime").val("12");
            $("#txtDelayTime").attr("disabled", "");
            $("#txtDelayTime").focus();
            nowPastTime = nowPastTime.ToDateTime();
        }
        else {
            $("#txtMemPastTime").html("<font color='red'>会员永久有效无需延期！</font>");
            $("#txtDelayTime").attr("disabled", "disabled");
            $("#btnDelaySave").attr("disabled", "disabled");
            $("#IsYongJiu").val(1);
        }
    }
    else {
        nowPastTime = global_mem.MemPastTime;
        $("#txtMemPastTime").html("<font color='red'>" + nowPastTime + " 此会员已经过期！</font>");
        $("#spNewPastTime").html("");
        $("#txtDelayTime").val("12");
        $("#txtDelayTime").attr("disabled", "");
        $("#txtDelayTime").focus();
        nowPastTime = nowPastTime.ToDateTime();
    }
    //查找到会员后 所有控件解禁
    if (global_mem.MemID != "") {
        $("#txDelayRemark").attr("disabled", "");
        $("#txtDelayTime").focus();
        if (nowPastTime != "永久有效") {
            $("#btnDelaySave").attr("disabled", "");
            DelayTime();
        }
    }
    return true;
}