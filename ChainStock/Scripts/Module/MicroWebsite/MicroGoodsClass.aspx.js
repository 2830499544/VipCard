$(document).ready(function () {
    //新增商品分类
    $("#btnGoodsClassAdd").bind("click", btnGoodsClassAdd);
    //保存
    $("#btnGoodsClassSave").bind("click", btnGoodsClassSave);
    //BindNullList("gvwGoodsClass");
})

function btnGoodsClassAdd() {
    $("#txtGoodsClassID,#txtGoodsClassName,#txtGoodsClassRemark,#txtClassShopID").val("");
    art.dialog({
        lock: true,
        title: '商品分类新增',
        width: '300',
        content: document.getElementById('dvGoodsClassInfo'),
        id: 'dvGoodsClassInfo',
        close: function () { $("#txtGoodsClassID,#txtGoodsClassName,#txtGoodsClassRemark,#txtClassShopID").val(""); }
    });
}
function btnGoodsClassEdit(classID, className, classRemark, shopID) {
    $("#txtGoodsClassID").val(classID);
    $("#txtGoodsClassName").val(className);
    $("#txtGoodsClassRemark").val(classRemark);
    $("#txtClassShopID").val(shopID);
    art.dialog({
        lock: true,
        title: '商品分类编辑',
        width: '300',
        content: document.getElementById('dvGoodsClassInfo'),
        id: 'dvGoodsClassInfo',
        close: function () { $("#txtGoodsClassID,#txtGoodsClassName,#txtGoodsClassRemark,#txtClassShopID").val(""); }
    });
}

function btnGoodsClassSave() {
    var strErrorMsg = "";
    var txtGoodsClassName = $.trim($("#txtGoodsClassName").val());
    var txtGoodsClassID = $("#txtGoodsClassID").val();
    var txtGoodsClassRemark = $.trim($("#txtGoodsClassRemark").val());
    var txtClassShopID = $("#txtClassShopID").val();
    var type = txtGoodsClassID == "" ? "AddMicroGoodsClass" : "EditMicroGoodsClass";
    
    if (txtGoodsClassName == "") { strErrorMsg = "商品类别不能为空,请输入商品类别！"; }
    if (txtGoodsClassName.length>4) {
        strErrorMsg = "商品分类名称长度不能大于4个汉字，请重新输入!";
    }

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
        content: "将" + (type == "AddMicroGoodsClass" ? "增加" : "编辑") + "商品分类 [" + txtGoodsClassName + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            doAjax(
                    "../",
                    "AddOrEditGoodsClass",
                    {
                        "GoodsClassName": txtGoodsClassName,
                        "ClassShopID": txtClassShopID,
                        "GoodsClassRemark": txtGoodsClassRemark,
                        "GoodsClassID": txtGoodsClassID
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
                                          close: function () { location.href = "MicroGoodsClass.aspx?PID=116"; },
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

function btnGoodsClassDel(classID, claaName) {
    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除商品分类【' + claaName + '】吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../", "DelGoodsClass", { "classID": classID }, "text", function (text) {
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
                               content: ("此分类在商品中存在记录,不能删除！"),
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