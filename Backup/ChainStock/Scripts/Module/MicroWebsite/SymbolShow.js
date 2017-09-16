$(function () {
    //BindNullList("gvwSymbolShow");

    //新增产品
    $("#btnSymbolShowAdd").bind("click", btnSymbolShowAddClick);
    //保存
    $("#btnSymbolSave").bind("click", btnSymbolSaveClick);

    $("#SymbolPhoto_Uploadify").uploadify({
        'uploader': "../images/swf/uploadify.swf",
        'script': "../Service/MicroWebsiteUpload.ashx",
        'cancelImg': "../images/member/cancel.png",
        'folder': "../Upload/MicroWebsite/MicroWebsiteSymbol",
        'queueID': 'Symbol_fileQueue',
        'buttonImg': "../images/member/selectImg.jpg",
        'height': 25,
        'width': 70,
        'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
        'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
        'fileDataName': "MicroWebsiteSymbol",
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
                $("#txtUpdateSymbolName").val(response);
                $("#imgSymbolPhoto").attr("src", "../Upload/MicroWebsite/MicroWebsiteSymbol/" + response + "?" + GetGuid());
            }
        },
        'onSelect': function (event, queueID, fileObj) {
            $("#SymbolPhoto_Uploadify").uploadifySettings('scriptData', { 'name': $("#MerchantPhoto").val() });
        }
    });
})

//新增
function btnSymbolShowAddClick() {
    $("#txtSymbolID,#txtUpdateSymbolName,#txtSymbolName,#txtSymbolRemark").val("");
    $("#imgSymbolPhoto").attr("src", "../images/Gift/nogift.jpg");

    art.dialog({
        lock: true,
        title: '形象图片新增',
        width: '300',
        content: document.getElementById('DiveSymbolInfo'),
        id: 'DiveSymbolInfo',
        close: function () { }
    });
}

//编辑
function btnSymbolEdit(SymbolID, SymbolTitle, SymbolPhoto, SymbolDesc) {
    $("#txtSymbolID").val(SymbolID);
    $("#txtUpdateSymbolName").val(SymbolPhoto);
    $("#txtSymbolName").val(SymbolTitle);
    $("#txtSymbolRemark").val(SymbolDesc);
    $("#imgSymbolPhoto").attr("src", SymbolPhoto);

    art.dialog({
        lock: true,
        title: '形象编辑',
        width: '300',
        content: document.getElementById('DiveSymbolInfo'),
        id: 'DiveSymbolInfo',
        close: function () { }
    });
}
//保存
function btnSymbolSaveClick() {
    var strErrorMsg = "";
    var txtSymbolTitle = $.trim($("#txtSymbolName").val());
    var txtSymbolID = $("#txtSymbolID").val();
    var txtSymbolRemark = $.trim($("#txtSymbolRemark").val());
    var txtUpdateSymbolName = $("#txtUpdateSymbolName").val()
    var type = txtSymbolID == "" ? "AddSymbol" : "EditSymbol";
    if (txtSymbolTitle == "") { strErrorMsg = "形象标题不能为空,请输入形象标题！"; }
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
        content: "将" + (type == "AddSymbol" ? "增加" : "编辑") + "形象 [" + txtSymbolTitle + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            doAjax("../", type,
                    {
                        "SymbolTitle": txtSymbolTitle,
                        "SymbolPhoto": txtUpdateSymbolName,
                        "SymbolDesc": txtSymbolRemark,
                        "SymbolID": txtSymbolID
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
        var image = "<img src='" + path + "' width=\"379\" height=\"500\" />";
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

function btnSymbolDel(SymbolID, SymbolTitle) {
    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除形象【' + SymbolTitle + '】吗? 此操作不可恢复',
        ok: function () {
            this.lock();
            doAjax("../", "DelSymbol", { "SymbolID": SymbolID }, "text", function (text) {
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