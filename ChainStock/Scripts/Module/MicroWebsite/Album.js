$(function () {
    //BindNullList("gvwAlbumShow");

    //新增产品
    $("#btnAlbumAdd").bind("click", btnAlbumShowAddClick);
    //保存
    $("#btnAlbumSave").bind("click", btnAlbumSaveClick);

    $("#AlbumPhoto_Uploadify").uploadify({
        'uploader': "../images/swf/uploadify.swf",
        'script': "../Service/MicroWebsiteUpload.ashx",
        'cancelImg': "../images/member/cancel.png",
        'folder': "../Upload/MicroWebsite/MicroWebsiteAlbum",
        'queueID': 'Album_fileQueue',
        'buttonImg': "../images/member/selectImg.jpg",
        'height': 25,
        'width': 70,
        'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
        'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
        'fileDataName': "MicroWebsiteAlbum",
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
                $("#txtUpdateAlbumName").val(response);
                $("#imgAlbumPhoto").attr("src", "../Upload/MicroWebsite/MicroWebsiteAlbum/" + response + "?" + GetGuid());
            }
        },
        'onSelect': function (event, queueID, fileObj) {
            $("#AlbumPhoto_Uploadify").uploadifySettings('scriptData', { 'name': $("#MerchantPhoto").val() });
        }
    });
})

//新增
function btnAlbumShowAddClick() {
    $("#txtAlbumID,#txtUpdateAlbumName,#txtAlbumName,#txtAlbumRemark").val("");
    $("#imgAlbumPhoto").attr("src", "../images/Gift/nogift.jpg");

    art.dialog({
        lock: true,
        title: '相册新增',
        width: '300',
        content: document.getElementById('DiveAlbumInfo'),
        id: 'DiveAlbumInfo',
        close: function () { }
    });
}

//编辑
function btnAlbumEdit(AlbumID, AlbumTitle, AlbumPhoto, AlbumDesc) {
    $("#txtAlbumID").val(AlbumID);
    $("#txtUpdateAlbumName").val(AlbumPhoto);
    $("#txtAlbumName").val(AlbumTitle);
    $("#txtAlbumRemark").val(AlbumDesc);
    $("#imgAlbumPhoto").attr("src", AlbumPhoto);

    art.dialog({
        lock: true,
        title: '相册编辑',
        width: '300',
        content: document.getElementById('DiveAlbumInfo'),
        id: 'DiveAlbumInfo',
        close: function () { }
    });
}
//保存
function btnAlbumSaveClick() {

    var strErrorMsg = "";
    var txtAlbumTitle = $.trim($("#txtAlbumName").val());
    var txtAlbumID = $("#txtAlbumID").val();
    var txtAlbumRemark = $.trim($("#txtAlbumRemark").val());
    var txtUpdateAlbumName = $("#txtUpdateAlbumName").val()
    var type = txtAlbumID == "" ? "AddAlbum" : "EditAlbum";
    if (txtAlbumTitle == "") { strErrorMsg = "相册标题不能为空,请输入相册标题！"; }
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
        content: "将" + (type == "AddAlbum" ? "增加" : "编辑") + "相册 [" + txtAlbumTitle + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            doAjax("../", type,
                    {
                        "AlbumTitle": txtAlbumTitle,
                        "AlbumPhoto": txtUpdateAlbumName,
                        "AlbumDesc": txtAlbumRemark,
                        "AlbumID": txtAlbumID
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
        title: '相册',
        content: image,
        lock: true,
        width: 300,
        height: 300
    });
}

function btnAlbumDel(AlbumID, AlbumTitle) {
    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除相册【' + AlbumTitle + '】吗? 此操作不可恢复',
        ok: function () {
            this.lock();
            doAjax("../", "DelAlbum", { "AlbumID": AlbumID }, "text", function (text) {
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