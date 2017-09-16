$(function () {

    //保存
    $("#btnNewsSave").bind("click", btnNewsSaveClick);
    $("#btnNewsReset").bind("click", btnNewsResetClick);
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


function btnNewsResetClick() {
   
    $("#txtNewsName").val("");

    $("#txtNewsRemark").val("");
    $("#txtNewsDesc").val("");
  
   
   
    }

function btnNewsSaveClick() {

    var strErrorMsg = "";
    var txtNewsName = $.trim($("#txtNewsName").val());
    var txtNewsID = $("#txtNewsID").val();
    var txtNewsRemark = $.trim($("#txtNewsRemark").val());
    var txtNewsDesc = $.trim($("#txtNewsDesc").val());
    var txtUpdateNewsName = $("#txtUpdateNewsName").val()
    var type = txtNewsID == "" ? "AddNews" : "EditNews";

    if (txtNewsName == "") { strErrorMsg = "动态名称不能为空,请输入动态名称！"; }
    if (txtUpdateNewsName == "") { strErrorMsg = "动态图片不能为空,请上传动态图片！"; }
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
        content: "将" + (type == "AddNews" ? "增加" : "编辑") + "动态 [" + txtNewsName + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            doAjax("../", type,
                    {
                        "NewsName": txtNewsName,
                        "NewsPhoto": txtUpdateNewsName,
                        "NewsDesc": txtNewsDesc,
                        "NewsRemark":txtNewsRemark,
                        "NewsID": txtNewsID,
                        "IsRecommend":$("#cbkIsRecommend").val()
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
                                    close: function () { location.href = "../MicroWebsite/News.aspx?PID=122"; },
                                    lock: true
                                });
                        }
                    })
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    })
}

