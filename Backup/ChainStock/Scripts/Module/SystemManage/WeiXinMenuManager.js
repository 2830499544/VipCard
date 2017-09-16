$(function () {
    //添加菜单
    $("#btnAddMenu").bind("click", btnAddMenuClick);

    $("#sltType").bind("change", sltTypeChange);

    //添加菜单 保存
    $("#btnAddMenuSave").bind("click", btnAddMenuSaveClick);

    //保存一级菜单
    $("#btnAddMenuFirstSave").bind("click", btnAddMenuFirstSaveClick);

    //保存系统定义的子菜单
    $("#btnAddMenuBySystemSave").bind("click", btnAddMenuBySystemSaveClick);
    //创建菜单
    $("#btnCreateMenu").bind("click", btnCreateMenuClick);
})

//添加菜单
function btnAddMenuClick() {
    showRuleOrUrl(1);
    $("#txtMenuIDForEdit").val(""); //用于 状态判断(添加还是删除)
    art.dialog({
        lock: true,
        title: '新增二级菜单',
        width: '450',
        content: document.getElementById('divAddMenuInfo'),
        id: 'divAddMenuInfo',
        close: function () { $("#txtMenuName,#txtUrl").val(""); $("#sltType").val("1"); }
    });
}

//按钮类型
function sltTypeChange() {
    var type = parseInt($(this).val());
    showRuleOrUrl(type);
}

//显示Key(1)或者显示Url(2)
function showRuleOrUrl(type) {
    if (type == 1) {
        $("#tr_url").css("display", "none");
        $("#tr_rule").css("display", "");
    } else {
        $("#tr_rule").css("display", "none");
        $("#tr_url").css("display", "");
    }
}

function btnAddMenuSaveClick() {
    var MenuID = $("#txtMenuIDForEdit").val();
    var MenuName = $.trim($("#txtMenuName").val());
    var MenuType = $("#sltType").val(); //按钮类型
    var MenuKey = $("#sltReply").val(); // null
    var MenuUrl = $.trim($("#txtUrl").val());
    var parentMenuID = MenuID == "" ? $("#sltMenuType").val() : $("#sltMenuSecondType").val(); //所属一级菜单

    var strErrorMsg = "";
    if (MenuName == "") { strErrorMsg += "<li>菜单名称不能为空;</li>"; }
    if (MenuType == 1) {
        if (MenuKey == null) { strErrorMsg += "<li>您还没有添加任何回复规则;</li>"; }
    } else {
        if (MenuUrl == "") { strErrorMsg += "<li>链接地址不能为空;</li>"; } else {
            var regex = new RegExp("(http|ftp|https)\:\/\/(\\w+\.)?\\w+\.\\w+\/?");
            if (!regex.test(MenuUrl)) { strErrorMsg += "<li>链接地址格式不正确;</li>"; }
        }
    }

    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提示',
            icon: 'error',
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    var method = MenuID == "" ? "AddMenu" : "EditMenu";
    art.dialog({
        content: "将" + (MenuID == "" ? "增加" : "编辑") + "菜单 [" + MenuName + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            doAjax(
                        "../",
                        method,
                        {
                            "MenuName": MenuName,
                            "MenuType": MenuType,
                            "MenuKey": MenuKey,
                            "MenuUrl": MenuUrl,
                            "parentMenuID": parentMenuID,
                            "MenuID": MenuID
                        },
                        "text",
                        function (text) {
                            if (text == "0") {
                                art.dialog
                                      ({
                                          title: '系统提示',
                                          time: 4,
                                          content: ("系统异常，未保存数据，请再次点击保存！"),
                                          lock: true
                                      });
                            } else {
                                art.dialog
                                      ({
                                          title: '系统提示',
                                          time: 0.5,
                                          content: '保存成功',
                                          close: function () { location.href = "WeiXinMenuManager.aspx?PID=133"; },
                                          lock: true
                                      });
                            }
                        });
            return false;
        },
        cancelVal: '取消',
        cancel: true
    })
}

//删除菜单
function menuDel(MenuID, MenuName) {
    art.dialog({
        content: "将删除菜单 [" + MenuName + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            doAjax("../", "DelMenu", { "MenuID": MenuID }, "text", function (text) {
                if (text == "0") {
                    art.dialog
                                      ({
                                          title: '系统提示',
                                          time: 4,
                                          content: ("系统异常，未保存数据，请再次点击保存！"),
                                          lock: true
                                      });
                } else {
                    art.dialog
                                      ({
                                          title: '系统提示',
                                          time: 0.5,
                                          content: '删除成功',
                                          close: function () { location.href = "WeiXinMenuManager.aspx?PID=133"; },
                                          lock: true
                                      });
                }
            });
            return false;
        },
        cancelVal: '取消',
        cancel: true
    })
}

//编辑一级菜单
function updateMenuFirst(MenuID, MenuName) {
    $("#txtMenuNameFirst").val(MenuName);
    $("#txtMenuIDFirts").val(MenuID);
    art.dialog({
        lock: true,
        title: '编辑一级菜单',
        width: '400',
        content: document.getElementById('divMenuFirst'),
        id: 'divMenuFirst',
        close: function () { }
    });
}

