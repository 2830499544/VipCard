<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="memAddressList.aspx.cs" Inherits="ChainStock.mobile.member.memAddressList" %>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-选择收货地址</title>
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
				<h1>选择收货地址</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="memAddressManage.aspx" class="" id="sureBtn">管理</a>
			</div>
		</div>
		<div id="content" class="section" style="background-color:#f7f7f7;">
			<div class="section ex-integral">
            <asp:Repeater ID="rptMemAddress" runat="server">
                   <ItemTemplate>   
                 
				        <div class="section record-box selectMemAddressOk" >
                        <span class="addressid"><%#Eval("ID")%></span>
					        <p class="f-left t-left"><%#Eval("MemName")%></p>
					        <p class="f-right t-right"><%#Eval("MemMobile")%></p>
                            <p class="f-left t-left" style="width:100%;"> 
					
                             <span  <%#Eval("IsDefault").ToString()=="0"?"style=display:none":" " %>  ><span style="color:Orange"> [默认地址]</span></span><%#GetMemAddress(Eval("MemProvince"), Eval("MemCity"), Eval("MemCounty"), Eval("MemVillage"), Eval("MemDetailAddress"))%>
                             </p>
                              <%--<p class="f-right t-right">&nbsp;</p>--%>
                           
                               <%-- <input type="checkbox" value="<%#Eval("ID")%>"  class="setDefautAddress" onclick="check(this)"  <%#Eval("IsDefault").ToString()=="1"?"checked=checked":"" %> />--%>
                                 
				        </div>
                      
                  </ItemTemplate>
                </asp:Repeater>

               
		
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
    <span id="txtGiftList" runat="server" style="display:none;"></span> 
     <input type="hidden" id="txtMemID" runat="server" />
<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>
<script>


    var GiftList = new Array();
    $(function () {

        var string = getUrlParam('GiftList');
        string = decodeURIComponent(string);
    
        $("#txtGiftList").html(string);
        GiftList = JSON.parse(string);

    });
    $(".selectMemAddressOk").click(function () {

        var id = $(this).find(".addressid").html();
     
      //  var GiftList = $("#txtGiftList").html();
        var str = JSON.stringify(GiftList);
        str = escape(str);

        var url = "pointExchangeSure.aspx?GiftList=" + str + "&AddressID=" + id;
        location.href = url;

    });
    
    //获取url中的参数
    function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg); //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }

   
</script>
</body>
</html>