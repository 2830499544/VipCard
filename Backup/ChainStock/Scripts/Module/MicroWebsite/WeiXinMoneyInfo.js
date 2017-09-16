$(function () {

    $("#txtMoneyTitle").bind("blur", GetMoneyTitle);
    $("#txtMoneyDesc").bind("blur", GetMoneyDesc);
    $("#txtMoneyWish").bind("blur", GetMoneyWish);
    $("#btnMoneySave").bind("click", btnMoneySaveClick);
    $("#btntest").bind("click", btntest);
    
  //  $("#radMoneyTypeOne").attr("checked", "checked");
    $("[name='radMoneyType']").bind("change", BindTxtMoney);

    BindTxtMoney();
    $("#radStartTypeOne").attr("checked", "checked");
    $("[name='radStartType']").bind("change", BindStartType);
    BindStartType();

    $("#MoneyPhoto_Uploadify").uploadify({
        'uploader': "../images/swf/uploadify.swf",
        'script': "../Service/MicroWebsiteUpload.ashx",
        'cancelImg': "../images/member/cancel.png",
        'folder': "../Upload/MicroWebsite/WeiXinMoney",
        'queueID': 'Money_fileQueue',
        'buttonImg': "../images/member/selectImg.jpg",
        'height': 25,
        'width': 70,
        'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
        'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
        'fileDataName': "WeiXinMoney",
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
                $("#txtMoneyPhoto").val("../Upload/MicroWebsite/WeiXinMoney/" + response);
                $("#imgMoneyPhoto").attr("src", "../Upload/MicroWebsite/WeiXinMoney/" + response + "?" + GetGuid());
            }
        },
        'onSelect': function (event, queueID, fileObj) {
            $("#MoneyPhoto_Uploadify").uploadifySettings('scriptData', { 'name': $("#MerchantPhoto").val() });
        }
    });
})
function BindStartType() {

    var type = $("input[name='radStartType']:checked").val();

    if (type == "1") {
        $("#txtStartTime").attr("style", "display:none;");
      
    }
    else {
        $("#txtStartTime").attr("style", "display:block;");
       
    }
}
function BindTxtMoney() {


    var type = $("input[name='radMoneyType']:checked").val();
   
    if (type == "1") {
        $("#trMoneyTypeOne").attr("style", "display:block;")
        $("#trMoneyTypeTwo").attr("style", "display:none;")
    }
    else {
        $("#trMoneyTypeOne").attr("style", "display:none;")
        $("#trMoneyTypeTwo").attr("style", "display:block;")
    }
}
function GetMoneyTitle() {
    
    $("#spMoneyTitle").html($("#txtMoneyTitle").val());
}
function GetMoneyDesc() {
    $("#spMoneyDesc").html($("#txtMoneyDesc").val());
}
function GetMoneyWish() {
    $("#spMoneyWish").html($("#txtMoneyWish").val());
}

function btntest() {

    location.href = "../MicroWebsite/WeiXinMoneyList.aspx?PID=140";
}
function btnMoneySaveClick() {
    var strErrorMsg = "";
    var type = ($("#txtMoneyID").val() == "") ? "AddWeiXinMoney" : "EditWeiXinMoney";
    var txtMoneyID = $("#txtMoneyID").val();
    var txtMoneyTitle = $.trim($("#txtMoneyTitle").val());
    var txtMoneyDesc = $("#txtMoneyDesc").val();
    var txtMoneyWish = $("#txtMoneyWish").val();
    var imgUrl = $("#imgMoneyPhoto").attr("src");
    var txtEndTime = $.trim($("#txtEndTime").val());
    var txtStartTime = $.trim($("#txtStartTime").val());
    var txtTotalMoney = $("#txtTotalMoney").val();
    var txtStartMoney = $("#txtStartMoney").val();
    var txtEndMoney = $("#txtEndMoney").val();
    var txtFixMoney = $("#txtFixMoney").val();
    var txtMaxCount = $("#txtMaxCount").val();
    var txtMoneyRate = $("#txtMoneyRate").val();
    var txtMemIDList = $("#txtMemIDList").val();
    var txtMoneyID = $("#txtMoneyID").val();
    var moneyType = $("input[name='radMoneyType']:checked").val();
    var startType = $("input[name='radStartType']:checked").val();
    var txtMoneyRegion = $.trim($("#txtMoneyRegion").val());
    if (txtMoneyTitle == "") { strErrorMsg = "红包活动名称不能为空！"; }
    if (txtMoneyDesc == "") { strErrorMsg = "红包活动说明不能为空！"; }
    if (txtMoneyWish == "") { strErrorMsg = "红包祝福语不能为空！"; }
    if (txtEndTime == "") { strErrorMsg = "结束时间不能为空！"; }
    if (txtTotalMoney == "") { strErrorMsg = "活动总预算不能为空！"; }
    if (moneyType == "1")//随机金额
    {
        if (txtStartMoney == "") { strErrorMsg = "红包随机起始金额不能为空！"; }
        if (txtEndMoney == "") { strErrorMsg = "红包随机结束金额不能为空！"; }
    }
    else {
        if (txtFixMoney == "") { strErrorMsg = "红包固定金额不能为空！"; }
    }
    
    if (txtMaxCount == "") { strErrorMsg = "最多领取个数不能为空！"; }
    if (txtMoneyRate == "") { strErrorMsg = "领取红包机率不能为空！"; }
    if (txtMemIDList == "") { strErrorMsg = "活动对象不能为空！"; }
    if (txtMoneyRegion == "") { strErrorMsg = "红包指令不能为空！"; }
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

    return true;


}
