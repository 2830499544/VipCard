$(document).ready(function () {
    $('#txtSalaryDate').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM', maxDate: '2099-12', isShowClear: true, readOnly: true });
    });
    $('#txtStaffSalaryDate').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM', maxDate: '2099-12', isShowClear: true, readOnly: true });
    });
    //绑定员工
    GetStaffList();
    //新增工资
    $("#btnStaffSalaryAdd").bind("click", StaffSalaryAdd);
    //员工部门改变
    $("#sltStaffClass").bind("change", GetStaffList);
    //员工提成
    $("#sltStaff").bind("change", GetStaffCommission);
    //保存
    $("#btnStaffSalarySave").bind("click", StaffSalarySave);
});
//保存响应
function StaffSalarySave() {
    var strErrorMsg = "";
    var strShopID = $("#ShopID").val();
    var strStaffClassID = $.trim($("#sltStaffClass").val());
    var strStaffID = $("#sltStaff option:selected").val();
    var strStaffName = $("#sltStaff option:selected").html();
    var strSalaryDate = $("#txtSalaryDate").val();
    var strSalaryCommission = $.trim($("#txtSalaryCommission").val());
    var strSalaryBase = $.trim($("#txtSalaryBase").val());
    var strSalarySubsidy = $.trim($("#txtSalarySubsidy").val());
    var strSalaryReward = $.trim($("#txtSalaryReward").val());
    var strSalaryPunish = $.trim($("#txtSalaryPunish").val());
    var strSalaryRemark = $("#txtSalaryRemark").val();
    var strStaffSalaryID = $("#txtStaffSalaryID").val();
    var type = (strStaffSalaryID == "") ? "StaffSalaryAdd" : "StaffSalaryEdit";

    if (strSalaryDate == "") {
        strErrorMsg += "<li>工资日期不能为空;</li>";
    }
    if (strSalaryBase == "") {
        strErrorMsg += "<li>基本工资不能为空;</li>";
    } else {
        if (isNaN(strSalaryBase) || parseFloat(strSalaryBase) < 0) {
            strErrorMsg += "<li>基本工资必须是大于0的数字;</li>";
        }
    }
    if (isNaN(strSalaryCommission) || parseFloat(strSalaryCommission) < 0) {
        strErrorMsg += "<li>提成必须是大于0的数字;</li>";
    }
    if (isNaN(strSalarySubsidy) || parseFloat(strSalarySubsidy) < 0) {
        strErrorMsg += "<li>补助必须是大于0的数字;</li>";
    }
    if (isNaN(strSalaryReward) || parseFloat(strSalaryReward) < 0) {
        strErrorMsg += "<li>奖励必须是大于0的数字;</li>";
    }
    if (isNaN(strSalaryPunish) || parseFloat(strSalaryPunish) < 0) {
        strErrorMsg += "<li>惩罚必须是大于0的数字;</li>";
    }
    var str = typeof (strStaffID);
    if (typeof (strStaffID) == 'undefined') {
        strErrorMsg += "<li>请至少选择一个员工;</li>";
    }
    if (strStaffID == "0") {
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
        content: "将要为员工[" + strStaffName + "]" + (type == "StaffSalaryAdd" ? "增加" : "编辑") + "工资记录。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
             type,
                {
                    "StaffClassID": strStaffClassID,
                    "StaffID": strStaffID,
                    "SalaryDate": strSalaryDate,
                    "SalaryCommission": strSalaryCommission,
                    "SalaryBase": strSalaryBase,
                    "SalarySubsidy": strSalarySubsidy,
                    "SalaryReward": strSalaryReward,
                    "SalaryPunish": strSalaryPunish,
                    "ShopID": strShopID,
                    "SalaryRemark": strSalaryRemark,
                    "StaffSalaryID": strStaffSalaryID
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
                                  content: ("员工不存在，不能完成添加"),
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
                                  close: function () { location.href = "/SystemManage/StaffSalary.aspx?PID=184"; },
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
//员工提成
function GetStaffCommission(staffid) {
    $("#txtSalaryCommission").val("0");
    var staffID = $("#sltStaff").val();
    if (typeof (staffid) != "undefined") {
        staffID = staffid;
    }
    var shopID = $("#ShopID").val();
    var date = $("#txtSalaryDate").val();
    if (staffID != 0) {
        doAjax(
         "../",
         "GetStaffCommission",
         {
             "ShopID": shopID,
             "StaffID": staffID,
             "date": date
         },
         "text",
         function (data) {
             $("#txtSalaryCommission").val(parseFloat(data).toFixed(2));
         });
    }
}
//获取员工列表
function GetStaffList(staffid) {
    var staffClassID = $("#sltStaffClass").val();
    var shopID = $("#ShopID").val();
    var type = typeof (staffid);
    if (staffClassID == "") {
        staffClassID = 0;
    }
    if (type == "object") {
        SalaryReset(); //添加重置
    }
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
           if (typeof (staffid) != "object") { //编辑
               html += '   <select id="sltStaff" disabled="disabled"  onchange="GetStaffCommission()" runat="server" class="selectWidth" style="width: 160px">'
           } else { //添加
               html += '   <select id="sltStaff" onchange="GetStaffCommission()" runat="server" class="selectWidth" style="width: 160px">'
           }
           html += '<option value="0">=====请选择=====</option>';
           if (json.length > 0) {
               $.each(json, function (index, item) {
                   if (typeof (staffid) != "object") { //编辑
                       if (item.StaffID == staffid) {
                           html += '<option value="' + item.StaffID + '" selected=true>' + item.StaffName + '</option>'
                       } else {
                           html += '<option value="' + item.StaffID + '">' + item.StaffName + '</option>';
                       }
                   } else {
                       html += '<option value="' + item.StaffID + '">' + item.StaffName + '</option>';
                   }
               });
           }
           html += '</select>';
           $("#tdStaffList").html(html);
       });
    GetStaffCommission(staffid);

}

/*****************************************************************************************************
    *新增按钮响应函数
    *****************************************************************************************************/
function StaffSalaryAdd() {
    SalaryReset();
    art.dialog({
        title: '新增任务',
        content: document.getElementById('divStaffSalary'),
        id: 'divStaffSalary',
        lock: true,
        close: function () {
            SalaryReset();
            $("#sltStaffClass").val("");
        }
    });
    $("#txtSalaryBase").focus();
}
//重置
function SalaryReset() {
    $("#txtSalaryCommission").val("0");
    $("#txtSalaryBase").val("0");
    $("#txtSalarySubsidy").val("0");
    $("#txtSalaryReward").val("0");
    $("#txtSalaryPunish").val("0")
    $("#txtSalaryRemark").val("");
    var html = '';
    html += '   <select id="sltStaff" onchange="GetStaffCommission()" runat="server" class="selectWidth" style="width: 160px">'
    html += '<option value="0">=====请选择=====</option>';
    $("#tdStaffList").html(html);
    $("#sltStaffClass").removeAttr("disabled");    
}

//编辑员工工资
function StaffSalaryEdit(SalaryID) {
    $("#txtStaffSalaryID").val(SalaryID);
    art.dialog({
        title: '编辑任务',
        content: document.getElementById('divStaffSalary'),
        id: 'divStaffSalary',
        lock: true,
        close: function () {
            SalaryReset();
        }
    });
    doAjax("../",
           "GetStaffSalary",
           { "SalaryID": SalaryID },
           "json",
           function (json) {
               if (json != null) {
                   $("#txtSalaryCommission").val(json[0].SalaryCommission);
                   $("#txtSalaryBase").val(json[0].SalaryBase);
                   $("#txtSalarySubsidy").val(json[0].SalarySubsidy);
                   $("#txtSalaryReward").val(json[0].SalaryReward);
                   $("#txtSalaryPunish").val(json[0].SalaryPunish);
                   $("#txtSalaryRemark").val(json[0].SalaryRemark);
                   $("txtSalaryDate").val(json[0].SalaryDate);
                   $("#sltStaffClass").find("option[value='" + json[0].StaffClassID + "']").attr("selected", true);
                   $("#sltStaffClass").attr("disabled", "disabled");
                   GetStaffList(json[0].StaffID);
                   //$("#sltStaff").find("option[value='" + json[0].StaffID + "']").attr("selected",true);
               }
           });
}
//删除
function StaffSalaryDelete(SalaryID, StaffName) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除[' + StaffName + ']的工资记录吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
                   "StaffSalaryDelete",
                   { "SalaryID": SalaryID },
                   "json",
                   function (json) {
                       switch (json) {
                           case -1:
                               art.dialog({
                                   title: '系统提示',
                                   time: 4,
                                   content: ("工资记录不存在，请联系管理员"),
                                   lock: true
                               });
                               break;
                           case 0:
                               art.dialog({
                                   title: '系统提示',
                                   time: 4,
                                   content: ("系统异常，未删除工资记录，请再次点击删除！"),
                                   lock: true
                               });
                               break;
                           default:
                               art.dialog({
                                   title: '系统提示',
                                   time: 2,
                                   content: '删除成功！',
                                   close: function () { location.href = "/SystemManage/StaffSalary.aspx?PID=179"; }
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
