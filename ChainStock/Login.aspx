<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ChainStock.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">




<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>会员管理系统_会员积分系统</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta name="keywords" content="连锁会员管理系统 专业版" />
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="Scripts/Module/System/Login.js" type="text/javascript"></script>
    <script src="Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <asp:Literal ID="litObject" runat="server" />
    <link href="themes/css/v3/public.css" rel="stylesheet" />
    <link href="themes/css/v3/register.css" rel="stylesheet" />
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
           <h1 class="png_bg"></h1>
           
        </div>
    </div>



    <div class="login_bg">
        <div class="form">
            <div class="loginad">
                <div class="loginadtitle"></div>
                <div class="loginaddes"></div>
                
                
            </div>
            <div class="login_form_container">
            <form name="loginForm" id="login_form" method="post" action="login/ldo" autocomplete="off">

                <input id="txtIsType" type="hidden" runat="server" />
                <input id="txtISCheckKey" type="hidden" runat="server" />
                <h2>商家登录</h2>
                <div class="div_user">
                    <span></span>
                    <input id="txtAccount" type="text" maxlength="20" runat="server" value="" placeholder="用户名" class="username" autocomplete="off" />
                </div>
                <div class="div_pw">
                    <span></span>
                    <input id="txtPassword" name="txtPassword" type="password" maxlength="20" autocomplete="off" value="" class="pw" runat="server" />
                </div>
                <div class="div_user"> <input id="txtValCode" type="text" class="yzm" value="" maxlength="4" placeholder="验证码" />
                    <a href="javascript:void(0);" onclick="javascript:Login_ChangeValImg();">
                        <img id="Login_ValImg" src="" style="height: 30px; width: 90px; border: 0" alt=""
                            title="看不清？换一个" align="top" /></a>
                </div>
                <div class="div_box">
                    <label>
                        <input type="checkbox" class="" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;下次自动登录</label>
                    <a class="forgetPw" onclick="CreateShortCut()">快捷桌面</a>
                </div>
                <div>
                    <input id="btnSubmit" type="button" value="登录" class="login_btn" runat="server" />
                </div>
                <div style="line-height: 30px; text-indent: 5px; color: #F30" id="loginTips">&nbsp;</div>

            </form>
                </div>
        </div>
    </div>
    <script  type="text/javascript">
        function toDesktop(sUrl, sName) {
            try {
                var WshShell = new ActiveXObject("WScript.Shell");
                var oUrlLink = WshShell.CreateShortcut(WshShell.SpecialFolders("Desktop") + "\\" + sName + ".url");
                oUrlLink.TargetPath = sUrl;
                oUrlLink.Save();
            } catch (e) {
                alert("当前IE安全级别不允许操作！");
            }
        }

        function CreateShortCut()
        {
            toDesktop(location.href, "长灵连锁店会员管理系统");
        }
    </script>

    <!-- footer start -->
    <div id="footer" class="clear">
        <h1 class="png_bg">JS代码网</h1>
        <div class="friendLink clear">
        
        </div>
        <p>上海科技有限公司©版权所有 沪ICP备140068888</p>
        <div class="weixin">
            <img src="themes/images/v3/weiyi_qr.png" alt="" />
            <h3>关注长灵科技</h3>
        </div>
    </div>
    <script type="text/javascript">
        var _bdhmProtocol = (("https:" == document.location.protocol) ? " https://" : " http://");
        document.write(unescape("%3Cscript src='" + _bdhmProtocol + "hm.baidu.com/h.js%3F5d12e2b4eed3b554ae941c0ac43c330a' type='text/javascript'%3E%3C/script%3E"));
</script>
    <!-- footer end -->
    <script type="text/javascript">
        var refer = '';
        $(document).ready(function () {
            $("#loginBtn").click(function () {
                $("#loginBtn").val('登录中...').attr("disabled", "disabled");
                var username = $(".username").val();
                var userpass = $(".pw").val();
                username = $.trim(username);
                userpass = $.trim(userpass);
                $("#loginTips").css({ display: "none" });

                if (username.length < 5) {
                    //$("#username_tips").html("请输入正确的用户名!").css({display:"block"});
                    $("#loginTips").html("请输入正确的用户名!").fadeIn();
                    $("#loginBtn").val('登录').removeAttr('disabled');
                    return false;
                }
                if (userpass == '') {
                    $("#loginTips").html("请输入正确的密码!").fadeIn();
                    $("#loginBtn").val('登录').removeAttr('disabled');
                    return false;
                }

                $("#login_form").ajaxSubmit(function (e) {
                    var obj = json_parse(e);
                    var code = obj.code;
                    if (code == '1') {
                        //sycSiteLogin();
                        if (refer) {
                            window.location.href = refer;
                        } else {
                            window.location.href = 'my';
                        }

                    } else if (code == '-1') {
                        $("#loginTips").html(obj.info).fadeIn();
                        $("#loginBtn").val('登录').removeAttr('disabled');
                        return false;
                    } else if (code == '-2') {
                        $("#loginTips").html(obj.info).fadeIn();
                        $("#loginBtn").val('登录').removeAttr('disabled');
                        return false;
                    } else if (code == '-4') {
                        $("#loginTips").html(obj.info).fadeIn();
                        $("#loginBtn").val('登录').removeAttr('disabled');
                        return false;
                    } else {
                        $("#loginTips").html(obj.info).fadeIn();
                        $("#loginBtn").val('登录').removeAttr('disabled');
                        return false;
                    }

                })

            })
        })

        function sycSiteLogin() {
            /* $.get('login/syc',function(e){
                
            }) */
        }

        //bind keyCode=13 Event
        $(function () {
            document.onkeydown = function (e) {
                var ev = document.all ? window.event : e;
                if (ev.keyCode == 13) {

                    $("#loginBtn").click();

                }
            }
        });
</script>
</body>
</html>
