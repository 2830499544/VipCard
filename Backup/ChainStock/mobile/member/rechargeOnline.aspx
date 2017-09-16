<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rechargeOnline.aspx.cs" Inherits="ChainStock.mobile.member.rechargeOnline" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-在线充值</title>
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
				<h1>在线充值</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<!-- 跳转到微官网(按需使用) -->
				<a href="##" class="head-icon" id="inWeb"><img src="images/web.png"/></a>
				<!-- 跳转到微商城(按需使用) -->
				<!-- <a href="##" class="head-icon" id="inStore"><img src="images/store.png"/></a> -->
			</div>
		</div>
		<div id="content" class="section">
			<div class="section line_box">
				<%--<div class="line_group">
					<p class="f-left">订单号</p>
					<div class="f-right">
						<p id="spOrderAccount" runat="server">12960003600</p>
					</div>
				</div>--%>
				<div class="line_group">
					<p class="f-left">会员卡号</p>
					<div class="f-right">
						<p id="spMemCard" runat="server">ZL26875003</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">会员姓名</p>
					<div class="f-right">
						<p id="spMemName" runat="server">欧阳娜娜</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">充值金额</p>
					<div class="f-right">
						<input type="text"  id="money" placeholder="请输入充值金额" maxlength="8" />
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">赠送金额</p>
					<div class="f-right">
						<p id="givemoney">0.00</p>
					</div>
				</div>
                <div class="line_group">
					<p class="f-left">可得积分</p>
					<div class="f-right">
						<p id="point">0.00</p>
					</div>
				</div>
				<!-- 确认充值跳转到微信充值，充值成功后跳转到rechargeSuccess.html页面 -->
				<div class="section login_form form_btn">
					<a href="#" id="onlineRechargeOk">确认充值</a>
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
	</div><input type="hidden" id="txtPointRate" runat="server" />
    <input type="hidden" id="txtMemID" runat="server" />
     <input type="hidden" id="txtMaxMoney" runat="server" />

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>
    <script src="../../Scripts/Module/Common/Common.js" type="text/javascript"></script>
   

 <script type="text/javascript">

     $(document).ready(function () {
      
         $("#money").bind("keyup", getPoint);
     });
     // 检测文本框输入只能是正数Float类型
     function isFloatPositive(jQuerySelector) {

         var numFloatPositive = /^\d+(\.\d+)?|[0-9]*[1-9][0-9]*$/; //^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$正浮点数(8位小数)^\d+(\.\d+)?$

         //确保用户输入的是正实数
         if (isNaN($(jQuerySelector).val())) { $(jQuerySelector).val(''); return false; }
         if (!numFloatPositive.test($(jQuerySelector).val()) && $(jQuerySelector).val() != '') {
             document.execCommand('undo'); //确保用户输入的是正实数
             return false;
         }
         return true;
     }

     function getPoint() {
         if (isFloatPositive(this)) {
             var rechargepointrate = parseFloat($("#txtPointRate").val()); //比例            
             
             //计算赠送金额
             if ($("#money").val() != "") {

                 $.ajax({
                     url: "../../Service/AjaxService.ashx?Method=GetGiveMoney",
                     data: {
                         "RechargeMoney": $("#money").val()

                     },
                     type: "POST",
                     dataType: "json",
                     contentType: "application/x-www-form-urlencoded; charset=utf-8",
                     success: function (data) {
                         $("#givemoney").html(data);
                     }
                 });
             }
             else {
                 $("#givemoney").html(0);
             }
             //计算积分
             if (rechargepointrate == "0") {
                 $("#point").val(0);
             } else {
                 if ($("#money").val() != "") {
                    
                     $("#point").html(parseInt($("#money").val() / rechargepointrate));
                 }
             }
             
         }
         else {
             alert("充值金额格式错误，请重新输入");
         }

     }


     $("#onlineRechargeOk").click(function () {
         var memid = $("#txtMemID").val();
         if (memid == "") {
             alert("未获取到会员信息，请重新登录！");
             location.href = "login.aspx";
         }
         var money = $("#money").val();
         if (money == "") {
             alert("请输入充值金额！");
             return;
         }
         if (parseFloat(money) == 0) {
             alert("充值金额不能为0！");
             return;
         }
         var givemoney = $("#givemoney").html();

         var point = $("#point").html();
       
         location.href = "rechargeOnlineSure.aspx?MemID=" + memid + "&money=" + money + "&givemoney=" + givemoney + " &point=" + point;
        
     });

</script>

</body>
</html>