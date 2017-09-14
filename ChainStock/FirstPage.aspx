<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FirstPage.aspx.cs" Inherits="ChainStock.FirstPage" %>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
	<title>嘉映映嘉会员管理系统</title>

	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/index.css">
        <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="Scripts/highcharts.js" type="text/javascript"></script>
    <script src="Scripts/grid.js" type="text/javascript"></script>    
    <script src="Scripts/FusionCharts/FusionCharts.js" type="text/javascript"></script>
    <script src="Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="Scripts/Module/System/FirstPage.js" type="text/javascript"></script>
    <script src="Scripts/Module/Common/Tab.js" type="text/javascript"></script>
    <script src="Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="Scripts/jquery.SuperSlide.2.1.js" type="text/javascript"></script>
    <script src="Scripts/script.js"></script>
</head>
<body>
	<div class="section" id="container">

		<div id="content" class="section f-left">
			<div class="section">
				<div class="section head-line">
					<a href="##"><i><img src="images/icon (22).png"></i>首页</a>
				</div>
				<div class="section admin-mesg">
					<div class="f-left admin-pic">
						<p><img src="images/head.png" id="spShopPhoto" runat="server"></p>
					</div>
					<div class="f-left admin-txt">
						<p id="spShopName" runat="server">总部</p>
						<p>操作权限：<span id="spGroupName" runat="server">运营商管理员</span></p><br/>
						<p>联系人：<span id="spShopContactMan" runat="server">张总经理</span></p>
						<p>电话：<span id="spShopTel" runat="server">075529755361</span></p>
						<p>地址：<span id="spShopAddress" runat="server">成都市高新区天府大道中段1388号美年广场A座649(腾讯斜对面）</span></p>
					</div>
				</div>
				<div class="section admin-data">
					<!-- 新增会员 -->
					<div class="f-left account-data new-member">
						<div class="account-head t-center">
							<h3><span><img src="images/icon (15).png" style="width:0.16rem;" /></span>新增会员</h3>
							<p><span><img src="images/icon (18).png"  id="imgMemberRate"  runat="server"/></span><a  style=" color:White;" id="spMemberRate"  runat="server">15.2%</a></p>
						</div>
						<div class="add-account t-center c-fff">
							<div class="f-left" style="border-right: 1px solid #fff;">
								<p>今日新增</p>
								<span id="spMemToday" runat="server">3</span>
							</div>
							<div class="f-left">
								<p>昨日新增</p>
								<span id="spMemYesterday" runat="server">12</span>
							</div>
						</div>
					</div>
					<!-- 充值金额 -->
					<div class="f-left account-data recharge-money">
						<div class="account-head t-center">
							<h3><span><img src="images/icon (16).png" style="width:0.16rem;"  /></span>充值金额</h3>
							<p><span><img src="images/icon (1).png" id="imgMoneyRate"  runat="server"/></span><a  style=" color:White;" id="spMoneyRate"  runat="server">15.2%</a></p>
						</div>
						<div class="add-account t-center c-fff">
							<div class="f-left" style="border-right: 1px solid #fff;">
								<p>今日新增</p>
								<span id="spMoneyToday" runat="server">3</span>
							</div>
							<div class="f-left">
								<p>昨日新增</p>
								<span id="spMoneyYesterday" runat="server">12</span>
							</div>
						</div>
					</div>
					<!-- 现金收入 -->
					<div class="f-left account-data in-money">
						<div class="account-head t-center">
							<h3><span><img src="images/icon (17).png" style="width:0.16rem;" /></span>现金收入</h3>
							<p><span><img src="images/icon (18).png"  id="imgCashRate"  runat="server"/></span><a  style=" color:White;" id="spCashRate"  runat="server">15.2%</a></p>
						</div>
						<div class="add-account t-center c-fff">
							<div class="f-left" style="border-right: 1px solid #fff;">
								<p>今日新增</p>
								<span id="spGetMoneyToday" runat="server">3</span>
							</div>
							<div class="f-left">
								<p>昨日新增</p>
								<span id="spGetMoneyYesterday" runat="server">12</span>
							</div>
						</div>
					</div>
					<!-- 12个月会员增长趋势 -->
               
					<div class="f-left growth-trend">
						<h3>12个月会员增长趋势</h3>
						<div class="growth-img bg-fff"  id="divMemContainer">
							<!-- 趋势图 -->
							<%--<img src="images/qushi.png"/>--%>
                                    <input type="text" id="txtMemStartTime" runat="server" />
                            <input type="text" id="txtMemEndTime" runat="server" />
                            <input type="hidden" id="sltShop" runat="server" />
						</div>
					</div>
					<!-- 系统公告 -->
					<div class="f-left system-notice bg-fff">
						<h3>系统公告</h3>
						<ul class="section">
                            <asp:Repeater ID="rptNotice" runat="server">
                            <ItemTemplate>
                            
                         
							<li>
								<i></i>
								 <a>
                                                        <%# Eval("SysNoticeTitle").ToString().Length > 20 ? Eval("SysNoticeTitle").ToString().Substring(0, 20) + "..." : Eval("SysNoticeTitle").ToString()%></a>
								<span>   <%# Eval("SysNoticeTime", "{0:yyyy.MM.dd}")%></span>
							</li>
							
                               </ItemTemplate>
                            </asp:Repeater>
						</ul>
                      
						<!-- 查看更多 -->
						<a href="SystemManage/SysNoticeList.aspx" class="the-more">查看更多&gt;&gt;</a>
					</div>
				</div>
			</div>
		</div>
	</div>



</body>
</html>