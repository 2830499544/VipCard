<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productShow.aspx.cs" Inherits="ChainStock.mobile.website.productShow" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="renderer" content="webkit|ie-comp|ie-stand" />
    <meta name="viewport" content="width=device-width,height=device-height,maximum-scale=1.0,user-scalable=no" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <title>微官网-产品展示</title>
    <link rel="stylesheet" type="text/css" href="css/bootstrap.min.css">
    <link rel="stylesheet" type="text/css" href="css/common.css">
    <link rel="stylesheet" type="text/css" href="css/color.css">
    <link rel="stylesheet" type="text/css" href="css/style.css">
    <link rel="stylesheet" type="text/css" href="css/media.css">
</head>
<body>
    <div class="section" id="container">

        <div id="head" class="section">
            <h1>产品展示</h1>
            <!-- 返回上一页 -->
            <a href="javascript:void(0);" class="back-btn">
                <img src="images/prev.png" /></a>
            <!-- 跳转到微会员 -->
            <a href="javascript:void(0);" class="head-icon" id="inMember">
                <img src="images/w-member.png" /></a>
        </div>
        <div id="content" class="section">
            <%--产品分类导航--%>
            <div class="section product-nav">
                <div class="f-left btn-left pro-btn">
                    <a href="javascript:void(0);"><img src="images/left.png" /></a>
                </div>

                <div class="f-left section t-center pro-nav-list">
                    <ul>
                        <asp:Repeater ID="rptProductClass" runat="server">
                    <ItemTemplate>

                        <li><a href="productShow.aspx?ClassID=<%#Eval("ClassID") %>"><%#Eval("ClassName") %></a></li>
                       
                        </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>

                <div class="f-right btn-right pro-btn">
                    <a href="javascript:void(0);"><img src="images/right.png" /></a>
                </div>
            </div>
            <!-- 这是产品列表 -->
            <ul class="section product-list">
                <asp:Repeater ID="rptProduct" runat="server">
                    <ItemTemplate>

                        <li>
                            <div class="product-img">
                                <img src='../<%#Eval("ProductPhoto") %>' />
                            </div>
                            <div class="product-mode">
                                <a href="productDetail.aspx?ProductID=<%#Eval("ProductID") %>">
                                    <h3><%#Eval("ProductName") %></h3>
                                </a>
                            </div>
                        </li>
                    </ItemTemplate>

                </asp:Repeater>
                <a href="javascript:void(0);" class="moreProduct" id="moreProduct" runat="server">加载更多</a>
            </ul>


        </div>

        <!-- 底部浮动导航 -->
        <div class="foot-nav">
            <!-- 返回主页 -->
            <div class="f-left fix-nav fix-home">
                <a href="index.aspx">
                    <img src="images/home.png" /></a>
            </div>
            <div class="f-left fix-nav fix-ch">
                <a href="productShow.aspx">
                    <p>产品展示</p>
                </a>
            </div>
            <div class="f-left fix-nav fix-ch">
                <a href="active.aspx">
                    <p>优惠活动</p>
                </a>
            </div>
            <div class="f-left fix-nav fix-ch">
                <a href="queryStore.aspx">
                    <p>门店查询</p>
                </a>
            </div>
            <!-- 更多菜单 -->
            <div class="f-left fix-nav fix-more">
                <a href="javascript:void(0);">
                    <p>更多</p>
                    <img src="images/icon.png" /></a>
            </div>
            <!-- 更多菜单弹出项 -->
            <div class="fix-more-nav">
                <a href="onlineAsk.aspx">在线咨询</a>
                <a href="photos.aspx">相册</a>
                <a href="news.aspx">动态</a>
            </div>
        </div>
    </div>

    <input id="txtClassID" type="hidden"  runat="server"/>
    <script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
    <script type="text/javascript" src="scripts/script.js"></script>
    <script>

        //产品分类导航

        (function () {

            var ifLen = false;

            //$(".product-nav .pro-nav-list").width($(".product-nav").width() - 2 * $(".pro-btn").width());
            $(".pro-nav-list li").width($(".pro-nav-list").width() / 4-1);
            $(".product-nav ul").width($(".product-nav li").length * ($(".product-nav li").width() + 3));
            if ($(".product-nav ul").width() < $(".pro-nav-list").width()) {
                $(".product-nav ul").css({
                    "left": "50%",
                    "margin-left": -$(".product-nav ul").width() / 2 + "px"
                });
                ifLen = false;
            } else {
                $(".product-nav ul").css("left", 0);
                ifLen = true;
            }

            var left = $(".product-nav .btn-left");
            var right = $(".product-nav .btn-right");
            var ul = $(".product-nav ul");
            var poro = ul.offset().left;
            var len = $(".product-nav li").width()+2;
            var init = 0;
            var num = $(".product-nav li").length;

            if (ifLen) {

                left.click(function () {
                    if (!ul.is(":animated")) {
                        if (init > 0) {
                            init--;
                            ul.animate({ left: -init * len }, 300)
                        }
                    }
                });

                right.click(function () {
                    if (!ul.is(":animated")) {
                        init++;
                        if (init == (num - 3)) {
                            init = 0;
                        }
                        ul.animate({ left: -init * len }, 300);
                    }
                });
            }

        }());
    </script>
</body>
</html>
