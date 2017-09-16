<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fastConsumption.aspx.cs" Inherits="ChainStock.mobile.business.fastConsumption" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>快速消费</title>
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
				<h1>快速消费</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<!-- <a href="index.html" class="head-icon" id="home"><img src="images/home_f.png"/></a> -->
			</div>
		</div>
		<div id="content" class="section">
			<div class="section line_box">
				<div class="line_group">
					<p class="f-left">订单编号</p>
					<div class="f-right">
						<!-- 获取订单号 -->
						<p id="spOrderAccount" runat="server"></p>
					</div>
				</div>
				<div class="line_group" >
					<p class="f-left">散客消费</p>
					<div class="f-right">
						<a href="javascript:void(0);" class="line_btn active" id="ismember">否</a>
						<a href="javascript:void(0);" class="line_btn" id="nomember">是</a>
					</div>
				</div>
				<div class="line_group" id="memberCard">
					<p class="f-left">会员卡号</p>
					<div class="f-right">
						<!-- 输入卡号 -->
						<input type="text" id="memCard" name="memberCard" placeholder="请输入会员卡号">
					</div>
				</div>
				<div class="line_group dis-n" id="memberName">
					<p class="f-left">会员姓名</p>
					<div class="f-right">
						<!-- 根据卡号获取会员姓名 -->
						<p id="spMemName" runat="server">未找到</p>
					</div>
				</div>
				<div class="line_group" id="consumptionWay">
					<p class="f-left">消费方式</p>
					<div class="f-right">
						<a href="javascript:void(0);" class="line_btn active" id="balance">余额</a>
						<a href="javascript:void(0);" class="line_btn" id="cash">现金</a>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">消费金额（元）</p>
					<div class="f-right">
						￥<input type="text" placeholder="0.00" name="conMoney" id="money"  runat="server" maxlength="8" />
					</div>
				</div>
				<div class="line_group">
					<p class="f-left"  >折后金额（元）</p>
					<div class="f-right">
						<p id="discountMoney"  runat="server">0</p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">可得积分</p>
					<div class="f-right">
						<p id="point" runat="server">0</p>
					</div>
				</div>
				<div class="section form_btn" style="padding:0 0.2rem;margin-top:0.3rem;">
					<a href="##" id="sureConsumption">确认消费</a>
				</div>
			</div>
		</div>
		<!-- 底部浮动导航 -->
		<div class="foot-nav">
			<div class="f-left fix-nav fix-home">
				<a href="index.aspx"><img src="images/home.png"/></a>
			</div>
<%--			<div class="f-left fix-nav fix-ch">
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
				<a href="bossCenter.aspx"><p>老板中心</p></a>
			</div>
		</div>
	</div>
    <input id="txtUserID" type="hidden" runat="server" />
      <input id="txtMemID" type="hidden" runat="server" />
        <input id="txtLevelID" type="hidden" runat="server" />
        <input id="txtShopID" type="hidden"  runat="server"/>
