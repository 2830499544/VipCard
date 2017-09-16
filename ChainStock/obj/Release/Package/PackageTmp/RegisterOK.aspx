<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterOK.aspx.cs" Inherits="ChainStock.RegisterOK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>嘉映映嘉会员管理系统</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta name="keywords" content="嘉映映嘉会员管理系统" />
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <asp:Literal ID="litObject" runat="server" />
    <link href="themes/css/v3/public.css" rel="stylesheet" />
    <link href="themes/css/v3/registershop.css" rel="stylesheet" />
    <link href="themes/css/v3/reset.css" rel="stylesheet" />
    <link href="Inc/artDialogskins/blue1.css" rel="stylesheet" type="text/css" />

    <style>
        .loginad {
            width: 580px;
            height: 595px;
            position: absolute;
        }

            .loginad .loginadtitle {
                width: 100%;
                text-align: center;
                line-height: 60px;
                font-size: 32px;
                font-family: "幼圆",Microsoft Yahei;
                margin-top: 70px;
                color: #FFF;
            }

            .loginad .loginaddes {
                line-height: 30px;
                color: #FFF;
                font-family: Microsoft Yahei;
                font-size: 16px;
                text-align: center;
            }

            .loginad .loginadimg {
                width: 100%;
                height: 320px;
                margin-top: 40px;
            }

            .loginad .loginadbtn {
                width: 100%;
                height: 40px;
                margin-top: 30px;
            }

                .loginad .loginadbtn a {
                    display: block;
                    width: 190px;
                    height: 40px;
                    margin: 0 auto;
                    text-align: center;
                    line-height: 40px;
                    font-size: 16px;
                    color: #009fe3;
                    font-weight: bold;
                    background: url(/themes/images/v3/btn.png) center center no-repeat;
                    -moz-border-radius: 6px;
                    -khtml-border-radius: 6px;
                    -webkit-border-radius: 6px;
                    border-radius: 6px;
                }
    </style>
</head>

<body>
    <div id="header">
        <div class="header">
           
        </div>
    </div>

    <div class="login_bg">
        <div class="form">
           
            <div class="login_form_container" >
            <form name="loginForm" id="login_form" method="post" action="login/ldo" autocomplete="off">

                <input id="txtIsType" type="hidden" runat="server" />
                <input id="txtISCheckKey" type="hidden" runat="server" />
                <h1 style="text-align:center">企业注册成功</h1>
                <div style="text-align:center">
                    <span></span>
                    <a style="line-height: 50px; font-size:20px">您已经成功注册使用会员管理系统</a>
                    <br />
                    <a style="line-height: 50px; font-size:20px" href="Download/setup.exe">点击下载会员管理系统</a>
                </div>
                <div>
                    <span></span>
                    <a id="txtShopName" style="line-height: 50px; font-size:20px" runat="server">企业名称:</a>
                </div>
                <div >
                    <span></span>
                     <a id="txtShopCode" style="line-height: 50px; font-size:20px" runat="server">企业代码:</a>
                </div>
                <div >
                    <span></span>
                     <a id="txtUserName" style="line-height: 40px; font-size:20px" runat="server">用&nbsp;户&nbsp;名:</a>
                </div>
                <div >
                    <span></span>
                     <a  id="txtPassword" style="line-height: 40px; font-size:20px" runat="server">密&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;码:</a>
                </div>
                
                <div style="line-height: 40px; text-indent: 5px; color: #F30;text-align:center" id="loginTips">
                    <a href="Default.aspx">跳转至登入页面</a>
                </div>

            </form>
                </div>
        </div>
    </div>

    <!-- footer start -->
    
    <script type="text/javascript">
        var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");
        document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3F5d12e2b4eed3b554ae941c0ac43c330a' type='text/javascript'%3E%3C/script%3E"));
</script>
    <!-- footer end -->
    
</body>
</html>
