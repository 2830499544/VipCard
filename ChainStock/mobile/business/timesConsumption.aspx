<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="timesConsumption.aspx.cs" Inherits="ChainStock.mobile.business.timesConsumption" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>计次消费</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
	<link rel="stylesheet" type="text/css" href="css/media.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
</head>
<body>
	<div class="section index" id="container" style="margin-bottom:1.6rem;">
		<div id="head" class="section">
			<div class="section header">
				<h1>计次消费</h1>
				<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png"/></a>
				<!-- <a href="index.html" class="head-icon" id="home"><img src="images/home_f.png"/></a> -->
			</div>
		</div>
		<div id="content" class="section" style="background-color:#f7f7f7;">
			<div class="section">
				<div class="section line_box">
					<div class="line_group">
						<p class="f-left">订单编号</p>
						<div class="f-right">
							<!-- 获取订单号 -->
							<p id="spOrderAccount" runat="server"></p>
						</div>
					</div>
					<div class="line_group" id="queryMember">
						<p class="f-left">会员卡号</p>
						<div class="f-right">
							<!-- 输入卡号 -->
							<input type="text" name="memberCard" placeholder="请输入会员卡号" id="memCard">
						</div>
					</div>
					<div class="line_group dis-n" id="memberName">
						<p class="f-left">会员姓名</p>
						<div class="f-right">
							<!-- 根据卡号获取会员姓名 -->
							<p id="spMemName" runat="server">未找到</p>
						</div>
					</div>
				</div>

				<!-- <div class="section in-search">
					<input type="text" placeholder="输入服务名称进行查找" />
					<a href="##"><img src="images/search.png" alt="搜索" /></a>
				</div> -->

				<div class="section bg-fff" style="margin-top:0.1rem;">
				
					<div class="section f-right gift-list" style="width:100%;">
						<!-- 服务列表 -->
						<ul class="section" id="spHtml">
                       
                          
						<%--	<li>
								<div class="f-left gift-txt">
									<h3>拽猫系列卡通硅胶手机挂绳</h3>
									<p>剩余次数：<span class="count-num">10</span></p>
								</div>
								<!-- 增减控制器 -->
								<div class="buy-ctrl">
									<a href="##" class="reduceBtn"><img src="images/reduce.png" /></a>
									<!-- 这里默认为0 -->
									<input type="text" value="0" class="giftTxt" />
									<a href="##" class="plusBtn"><img src="images/plus.png" /></a>
									<a href="##" class="plusShow"><img src="images/plus1.png" /></a>
								</div>
							</li>--%>
					
							
						
							
						<%--	<a href="##">加载更多...</a>--%>
						</ul>
					</div>
				</div>

				<div class="gift-fix">
					<h3 style="padding-left:0.2rem;">消费总次：<span class="count-total" id="spTotalCount">0</span></h3>
					<a href="##" id="nowCount" class="queryCity" >立即消费</a>
					<div class="dis-n city-mode">
						<div class="city-query">
							<p style="width: 100%;text-align: center;font-size: 0.3rem;">是否确定立即结算？</p>
							<div class="section ad-detail">
								<a href="##" class="count-settlement" id="btnSetttleCount">立即结算</a>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- 底部浮动导航 -->
		<div class="foot-nav">
			<div class="f-left fix-nav fix-home">
				<a href="index.aspx"><img src="images/home.png"/></a>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="memberCard.aspx"><p>会员办卡</p></a>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="##"><p>收银记账</p><img src="images/icon.png"/></a>
				<div class="foot-more">
					<a href="fastConsumption.aspx">快速消费</a>
					<a href="goodsConsumption.aspx">商品消费</a>
					<a href="timesConsumption.aspx">计次消费</a>
				</div>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="bossCenter.aspx"><p>老板中心</p></a>
			</div>
		</div>
   
	</div>        <input id="txtMemID" type="hidden" runat="server" />
         <input id="txtShopID" type="hidden" runat="server" />
              <input id="txtUserID" type="hidden" runat="server" />
<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
    <script src="scripts/script.js" type="text/javascript"></script>
