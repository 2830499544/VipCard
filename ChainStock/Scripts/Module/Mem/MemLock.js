$(document).ready(function () {
    //没有查询会员时 所有空间禁用
    if ($.isEmptyObject(global_mem)) {
        $("#sltMemNewState").attr("disabled", "disabled");
        $("#txtMemLockRemark").attr("disabled", "disabled");
    };
    if ($("#MemCard").val() != null) {
        $("#txtFindMember").val($("#MemCard").val());
    }
    //保存按钮响应函数
    $("#btnMemLockSave").bind("click", BtnMemLockSave);
    $("#btnMemLockReset").bind("click", btnMemLockReset);
});

/*******************************************************************************
*重置按钮响应函数
*******************************************************************************/
function btnMemLockReset() {
    window.location.href = "../Member/MemLock.aspx?PID=8";
}

/*****************************************************************************************
*保存按钮响应函数
*****************************************************************************************/
function BtnMemLockSave() {
    //if (!confirm("您确定要修改此会员状态么？")) return false;
    //if (state == 当前状态) { alert("请选择需要更改的状态！"); return false; } 

    var strErrorMsg = "";
    var state = $("#sltMemNewState").val();
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员！</li>";
    }

    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        //var throughBox = art.dialog.through;
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
        content: '将为会员 [' + global_mem.MemName + '],状态修改为:[' + (state == 1 ? "锁定" : (state == 2 ? "挂失" : "正常")) + ']。\n确定操作吗？',
        lock: true,
        ok: function () {
            this.close();
//            this.lock();
            doAjax(
           "../",
           "MemLock",
           {
               "memID": global_mem.MemID,
               "state": state,
               "remark": $("#txtMemLockRemark").val()
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
                                content: '会员状态改变成功！',
                                close: function () { location.href = location.href; },
                                lock: true
                            });
                        break;
                }
            });
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
    if (global_mem.MemIsPast == "True") {
        strErrorMsg = "当前会员卡已过期，暂不允许进行设置。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    var strMemState = (global_mem.MemState == 1) ? "锁定" : ((global_mem.MemState == 2) ? "挂失" : "正常");
    $("#MemCurrentState").html(strMemState);

    if (global_mem.MemID != "") {
        $("#sltMemNewState").attr("disabled", "");
        $("#txtMemLockRemark").attr("disabled", "");
        $("#sltMemNewState").focus();
    }
    return true;
}