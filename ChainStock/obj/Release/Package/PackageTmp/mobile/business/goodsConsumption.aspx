<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="goodsConsumption.aspx.cs" Inherits="ChainStock.mobile.business.goodsConsumption" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="renderer" content="webkit|ie-comp|ie-stand" />
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <title>商品消费</title>
    <link rel="stylesheet" type="text/css" href="css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="css/common.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link rel="stylesheet" type="text/css" href="css/media.css" />
    <link rel="stylesheet" type="text/css" href="css/color.css" />
</head>
<body>
    <div class="section index" id="container" style="margin-bottom: 1.6rem;">
        <div id="head" class="section">
            <div class="section header">
                <h1>商品消费</h1>
                <a href="javascript:void(0);" class="back-btn">
                    <img src="images/prev.png" /></a>
                <!-- <a href="index.html" class="head-icon" id="home"><img src="images/home_f.png"/></a> -->
            </div>
        </div>
        <div id="content" class="section" style="background-color: #f7f7f7;">
            <div class="section">
                <div class="section line_box">
                    <div class="line_group">
                        <p class="f-left">订单编号</p>
                        <div class="f-right">
                            <!-- 获取订单号 -->
                            <p id="spOrderAccount" runat="server"></p>
                        </div>
                    </div>
                    <div class="line_group">
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
                            <input type="text" name="memCard" id="memCard" placeholder="请输入会员卡号" />
                        </div>
                    </div>
                    <div class="line_group" id="memberName" style="display: none;">
                        <p class="f-left">会员姓名</p>
                        <div class="f-right">
                            <!-- 根据卡号获取会员姓名 -->
                            <p id="spMemName">未找到该会员</p>
                            <p id="spMemMoney">0</p>
                        </div>
                        <div style="display: none;">
                            <input type="hidden" id="txtMemMoney" value="0" />
                            <input type="hidden" id="txtMemID" value="" runat="server" />
                            <input type="hidden" id="hidMemState" value="" />
                            <input type="hidden" id="hidMemisPast" value="" />
                        </div>
                    </div>
                </div>

                <!-- <div class="section in-search">
					<input type="text" placeholder="输入商品名称进行查找" />
					<a href="##"><img src="images/search.png" alt="搜索" /></a>
				</div> -->

                <div class="section bg-fff" style="margin-top: 0.1rem;">

                    <!-- 商品分类 -->
                    <div class="f-left gift-nav">

                        <ul class="section">
                            <li>
                                <a href="goodsConsumption.aspx"  id="allclass" class="active" runat="server">所有分类</a>
                            </li>
                            <asp:Repeater ID="rptGoodsClass" runat="server" OnItemDataBound="rptGoodsClass_ItemDataBound">
                                <ItemTemplate>
                                    <li>
                                        <a id="classname" runat="server" href='<%#"goodsConsumption.aspx?ClassID="+Eval("ClassID")%>'><%#Eval("ClassName") %></a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>

                    <div class="section f-right gift-list">
                        <!-- 商品列表 -->
                        <ul class="section">
                            <asp:Repeater ID="rptGoodsList" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <div class="f-left gift-txt">
                                            <h3><%#Eval("Name") %></h3>
                                            <p>￥<span class="gift-integral" style="color:#16a2de">
                                          <%# (Eval("Price","{0:f2}")) %>
                                            
                                            </p>
                                            
                                         
                                        
                                            <input type="hidden" value="<%#Eval("GoodsID") %>" />
                                           
                                        </div>
                                        <!-- 增减控制器 -->
                                        <div class="buy-ctrl">
                                         
                                            <a href="##" class="reduceBtn">
                                                <img src="images/reduce.png" />
                                            </a>
                                            <span class="goodsPoint" ><%#Eval("Point")%></span>
                                            <span class="goodsid" ><%#Eval("GoodsID")%></span>                                        
                                            <span class="goodsPrice"><%#Eval("Price") %> </span>
                                             <span class="goodsType"><%#Eval("GoodsType") %> </span>
                                       
                                            <!-- 这里默认为0 -->
                                            <input type="text" value="0" class="giftTxt" />
                                             
                                            <a href="##" class="plusBtn"> 
                                                <img src="images/plus.png" /></a>
                                            <a href="##" class="plusShow">
                                                <img src="images/plus1.png" /></a>
                                            
                                                
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <a  id="moreGoods" class="moreGoods" runat="server" href="##">加载更多...</a>
                        </ul>
                    </div>
                </div>

                <div class="gift-fix">
                    <p>共<span class="gift-num" style=" color:#16a2de">0</span>件 积分:<span class="goods-point" style=" color:#16a2de">0 </span></p>
                     
                    <h3>总价:<span class="gift-total" style=" color:#16a2de"> 0</span></h3>
                    <a href="##" id="nowBuy" class="nowBuy" style=" background:#16a2de">立即购买</a>
                    <div class="dis-n city-mode">
                        <div class="city-query">
                            <div class="section line_box">
                                <div class="line_group">
                                    <p class="f-left">消费方式</p>
                                    <div class="f-right">
                                       <a href="javascript:void(0);" class="line_btn active" id="balance">余额</a>
                                         <a href="javascript:void(0);" class="line_btn xianjin"  id="cash">现金</a>

                                    </div>
                                </div>
                                <div class="line_group dis-n">
                                    <p class="f-left">优惠券号</p>
                                    <div class="f-right">
                                        <input type="text" name="" placeholder="请输入优惠券号码">
                                    </div>
                                </div>
                            </div>

                            <div class="section ad-detail">
                                <a href="##" class="settlement" id="goodsExpenseSure" style=" background:#16a2de">立即结算</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <input type="hidden" id="hidIsMem" runat="server" value="1" />
            <input type="hidden" id="hidPayType" value="1" />
            <input type="hidden" id="txtShopID" runat="server" value="" />
            <input type="hidden" id="hidUserID" runat="server" value="" />
        </div>
        <!-- 底部浮动导航 -->
        <div class="foot-nav">
            <div class="f-left fix-nav fix-home">
                <a href="index.aspx">
                    <img src="images/home.png" /></a>
            </div>
    <%--        <div class="f-left fix-nav fix-ch">
                <a href="memberCard.aspx">
                    <p>会员办卡</p>
                </a>
            </div>
            <div class="f-left fix-nav fix-ch">
                <a href="##">
                    <p>收银记账</p>
                    <img src="images/icon.png" /></a>
              <div class="foot-more">
                    <a href="fastConsumption.aspx">快速消费</a>
                    <a href="goodsConsumption.aspx">商品消费</a>
                    <a href="timesConsumption.aspx">计次消费</a>
                </div>
            </div>--%>
            <div class="f-left fix-nav fix-ch">
                <a href="bossCenter.aspx">
                    <p>老板中心</p>
                </a>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
       <script type="text/javascript" src="scripts/script.js"></script>   

    <script type="text/javascript">

        $("#memCard").focus();

        $("#memCard").keyup(function () {

            var memCard = $.trim($("#memCard").val());
            $("#txtMemMoney").val("0");
            $("#spMemNoney").html("0");
            $("#spMemName").html("");

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
                             $("#spMemNoney").html(json[0].MemMoney);
                             $("#memberName").show();

                             $("#txtMemMoney").val(json[0].MemMoney);
                             $("#txtMemID").val(json[0].MemID);

                         }

                     }, "json");
            }

             });

 
    </script>
 
</body>
</html>
