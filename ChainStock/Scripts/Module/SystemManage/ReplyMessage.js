$(document).ready(function () {
    //回复按钮响应的事件
    $("#btReply").bind("click", Reply);
});
//弹出回复窗口
function ReplyMsg(content, messageID) {
    art.dialog({
//        height: 350,
//        width: 400,       
        width: 600,
        title: '会员留言回复',
        content: document.getElementById('dvMessageReply'),
        id: 'dvMessageReply',
        lock: true,
        init: function () {
            $('#content').focus().select();
        },
        close: function () { }
    });
    $("#MessageID").val(messageID);
    $("#spMessage").html(content);
}
//回复按钮响应事件
function Reply() {
    var messageID = $("#MessageID").val();
    var content = $("#content").val();
    var memID = $("#txtMemID").val();
    if (content == "") {
        art.dialog({
            title: '系统提示',
            icon: 'error',
            //图标
            content: '回复内容不能为空，请重新输入！',
            lock: true
        });
        return false;
    }
    doAjax("../",
           "MessageReply",
           { "messageID": messageID, "content": content },
           "json",
            function (json) {
                switch (json) {
                    case 0:
                        art.dialog
                          ({
                              title: '系统提示',
                              time: 2,
                              content: ("系统异常，未保存数据，请再次点击保存！"),
                              lock: true
                          });
                        break;
                    case -1:
                        art.dialog
                          ({
                              title: '系统提示',
                              time: 2,
                              content: ("系统错误 请与系统管理员联系！"),
                              lock: true
                          });
                        break;
                    default:
                        art.dialog
                            ({
                                time: 0.5,
                                content: '回复成功！',
                                close: function () { location.href = "ReplyMessage.aspx?PID=90&MemID=" + memID; },
                                lock: true
                            });
                        break;
                }
            }
        );
}

function DelMsg(messageID, memID) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除这条留言吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
             'MessageDel', { "messageID": messageID }, "json",
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
                                       close: function () { location.href = "ReplyMessage.aspx?PID=90&MemID=" + memID; }
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
 