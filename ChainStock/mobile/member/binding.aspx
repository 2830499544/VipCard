<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="binding.aspx.cs" Inherits="ChainStock.mobile.member.binding" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-会员绑定</title>
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
				<h1>会员绑定</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="##" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
			</div>
		</div>
		<div id="content" class="section">
			<div class="section login_form" style="padding-bottom:0;">
				<form class="section" name="">
					<%--<div class="section form_input">
						<span><img src="images/3_2.png"/></span>
						<input type="text" placeholder="" id="txtMemCard"  runat="server" maxlength="20" value="这里获取会员账号" disabled="" />
					</div>--%>
					<div class="section t-center member-weixin">
						<span><img src="images/weixin.jpg"  id="headimgurl"  runat="server"/></span>
						<p><span runat="server" id="spNickName"></span></p>
					</div>
                    
					<div class="section form_btn">
						<a href="#" id="bindCardOk" runat="server">确认绑定</a>
                        <a href="#" id="cancelBindCard" runat="server">解绑</a>
					</div>
				</form>
			</div>

		</div>
     	<!-- 底部浮动导航 -->
		<div class="foot-nav">
			<!-- 返回主页 -->
			<div class="f-left fix-nav fix-home">
				<a href="index.aspx"><img src="images/home.png"/></a>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="##"><p>我的会员</p><img src="images/icon.png"/></a>
				<div class="foot-more">
					<a href="binding.aspx">会员卡绑定</a>
					<a href="myMember.aspx">我的会员卡</a>
					<a href="modifyPassword.aspx">修改密码</a>
				</div>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="##"><p>会员服务</p><img src="images/icon.png"/></a>
				<div class="foot-more">
					<a href="pointExchange.aspx">积分兑换</a>
					<a href="bill.aspx">账单记录</a>
				</div>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="rechargeOnline.aspx"><p>在线充值</p></a>
			</div>
		</div>
	</div>

          <input type="hidden" id="txtMemID" runat="server" />
       
      <input type="hidden" id="txtOpenID" runat="server" />
<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>
<script type="text/javascript">

</script>
</body>
</html>