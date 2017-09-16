$(document).ready(function () {
    $('#txtCustomRemindTime').bind("focus", function () {
        WdatePicker({ skin: 'ext', minDate: '%y-%M-%d', isShowClear: false, readOnly: true });
    });

    //保存按钮响应函数
    $("#btnCustomRemindSave").bind("click", btnCustomRemindSave);

    //重置按钮响应函数
    $("#btnCustomRemindReset").bind("click", btnCustomRemindReset);

    //绑定空列表
    BindNullList("gvCustomRemind");
})

function CustomRemind() {
    art.dialog({
        title: '新增自定义提醒',
        content: document.getElementById('DIVCustomRemind'),
        id: 'DIVCustomRemind',
        lock: true,
        close: function () { btnCustomRemindReset(); }
    });
}
var CustomRemindList = "";

function btnCustomRemindSave() {
    var strErrorMsg = "";
    var type = ($("#txtCustomRemindID").val() == "") ? "CustomRemindAdd" : "CustomRemindEdit";
    if ($("#txtCustomRemindTime").val() == "")
        strErrorMsg += "<li>请输入提醒日期,否则无法提醒!</li>";
    if ($.trim($("#txtCustomRemindTitle").val()) == "")
        strErrorMsg += "<li>为了更好的能够提醒,请输入您要提醒的标题!</li>";
    GetCustomReminder();
    if (CustomRemindList == "")
        strErrorMsg += "<li>请至少选择一位要提醒的对象</li>"

    if (strErrorMsg != "") {

        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            id: 'KDf435',
            title: '系统提示',
            content: strErrorMsg
        });
        return false;
    }
    art.dialog({
        title: '系统提示',
        content: "将要" + (type == "CustomRemindAdd" ? "增加" : "编辑") + "自定义提醒 [" + $.trim($("#txtCustomRemindTitle").val()) + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();

            doAjax("../",
             type,
               {
                   "CustomRemindID": $("#txtCustomRemindID").val(),
                   "CustomRemindTitle": $.trim($("#txtCustomRemindTitle").val()),
                   "CustomRemindDetail": $.trim($("#txtCustomRemindDetail").val()),
                   "CustomReminder": CustomRemindList,
                   "CustomRemindTime": $("#txtCustomRemindTime").val()
               },
                "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog.alert("系统异常，未保存数据，请再次点击保存！");
                             break;
                         case -3:
                             art.dialog.alert("系统错误 请与系统管理员联系！");
                             break;
                         case -1:
                             art.dialog.alert("此字段已经在系统中存在，请重新输入，然后重试！");
                             break;
                         default:
                             art.dialog
                                   ({
                                       time: 0.5,
                                       content: '自定义提醒保存成功！',
                                       close: function () { location.href = "SysCustomRemind.aspx?PID=51"; },
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
function btnCustomRemindReset() {
    $("#txtCustomRemindTitle").val("");
    $("#txtCustomRemindDetail").val("");
    $("#txtCustomRemindTime").val("");
    $("input[name^='cblCustomReminder']").each(function () {
        $(this).attr("checked", false);
    });
}

function btnCustomRemindDel(title, cid) {
    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除自定义提醒【' + title + '】吗? 此操作不可恢复',
        ok: function () {
            doAjax("../",
             'CustomRemindDel', { "CustomRemindID": cid }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog.alert("系统异常 记录未能删除，请重试！");
                             break;
                         case -3:
                             art.dialog.alert("系统错误 请与系统管理员联系！");
                             break;
                         default:
                             art.dialog
                                   ({
                                       time: 1,
                                       content: '删除成功！',
                                       close: function () { location.href = "SysCustomRemind.aspx?PID=51"; }
                                   });
                             break;
                     }
                 })

            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    });
    return false;
}
function GetCustomReminder() {
    var Reminder = "";
    var ReminderList = "";
    $("input[name^='cblCustomReminder']").each(function () {
        if ($(this).is(":checked")) {
            Reminder = $(this).parent("span").attr("alt");
            ReminderList = $(this).next().text();
            CustomRemindList += ReminderList + ",";
        }
    });
}
