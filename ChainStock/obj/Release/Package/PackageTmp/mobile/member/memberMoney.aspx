<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="memberMoney.aspx.cs" Inherits="ChainStock.mobile.member.memberMoney" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微会员-会员转账</title>
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
				<h1>会员转账</h1>
				<a href="index.aspx"   style="	position: absolute;	left: 0.2rem;	top: 0;	width: 0.24rem;"><img src="images/prev.png"/></a>
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
					<p class="f-left">转账账户</p>
					<div class="f-right">
					<input type="text"  id="memcard" placeholder="会员卡号/手机号码" maxlength="11" />
					</div>
				</div>
				<div class="line_group divMemName" style=" display:none" >
					<p class="f-left">转账姓名</p>
					<div class="f-right">
						<p id="spMemName" runat="server"></p>
					</div>
				</div>
				<div class="line_group">
					<p class="f-left">转账金额</p>
					<div class="f-right">
						<input type="text" style=" color:#16a2de; "   id="money" placeholder="请输入转账金额" maxlength="6"  disabled="disabled"/>
					</div>
				</div>
                
                <div class="line_group">
					<p class="f-left">手续费</p>
					<div class="f-right">
						<p id="spElseMoney" runat="server">0.00</p>
					</div>
				</div>
                <div class="line_group">
					<p class="f-left">本次扣除</p>
					<div class="f-right">
						<p id="spMoney" runat="server">0.00</p>
					</div>
				</div>
                	<div class="line_group">
					<p class="f-left">备注</p>
					<div class="f-right">
					<input type="text"  id="remark" placeholder="转账说明" maxlength="11" />
					</div>
				</div>
				   <div class="line_group" style=" font-size:10pt;text-align:center; color:Gray; margin-top:0px;">
            
					手续费<span runat="server" id="spRate" style=" color:Orange; ">0.1%</span>,最多可转<span runat="server" id="spMaxMoney" style=" color:Orange; " >1000.00</span>元
					
				</div>
                
				<!-- 确认充值跳转到微信充值，充值成功后跳转到rechargeSuccess.html页面 -->
				<div class="section login_form form_btn">
					<a href="#" id="btnGiveMemMoney" class="btnGiveMemMoney"  >确认转账</a>
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

    <input type="hidden" id="txtMemID" runat="server" />
        <input type="hidden" id="txtGiveMemID" runat="server" />
        <input type="hidden" id="txtRate" runat="server" />
<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>
<script>
    $(document).ready(function () {
        $("#money").bind("keyup", getMoney);
        $("#memcard").bind("keyup", GetMemName);
        $("#btnGiveMemMoney").bind("click", GiveMemMoney);
       
        $("#memcard").focus();
    });
    function getMoney() {

        var rate = parseFloat($("#txtRate").val()); //比例
        var reg = /^([0-9])|([1-9]\d+)\.\d?$/;
        if ($("#money").val() != "") {
            if (!reg.test($("#money").val())) {

                alert("转账金额格式错误，请重新输入!");
                return;
            }

        }

        if ($("#money").val() != "") {
            var money = parseFloat($("#money").val());
            var elsemoney = parseFloat(money * (rate)).toFixed(2);

            var givemoney = parseFloat(money * (1 + rate)).toFixed(2);
            var maxmoney = parseFloat($("#spMaxMoney").html());
            $("#spMoney").html(givemoney);
            $("#spElseMoney").html(elsemoney);
            if (money > maxmoney) {
                alert("转账金额超出可用余额，请重新输入!");
                $("#money").val("")
                $("#spMoney").html("0.00")
                $("#spElseMoney").html("0.00");
                return;
            }
        }
        else {

            $("#spMoney").html("0.00");
            $("#spElseMoney").html("0.00");
        }
        

     }

     function GetMemName() {
         var memcard = $("#memcard").val()
         var memid= $("#txtMemID").val()
         $.get("../../Service/AjaxService.ashx?Method=GetMemName",
        {
            "memcard": memcard
        }
        , function (text) {
            var json = JSON.parse(text);
           
            if (json.memid != "0") {
                $(".divMemName").show();
                $("#spMemName").html(json.memname);
                if (json.memid == memid) {
                    alert("不能转账给自己!");
                    $(".divMemName").hide();
                    $("#spMemName").html("");
                    $("#txtGiveMemID").val("");
                    $("#memcard").val("")
                    return;
                }

                $("#txtGiveMemID").val(json.memid);
                
                $("#money").removeAttr("disabled");
                $("#money").focus();
            }
            else {
               
                $(".divMemName").hide();
                $("#spMemName").html("");
            }
        }, "text")
    }

    function GiveMemMoney() {
        $('#btnGiveMemMoney').removeAttr('href'); //去掉a标签中的href属性
        $('#btnGiveMemMoney').removeAttr('onclick'); //去掉a标签中的onclick事件
     
        var memid = $("#txtMemID").val();

        if (memid == "") {
            alert("未获取到会员信息，请重新登录！");
            return;
        }
        var giveMemID = $("#txtGiveMemID").val();

        if (giveMemID == "") {
            alert("请先查询出转账会员信息！");
            return;
        }
        var reg = /^([0-9])|([1-9]\d+)\.\d?$/;
        if ($("#money").val() != "") {
            if (!reg.test($("#money").val())) {

                alert("转账金额格式错误，请重新输入!");
                return;
            }

        }
        var money=$("#money").val();
        var remark=$("#remark").val();
        var elseMoney=$("#spElseMoney").html();
        var totalMoney=$("#spMoney").html();
        $.get("../../Service/AjaxService.ashx?Method=Wx_GiveMemMoney",
        { "giveMemID": giveMemID, "memid": memid, "money": money, "remark": remark, "elseMoney": elseMoney, "totalMoney": totalMoney
        }, function (text) {

            switch (text) {
                case "0":
                    alert("系统错误，转账失败！");
                    break;

                default:
                    alert("转账成功！");
                    $("#btnGiveMemMoney").removeAttr("disabled");
                    location.href = "memberMoney.aspx";
                    break;
            }
        }, "text");
    }
</script>


</body>
</html>