$(document).ready(function () {
    $("[class=parent]").click(function () {
        $(this).parent().parent().next().css("display") == "none" ? $(this).parent().parent().next().css("display", "") : $(this).parent().parent().next().css("display", "none");
        $(this).parent().parent().next().css("display") == "none" ? $(this).prev("div").css("background", "url(../Inc/Style/images/plus.gif) no-repeat") : $(this).prev("div").css("background", "url(../Inc/Style/images/minus.gif) no-repeat");
    })
    $("[class=parent]").mouseover(function () {
        $(this).css({ "textDecoration": "underline", "color": "#FF00FE" });
    }).mouseout(function () {
        $(this).css({ "textDecoration": "none", "color": "#AF0081" });
    })

    if (SysParameter().WeiXinType == false && SysParameter().WeiXinVerified == false) {
        $("#tab2").css("display", "none");
    }
})

$(document).ready(function () {
    //图片上传
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

    CKEDITOR.editorConfig = function (config) {
        config.toolbar_Full = [
                    ['Source'],
                    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
                    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
                    ['Undo', 'Redo'],
                    ['Link', 'Unlink', 'Anchor']
                ];
    };
})

$(document).ready(function () {
    var exchangeTab = new CommonTab("RemindTab", "", function (index) {
        if (index == 0) {
            $("#btnAdd").val("添　加");
        }
        else if (index == 1) {
            $("#btnAdd").val("添　加");
        }
        else if (index == 2) {
            $("#btnAdd").val("管　理");
        }
        //$("#btnAdd").attr("style", "display:" + (index == 2 ? "none" : "") + "");
    });
    var result = /\w+?\w+&number=(\d+)/.test(window.document.location.href);
    if (result) {
        if (RegExp.$1 == "1") {
            $("#RemindTab ul li:eq(" + 1 + ")").click();
        }
    }

    //绑定空列表
    BindNullList("gvTextRule");
    BindNullList("rptNewsRuleTable");
    BindNullList("gvSystemRule");

    //添加按钮点击事件
    $("#btnAddRule").bind("click", btnAddRuleClick);

    //文本消息保存按钮点击事件
    $("#btnTextRuleSave").bind("click", btnTextRuleSaveClick);
    //文本消息重置按钮点击事件
    $("#btnTextRuleReset").bind("click", btnTextRuleResetClick);

    //图文消息保存按钮点击事件
    $("#btnNewsRuleSave").bind("click", btnNewsRuleSaveClick);
    //修改图文消息规则【保存按钮】点击事件
    $("#btnUpdateNewRuleByRuleNUmber").bind("click", btnUpdateNewRuleByRuleNUmberClick);
    //图文消息重置按钮点击事件
    $("#btnNewsRuleReset").bind("click", btnNewsRuleResetClick);
})

//添加按钮点击事件 响应函数
function btnAddRuleClick() {
    $("#txtNumber,#txtDescRule,#txtSendContent").val("").attr("disabled", false); //清空文本框的值
    var len = $("#RemindTab ul li").length;
    for (var i = 0; i < len; i++) {
        if ($("#MainContent" + i).css("display") == "block") {
            if (i == 0) {
                //弹出文本消息对话框
                $("#txtNumber").bind("blur", setValueForText);
                textRuleAdd();
            }
            else if (i == 1) {
                location.href = 'WeiXinRule.aspx?PID=107';
            }
            else if (i == 2) {
                window.location.href = "WeiXinMenuManager.aspx?PID=133";
            }
            break;
        }
    }
}

