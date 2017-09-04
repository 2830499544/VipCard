<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ChainStock.mobile.business.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>商家中心</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css" />
	<link rel="stylesheet" type="text/css" href="css/common.css"/>
	<link rel="stylesheet" type="text/css" href="css/style.css"/>
	<link rel="stylesheet" type="text/css" href="css/media.css"/>
	<link rel="stylesheet" type="text/css" href="css/color.css" />
</head>
<body>
   <div class="section index" id="container">
		<div id="head" class="section">
			<div class="section header">
				<%--<a href="##" class="logo"><img src="images/logo.png"/></a>--%>
                <h1>商家登录</h1>
			</div>
		</div>
		<div id="content" class="section">
			<div class="section login_form">
				<form class="section">
					<div class="section form_input">
						<span><img src="images/3_2.png" /></span>
						<input type="text" id="username" placeholder="请输入账号" maxlength="20" />
					</div>
					
					<div class="section form_input">
						<span><img src="images/lock.png" /></span>
						<input type="password" id="pwd" placeholder="请输入密码" maxlength="20" minlength="6" />
					</div>
					<div class="section form_input">
						<span><img src="images/2_6.png" /></span>
						<input type="text" placeholder="请输入验证码" id="YanZhengMa" maxlength="6" />
						<%--<a href="javascript:void(0);" id="getCode">获取验证码</a>--%>                       
						<%--<a href="##" id="codeImg"><img src="images/code.png"/></a>--%>
                        <a href="javascript:void(0);"  id="codeImg"  onclick="javascript:Login_ChangeValImg();">
                        <img id="Login_ValImg" src="" style="height: 30px; width: 90px; border: 0" alt=""  title="看不清？换一个" align="top" /></a>
					</div>
					<div class="section form_btn">
						<a href="javascript:void(0);" id="loginSumbit">登录</a>
					</div>
				</form>
			</div>
		</div>
	</div>    
<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>
</body>
</html>