//保存一级菜单
function btnAddMenuFirstSaveClick() {
    var MenuName = $.trim($("#txtMenuNameFirst").val());
    var MenuID = $("#txtMenuIDFirts").val();
    if (MenuName == "") {
        art.dialog({
            title: '系统提示',
            icon: 'error',
            content: '菜单名称不能为空!',
            lock: true
        });
        return false;
    }

    doAjax("../", "UpdataMenuFirst",
            {
                "MenuID": MenuID,
                "MenuName": MenuName
            }
            , "text", function (text) {
                if (text == "0") {
                    art.dialog
                        ({
                            title: '系统提示',
                            time: 4,
                            content: ("系统异常，未保存数据，请再次点击保存！"),
                            lock: true
                        });
                } else {
                    art.dialog
                    ({
                        title: '系统提示',
                        time: 0.5,
                        content: '保存成功',
                        close: function () { location.href = "WeiXinMenuManager.aspx?PID=133"; },
                        lock: true
                    });
                }
            })
}

function createMenuFirstList(MenuID, position) {
    doAjax("../", "GetMenuFirstList", { "MenuID": MenuID }, "text", function (text) {
        if (position == 1) {//编辑二级菜单 非系统菜单
            $("#sltMenuType").css("display", "none");
            $("#td_sltMenuType").append(text);
        } else { //编辑二级菜单 系统菜单 2
            $("#td_sltMenuTypeBySystem").append(text);
        }
    }, false)
}

function updateMenuSecound(MenuID) {
    doAjax("../", "GetMenuList", { "MenuID": MenuID }, "json", function (json) {
        $("#txtMenuName").val(json[0].MenuName);
        $("#sltType").val(json[0].MenuType);
        showRuleOrUrl(parseInt(json[0].MenuType));
        createMenuFirstList(json[0].parentMenuID, 1);
        $("#txtUrl").val(json[0].MenuUrl);
        if (parseInt(json[0].MenuType) == 1) { $("#sltReply").val(json[0].MenuKey); }
        $("#txtMenuIDForEdit").val(MenuID); //用于 状态判断(添加还是删除)

        art.dialog({
            lock: true,
            title: '编辑二级菜单',
            width: '450',
            content: document.getElementById('divAddMenuInfo'),
            id: 'divAddMenuInfo',
            close: function () { $("#txtMenuName,#txtUrl").val(""); $("#sltType").val("1"); $("#sltMenuType").css("display", ""); $("#sltMenuSecondType").remove(); }
        });
    }, false)
}

//编辑系统规则
function updateMenuFirstBySystem(MenuID, MenuName, parentMenuID) {
    $("#sp_MenuName").text(MenuName);
    createMenuFirstList(parentMenuID, 2);
    $("#txtMenuIDBySystem").val(MenuID);
    art.dialog({
        lock: true,
        title: '系统定义菜单',
        width: '400',
        content: document.getElementById('divMenuFirstBySystem'),
        id: 'divMenuFirstBySystem',
        close: function () { $("#sltMenuSecondType").remove() }
    });
}

//保存系统定义的子菜单
function btnAddMenuBySystemSaveClick() {
    var MenuID = $("#txtMenuIDBySystem").val();
    var parentMenuID = $("#sltMenuSecondType").val();
    doAjax("../", "UpdateMenuBySystem", { "MenuID": MenuID, "parentMenuID": parentMenuID }, "text", function (text) {
        if (text == "0") {
            art.dialog
                        ({
                            title: '系统提示',
                            time: 4,
                            content: ("系统异常，未保存数据，请再次点击保存！"),
                            lock: true
                        });
        } else {
            art.dialog
                    ({
                        title: '系统提示',
                        time: 0.5,
                        content: '保存成功',
                        close: function () { location.href = "WeiXinMenuManager.aspx?PID=133"; },
                        lock: true
                    });
        }
    })
}

//创建菜单
function btnCreateMenuClick() {
    art.dialog({
        content: "将同步菜单。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            doAjax("../", "CreateMenu", { "CreateMenu": "CreateMenu" }, "text", function (text) {
                if (text == "0") {
                    art.dialog
                                ({
                                    title: '系统提示',
                                    time: 4,
                                    content: ("系统异常，未保存数据，请再次点击保存！"),
                                    lock: true
                                });
                } else if (text == "1") {
                    art.dialog
                                    ({
                                        title: '系统提示',
                                        time: 4,
                                        content: ("AppId不能为空，请设置微信参数！"),
                                        lock: true
                                    });
                } else if (text == "2") {
                    art.dialog
                                ({
                                    title: '系统提示',
                                    time: 4,
                                    content: ("AppSecret不能为空，请设置微信参数！"),
                                    lock: true
                                });
                } else if (text == "3") {
                    art.dialog
                                    ({
                                        title: '系统提示',
                                        time: 4,
                                        content: ("获取 access_token 失败，请稍后重试！"),
                                        lock: true
                                    });
                } else if (text == "4") {
                    art.dialog
                                    ({
                                        title: '系统提示',
                                        time: 2,
                                        content: ("同步菜单成功！"),
                                        lock: true
                                    });
                }
            });
            return false;
        },
        cancelVal: '取消',
        cancel: true
    })
}