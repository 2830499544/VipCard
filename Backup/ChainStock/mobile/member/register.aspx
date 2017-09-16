<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="ChainStock.mobile.member.register" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-注册会员</title>
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
				<h1>注册会员<span id="spCode" style="display:none"></span></h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="##" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
			</div>
		</div>
		<div id="content" class="section">
			<div class="section login_form reg_form">
				<form class="section" method="" accept="" name="">
					<div class="section form_input">
						<span><img src="images/3_2.png" /></span>
						<input type="text" id="memname" placeholder="请输入会员名称" maxlength="20" />
					</div>
					<div class="section form_input">
						<span><img src="images/2_5.png" /></span>
						<input type="text" id="mobile" placeholder="请输入手机号" maxlength="20" />
					</div>
					<div class="section form_input">
						<span><img src="images/2_6.png" /></span>
						<input type="text" id="smscode" placeholder="请输入手机验证码" maxlength="6" />
						<a href="javascript:void(0);" id="getCode"  onclick="sendregisterMessage()"  >获取验证码</a>
					</div>
					<div class="section form_input">
						<span><img src="images/lock.png" /></span>
						<input type="password" id="pwd" placeholder="请输入6-20位密码" maxlength="20" minlength="6" />
					</div>
					<div class="section form_input">
						<span><img src="images/lock.png" /></span>
						<input type="password" id="pwdok" placeholder="确认密码" maxlength="20" minlength="6" />
					</div>
					<div class="section form_btn">
						<a href="#" id="memRegister">立即注册</a>
					</div>
				</form>
			</div>
		</div>
	</div>

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>

</body>
</html>