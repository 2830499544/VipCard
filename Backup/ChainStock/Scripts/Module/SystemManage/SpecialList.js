function DeleteSpecial(SpecialID, SpecialName) {
    art.dialog({
        title: '系统提示',
        content: '您确定要删除该优惠活动吗？此操作不可恢复，请慎重！ ',
        lock: true,
        ok: function () {
            this.close();
            doAjax("../",
                   "SpecialDelete",
                   { "SpecialID": SpecialID },
                   "json",
                   function (json) {
                       switch (json.result.toString()) {
                           case "0":
                               art.dialog({
                                   title: '系统提示',
                                   time: 4,
                                   content: ("系统错误 请与系统管理员联系！"),
                                   lock: true
                               });
                               break;
                           case "1":
                               art.dialog({
                                   title: '系统提示',
                                   time: 2,
                                   content: ("删除成功！"),
                                   close: function () { location.href = "SpecialList.aspx" }
                               });
                               break;
                           default:
                               strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + json.result.toString() + "</ul>";
                               art.dialog({
                                   title: '系统提示',
                                   icon: 'error', //图标
                                   content: strErrorMsg,
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

function AddSpecial() {
    //window.location.href = '../Member/AddSubMem.aspx?PID=164';
    window.location.href = '../SystemManage/MemSpecial.aspx';
}