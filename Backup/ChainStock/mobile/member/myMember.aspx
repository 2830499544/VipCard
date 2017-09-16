<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myMember.aspx.cs" Inherits="ChainStock.mobile.member.myMember" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-我的会员卡</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
	<link rel="stylesheet" type="text/css" href="css/media.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
</head>
<body>
	<div class="section index" id="container">
		<div id="head" class="section index">
			<div class="section header">
				<h1>我的会员卡</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<!-- 跳转到微官网(按需使用) -->
				<a href="##" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
				<!-- 跳转到微商城(按需使用) -->
				<!-- <a href="##" class="head-icon" id="inStore"><img src="images/store.png"/></a> -->
			</div>
			<div class="section member-mesg">
				<div class="f-left member-img"><img src="images/head.png"  id="imgPhoto" runat="server"/></div>
				<div class="f-left c-fff head-mesg">
					<h3 id="spMemName" runat="server">欧阳娜娜</h3>
					<p><span id="spLevelName" runat="server">普通会员</span></p>
					<p><span>NO.<span id="spMemCard" runat="server">000002</span></span></p>
					<p><span>状态：<span id="spMemStatus" runat="server">挂失</span></span></p>
					<p><span>余额：￥<span id="spMemMoney" runat="server">90.00</span></span></p>
					<p><span></sapn>积分：<span id="spMemPoint" runat="server">20000</span></span></p>
					<a href="editMemberData.aspx"><img src="images/edit.png" /></a>
				</div>
			</div>
		</div>
		<div id="content" class="section">
			<div class="section line_box">
				<!-- 读取会员信息在此不可编辑 -->
				<div class="line_group">
					<p class="f-left">所属商家</p>
					<div class="f-right">
						<p id="spShopName" runat="server">成都运营商</p>
					</div>
				</div>
              
				<div class="line_group divMemAddressList divMemCouponList">
					<p class="f-left" t">我的优惠券</p>
					<div class="f-right">
						<a href="coupon.aspx" class="line_choice" id="">
							<p>查看</p>
							<span><img src="images/right_d.png" /></span>
						</a>
					</div>
				</div>
             
              
                <div class="line_group divMemAddressList">
					<p class="f-left">我的收货地址</p>
					<div class="f-right">
						<a href="memAddressManage.aspx" class="line_choice" id="A1">
							<p>查看</p>
							<span><img src="images/right_d.png" /></span>
						</a>
					</div>
				</div>
              
				<div class="line_group">
					<p class="f-left">身份证</p>
					<div class="f-right">
						<p id="spIdentityCard" runat="server">5109211991****2630</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">电子邮箱</p>
					<div class="f-right">
						<p id="spEmail" runat="server">无</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">联系地址</p>
					<div class="f-right">
						<p id="spAddress" runat="server">成都市高新区</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">手机号码</p>
					<div class="f-right">
						<p id="spMobile" runat="server">15899996666</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">生日</p>
					<div class="f-right">
						<p id="spBirthday" runat="server">1990-1-1</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">卡面号码</p>
					<div class="f-right">
						<p id="spCardNumber" runat="server">20161012001</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">办卡人员</p>
					<div class="f-right">
						<p id="spUserName" runat="server">习大大</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">推荐人卡号</p>
					<div class="f-right">
						<p id="spRecommendCard" runat="server">无</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">开卡日期</p>
					<div class="f-right">
						<p id="spCreateTime" runat="server">2016-10-12</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">失效日期</p>
					<div class="f-right">
						<p id="spPastTime" runat="server">永久有效</p>
					</div>
				</div>
			</div>
		</div>
		<!-- 底部浮动导航 -->
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

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>
</script>
</body>
</html>