<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pointExchangeRecord.aspx.cs" Inherits="ChainStock.mobile.member.pointExchangeRecord" %>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-积分兑换记录</title>
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
				<h1>积分兑换记录</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="##" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
			</div>
		</div>
		<div id="content" class="section" style="background-color:#f7f7f7;">
			<div class="section ex-integral">
            <asp:Repeater ID="rptExchange" runat="server" OnItemDataBound="rptExchange_ItemDataBound">
                   <ItemTemplate>   
				<div class="section record-box">
					<p class="f-left t-left"><%#DateTime.Parse(Eval("ApplicationTime").ToString()).ToString("yyyy-MM-dd HH:mm:ss")%></p>
					<p class="f-right t-right">订单号：<%#Eval("ExchangeAccount")%></p>
					<ul class="section">
                           <asp:Repeater ID="rptExchangeDetail" runat="server">
                   <ItemTemplate>             
                    
						<li>
							<div class="f-left record-img">
								<img src="../..<%#Eval("GiftPhoto")%>"/>
							</div>
							<h3><%#Eval("GiftName")%></h3>
							<span>x<%#Eval("ExchangeNumber")%></span>
							<p>-<%#Eval("ExchangePoint")%></p>
						</li>
					   </ItemTemplate>
					</asp:Repeater>
						
					</ul>
				</div>
                  </ItemTemplate>
                </asp:Repeater>
			<a href="##"  runat="server" id="moreExchange" class="moreExchange">展开全部</a>
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