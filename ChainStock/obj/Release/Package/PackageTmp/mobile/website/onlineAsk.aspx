<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="onlineAsk.aspx.cs" Inherits="ChainStock.mobile.website.onlineAsk" %>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,maximum-scale=1.0,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微官网-在线咨询</title>
 
    <script src="scripts/jquery-2.1.4.min.js" type="text/javascript"></script>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
	<link rel="stylesheet" type="text/css" href="css/media.css">

 

  
</head>
<body>
	<div class="section" id="container">
		

		<div id="head" class="section">
			<h1>在线咨询</h1>
			<!-- 返回上一页 -->
			<a href="index.aspx"   style="	position: absolute;	left: 0.2rem;	top: 0;	width: 0.24rem;"><img src="images/prev.png" /></a>
			<!-- 跳转到微会员 -->
			<a href="javascript:void(0);" class="head-icon" id="inMember"><img src="images/w-member.png"/></a>
		</div>
		<div id="content" class="section">
			<div class="section online-ask">

				<%--<div class="t-center ask-more"><a href="#" id="moreMessage" runat="server" class="moreMessage">查看更多记录</a></div>--%>

                <asp:Repeater ID="rptOnlinAsk" runat="server"  OnItemDataBound="rptOnlinAsk_ItemDataBound">
                <ItemTemplate>
                
             

				<div class="t-center ask-time" runat="server" id="divTime"><span><%#DateTime.Parse(Eval("MessageTime").ToString()).ToString("yyyy-MM-dd HH:mm:ss")%></span></div>

                <!-- 客户对话框（右边） -->
				<div class="ask-box customer-ask" runat="server" id="divMessage">
					<!-- 客户头像 -->
					<div class="f-right ask-img customer-img">
						<img src="images/head.png"/>
					</div>
					<!-- 客户咨询 -->
					<div class="f-right ask-talk customer-talk">
						<p><%#Eval("MessageContent")%></p>
					</div>
				</div>

                 	<!-- 商家对话框（左边） -->
				<div class="ask-box self-answer" id="divReply" runat="server">
					<!-- 商家头像 -->
					<div class="f-left ask-img self-img">
						<img src="images/head01.png"/>
					</div>
					<!-- 商家回答 -->
					<div class="f-left ask-talk self-talk">
						<p><%#Eval("MessageContent")%></p>
					</div>
				</div>

			

				   </ItemTemplate>
                </asp:Repeater>

               


             <%--   <div class="t-center ask-more"><a href="javascript:void(0);">查看更多记录</a></div>--%>
			<%--	<div class="t-center ask-time"><span>2016-10-12 14:20:45</span></div>--%>
			

			<!-- 客户对话框（右边） -->
				

                <div id="spHtml" class="spHtml"></div>
		

				<!-- 客户输入框 -->
				<div class="input-in">
					<textarea placeholder="在此输入内容"  runat="server" id="txtMessage"></textarea>
                   
					<a  id="sendMessage" class="sendMessage">
                        发送                    
                    </a>
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
    <input id="txtMemID" type="hidden"  runat="server"/>
     <input id="txtMemCard" type="hidden"  runat="server"/>
<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>
   <script language="JavaScript">

       setTimeout('myrefresh()', 5000); //指定1秒刷新一次
        </script>
</body>
</html>