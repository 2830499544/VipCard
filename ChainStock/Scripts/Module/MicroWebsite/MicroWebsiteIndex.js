function ShowPic(path) {
    if (path != "") {
        var image = "<img src='" + path + "' width=\"379\" height=\"300\" />";
    }
    else {
        var image = "<img src='../images/Gift/nogift.jpg' width=\"379\" height=\"300\" />";
    }
    art.dialog({
        padding: 0,
        title: '照片',
        content: image,
        lock: true,
        width: 300,
        height: 300
    });
}

function RandomChar(len) {
    var char = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefjhijklmnopqrstuvwxyz";
    var tmp = "";
    for (var i = 0; i < len; i++) {
        tmp += char.charAt(Math.ceil(Math.random() * (char.length - 1)));
    }
    return tmp;
}

function btnModuleEdit(MerchantID, MerchantDesc, MerchantPhoto) {
    art.dialog({
        lock: true,
        title: '模块编辑',
        content: document.getElementById('DiveModuleInfo'),
        id: 'DiveModuleInfo'
    });

    var imageSize;
    switch (MerchantID) {
        case "1": //关于我们
            imageSize = "158*138";
            break;
        case "2": //优惠活动
            imageSize = "158*145";
            break;
        case "3": //意见反馈
            imageSize = "158*97";
            break;
        case "4": //产品中心
            imageSize = "158*66";
            break;
        case "5": //门店查询
            imageSize = "158*159";
            break;
        case "6": //形象展示
            imageSize = "158*155";
            break;
    }
    $("#imageSizeDesc").html("图片支持大小为" + imageSize);
    $("#txtMerchantDesc").val(MerchantDesc);
    $("#txtMerchantRemark").val($("#MerchantRemark" + MerchantID).val());
    $("#imgModulePhoto").attr("src", MerchantPhoto + "?vi=" + RandomChar(4));
    $("#MerchantPhoto").val(MerchantPhoto);
    $("#MerchantID").val(MerchantID);
}

$(function () {
    $("#ModulePhoto_Uploadify").uploadify({
        'uploader': "../images/swf/uploadify.swf",
        'script': "../Service/MicroWebsiteUpload.ashx",
        'cancelImg': "../images/member/cancel.png",
        'folder': "../Upload/MicroWebsite/MicroWebsiteIndex",
        'queueID': 'module_fileQueue',
        'buttonImg': "../images/member/selectImg.jpg",
        'height': 25,
        'width': 70,
        'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
        'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
        'fileDataName': "MicroWebsiteIndex",
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
            if (response == "1") {
                $("#imgModulePhoto").attr("src", $("#imgModulePhoto").attr("src") + "?" + GetGuid());
            }
        },
        'onSelect': function (event, queueID, fileObj) {
            $("#ModulePhoto_Uploadify").uploadifySettings('scriptData', { 'name': $("#MerchantPhoto").val() });
        }
    });

    $("#btnModuleSave").bind("click", btnModuleSaveClick);
})

function btnModuleSaveClick() {
    var txtMerchantDesc = $("#txtMerchantDesc").val();
    var txtMerchantRemark = $("#txtMerchantRemark").val();
    var MerchantID = $("#MerchantID").val();

    art.dialog({
        content: '将编辑模块名称 [' + txtMerchantDesc + ']。确定操作吗?',
        lock: true,
        ok: function () {
            this.close();
            doAjax("../", "EditModule",
                    {
                        "MerchantID": MerchantID,
                        "MerchantDesc": txtMerchantDesc,
                        "MerchantRemark": txtMerchantRemark
                    },
                    "json", function (json) {
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
                            case 1: default:
                                art.dialog
                                ({
                                    title: '系统提示',
                                    time: 0.5,
                                    content: '保存成功！',
                                    close: function () { location.href = window.location; },
                                    lock: true
                                });
                                break;
                        }
                    });
            return false;
        },
        cancelVal: '取消',
        cancel: true
    })

}