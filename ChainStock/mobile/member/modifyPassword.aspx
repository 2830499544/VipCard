<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modifyPassword.aspx.cs" Inherits="ChainStock.mobile.member.modifyPassword" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-修改密码</title>
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
				<h1>修改密码<span id="spCode" style="display:none"></span></h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="../website/index.aspx" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
			</div>
		</div>
		<div id="content" class="section">

			<div class="section login_form reg_form ">
				<form class="section" method="" accept="" name="">
					<div class="section form_input">
						<span><img src="images/2_5.png" /></span>
						<input type="text" id="mobile"  maxlength="20"  runat="server" disabled="disabled" />
					</div>
					<div class="section form_input">
						<span><img src="images/2_6.png" /></span>
						<input type="text" id="smscode" placeholder="请输入手机验证码" maxlength="6" />
                       


                   <a href="javascript:void(0);" id="getCode" onclick="sendmodifyPwdMessage()"  >获取验证码</a>
					</div>
                    
					<div class="section form_input">
						<span><img src="images/lock.png" /></span>
						<input type="password" id="pwd" placeholder="请输入6-20位新密码" maxlength="20" minlength="6" />
					</div>
					<div class="section form_input">
						<span><img src="images/lock.png" /></span>
						<input type="password" id="pwdok" placeholder="确认新密码" maxlength="20" minlength="6" />
					</div>
					<div class="section form_btn">
						<a href="#" id="changePwd">确认修改</a>
					</div>
				</form>
			</div>
		</div>
	</div>
    <input type="hidden" id="txtMemID" />
<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>

</body>
</html>