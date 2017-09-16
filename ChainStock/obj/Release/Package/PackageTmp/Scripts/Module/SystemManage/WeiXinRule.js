$(document).ready(function () {
    if ($("#txtNewsID").val() == "0") {
        $("#txtNewsNumber").attr("disabled", false);
        if ($("#txtRuleID").val() != "") {
            newsAdd($("#txtRuleID").val());
        }
        else {
            $("#txtNewsType").val("");
        }
    }
    else {
        $("#trNewsRuleDescAdd").css("display", "none");
        $("#txtNewsType").val("NewsEdit");
        $("#txtNewsNumber").attr("disabled", true);
        newsEdit($("#txtNewsID").val());
    }
    $("#btnNewsRuleReset").bind("click", btnNewsRuleResetClick);
    $("#btnNewsRuleSave").bind("click", btnNewsRuleSaveClick);
    $("#txtNewsNumber").bind("blur", setValueForNews);
    $("#trNewsRuleDescAdd").css("display", "");
    $("#imgNewsPhoto").attr("src", "../Upload/WeiXin/Images/zwtp.jpg");
    $("#txtNewsNumber").bind("blur", setValueForNews);
    $("#btnNewsPicUpload").uploadify({
        'uploader': "../images/swf/uploadify.swf",
        'script': "../Service/upload.ashx",
        'cancelImg': "../images/member/cancel.png",
        'queueID': 'divNewsPic_fileQueue',
        'buttonImg': "../images/member/selectImg.jpg",
        'height': 25,
        'width': 70,
        'fileExt': "*.jpg;*.png",
        'fileDesc': "请选择格式为JPG或PNG的图片",
        'auto': false,
        'multi': false,
        'sizeLimit': 512000,
        'fileDataName': "WeiXin",
        'onError': function (event, ID, fileObj, errorObj) {
            if (errorObj.type == "File Size")
                alert("对不起，上传的图片不能超过500K");
            else
                alert(errorObj.type + ' Error: ' + errorObj.info);
        },
        'onComplete': function (event, ID, fileObj, response, data) {
            if (response != "0") {
                $("#imgNewsPhoto").attr("src", "../Upload/WeiXin/Images/" + response);
                var imgUrl = "http://" + $("#txtSystemDomain").val() + "/Upload/WeiXin/Images/" + response;
                $("#txtPicUrl").val(imgUrl);
            }
        }
    });
})

//添加图文列表
function newsAdd(RuleID) {
    $("#txtNewsNumber").attr("disabled", true);
    $("#txtNewsNumber").unbind("blur", setValueForNews);
    $("#txtNewsType").val("NewsAdd"); //标识 现在处于添加图文列表状态
    doAjax("../", "GetTextRuleByID", { "RuleID": RuleID }, "json", function (json) {
        if (json.length > 0) {
            $("#txtNewsNumber").val(json[0].RuleNUmber);
            $("#txtNewsRule").val(RuleID)//保存状态，用于关联主从表
            $("#trNewsRuleDescAdd").css("display", "none");
            $("#imgNewsPhoto").attr("src", "../Upload/WeiXin/Images/zwtp.jpg");
        }
    })
}

//编辑图文列表(指定编号)
function newsEdit(NewsID) {
    $("#txtNewsNumber").unbind("blur", setValueForNews);
    doAjax("../", "GetNewsRuleByID", { "NewsID": NewsID }, "json", function (json) {
        if (json.length > 0) {
            $("#txtNewsNumber").val(json[0].RuleNUmber);
            $("#txtNewsRule").val(json[0].NewsRuleID)//保存状态，用于关联主从表
            $("#txtNewsTitle").val(json[0].NewsTitle);
            $("#txtNewsDesc").val(json[0].NewsDesc);
            $("#txtPicUrl").val(json[0].NewsUrlFirst);
            $("#txtUrl").val(json[0].NewsUrlSecond);
            $("#txtNewsID").val(NewsID);
            $("#imgNewsPhoto").attr("src", json[0].NewsUrlFirst);
            $("#trNewsRuleDescAdd").css("display", "none");
            $("#txtNoticeDetail").val((json[0].NewsLinkContent));
            json[0].NewsUrlFirst.match(".*/(.*)");
            var imgSrc = "../Upload/WeiXin/Images/" + RegExp.$1;
            $("#imgNewsPhoto").attr("src", imgSrc);
        }
    })
}

