<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="ChainStock.mobile.member.login" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-会员登录</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
	<link rel="stylesheet" type="text/css" href="css/media.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
</head>
<body>
	<div class="section index" id="container">
		<div id="head" class="section">
			<div class="section header">
				<h1>会员登录</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="../website/index.aspx" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
			</div>
		</div>
		<div id="content" class="section">
			<div class="section login_form">
				<form class="section">
					<div class="section form_input">
						<span><img src="images/3_2.png" /></span>
						<input type="text" id="memcard" placeholder="请输入会员账号" maxlength="20" />
					</div>
					<div class="section form_input">
						<span><img src="images/lock.png" /></span>
						<input type="password" id="pwd" placeholder="密码" maxlength="20" minlength="6" />
					</div>
					<div class="checkbox">
						<label>
					    
					    </label>
					    <a href="modifyPassword.aspx" id="forPassword">忘记密码？</a>
					</div>
					<div class="section form_btn">
						<a href="#" id="loginBtn" class="loginBtn">登录</a>
					</div>
					<p>还没有账号？30秒<a href="register.aspx" id="registerBtn">免费注册</a></p>
				</form>
			</div>
		</div>
	</div>

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>

</body>
</html>