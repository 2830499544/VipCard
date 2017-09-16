$(document).ready(function () {
    $('#txtStaffTaskDate').bind("focus click", function () {
        WdatePicker({ skin: 'ext',dateFmt: 'yyyy-MM', maxDate: '2099-12', isShowClear: true, readOnly: true });
    });
    $('#txtStaffTaskTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM', maxDate: '2099-12', isShowClear: true, readOnly: true });
    });
    //绑定任务员工
    //GetStaffList();
    //新增任务
    $("#btnStaffTaskAdd").bind("click", StaffTaskAdd);
    //任务员工
    $("#sltStaffClass").bind("change", GetStaffList);
    //获取选中员工
    $(".chkAll").bind("click", GetStaffLists);
    //保存
    $("#btnStaffSave").bind("click", StaffTaskSave);
});
//编辑任务
function StaffTaskEdit(StaffTaskID) {
    $("#txtStaffTaskID").val(StaffTaskID);
    art.dialog({
        title: '编辑任务',
        content: document.getElementById('divStaffTask'),
        id: 'divStaffClass',
        lock: true,
        close: function () {
            TaskReset();
        }
    });

    doAjax("../",
           "GetStaffTask",
           { "StaffTaskID": StaffTaskID },
           "json",
           function (json) {
               if (json != null) {
                   $("#txtStaffTaskNumber").val(json[0].StaffTaskNumber);
                   $("#txtStaffTaskTitle").val(json[0].StaffTaskTitle);
                   $("#txtStaffTaskCount").val(json[0].StaffTaskCount);
                   $("#txtStaffTaskRemark").val(json[0].StaffTaskRemark);
                   $("#txtStaffTaskTime").val(json[0].StaffTaskTime.substring(0,7));
                   GetStaffList(json[0].StaffID);
                  // BindStaff(json[0].StaffID);                  
               }
           });
}
//绑定员工
function BindStaff(staffIDList) {
    $(".chk").each(function () { //遍历html中class为chk的复选框标签  
        var val = $(this).val();
        //alert(val);       
        var index = staffIDList.indexOf(val);
        //alert(index);
        if (staffIDList.indexOf(val)>=0) {
            $(this).attr("checked", "checked");
        }      
    })    
}