//文本消息保存按钮点击事件 响应函数
function btnTextRuleSaveClick() {
    var type = $("#textRuleType").val() == "" ? "TextRuleAdd" : "TextRuleEdit";
    var txtNumber = $.trim($("#txtNumber").val());
    var txtDescRule = $.trim($("#txtDescRule").val());
    var txtSendContent = $.trim($("#txtSendContent").val());
    var strErrorMsg = "";
    if (txtNumber == "") { strErrorMsg += "<li>回复序号不能为空;</li>"; }
    if (txtDescRule == "") { strErrorMsg += "<li>规则描述不能为空;</li>"; }
    if (txtSendContent == "") { strErrorMsg += "<li>回复内容不能为空;</li>"; }
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
        content: "将要" + (type == "TextRuleAdd" ? "添加" : "编辑") + "回复序号 [" + txtNumber + "]的文本消息规则。\n确定操作吗？",
        lock: true,
        ok: function () {
            doAjax("../", type, {
                "RuleNUmber": txtNumber,
                "RuleDesc": txtDescRule,
                "RuleContent": txtSendContent,
                "RuleID": $.trim($("#textRuleType").val())
            }, "json", function (json) {
                if (json == "0") {
                    art.dialog({
                        title: '系统提示',
                        content: ("系统异常，未保存数据，请再次点击保存！"),
                        lock: true
                    })
                } else if (json == "-1") {
                    $("#txtNumber").val(""); //清空回复序号
                    art.dialog({
                        title: '系统提示',
                        content: ("回复序号在系统中已存在，请更换后再次点击保存！"),
                        lock: true
                    })
                } else {
                    art.dialog({
                        title: '系统提示',
                        time: 1.5,
                        content: '保存成功！',
                        close: function () { location.href = "WeiXinRuleList.aspx?PID=98"; }
                    })
                }
            })
        },
        cancelVal: '取消',
        cancel: true
    })
}

//文本消息重置按钮点击事件  响应函数
function btnTextRuleResetClick() {
    var number = parseInt($("#txtNumber").val());
    if (!isNaN(number)) {
        if (number < 0) {
            $("#txtSendContent").val("");
        }
    }
    else {
        $("#txtNumber,#txtDescRule,#txtSendContent").val("");
    }
}

//删除消息规则方法(文本消息、图文消息)
//type=1:文本消息;type=2:图文消息
function delRule(RuleID, type) {
    art.dialog({
        content: '确定要删除吗?',
        lock: true,
        ok: function () {
            var delMethodName = type == 1 ? "TextRuleDel" : type == 2 ? "NewsRuleDel" : "None";
            doAjax("../", delMethodName, { "RuleID": RuleID }, "json", function (json) {
                if (json == "0") {
                    art.dialog({
                        title: '系统提示',
                        content: '系统错误 删除失败 请与系统管理员联系！',
                        lock: true
                    })
                } else if (json == "-1") {
                    art.dialog({
                        title: '系统提示',
                        content: '该规则在自定义菜单中已使用，不允许删除！',
                        lock: true,
                        time:2
                    })
                } else {
                    art.dialog({
                        title: '系统提示',
                        time: 1.5,
                        content: '删除成功！',
                        close: function () { if (type == 2) { location.href = "WeiXinRuleList.aspx?PID=98&number=1" } else { location.href = "WeiXinRuleList.aspx?PID=98" } }
                    })
                }
            })
        },
        cancelVal: '取消',
        cancel: true
    })
}

//编辑文本消息规则方法
function textRuleEdit(RuleID) {
    $("#textRuleType").val(RuleID);
    $("#txtNumber").unbind("blur", setValueForText);
    doAjax("../", "GetTextRuleByID", { "RuleID": RuleID }, "json", function (json) {
        if (json.length > 0) {
            var row = json[0];
            $("#txtNumber").val(row.RuleNUmber);
            $("#txtDescRule").val(row.RuleDesc);
            $("#txtSendContent").val(row.RuleContent);

            var number = parseInt($("#txtNumber").val());
            if (!isNaN(number)) {
                if (number < 0) {
                    $("#txtNumber,#txtDescRule").attr("disabled", true);
                } else {
                    $("#txtNumber,#txtDescRule").attr("disabled", false);
                }
            }
        }
    })
    textRuleAdd();
}

