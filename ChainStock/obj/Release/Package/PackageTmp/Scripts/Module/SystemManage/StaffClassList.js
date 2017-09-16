$(document).ready(function () {
    $("#txtShop").val($("#HDsltshop").val());

    //默认不提成
    $("#radTypeNo").attr("checked", true);

    //新增部门
    $("#btnClassAdd").bind("click", StaffClassAdd);

    //"保存"按钮响应函数
    $("#btnClssSave").bind("click", StaffClassSave);

    //"重置"按钮响应函数
    $("#btnClassReset").bind("click", StaffClassReset);

    //是否提成
    $("#radTypeNo").bind("click", RadType);
    $("#radTypeYes").bind("click", RadType);

    if ($("#radTypeNo").attr("checked")) {
        $('#txtClassPercent').val("");
        $('#txtClassPercent').css("background-color", "#eee");
        $('#txtClassPercent').attr("readonly", true);
    }
    else {
        $('#txtClassPercent').css("background-color", "");
        $('#txtClassPercent').attr("readonly", false);
    }
});

/*****************************************************************************************************
*提成选择框响应函数
*****************************************************************************************************/
function RadType() {
    if ($("#radTypeNo").attr("checked")) {
        $('#txtClassPercent').val("");
        $('#txtClassPercent').css("background-color", "#eee");
        $('#txtClassPercent').attr("readonly", true);
    }
    else {
        $('#txtClassPercent').css("background-color", "");
        $('#txtClassPercent').attr("readonly", false);
    }
}

/*****************************************************************************************************
*新增按钮响应函数
*****************************************************************************************************/
function StaffClassAdd() {
    art.dialog({
        title: '新增部门',
        content: document.getElementById('divStaffClass'),
        id: 'divStaffClass',
        lock: true,
        close: function () {
            $("#txtClassName").val("");
            $("#sltClassShopID").val("");
            $("#radTypeYes").attr("checked", true);
            $("#txtClassPercent").val("");
            $("#txtClassRemark").val("");
        }
    });
    $("#txtClassName").focus();
}

/*****************************************************************************************************
*保存按钮响应函数
*****************************************************************************************************/
function StaffClassSave() {
    var strErrorMsg = "";

    var strClassID = $.trim($("#txtClassID").val());
    var strClassName = $.trim($("#txtClassName").val());
    var strClassShopID = $.trim($("#sltClassShopID").val());
    var strClassType = $("input[name='radType']:checked").val()
    var strClassPercent = $.trim($("#txtClassPercent").val());
    var strClassRemark = $.trim($("#txtClassRemark").val());

    var type = (strClassID == "") ? "StaffClassAdd" : "StaffClassEdit";
    if (strClassName == "") {
        strErrorMsg += "<li>部门名称不能为空;</li>";
    }
    if ($("#radTypeYes").attr("checked") && strClassPercent == "") {
        strErrorMsg += "<li>请选择不提成，或者输入提成比率;</li>";
    }
    if (strClassPercent != "") {
        if (!strClassPercent.IsDecimal()) {
            strErrorMsg += "<li>请输入正确的数字，表示“提成比例”;</li>";
        }
        else if (strClassPercent <= 0 || strClassPercent >= 1) {
            strErrorMsg += "<li>提成比例必须是0～1之间的小数;</li>";
        }
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
        content: "将要" + (type == "StaffClassAdd" ? "增加" : "编辑") + "部门 [" + $.trim($("#txtClassName").val()) + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
             type,
                {
                    "ClassID": strClassID,
                    "ClassName": strClassName,
                    "ClassShopID": strClassShopID,
                    "ClassType": strClassType,
                    "ClassPercent": strClassPercent,
                    "ClassRemark": strClassRemark
                },
                 "json",
                  function (json) {
                      switch (json) {
                          case 0:
                              art.dialog({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("系统异常，未保存数据，请再次点击保存！"),
                                  lock: true
                              });
                              break;
                          case -1:
                              art.dialog({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("部门名称已经存在，不能使用该部门名称！"),
                                  lock: true
                              });
                              break;
                          case -2:
                              art.dialog({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("系统错误，请与系统管理员联系！"),
                                  lock: true
                              });
                              break;
                          default:
                              art.dialog({
                                  title: '系统提示',
                                  time: 0.5,
                                  content: '保存成功！',
                                  close: function () { location.href = "./StaffClassList.aspx?PID=75"; },
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

/*****************************************************************************************************
*重置按钮响应函数
*****************************************************************************************************/
function StaffClassReset() {
    if ($("#txtClassID").val() == "") {
        $("#txtClassName").val("");
        $("#sltClassShopID").val("");

        $("#radTypeYes").attr("checked", true);
        $('#txtClassPercent').attr("readonly", false);
        $("#txtClassPercent").val("");
        $('#txtClassPercent').css("background-color", "");
        $("#txtClassRemark").val("");

    }
    else {
        StaffClassEdit($("#txtClassID").val());
    }
}

/*******************************************************************************
*编辑按钮响应函数
*******************************************************************************/
function StaffClassEdit(classID) {
    $("#txtClassID").val(classID);

    art.dialog({
        title: '编辑员工部门',
        content: document.getElementById('divStaffClass'),
        id: 'divStaffClass',
        lock: true,
        close: function () {
            $("#txtClassID").val("");
            $("#txtClassName").val("");
            $("#sltClassShopID").val("");
            $("#radTypeYes").attr("checked", true);
            $("#txtClassPercent").val("");
            $("#txtClassRemark").val("");
        }
    });

    doAjax("../",
           "GetStaffClass",
           { "ClassID": classID },
           "json",
           function (json) {
               if (json != null) {
                   $("#txtClassName").val(json[0].ClassName);
                   $("#sltClassShopID").val(json[0].ClassShopID);
                   if (json[0].ClassType == "True") {
                       $("#radTypeYes").attr("checked", true);
                       $("#txtClassPercent").val(json[0].ClassPercent);
                       $('#txtClassPercent').css("background-color", "");
                       $('#txtClassPercent').attr("readonly", false);
                   }
                   else {
                       $("#radTypeNo").attr("checked", true);
                       $("#txtClassPercent").val("");
                       $('#txtClassPercent').css("background-color", "#eee");
                       $('#txtClassPercent').attr("readonly", true);
                   }
                   $("#txtClassRemark").val(json[0].ClassRemark);
               }
           });
}

/*******************************************************************************
*删除按钮响应函数
*******************************************************************************/
function StaffClassDelete(classID, className) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除【' + className + '】吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
                   "StaffClassDelete",
                   { "ClassID": classID },
                   "json",
                   function (json) {
                       switch (json) {
                           case -1:
                               art.dialog({
                                   title: '系统提示',
                                   time: 4,
                                   content: ("该部门已经存在员工，不能删除。请将该部门员工移到其它部门再删除！"),
                                   lock: true
                               });
                               break;
                           case 0:
                               art.dialog({
                                   title: '系统提示',
                                   time: 4,
                                   content: ("系统异常，未删除部门，请再次点击删除！"),
                                   lock: true
                               });
                               break;
                           default:
                               art.dialog({
                                   title: '系统提示',
                                   time: 2,
                                   content: '删除成功！',
                                   close: function () { location.href = "./StaffClassList.aspx?PID=75"; }
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

