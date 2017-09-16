
$(document).ready(function () {
    //增加模版按钮响应函数
    $("#btnAddTemplate").bind("click", BtnAddTemplate);

    //"保存"按键响应函数
    $("#btnTemplateSave").bind("click", BtnTemplateSave);

    //重置按钮响应事件
    $("#btnTemplateReset").bind("click", BtnTemplateReset);
});


/*******************************************************************************
*增加模版按钮响应函数
*******************************************************************************/
function BtnAddTemplate() {
    art.dialog({
        title: '新增短信模板',
        content: document.getElementById('divAddOrEditTemplate'),
        id: 'divAddOrEditTemplate',
        lock: true,
        close: function () {
            $("#txtTemplateID").val("");
            $("#txtTemplateName").val("");
            $("#txtTemplateContent").val("");
        }
    });
}

/*******************************************************************************
*编辑模版响应函数
*******************************************************************************/
function EditTemplate(templateID) {
    $("#txtTemplateID").val(templateID);

    art.dialog({
        title: '编辑短信模板',
        content: document.getElementById('divAddOrEditTemplate'),
        id: 'divAddOrEditTemplate',
        lock: true,
        close: function () {
            $("#txtTemplateID").val("");
            $("#txtTemplateName").val("");
            $("#txtTemplateContent").val("");
        }
    });

    doAjax("../",
           "GetSmsTemplate",
           { "TemplateID": templateID },
           "json",
           function (json) {
               $("#txtTemplateName").val(json[0].TemplateName);
               $("#txtTemplateContent").val(json[0].TemplateContent);
           });
}

/*******************************************************************************
*删除模版响应函数
*******************************************************************************/
function DeleteTemplate(templateID, templateName) {
    if (templateID <= 9) {
        art.dialog({
            title: '系统提示',
            content: "系统自动发送短信模板不能删除。",   //弹出层显示文本
            icon: 'error',  //图标
            lock: true      //是否锁定背景
        });
    }
    else {
        art.dialog({
            title: '系统提示',
            lock: true,
            content: '确定要删除【' + templateName + '】吗? 此操作不可恢复',
            ok: function () {
                this.close();
                doAjax("../",
                   "SmsTemplateDelete",
                   { "TemplateID": templateID },
                   "json",
                   function (json) {
                       switch (json) {
                           case 0:
                               art.dialog({
                                   title: '系统提示',
                                   time: 4,
                                   content: ("系统异常，未删除模板，请再次点击删除！"),
                                   lock: true
                               });
                               break;
                           case 1:
                               art.dialog({
                                   title: '系统提示',
                                   time: 0.5,
                                   content: '删除成功！',
                                   close: function () { location.href = "./SmsTemplate.aspx?PID=54"; }
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
    return false;
}

/*******************************************************************************************
*保存按键响应函数
*******************************************************************************************/
function BtnTemplateSave() {
    var strErrorMsg = "";

    var strTemplateID = $.trim($("#txtTemplateID").val());
    var strTemplateName = $.trim($("#txtTemplateName").val());
    var strTemplateContent = $.trim($("#txtTemplateContent").val());

    var type = (strTemplateID == "") ? "SmsTemplateAdd" : "SmsTemplateEdit";
    if (strTemplateName == "") strErrorMsg += "<li>模板名称不能为空;</li>";
    strTemplateContent = strTemplateContent.replace(/\s/g, "");
    if (strTemplateContent == "") strErrorMsg += "<li>模板内容不能为空;</li>";
    else if ((/[银銀]行/).test(strTemplateContent))
        strErrorMsg += "<li>短信模版中不能出现银行关键字(词);</li>";
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        var throughBox = art.dialog.through;
        throughBox({
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    art.dialog({
        title: '系统提示',
        content: "将" + (type == "SmsTemplateAdd" ? "增加" : "修改") + "模板 [" + strTemplateName + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
     type,
     {
         "templateID": strTemplateID,
         "templateName": strTemplateName,
         "templateContent": strTemplateContent
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
                      time: 1.5,
                      content: '保存成功！',
                      close: function () { location.href = "./SmsTemplate.aspx?PID=54"; }
                  });
                  break;
          }
      });
        },
        cancelVal: '取消',
        cancel: true
    });
}

/*******************************************************************************************
*重置按钮响应事件
*******************************************************************************************/
function BtnTemplateReset() {
    var strTemplateID = $.trim($("#txtTemplateID").val());
    if (strTemplateID != "") {
        EditTemplate(strTemplateID);
    }
    else {
        $("#txtTemplateID").val("");
        $("#txtTemplateName").val("");
        $("#txtTemplateContent").val("");
    }
}