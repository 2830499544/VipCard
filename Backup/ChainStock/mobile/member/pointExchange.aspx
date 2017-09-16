<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pointExchange.aspx.cs" Inherits="ChainStock.mobile.member.pointExchange" %>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-积分兑换</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
	<link rel="stylesheet" type="text/css" href="css/media.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
</head>
<body>
	<div class="section index" id="container" style="margin-bottom:1.6rem;">
		<div id="head" class="section">
			<div class="section header">
				<h1>积分兑换</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="##" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
			</div>
		</div>
		<div id="content" class="section" style="background-color:#f7f7f7;">
			<div class="section">
                <div class="section in-search">
					<input type="text" id="txtKey" placeholder="请输入礼品的名称"  runat="server"/>
					<a href="#"><img src="images/search.png"  alt="搜索" id="searchGift"  /></a>
				</div>
				<div class="section">
					<!-- 显示会员账户内积分 -->
					<div class="integral-num">
						<div class="section">
							<h3>可用积分</h3>
							<p class="in-total" id="spMemPointTotal" runat="server">20000</p>       
                            <div class="my-can" style="float:right;padding:0;">
                             <a href="pointExchangeRecord.aspx">兑换记录</a>
                            </div>                     
						</div>
					</div>
					<!-- 动态显示会员当前兑换后积分 -->
					<%--<div class="integral-num dis-n">
						<div class="section">
							<h3>剩余积分</h3>
							<p class="in-balance" id="spMemPoint" runat="server">20000</p>
						</div>
					</div>--%>
				</div>

				

				<div class="section bg-fff">
					<!-- 礼品分类 -->
					<div class="f-left gift-nav">
						<ul class="section">
                        	<li>
								<a href="pointExchange.aspx" class="" id="allclass" runat="server" >所有分类</a>
							</li>
								<asp:Repeater ID="rptClass" runat="server" OnItemDataBound="rptClass_ItemDataBound">
                                <ItemTemplate>   
                                      
							<li>
								<a id="classname"  runat="server" href='<%#"pointExchange.aspx?ClassID="+Eval("GiftClassID")%>'  ><%#Eval("GiftClassName") %></a>
							</li>
							 </ItemTemplate>
                             </asp:Repeater>
						</ul>
					</div>
					<div class="section f-right gift-list">
						
						<!-- 礼品列表 -->
						<ul class="section">
                        	<asp:Repeater ID="rptGift" runat="server">
                                <ItemTemplate>   
							<li>
								<div class="f-left gift-img">

                                <img src='../..<%#Eval("GiftPhoto")%>'/>
                                   
								</div>
								<div class="f-left gift-txt">
									<h3><%#Eval("GiftName")%></h3>
									<p>积分：<span class="gift-integral"><%#Eval("GiftExchangePoint")%></span></p>
								</div>
								<!-- 增减控制器 -->
								<div class="buy-ctrl">
									<a href="##" class="reduceBtn"><img src="images/reduce.png" /></a>
                                    	<span  class="giftid"  ><%#Eval("GiftID")%></span>
                                        	<span  class="giftimage"><%#Eval("GiftPhoto")%></span>
                                        <span  class="giftname"  ><%#Eval("GiftName")%></span>
                                        <span  class="giftpoint"  ><%#Eval("GiftExchangePoint")%></span>
									<!-- 这里默认为0 -->
									<input type="text" value="0" class="giftTxt" />
									<a href="##" class="plusBtn"><img src="images/plus.png" /></a>
									<a href="##" class="plusShow"><img src="images/plus1.png" /></a>
								</div>
							</li>
						</ItemTemplate>
                        </asp:Repeater>
							<a href="##"  id="moreGift" class="moreGift" runat="server">加载更多...</a>
						</ul>
					</div>
				</div>

				<div class="gift-fix">
					<p>共<span class="gift-num">0</span>件</p>
					<h3>合计积分：<span class="gift-total">0</span></h3>
					<a href="#" id="pointExchangeOk">立即兑换</a>
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
 <input type="hidden" id="txtMemID" runat="server" />

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>

<script type="text/javascript" src="scripts/script.js"></script>


</html>