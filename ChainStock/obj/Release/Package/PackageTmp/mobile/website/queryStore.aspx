<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="queryStore.aspx.cs" Inherits="ChainStock.mobile.website.queryStore" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,maximum-scale=1.0,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微官网-门店查询</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
	<link rel="stylesheet" type="text/css" href="css/media.css">
</head>
<body>
	<div class="section" id="container">
		
		<div id="head" class="section">
			<h1>门店查询</h1>
			<!-- 返回上一页 -->
			<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png" /></a>
			<!-- 跳转到微会员 -->
			<a href="javascript:void(0);" class="head-icon" id="inMember"><img src="images/w-member.png"/></a>
		</div>
		<div id="content" class="section">
			<div class="section store-query">
				<!-- 搜索输入框 -->
				<div class="section store-search">
					<input type="text" placeholder="请输入商家名称进行搜索" runat="server"  id="txtKey"/>
					<a href="javascript:void(0);" class="searchShop"><img src="images/search.png"/></a>
				</div>
              
				<!-- 三级城市查询 -->
				<div class="section" id="province">
                <select class='province' id="sltProvince" runat="server">
           
                </select>
                <select class='city1' id="sltCity"  runat="server">
                  <option value="">请选择</option>
           
                </select>
                <select class='city2' id="sltCounty"  runat="server">
             <option value="">请选择</option>
                </select>
                    <input id="txtPID" type="hidden" runat="server" />
                       <input id="txtCID" type="hidden" runat="server"  />
                          <input id="txtCYID" type="hidden" runat="server" />
					<a href="javascript:void(0);" class="okSearchShop">确定</a>
				</div>
				<!-- 商家列表 -->
				<div class="section store-list">
					<ul class="section">

                        <asp:Repeater ID="rptShop" runat="server">
                     <ItemTemplate>
						<li>
							<div class="f-left store-img">
								<!-- 图片尺寸为180*160 -->
								<img src='../<%#Eval("ShopImageUrl") %>'/>
							</div>
							<div class="f-right store-mesg">
								<h3><%#Eval("ShopName") %></h3>
								<p>
									<span>电话：<%#Eval("ShopTelephone")%></span>
									<a href="tel:<%#Eval("ShopTelephone")%>" class="call-phone"><img src="images/phone-c.png"/></a>
								</p>
								<p>
									<span>地址：<%#BindAddress(Eval("ShopID"))%></span>
									<a href="map.aspx?ShopID=<%#Eval("ShopID") %>" class="query-map"><img src="images/ad-c.png"/></a>
								</p>
							</div>
						</li>
                        </ItemTemplate>
                        </asp:Repeater>
				
						<a href="javascript:void(0);" class="moreShop" id="moreShop" runat="server">点击获取更多商家...</a>
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
<script>
    
      
        $("#sltProvince").bind("change", Province);
        $("#sltCity").bind("change", City);
    
 
</script>

</body>
</html>