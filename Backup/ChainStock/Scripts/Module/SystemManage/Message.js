function btnMessageReply(memID) {
    window.location.href = 'ReplyMessage.aspx?PID=90&MemID=' + memID;

}
function btnMessageDel(memID, memName) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除会员【' + memName + '】的全部留言吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
             'MemMessageDel', { "memID": memID, "memName": memName }, "json",
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
                                       time: 0.5,
                                       content: '删除成功！',
                                       close: function () { location.href = "Message.aspx?PID=89"; },
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