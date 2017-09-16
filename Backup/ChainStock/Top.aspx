<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="ChainStock.Top" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Inc/Style/Style.css" rel="stylesheet" />    
    <link href="Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-common.js" type="text/javascript"></script>    
    <script src="Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="Scripts/jquery.SuperSlide.2.1.js" type="text/javascript"></script>
    <script src="Scripts/Module/System/Top.js" type="text/javascript"></script>
    <script type="text/javascript">
        var Show = true;
        var Showdia = false;
        function AppendStatus(szStatus, IsMem) {
            if (Show) {
                if (szStatus != "") {
                    art.dialog.data('mobile', szStatus);
                    art.dialog.open('Common/CallerMemDisplay.aspx?Mobile=' + szStatus + '&IsMem=' + IsMem, { id: "topCaller", title: IsMem == 0 ? '会员来电' : '非会员来电', lock: false }, false);
                }
            }
        }
        function T_GetEvent(uID, uEventType, uHandle, uResult, szdata) {
            var vValue = " type=" + uEventType + " Handle=" + uHandle + " Result=" + uResult + " szdata=" + szdata;
            switch (uEventType) {
                case BriEvent_GetCallID: //得到来电号码
                    doAjax("../",
                           "GetMemByMobile",
                           {
                               "mobile": szdata
                           },
                           "json",
                           function (json) {
                               if (json.RealModile.IsTelePhone()) { return false; }
                               if (json.flag > 0) {
                                   AppendStatus(json.RealModile, 0);
                               }
                               else if ($("#chkTelNoMember").attr("checked")) {
                                   AppendStatus(json.RealModile, 1);
                               }

                           }
                    )
                    break;
            }
        }
        //EOE_ShowDate();
    </script>
    <script type="text/javascript" for="qnviccub" event="OnQnvEvent(chID,type,handle,result,param,szdata,szdataex)">
	T_GetEvent(chID,type,handle,result,szdata)
    </script>
    <script src="Scripts/CallerMem/qnvfunc.js" type="text/javascript"></script>
    <script src="Scripts/CallerMem/qnviccub.js" type="text/javascript"></script>
    <asp:Literal ID="litObject" runat="server" />
    <object classid="clsid:F44CFA19-6B43-45EE-90A3-29AA08000510" id="qnviccub" data="DATA:application/x-oleobject;BASE64,GfpM9ENr7kWQoymqCAAFEAADAAD7FAAADhEAAA==
" width="0" height="0">
    </object>
</head>
<body style="padding: 0px; margin: 0px;">
    <form id="form1" runat="server">
    <input id="chkIsTel" runat="server" type="checkbox" style="display: none" />
    <input id="chkTelNoMember" runat="server" type="checkbox" style="display: none" />
    <div id="UserEdit" style="display: none">
        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 400px; margin: auto">
            <tr>
                <td class="tableStyle_left">
                    用户名称：
                </td>
                <td class="tableStyle_right">
                    <label id="lblUserName" runat="server">
                    </label>
                </td>
            </tr>
            <tr>
                <td class="tableStyle_left">
                    所属店铺：
                </td>
                <td class="tableStyle_right">
                    <label id="lblUserShop" runat="server">
                    </label>
                </td>
            </tr>
            <tr>
                <td class="tableStyle_left">
                    所属权限：
                </td>
                <td class="tableStyle_right">
                    <label id="lblUserGroup" runat="server">
                    </label>
                </td>
            </tr>
            <tr>
                <td class="tableStyle_left">
                    登录密码：
                </td>
                <td class="tableStyle_right">
                    <input type="password" id="txtPwd" runat="server" class="border_radius" />
                </td>
            </tr>
            <tr>
                <td class="tableStyle_left">
                    新的密码：
                </td>
                <td class="tableStyle_right">
                    <input type="password" id="txtNewPwd" runat="server" class="border_radius" />
                </td>
            </tr>
            <tr>
                <td class="tableStyle_left">
                    重复密码：
                </td>
                <td class="tableStyle_right">
                    <input type="password" id="txtNewRePwd" runat="server" class="border_radius" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <input type="button" id="btnUserSave" class="buttonColor" onclick="btnUserPwdSave()" value="保   存" />
                    
                    &nbsp
                    <input id="UseriD" type="hidden" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div id="top">
        <!-- 头部-->
        <div class="head">
            <div class="logo">
                <a href="StartPage.aspx" target="mainFrame">
                    <img id="topLogo"  alt=""  runat="server" src="/Upload/WebImage/main.png" />
                   
                </a>
            </div>
            <div class="userInfo">
                <span>欢迎</span> <a id="EditUser" style="color: #FFF; font-weight: bolder" runat="server"
                    href="javascript:void(0);"></a>| <a class="top_caller" runat="server" id="topCaller">
                        来电弹屏未启用</a> | <a href="#" id="top_hidefrm">隐藏菜单</a> | <a href="#" onclick="LoginOut()">
                            退出系统</a>
            </div>
        </div>
        <!-- 导航条-->
        <div class="navBar">
            <ul class="nav clearfix">
                <%= MainMenuString %>
            </ul>
            <ul class="subNav" style="position: relative;">
                <%= ChildMenuString %>
            </ul>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            //用titCell和mainCell 处理
            jQuery(".navBar").slide({
                titCell: ".nav .m", // 鼠标触发对象
                mainCell: ".subNav", // 切换对象包裹层
                delayTime: 0, // 效果时间
                triggerTime: 150, //鼠标延迟触发时间
                returnDefault: false  //返回默认状态
            });
            var cWidth = document.body.clientWidth;
            jQuery(".nav .m").each(function () {
                var x = $(this).offset().left - 50;
                var element = $("#" + $(this).attr('target'));
                if (cWidth - x < element.width()) {
                    element.css("left", cWidth - element.width() - 10);
                }
                else {
                    element.css("left", x);
                }
            });
        });
    </script>
    </form>
</body>
</html>