function setValueForNews() {
    var txt = $.trim($(this).val());
    if (txt != "") {
        var result = "回复" + txt + " 查看";
        $("#txtNewsRuleDescAdd").val(result);
    }
}

//图文消息保存按钮点击事件 响应函数
function btnNewsRuleSaveClick() {
    var mothed = $("#txtNewsType").val() == "" ? "NewsRuleAdd" : $("#txtNewsType").val() == "NewsAdd" ? "NewsAdd" : "NewsEdit";
    var RuleNUmber = $.trim($("#txtNewsNumber").val());
    var NewsTitle = $.trim($("#txtNewsTitle").val());
    var NewsDesc = $.trim($("#txtNewsDesc").val());
    var NewsUrlFirst = $.trim($("#txtPicUrl").val());
    var NewsUrlSecond = $.trim($("#txtUrl").val());
    var RuleDesc = $("#txtNewsRuleDescAdd").val();
    //var NewsLinkContent = $.trim($("#txtLinkContent").val());
    var NewsLinkContent = $("#txtNoticeDetail").val();
    var strErrorMsg = "";
    if (RuleNUmber == "") { strErrorMsg += "<li>回复序号不能为空;</li>"; }
    if (NewsTitle == "") { strErrorMsg += "<li>图文标题不能为空;</li>"; }
    if (NewsDesc == "") { strErrorMsg += "<li>图文描述不能为空;</li>"; }
    if (NewsUrlFirst == "") { strErrorMsg += "<li>请先上传照片再点击保存按钮;</li>"; }
    if (NewsLinkContent == "") { strErrorMsg += "<li>跳转后的网页内容不能为空</li>"; }
    var contentSte = "";
    if (mothed == "NewsAdd") {
        contentSte = "将要添加图文标题[" + NewsTitle + "]的图文消息。\n确定操作吗？";
    } else {
        contentSte = "将要编辑图文标题[" + NewsTitle + "]的图文消息。\n确定操作吗？";
    }
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提醒',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }

    art.dialog({
        title: '系统提示',
        content: contentSte,
        lock: true,
        ok: function () {
            this.close();
            doAjax("../",
             mothed,
             {
                 "RuleNUmber": RuleNUmber,
                 "NewsTitle": NewsTitle,
                 "NewsDesc": NewsDesc,
                 "NewsUrlFirst": NewsUrlFirst,
                 "NewsUrlSecond": NewsUrlSecond,
                 "NewsRuleID": $("#txtNewsRule").val(),
                 "NewsID": $("#txtNewsID").val(),
                 "RuleDesc": RuleDesc,
                 "NewsLinkContent": NewsLinkContent
             },
             "json",
             function (json) {
                 if (json == "0") {
                     art.dialog({
                         title: '系统提示',
                         content: ("系统异常，未保存数据，请再次点击保存！"),
                         lock: true
                     })
                 }
                 else if (json == "-1") {
                     art.dialog({
                         title: '系统提示',
                         content: ("回复序号在系统中已存在，请更换后再次点击保存！"),
                         lock: true
                     })
                 }
                 else {
                     art.dialog({
                         title: '系统提示',
                         time: 3,
                         content: '保存成功！',
                         close: function () { location.href = "WeiXinRuleList.aspx?PID=98&number=1"; },
                         lock: true
                     });
                 }
             });
        },
        cancelVal: '取消',
        cancel: true
    });
}

function btnNewsRuleResetClick() {
    $("#txtNewsRuleDescAdd,#txtNewsTitle,#txtNewsDesc").val("");

}