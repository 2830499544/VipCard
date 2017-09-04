$(function () {

    //保存
    $("#btnProductSave").bind("click", btnProductSaveClick);
    $("#btnProductReset").bind("click", btnProductResetClick);
    $("#ProductPhoto_Uploadify").uploadify({
        'uploader': "../images/swf/uploadify.swf",
        'script': "../Service/MicroWebsiteUpload.ashx",
        'cancelImg': "../images/member/cancel.png",
        'folder': "../Upload/MicroWebsite/MicroWebsiteProductCenter",
        'queueID': 'productCenter_fileQueue',
        'buttonImg': "../images/member/selectImg.jpg",
        'height': 25,
        'width': 70,
        'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
        'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
        'fileDataName': "MicroWebsiteProductCenter",
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
                $("#txtUpdateProductName").val(response);
                $("#imgProductPhoto").attr("src", "../Upload/MicroWebsite/MicroWebsiteProductCenter/" + response + "?" + GetGuid());
            }
        },
        'onSelect': function (event, queueID, fileObj) {
            $("#ProductPhoto_Uploadify").uploadifySettings('scriptData', { 'name': $("#MerchantPhoto").val() });
        }
    });
})


function btnProductResetClick() {
   
    $("#txtProductName").val("");

    $("#txtProductRemark").val("");
    $("#txtProductDesc").val("");
  
   
   
    }

function btnProductSaveClick() {

    var strErrorMsg = "";
    var txtProductName = $.trim($("#txtProductName").val());
    var txtProductID = $("#txtProductID").val();
    var ClassID = $("#sltClassID").val();
    var txtProductRemark = $.trim($("#txtProductRemark").val());
    var txtProductDesc = $.trim($("#txtProductDesc").val());
    var txtUpdateProductName = $("#txtUpdateProductName").val()
    var type = txtProductID == "" ? "AddProduct" : "EditProduct";

    if (txtProductName == "") { strErrorMsg = "产品名称不能为空,请输入产品名称！"; }
    if (ClassID == "") { strErrorMsg = "产品类别不能为空！"; }
    if (txtUpdateProductName == "") { strErrorMsg = "产品图片不能为空,请上传产品图片！"; }
    if (strErrorMsg != "") {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }

    art.dialog({
        content: "将" + (type == "AddProduct" ? "增加" : "编辑") + "产品 [" + txtProductName + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            doAjax("../", type,
                    {
                        "ProductName": txtProductName,
                        "ProductPhoto": txtUpdateProductName,
                        "ProductDesc": txtProductDesc,
                        "ProductRemark":txtProductRemark,
                        "ProductID": txtProductID,
                        "ClassID": ClassID
                    }
                    , "text", function (text) {
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
                                    content: '保存成功！',
                                    close: function () { location.href = "../MicroWebsite/ProductCenter.aspx?PID=122"; },
                                    lock: true
                                });
                        }
                    })
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    })
}

