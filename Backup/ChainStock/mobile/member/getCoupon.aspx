﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="getCoupon.aspx.cs" Inherits="ChainStock.mobile.member.getCoupon" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>领取优惠券</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
	<link rel="stylesheet" type="text/css" href="css/media.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
	<style type="text/css">
		.get-coupon .coupon-mesg{
			width: 80%;
			float: left;
		}
		.get-coupon > a.get-btn{
			height: 0.9rem;
			line-height: 0.9rem;
			width: 18%;
			text-align: center;
			float: right;
			background-color: #16a2de;
			color: #fff;
			font-size: 0.3rem;
			border-radius: 0.1rem;
		}
	</style>
</head>
<body>
	<div class="section index" id="container">
		<div id="head" class="section">
			<div class="section header">
				<h1>领取优惠券</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="##" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
			</div>
		</div>
        <input id="txtMemID" type="hidden"  runat="server"/>
		<div id="content" class="section" style="background-color:#f7f7f7;">
			<div class="section">
               <asp:Repeater ID="rptCoupon" runat="server">
                <ItemTemplate>   
				<div class="section bg-fff coupon-box get-coupon">
					<div class="section coupon-mesg">
						<div class="f-left">
							<h3><%#Eval("CouponTitle")%></h3>
							<p><span><%#GetCouponTime(Eval("ID"))%></span></p>
						</div>
						<div class="f-right">
							<h3><span>￥</span><%#Eval("CouponNumber")%></h3>
							<p>（满<%#Eval("CouponMinMoney","{0:f0}")%>元使用）</p>
						</div>
					</div>
					<a href="##" class="get-btn" onclick='<%# string.Format("return GetMemCoupon(\"{0}\")",Eval("ID")) %>'>领取</a>
				</div>
                 </ItemTemplate>
                </asp:Repeater>
                	<div style=" font-size:10pt; text-align:center;"   id="divMsg" runat="server" visible="false">暂无优惠券可领取!</div>
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

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>
</script>
</body>
</html>