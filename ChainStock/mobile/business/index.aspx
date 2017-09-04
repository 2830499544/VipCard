<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ChainStock.mobile.business.index" %>

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
	<link rel="stylesheet" type="text/css" href="css/common.css" />
	<link rel="stylesheet" type="text/css" href="css/style.css"/>
	<link rel="stylesheet" type="text/css" href="css/media.css"/>
	<link rel="stylesheet" type="text/css" href="css/color.css"/>

</head>
<body>
    <div class="section index" id="container" style="margin-bottom:0;">
		<div id="head" class="section" style="background: #16a2de 100% 100% no-repeat;">
			<div class="section header">
				<h1>商家中心</h1>
				<a href="javascript:void(0);" class="logout"><img src="images/logout.png"/></a>
			</div>
			<div class="section member-mesg">
				<div class="f-left c-fff head-mesg">
					<h3>店铺名称：<span id="spShopName"  runat="server">0</span></h3>
					<p>新增会员：今日<span id="spMemToday"  runat="server">0</span>人</p>
					<p>充值金额：今日￥<span id="spMoneyToday" runat="server">0</span></p>
				</div>
			</div>
		</div>
		<div id="content" class="section">
			<div class="member-list section">
				<ul class="section">
					<li runat="server" id="liExpense2">
						<a href="memberCard.aspx">
							<img src="images/member10.png"/>
							<p>会员办卡</p>
						</a>
					</li>
					<li runat="server" id="liExpense">
                 
						<a href="fastConsumption.aspx" >
                        
							<img src="images/member11.png"/>
							<p>快速消费</p>
                          
						</a>
                       
					</li>
					<li runat="server" id="liExpense1">
						<a href="goodsConsumption.aspx">
							<img src="images/member12.png"/>
							<p>商品消费</p>
						</a>
					</li>
					<li runat="server" id="liExpense3">
						<a href="memExpenseRecord.aspx">
							<img src="images/member13.png"/>
							<p>消费记录</p>
						</a>
					</li>
					<li>
						<a href="bossCenter.aspx">
							<img src="images/member14.png"/>
							<p>老板中心</p>
						</a>
					</li>
                 
				</ul>
			</div>
		</div>
	</div>
<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>
</body>
</html>
