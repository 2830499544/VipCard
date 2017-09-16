$(document).ready(function () {
    $("[class=parent]").click(function () {
        $(this).parent().parent().next().css("display") == "none" ? $(this).parent().parent().next().css("display", "") : $(this).parent().parent().next().css("display", "none");
        $(this).parent().parent().next().css("display") == "none" ? $(this).prev("div").css("background", "url(../Inc/Style/images/plus.gif) no-repeat") : $(this).prev("div").css("background", "url(../Inc/Style/images/minus.gif) no-repeat");
    })
    $("[class=parent]").mouseover(function () {
        $(this).css({ "textDecoration": "underline", "color": "#FF00FE" });
    }).mouseout(function () {
        $(this).css({ "textDecoration": "none", "color": "#AF0081" });
    })

    //文本消息保存按钮点击事件
    $("#btnRotateSave").bind("click", btnRotateSave);
    //文本消息重置按钮点击事件
    $("#btnTextRuleReset").bind("click", btnTextRuleResetClick);

    $("#RotatePhoto_Uploadify").uploadify({
        'uploader': "../images/swf/uploadify.swf",
        'script': "../Service/MicroWebsiteUpload.ashx",
        'cancelImg': "../images/member/cancel.png",
        'folder': "../Upload/MicroWebsite/Rotate",
        'queueID': 'Rotate_fileQueue',
        'buttonImg': "../images/member/selectImg.jpg",
        'height': 25,
        'width': 70,
        'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
        'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
        'fileDataName': "Rotate",
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
                $("#txtRotatePhoto").val("../Upload/MicroWebsite/Rotate/" + response);            
                $("#imgRotatePhoto").attr("src", "../Upload/MicroWebsite/Rotate/" + response + "?" + GetGuid());
            }
        },
        'onSelect': function (event, queueID, fileObj) {
            $("#RotatePhoto_Uploadify").uploadifySettings('scriptData', { 'name': $("#MerchantPhoto").val() });
        }
    });
})