<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>
<script type="text/javascript">
    $("#money").attr("disabled", "disabled");
    $("#discountMoney").attr("disabled", "disabled");
    $("#point").attr("disabled", "disabled");
    $("#memCard").focus();

    $("#nomember").click(function () {
        $("#memCard").attr("disabled", "disabled");
        $("#spMemName").html("");
        $("#memberName").hide();
        $("#memCard").val("");
        $("#memberCard").hide();
        $("#money").removeAttr("disabled");
        $("#money").focus();
        $("#cash").removeAttr("class");
        $("#cash").attr("class", "line_btn active");
        $("#balance").removeAttr("class");
        $("#balance").attr("disabled", "disabled");
        $("#money").val("");
        $("#discountMoney").html("0.00");
        $("#point").html("0");
    });
    $("#ismember").click(function () {
        $("#memCard").removeAttr("disabled");
        $("#spMemName").html("");
        $("#memberName").hide();
        $("#memCard").val("");
        $("#memberCard").show();
        $("#money").attr("disabled", "disabled");

        $("#cash").removeAttr("class");
        $("#cash").attr("class", "line_btn");
        $("#balance").removeAttr("class");
        $("#balance").attr("class", "line_btn active");
        $("#money").val("");
        $("#discountMoney").html("0.00");
        $("#point").html("0");
        $("#memCard").focus();

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
    $("#memCard").keyup(function () {
        var memCard = $.trim($("#memCard").val());
        $("#money").attr("disabled", "disabled");
        $("#spMemName").html("");
        $("#txtLevelID").val("");
        $("#memberName").hide();
        if (memCard != "") {

            $.get("../../Service/AjaxService.ashx?Method=GetWXMem",
                     {
                         "memCard": memCard,
                         "shopid": $("#txtShopID").val()
                     }
                     , function (json) {

                         if (json != "") {
                         
                             $("#spMemName").html(json[0].MemName);
                             $("#memberName").show();
                             $("#money").removeAttr("disabled");
                             $("#txtLevelID").val(json[0].MemLevelID);
                             $("#txtMemID").val(json[0].MemID);
                             $("#money").focus();
                         }
                         else {
                           
                             $("#money").attr("disabled", "disabled");
                             $("#txtLevelID").val("");
                         }
                     }, "json");
        }
        else {

            $("#money").attr("disabled", "disabled");
            $("#txtLevelID").val("");

        }
    });
    $("#money").keyup(function () {


        var money = $.trim($("#money").val());
        if (money != "") {
            if (!isFloatPositive(this)) {
                alert("输入的消费金额格式不正确,请重新输入!");
                return;
            }
        }
       
        var member = $("#ismember").attr("class");
        var nomember = $("#nomember").attr("class");

        var ismember = 1;
        if (member == "line_btn active") {
            ismember = 1; //是会员
        }
        else {
            ismember = 0;
        }
        if (ismember == 1) {

            $.get("../../Service/AjaxService.ashx?Method=WxGetDiscountMoney",
                     {
                         "money": money,
                         "shopid": $("#txtShopID").val(),
                         "levelid": $("#txtLevelID").val()
                     }
                     , function (text) {
                         if (text != null) {
                             var json = JSON.parse(text);

                             $("#discountMoney").html(json.Money);
                             $("#point").html(json.Point);
                         }

                     }, "text")
        }
        else {
            $("#discountMoney").html($("#money").val());

        }
        if(money == "") {
        
            $("#discountMoney").html("0.00");
        }

    });
    String.prototype.IsDecimal = function () {
        var reg = /^[0-9]+(.[0-9]{1,4})?$/;
        return reg.test(this);
    };

                 //提交
    $("#sureConsumption").click(function () {
        var member = $("#ismember").attr("class");
        var nomember = $("#nomember").attr("class");

        var ismember = 1;
        if (member == "line_btn active") {
            ismember = 1; //是会员
        }
        else {
            ismember = 0;
        }

        if ($("#txtMemID").val() == "" && ismember==1) {
                         alert("未获取到会员信息，请先查询出会员信息！");
                         return;
                     }
                     if ($("#money").val() == "") {
                         alert("请输入消费金额!");
                         return;
                     }
                     var money = $("#money").val();
                     if (money != "") {
                         if (!money.IsDecimal()) {
                             alert("输入的消费金额格式不正确,请重新输入!");
                             return;
                         }
                         if (parseFloat($("#money").val()) <= 0) {
                             alert("消费金额必须为大于0,请重新输入!");
                             return;
                         }

                     }
                   


                     var balance = $("#balance").attr("class");
                     var cash = $("#cash").attr("class");

                     var payType = "cash";
                     if (balance == "line_btn active") {
                         payType = "balance"; //余额消费
                     }
                     else {
                         payType = "cash";
                     }



                     $.get("../../Service/AjaxService.ashx?Method=WX_Expense",
                    {
                        "money": $("#money").val(),
                        "discountmoney": $("#discountMoney").html(),
                        "money": $("#money").val(),
                        "point": $("#point").html(),
                        "OrderAccount": $("#spOrderAccount").html(),
                        "memID": $("#txtMemID").val(),
                        "shopid": $("#txtShopID").val(),
                        "userid": $("#txtUserID").val(),
                        "payType": payType,
                        "isMember": ismember

                    }
                    , function (text) {

                        if (text == "-1") { alert("系统错误 请与系统管理员联系！"); }
                        else if (text == "-2") { alert("消费成功！,短信余额不足，不能发送短信，请充值短信！！"); }
                        else if (text == "-6") { alert("本店积分不足无法消费，请与总店联系！"); }
                        else if (text == "-3") { alert("会员余额不足！"); }
                        else {
                            alert("消费成功！");
                            location.href = "fastConsumption.aspx";
                        }
                    }, "text");
                })

</script>

</body>
</html>