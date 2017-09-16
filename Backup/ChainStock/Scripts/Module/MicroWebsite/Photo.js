$(function () {
    //BindNullList("gvwPhotoShow");

    //新增产品
    $("#btnPhotoAdd").bind("click", btnPhotoShowAddClick);
    //保存
    $("#btnPhotoSave").bind("click", btnPhotoSaveClick);

    $("#PhotoPhoto_Uploadify").uploadify({
        'uploader': "../images/swf/uploadify.swf",
        'script': "../Service/MicroWebsiteUpload.ashx",
        'cancelImg': "../images/member/cancel.png",
        'folder': "../Upload/MicroWebsite/MicroWebsitePhoto",
        'queueID': 'Photo_fileQueue',
        'buttonImg': "../images/member/selectImg.jpg",
        'height': 25,
        'width': 70,
        'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
        'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
        'fileDataName': "MicroWebsitePhoto",
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
                $("#txtUpdatePhotoName").val(response);
                $("#imgPhotoPhoto").attr("src", "../Upload/MicroWebsite/MicroWebsitePhoto/" + response + "?" + GetGuid());
            }
        },
        'onSelect': function (event, queueID, fileObj) {
            $("#PhotoPhoto_Uploadify").uploadifySettings('scriptData', { 'name': $("#MerchantPhoto").val() });
        }
    });
})

//新增
function btnPhotoShowAddClick() {
    $("#txtPhotoID,#txtUpdatePhotoName,#txtPhotoName,#txtPhotoRemark").val("");
    $("#imgPhotoPhoto").attr("src", "../images/Gift/nogift.jpg");

    art.dialog({
        lock: true,
        title: '照片新增',
        width: '300',
        content: document.getElementById('DivePhotoInfo'),
        id: 'DivePhotoInfo',
        close: function () { }
    });
}

//编辑
function btnPhotoEdit(PhotoID, PhotoTitle, PhotoPhoto, PhotoDesc, AlbumID) {
    $("#txtPhotoID").val(PhotoID);
    $("#txtUpdatePhotoName").val(PhotoPhoto);
    $("#txtPhotoName").val(PhotoTitle);
    $("#txtPhotoRemark").val(PhotoDesc);
    $("#imgPhotoPhoto").attr("src", PhotoPhoto);
    $("#sltAlbumID").val(AlbumID);
    art.dialog({
        lock: true,
        title: '照片编辑',
        width: '300',
        content: document.getElementById('DivePhotoInfo'),
        id: 'DivePhotoInfo',
        close: function () { }
    });
}
//保存
function btnPhotoSaveClick() {
    var strErrorMsg = "";
    var txtPhotoTitle = $.trim($("#txtPhotoName").val());
    var txtPhotoID = $("#txtPhotoID").val();
    var sltAlbumID = $("#sltAlbumID").val();
    var txtPhotoRemark = $.trim($("#txtPhotoRemark").val());
    var txtUpdatePhotoName = $("#txtUpdatePhotoName").val()
    var type = txtPhotoID == "" ? "AddPhoto" : "EditPhoto";
    if (txtPhotoTitle == "") { strErrorMsg = "照片标题不能为空,请输入照片标题！"; }
    if (sltAlbumID == "") { strErrorMsg = "请选择所属相册！"; }
    if (txtUpdatePhotoName == "") { strErrorMsg = "照片不能为空！"; }
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
        content: "将" + (type == "AddPhoto" ? "增加" : "编辑") + "照片 [" + txtPhotoTitle + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            doAjax("../", type,
                    {
                        "PhotoTitle": txtPhotoTitle,
                        "PhotoPhoto": txtUpdatePhotoName,
                        "PhotoDesc": txtPhotoRemark,
                        "PhotoID": txtPhotoID,
                        "AlbumID": sltAlbumID
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
        var image = "<img src='" + path + "' width=\"500\" height=\"500\" />";
    }
    else {
        var image = "<img src='../images/Gift/nogift.jpg' width=\"379\" height=\"500\" />";
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

function btnPhotoDel(PhotoID, PhotoTitle) {
    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除照片【' + PhotoTitle + '】吗? 此操作不可恢复',
        ok: function () {
            this.lock();
            doAjax("../", "DelPhoto", { "PhotoID": PhotoID }, "text", function (text) {
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