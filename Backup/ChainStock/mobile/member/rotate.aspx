<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rotate.aspx.cs" Inherits="ChainStock.mobile.member.rotate" %>

<!DOCTYPE html>
<html>
<head>
 <meta http-equiv="Content-Type" content="text/html;charset=UTF-8">
<meta name="viewport" content="width=device-width,initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
<title>大转盘活动</title>
<link rel="stylesheet" type="text/css" href="marketcss/public/reset.css">
<link rel="stylesheet" type="text/css" href="marketcss/public/font-awesome.css">
<link rel="stylesheet" type="text/css" href="marketcss/public/function.css">
<link rel="stylesheet" type="text/css" href="marketcss/style.css">

<script type="text/javascript" src="js/model/jquery-1.8.0.min.js"></script>
<script type="text/javascript" src="js/model/jquery.SuperSlide.2.1.1.source.js"></script>
<script type="text/javascript" src="js/model/html5.js"></script>
<script type="text/javascript" src="js/public.js"></script>
<script charset="utf-8" type="text/javascript" src="js/app.js"></script>

</head>
<body class="g-body-in">
<div class="g-wrap" runat="server" >

	<section class="h15"></section>
	<section class="main-sec pt5 main-wheel">
		<div class="big-border">
			<div class="small-border g7">

				<div class="shan">
					<span style=" font-size:8pt;" id="spOnePrizeName" runat="server">1元现金</span>
						<span style=" font-size:14pt; font-weight:bold;" id="spOneName" runat="server">一等奖</span>
				</div>

				<div class="shan">
				<span style=" font-size:8pt;" id="spTwoPrizeName" runat="server">2元现金</span>
				<span style=" font-size:14pt; font-weight:bold;"  id="spTwoName" runat="server">二等奖</span>
				</div>

				<div class="shan">
					<span style=" font-size:8pt;" id="spThreePrizeName" runat="server">3元现金</span>
				<span style=" font-size:14pt; font-weight:bold;"  id="spThreeName" runat="server">三等奖</span>
				</div>

				<div class="shan">
					<span style=" font-size:8pt;" id="spFourPrizeName" runat="server">4元现金</span>
				<span style=" font-size:14pt; font-weight:bold;"  id="spFourName" runat="server">四等奖</span>
				</div>
				
				<div class="shan">
					<span style=" font-size:8pt;" id="spFivePrizeName" runat="server">5元现金</span>
				<span style=" font-size:14pt; font-weight:bold;"  id="spFiveName" runat="server">五等奖</span>
				</div>

				<div class="shan">
					<span style=" font-size:8pt;" id="spSixPrizeName" runat="server">6元现金</span>
				<span style=" font-size:14pt; font-weight:bold;"  id="spSixName" runat="server">六等奖</span>
				</div>

				<div class="shan">
					<span style=" font-size:14pt; font-weight:bold;">谢谢参与</span>
				
				</div>

			

				<img src="images/middle.png" width="50%" class="middle">
			</div>
		</div>
	</section>

	<section class="main-sec">
		<div class="g-num">您还有<em><span id="spNoUseCount"  runat="server">0</span></em>次抽奖机会</div>
	</section>

	<section class="main-sec loptop">
		<div class="m-title"><h3>中奖记录</h3></div>
      
       
        
		<dl class="peolist">
          <asp:Repeater ID="rptWinList" runat="server" runat="server">
         <ItemTemplate>        
			<dd>
            
				<img src="<%#BindPhoto(Eval("MemPhoto")) %>" width="20%" id="imgPhoto">
				<div class="right">
					<span><h2><%#Eval("MemName") %></h2><em><%# BindTime(Eval("CreateTime")) %></h2></em></span>
					<p>恭喜 <%#BindMobile(Eval("MemMobile")) %> 抽中<%#Eval("PrizeName") %></p>
				</div>
			</dd>
                </ItemTemplate>
			  </asp:Repeater>
		</dl>
    
      
	</section>

	<script type="text/javascript">
	    jQuery(".loptop").slide({ mainCell: "dl", autoPage: true, effect: 'topLoop', autoPlay: true, scroll: 1, vis: 1, easing: 'swing', delayTime: 500, interTime: 3000, pnLoop: true });

	</script>

	<section class="main-sec">
		<div class="m-title"><h3>活动说明</h3></div>
		<div class="einfo">
			<p>活动时间：<span id="spStartTime" runat="server"></span>-<span id="spEndTime" runat="server"></span></p>
			<p><span id="spRotateDesc" runat="server">每日签到可获1次抽奖机会</span></p>
		
		</div>
	</section>

	<section class="main-sec">
		<footer><a href="index.aspx" style=" color:White;"> 返回首页</a></footer>
	</section>
</div>

<div class="dialog gz">
	<div class="d-main">
		<p>请进入"镇江微生活"公众号立即参与</p>
		<div class="btn-w">
			<a class="btn" href="#">进入</a>
			<a class="btn false" href="javascript:void(0);">取消</a>
		</div>
	</div>
</div>

<div class="dialog info">
	<div class="d-main">
		<p><span id="Span1"  runat="server" >次数用光,金额不足 之类的 信息提示表单</span></p>
	</div>
</div>


