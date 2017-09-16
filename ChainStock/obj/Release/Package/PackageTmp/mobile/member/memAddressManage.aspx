<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="memAddressManage.aspx.cs" Inherits="ChainStock.mobile.member.memAddressManage" %>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-管理收货地址</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
	<link rel="stylesheet" type="text/css" href="css/media.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
    <style>
        .record-box > p{
            border-bottom:none;
            font-size:0.24rem;
        }
        .record-box{
            border-bottom:1px solid #e5e5e5;
        }
    </style>
</head>
<body>
	<div class="section index" id="container">
		<div id="head" class="section">
			<div class="section header">
				<h1>管理收货地址</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="##" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
			</div>
		</div>
		<div id="content" class="section" style="background-color:#f7f7f7;">
			<div class="section ex-integral">
            <asp:Repeater ID="rptMemAddress" runat="server">
                   <ItemTemplate>   
				        <div class="section record-box">
					        <p class="f-left t-left"><%#Eval("MemName")%></p>
					        <p class="f-right t-right"><%#Eval("MemMobile")%></p>
                            <p class="f-left t-left" style="width:100%;"> 
					
                             <%#GetMemAddress(Eval("MemProvince"), Eval("MemCity"), Eval("MemCounty"), Eval("MemVillage"), Eval("MemDetailAddress"))%>
                             </p>
                              
                              <p class="f-left t-left" id="spDefault" runat="server">
                                <label style="font-weight:100;color:#323232;">
                                <span class="addressid" style=" display:none;"><%#Eval("ID")%></span>
                                <input type="checkbox" value="<%#Eval("ID")%>"  class="setDefautAddress" onclick="check(this)"  <%#Eval("IsDefault").ToString()=="1"?"checked=checked":"" %> />
                                 
                                    <span>默认地址</span>
                                </label>
                            
                            </p>
					        <p class="f-right t-right"><a href="memAddressInfo.aspx?AddressID=<%#Eval("ID")%>">编辑</a>
                            &nbsp;&nbsp;&nbsp;&nbsp;<a href="#" onclick='<%# string.Format(" DeleteAddressInfo(\"{0}\")",Eval("id")) %>' >删除</a></p>
				        </div>
                  </ItemTemplate>
                </asp:Repeater>

                <div class="section login_form" style="padding:0 0.1rem;">
					<form class="section" method="" accept="" name="">
						<div class="section form_btn">
							<a href="memAddressInfo.aspx" id="newAddress">添加新地址</a>
						</div>
					</form>
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

</body>
</html>