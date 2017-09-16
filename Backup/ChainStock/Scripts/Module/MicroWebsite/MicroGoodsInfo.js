$(document).ready(function () {
    //商品编码获取焦点
    $("#txtGoodsName").focus();
    if ($("#txtGoodsID").val() == "") {
        $('#txtGoodsCode').val($("#txtCode").val());
    }
    $("#chkAutoGoodsCode").attr("checked", true);
    //确定按钮响应事件
    $("#btnGoodsSave").bind("click", BtnGoodsSave);

    //    //重置按钮响应事件
    //    $("#btnGoodsReset").bind("click", BtnGoodsReset);

    //自动创建商品编码响应事件
    $("#chkAutoGoodsCode").bind("click", AutoGoodsCode);
    //根据商品名称自动生成商品简码
//    $("#txtGoodsName").keyup(function () {
//        $("#txtGoodsNameCode").val(getPYCode($(this).val()));
//    });
    if ($("#txtGoodsID").val() != "") {
        $("#lblAutoGoodsCode").css("display", "none");
    }
    else {
        $("#lblAutoGoodsCode").css("display", "");
    }

    // 图片上传处理

    $("#GoodsPhoto_Uploadify").uploadify({
        'uploader': "../images/swf/uploadify.swf",
        'script': "../Service/MicroWebsiteUpload.ashx",
        'cancelImg': "../images/member/cancel.png",
        'folder': "../Upload/MicroWebsite/MicroGoods",
        'queueID': 'Goods_fileQueue',
        'buttonImg': "../images/member/selectImg.jpg",
        'height': 25,
        'width': 70,
        'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
        'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
        'fileDataName': "MicroGoods",
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
                $("#txtUpdateGoodsName").val(response);
                $("#imgGoodsPhoto").attr("src", "../Upload/MicroWebsite/MicroGoods/" + response + "?" + GetGuid());
            }
        },
        'onSelect': function (event, queueID, fileObj) {
            $("#GoodsPhoto_Uploadify").uploadifySettings('scriptData', { 'name': $("#MerchantPhoto").val() });
        }
    });

})

//自动编码
function AutoGoodsCode() {
    if ($("#chkAutoGoodsCode").attr("checked")) {
        //        $('#txtGoodsCode').css("background-color", "#eee");
        //        $('#txtGoodsCode').attr("readonly", true);
        $('#txtGoodsCode').val($("#txtCode").val());
    }
    else {
        $('#txtGoodsCode').css("background-color", "#fff");
        $('#txtGoodsCode').attr("readonly", false);
        $('#txtGoodsCode').val("");
        $("#txtGoodsCode").focus();
    }
}

function BtnGoodsSave() {
    var strErrorMsg = "";
    var strGoodCode = $.trim($("#txtGoodsCode").val());
    if (strGoodCode == "") {
        strErrorMsg += "<li>必须输入商品编码;</li>";
    }
    else {
        if (!/\d+/.test(strGoodCode)) {
            strErrorMsg += "<li>商品编码是由数字组成的一个字符串;</li>";
        }
    }
    if (strGoodCode.length < 5) {
        strErrorMsg += "<li>商品编码最小长度为5位最长为25位;</li>";
    }
    var strGoodsName = $.trim($("#txtGoodsName").val());
    if (strGoodsName.IsEmpty()) {
        strErrorMsg += "<li>必须输入商品名称;</li>";
    }
    if ($("#sltGoodsClass").val() == "") {
        strErrorMsg += "<li>必须选择商品类别;</li>";
    }
    var decGoodsPrice = $.trim($("#txtMicroPrice").val());
    if (decGoodsPrice.IsEmpty()) {
        strErrorMsg += "<li>必须输入商品零售金额;</li>";
    }
    var decSalePrice = $.trim($("#txtMicroSalePrice").val());
    if (decGoodsPrice.IsEmpty()) {
        strErrorMsg += "<li>必须输入商品原价;</li>";
    }
    var intPoint = $.trim($("#txtGoodsPoint").val());
    if (!intPoint.IsEmpty() && !intPoint.IsNumber()) {
        strErrorMsg += "<li>积分数量只能为数字;</li>";
    }
    if (intPoint < 0) {
        strErrorMsg += "<li>积分数量必须大于0;</li>";
    }
    var pic = $("#txtUpdateGoodsName").val();
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
        content: "将" + ($('#txtGoodsID').val() == "" ? "增加" : "编辑") + "商品 [" + $.trim($("#txtGoodsName").val()) + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            doAjax(
        "../",
        "MicroGoodsAddAndEdit",
        $("#frmGoodsInfo").serialize(),
        "json",
         function (json) {
             switch (json) {
                 case -1:
                     art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: ("此商品编码已经在系统中存在，请重新输入商品编码，然后重试！"),
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
                                close: function () {
                                    if ($('#txtGoodsID').val() == "") {
                                        location.href = "MicroGoodsInfo.aspx?PID=129";
                                    }
                                    else {
                                        location.href = "MicroGoodsList.aspx?PID=128";
                                    }
                                },
                                lock: true
                            });
                     break;
             }
         });
        },
        cancelVal: '取消',
        cancel: true
    });

}

//function BtnGoodsReset() {
//    var strGoodsID = $('#txtGoodsID').val();
//    if (strGoodsID != "") {
//        doAjax(
//        "../",
//        "GetMicroGoodsInfo",
//        { "goodsID": $("#txtGoodsID").val() },
//        "json",
//        function (json) {
//            $("#txtGoodsID").val(json[0].MicroGoodsID);
//            $("#txtCode").val(json[0].MicroGoodsCode);
//            $("#txtGoodsCode").val(json[0].MicroGoodsCode);
//            $("#txtGoodsName").val(json[0].MicroGoodsName);
//            $("#sltGoodsClass").val(json[0].MicroGoodsClassID);
//            $("#txtMicroPrice").val(json[0].MicroPrice);
//            $("#txtMicroSalePrice").val(json[0].MicroSalePrice);
//            $("#txtGoodsBidPrice").val(json[0].MicroGoodsBidPrice);
//            $("#txtGoodsPoint").val(json[0].MicroPoint);
//            $("#txtGoodsRemark").val(json[0].GoodsRemark);
//        });
//    }
//    else {
//        $("#txtGoodsID").val("");
//        $("#txtCode").val("");
//        $("#txtGoodsCode").val("");
//        $("#txtGoodsName").val("");
//        $("#sltGoodsClass").val("");
//        $("#txtMicroPrice").val("0");
//        $("#txtMicroSalePrice").val("0");
//        $("#txtGoodsBidPrice").val("0");
//        $("#txtGoodsPoint").val("0");
//        $("#txtGoodsRemark").val("");
//    }
//}