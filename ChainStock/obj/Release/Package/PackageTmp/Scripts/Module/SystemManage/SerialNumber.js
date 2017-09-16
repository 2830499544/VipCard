var locationUrl;
$(document).ready(function () {
    locationUrl = "SerialNumberList.aspx?PID=181";
    //"保存"按键响应函数
    $("#btnSNSave").bind("click", SNSave);

    //"重置"按键响应函数
    //$("#btnUserReset").bind("click", UserReset);

    $("#makeSN").bind("click", MaskSN);
     
   
});

function Lock(id, serialNumber, isLock) {
    var content_Str = '';
    if (isLock=='False')
        content_Str = '确定要锁定【' + serialNumber + '】吗? 锁定以后该卡不能进行注册';
    else
        content_Str = '确定要解锁【' + serialNumber + '】吗? 解锁以后该卡可以进行注册';
    art.dialog({
        title: '系统提示',
        lock: true,
        content: content_Str,
        ok: function () {
            this.close();
            doAjax("../",
             'SerialNumberLock', { "ID": id, "IsLock": isLock }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统异常 操作失败，请重试！"),
                                       lock: true
                                   });
                             break;
                         case -3:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统错误 请与系统管理员联系！"),
                                       lock: true
                                   });
                             break;
                         case 1:
                             art.dialog
                                   ({
                                       time: 2,
                                       content: isLock == 'False' ? "锁定成功" : "解锁成功",
                                       close: function () { location.href = locationUrl; }
                                   });
                             break;
                         default:
                             strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + json.result.toString() + "</ul>";
                             art.dialog({
                                 title: '系统提示',
                                 icon: 'error', //图标
                                 content: strErrorMsg,
                                 lock: true
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

function Card(id, serialNumber, isCard) {
    var content_Str = '';
    if (isCard=='False')
        content_Str = '确定要将【' + serialNumber + '】进行制卡吗?';
    else
        content_Str = '确定要将【' + serialNumber + '】解除制卡吗? ';
    art.dialog({
        title: '系统提示',
        lock: true,
        content: content_Str,
        ok: function () {
            this.close();
            doAjax("../",
             'SerialNumberCard', { "ID": id, "IsCard": isCard }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统异常 操作失败，请重试！"),
                                       lock: true
                                   });
                             break;
                         case -3:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统错误 请与系统管理员联系！"),
                                       lock: true
                                   });
                             break;
                         case 1:
                             art.dialog
                                   ({
                                       time: 2,
                                       content: isCard == 'False' ? "制卡成功" : "解除制卡成功",
                                       close: function () { location.href = locationUrl; }
                                   });
                             break;
                         default:
                             strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + json.result.toString() + "</ul>";
                             art.dialog({
                                 title: '系统提示',
                                 icon: 'error', //图标
                                 content: strErrorMsg,
                                 lock: true
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


function SNSave() {
    var strErrorMsg = "";
    var snNumber = $("#txtNumber").val();
    var isLock = $("input[name='radChooseYesOrNo']:checked").val();
    isLock = isLock == 1 ? true : false;
    if (snNumber == "" ) {
        strErrorMsg += "<li>请输入需要生成序列号数量;</li>";
    }
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        //var throughBox = art.dialog.through;
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
        content: '将产生新的[' + snNumber + ']个序列号。\n确定操作吗？',
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
               "MakeSerialNumber",
               {
                   "Number": snNumber,
                   "IsLock": isLock
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
                        case -1:
                            art.dialog
                                ({
                                    title: '系统提示',
                                    time: 4,
                                    content: ("会员旧密码输入不正确,请重新输入！"),
                                    lock: true
                                });

                            break;
                        case -2:
                            art.dialog
                             ({
                                 title: '系统提示',
                                 time: 4,
                                 content: ("系统错误 请与系统管理员联系！"),
                                 lock: true
                             });
                            break;
                        default:
                            art.dialog
                             ({
                                 title: '系统提示',
                                 time: 0.5,
                                 content: '序列号生成成功！',
                                 close: function () { location.href = locationUrl; },
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

function SerialNumberAdd() {
    $("#txtNumber").val("");
    art.dialog({
        lock: true,
        title: '新增序列号',
        content: document.getElementById('MaskSN'),
        id: 'MaskSN',
        close: function () { $("#txtNumber").val(""); $("#radChooseNo").val(0); }
    });
    $("#txtNumber").focus();
    
}