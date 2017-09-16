<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ChainStock.index" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>商家联盟系统</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
    <!--[if lt IE 9]>
    <script>
        alert('请注意：\n本系统不支持IE6-8，请使用谷歌、火狐等浏览器\n或360、QQ等国产浏览器的极速模式使用本系统！');
        window.location.href = 'login.aspx';
    </script>
    <![endif]-->
</head>
<body>
	<div class="section" id="container">
		<div class="f-left" id="aside">
			<div class="logo">
    
				<a href="##"><img src="images/logo.png" alt="商家联盟系统" /></a>
			</div>
			<!-- 左边导航 -->
        
			<ul class="section aside-nav">
                <asp:Repeater ID="rptFirstMenu" runat="server" OnItemDataBound="rptFirstMenu_ItemDataBound">
                <ItemTemplate>
                
           
				<li class="section">
					<div class="section main-nav">
						<i><img src='<%#Eval("ModuleIcoPath") %>'/></i>
						<p><%#Eval("ModuleCaption") %></p>
						<span><img src="images/icon (3).png"/></span>
					</div>
					<div class="section min-nav">
                     <asp:Repeater ID="rptSecondMenu" runat="server">
                   <ItemTemplate>
						<a href='<%#Eval("ModuleLink") %>' class="" target="main"><%#Eval("ModuleCaption") %></a>
					
                 </ItemTemplate>
                </asp:Repeater>
					</div>
				</li>
                     </ItemTemplate>
            </asp:Repeater>
			
			</ul>
		</div>
		<div id="content" class="section f-left">
			<div class="section header">
				<div class="f-right head-nav">
					<ul class="f-left">
						<li>
							<a href="FirstPage.aspx" target="main"><i><img src="images/icon (12).png"></i>返回首页</a>
						</li>
						<li>
							<a href="Common/TodayRemind.aspx?PID=93" target="main" ><i><img src="images/icon (19).png"></i>今日提醒<span id="spRemindCount" runat="server">2</span></a>
						</li>
						<li>
							<a href="MicroWebsite/Opinion.aspx"  target="main"><i><img src="images/icon (20).png"></i>留言中心<span style="background-color:#ff6c00;" id="spMessageCount" runat="server">0</span></a>
						</li>
						<li>
							<a href="##" onclick="LoginOut()"><i><img src="images/icon (21).png" ></i>退出登录</a>
						</li>
					</ul>
					<div class="f-left head-admin">
						<div class="f-left admin-img"><a href="##"><img src="images/head.png"  id="spShopPhoto" runat="server"/></a></div>
						<a href="##" class="admin-name"  id="spUserName" runat="server">2<span></span></a>
					</div>
				</div>
			</div>
			<div class="section iframe-box">
				<!-- 框架页面 -->
				<iframe class="s_iframe" name="main" id="main" width="100%" height="100%" src="FirstPage.aspx" frameborder="0" data-id="FirstPage.aspx" seamless="">
				</iframe>
			</div>
		</div>
	</div>
        <input id="chkIsTel" runat="server" type="checkbox" style="display: none" />
    <input id="chkTelNoMember" runat="server" type="checkbox" style="display: none" />
        <asp:Literal ID="litObject" runat="server" />
<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>

 <script src="Scripts/Module/System/index.js" type="text/javascript"></script>
</body>
</html>