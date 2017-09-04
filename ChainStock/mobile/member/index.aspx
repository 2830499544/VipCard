<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ChainStock.mobile.member.index" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-首页</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
	<link rel="stylesheet" type="text/css" href="css/media.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
</head>
<body>
	<div class="section index" id="container" style="margin-bottom:0;">
		<div id="head" class="section index">
			<div class="section header">
				<h1>会员中心</h1>
				<a href="javascript:void(0);" class="logout"><img src="images/logout.png"/></a>
				<!-- 跳转到微官网(按需使用) -->
				<a href="../website/index.aspx" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
				<!-- 跳转到微商城(按需使用) -->
				<!-- <a href="##" class="head-icon" id="inStore"><img src="images/store.png"/></a> -->
			</div>
			<div class="section member-mesg">
				<div class="f-left member-img"><img src="images/head.png"  id="imgPhoto" runat="server"/></div>
				<div class="f-left c-fff head-mesg">
					<h3 id="spMemName" runat="server"></h3>
					<p><span id="spLevelName" runat="server">普通会员</span></p>
					<p><span>NO.<span id="spMemCard" runat="server">000002</span></span></p>
					<p><span>状态：<span id="spMemState" runat="server"></span></span></p>
					<p><span>余额：￥<span id="spMemMoney" runat="server">90.00</span></span></p>
					<p><span>积分：<span id="spMemPoint" runat="server">20000</span></span></p>
                    <p><a href="../member/surplusTimes.aspx">查看剩余计次<span style="font-family:'宋体';">&gt;&gt;</span></a></p>
					<a href="editMemberData.aspx"><img src="images/edit.png" /></a>
				</div>
			</div>
		</div>
		<div id="content" class="section">
			<div class="member-list section">
				<ul class="section">
					<li>
						<a href="#" id="bindCard">
							<img src="images/member01.png"/>
							<p>会员卡绑定</p>
						</a>
					</li>
					<li>
						<a href="myMember.aspx">
							<img src="images/member02.png"/>
							<p>我的会员卡</p>
						</a>
					</li>
					<li>
						<a href="pointExchange.aspx">
							<img src="images/member03.png"/>
							<p>积分兑换</p>
						</a>
					</li>
					<li>
						<a href="bill.aspx">
							<img src="images/member04.png"/>
							<p>账单记录</p>
						</a>
					</li>
					<li>
						<a href="rechargeOnline.aspx">
							<img src="images/member05.png"/>
							<p>在线充值</p>
						</a>
					</li>
					<li>
						<a href="modifyPassword.aspx">
							<img src="images/member06.png"/>
							<p>修改密码</p>
						</a>
					</li>
                    <li>
						<a href="moneyregion.aspx">
							<img src="images/member07.png"/>
							<p>红包大奖</p>
						</a>
					</li>
					<li>
						<a href="rotateregion.aspx" id="A1">
							<img src="images/member08.png"/>
							<p>幸运转盘</p>
						</a>
					</li>
					<li>
						<a href="memsign.aspx">
							<img src="images/member09.png"/>
							<p>每日签到</p>
						</a>
					</li>
                    <li>
						<a href="memberMoney.aspx">
							<img src="images/member10.png"/>
							<p>会员转账</p>
						</a>
					</li>
				</ul>
			</div>
		</div>
	</div>

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>
 <input type="hidden" id="txtUrl" runat="server" />
  <input type="hidden" id="txtLinkUrl" runat="server" />
    <input type="hidden" id="txtRechargeUrl" runat="server" />
</body>
</html>