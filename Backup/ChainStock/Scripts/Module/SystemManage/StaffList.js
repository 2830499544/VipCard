$(document).ready(function () {
    $("#txtQuery").focus();

    //绑定部门列表到树形菜单
    mkTree_convertTrees();
    expandTree('treeStaff');

    //获取员工列表
    GetStaffList();

    //按“回车键”响应查询
    $("#txtQuery").keypress(function (e) {
        var keyAscii = window.event ? e.keyCode : e.which;
        if (keyAscii == 13) {
            GetStaffList();
        }
    });

    //"查询"按钮响应函数
    $("#btnStaffQuery").bind("click", GetStaffList);

    //"新增员工"响应函数
    $("#btnStaffAdd").bind("click", StaffAdd);

    //"保存"按钮响应函数
    $("#btnStaffSave").bind("click", StaffSave);

    //"重置"按钮响应函数
    $("#btnStaffReset").bind("click", StaffReset);
});

var intShopID = "";
var intClassID = "";

function bindtree(id) {
    if ($(".selected" + id).css("display") == "none") {
        $("#img" + id).attr('src', '/images/ico/open.png');
    }
    else {
        $("#img" + id).attr('src', '/images/ico/close.png');
    }
    $(".selected" + id).toggle();
}
/*****************************************************************************************************
*新增按钮响应函数
*****************************************************************************************************/
function StaffAdd() {
    art.dialog({
        title: '新增员工',
        content: document.getElementById('divStaff'),
        id: 'divStaff',
        lock: true,
        close: function () {
            $("#txtStaffID").val("");
            $("#txtStaffNumber").val("");
            $("#txtStaffName").val("");
            $("#sltStaffSex").val("");
            $("#txtStaffMobile").val("");
            $("#sltStaffClass").val("");
            $("#txtStaffAddress").val("");
            $("#txtStaffRemark").val("");
        }
    });
    $("#txtStaffNumber").focus();
}

