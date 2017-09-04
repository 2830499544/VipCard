<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="map.aspx.cs" Inherits="ChainStock.mobile.website.map" %>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,maximum-scale=1.0,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微官网-商家地图</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
	<link rel="stylesheet" type="text/css" href="css/media.css">
    
</head>
<body>
	<div class="section" id="container">
		
		<div id="head" class="section">
			<h1>商家地图</h1>
			<!-- 返回上一页 -->
			<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png" /></a>
			<!-- 跳转到微会员 -->
			<a href="javascript:void(0);" class="head-icon" id="inMember"><img src="images/w-member.png"/></a>
		</div>
		<div id="content" class="section">
		
				<!-- 这里面放置产品详情的图片与文字 -->
           
             <div id="map_content"  >
            <div id="divmap_container" style="width:100%; height:600px;"></div>    
            </div>
               <span id="spAddress" runat="server" style="display:none;" ></span>
             <span id="spLat" runat="server" style="display:none;"></span>
             <span id="spLng" runat="server" style="display:none;"></span>            
          
				<%--<img src="images/pro_01.jpg"/>
				<img src="images/pro_01.jpg"/>--%>
		
		</div>
		<!-- 底部浮动导航 -->
		<div class="foot-nav">
			<!-- 返回主页 -->
			<div class="f-left fix-nav fix-home">
				<a href="index.aspx"><img src="images/home.png"/></a>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="productShow.aspx"><p>产品展示</p></a>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="active.aspx"><p>优惠活动</p></a>
			</div>
			<div class="f-left fix-nav fix-ch">
				<a href="queryStore.aspx"><p>门店查询</p></a>
			</div>
			<!-- 更多菜单 -->
			<div class="f-left fix-nav fix-more">
				<a href="javascript:void(0);"><p>更多</p><img src="images/icon.png"/></a>
			</div>
			<!-- 更多菜单弹出项 -->
			<div class="fix-more-nav">
				<a href="onlineAsk.aspx">在线咨询</a>
				<a href="photos.aspx">相册</a>
				<a href="news.aspx">动态</a>
			</div>
		</div>
	</div>

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>
<script charset="utf-8" src="http://map.qq.com/api/js?v=2.exp"></script>
<script type="text/javascript">
    //地图
    var geocoder, map, marker = null;
    var lat = $("#spLat").html();
    var lng = $("#spLng").html();

    var center = new qq.maps.LatLng(lat, lng);
    map = new qq.maps.Map(document.getElementById('divmap_container'), {
        center: center,
        zoom: 15
    });
    //调用地址解析类
    geocoder = new qq.maps.Geocoder(
        {
            complete: function (result) {
                map.setCenter(result.detail.location);
                var marker = new qq.maps.Marker({
                    map: map,
                    position: result.detail.location
                });

            }
        });

    var address = $("#spAddress").html();

    //通过getLocation();方法获取位置信息值

    geocoder.getLocation(address);
 
</script>

</body>
</html>