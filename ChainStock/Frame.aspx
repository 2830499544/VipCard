<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Frame.aspx.cs" Inherits="ChainStock.Frame" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商家联盟会员消费管理系统_商家联盟会员积分管理系统_异业联盟会员消费管理系统|可百连锁店会员管理系统_连锁店会员积分系统_专业版在线免费试用地址</title>
    <%--<title>商家联盟会员消费管理系统|||连锁会员管理系统 专业版|||连锁会员管理系统 积分版</title>--%>
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta name="keywords" content="连锁会员管理系统 专业版" />
    <link href="Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="Scripts/artDialog.iframeTools.js" type="text/javascript"></script>

    <script type="text/javascript">
        function btnUserPwdSave() {
            var strErrorMsg = "";
            var userName = $("#lblUserName").html();
            var oldPwd = $("#txtPwd").val();
            var newPwd = $("#txtNewPwd").val();
            var newRePwd = $("#txtNewRePwd").val();
            if (oldPwd == "") {
                strErrorMsg += "<li>请输入用户登录旧密码!</li>";
            }
            if (newPwd != newRePwd) {
                strErrorMsg += "<li>两次密码输入不一致,请重新输入!</li>";
            }
            if (newPwd == "") {
                strErrorMsg += "<li>请输入用户登录新密码!</li>";
            }

            if (strErrorMsg != "") {
                strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
                art.dialog({
                    icon: 'error', //图标
                    title: '系统提示',
                    content: strErrorMsg,
                    lock: true
                });
                return false;
            }

            doAjax("../",
           "UserPwdEdit",
           {
               "oldPwd": oldPwd,
               "newPwd": newPwd,
               "newRePwd": newRePwd
           },
           "json",
           function (json) {
               var t = art.dialog.list['EF893L'];
               switch (json) {
                   case 0:
                       art.dialog.alert("系统异常，未保存数据，请再次点击保存！");
                       break;
                   case -3:
                       art.dialog.alert("系统错误 请与系统管理员联系！");
                       break;
                   case -1:
                       art.dialog.alert("登录密码与原密码不一致,请重新输入，然后重试！");
                       break;
                   default:
                       art.dialog({
                           time: 1.5,
                           content: '用户登录密码保存成功！', close: function () { $("#txtPwd").val(""); $("#txtNewPwd").val(""); $("#txtNewRePwd").val(""); t.close(); }
                       });
                       break;
               }
           });
        }
    </script>
    <style>
        *
        {
            padding: 0;
            margin: 0;
        }
        html, body
        {
            height: 100%;
            border: none 0;
        }
        #iframe
        {
            width: 100%;
            height: 100%;
            border: none 0;
        }
    </style>
</head>
<body style="overflow: SCROLL; overflow-y: HIDDEN; overflow-x: HIDDEN">
    <iframe id="iframe" src="Main.aspx" scrolling="no" frameborder="0" margin="0" scrolling="yes"
        noresize></iframe>
</body>
</html>
