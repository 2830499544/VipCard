<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bossCenter.aspx.cs" Inherits="ChainStock.mobile.business.bossCenter" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>老板中心</title>
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
				<h1 id="title" runat="server"></h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
			</div>
		</div>
		<div id="content" class="section">
			<div class="section">
				
			</div>
			<div class="section line_box">
				<div class="line_group">
					<p class="f-left">时间范围</p>
					<div class="f-right">
						<!-- 考虑长度问题，最好不显示时分秒 -->
						<p><span id="spTime" runat="server"></span></p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">充值总金额</p>
					<div class="f-right">
						<p><span  id="spRechargeMoneySum">0.00</span>元</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">消费总金额</p>
					<div class="f-right">
						<p><span id="spExpenseMoneySum">0.00</span>元</p>
					</div>
				</div>
				<%--<div class="line_group">
					<p class="f-left">充次总金额</p>
					<div class="f-right">
						<p><span id="spCountMoneySum">0.00</span>元</p>
					</div>
				</div>--%>
				<div class="line_group">
					<p class="f-left">提现总金额</p>
					<div class="f-right">
						<p><span id="spDrawMoneySum">0.00</span>元</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">现金总收入</p>
					<div class="f-right">
						<p><span id="spCashMoneySum">0.00</span>元</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">余额总收入</p>
					<div class="f-right">
						<p><span id="spCardMoneySum">0.00</span>元</p>
					</div>
				</div>
                <div class="line_group">
					<p class="f-left">积分抵用</p>
					<div class="f-right">
						<p><span id="spPointSum">0.00</span>元</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">优惠券抵用</p>
					<div class="f-right">
						<p><span id="spCouponSum">0.00</span>元</p>
					</div>
				</div>
			</div>
		</div>
		<!-- 底部浮动导航 -->
		<div class="foot-nav">
			<!-- 返回主页 -->
			<%--<div class="f-left fix-nav fix-home">
				<a href="index.aspx"><img src="images/home.png"/></a>
			</div>
			<div class="f-left fix-nav fix-ch"  runat="server" id="liExpense">
				<a href="fastConsumption.aspx"><p>快速消费</p></a>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="##"><p>收银记账</p><img src="images/icon.png"/></a>
				<div class="foot-more">
					<a href="fastConsumption.aspx">快速消费</a>
					<a href="goodsConsumption.aspx">商品消费</a>
					<a href="timesConsumption.aspx">计次消费</a>
				</div>
			</div>--%>
			<div class="f-left fix-nav fix-ch">
				<a href="##"><p>老板中心</p><img src="images/icon.png"/></a>
				<div class="foot-more">
					<a href="bossCenter.aspx?type=1" >今日</a>
					<a href="bossCenter.aspx?type=2">昨日</a>
					<a href="bossCenter.aspx?type=3">本周</a>
					<a href="bossCenter.aspx?type=4">本月</a>
					<a href="bossCenter.aspx?type=5">30天内</a>
				</div>
			</div>
		</div>
	</div>
    <input id="txtType" type="hidden" runat="server" value="1" />
        <input id="txtShopID" type="hidden" runat="server" />

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>

  <script type="text/javascript">
      GetTotalInfo();
      function GetTotalInfo() {
          var type = $("#txtType").val();
          switch (parseInt(type)) {
              case 1:
                  $("#title").html("今日");
                  break;
              case 2:
                  $("#title").html("昨日");
                  break;
              case 3:
                  $("#title").html("本周");
                  break;
              case 4:
                  $("#title").html("本月");
                  break;
              case 5:
                  $("#title").html("30天内");
                  break;
              default:

          }
          $.get("../../Service/AjaxService.ashx?Method=GetWxRptTotal",
                     {
                         "type": type,
                         "shopid": $("#txtShopID").val()
                     }
                     , function (text) {

                         var json = JSON.parse(text);

                         if (json != "") {
                             $("#spTime").html(json.timeRadion);
                             $("#spMemCount").html(json.intNumber);
                             var RecharegeMoneySum = parseFloat(json.memSRechargeMoney) + parseFloat(json.memFRechargeMoney) + parseFloat(json.expenseBankSumMoneys) + parseFloat(json.FRechargeWebMoney) + parseFloat(json.WXCZ) + parseFloat(json.FRechargeGiveMoney);
                             $("#spRechargeMoneySum").html(RecharegeMoneySum.toFixed(2));
                           
                             var ExpeseMoneySum = parseFloat(json.payCard) + parseFloat(json.expenseSumMoneys) + parseFloat(json.payBink) + parseFloat(json.payCoupon) + parseFloat(json.payPoint);
                            // $("#spExpenseMoneySum").html(ExpeseMoneySum.toFixed(2));
                             //  var CountMoneySum = parseFloat(json.countPayCard) + parseFloat(json.countSumMoneys) + parseFloat(json.countPayBink) + parseFloat(json.countpayCoupon) ;
                             // $("#spCountMoneySum").html(CountMoneySum.toFixed(2));
                             var TimingMoneySum = parseFloat(json.StorageTimingPayCard) + parseFloat(json.StorageTimingPayCash) + parseFloat(json.StorageTimingPayBink) + parseFloat(json.StorageTimingPayCoupon);
                             $("#spPointSum").html(parseFloat(json.payPoint).toFixed(2));
                             $("#spDrawMoneySum").html(parseFloat(json.AllDrawMoney).toFixed(2));
                             $("#spCashMoneySum").html(parseFloat(json.allMoney).toFixed(2));
                             var CardMoneySum = parseFloat(json.payCard) + parseFloat(json.countPayCard) + parseFloat(json.StorageTimingPayCard);
                             $("#spCardMoneySum").html(CardMoneySum.toFixed(2));
                             var BankMoneySum = parseFloat(json.payBink) + parseFloat(json.countPayBink) + parseFloat(json.StorageTimingPayBink);
                             $("#spBankMoneySum").html(BankMoneySum.toFixed(2));
                             //var WeiXinSum = parseFloat(json.payWeiXin) + parseFloat(json.countPayWeiXin) + parseFloat(json.StorageTimingPayWeiXin);
                             //   $("#spWeiXinSum").html(WeiXinSum.toFixed(2));
                             var CouponSum = parseFloat(json.payCoupon) + parseFloat(json.countpayCoupon) + parseFloat(json.StorageTimingPayCoupon);
                             $("#spCouponSum").html(CouponSum.toFixed(2));
                             //var PointSum = parseFloat(json.payPoint) + parseFloat(json.countPayPoint) + parseFloat(json.StorageTimingPayPoint);
                            // $("#spPointSum").html(PointSum.toFixed(2));
                         }
                         else {

                         }


                     }, "text");
      }
       
    </script>  

</body>
</html>