<script>
    $("#memCard").keyup(function () {

        var memCard = $.trim($("#memCard").val());

        $("#spMemName").html("");
        $("#memberName").hide();
        $.get("../../Service/AjaxService.ashx?Method=GetWXMem", {
            "memCard": memCard,
            "shopid": $("#txtShopID").val()
        },
    function (json) {
        if (json != "") {

            $("#spMemName").html(json[0].MemName);
            $("#memberName").show();
            $("#txtMemID").val(json[0].MemID);
            GetMemCountList();
        }
        else {
            $("#txtMemID").val("");
            $("#spHtml").html("");
        }

    },
    "json");

    });
    function GetMemCountList() {

        $.get("../../Service/AjaxService.ashx?Method=GetWx_MemCountProductList", {
            "memID": $("#txtMemID").val()

        },
        function (text) {

            if (text != "") {
                var json = JSON.parse(text);

                if (json != "") {

                    CreateCountProductTable(json.List);
                }
             
            }
            else 
            {

              $("#spHtml").html('<h5 style=" text-align:center;">该会员暂无计次产品！</h3>');
               
              
            }

        },
        "text");
}
    function CreateCountProductTable(obj) {
        var html = '';
        if (obj.length > 0) {
            $.each(obj,
      function (index, item) {

          html += '<li><div class="f-left gift-txt"><h3>'+item.Name+'</h3>'
          + '<p>剩余次数：<span class="count-num" id="count-num' + item.CountDetailGoodsID + '">' + item.Number   + '</span></p></div>'
          + '<div class="buy-ctrl"  onclick=\"javascript:countGift();\"><a href="##" class="reduceBtn" id="reduceBtn' + item.CountDetailGoodsID + '" onclick=\"javascript:reduceBtnMethod(' + item.CountDetailGoodsID + ');\"><img src="images/reduce.png" /></a>'
          + '<input type="text" value="0" class="giftTxt" id="giftTxt' + item.CountDetailGoodsID + '" /><a href="##" class="plusBtn" id="plusBtn' + item.CountDetailGoodsID + '"  onclick=\"javascript:plusBtnMethod(' + item.CountDetailGoodsID + ');\"><img src="images/plus.png" /></a>'
          + '<a href="##" class="plusShow" id="plusShow' + item.CountDetailGoodsID + '"  onclick=\"javascript:plusShowMethod(' + item.CountDetailGoodsID + ');\"><img src="images/plus1.png" /></a></div></li>';
      });

        } 
        else {
            html += '<td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="5">未找到符合此条件的数据！请重试！</td>';
        }

        $("#spHtml").html(html);
       
      
     
    }


    var isBuy = false;

    var GiftList = new Array();
    function plusShowMethod(id) {

      
        var value = 1;     
        $("#giftTxt"+id).val(value);
        $("#plusShow" + id).hide();
        $("#reduceBtn" + id).show();       
        $("#plusBtn" + id).show();
        $("#giftTxt" + id).show();

   

     
        var gift = new Object();
        gift.id = id;
        gift.count = 1;
        GiftList.push(gift);

     
    }

    function plusBtnMethod(id) {

        //将要消费的数量
        var value = parseInt($("#giftTxt"+id).val());
        // 记次消费单个服务拥有总次数
        var count = parseInt($("#count-num"+id).text());
        if (count) {
            if (value < count) {
                value++;
            }
        } else {
            value++;
        }

        $("#giftTxt" + id).val(value);

        for (var i = 0; i < GiftList.length; i++) {
            if (GiftList[i].id == id && value > 0) {
                GiftList[i].count = value;
            }
        }
    }
    function reduceBtnMethod(id) {
        var value = $("#giftTxt" + id).val();
        if (value > 0) {
            value = $("#giftTxt" + id).val();
            value--;
            $("#giftTxt" + id).val(value);

            for (var i = 0; i < GiftList.length; i++) {
                if (GiftList[i].id == id) {
                    GiftList[i].count = value;
                }
            }

            if (value == 0) {
            
                $("#plusShow" + id).show();
                $("#reduceBtn" + id).hide();
                $("#plusBtn" + id).hide();
                $("#giftTxt" + id).hide();
            
            }
        }
        if (value == 0) {
            for (var i = 0; i < GiftList.length; i++) {
                var gift = new Object();
                gift.id = id;
                gift.count = 1;
                GiftList.push(gift);
                if (GiftList[i].id == id) {

                    GiftList.remove(i);
                  
                }
            }
      }
   
     
    } 

    countGift();

    function countGift() {
  
        var num = 0;
        var total = 0;
        $(".gift-list li").each(function (index, element) {
            var danNum = $(this).find(".buy-ctrl input").val();
            var danIntegral = $(this).find(".gift-integral").text();
            num = num + parseInt(danNum);
            total = total + parseInt(danIntegral * danNum);
        });
        // console.log(num)
        $(".gift-num").text(num);
        $(".gift-total").text(total);
        $(".count-total").text(num);
    }
    //点击填写弹出城市选择模态框
    $("#nowCount").click(function () {
        $(this).siblings(".city-mode").fadeIn();
        $("body,html").css("overflow", "hidden");
    });
    //计次消费-立即消费
    $("#btnSetttleCount").click(function () {


      
        if (GiftList.length == 0) {
            alert("请选择消费商品！");
            return;
        }
        $.get("../../Service/AjaxService.ashx?Method=WX_CountExpense", {
            "shopid": $("#txtShopID").val(),
            "userid": $("#txtUserID").val(),
            "memID": $("#txtMemID").val(),
            "number": $("#spTotalCount").html(),
            "OrderAccount": $.trim($("#spOrderAccount").html()),
            "data": GiftList,
            "count": GiftList.length

        },
        function (text) {
            if (text == "-1") { alert("消费失败！"); }
            else {
                alert("消费成功！");
                location.href = "timesConsumption.aspx";
            }

        },
        "text");

    });
</script>
</body>
</html>