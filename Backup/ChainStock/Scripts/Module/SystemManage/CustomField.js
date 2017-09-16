$(document).ready(function () {
    //数据类型响应函数
    $("input[@name='radCustomFieldType']:radio").bind("change", radCustomFieldType);

    //保存响应函数
    $("#btnCustomSave").bind("click", btnCustomSave);

    //重置响应函数
    $("#btnCustomReset").bind("click", btnCustomReset);

    //新增自定义按钮响应函数
    $("#btnCustomFieldAdd").bind("click", btnCustomFieldAdd);

    //绑定空列表
    BindNullList("gvCustomFieldList");

});

//数据类型响应函数
function radCustomFieldType() {
    if ($('input[name=radCustomFieldType]:checked').val() == "select") {
        $("#trCustomSelectData").show();
    }
    else {
        $("#trCustomSelectData").hide();
    }
}
//保存响应函数
function btnCustomSave() {
    //    alert(document.getElementById("#txtCustomName"));
    var type = $("#txtCustomID").val() == "" ? "CustomFieldAdd" : "CustomFieldEdit";
    var strErrorMsg = "";
    if ($.trim($("#txtCustomName").val()) == "") {
        strErrorMsg += "<li>属性名称不能为空;</li>"
    }
    var CustomCode = $.trim($("#txtCustomCode").val());
    if (type == "CustomFieldAdd") {
        var CustomCode = "C_" + $.trim($("#txtCustomCode").val());
        if (!CustomCode.IsEnglish()) {
            strErrorMsg += "<li>属性代码输入错误，属性代码为此属性对应的英文名（或拼音）;</li>"
        }
    }

    if ($('input[name=radCustomFieldType]:checked').val() == "select" && $("#txtCustomSelectData").val() == "") {
        strErrorMsg += "<li>如果数据类型为“选择项”,请在”属性值”中填写选择项内容,例如:“身份证|驾驶证|学生证”,多个选项以“|”分隔;</li>";
    }
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        //var throughBox = art.dialog.through;
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
        content: "将要" + (type == "CustomFieldAdd" ? "增加" : "编辑") + "自定义字段 [" + $.trim($("#txtCustomName").val()) + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
//            this.lock();
            doAjax("../",
         type,
        {
            "CustomFieldID": $.trim($("#txtCustomID").val()),
            "CustomType": $('input[name=radCustomType]:checked').val(),
            "CustomName": $.trim($("#txtCustomName").val()),
            "CustomCode": CustomCode,
            "CustomFieldType": $('input[name=radCustomFieldType]:checked').val(),
            "CustomInfo": $.trim($("#txtCustomSelectData").val()),
            "isNull": $("#chkIsNull").attr("checked"),
            "isShow": $("#chkIsShow").attr("checked")
        },
        "json",
         function (json) {
             switch (json) {
                 case 0:
                     art.dialog
                          ({
                              title: '系统提示',
                              content: ("系统异常，未保存数据，请再次点击保存！"),
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
                 case -1:
                     art.dialog
                          ({
                              title: '系统提示',
                              content: ("此字段/字段名称已经在系统中存在，请重新输入，然后重试！"),
                              lock: true
                          });
                     break;
                 default:
                     art.dialog
                            ({
                                time: 0.5,
                                content: '自定义字段保存成功！',
                                close: function () { location.href = "CustomField.aspx?PID=34"; },
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
//重置响应函数
function btnCustomReset() {
    if ($("#CustomEditOrAdd").val() == 0) {
        $("#txtCustomID").val("");
        $("#txtCustomName").val("");
        $("#txtCustomCode").val("");
        $("#radText").attr("checked", true);
        $("#MemCustom").attr("checked", true);
        $("#chkIsNull").attr("checked", true);
        $("#chkIsShow").attr("checked", false);
        $("#trCustomSelectData").hide();
        $("#txtCustomSelectData").val("");
        $('input[name=radCustomType]:radio').attr("disabled", "");
        $("#txtCustomCode").attr("disabled", "");
    }
    else if ($("#CustomEditOrAdd").val() == 1) {
        btnCustomFieldEdit($("#txtCustomName").val(), $("#txtCustomID").val());
    }
}

function btnCustomFieldAdd() {
    $("#chkIsNull").attr("checked", true);
    //$("#chkIsShow").attr("checked", true);
    $("#radText").attr("checked", true);
    $("#MemCustom").attr("checked", true);
    $('input[name=radCustomType]:radio').attr("disabled", "");
    $("#txtCustomCode").attr("disabled", "");
    art.dialog({
        title: '自定义属性新增',
        content: document.getElementById('CustomFieldAddOrEdit'),
        id: 'CustomFieldAddOrEdit',
        lock: true,
        close: function () { btnCustomReset(); }
    });
    $("#CustomEditOrAdd").val(0);
    $("#txtCustomName").focus();
}

function btnCustomFieldEdit(CustomFieldName, CustomFieldID) {
    //    $("#chkIsNull").attr("checked", false);
    //    $("#chkIsShow").attr("checked", false);
    $("#CustomEditOrAdd").val(1);
    art.dialog({
        lock: true,
        title: '自定义属性编辑',
        content: document.getElementById('CustomFieldAddOrEdit'),
        id: 'CustomFieldAddOrEdit',
        close: function () { $("#CustomEditOrAdd").val(0); btnCustomReset(); }
    });
    doAjax("../",
             'GetCustomField', { "CustomFieldID": CustomFieldID }, "json",
                 function (json) {
                     $('input[name=radCustomType]:radio').each(function () {
                         if ($(this).val() == json.CustomType) { $(this).attr("checked", true); }
                     });
                     $('input[name=radCustomFieldType]:radio').each(function () {
                         if ($(this).val() == json.CustomFieldType) { $(this).attr("checked", true); }
                     });
                     if (json.CustomFieldType == "select") {
                         $("#trCustomSelectData").show(); $("#txtCustomSelectData").val(json.CustomFieldInfo);
                     } else { $("#trCustomSelectData").hide(); }
                     $("#txtCustomID").val(json.CustomFieldID);
                     $("#txtCustomName").val(json.CustomFieldName);
                     $("#txtCustomCode").val(json.CustomField);
                     $("#txtCustomSelectData").val(json.CustomFieldInfo);
                     if (json.CustomFieldIsNull == "True") {
                         $("#chkIsNull").attr("checked", true);
                     }
                     else {
                         $("#chkIsNull").attr("checked", false);
                     }
                     if (json.CustomFieldIsShow == "True") {
                         $("#chkIsShow").attr("checked", true);
                     }
                     else {
                         $("#chkIsShow").attr("checked", false);
                     }
                     //                     $("#chkIsNull").attr("checked", json.CustomFieldIsNull);
                     //$("#chkIsShow").attr("checked", json.CustomFieldIsShow);
                     $('input[name=radCustomType]:radio').attr("disabled", "disabled");
                     $("#txtCustomCode").attr("disabled", "disabled");
                     $(document).scrollTop(0);
                 })
}

function btnCustomFieldDel(CustomFieldName, CustomFieldID) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除自定义属性【' + CustomFieldName + '】吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
             'CustomFieldDel', { "CustomFieldID": CustomFieldID }, "json",
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
                         case -2:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("此属性已经在系统中存在记录,不能删除！"),
                                       lock: true
                                   });
                             break;
                         default:
                             art.dialog
                                   ({
                                       time: 1,
                                       content: '删除成功！',
                                       close: function () { location.href = "CustomField.aspx?PID=34"; }
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
 