$(function () {
    //新增活动
    $("#btnRechargeRuleAdd").bind("click", btnRechargeRuleAddClick);

    //保存
    $("#btnRechargeRuleSave").bind("click", btnRechargeRuleSaveClick);
})

//新增充值赠送活动
function btnRechargeRuleAddClick() {
    $("#txtRechargeMoney,#txtGiveMoney,#txtRuleDesc").val("");
    artDialog({
        title: '新增活动',
        content: document.getElementById('RechargeRuleInfo'),
        id: 'RechargeRuleInfo',
        lock: true
    })
}

//保存
function btnRechargeRuleSaveClick() {
 
    var txtRechargeMoney = $.trim($("#txtRechargeMoney").val());
  
    var txtRuleID = $.trim($("#txtRuleID").val());

    var txtGiveMoney = $.trim($("#txtGiveMoney").val());

    var txtRuleDesc = $.trim($("#txtRuleDesc").val());

    var type = $("#txtRuleID").val() == "" ? "AddRechargeRule" : "EditRechargeRule";

    var strErrorMsg = "";
    if (txtRechargeMoney == "") { strErrorMsg += "<li>请填写充值金额</li>"; }
    if (txtGiveMoney == "") { strErrorMsg += "<li>请填写赠送金额</li>"; }
   
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提醒',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }

    art.dialog({
        title: '系统提示',
        content: "将" + (type == "AddRechargeRule" ? "新增" : "修改") + "充值赠送活动。\n确定操作吗？",
        lock: true,
        ok: function () {
            doAjax("../", type, {
                "RuleID": txtRuleID,
                "RechargeMoney": txtRechargeMoney,
                "GiveMoney": txtGiveMoney,
                "RuleDesc": txtRuleDesc

            }, "text", function (text) {
                if (text == "0") {
                    art.dialog({
                        title: '系统提示',
                        time: 4,
                        content: ("系统异常，未保存数据，请再次点击保存！"),
                        lock: true
                    });
                } else {
                    art.dialog({
                        title: '系统提示',
                        time: 0.5,
                        content: '保存成功！',
                        close: function () { window.location = window.location; },
                        lock: true
                    });
                }
            })
        },
        cancelVal: '取消',
        cancel: true
    })
}


//删除
function btnRechargeRuleDel(RuleID, RechargeMoney,GiveMoney) {
    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除活动【充' + RechargeMoney + '送' + GiveMoney + '】吗? 此操作不可恢复',
        ok: function () {
            this.lock();
            doAjax("../", "DelRechargeRule", { "RuleID": RuleID }, "text", function (text) {
                if (text == "0") {
                    art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("系统错误 请与系统管理员联系！"),
                                  lock: true
                              });
                } else {
                    art.dialog
                                ({
                                    title: '系统提示',
                                    time: 0.5,
                                    content: '删除成功！',
                                    close: function () { window.location = window.location; },
                                    lock: true
                                });
                }
            })
            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    })
}

//编辑
function btnRechargeRuleEdit(RuleID) {
    artDialog({
        title: '编辑充值活动',
        content: document.getElementById('RechargeRuleInfo'),
        id: 'RechargeRuleInfo',
        lock: true
    })

    doAjax("../", "GetRechargeRule",
                    {
                        "RuleID": RuleID

                    }
                    , "json", function (json) {
                  
                        $("#txtRuleID").val(json[0].RuleID);
                        $("#txtRechargeMoney").val(json[0].RechargeMoney);
                        $("#txtGiveMoney").val(json[0].GiveMoney);
                        $("#txtRuleDesc").val(json[0].RuleDesc);

                    });


 
}