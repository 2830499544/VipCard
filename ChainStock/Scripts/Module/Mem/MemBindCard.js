/*************************************************************************************************************
会员换卡
**************************************************************************************************************/
$(document).ready(function () {
    if ($.isEmptyObject(global_mem)) {
        $("#txtOldPwd").attr("disabled", "disabled");
        $("#txtNewCard").attr("disabled", "disabled");
        $("#txtNewPwd").attr("disabled", "disabled");
        $("#txtReNewPwd").attr("disabled", "disabled");
        $("#txtCgCardRemark").attr("disabled", "disabled");
        //$("#txtCardNumber").attr("disabled", "disabled");
    };

    if ($("#MemCard").val() != null) {
        $("#txtFindMember").val($("#MemCard").val());
    }
    //是否"密码与旧卡保持一致"
    $("#chkPwd").bind("click", ChkPwd);

    //保存按钮响应函数
    $("#btnChangeCardSave").bind("click", BtnChangeCardSave);

    //重置按钮响应函数
    $("#btnChangeCardReset").bind("click", BtnChangeCardReset);


    //感应式IC卡初始化 按钮响应函数
    $("#btnSenseICCardInit").bind("click", btnSenseICCardInit)
    //接触式IC卡初始化 按钮响应函数
    $("#btnContactICCardInit").bind("click", btnContactICCardInit);

    //发卡：感应式IC卡按钮响应函数
    $("#btnSendSenseICCard").bind("click", btnSendSenseICCard);
    //发卡：接触式IC卡按钮响应函数
    $("#btnContactICCard").bind("click", btnContactICCard);
});

/*******************************************************************************
* 密码与旧卡保持一致响应函数
*******************************************************************************/
function ChkPwd() {
    if (document.getElementById("chkPwd").checked) {
        $("#txtNewPwd").attr("disabled", "disabled");
        $("#txtReNewPwd").attr("disabled", "disabled");
        $("#txtNewPwd").css("background-color", "#ddd");
        $("#txtReNewPwd").css("background-color", "#ddd");
        $("#txtNewPwd").val("");
        $("#txtReNewPwd").val("");
    }
    else {
        $("#txtNewPwd").attr("disabled", false);
        $("#txtReNewPwd").attr("disabled", false);
        $("#txtNewPwd").css("background-color", "#fff");
        $("#txtReNewPwd").css("background-color", "#fff");
    }
}