//添加保存
function StaffTaskSave() {
    GetStaffLists();
    var strErrorMsg = "";
    var strStaffTaskTitle = $.trim($("#txtStaffTaskTitle").val());
    var strStaffTaskNumber = $.trim($("#txtStaffTaskNumber").val());
    var strStaffTaskTime = $("#txtStaffTaskTime").val();
    var strStaffTaskCount = $.trim($("#txtStaffTaskCount").val());
    var strStaffCount = $("#lblStaffCount").html();
    var strStaffIDlist = $("#lblStaffIDlist").html();
    var strStaffTaskRemark = $("#txtStaffTaskRemark").val();
    var strStaffTaskID = $("#txtStaffTaskID").val();
    var strShopID = $("#ShopID").val();

    var type = (strStaffTaskID == "") ? "StaffTaskAdd" : "StaffTaskEdit";

    if (strStaffTaskNumber == "") {
        strErrorMsg += "<li>任务编号不能为空;</li>";
    }
    if (strStaffTaskTitle=="") {
        strErrorMsg += "<li>任务名称不能为空;</li>";
    }
    if (strStaffTaskTime=="") {
        strErrorMsg += "<li>任务日期不能为空;</li>";
    }
    if (strStaffTaskCount=="") {
        strErrorMsg += "<li>目标数量不能为空;</li>";
    } else {
        if (isNaN(strStaffTaskCount) || parseInt(strStaffTaskCount)<0) {
            strErrorMsg += "<li>目标数量必须是大于0的数字;</li>";
        }
    }
    if (parseInt(strStaffCount)<=0) {
        strErrorMsg += "<li>请至少选择一个员工;</li>";
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
        content: "将要" + (type == "StaffTaskAdd" ? "增加" : "编辑") + "任务 [" + $.trim($("#txtStaffTaskTitle").val()) + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
             type,
                {
                    "StaffTaskNumber":strStaffTaskNumber,
                    "StaffTaskID": strStaffTaskID,
                    "StaffTaskTitle": strStaffTaskTitle,
                    "StaffTaskCount": strStaffTaskCount,
                    "StaffCount": strStaffCount,
                    "StaffIDlist": strStaffIDlist,
                    "StaffTaskTime": strStaffTaskTime,
                    "ShopID":strShopID,
                    "StaffTaskRemark": strStaffTaskRemark
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
                                  content: ("任务编号已经存在，不能使用该任务编号"),
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
                                  close: function () { location.href = "/SystemManage/StaffTask.aspx?PID=179"; },
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
*新增按钮响应函数
*****************************************************************************************************/
function StaffTaskAdd() {
    GetStaffList();
    art.dialog({
        title: '新增任务',
        content: document.getElementById('divStaffTask'),
        id: 'divStaffTask',
        lock: true,
        close: function () {
            TaskReset();
        }
    });
    $("#txtStaffTaskTitle").focus();
}
/*************************************************************************************************************
*全选
*************************************************************************************************************/
function CheckAll() {
    var chk = typeof ($("#chkAll").attr("checked"));
    var ck = $("#chkAll").attr("checked");
    var i = 0;
    if ($(".chkAll").attr("checked") == true) {
        $(".chk").each(function () { //遍历html中class为chk的复选框标签        
            $(this).attr("checked", true);
        })
    }
    else {
        $(".chk").each(function () {
            $(this).attr("checked", false);
            //$(this).removeAttr("checked");
        })
    }
    //if (chk == "undefined") { //第一次       
    //    $("#chkAll").attr("checked", "checked");
    //    if ($(".chkAll").attr("checked") == "checked") {
    //        $(".chk").each(function () { //遍历html中class为chk的复选框标签        
    //            $(this).attr("checked", "checked");                
    //        })
    //    }
    //    else {
    //        $(".chk").each(function () {
    //            //$(this).attr("checked", 'false');
    //            $(this).removeAttr("checked"); 
    //        })
    //    }        
    //} else { //不是第一次全选
    //    if ($("#chkAll").attr("checked") == "checked") {
    //        $(".chk").each(function () { //遍历html中class为chk的复选框标签        
    //            //$(this).attr("checked", "checked");
    //            $(this).attr("checked", "checked");                
    //        })
    //    }
    //    else {
    //        $(".chk").each(function () {
    //            //$(this).attr("checked", 'false');                
    //            $(this).removeAttr("checked");
    //        })
    //    }
    //}
    GetStaffLists();
}
//获取任务员工列表
function GetStaffList(staffidlist) {    
    var type = typeof (staffidlist);
    var staffClassID = $("#sltStaffClass").val();
    var shopID = $("#ShopID").val();
    doAjax(
       "../",
       "GetStaffList",
       {
           "txtQuery": "",
           "ShopID": shopID,
           "ClassID": staffClassID
       },
       "json",
       function (json) {          
           var html = '';
           if (json.length > 0) {              
               $.each(json, function (index, item) {                   
                   if (type == "undefined" || type=="object") {
                       html += "<span style='width:56px;float:left;'><label for=\"" + item.StaffID + "\" >" + item.StaffName + "</label><input type=\"checkbox\" id=\"" + item.StaffID + "\"  name=\"" + item.StaffID + "\"  class=\"chk\" value=\"" + item.StaffID + "\" onclick=\"GetStaffLists()\"/> </span>";
                   } else if (type == "string" && staffidlist.indexOf(item.StaffID) >= 0) { //编辑
                       $("#sltStaffClass").attr("disabled", "disabled");
                       html += "<span style='width:56px;float:left;'><label for=\"" + item.StaffID + "\" >" + item.StaffName + "</label><input type=\"checkbox\" checked='checked' disabled='disabled' id=\"" + item.StaffID + "\"  name=\"" + item.StaffID + "\"  class=\"chk\" value=\"" + item.StaffID + "\" onclick=\"GetStaffLists()\"/> </span>";
                   }
               });
           }
           $("#tdStaffList").html(html);           
       });
    $("#lblStaffIDlist").text("");
    $("#lblStaffCount").text("");
}
//获取任务员工
function GetStaffLists() {
    //alert(1);
    var staffIDlist = "";
    var staffCount = 0;    
    $(".chk").each(function () {
        if ($(this).attr("checked") == true) {
            staffCount++;
            staffIDlist += $(this).val() + ",";
        }
    }); 
    $("#lblStaffIDlist").text(staffIDlist);
    $("#lblStaffCount").text(staffCount);
}
//删除
function StaffTaskDelete(StaffTaskID, StaffTaskTitle) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除【' + StaffTaskTitle + '】吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
                   "StaffTaskDelete",
                   { "StaffTaskID": StaffTaskID },
                   "json",
                   function (json) {
                       switch (json) {
                           case -1:
                               art.dialog({
                                   title: '系统提示',
                                   time: 4,
                                   content: ("任务不存在，请联系管理员"),
                                   lock: true
                               });
                               break;
                           case 0:
                               art.dialog({
                                   title: '系统提示',
                                   time: 4,
                                   content: ("系统异常，未删除任务，请再次点击删除！"),
                                   lock: true
                               });
                               break;
                           default:
                               art.dialog({
                                   title: '系统提示',
                                   time: 2,
                                   content: '删除成功！',
                                   close: function () { location.href = "/SystemManage/StaffTask.aspx?PID=179"; }
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

//重置
function TaskReset() {
    $("#txtStaffTaskNumber").val("");
    $("#txtStaffTaskTitle").val("");
    $("#txtStaffTaskCount").val("");
    $("#txtStaffTaskRemark").val("");
    $("#lblStaffIDlist").html("");
    $("#lblStaffCount").html("");
    $("#tdStaffList").html("");
    $("#sltStaffClass").removeAttr("disabled");
    $(".chkAll").attr("checked", false);
}