<div class="dialog theForm">
	<div class="d-main">
		<p>恭喜您抽中了【<span runat="server" id="spGetPrizeLevel" ></span>】,获得了【<span  id="spGetPrizeName"  runat="server"></span>】,兑换码【<span runat="server" id="spCode"></span>】,请及时兑换！</p>
		<div class="btn-w">
			<a class="btn btn-lang close" href="javascript:;">确定</a>
		</div>
	</div>
</div>


<div class="dialog again">
	<div class="d-main">
		<p id="spMsgInfo">很遗憾,未抽中奖品,谢谢您的参与！</p>
		<div class="btn-w">
			<a class="btn btn-lang close" href="javascript:;">确定</a>
		</div>
	</div>
</div>

<script type="text/javascript">
    $(document).ready(function () {



        var bgImage = $("#spImageUrl").html();
        if (bgImage != "") {
            $(".g-wrap").attr("style", "width: 100%;overflow: hidden;background:url('../" +
                            $("#spImageUrl").html() + "');min-height:100%;padding-bottom: 20px;background-size:cover;");
        }
    });






    var valueJson = {
        'wheelBody': $('.big-border'), //转盘主体
        'wheelSmall': $('.small-border'), //转盘内部
        'starsNum': 16, //转盘边缘小黄点个数

        'starsPostion': [[50, 0.5], [70, 6], [84.5, 18], [92.5, 32], [95.5, 50], [91, 68], [81.5, 81.5], [68, 91], [50, 95.5], [32, 92.5], [16, 83], [6, 70], [0.5, 50], [3.5, 32], [14, 15], [27, 5.5]], //小圆点坐标
        'actionRan': 7200, //转盘转动弧度
        'theOnce': 0, //初始化转盘第一个
        'startBtn': $('.middle'), //开始按钮

        //需要后台传值的参数
        'clickAjaxUrl': 'www.baidu.com', //点击抽奖获取信息的交互的ajax
        'is_gz': 1, //是否开启关注 1开 2 关
        'is_follow': 1 //是否关注

    };
    indexApp.init(valueJson).wheelStart(); //应用开始

    function getRandom(n) {

        return Math.floor(Math.random() * n + 1)
    }


   
</script>
   <span id="spWinArray" runat="server" style=" display:block;" ></span>
    <span id="spRoateArray" runat="server" style=" display:block;" ></span>
   <span id="spRotateID" runat="server" style=" display:none;"></span>
      <span id="spRotateCount" runat="server" style=" display:none;"></span>
   
         <span id="spPersonTotalCount" runat="server"  style=" display:none;"></span>
           <span id="spPersonTotalCountWin" runat="server"  style=" display:none;"></span>
           <span id="spPersonDayCount" runat="server" style=" display:none;"></span>         
         <span id="spPersonDayCountWin" runat="server"  style=" display:none;"></span>
 
<input id="txtMemID" type="hidden" runat="server" />

         <span id="spIsOne" runat="server" style=" display:none;">0</span>
   <span id="spIsTwo" runat="server" style=" display:none;">0</span>
<span id="spIsThree" runat="server" style=" display:none;">0</span> 
<span id="spIsFour" runat="server" style=" display:none;">0</span> 
<span id="spIsFive" runat="server"  style=" display:none;">0</span> 
  <span id="spIsSix" runat="server" style=" display:none;">0</span> 

         <span id="spOneRate" runat="server" style=" display:none;"></span>
   <span id="spTwoRate" runat="server" style=" display:none;"></span>
<span id="spThreeRate" runat="server" style=" display:none;"></span> 
<span id="spFourRate" runat="server" style=" display:none;"></span> 
<span id="spFiveRate" runat="server"  style=" display:none;"></span> 
  <span id="spSixRate" runat="server" style=" display:none;"></span> 

<input id="txtMemTotalCount" type="hidden"  runat="server"/>
<input id="txtRotateID" type="hidden"  runat="server"/>
<input id="txtMsg" type="hidden"  runat="server"/>
<span id="spImageUrl"   runat="server" style=" display:none;"></span>
    <span id="spOnePrizeCount" runat="server" style=" display:none;"></span>
   <span id="spTwoPrizeCount" runat="server" style=" display:none;"></span>
<span id="spThreePrizeCount" runat="server" style=" display:none;"></span> 
<span id="spFourPrizeCount" runat="server" style=" display:none;"></span> 
<span id="spFivePrizeCount" runat="server"  style=" display:none;"></span> 
  <span id="spSixPrizeCount" runat="server" style=" display:none;"></span> 
      <span id="spOnePrizeWinCount" runat="server" style=" display:none;"></span>
   <span id="spTwoPrizeWinCount" runat="server" style=" display:none;"></span>
<span id="spThreePrizeWinCount" runat="server" style=" display:none;"></span> 
<span id="spFourPrizeWinCount" runat="server" style=" display:none; "></span> 
<span id="spFivePrizeWinCount" runat="server"  style=" display:none;"></span> 
  <span id="spSixPrizeWinCount" runat="server" style=" display:none;"></span> 
    <span id="spIsWinOne" runat="server" style=" display:none;"></span> 



      <span id="spWinCount" runat="server" style=" display:none;"></span> 
</body>

</html>
