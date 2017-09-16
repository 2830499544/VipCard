$(function () {


    $("#BackgroundPhoto_Uploadify").uploadify({
        'uploader': "../images/swf/uploadify.swf",
        'script': "../Service/MicroWebsiteUpload.ashx",
        'cancelImg': "../images/member/cancel.png",
        'folder': "../Upload/MicroWebsite/MicroWebsiteBackground",
        'queueID': 'Background_fileQueue',
        'buttonImg': "../images/member/selectImg.jpg",
        'height': 25,
        'width': 70,
        'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
        'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
        'fileDataName': "MicroWebsiteBackground",
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
                $("#txtUpdateBackgroundName").val(response);
                $("#imgBackgroundPhoto").attr("src", "../Upload/WeiXin/Images/" + response + "?" + GetGuid());
            }
        },
        'onSelect': function (event, queueID, fileObj) {
            $("#BackgroundPhoto_Uploadify").uploadifySettings('scriptData', { 'name': $("#MerchantPhoto").val() });
        }
    });
})

