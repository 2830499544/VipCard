<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pointExchangeSure.aspx.cs" Inherits="ChainStock.mobile.member.pointExchangeSure" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-确认订单信息</title>
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
				<h1>确认订单信息</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<a href="##" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
			</div>
		</div>
		<div id="content" class="section" style="background-color:#f7f7f7;">
			<div class="section line_box">
                <%--	<div class="line_group">   
                  <div class="section my-can"><a id="selectMemAddressList">选择收货地址</a></div>
         
             </div>--%>
				 <h3 class="order-head">收货人信息<span id="txtGiftList" runat="server" style=" display:none;"></span> </h3>
                <%-- 若没有默认地址出现此新增按钮 --%>
                <div class="section login_form" style="padding:0 0.1rem; ">
					<form class="section">
						<div class="section form_btn">
							<a href="#" id="addNewAddress" runat="server">添加收货地址</a>
                           
						</div>
					</form>
				</div>
               
                <%-- 有默认收货信息则显示下面信息 --%>
				<div class="line_group">
					<p class="f-left">收货人姓名</p>
					<div class="f-right">
						<%--<input type="text" id="memname" runat="server" placeholder="输入收货人姓名" value="" />--%>
                        <p id="memname" runat="server">张三</p>
					</div>
				</div>
				<div class="line_group " >
					<p class="f-left">手机号</p>
					<div class="f-right">
						<%--<input type="text" id="mobile"  runat="server"  placeholder="输入手机号码" value="" />--%>
                        <p  id="mobile" runat="server" >13988886666</p>
					</div>
				</div>
				<div class="line_group selectMemAddressList">
					<p class="f-left">收货地址</p>
					<div class="f-right">
                        <span style="float:right;width:9%"><img src="images/right_d.png" style="width:0.14rem;"/></span>
                        <p id="address" runat="server" style="width:90%;">这里显示会员默认收货地址</p>
                        
						<%--<!-- 会员填写的地址显示在这里 -->
						<p class="show-address dis-n"></p>
						
						<a href="javascript:void(0);" class="line_choice queryCity" id="">
							<p>这里显示会员默认收货地址（新增地址）</p>
							<span><img src="images/right_d.png" /></span>
						</a>
						<!-- 城市选择，地址填写 -->
						<div class="dis-n city-mode">
							<div class="city-query">
								<select>
									<option value="请选择">请选择</option>
								</select>
								<select>
									<option value="请选择">请选择</option>
								</select>
								<select>
									<option value="请选择">请选择</option>
								</select>                               
								<div class="section ad-detail">
									<input type="text" placeholder="请填写详细地址"/>
									<a href="##" class="ad-sure">确定</a>
								</div>
							</div>
						</div>--%>
                        
					</div>
				</div>
			</div>
			<div class="section ex-integral">
				<div class="section record-box">
					<p class="f-right t-right" style="width:100%;">订单号：<span id="spOrderAccount" runat="server"></span></p>
					<ul class="section" id="tableExchange">
					  <!--这里动态加载列表--->
			
					</ul>
				</div>
                <div class="section record-box" style="display:none;">
					<p>共<span class="gift-num">0</span>件</p>
					<h3>合计积分：<span class="gift-total">0</span></h3>
					
				</div>
             
				<div class="section login_form" style="padding:0 0.1rem;">
					<form class="section" method="" accept="" name="">
						<div class="section form_btn">
							<a href="#" id="pointExchangeSure">确认订单</a>
						</div>
					</form>
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
      <input type="hidden" id="txtAddressID" runat="server" />
<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>

<script>

    var GiftList = new Array();

    $(function () {
        
        var string = getUrlParam('GiftList');


        string = decodeURIComponent(string);

        $("#txtGiftList").html(string);

        GiftList = JSON.parse(string);
    

        CreateExchangeGiftTable(GiftList);
    });
    $(".selectMemAddressList").click(function () {

        //  var GiftList = $("#txtGiftList").html();
        var str = JSON.stringify(GiftList);
        str = escape(str);
        var url = "memAddressList.aspx?GiftList=" + str + "";
        location.href = url;

    });
    //获取url中的参数
    function getUrlParam(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
        var r = window.location.search.substr(1).match(reg); //匹配目标参数
        if (r != null) return unescape(r[2]); return null; //返回参数值
    }

    function CreateExchangeGiftTable(GiftList) {

        var point = 0;
        var count = 0;
        var strHtml = "";
        for (var i = 0; i < GiftList.length; i++) {
            strHtml += "<li><div class='f-left record-img'><img src='../.." + GiftList[i].image + "'/>";
            strHtml += "</div><h3>" + GiftList[i].name + "</h3>";
            strHtml += "<span>x" + GiftList[i].count + "</span>";
            strHtml += "<p>-" + GiftList[i].point + "</p>";
            strHtml += "</li>";
            count +=parseInt(GiftList[i].count);
            point += parseInt(GiftList[i].point);
        }
        $(".gift-num").html(count);
        $(".gift-total").html(point);

        $("#tableExchange").html(strHtml);
    }
</script>
</body>
</html>