//弹出文本消息对话框
function textRuleAdd() {
    var type = $("#textRuleType").val() == "" ? "TextRuleAdd" : "TextRuleEdit";
    art.dialog({
        title: (type == "TextRuleAdd" ? "添加" : "编辑") + "文本消息规则",
        content: document.getElementById('textRuleDialog'),
        id: 'textRuledialog',
        lock: true,
        close: function () {
            $("#textRuleType").val("");
        } //清空状态
    });
}
//////////////////////////////////////以下是图文消息///////////////////////////////////
//弹出图文消息对话框(txtNewsType对应Ajax请求时的方法名)
function newsRuleAdd() {
    var mothed = $("#txtNewsType").val() == "" ? "NewsRuleAdd" : $("#txtNewsType").val() == "NewsAdd" ? "NewsAdd" : "NewsEdit";
    var titleStr = "";
    if (mothed == "NewsRuleAdd") {
        titleStr = "添加图文消息规则";
        $("#txtNewsNumber").attr("disabled", false);
    } else if (mothed == "NewsAdd") {
        titleStr = "添加图文消息列表";
        $("#txtNewsNumber").attr("disabled", true);
    } else {
        titleStr = "编辑图文消息列表";
        $("#txtNewsNumber").attr("disabled", true);
    }
    art.dialog({
        title: titleStr,
        content: document.getElementById('newsRuleDialog'),
        id: 'newsRuleDialog',
        lock: true,
        close: function () { $("#txtNewsType").val(""); btnNewsRuleResetClick(); }
    })
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
    var NewsLinkContent = CKEDITOR.instances.txtLinkContent.getData();
    var strErrorMsg = "";
    if (RuleNUmber == "") { strErrorMsg += "<li>回复序号不能为空;</li>"; }
    if (NewsTitle == "") { strErrorMsg += "<li>图文标题不能为空;</li>"; }
    if (NewsDesc == "") { strErrorMsg += "<li>图文描述不能为空;</li>"; }
    if (NewsUrlFirst == "") { strErrorMsg += "<li>请先上传照片再点击保存按钮;</li>"; }
    if (NewsLinkContent == "") { strErrorMsg += "<li>跳转后的网页内容不能为空</li>"; }
    var contentSte = "";
    if (mothed == "NewsRuleAdd") {
        contentSte = "将要添加回复序号 [" + RuleNUmber + "]的图文消息规则。\n确定操作吗？";
        if (NewsDesc == "") { strErrorMsg += "<li>规则描述不能为空;</li>"; }
    } else if (mothed == "NewsAdd") {
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
        content: contentSte,
        lock: true,
        ok: function () {
            doAjax("../", mothed, {
                "RuleNUmber": RuleNUmber,
                "NewsTitle": NewsTitle,
                "NewsDesc": NewsDesc,
                "NewsUrlFirst": NewsUrlFirst,
                "NewsUrlSecond": NewsUrlSecond,
                "NewsRuleID": $("#txtNewsRule").val(),
                "NewsID": $("#txtNewsID").val(),
                "RuleDesc": RuleDesc,
                "NewsLinkContent": NewsLinkContent
            }, "json", function (json) {
                if (json == "0") {
                    art.dialog({
                        title: '系统提示',
                        content: ("系统异常，未保存数据，请再次点击保存！"),
                        lock: true
                    })
                } else if (json == "-1") {
                    art.dialog({
                        title: '系统提示',
                        content: ("回复序号在系统中已存在，请更换后再次点击保存！"),
                        lock: true
                    })
                } else {
                    art.dialog({
                        title: '系统提示',
                        time: 1.5,
                        content: '保存成功！',
                        close: function () { location.href = "WeiXinRuleList.aspx?PID=98&number=1"; }
                    })
                }
            })
        },
        cancelVal: '取消',
        cancel: true
    })
}
//修改图文消息【保存按钮】点击事件  响应函数
function btnUpdateNewRuleByRuleNUmberClick() {
    var RuleID = $("#txtNewsRuleID").val();
    var RuleNUmber = $("#txtUpdateNewRuleByRuleNUmber").val();
    var RuleDesc = $("#txtNewsDescRule").val();
    var strErrorMsg = "";
    if (RuleNUmber == "") { strErrorMsg += "<li>回复序号不能为空;</li>"; }
    if (RuleDesc == "") { strErrorMsg += "<li>规则描述不能为空;</li>"; }
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
    doAjax("../", "NewsRuleEdit", { "RuleID": RuleID, "RuleNUmber": RuleNUmber, "RuleDesc": RuleDesc }, "json", function (json) {
        if (json == "0") {
            art.dialog({
                title: '系统提示',
                content: ("系统异常，未保存数据，请再次点击保存！"),
                lock: true
            })
        } else if (json == "-1") {
            art.dialog({
                title: '系统提示',
                content: ("回复序号在系统中已存在，请更换后再次点击保存！"),
                lock: true
            })
        } else {
            art.dialog({
                title: '系统提示',
                time: 1.5,
                content: '保存成功！',
                close: function () { location.href = "WeiXinRuleList.aspx?PID=98&number=1"; }
            })
        }
    })
}

