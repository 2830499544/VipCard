<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="memExpenseRecord.aspx.cs" Inherits="ChainStock.mobile.member.memExpenseRecord" %>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-消费记录</title>
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
				<h1>消费记录</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="##" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
			</div>
		</div>
		<div id="content" class="section" style="background-color:#f7f7f7;">
			<div class="section ex-integral">
            <asp:Repeater ID="rptExchange" runat="server" OnItemDataBound="rptOrder_ItemDataBound">
                   <ItemTemplate>   
				<div class="section record-box">
                <div style="font-size:10pt; line-height:20px;">订单号：<%#Eval("OrderAccount")%></div>
            <div style="font-size:10pt; line-height:20px;">下单时间：<%#DateTime.Parse(Eval("OrderCreateTime").ToString()).ToString("yyyy-MM-dd HH:mm:ss")%></div>
              <div style="font-size:10pt; line-height:20px;">会员姓名：<%#GetMemName(Eval("OrderMemId"))%></div>
					   <div style="font-size:10pt; line-height:20px;">订单金额：￥<%#Eval("OrderDiscountMoney","{0:f2}")%></div>
                     <div style="font-size:10pt; line-height:20px;">获得积分：<%#Eval("OrderPoint")%></div>
					<ul class="section">
                           <asp:Repeater ID="rptOrderDetail" runat="server">
                   <ItemTemplate>             
                    
						<li>
							<div class="f-left record-img">
									<h3><%#Eval("Name")%></h3>
							</div>
							<h3>￥<%#Eval("OrderDetailPrice","{0:f2}")%></h3>
							<span>x<%#Eval("OrderDetailNumber")%></span>
							<p>￥<%#Eval("OrderDetailDiscountPrice", "{0:f2}")%></p>
                           
						</li>
					   </ItemTemplate>
					</asp:Repeater>
						
					</ul>

                 
				</div>
                  </ItemTemplate>
                </asp:Repeater>
       
		
			</div>
                
		</div>
         	<a href="##"  runat="server" id="moreExpense" class="moreExpense" style=" font-size:12pt">展开全部</a> 
		<!-- 底部浮动导航 -->
			<!-- 底部浮动导航 -->
		      <div class="foot-nav">
            <div class="f-left fix-nav fix-home">
                <a href="index.aspx">
                    <img src="images/home.png" /></a>
            </div>
       <%--     <div class="f-left fix-nav fix-ch">
                <a href="memberCard.aspx">
                    <p>会员办卡</p>
                </a>
            </div>
            <div class="f-left fix-nav fix-ch">
                <a href="##">
                    <p>收银记账</p>
                    <img src="images/icon.png" /></a>
              <div class="foot-more">
                    <a href="fastConsumption.aspx">快速消费</a>
                    <a href="goodsConsumption.aspx">商品消费</a>
                    <a href="timesConsumption.aspx">计次消费</a>
                </div>
            </div>--%>
            <div class="f-left fix-nav fix-ch">
                <a href="bossCenter.aspx">
                    <p>老板中心</p>
                </a>
            </div>
        </div>
	</div>

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>

</body>
</html>