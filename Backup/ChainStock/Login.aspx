<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ChainStock.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">


 

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>智络连锁店会员管理系统_连锁店会员积分系统—在线免费试用地址</title>   
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta name="keywords" content="连锁会员管理系统 专业版" />
    <link href="Inc/Style/style.css" rel="stylesheet" type="text/css" />
    <link href="Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="Scripts/Module/System/Login.js" type="text/javascript"></script>
    <script src="Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <asp:Literal ID="litObject" runat="server" />
    
</head>
<body runat="server" id="loginBody">
    <ul class="s-skin-container" id="bg-container">
        <li id="bg0" style="background-image: url(Upload/WebImage/MainLogin.png)"></li></ul><div class="wrapper">
        <div class="logo-bar">
            <a class="logo" id="imgLogo" runat="server">连锁会员管理软件--专业版</a></div><div class="content">
            <div class="content-bg">              
                <div class="erweima">
                    <img src="/images/login/ZL_erweima.jpg"/>
                    <p>免费体验微商城</p>
                </div>
            </div>
            <div class="line-bg">
            </div>
            <div class="main">
                <div class="login-mod">
                    <div class="title1">
                        用户登录</div>
                    <div class="loginbox">
                        <label id="AccountLabel" for="txtAccount" class="tips">
                            登录账号</label>
                        <input id="txtAccount" type="text" maxlength="20" class="user-text"  runat="server" value="" />
                        <input id="txtIsType" type="hidden" runat="server" />
                        <input id="txtISCheckKey" type="hidden" runat="server" />
                        <label id="passwordLabel" for="txtPassword" class="tips ptips">
                            登录密码</label>
                        <input id="txtPassword" name="txtPassword" type="password" maxlength="20" autocomplete="off" class="user-text" value="" runat="server" />
                        <label id="valcodeLabel" for="txtValCode" class="tips2">
                            验证码</label>
                        <input id="txtValCode" type="text" class="user-text2" value="" maxlength="4" />
                        <div class="check">
                            <a href="javascript:void(0);" onclick="javascript:Login_ChangeValImg();">
                                <img id="Login_ValImg" src="" style="height: 30px; width: 90px; border: 0" alt=""
                                    title="看不清？换一个" align="top" /></a>
                        </div>
                        <p>
                            <input id="btnSubmit" type="button" value="登录" class="login-btn" runat="server" />&nbsp;&nbsp;
                            <input id="btnReset" type="button" value="重置" class="login-btn2"  onclick="Login_Reset()" />
                        </p>
                    </div>
                </div>
            </div>
            <div class="aside">
                <div class="title2" style="display:none;">
                    <a id="aSelf" runat="server" target="_blank">点击进入会员自助平台</a>
                </div>

                <p>
                    <a href="Common/Exec.aspx?todo=shortcut">添加快捷桌面</a>| <a href='javascript:bookmark()'>
                        加入收藏</a>
                </p>
                <%--<div class="title_pic">
                    会员越来越多，生意越来越好！</div>--%>
                
                <span style=" color:white; line-height:26px;">
                <br />
                       运营商测试账号 admin 密码 admin  <br />
                       联盟商测试账号 lms001 密码 123 <br />
                         商家测试账号 sj001 密码 123
                     </span>

            </div>
               
        </div>
         
    </div>       
    
    
     <div id="divFoot" runat="server" class="footer-nav-box">
            <div class="footer-nav">
                <div class="copyright">
                    <span id="spCompName" runat="server">Copyright © 智络科技 2012-2014</span>
                    <span id="spBeiAn" runat="server"><a id="aBeiAnName" runat="server" href="http://www.miitbeian.gov.cn" target="_blank"></a></span>
                    <span id="spEdition" runat="server"></span>
                    <span id="spDesc" runat="server" style="display:none"></span>
                </div>

                <p runat="server" id="pHelpInfo">
                    <a href="http://www.liansuohuiyuan.net/" target='_blank'>公司网址</a>|<a href="http://kchelp.vip5968.net/" target='_blank'>帮助文档</a>|<a href="http://www.liansuohuiyuan.net/about-664.html" target='_blank'>联系方式</a>|<a>热线4000-525-526</a>
                </p>
            </div>
        </div>
</body>
</html>
