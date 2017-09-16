$(function () {


    $("#PromotionsPhoto_Uploadify").uploadify({
        'uploader': "../images/swf/uploadify.swf",
        'script': "../Service/MicroWebsiteUpload.ashx",
        'cancelImg': "../images/member/cancel.png",
        'folder': "../Upload/MicroWebsite/MicroWebsitePromotions",
        'queueID': 'PromotionsCenter_fileQueue',
        'buttonImg': "../images/member/selectImg.jpg",
        'height': 25,
        'width': 70,
        'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
        'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
        'fileDataName': "MicroWebsitePromotions",
        'auto': false,
        'multi': false,
        'method': 'get',
        'sizeLimit': 512000,
        'onError': function (event, ID, fileObj, errorObj) {
            if (errorObj.type == "File Size")
                alert("对不起，上传的图片不能超过500K");
            else
                alert(errorObj.type + ' Error: ' + errorObj.info);
        },
        'onComplete': function (event, ID, fileObj, response, data) {
            if (response.length > 1) {
                $("#txtUpdatePromotionsName").val(response);
                $("#imgPromotionsPhoto").attr("src", "../Upload/MicroWebsite/MicroWebsitePromotions/" + response + "?" + GetGuid());
            }
        },
        'onSelect': function (event, queueID, fileObj) {
            $("#PromotionsPhoto_Uploadify").uploadifySettings('scriptData', { 'name': $("#MerchantPhoto").val() });
        }
    });

    $("input[name='radCouponYesOrNo']:radio").bind("change", function () {
        $("#txtPromotionsStartTime,#txtPromotionsEndTime").val("");
    });

    $('#txtPromotionsEndTime').bind("focus", function () {
        WdatePicker({ skin: 'whyGreen', minDate: '#F{$dp.$D(\'txtPromotionsStartTime\')}', isShowClear: true, readOnly: true });

    });
    $('#txtPromotionsStartTime').bind("focus", function () {
        var EndTime = $dp.$('txtPromotionsEndTime');
        WdatePicker({ skin: 'whyGreen', minDate: '%y-%M-%d', isShowClear: true, readOnly: true, onpicked: function () { EndTime.focus(); } });
    });

 
    //保存
    $("#btnPromotionsSave").bind("click", btnPromotionsSaveClick);


    $("#btnSelectShop").bind("click", btnSelectShop);

})
//选择同步的商家
function btnSelectShop() {

    var dialogSelectShop = art.dialog({
        title: '选择参与活动的商家',
        content: document.getElementById('divShopSelect'),
        id: 'divShopSelect',
        lock: true
    });

    $("#btnSelectShopOK").bind("click", function () {
        var str = "";
        var strName = "";
        $(".cbkSelectShop").each(function () {
            if ($(this).attr("checked")) {
                str += $(this).val() + ",";
                strName += $(this).parents("tr").find(".shopname").html()+";";              
            }
        });
        str = str.substring(0, str.length - 1);
        $("#txtShopList").val(str);
        $("#txtShopNameList").val(strName);
        dialogSelectShop.close();
    });

}


//保存
function btnPromotionsSaveClick() {
   
    var txtPromotionsTitle = $.trim($("#txtPromotionsTitle").val());
    var radPromotionsYes = $("#radPromotionsYes").attr("checked");
    var txtPromotionsStartTime = $.trim($("#txtPromotionsStartTime").val());
    var txtPromotionsEndTime = $.trim($("#txtPromotionsEndTime").val());
    var sltPromotionsLevel = $("#sltPromotionsLevel").val();
    var txtPromotionsDesc = $("#txtPromotionsDesc").val();
    var txtPromotionsRemark = $("#txtPromotionsRemark").val();
    var txtUpdatePromotionsName = $("#txtUpdatePromotionsName").val();
    var txtPromotionsID = $("#txtPromotionsID").val();
    var txtShopList = $("#txtShopList").val();
    var type = txtPromotionsID == "" ? "AddPromotions" : "EditPromotions";

    var strErrorMsg = "";

    if (txtUpdatePromotionsName == "") { strErrorMsg += "<li>请上传活动图片</li>"; }
    if (txtPromotionsTitle == "") { strErrorMsg += "<li>请填写优惠活动标题</li>"; }
    if (txtShopList == "") { strErrorMsg += "<li>请选择活动商家</li>"; }
    if (!radPromotionsYes && (txtPromotionsStartTime == "" || txtPromotionsEndTime == "")) { strErrorMsg += "<li>请选择正确的优惠活动日期</li>"; }

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
        content: "将" + (type == "AddPromotions" ? "新增" : "修改") + "优惠活动。\n确定操作吗？",
        lock: true,
        ok: function () {
            doAjax("../", type, {
                "ShopList": txtShopList,
                "PromotionsID": txtPromotionsID,
                "PromotionsTitle": txtPromotionsTitle,
                "PromotionsStart": txtPromotionsStartTime,
                "PromotionsEnd": txtPromotionsEndTime,
                "PromotionsMemLevel": sltPromotionsLevel,
                "PromotionsPhoto": txtUpdatePromotionsName,
                "PromotionsRemark": txtPromotionsRemark,
                "PromotionsDesc": txtPromotionsDesc,
                "PromotionsType": radPromotionsYes ? 0 : 1
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
                        close: function () { location.href= "../MicroWebsite/Promotions.aspx?PID=124"},
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
function btnPromotionsDel(PromotionsID, PromotionsTitle) {
    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除优惠活动【' + PromotionsTitle + '】吗? 此操作不可恢复',
        ok: function () {
            this.lock();
            doAjax("../", "DelPromotions", { "PromotionsID": PromotionsID }, "text", function (text) {
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
function btnPromotionsEdit(PromotionsID, PromotionsTitle, PromotionsStart, PromotionsEnd, PromotionsMemLevel, PromotionsType) {
    $("#txtType").val(PromotionsID);
    $("#txtPromotionsTitle").val(PromotionsTitle);
    if (PromotionsType == 0) {
        $("#radPromotionsYes").attr("checked", true);
        $("#txtPromotionsStartTime").val("")
        $("#txtPromotionsEndTime").val("")
    } else {
        $("#radPromotionsNo").attr("checked", true);
        $("#txtPromotionsStartTime").val(PromotionsStart.substring(0, PromotionsStart.length - 8));
        $("#txtPromotionsEndTime").val(PromotionsEnd.substring(0, PromotionsEnd.length - 8));
    }
    $("#sltPromotionsLevel").val(PromotionsMemLevel);

    artDialog({
        title: '编辑活动',
        content: document.getElementById('PromotionsInfo'),
        id: 'PromotionsInfo',
        lock: true
    })
}