/*******************************************************************************
* 保存按钮响应函数
*******************************************************************************/
function BtnChangeCardSave() {
    var strErrorMsg = "";
    if ($.isEmptyObject(global_mem)) {
        strErrorMsg += "<li>请先选择会员！</li>";
    }
    var Name = global_mem.MemName;
    var oldCard = global_mem.MemCard;
    if (oldCard != "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: '该会员已经绑定会员卡,不能再绑定',
            lock: true
        });
        return false;
        
    }
    var memID = global_mem.MemID;
    var strNewCard = $.trim($("#txtNewCard").val());


    var strMemCardNumber = $.trim($("#txtCardNumber").val());
    if (strNewCard == "") {
        strErrorMsg += "<li>请输入会员卡号！</li>";
    }
    if (strNewCard.length < 4) {
        strErrorMsg += "<li>会员卡号最小长度为4位最长为20位;</li>";
    }
   

    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        //        var throughBox = art.dialog.through;
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
        content: "将为会员[" + Name + "] 绑定卡号：[" + strNewCard + "] 。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
                   "BindMemCard",
                   {
                       "memID": memID,
                       "newCard": strNewCard,                      
                       "newCardNumber": strMemCardNumber,
                       "remark": $("#txtCgCardRemark").val(),
                       "chkIsOldPwd": $("#chkIsOldPwd").attr("checked")
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
                           case 1:
                               art.dialog
                                    ({
                                        title: '系统提示',
                                        time: 0.5,
                                        content: '会员绑卡成功',
                                        close: function () { window.location.href = "../Member/MemBindCard.aspx?PID=186"},
                                        lock: true
                                    });
                               break;
                           case -1:
                               art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统错误 请与系统管理员联系！"),
                                     lock: true
                                 });
                               break;
                          
                           case -4:
                               art.dialog
                                ({
                                    title: '系统提示',
                                    time: 4,
                                    content: ("会员卡号已存在，请重新输入！"),
                                    lock: true
                                });
                               break;
                           case -7:
                               art.dialog
                                ({
                                    title: '系统提示',
                                    time: 4,
                                    content: ("该卡不在发卡范围内，请与总店联系！"),
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

/*******************************************************************************
*重置按钮响应函数
*******************************************************************************/
function BtnChangeCardReset() {
    window.location.href = '../Member/MemChangeCard.aspx?PID=6';
}

/****************************************************************************************************
*在选择好会员时可以执行回调函数
*****************************************************************************************************/
function FindMember_CallBack() {
    var strErrorMsg;
    if (global_mem.MemState != 0) {
        strErrorMsg = "当前会员卡处于锁定或者挂失状态，暂不允许进行换卡。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    if (global_mem.MemIsPast == "True") {
        strErrorMsg = "当前会员卡已过期，暂不允许进行换卡。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    if (global_mem.MemPassword == "E62A9E6C1892C6BB") {
        $("#lblchkPwd").css("display", "none");
        //        $("#trNewPwd").css("display", "none");
        $("#trOldPwd").css("display", "none");
        //        $("#trReNewPwd").css("display", "none");
        //        $("#chkPwd").attr("checked", "true");
    }
    //    else {
    //        $("#lblchkPwd").css("display", "");
    //        //        $("#trNewPwd").css("display", "");
    //        //        $("#trReNewPwd").css("display", "");
    //        $("#trOldPwd").css("display", "");
    //    }
    if (global_mem.MemID != "") {
        $("#txtOldPwd").attr("disabled", false);
        $("#txtNewCard").attr("disabled", false);
        $("#txtNewPwd").attr("disabled", false);
        $("#txtReNewPwd").attr("disabled", false);
        $("#txtCgCardRemark").attr("disabled", false);
        //$("#txtCardNumber").attr("disabled", "");
        $("#txtOldPwd").focus();
    }

    return true;
}




function btnSenseICCardInit() {
    art.dialog({
        title: '系统提示',
        content: "确定要初始化感应式IC卡吗?",
        lock: true,
        ok: function () {
            this.close();
            CardReader.IniCard(); //初始化
            setTimeout("showMsg()", 500);
        },
        cancelVal: '关闭',
        cancel: true
    })
}
function showMsg() {
    var aa = CardReader.errMsg;
    //    alert(CardReader.errMsg);
    art.dialog({
        title: '系统提示',
        content: aa,
        time: 1.5,
        lock: true
    })



}

/*******************************************************************************
*初始化：感应式IC卡按钮响应函数
*******************************************************************************/
function btnContactICCardInit() {
    art.dialog({
        title: '系统提示',
        content: "确定要初始化接触式IC卡吗?",
        lock: true,
        ok: function () {
            this.close();
            this.lock();
            setTimeout("InitContacticcard()", 200);
        },
        cancelVal: '关闭',
        cancel: true
    })
}
/*******************************************************************************
*初始化：接触式IC卡按钮响应函数
*******************************************************************************/
function InitContacticcard() {
    var status = null;
    var setValue = "";
    MWReaderCtl.MWic_init(0, 9600);
    status = MWReaderCtl.LastRet;
    if (status == 0) {
        MWReaderCtl.MWcsc_4442(6, "ffffff");
        status = MWReaderCtl.LastRet;
        if (status != 0) {
            art.dialog({
                title: '系统提示',
                content: '密码验证错误，请检查设备的LED灯是否处于绿色状态，若不是请将IC卡插好，否则请与管理员联系！！',
                time: 3
            })
            MWReaderCtl.MWic_exit();
            return;
        }
        for (var i = 0; i < 20; i++) {
            setValue += "F";
        }
        MWReaderCtl.MWswr_4442(60, 20, setValue);
        status = MWReaderCtl.LastRet;
        if (status == 0) {
            art.dialog({
                title: '系统提示',
                content: '接触式IC卡初始化成功！！',
                time: 3
            })
        }
        else {
            art.dialog({
                title: '系统提示',
                content: '接触式IC卡初始化失败，请与管理员联系！！',
                time: 3
            })
            MWReaderCtl.MWic_exit();
            return;
        }
        MWReaderCtl.MWic_exit();
    }
    else {
        art.dialog({
            title: '系统提示',
            content: '接触式IC卡设备打开失败，请检查设备连接！',
            time: 3
        });
    }
}

/*******************************************************************************
*发卡：感应式IC卡按钮响应函数
*******************************************************************************/
function btnSendSenseICCard() {
    var strErrorMsg = "";
    var memCard = $.trim($("#txtNewCard").val());
    memCard = memCard.replace(/\s/ig, '');

    if (!/\d+/.test(memCard)) {
        strErrorMsg += "<li>会员卡号应该是由数字组成的一个字符串;</li>";
    }
    if (memCard.length < 4 || memCard.length > 20) {
        strErrorMsg += "<li>会员卡号必须4~20位;</li>";
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
    doAjax("../", "GetModelByMemCard", { "memCard": memCard }, "json", function (json) {
        CardReader.GiveCard(memCard);  //发卡  
        setTimeout("showMsg()", 200)
    });
}

function showMsg() {
    var msg = CardReader.errMsg;
    art.dialog({
        title: "系统提示",
        content: msg,
        time: 3
    });
    if (msg == "发卡成功！！") {
        $("#txtMemCard").attr("readOnly", true);
    }
    else {
        //alert('发卡失败');
    }
}




/*******************************************************************************
*发卡：接触式IC卡按钮响应函数
*******************************************************************************/
function btnContactICCard() {
    var strErrorMsg = "";
    var memCard = $.trim($("#txtMemCard").val());
    memCard = memCard.replace(/\s/ig, '');
    if (!/\d+/.test(memCard)) {
        strErrorMsg += "<li>会员卡号应该是由数字组成的一个字符串;</li>";
    }
    if (memCard.length < 4 || memCard.length > 20) {
        strErrorMsg += "<li>会员卡号必须4~20位;</li>";
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

    doAjax("../", "GetModelByMemCard", { "memCard": memCard }, "json", function (json) {
        if (json == "1") {
            art.dialog({
                title: '系统提示',
                content: '此卡号在系统中已存在，请换个卡号',
                lock: true,
                time: 3
            })
            $("#txtMemCard").val("").focus();
        }
        else {
            sendContactICCard(memCard);
        }
    })
}
function sendContactICCard(memCard) {
    var status = null;
    var getValue = null;
    MWReaderCtl.MWic_init(0, 9600);
    status = MWReaderCtl.LastRet;
    if (status == 0) {
        MWReaderCtl.MWcsc_4442(6, "ffffff");
        status = MWReaderCtl.LastRet;
        if (status != 0) {
            art.dialog({
                title: '系统提示',
                content: '密码验证错误，请检查设备的LED灯是否处于绿色状态，若不是请将IC卡插好，否则请与管理员联系！！',
                lock: true,
                time: 3
            })
            MWReaderCtl.MWic_exit();
            return;
        }
        getValue = MWReaderCtl.MWsrd_4442(60, 20);
        status = MWReaderCtl.LastRet;
        if (status == 0) {
            if (!/^[F,f]{20,40}$/.test(getValue)) {
                art.dialog({
                    title: '系统提示',
                    content: '此卡未初始化，请初始化后再发卡！！',
                    lock: true,
                    time: 3
                })
                MWReaderCtl.MWic_exit();
                return;
            }
        }
        else {
            art.dialog({
                title: '系统提示',
                content: '检查此卡是否初始化时失败，请与管理员联系！！',
                lock: true,
                time: 3
            })
            MWReaderCtl.MWic_exit();
            return;
        }
        if (memCard.length < 20) {
            var end = 20 - memCard.length;
            for (var i = 0; i < end; i++) {
                memCard += "F";
            }
        }
        MWReaderCtl.MWswr_4442(60, 20, memCard);
        status = MWReaderCtl.LastRet;
        if (status == 0) {
            art.dialog({
                title: '系统提示',
                content: '发卡成功！！',
                time: 3
            });
            $("#txtMemCard").attr("readOnly", true);
        }
        else {
            art.dialog({
                title: '系统提示',
                content: '发卡失败,请重试！！',
                lock: true,
                time: 3
            });
            MWReaderCtl.MWic_exit();
            return;
        }
        MWReaderCtl.MWic_exit();
    }
    else {
        art.dialog({
            title: '系统提示',
            content: '接触式IC卡设备打开失败，请检查设备连接！',
            lock: true,
            time: 3
        });
    }
}