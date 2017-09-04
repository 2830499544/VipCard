$(function () {
    //BindNullList("gvwMemberExplanation");

    //新增公布
    $("#btnMemberExplanationAdd").bind("click", btnMemberExplanationAddClick);

    //保存
    $("#btnMemberExplanationSave").bind("click", btnMemberExplanationSaveClick);

})

//新增公布
function btnMemberExplanationAddClick() {
    $("#txtType,#txtMemberExplanationDesc").val("");
    artDialog({
        title: '新增公布',
        content: document.getElementById('MemberExplanationInfo'),
        id: 'MemberExplanationInfo',
        lock: true
    })
}

//保存
function btnMemberExplanationSaveClick() {
    var txtMemberExplanationDesc = $.trim($("#txtMemberExplanationDesc").val());
    var type = $("#txtType").val() == "" ? "AddMemberExplanation" : "EditMemberExplanation";
    var txtType = $("#txtType").val();

    if (txtMemberExplanationDesc == "") {
        art.dialog({
            title: '系统提醒',
            icon: 'error', //图标
            content: '发布内容不能为空，请填写发布内容！',
            lock: true
        });
        return false;
    }

    doAjax("../", type, {
        "MemberExplanationID": txtType,
        "MemberExplanationDesc": txtMemberExplanationDesc
    }, "text", function (text) {
        if (text == "0") {
            art.dialog({
                title: '系统提示',
                time: 4,
                content: ("系统异常，未保存数据，请再次点击保存！"),
                lock: true
            });
        } else {
            art.dialog({
                title: '系统提示',
                time: 0.5,
                content: '保存成功！',
                close: function () { window.location = window.location; },
                lock: true
            });
        }
    })
}

//删除公布
function btnMemberExplanationDel(MemberExplanationID) {
    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除选中的这条公布吗? 此操作不可恢复',
        ok: function () {
            this.lock();
            doAjax("../", "DelMemberExplanation", { "MemberExplanationID": MemberExplanationID }, "text", function (text) {
                if (text == "0") {
                    art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("系统错误 请与系统管理员联系！"),
                                  lock: true
                              });
                } else {
                    art.dialog
                                ({
                                    title: '系统提示',
                                    time: 0.5,
                                    content: '删除成功！',
                                    close: function () { window.location = window.location; },
                                    lock: true
                                });
                }
            })
            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    })
}

//编辑公布
function btnMemberExplanationEdit(MemberExplanationID, MemberExplanationDesc) {
    $("#txtType").val(MemberExplanationID); ;
    $("#txtMemberExplanationDesc").val(MemberExplanationDesc);
    artDialog({
        title: '编辑公布',
        content: document.getElementById('MemberExplanationInfo'),
        id: 'MemberExplanationInfo',
        lock: true
    })
}