/*****************************************************************************************************
*保存按钮响应函数
*****************************************************************************************************/
function StaffSave() {
    var strErrorMsg = "";

    var strStaffID = $.trim($("#txtStaffID").val());
    var strStaffName = $.trim($("#txtStaffName").val());
    var strStaffNumber = $.trim($("#txtStaffNumber").val());
    var strStaffSex = $.trim($("#sltStaffSex").val());
    var strStaffMobile = $.trim($("#txtStaffMobile").val());
    var strStaffClass = $.trim($("#sltStaffClass").val());
    var strStaffAddress = $.trim($("#txtStaffAddress").val());
    var strStaffRemark = $.trim($("#txtStaffRemark").val());

    var type = (strStaffID == "") ? "StaffAdd" : "StaffEdit";
    if (strStaffNumber == "") strErrorMsg += "<li>员工编号不能为空;</li>";
    if (strStaffName == "") strErrorMsg += "<li>员工姓名不能为空;</li>";
    if (strStaffClass == "") strErrorMsg += "<li>员工所属部门不能为空;</li>";

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
        content: "将要" + (type == "StaffAdd" ? "增加" : "编辑") + "员工 [" + $.trim($("#txtStaffName").val()) + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
            type,
              {
                  "StaffID": strStaffID,
                  "StaffName": strStaffName,
                  "StaffNumber": strStaffNumber,
                  "StaffSex": strStaffSex,
                  "StaffMobile": strStaffMobile,
                  "StaffClass": strStaffClass,
                  "StaffAddress": strStaffAddress,
                  "StaffRemark": strStaffRemark
              },
             "json",
             function (json) {
                 switch (json) {
                     case -1:
                         art.dialog({
                             title: '系统提示',
                             content: ("该员工编号已经存在，请用另一个员工编号！"),
                             lock: true
                         });
                         break;
                     case 0:
                         art.dialog({
                             title: '系统提示',
                             content: ("系统异常，未保存数据，请再次点击保存！"),
                             lock: true
                         });
                         break;
                     default:
                         art.dialog({
                             title: '系统提示',
                             time: 0.5,
                             content: '保存成功！',
                             close: function () { location.href = "./StaffList.aspx?PID=71"; },
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
function StaffReset() {
    if ($("#txtStaffID").val() == "") {
        $("#txtStaffNumber").val("");
        $("#txtStaffName").val("");
        $("#sltStaffSex").val("");
        $("#txtStaffMobile").val("");
        $("#sltStaffClass").val("");
        $("#txtStaffAddress").val("");
        $("#txtStaffRemark").val("");
    }
    else {
        StaffEdit($("#txtStaffID").val());
    }
}

/*****************************************************************************************************
*获取员工列表
*****************************************************************************************************/
function GetStaffList() {
    doAjax(
           "../",
           "GetStaffList",
           {
               "txtQuery": $.trim($("#txtQuery").val()),
               "ShopID": intShopID,
               "ClassID": intClassID
           },
           "json",
           function (json) {
               CreateStaffTable(json);
               intShopID = "";
               intClassID = "";
           });
}

/*****************************************************************************************************
*获取员工列表
*****************************************************************************************************/
function CreateStaffTable(obj) {
    var html = '<tr class=\"th\">'
             + ' <th>员工编号</th><th>员工姓名</th><th>员工性别</th><th>员工手机号码</th><th>员工地址</th><th>所属部门</th><th>所属商家</th><th>备注</th><th>操作</th>'
             + '</tr>';
    if (obj != null) {
        $.each(obj, function (index, item) {
            html += "<tr class=\"td\">"
                        + '<td>' + item.StaffNumber + '</td>'
                        + '<td style="text-align: left">' + item.StaffName + '</td>'
                        + '<td>' + (item.StaffSex == "True" ? "男" : "女") + '</td>'
                        + '<td>' + item.StaffMobile + '</td>'
                        + '<td style="text-align: left">' + item.StaffAddress + '</td>'
                        + '<td align="left">' + item.ClassName + '</td>'
                        + '<td align="left">' + item.ShopName + '</td>'
                        + '<td align="left">' + item.StaffRemark + '</td>'
                        + '<td class="listtd" style="width: 60px;">' + "<a href=\"javascript:void(0);\" onclick=\"javascript:StaffEdit(" + item.StaffID + ");\"> <img src='../images/Gift/eit.png' alt='编辑' title='编辑' /></a>"
                        + "<a href=\"javascript:void(0);\" onclick=\"javascript:StaffDelete(" + item.StaffID + ",'" + item.StaffName + "');\"><img src='../images/Gift/del.png' alt='删除' title='删除' /></a>" + '</td>'
                        + '</tr>';
        });
    }
    else {
        html += '<tr><td style="height:25px; line-height:25px;padding-left:20px; background-color:#fff;" colspan="9">未找到符合此条件的数据！</td></tr>';
    }
    $("#StaffList").html(html);
}

/*****************************************************************************************************
*编辑员工
*****************************************************************************************************/
function StaffEdit(intStaffID) {
    art.dialog({
        title: '编辑员工',
        content: document.getElementById('divStaff'),
        id: 'divStaff',
        lock: true,
        close: function () {
            $("#txtStaffID").val("");
            $("#txtStaffNumber").val("");
            $("#txtStaffName").val("");
            $("#sltStaffSex").val("");
            $("#txtStaffMobile").val("");
            $("#sltStaffClass").val("");
            $("#txtStaffAddress").val("");
            $("#txtStaffRemark").val("");
        }
    });
    $("#txtStaffName").focus();

    doAjax(
           "../",
           "GetStaff",
           {
               "StaffID": intStaffID
           },
           "json",
           function (json) {
               if (json != null) {
                   $("#txtStaffID").val(intStaffID);
                   $("#txtStaffNumber").val(json[0].StaffNumber);
                   $("#txtStaffName").val(json[0].StaffName);
                   if (json[0].StaffSex == 'True') {
                       $("#sltStaffSex").val("0");
                   }
                   else {
                       $("#sltStaffSex").val("1");
                   }
                   $("#txtStaffMobile").val(json[0].StaffMobile);
                   $("#sltStaffClass").val(json[0].StaffClassID);
                   $("#txtStaffAddress").val(json[0].StaffAddress);
                   $("#txtStaffRemark").val(json[0].StaffRemark);
               }
           });
}

/*****************************************************************************************************
*删除员工
*****************************************************************************************************/
function StaffDelete(intStaffID, strStaffName) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除【' + strStaffName + '】吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax(
           "../",
           "StaffDelete",
           {
               "StaffID": intStaffID
           },
           "json",
          function (json) {
              switch (json) {
                  case 0:
                      art.dialog({
                          title: '系统提示',
                          content: ("系统异常，未保存数据，请再次点击保存！"),
                          lock: true
                      });
                      break;
                  case -1:
                      art.dialog({
                          title: '系统提示',
                          content: ("系统错误，请与系统管理员联系！"),
                          lock: true
                      });
                      break;
                  default:
                      art.dialog({
                          title: '系统提示',
                          time: 2,
                          content: '保存成功！',
                          close: function () { location.href = "./StaffList.aspx?PID=71"; }
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
/*****************************************************************************************************
*绑定部门列表到树形菜单
*****************************************************************************************************/
function StaffListTree(intID, bolIsShop) {
    $("#txtQuery").val("");
    if (intID != undefined) {
        if (bolIsShop) {
            intShopID = intID;
        }
        else {
            intClassID = intID;
        }
    }
    GetStaffList();
}