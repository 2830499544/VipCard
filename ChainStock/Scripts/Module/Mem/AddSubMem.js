$(document).ready(function () {
    //没有查询会员时 所有空间禁用
    if ($.isEmptyObject(global_mem)) {

        $("#btnSubMemAdd").attr("disabled", "disabled");
    };

    if ($("#MemCard").val() != null) {
        $("#txtFindMember").val($("#MemCard").val());
    }
    //添加子卡
    $("#btnSubMemAdd").bind("click", BtnSubMemAdd);

    //保存按钮响应函数
    $("#btnSave").bind("click", BtnSave);

    //重置按钮响应函数
    $("#btnReset").bind("click", BtnReset);

    CreateSubMemListTable(null);
});

function BtnSubMemAdd() {
    art.dialog({
        title: '添加子卡_' + global_mem.MemName + '(' + global_mem.MemCard + ')',
        content: document.getElementById('divAddSubMem'),
        id: 'divAddSubMem',
        lock: true,
        close: function () {
            $("#txtSubCardNumber").val("");
            $("#txtSubName").val("");
            $("#txtSubMemMobile").val("");
            $("#rdusedT").attr("checked", "true");    
        }
    });
}

/*******************************************************************************
*保存按钮响应函数
*******************************************************************************/

function BtnSave() {
    var strErrorMsg = "";
    var SubMemID = $("#SubMemID").val();
    var type = (SubMemID == "") ? "AddSubMem" : "EditSubMem";
    var subCardNumber = $("#txtSubCardNumber").val();
    var subName = $("#txtSubName").val();
    var subMemMobile = $("#txtSubMemMobile").val();
    var isUsed = $('input:radio[name="rd_used"]:checked').val();
    
    if (!subCardNumber.IsNumber()) {
        strErrorMsg += "<li>会员子卡卡号应该是由数字组成的一个字符串;</li>";
    }
    if (subCardNumber.length < 4) {
        strErrorMsg += "<li>会员子卡卡号必须4~20位;</li>";
    }
    if (subName == "") strErrorMsg += "<li>必须输入子卡姓名;</li>";
    if (subMemMobile != "" && !subMemMobile.IsMobile()) {
        strErrorMsg += "<li>手机号码格式输入错误;</li>";
    }
  
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提示',
            icon: 'error',
            //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    art.dialog({
        title: '系统提示',
        content: '将为父卡:[' + global_mem.MemCard + '] ,添加子卡:[' + subCardNumber + '] 。\n确定操作吗？',
        lock: true,
        ok: function () {
            this.close();
            doAjax("../",
           type,
           {
               "SubMemID":SubMemID,
               "subCardNumber": subCardNumber,
               "subName": subName,
               "subMemMobile": subMemMobile,
               "MemID": global_mem.MemID,
               "MemCard": global_mem.MemCard,
               "isUsed": isUsed              
           },
           "json",
           function (json) {
               switch (json) {                  
                   case -1:
                       art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("系统操作失败，请稍后重试！"),
                              lock: true
                          });
                          break;
                      case -2:
                          art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("不能使用该卡号，请重新输入子卡卡号！"),
                              lock: true
                          });
                          break;
                      case -3:
                          art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("不能使用该手机号，请重新输入子卡手机号码！"),
                              lock: true
                          });
                          break;     
                   default:
                       art.dialog
                            ({
                                title: '系统提示',
                                lock: true,
                                time: 0.5,
                                content: '提交成功！',
                                close: function () {
                                    GetSubMemList(global_mem.MemID);
                                    art.dialog.list['divAddSubMem'].close();
                                }
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




/*******************************************************************************
*重置按钮响应函数
*******************************************************************************/
function BtnReset() {
    $("#txtSubCardNumber").val("");
    $("#txtSubName").val("");
    $("#txtSubMemMobile").val("");
    $("#rdusedT").attr("checked", "true");
}

/****************************************************************************************************
*在选择好会员时可以执行回调函数
*****************************************************************************************************/
function FindMember_CallBack() {
    var strErrorMsg;
    if (global_mem.MemState != 0) {
        strErrorMsg = "当前会员卡处于锁定或者挂失状态，暂不允许进行充值。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;

    }
    if (global_mem.MemIsPast == "True") {
        strErrorMsg = "当前会员卡已过期，暂不允许此操作。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    //查找到会员后 所有控件解禁
    if (global_mem.MemID != "") {
        $("#btnSubMemAdd").attr("disabled", "");   
        GetSubMemList(global_mem.MemID);
    }  
    return true;
}

function GetSubMemList(memID) {
    doAjax(
           "../",
           "GetSubMemList",
           {               
               "memID": memID
           },
           "json",
           function (json) {
               CreateSubMemListTable(json);          
           });
}

function CreateSubMemListTable(obj) {
    var html = '<tr class=\"th\">'
             + ' <th>序号</th><th>子卡姓名</th><th>子卡卡号</th><th>子卡手机号</th><th>是否启用</th><th>操作</th>'
             + '</tr>';
    if (obj != null) {
        var num = 0;
        $.each(obj, function (index, item) {
            num = index + 1;
            html += "<tr class=\"td\">"
                       + '<td>' + num + '</td>'
                        + '<td>' + item.SubName + '</td>'
                        + '<td style="text-align: left">' + item.SubCardNumber + '</td>'
                        + '<td>' + item.SubMemMobile + '</td>'
                        + '<td>' + (item.IsUsed == "True" ? "<span style=\"color:Green;\">启用</span>" : "<span style=\"color:Red;\">停用</span>") + '</td>'
                        + '<td class="listtd" style="width: 120px;">' + "<a href=\"javascript:void(0);\" onclick=\"javascript:SubMemEdit(" + item.ID + ");\"> <img src='../images/Gift/eit.png' alt='编辑' title='编辑' /></a>"
                        + "<a href=\"javascript:void(0);\" onclick=\"javascript:SubMemDelete(" + item.ID + ",'" + item.SubName + "');\"><img src='../images/Gift/del.png' alt='删除' title='删除' /></a>" + '</td>'
                        + '</tr>';
        });
    }
    else {
        html += '<tr><td style="height:25px; line-height:25px;padding-left:20px; background-color:#fff;" colspan="9">未找到符合此条件的数据！</td></tr>';
    }
    $("#SubMemList").html(html);
}

function SubMemDelete(ID) {
    doAjax(
           "../",
           "SubMemDelete",
           {
               "ID": ID
           },
           "json",
           function (json) {
               switch (json) {
                   case 0:
                       art.dialog({
                           title: '系统提示',
                           content: ("系统异常，未保存数据，请再次操作！"),
                           lock: true
                       });
                       break;
                   default:
                       art.dialog({
                           title: '系统提示',
                           time: 0.5,
                           content: '删除成功！',
                           close: function () { GetSubMemList(global_mem.MemID); },
                           lock: true
                       });
                       break;
               }
           });
       }

       function SubMemEdit(ID) {   
           $("#txtSubCardNumber").attr("disabled", "disabled");
           art.dialog({
               title: '编辑子卡_' + global_mem.MemName + '(' + global_mem.MemCard + ')',
               content: document.getElementById('divAddSubMem'),
               id: 'divAddSubMem',
               lock: true,
               close: function () {
                   $("#txtSubCardNumber").val("");
                   $("#txtSubName").val("");
                   $("#txtSubMemMobile").val("");
                   $("#SubMemID").val("")
                   $("#txtSubCardNumber").attr("disabled", "");
               }
           });
           $("#txtSubCardNumber").focus();
           $("#SubMemID").val(ID)
           doAjax(
           "../",
           "SubMemInfo",
           {
               "ID": ID
           },
           "json",
           function (json) {
               if (json != null) {
                   $("#txtSubCardNumber").val(json[0].SubCardNumber);
                   $("#txtSubName").val(json[0].SubName);
                   $("#txtSubMemMobile").val(json[0].SubMemMobile);
                   if (json[0].IsUsed == 'True') {
                       $("#rdusedT").attr("checked", "true");
                   }
                   else {
                       $("#rdusedF").attr("checked", "true");
                   }                 
               }
           });
       }