//文本消息保存按钮点击事件 响应函数
function btnRotateSave() {
    var type = $("#txtRotateID").val() == "" ? "SysRotateAdd" : "SysRotateEdit";
    var txtRotateName = $.trim($("#txtRotateName").val());
    var txtRotateRemark = $.trim($("#txtRotateRemark").val());
    var txtStartTime = $.trim($("#txtStartTime").val());
    var txtEndTime = $.trim($("#txtEndTime").val());
//    var txtRotateCount = $.trim($("#txtRotateCount").val());
//    var txtPersonTotalCount = $.trim($("#txtPersonTotalCount").val());
//    var txtPersonDayCount = $.trim($("#txtPersonDayCount").val());
    var txtOnePrizeName = $.trim($("#txtOnePrizeName").val());
    var txtOnePrizeCount = $.trim($("#txtOnePrizeCount").val());
    var txtTwoPrizeName = $.trim($("#txtTwoPrizeName").val());
    var txtTwoPrizeCount = $.trim($("#txtTwoPrizeCount").val());
    var txtThreePrizeName = $.trim($("#txtThreePrizeName").val());
    var txtThreePrizeCount = $.trim($("#txtThreePrizeCount").val());
    var txtFourPrizeName = $.trim($("#txtFourPrizeName").val());
    var txtFourPrizeCount = $.trim($("#txtFourPrizeCount").val());
    var txtFivePrizeName = $.trim($("#txtFivePrizeName").val());
    var txtFivePrizeCount = $.trim($("#txtFivePrizeCount").val());
    var txtSixPrizeName = $.trim($("#txtSixPrizeName").val());
    var txtSixPrizeCount = $.trim($("#txtSixPrizeCount").val());
    var txtOneRate = $.trim($("#txtOneRate").val());
    var txtTwoRate = $.trim($("#txtTwoRate").val());
    var txtThreeRate = $.trim($("#txtThreeRate").val());
    var txtFourRate = $.trim($("#txtFourRate").val());
    var txtFiveRate = $.trim($("#txtFiveRate").val());
    var txtSixRate = $.trim($("#txtSixRate").val());
    var txtRotateRegion = $.trim($("#txtRotateRegion").val());
    var strErrorMsg = "";

    if (txtRotateName == "") { strErrorMsg += "<li>活动名称不能为空;</li>"; }
    if (txtRotateRemark == "") { strErrorMsg += "<li>活动说明不能为空;</li>"; }
    if (txtStartTime == "") { strErrorMsg += "<li>活动开始时间不能为空;</li>"; }
    if (txtEndTime == "") { strErrorMsg += "<li>活动结束时间不能为空;</li>"; }
    if (Date.parse(txtEndTime) < Date.parse(txtStartTime)) {
        strErrorMsg += "<li>活动结束时间不能小于开始时间;</li>";
    }
    if (txtRotateRegion == "") { strErrorMsg += "<li>活动指令不能为空;</li>"; }
    
//    if (txtRotateCount == "") { strErrorMsg += "<li>活动总人数不能为空;</li>"; }
//    if (txtPersonTotalCount == "") { strErrorMsg += "<li>抽奖总次数不能为空;</li>"; }
//    if (txtPersonDayCount == "") { strErrorMsg += "<li>每天抽奖次数不能为空;</li>"; }
    if (txtOnePrizeName == "") { strErrorMsg += "<li>一等奖奖品名称不能为空;</li>"; }
    if (txtOnePrizeCount == "") { strErrorMsg += "<li>一等奖奖品数量不能为空;</li>"; }
    if (txtTwoPrizeName == "") { strErrorMsg += "<li>二等奖奖品名称不能为空;</li>"; }
    if (txtTwoPrizeCount == "") { strErrorMsg += "<li>二等奖奖品数量不能为空;</li>"; }
    if (txtThreePrizeName == "") { strErrorMsg += "<li>三等奖奖品名称不能为空;</li>"; }
    if (txtThreePrizeCount == "") { strErrorMsg += "<li>三等奖奖品数量不能为空;</li>"; }
    if (txtFourPrizeName == "") { strErrorMsg += "<li>四等奖奖品名称不能为空;</li>"; }
    if (txtFourPrizeCount == "") { strErrorMsg += "<li>四等奖奖品数量不能为空;</li>"; }
    if (txtFivePrizeName == "") { strErrorMsg += "<li>五等奖奖品名称不能为空;</li>"; }
    if (txtFivePrizeCount == "") { strErrorMsg += "<li>五等奖奖品数量不能为空;</li>"; }
    if (txtSixPrizeName == "") { strErrorMsg += "<li>六等奖奖品名称不能为空;</li>"; }
    if (txtSixPrizeCount == "") { strErrorMsg += "<li>六等奖奖品数量不能为空;</li>"; }
    if (txtOneRate == "") { strErrorMsg += "<li>一等中奖几率不能为空;</li>"; }
    if (txtTwoRate == "") { strErrorMsg += "<li>二等奖中奖几率不能为空;</li>"; }
    if (txtThreeRate == "") { strErrorMsg += "<li>三等奖中奖几率不能为空;</li>"; }
    if (txtFourRate == "") { strErrorMsg += "<li>四等奖中奖几率不能为空;</li>"; }
    if (txtFiveRate == "") { strErrorMsg += "<li>五等奖中奖几率不能为空;</li>"; }
    if (txtSixRate == "") { strErrorMsg += "<li>六等奖中奖几率不能为空;</li>"; }
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

//    art.dialog({
//        content: "将要" + (type == "SysRotateAdd" ? "添加" : "编辑") + "幸运大转盘活动吗。\n确定操作吗？",
//        lock: true,
//        ok: function () {

//            doAjax("../", type, {
//                "RotateName": txtRotateName,
//                "RotateRemark": txtRotateRemark,
//                "StartTime": txtStartTime,
//                "EndTime": txtEndTime,
//                "RotateCount": txtRotateCount,
//                "PersonTotalCount": txtPersonTotalCount,
//                "PersonDayCount": txtPersonDayCount,
//                "OnePrizeName": txtOnePrizeName,
//                "OnePrizeCount": txtOnePrizeCount,
//                "TwoPrizeName": txtTwoPrizeName,
//                "TwoPrizeCount": txtTwoPrizeCount,
//                "ThreePrizeName": txtThreePrizeName,
//                "ThreePrizeCount": txtThreePrizeCount,
//                "FourPrizeName": txtFourPrizeName,
//                "FourPrizeCount": txtFourPrizeCount,
//                "FivePrizeName": txtFivePrizeName,
//                "FivePrizeCount": txtFivePrizeCount,
//                "SixPrizeName": txtSixPrizeName,
//                "SixPrizeCount": txtSixPrizeCount,
//                "OneRate": txtOneRate,
//                "TwoRate": txtTwoRate,
//                "ThreeRate": txtThreeRate,
//                "FourRate": txtFourRate,
//                "FiveRate": txtFiveRate,
//                "SixRate": txtSixRate,
//                "RotateID": $.trim($("#txtRotateID").val())
//               
//            }, "json", function (json) {
//                alert("");
//                if (json == "0") {
//                    art.dialog({
//                        title: '系统提示',
//                        content: ("系统异常，未保存数据，请再次点击保存！"),
//                        lock: true
//                    })
//                }
//                else if (json == "-2") {
//                    art.dialog({
//                        title: '系统提示',
//                        time: 1.5,
//                        content: ("该时间段有正在进行的大转盘活动，请修改活动时间再保存！"),
//                        lock: true
//                    })
//                }
//                else if (json == "-1") {
//                    art.dialog({
//                        title: '系统提示',
//                        time: 1.5,
//                        content: ("系统错误！"),
//                        lock: true
//                    })
//                }
//                else {
//                    art.dialog({
//                        title: '系统提示',
//                        time: 1.5,
//                        content: '保存成功！',
//                        close: function () { location.href = "SysRotateList.aspx?PID=144"; }
//                    })
//                }
//            })
//        },
//        cancelVal: '取消',
//        cancel: true
//    })
}

//文本消息重置按钮点击事件  响应函数
function btnTextRuleResetClick() {
    var number = parseInt($("#txtNumber").val());
    if (!isNaN(number)) {
        if (number < 0) {
            $("#txtSendContent").val("");
        }
    }
    else {
        $("#txtNumber,#txtDescRule,#txtSendContent").val("");
    }
}


