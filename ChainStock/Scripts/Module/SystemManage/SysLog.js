$(document).ready(function () {
    //日志清理按钮响应的事件
    $("#btnCleanSysLog").bind("click", btnCleanSysLog);
    //绑定日期控件
    $('#txtStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });

    });

    //绑定日期控件
    $('#txtEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });

    });
})

function btnCleanSysLog() {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要清理日志吗? 此操作不可恢复！',
        ok: function () {
            this.close();
            doAjax("../",
             'CleanSysLog',
             {
                 "cleanTime": $("#sltCleanTime").val()
             },
             "json",
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
                         default:
                             art.dialog
                                   ({
                                       time: 0.5,
                                       content: '删除成功！',
                                       close: function () { location.href = "SysLogList.aspx?PID=35"; },
                                       lock: true
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