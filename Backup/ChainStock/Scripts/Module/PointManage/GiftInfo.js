$(document).ready(function () {
    $("#txtGiftName").focus();

    //保存按钮响应函数
    $("#btnGiftSave").bind("click", BtnGiftSave);

    //重置按钮响应函数
    $("#btnGiftReset").bind("click", BtnGiftReset);

    //根据礼品名称自动生成礼品简码
    var PinYinCheck;
    $("#txtGiftName").keyup(function () {
        clearTimeout(PinYinCheck);
        PinYinCheck = setTimeout("getPYCode(document.getElementById('txtGiftName').value, 'txtGiftCode')", 500);
    });

    // 图片上传处理
    if (location.href.toLowerCase().indexOf("giftlist.aspx") != -1) {
        $("#GiftPhoto_Uploadify").uploadify({
            'uploader': "../images/swf/uploadify.swf",
            'script': "../Service/upload.ashx",
            'cancelImg': "../images/member/cancel.png",
            'folder': "../Upload/GiftPhoto",
            'queueID': 'Gift_fileQueue',
            'buttonImg': "../images/member/selectImg.jpg",
            'height': 25,
            'width': 71,
            'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
            'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
            'auto': false,
            'multi': false,
            'sizeLimit': 512000,
            'onError': function (event, ID, fileObj, errorObj) {
                if (errorObj.type == "File Size")
                    alert("对不起，上传的图片不能超过500K");
                else
                    alert(errorObj.type + ' Error: ' + errorObj.info);
            },
            'onComplete': function (event, ID, fileObj, response, data) {
                if (response != "0") {
                    $("#imgGiftPhoto").attr("src", response);
                    $("#txtGiftPhoto").val(response);
                }
            }
        });
    }

});

//function ShowOnlinePhoto() {
//    art.dialog.open('../PointManage/GiftOnLinePhoto.htm', { title: '在线拍照', lock: true
//    }, false);

//}

/*******************************************************************************
*礼品信息保存按钮响应函数
*******************************************************************************/
function BtnGiftSave() {
    var strErrorMsg = "";
    var type = ($("#txtGiftID").val() == "") ? "GiftAdd" : "GiftEdit";
    var reg = /^[0-9]+(.[0-9]{1,4})?$/;
    var r = /^\+?[1-9][0-9]*$/;
    if ($.trim($("#txtGiftName").val()) == "") strErrorMsg += "<li>必须输入礼品名称；</li>";
    if ($.trim($("#txtGiftCode").val()) == "") strErrorMsg += "<li>必须输入礼品简码（礼品简码是为了方便您搜索礼品而设置的）；</li>";
//    if ($("#sltGiftClass").find("option:selected").attr("parent")) {
//        strErrorMsg += "<li>选择的礼品类别，不能拥有子分类;</li>";
//    }
    if ($("#sltGiftClass").val() == "") {
        strErrorMsg += "<li>必须选择礼品类别;</li>";
    }
    if ($.trim($("#txtGiftExchangePoint").val()) == "") strErrorMsg += "<li>请输入正确的兑换此礼品所需的积分数量；</li>";
    if ($.trim($("#txtGiftStockNumber").val()) == "") strErrorMsg += "<li>请输入正确的礼品库存数量（当库存数量不足时，将无法被兑换）；</li>";

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
        content: "将" + (type == "GiftAdd" ? "增加" : "编辑") + "礼品 [" + $.trim($("#txtGiftName").val()) + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
           type,
           {
               "giftID": $("#txtGiftID").val(),
               "giftName": $.trim($("#txtGiftName").val()),
               "giftCode": $("#txtGiftCode").val(),
               "giftClassID": $("#sltGiftClass").val(),
               "giftStockNumber": $.trim($("#txtGiftStockNumber").val()),
               "giftExchangePoint": $.trim($("#txtGiftExchangePoint").val()),
               "giftPhoto": $("#txtGiftPhoto").val(),
               "giftExchangeNumber": $("#txtGiftExchangeNumber").val(),
               "giftRemark": $("#txtGiftRemark").val()
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
                              content: ("此礼品已经在系统中存在，请重新输入，然后重试！"),
                              lock: true
                          });

                        break;
                    case -3:
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
                                content: '保存成功！',
                                close: function () { location.href = "GiftList.aspx?PID=13"; },
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
}

/*******************************************************************************
*重置按钮响应函数
*******************************************************************************/
function BtnGiftReset() {
    if ($("#GiftAddOrEdit").val() == 0) {
        $("#txtGiftID").val("");
        $("#txtGiftName").val("");
        $("#txtGiftCode").val("");
        $("#sltGiftClass").val(""),
        $("#txtGiftStockNumber").val("");
        $("#txtGiftExchangePoint").val("");
        $("#txtGiftRemark").val("");
    }
    else if ($("#GiftAddOrEdit").val() == 1) {
        btnGiftEdit($("#txtGiftName").val(), $("#txtGiftID").val());
    }
}
//礼品新增
function GiftAdd() {
    art.dialog({
        lock: true,
        title: '礼品新增',
        width: '620px',
        content: document.getElementById('DiveGiftInfo'),
        id: 'DiveGiftInfo',
        close: function () { BtnGiftReset(); }
    });
    $("#GiftAddOrEdit").val(0);
    $("#txtGiftName").focus();
}
//礼品删除
function btnGiftDel(giftname, giftid) {
    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除礼品【' + giftname + '】吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
             'GiftDel', { "GiftID": giftid }, "json",
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
                         case -3:
                             art.dialog
                                ({
                                    title: '系统提示',
                                    time: 4,
                                    content: ("系统异常，请联系管理员！"),
                                    lock: true
                                });

                             break;
                         case -4:
                             art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("礼品已经进行过兑换，不可删除！"),
                                  lock: true
                              });
                             break;
                         default:
                             art.dialog
                                   ({
                                       time: 2,
                                       content: '删除成功！',
                                       close: function () { location.href = "GiftList.aspx?PID=13"; }
                                   });
                             break;
                     }
                 })

            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    });
    return false;
}
//礼品编辑
function btnGiftEdit(giftname, giftid) {
    $("#GiftAddOrEdit").val(1);
    art.dialog({
        lock: true,
        title: '礼品编辑',
        content: document.getElementById('DiveGiftInfo'),
        id: 'DiveGiftInfo',
        close: function () { $("#GiftAddOrEdit").val(0); BtnGiftReset(); }
    });
    doAjax("../",
             'GetGiftModel', { "GiftID": giftid }, "json",
                 function (json) {
                     $("#txtGiftName").val(json.GiftName);
                     $("#txtGiftID").val(json.GiftCode);
                     $("#txtGiftCode").val(json.GiftCode);
                     $("#sltGiftClass").val(json.GiftClassID),
                     $("#txtGiftName").val(json.GiftName);
                     $("#txtGiftRemark").val(json.GiftRemark);
                     if (json.GiftPhoto != "") {
                         $("#imgGiftPhoto").attr("src", json.GiftPhoto);
                         $("#txtGiftPhoto").val(json.GiftPhoto);
                     }
                     $("#txtGiftStockNumber").val(json.GiftStockNumber);
                     $("#txtGiftExchangePoint").val(json.GiftExchangePoint);
                     $("#txtGiftExchangeNumber").val(json.GiftExchangeNumber);
                     $("#txtGiftID").val(json.GiftID);
                 });
}