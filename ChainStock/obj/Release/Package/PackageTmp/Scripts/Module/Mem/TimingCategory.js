$(document).ready(function () {
    $("#btnTimingCategory").bind("click", TimingCategory);
});

function SaveForm() {
    var CategoryName = $("#txtCategoryName").val();
    var CategoryFatherID = $("#sltCategoryFather").val();
    var CategoryrRemark = $("#txtCategoryrRemark").val();
    var CategoryID = $("#txtCategoryID").val();
    var MethodName = CategoryID == "" ? "CategoryAdd" : "CategoryEdit";
    var strErrorMsg = "";
    if (CategoryName == "") {
        strErrorMsg += "<li>类别名称不能为空！</li>";
    }
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
        content: '将' + (CategoryID == "" ? "添加" : "编辑") + '类别[' + CategoryName + ']。\n确定操作吗？',
        lock: true,
        ok: function () {
            this.close();
            doAjax("../",
                    MethodName,
                   {
                       "CategoryName": CategoryName,
                       "CategoryFatherID": CategoryFatherID,
                       "CategoryrRemark": CategoryrRemark,
                       "CategoryID": CategoryID
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
                              default:
                                  art.dialog
                             ({
                                 title: '系统提示',
                                 time: 0.5,
                                 content: '保存成功！',
                                 close: function () { location.href = 'TimingCategory.aspx?PID=112'; },
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

function TimingCategory() {
    ResetForm();
    art.dialog({
        lock: true,
        title: '新增分类',
        content: $("#TimingCategoryEdit").html(),
        id: 'TimingCategoryEdit',
        close: function () { ResetForm(); }
    });
}
function EditCategory(CategoryID) {
    $('#sltCategoryFather').attr("disabled", "disabled");
    $('#sltCategoryFather').css("background-color", "#eee");
    doAjax("../",
                     'GetCategory', { "CategoryID": CategoryID }, "json",
                         function (json) {
                             $("#sltCategoryFather").val(json.CategoryFatherID);
                             $("#txtCategoryName").val(json.CategoryName);
                             $("#txtCategoryrRemark").val(json.CategoryrRemark);
                             $("#txtCategoryID").val(json.CategoryID);
                         }
                       )
    art.dialog({
        lock: true,
        title: '编辑分类',
        content: $("#TimingCategoryEdit").html(),
        id: 'TimingCategoryEdit',
        close: function () { ResetForm(); }
    });
}

function DeleteCategory(CategoryID, CategoryName) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除规则【' + CategoryName + '】吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
               'DelCategory', { "CategoryID": CategoryID }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统错误 请与系统管理员联系！"),
                                       lock: true
                                   });
                             break;
                         case -1:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("该分类已有使用记录无法删除！"),
                                       lock: true
                                   });
                             break;
                         default:
                             art.dialog
                                   ({
                                       time: 2,
                                       content: '删除成功！',
                                       close: function () { location.href = 'TimingCategory.aspx?PID=112'; }
                                   });
                             break;
                     }
                 });

            return false;
        },
        cancelVal: '取消',
        cancel: true
    });
    return false;
}

function ResetForm() {
    $('#sltCategoryFather').attr("disabled", ""); ;
    $('#sltCategoryFather').css("background-color", "#ffffff");
    $("#txtCategoryName,#txtCategoryrRemark,#txtCategoryID,#sltCategoryFather").val("", "", "", "0");
}