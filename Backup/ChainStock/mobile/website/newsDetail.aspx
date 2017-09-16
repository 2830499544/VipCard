<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newsDetail.aspx.cs" Inherits="ChainStock.mobile.website.newsDetail" %>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,maximum-scale=1.0,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微官网-新闻详情</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
	<link rel="stylesheet" type="text/css" href="css/media.css">
</head>
<body>
	<div class="section" id="container">
		
		<div id="head" class="section">
			<h1>新闻详情</h1>
			<!-- 返回上一页 -->
			<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png" /></a>
			<!-- 跳转到微会员 -->
			<a href="javascript:void(0);" class="head-icon" id="inMember"><img src="images/w-member.png"/></a>
		</div>
		<div id="content" class="section bg-fff">
			<div class="section news-detail">
				
				<div class="section news-d-head">
					<!-- 这里是新闻详情标题及时间 -->
					<h2 runat="server" id="spNewsName"></h2>
					<span><span runat="server" id="spNewsCreateTime"></span></span>
				</div>

				<div class="section news-d-con">
					<span runat="server" id="spNewsDesc"></span>
				</div>

				<!-- 推荐新闻 -->
				<div class="section news-reletive">
					<h3>推荐新闻</h3>
					<ul class="section news-re-list">
                      <asp:Repeater ID="rptNews" runat="server">
                    <ItemTemplate>
						<li><a href="newsDetail.aspx?NewsID=<%#Eval("NewsID")%>"><%#Eval("NewsName")%></a></li>
						
                        </ItemTemplate>
                        </asp:Repeater>
					</ul>
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
				<a href="productShow.aspx"><p>产品展示</p></a>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="active.aspx"><p>优惠活动</p></a>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="queryStore.aspx"><p>门店查询</p></a>
			</div>
			<!-- 更多菜单 -->
			<div class="f-left fix-nav fix-more">
				<a href="javascript:void(0);"><p>更多</p><img src="images/icon.png"/></a>
			</div>
			<!-- 更多菜单弹出项 -->
			<div class="fix-more-nav">
				<a href="onlineAsk.aspx">在线咨询</a>
				<a href="photos.aspx">相册</a>
				<a href="news.aspx">动态</a>
			</div>
		</div>
	</div>

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>

</body>
</html>