//弹出修改图文消息对话框(修改回复序号,这是一个独立的对话框)
function newsRuleEdit(RuleID) {
    doAjax("../", "GetTextRuleByID", { "RuleID": RuleID }, "json", function (json) {
        if (json.length > 0) {
            $("#txtUpdateNewRuleByRuleNUmber").val(json[0].RuleNUmber);
            $("#txtNewsRuleID").val(json[0].RuleID)
            $("#txtNewsDescRule").val(json[0].RuleDesc);
            art.dialog({
                title: '修改图文消息规则',
                content: document.getElementById('newsRuleDialogByRuleNUmber'),
                id: 'newsRuleDialogByRuleNUmber',
                lock: true,
                close: function () { $("#txtNewsRuleID").val("") }
            })
        }
    })
}

//删除图文消息列表(删除指定图文消息中的某一条文章)
function newsDel(NewsID) {
    art.dialog({
        content: '确定要删除吗?',
        lock: true,
        ok: function () {
            doAjax("../", "NewsDel", { "NewsID": NewsID }, "json", function (json) {
                if (json == "0") {
                    art.dialog({
                        title: '系统提示',
                        content: '系统错误 删除失败 请与系统管理员联系！',
                        lock: true
                    })
                } else {
                    art.dialog({
                        title: '系统提示',
                        time: 1.5,
                        content: '删除成功！',
                        close: function () { location.href = "WeiXinRuleList.aspx?PID=98&number=1"; }
                    })
                }
            })
        },
        cancelVal: '取消',
        cancel: true
    })
}

//添加图文列表
function newsAdd(RuleID) {
    $("#txtNewsNumber").unbind("blur", setValueForNews);
    $("#txtNewsType").val("NewsAdd"); //标识 现在处于添加图文列表状态
    doAjax("../", "GetTextRuleByID", { "RuleID": RuleID }, "json", function (json) {
        if (json.length > 0) {
            $("#txtNewsNumber").val(json[0].RuleNUmber);
            $("#txtNewsRule").val(RuleID)//保存状态，用于关联主从表
            $("#trNewsRuleDescAdd").css("display", "none");
            $("#imgNewsPhoto").attr("src", "../Upload/WeiXin/Images/zwtp.jpg");
            newsRuleAdd(); //弹出添加对话框
        }
    })
}

//编辑图文列表(指定编号)
function newsEdit(NewsID) {
    $("#txtNewsNumber").unbind("blur", setValueForNews);
    $("#txtNewsType").val("NewsEdit"); //标识 现在处于添加图文列表状态
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
            //var NewsLinkContent = json[0].NewsLinkContent.replace(/'/g, "\"");
            CKEDITOR.instances.txtLinkContent.setData(json[0].NewsLinkContent);
            json[0].NewsUrlFirst.match(".*/(.*)");
            var imgSrc = "../Upload/WeiXin/Images/" + RegExp.$1;
            $("#imgNewsPhoto").attr("src", imgSrc);
            newsRuleAdd(); //弹出添加对话框
        }
    })
}
//图文消息保存按钮点击事件 响应函数
function btnNewsRuleResetClick() {
    var mothed = $("#txtNewsType").val();
    if (mothed == "NewsAdd" || mothed == "NewsEdit") { $("#txtNewsRuleDescAdd,#txtNewsTitle,#txtNewsDesc").val(""); CKEDITOR.instances.txtLinkContent.setData(""); }
    else { $("#txtNewsNumber,#txtNewsRuleDescAdd,#txtNewsTitle,#txtNewsDesc").val(""); CKEDITOR.instances.txtLinkContent.setData(""); }
}
//设置文本规则描述默认值
function setValueForText() {
    var txt = $.trim($(this).val());
    if (txt != "") {
        var result = "回复" + txt + " 查看";
        $("#txtDescRule").val(result);
    }
}

//设置图文规则描述默认值
function setValueForNews() {
    var txt = $.trim($(this).val());
    if (txt != "") {
        var result = "回复" + txt + " 查看";
        $("#txtNewsRuleDescAdd").val(result);
    }
}