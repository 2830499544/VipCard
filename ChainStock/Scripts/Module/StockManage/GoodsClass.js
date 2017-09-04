$(document).ready(function () {
    $("#txtShop").val($("#HDsltshop").val());
    var visible = $("#share").val();
    if (visible == "false") {
        $("a[id*=ClassSync]").css("display", "none");
    }
    else {
        $("a[id*=ClassSync]").css("display", "");
    }
})

//新增分类
function GoodsClassAdd(ParentID) {
    location.href = "EditGoodsLevel.aspx?PID=103&ParentID=" + ParentID;
}
//分类编辑
function GoodsClassEdit(ClassID) {
    location.href = "EditGoodsLevel.aspx?PID=103&ClassID=" + ClassID;
}
//分类删除
function GoodsClassDel(ClassName, ClassID, ShopID) {
    if (ClassID.toString() == "1") {
        art.dialog
            ({
                title: '系统提示',
                content: ("无法删除初始分类！"),
                lock: true
            });
        return false;
    }
    $("#txtClassName").focus();
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除该分类吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
             'GoodsClassDel', { "ClassID": ClassID, "ShopID": ShopID }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统异常 分类未能删除，请重试！"),
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
                         case -2:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("此分类存在子分类,不能删除！"),
                                       lock: true
                                   });
                             break;
                         case -1:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("此分类下还有商品,不能删除！"),
                                       lock: true
                                   });
                             break;
                         case -100:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("错误,没有查找到此分类！"),
                                       lock: true
                                   });
                             break;
                         default:
                             art.dialog
                                   ({
                                       time: 1,
                                       content: '删除成功！',
                                       close: function () { location.href = "SetGoodsLevel.aspx?PID=73&ShopID=" + $.trim($("#sltShop").val()); }
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

//分类同步到其他商家
function GoodsClassSync(ClassID,ShopID) {
    $("#txtClassName").focus();
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要同步分类到其他商家吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
             'GoodsClassSync', { "ClassID": ClassID, "ShopID": ShopID }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统异常 分类同步失败，请重试！"),
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
                                       content: '同步成功！'
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