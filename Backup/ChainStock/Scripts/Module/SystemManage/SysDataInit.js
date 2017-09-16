$(document).ready(function () {
    //绑定日期控件
    $('#txtTimeStart').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });

    });

    //绑定日期控件
    $('#txtTimeEnd').bind("focus click", function () {
        WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
    });
    $("#btnAll").bind("click", btnAll);
    $("#btnRestore").bind("click", btnRestore);
})

//清除所有的数据
function btnAll() {
    art.dialog({
        content: '确定要清除系统全部的数据吗？\n\n此操作不可恢复，请慎重！',
        lock: true,
        ok: function () {
            this.close();
//            this.lock();
            doAjax("../",
               "DBInit",
               {
                   "type": "empty"
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
                                 time: 4,
                                 content: '系统初始化成功！',
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

function btnRestore() {
    art.dialog({
        content: '确定要还原系统测试数据吗？\n\n此操作不可恢复，请慎重！',
        lock: true,
        ok: function () {
            this.close();
//            this.lock();
            doAjax("../",
               "DBInit",
               {
                   "type": "Restore"
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
                                 content: '系统初始化成功！',
                                 close: function () { location.href = location.href; }
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
