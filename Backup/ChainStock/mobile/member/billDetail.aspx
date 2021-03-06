﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="billDetail.aspx.cs" Inherits="ChainStock.mobile.member.billDetail" %>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-账单记录详情</title>
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
				<h1>账单记录详情</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="##" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
			</div>
		</div>
		<div id="content" class="section">
			<div class="section message-detail">
				<div class="section mesg-record">
					<h3 id="spOrderType" runat="server">商品消费</h3>
				</div>
				<div class="section mesg-record">
					<p id="spOrderDiscountMoney" runat="server">-398.00</p>
				</div>
               <div class="section mesg-d-line">
					<p class="f-left mesg-name" id="spPayTypeInfo" runat="server">付款方式</p>
					<p class="f-right mesg-con" id="spPayType" runat="server">现金</p>
				</div>
			
					
				<div class="section mesg-d-line">
					<p class="f-left mesg-name" >商品说明</p>
					<p class="f-right mesg-con" id="spGoodsName" runat="server">商品消费</p>
				</div>		
				
				<div class="section mesg-d-line">
					<p class="f-left mesg-name" id="spOrderPointInfo" runat="server">获得积分</p>
					<p class="f-right mesg-con" id="spOrderPoint" runat="server">398.00</p>
				</div>
				<div class="section mesg-d-line">
					<p class="f-left mesg-name" >创建时间</p>
					<p class="f-right mesg-con" id="spOrderCreateTime" runat="server" >2016-10-10 12:36:48</p>
				</div>
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

</body>
</html>