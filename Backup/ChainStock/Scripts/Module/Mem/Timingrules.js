$(document).ready(function () {
    $("#btnAddTimingRules").bind("click", AddTimingRules);
});
function AddTimingRules() {
    ResetForm();
    art.dialog({
        lock: true,
        title: '新增规则',
        content: $("#TimingrulesEdit").html(),
        id: 'TimingrulesEdit',
        close: function () { ResetForm(); }
    });
}

function SaveForm() {
    var RulesName = $("#txtRulesName").val();
    var RulesInterval = $("#txtRulesInterval").val();
    var RulesUnitPrice = $("#txtRulesUnitPrice").val();
    var RulesExceedTime = $("#txtRulesExceedTime").val();
    var RulesRemark = $("#txtRulesRemark").val();
    var RulesID = $("#txtRulesID").val().toString();
    var MethodName = RulesID == "" ? "RulesAdd" : "RulesEdit";
    var strErrorMsg = "";
    if (RulesName == "") { strErrorMsg += "<li>规则名称不能为空！</li>"; }
    if (RulesInterval == "" || RulesUnitPrice == "") {
        strErrorMsg += "<li>单位计时规则不能为空！</li>";
    }
    else {
        if (!RulesInterval.IsNumber()) { strErrorMsg += "<li>分钟格式有误，只能输入正整数！</li>"; }
        if (!RulesUnitPrice.IsDecimal()) { strErrorMsg += "<li>金额格式输入有误！</li>"; }
    }
    if (RulesExceedTime == "") {
        strErrorMsg += "<li>不足单位计费不能为空！</li>";
    }
    else {
        if (!RulesExceedTime.IsNumber()) { strErrorMsg += "<li>不足单位计费格式有误，只能输入正整数！</li>"; }
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
        content: '将' + (RulesID == "" ? "添加" : "编辑") + '规则[' + RulesName + ']。\n确定操作吗？',
        lock: true,
        ok: function () {
            this.close();
            doAjax("../",
                    MethodName,
                   {
                       "RulesName": RulesName,
                       "RulesInterval": RulesInterval,
                       "RulesUnitPrice": RulesUnitPrice,
                       "RulesExceedTime": RulesExceedTime,
                       "RulesID": RulesID,
                       "RulesRemark": RulesRemark
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
                                 close: function () { location.href = 'Timingrules.aspx?PID=110'; },
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

function EditTimingrules(RulesID) {
    doAjax("../",
             'GetRules', { "RulesID": RulesID }, "json",
                 function (json) {
                     $("#txtRulesName").val(json.RulesName);
                     $("#txtRulesInterval").val(json.RulesInterval);
                     $("#txtRulesUnitPrice").val(parseFloat(json.RulesUnitPrice).toFixed(2));
                     $("#txtRulesExceedTime").val(json.RulesExceedTime);
                     $("#txtRulesID").val(json.RulesID);
                     $("#txtRulesRemark").val(json.RulesRemark);
                 }
               )
    art.dialog({
        lock: true,
        title: '编辑规则',
        content: $("#TimingrulesEdit").html(),
        id: 'TimingrulesEdit',
        close: function () { ResetForm(); }
    });
}

function DeleteTimingrules(RulesID, RulesName) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除规则【' + RulesName + '】吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
             'DelRules', { "RulesID": RulesID }, "json",
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
                                       content: ("该规则已有使用记录无法删除！"),
                                       lock: true
                                   });
                             break;
                         default:
                             art.dialog
                                   ({
                                       time: 2,
                                       content: '删除成功！',
                                       close: function () { location.href = 'Timingrules.aspx?PID=110'; }
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

function ResetForm() {
    $("#txtRulesName,#txtRulesInterval,#txtRulesUnitPrice,#txtRulesExceedTime,#txtRulesID,#txtRulesRemark").val("");
}