<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registerSuccess.aspx.cs" Inherits="ChainStock.mobile.member.registerSuccess" %>

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
				<h1>注册会员</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="../website/index.aspx" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
			</div>
		</div>
		<div id="content" class="section">
			<div class="section login_form reg_form">
				<div class="section reg_success t-center">
					<img src="images/success.png" />
					<p>恭喜您！注册成功！</p>
				</div>
				<div class="section login_form reg_form">
					<form class="section" method="" accept="" name="">
						<div class="section form_btn">
							<a href="login.aspx" id="loginBtn">立即登录</a>
						</div>
					</form>
				</div>
			</div>
		</div>
	</div>

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>

</body>
</html>