$(document).ready(function () {
    //保存按钮响应函数
    $("#btnNoticeSave").bind("click", NoticeSave);

    //重置按钮响应函数
    $("#btnNoticeReset").bind("click", NoticeReset);

    //绑定空列表
    BindNullList("gvNoticeList");

});

//保存按钮响应函数
function NoticeSave() {
    var t = $("#txtNoticeDetail").val();
    var strErrorMsg = "";
    var type = $("#NoticeID").val() == "" ? "NoticeAdd" : "NoticeEdit";
    if ($.trim($("#txtNoticeTitle").val()) == "") strErrorMsg += "<li>公告标题不能为空;</li>";
    if ($("#txtNoticeDetail").val() == "") strErrorMsg += "<li>公告内容不能为空</li>";

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
        content: "将要" + (type == "NoticeAdd" ? "增加" : "编辑") + "公告 [" + $.trim($("#txtNoticeTitle").val()) + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            doAjax("../",
           type,
           {
               "NoticeID": $("#NoticeID").val(),
               "NoticeCode": $("#spNoticeCode").html(),
               "RelaseName": $("#spRelaseName").html(),
               "NoticeTitle": $.trim($("#txtNoticeTitle").val()),
               "NoticeDetail": $("#txtNoticeDetail").val()
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
                       art.dialog.alert("此商家已经在系统中存在，请重新输入商家，然后重试！");
                       break;
                   default:
                       art.dialog
                            ({
                                time: 0.5,
                                content: '保存成功！',
                                close: function () { location.href = "../SystemManage/SysNoticeList.aspx?PID=44"; },
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

//重置响应事件
function NoticeReset() {
    var noticeid = $("#NoticeID").val();
    if ($("#NoticeaAddOrEdit").val() == 0) {
        $("#NoticeID").val("");
        $("#spNoticeCode").val("");
        $("#spRelaseName").val("");
        $("#txtNoticeTitle").val("");
        $("#txtNoticeDetail").val("");
    }
    else if ($("#NoticeaAddOrEdit").val() == 1) {
        NoticeEdit(noticeid);
    }
}

//公告新增
function NoticeAdd() {
    //    art.dialog({
    //        title: '系统公告新增',
    //        content: document.getElementById('NoticeInfo'),
    //        id: 'NoticeInfo',
    //        lock: true,
    //        close: function () { NoticeReset(); }
    //    });
    window.location.href = "../SystemManage/SysNotice.aspx?PID=43&NoticeID=0";
    $("#NoticeaAddOrEdit").val(0);
    $("#txtNoticeTitle").focus();
}

//公告编辑
function NoticeEdit(noticeid) {
    $("#NoticeaAddOrEdit").val(1);
    //    art.dialog({
    //        lock: true,
    //        title: '系统公告编辑',
    //        content: document.getElementById('NoticeInfo'),
    //        id: 'NoticeInfo',
    //        close: function () { $("#NoticeaAddOrEdit").val(0); NoticeReset(); }
    //    });
    //    doAjax("../",
    //             'GetNoticeInfo', { "NoticeID": noticeid }, "json",
    //                 function (json) {
    //                     $("#NoticeID").val(json.SysNoticeID);
    //                     $("#spNoticeCode").html(json.SysNoticeCode);
    //                     $("#spRelaseName").html(json.SysNotieceName);
    //                     $("#txtNoticeTitle").val(json.SysNoticeTitle);
    //                     CKEDITOR.instances.txtNoticeDetail.setData(json.SysNoticeDetail);
    //                     //                     $("#txtNoticeDetail").val(json.SysNoticeDetail);
    //                 });
    window.location.href = "../SystemManage/SysNotice.aspx?PID=43&NoticeID=" + noticeid;
}

function NoticeDel(noticeid) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除此系统公告吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
             'NoticeDel', { "NoticeID": noticeid }, "json",
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
                         case -3:
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
                                       time: 1,
                                       content: '删除成功！',
                                       close: function () { location.href = "SysNoticeList.aspx?PID=44"; }
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


