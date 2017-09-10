<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="ChainStock.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>嘉映映嘉会员管理系统</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta name="keywords" content="嘉映映嘉会员管理系统" />
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="Scripts/Module/System/RegisterShop.js" type="text/javascript"></script>
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
                <h2>企业注册</h2>
                <div class="div_user">
                    <span></span>
                    <input id="txtShopCode" type="text" maxlength="20" runat="server" value=""  placeholder="企业代码" class="username" autocomplete="off" />
                </div>
                <div class="div_user">
                    <span></span>
                    <input id="txtShopName" type="text" maxlength="20" runat="server" value="" placeholder="企业名称" class="username" autocomplete="off" />
                </div>
                <div class="div_user">
                    <span></span>
                    <input id="txtTel" type="text" maxlength="20" runat="server" value="" placeholder="联系电话" class="username" autocomplete="off" />
                </div>
                <div class="div_user">
                    <span></span>
                    <input id="txtAddress" type="text" maxlength="20" runat="server" value="" placeholder="详细地址" class="username" autocomplete="off" />
                </div>
                <div class="div_pw">
                    <span></span>
                    <input id="txtPasswordOne" name="txtPassword" type="password" maxlength="20" placeholder="管理员密码" autocomplete="off" value="" class="pw" runat="server" />
                </div>
                <div class="div_pw">
                    <span></span>
                    <input id="txtPasswordTwo" name="txtPassword" type="password" maxlength="20" placeholder="重复管理员密码" autocomplete="off" value="" class="pw" runat="server" />
                </div>
                <div class="div_user">
                    <span></span>
                    <input id="txtSN" type="text" maxlength="20" runat="server" value="" placeholder="激活码" class="username" autocomplete="off" />
                </div>
                <div>
                    <input id="btnSubmit" type="button" value="注册" class="login_btn"  />
                </div>
                <div style="line-height: 30px; text-indent: 5px; color: #F30" id="loginTips">&nbsp;</div>

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
