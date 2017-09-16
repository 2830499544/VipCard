$(function () {



    //保存
    $("#btnNewsSave").bind("click", btnNewsSaveClick);

    $("#NewsPhoto_Uploadify").uploadify({
        'uploader': "../images/swf/uploadify.swf",
        'script': "../Service/MicroWebsiteUpload.ashx",
        'cancelImg': "../images/member/cancel.png",
        'folder': "../Upload/MicroWebsite/MicroWebsiteNews",
        'queueID': 'productCenter_fileQueue',
        'buttonImg': "../images/member/selectImg.jpg",
        'height': 25,
        'width': 70,
        'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
        'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
        'fileDataName': "MicroWebsiteNews",
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
                $("#txtUpdateNewsName").val(response);
                $("#imgNewsPhoto").attr("src", "../Upload/MicroWebsite/MicroWebsiteNews/" + response + "?" + GetGuid());
            }
        },
        'onSelect': function (event, queueID, fileObj) {
            $("#NewsPhoto_Uploadify").uploadifySettings('scriptData', { 'name': $("#MerchantPhoto").val() });
        }
    });
})

//新增
function btnNewsAddClick() {
    $("#txtNewsID,#txtUpdateNewsName,#txtNewsName,#txtNewsRemark").val("");
    $("#imgNewsPhoto").attr("src", "../images/Gift/nogift.jpg");

    art.dialog({
        lock: true,
        title: '动态新增',
        width: '300',
        content: document.getElementById('DiveNewsInfo'),
        id: 'DiveNewsInfo',
        close: function () { }
    });
}

//编辑
function btnNewsEdit(NewsID) {
    window.location.href = "../MicroWebsite/NewsInfo.aspx?NewsID=" + NewsID;
}


function ShowPic(path) {
    if (path != "") {
        var image = "<img src='" + path + "' width=\"640\" height=\"320\" />";
    }
    else {
        var image = "<img src='../images/Gift/nogift.jpg' width=\"379\" height=\"500\" />";
    }
    art.dialog({
        padding: 0,
        title: '动态',
        content: image,
        lock: true,
        width: 300,
        height: 300
    });
}

function btnNewsDel(NewsID, NewsName) {
    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除动态【' + NewsName + '】吗? 此操作不可恢复',
        ok: function () {
            this.lock();
            doAjax("../", "DelNews", { "NewsID": NewsID }, "text", function (text) {
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