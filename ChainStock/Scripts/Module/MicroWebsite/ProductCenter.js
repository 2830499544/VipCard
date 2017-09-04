$(function () {



    //保存
    $("#btnProductSave").bind("click", btnProductSaveClick);

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

//新增
function btnProductAddClick() {
    $("#txtProductID,#txtUpdateProductName,#txtProductName,#txtProductRemark").val("");
    $("#imgProductPhoto").attr("src", "../images/Gift/nogift.jpg");

    art.dialog({
        lock: true,
        title: '产品新增',
        width: '300',
        content: document.getElementById('DiveProductInfo'),
        id: 'DiveProductInfo',
        close: function () { }
    });
}

//编辑
function btnProductEdit(ProductID) {
    window.location.href = "../MicroWebsite/ProductCenterInfo.aspx?ProductID=" + ProductID;
}

function btnProductSaveClick() {
    var strErrorMsg = "";
    var txtProductName = $.trim($("#txtProductName").val());
    var txtProductID = $("#txtProductID").val();
    var txtProductRemark = $.trim($("#txtProductRemark").val());
    var txtUpdateProductName = $("#txtUpdateProductName").val()
    var type = txtProductID == "" ? "AddProduct" : "EditProduct";
    if (txtProductName == "") { strErrorMsg = "产品名称不能为空,请输入产品名称！"; }
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
                        "ProductDesc": txtProductRemark,
                        "ProductID": txtProductID
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
                                    close: function () { window.location = window.location; },
                                    lock: true
                                });
                        }
                    })
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    })
}

function ShowPic(path) {
    if (path != "") {
        var image = "<img src='" + path + "' width=\"300\" height=\"300\" />";
    }
    else {
        var image = "<img src='../images/Gift/nogift.jpg' width=\"379\" height=\"500\" />";
    }
    art.dialog({
        padding: 0,
        title: '产品',
        content: image,
        lock: true,
        width: 300,
        height: 300
    });
}

function btnProductDel(ProductID, ProductName) {
    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除产品【' + ProductName + '】吗? 此操作不可恢复',
        ok: function () {
            this.lock();
            doAjax("../", "DelProduct", { "ProductID": ProductID }, "text", function (text) {
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