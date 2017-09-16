$(document).ready(function () {
    //查询员工
    $("#btnFindStaff").bind("click", FindStaff);
    //重置
    $("#btnAttendenceReset").bind("click", AttendenceReset);
    //保存
    $("#btnAttendenceSave").bind("click", AttendenceSave);
});
//保存
function AttendenceSave() {
    var strErrorMsg = "";
    var staffInfo = $.trim($("#txtStaffInfo").val());
    var StaffID = $("#hidStaffID").val();
    var StaffShopID = $("#hidStaffShopID").val();
    var StaffClassID = $("#hidStaffClassID").val();
    var ReadDate = $("#lblReadDate").html();
    var CardNumber = $("#lblCardNumber").html();
    var Remark = $("#txtRemark").val();
    if (staffInfo == "") {
        strErrorMsg += "<li>请选择一个员工！</li>";
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
    doAjax("../",
              "AttendenceSave",
               {
                   "StaffID": StaffID,
                   "StaffShopID": StaffShopID,
                   "StaffClassID": StaffClassID,
                   "ReadDate": ReadDate,
                   "CardNumber": CardNumber,
                   "Remark": Remark
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
                                 close: function () { location.href = "/SystemManage/Attendence.aspx?PID=189"; },
                                 lock: true
                             });
                             break;
                     }
                 });
}
//查询员工
function FindStaff() {
    var strErrorMsg = "";
    var staffInfo = $.trim($("#txtStaffInfo").val());
    var ShopID = $("#hidShopID").val();
    if (staffInfo == "") {
        strErrorMsg += "<li>请输入员工信息！</li>";
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
    doAjax("../",
              "GetStafInfo",
               {
                   "txtQuery": staffInfo,
                   "ShopID": ShopID,
                   "ClassID": ""
               },
                "json",
                 function (json) {
                     if (json.length > 0) {
                         $("#lblReadDate").html(json[0].Time);
                         $("#lblCardNumber").html(json[0].StaffNumber);
                         $("#hidStaffID").val(json[0].StaffID);
                         $("#lblStaffClassName").html(json[0].ClassName);
                         $("#hidStaffClassID").val(json[0].StaffClassID);
                         $("#lblStaffShopName").html(json[0].ShopName);
                         $("#hidStaffShopID").val(json[0].ClassShopID);
                     } else {
                         alert("未找到指定员工！");
                     }
                 });
}

//重置
function AttendenceReset() {
    $("#txtStaffInfo").val("");
    $("#lblReadDate").html("");
    $("#lblCardNumber").html("");
    $("#hidStaffID").val("");
    $("#lblStaffClassName").html("");
    $("#hidStaffClassID").val("");
    $("#lblStaffShopName").html("");
    $("#hidStaffShopID").val("");
    $("#txtRemark").val("");
}