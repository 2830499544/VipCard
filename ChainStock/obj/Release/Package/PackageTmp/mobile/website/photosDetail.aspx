<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="photosDetail.aspx.cs" Inherits="ChainStock.mobile.website.photosDetail" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<meta name="renderer" content="webkit|ie-comp|ie-stand"/>
    <meta name="viewport" content="width=device-width,height=device-height,maximum-scale=1.0,user-scalable=no" />
    <meta name="keywords" content=""/>
    <meta name="description" content=""/>
	<title>微官网-相册照片</title>
	<link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="css/common.css">
	<link rel="stylesheet" type="text/css" href="css/swipe.css">
	<link rel="stylesheet" type="text/css" href="css/color.css">
	<link rel="stylesheet" type="text/css" href="css/style.css">
	<link rel="stylesheet" type="text/css" href="css/media.css">
</head>
<body>
    

	<div class="section" id="container">
		
		<div id="head" class="section">
			<h1>相册照片</h1>
			<!-- 返回上一页 -->
			<a href="javascript:void(0);" class="back-btn"><img src="images/prev.png" /></a>
			<!-- 跳转到微会员 -->
			<a href="javascript:void(0);" class="head-icon" id="inMember"><img src="images/w-member.png"/></a>

		</div>
        <input id="txtAlbumID" type="hidden"  runat="server"/>
		<div id="content" class="section">
			<div class="section photos-detail">
				<!-- 
					* 这里是照片列表 *
					* 照片尺寸640*480 *
				-->
				<ul class="section photos-d-list">
                  <asp:Repeater ID="rptPhoto" runat="server">
                   <ItemTemplate>

                    <li>
                    	<div class="image"><img src='../<%#Eval("PhotoPhoto")%>'/></div>
                    </li>
                    </ItemTemplate>
                    </asp:Repeater>
                    	<a href="javascript:void(0);" class="morePhoto" runat="server" id="morePhoto">加载更多</a>
				</ul>
			</div>
			
			<!-- 点击图片弹出图片查看模态框 -->
			<div class="photo-mode page-swipe">
				<!-- 关闭按钮 -->
				<div class="photo-close">
					<a href="javascript:void(0);"><img src="images/close.png"/></a>
				</div>
				<!-- 轮播幻灯片查看照片 -->
				<div id="slider" class="swipe">
					<ul class="swipe-wrap">
                       <asp:Repeater ID="rptPhotoDetail" runat="server">
                   <ItemTemplate>

						<!-- 不要改变这里的布局结构，只需更换h3,p,img标签内容即可 -->
						<li>
	                        <div class="wrap">
	                        	<h3><%#Eval("PhotoName")%></h3>
	                            <div class="image">
	                            <!-- 这里放置的照片和列表中的照片一样 -->
	                            	<img src='../<%#Eval("PhotoPhoto")%>'/>
	                            </div>
	                            <p><%#Eval("PhotoDesc")%></p>
	                        </div>
	                    </li>
                        </ItemTemplate>
	                </asp:Repeater>
                    </ul>
				</div>
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
	</div>

<script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
<script type="text/javascript" src="scripts/script.js"></script>
<script type="text/javascript" src="scripts/swipe.js"></script>

<script>


    var moveW = $(".swipe-wrap li").outerWidth();
    var init = 0;
    var slider = "";
    var strHtml = $(".swipe").html();
   
    $(".photos-d-list li").each(function (index, element) {

        $(this).click(function () {          
            init = index;
            slider =
              Swipe(document.getElementById('slider'), {
                  startSlide: init,
                  auto: false,
                  continuous: true
              });
        });
    });

    $(".photo-close").click(function () {       
        $(".swipe").html(strHtml); 
    });

    
</script>
</body>
</html>