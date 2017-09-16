<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ChainStock.mobile.website.index" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,maximum-scale=1.0,user-scalable=no" />
    <meta name="keywords" content="智络微官网"/>
    <meta name="description" content="智络微官网"/>
	<title>微官网-首页</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
	<link rel="stylesheet" type="text/css" href="css/media.css">
</head>

<body>
	<div class="section index" id="container" runat="server" style="">
		<div id="head" class="section">
			<h1>微官网</h1>
			<!-- 跳转到微会员 -->
			<a href="../member/index.aspx" class="head-icon" id="inMember"><img src="images/w-member.png"/></a>
		</div>
		<div id="content" class="section">
			<ul class="section index-list">
				<li>
					<a href="productShow.aspx" class="section">
						<img src="images/index01.png"/>
						<p>产品展示</p>
					</a>
				</li>
				<li>
					<a href="active.aspx" class="section">
						<img src="images/index02.png"/>
						<p>优惠活动</p>
					</a>
				</li>
				<li>
					<a href="queryStore.aspx" class="section">
						<img src="images/index03.png"/>
						<p>门店查询</p>
					</a>
				</li>
				<li>
					<a href="photos.aspx" class="section">
						<img src="images/index04.png"/>
						<p>相册</p>
					</a>
				</li>
				<li>
					<a href="onlineAsk.aspx" class="section">
						<img src="images/index05.png"/>
						<p>在线咨询</p>
					</a>
				</li>
				<li>
					<a href="news.aspx" class="section">
						<img src="images/index06.png"/>
						<p>新闻动态</p>
					</a>
				</li>
			</ul>
		</div>
	</div>

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>

</body>
</html>