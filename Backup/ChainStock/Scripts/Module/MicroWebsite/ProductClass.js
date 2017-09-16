$(document).ready(function () {
    //新增产品分类
    $("#btnClassAdd").bind("click", btnClassAdd);
    //保存
    $("#btnClassSave").bind("click", btnClassSave);
    //BindNullList("gvwClass");
})

function btnClassAdd() {
    $("#txtClassID,#txtClassName,#txtClassRemark,#txtClassShopID").val("");
    art.dialog({
        lock: true,
        title: '产品分类新增',
        width: '300',
        content: document.getElementById('dvClassInfo'),
        id: 'dvClassInfo',
        close: function () { $("#txtClassID,#txtClassName,#txtClassRemark,#txtClassShopID").val(""); }
    });
}
function btnClassEdit(classID, className, classRemark, shopID) {
    $("#txtClassID").val(classID);
    $("#txtClassName").val(className);
    $("#txtClassRemark").val(classRemark);
    $("#txtClassShopID").val(shopID);
    art.dialog({
        lock: true,
        title: '产品分类编辑',
        width: '300',
        content: document.getElementById('dvClassInfo'),
        id: 'dvClassInfo',
        close: function () { $("#txtClassID,#txtClassName,#txtClassRemark").val(""); }
    });
}

function btnClassSave() {
    var strErrorMsg = "";
    var txtClassName = $.trim($("#txtClassName").val());
    var txtClassID = $("#txtClassID").val();
    var txtClassRemark = $.trim($("#txtClassRemark").val());

    var type = txtClassID == "" ? "AddClass" : "EditClass";
    if (txtClassName == "") { strErrorMsg = "产品类别不能为空,请输入产品类别！"; }
    if (strErrorMsg != "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    art.dialog({
        content: "将" + (type == "AddClass" ? "增加" : "编辑") + "产品分类 [" + txtClassName + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            doAjax(
                    "../",
                    "AddOrEditProductClass",
                    {
                        "ClassName": txtClassName,
                        "ClassRemark": txtClassRemark,
                        "ClassID": txtClassID
                    },
                    "text",
                    function (text) {
                        switch (text) {
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
                                          content: '保存成功',
                                          close: function () { location.href = "ProductClass.aspx"; },
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

}

function btnClassDel(classID, claaName) {
    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除产品分类【' + claaName + '】吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../", "DelProductClass", { "classID": classID }, "text", function (text) {
                if (text == "0") {
                    art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("系统异常，未保存数据，请再次点击保存！"),
                              lock: true
                          });

                }
                else if (text == "-1") {
                    art.dialog
                           ({
                               title: '系统提示',
                               time: 4,
                               content: ("此分类在产品中存在记录,不能删除！"),
                               lock: true
                           });
                }
                else if (text == "-2") {
                    art.dialog
                            ({
                                title: '系统提示',
                                time: 4,
                                content: ("系统错误 请与系统管理员联系！"),
                                lock: true
                            });
                }
                else {
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
    });
    return false;
}