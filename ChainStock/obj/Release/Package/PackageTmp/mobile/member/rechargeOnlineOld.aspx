<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rechargeOnlineOld.aspx.cs" Inherits="ChainStock.mobile.member.rechargeOnlineOld" %>

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
    <input type="hidden" id="txtOpenID" runat="server" />
    <input type="hidden" id="txtMemID" runat="server" />
     <input type="hidden" id="txtMaxMoney" runat="server" />
     <input type="hidden" id="appid" runat="server" />
    <input type="hidden" id="timestamps" runat="server" />
    <input type="hidden" id="nonceStrs" runat="server" />
    <input type="hidden" id="signatures" runat="server" />
    <input type="hidden" id="signTypes" runat="server" />
    <input type="hidden" id="paySigns" runat="server" />
    <input type="hidden" id="package" runat="server" />
        <input type="hidden" id="txtNow" runat="server" />
<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>

 <script type="text/javascript" charset="UTF-8" src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>

 <script type="text/javascript">
     $(function () {
         wx.config({
             debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            
             appId: $("#appid").val(), // 必填，公众号的唯一标识
             timestamp: $("#timestamps").val(), // 必填，生成签名的时间戳
             nonceStr: $("#nonceStrs").val(), // 必填，生成签名的随机串
             signature: $("#signatures").val(), // 必填，签名，见附录1
             jsApiList: ['chooseWXPay', 'closeWindow'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
         });
         wx.error(function (res) {
            
             alert("权限验证失败!");
             wx.closeWindow();
             return;
         });
//         wx.ready(function (res) {           
//            alert("权限验证成功!");
//            return;
//        });
//         if ($("#txtOpenID").val() == "") {
//             alert("获取微信参数openid错误,请稍后再试！");
//             wx.closeWindow();
//             return;
//         }        
        
         //绑定事件
        $("#onlineRechargeOk").bind("click", wxPay);
         $("#money").bind("keyup", getPoint);
     });

     function getPoint() {
         
         var rechargepointrate = parseFloat($("#txtPointRate").val()); //比例
         var reg = /^(([1-9]\d{0,6})|(0))(\.\d{1,2})?$/;
         if ($("#money").val() != "") {
             if (!reg.test($("#money").val())) {

                 alert("充值金额必须为正整数，请重新输入!");
                 return;             }
            
         }
         //计算积分
         if (rechargepointrate == "0") {
             $("#point").val(0);
         } else {
             if ($("#money").val() != "") {
                 if (!reg.test($("#money").val())) {
                   //  $("#money").val(0.01);
                 }
                 $("#point").html(parseInt($("#money").val() / rechargepointrate));
             }
         }
         //计算充值金额
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

     }
     //支付
     function wxPay() {

//         var reg = /^[0-9]*[1-9][0-9]*$/;
//         if (!reg.test($("#money").val())) {
//             alert("充值必须为正整数，最小充值金额为：0.01元");
//             return;
         //         }
         var money = $("#money").val();
         
         if (money == "") {
             alert("充值金额不能为空！");
             return;
         }
         if (parseInt($("#txtMaxMoney").val()) < parseInt($("#money").val())) {
             alert("超过最大充值金额");
             return;

         }

         var memid = $("#txtMemID").val();
         if (memid == "") {
             alert("未获取到会员信息，无法进行解绑！");
             return;
         }
      var openid = $("#txtOpenID").val();
//         if (openid == "") 
//         {
//            alert("获取微信参数openid错误,请稍后再试！");                    
//            return;
      //         }
  
         $.ajax({
             url: "../../Service/AjaxService.ashx?Method=Membersrecharge",
             data: {
                 "timestamps": $("#timestamps").val(),
                 "nonceStrs": $("#nonceStrs").val(),
                 "money": money,
                 "point": $("#point").text(),
                 "memid": memid,
                 "openid": openid,
                 "now": $("#txtNow").val(),
                 "givemoney": $("#givemoney").text()
             },
             type: "POST",
             dataType: "json",
             contentType: "application/x-www-form-urlencoded; charset=utf-8",
             success: function (data) {
                 if (data.paySign == "" || data.prepay_id == "") {
                     alert("系统异常，请稍后再试！");
                     wx.closeWindow();
                     return;
                 } else {
                     $("#paySigns").val(data.paySign); //支付签名
                     $("#package").val(data.prepay_id);
                     wx.chooseWXPay({
                         timestamp: $("#timestamps").val(), // 支付签名时间戳，注意微信jssdk中的所有使用timestamp字段均为小写。但最新版的支付后台生成签名使用的timeStamp字段名需大写其中的S字符
                         nonceStr: $("#nonceStrs").val(), // 支付签名随机串，不长于 32 位
                         package: $("#package").val(), // 统一支付接口返回的prepay_id参数值，提交格式如：prepay_id=***）
                         signType: $("#signTypes").val(), // 签名方式，默认为'SHA1'，使用新版支付需传入'MD5'
                         paySign: $("#paySigns").val(), // 支付签名
                         success: function (res) {
                             //支付成功后的回调函数
                             if (res.errMsg == "chooseWXPay:ok") {
                                 alert("充值成功！");
                                 wx.closeWindow();
                             }
                         },
                         fail: function (res) {
                             alert("充值失败！");
                             wx.closeWindow();
                         }
                     });
                 }
             }
         });


     }
   

</script>

</body>
</html>