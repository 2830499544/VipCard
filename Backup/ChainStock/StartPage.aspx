<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StartPage.aspx.cs" Inherits="ChainStock.StartPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="Inc/Style/Style.css" rel="stylesheet" />  
    <link href="Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="Scripts/highcharts.js" type="text/javascript"></script>
    <script src="Scripts/grid.js" type="text/javascript"></script>    
    <script src="Scripts/FusionCharts/FusionCharts.js" type="text/javascript"></script>
    <script src="Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="Scripts/Module/System/StartPage.js" type="text/javascript"></script>
    <script src="Scripts/Module/Common/Tab.js" type="text/javascript"></script>
    <script src="Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="Scripts/jquery.SuperSlide.2.1.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" id="txtMemStartTime" runat="server" />
    <input type="hidden" id="txtMemEndTime" runat="server" />
    <input type="hidden" id="sltShop" runat="server" />
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr style="">
            <td colspan="2" style="width: 100%; text-align: left;">
                <!--系统信息-->
                <div class="system_Info">
                    <div class="system_Top system_detail">
                        系统信息
                    </div>
                    <div class="system_user">
                    </div>
                    <div class="user_Info">
                        <p class="admin">
                            <b>
                                <asp:Literal runat="server" ID="lblShopName" Text="Label"></asp:Literal>&nbsp;&nbsp;
                            </b>操作权限：<b><asp:Literal ID="lbGroupName" runat="server" Text="Label"></asp:Literal></b>
                        </p>
                        <p class="cash">
                            新增会员：<b>今日 <span>
                                <asp:Literal runat="server" ID="ltlMemToday" Text="Label"></asp:Literal></span>
                                人/昨日 <span>
                                    <asp:Literal runat="server" ID="ltlMemYesterday" Text="Label"></asp:Literal></span>
                                人</b> 充值金额：<b> 今日 <span>
                                    <asp:Literal runat="server" ID="lblrMoneyToday" Text="Label"></asp:Literal></span>
                                    元/昨日 <span>
                                        <asp:Literal runat="server" ID="lblrMoneyYesterday" Text="Label"></asp:Literal></span>
                                    元</b> 现金收入：<b>今日 <span>
                                        <asp:Literal runat="server" ID="lblGetMoneyToday" Text="Label"></asp:Literal></span>
                                        元/昨日 <span>
                                            <asp:Literal runat="server" ID="lblGetMoneyYesterday" Text="Label"></asp:Literal></span>
                                        元</b>
                        </p>                       
                        <p class="contact">                    
                           <label id="lblPoint" runat="server"> 剩余积分量：
                           <b style="color:#ff3300;font-weight: bold;"><asp:Literal ID="lblPointCount" runat="server" Text="" ></asp:Literal> </b>&nbsp;
                           </label>
                           <label id="lblSms" runat="server"> 剩余短信量：
                           <b style="color:#ff3300;font-weight: bold;"><asp:Literal ID="lblSmsCount" runat="server" Text=""></asp:Literal> </b>&nbsp;
                           </label>
                            联系人：<b><asp:Literal ID="lblShopMem" runat="server" Text="Label"></asp:Literal></b>&nbsp; 
                            电话：<b><asp:Literal ID="lblShopPhone" runat="server" Text="Label"></asp:Literal></b>&nbsp; 
                            地址：<b><asp:Literal ID="lblShopAddress" runat="server" Text="Label"></asp:Literal></b>
                        </p>                  
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 50%;">
                <!--系统公告-->
                <div class="system_Info system_Announce" style="width: 100%;">
                    <div class="system_Top system_notice">
                        系统公告
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <div class="system_user system_pic" >
                                    </div>
                                </td>
                                <td>
                                    <div class="user_Info announce_list" style="float: none; height:100%;">
                                        <asp:Repeater ID="rpNotice" runat="server">
                                            <ItemTemplate>
                                                <div class="announce_text">
                                                    <div class="number">
                                                        <%# Container.ItemIndex+1 %></div>
                                                    <a id="SysNoticeShow" href="javascript:void(0);" onclick="javascript:NoticeShow('<%#Eval("SysNoticeID") %>')">
                                                        <%# Eval("SysNoticeTitle").ToString().Length > 20 ? Eval("SysNoticeTitle").ToString().Substring(0, 20) + "..." : Eval("SysNoticeTitle").ToString()%></a>
                                                    <div class="date">
                                                        <%# Eval("SysNoticeTime", "{0:yyyy-MM-dd}")%></div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
            <td style="width: 50%">
                <!--会员趋势图-->
                <div class="system_Info system_Announce uder_Trend">
                    <div class="system_Top system_trend">
                        会员趋势图
                    </div>
                    <div id="container" style="width: auto; height: 182px; overflow: hidden;">
                    </div>
                </div>
            </td>
        </tr>
        <tr style="vertical-align: top">
            <td colspan="2" style="width: 100%; vertical-align: top">
                <!--系统提醒-->
                <div class="system_Info notice">
                    <div class="system_Top tab-hd" style="padding-left: 0px;">
                        <ul class="tab-nav">
                            <li>会员生日提醒</li><b>|</b>
                            <li>会员期限提醒</li><b>|</b>
                            <li>积分清零提醒</li><b>|</b>
                            <li>库存不足提醒</li><b>|</b>
                            <li>自定义提醒</li>
                        </ul>
                        <div style="float: right; margin-right: 10px; font-size: 12px; margin-top: 5px;">
                            <a href="Common/TodayRemind.aspx?PID=93">
                                <img src="Inc/Style/images/more.gif" alt="更多" title="更多" /></a>
                        </div>
                    </div>
                    <div class="tab-bd">
                        <div class="tab-pal">
                            <table class="table-style table-hover">
                                <asp:Repeater runat="server" ID="gvMemBirthday">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    序号
                                                </th>
                                                <th>
                                                    会员卡号
                                                </th>
                                                <th>
                                                    会员姓名
                                                </th>
                                                <th>
                                                    会员等级
                                                </th>
                                                <th>
                                                    手机号码
                                                </th>
                                                <th>
                                                    生日
                                                </th>
                                                <th>
                                                    积分
                                                </th>
                                                <th>
                                                    所属店铺
                                                </th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="td">
                                            <td>
                                                <asp:Literal ID="lblNumber" runat="server" Text="1"></asp:Literal>
                                            </td>
                                            <td>
                                                <b>
                                                    <%# Eval("MemCard")%></b>
                                            </td>
                                            <td>
                                                <%# Eval("MemName") %>
                                            </td>
                                            <td>
                                                <%# Eval("LevelName") %>
                                            </td>
                                            <td>
                                                <%# Eval("MemMobile") %>
                                            </td>
                                            <td>
                                                <%# Eval("MemBirthday", "{0:yyyy-MM-dd}")%>
                                            </td>
                                            <td>
                                                <%# Eval("MemPoint") %>
                                            </td>
                                            <td>
                                                <%# Eval("ShopName") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                        <div class="tab-pal">
                            <table class="table-style table-hover">
                                <asp:Repeater runat="server" ID="gvMemPastTime">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    序号
                                                </th>
                                                <th>
                                                    会员卡号
                                                </th>
                                                <th>
                                                    会员姓名
                                                </th>
                                                <th>
                                                    会员等级
                                                </th>
                                                <th>
                                                    手机号码
                                                </th>
                                                <th>
                                                    过期时间
                                                </th>
                                                <th>
                                                    积分
                                                </th>
                                                <th>
                                                    所属店铺
                                                </th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="td">
                                            <td>
                                                <asp:Literal ID="lblNumber" runat="server" Text="1"></asp:Literal>
                                            </td>
                                            <td>
                                                <b>
                                                    <%# Eval("MemCard") %></b>
                                            </td>
                                            <td>
                                                <%# Eval("MemName") %>
                                            </td>
                                            <td>
                                                <%# Eval("LevelName") %>
                                            </td>
                                            <td>
                                                <%# Eval("MemMobile") %>
                                            </td>
                                            <td>
                                                <%# Eval("MemPastTime","{0:yyyy-MM-dd}") %>
                                            </td>
                                            <td>
                                                <%# Eval("MemPoint") %>
                                            </td>
                                            <td>
                                                <%# Eval("ShopName") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                        <div class="tab-pal">
                            <table class="table-style table-hover">
                                <asp:Repeater runat="server" ID="gvdMemPontReset">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    序号
                                                </th>
                                                <th>
                                                    会员卡号
                                                </th>
                                                <th>
                                                    会员姓名
                                                </th>
                                                <th>
                                                    会员等级
                                                </th>
                                                <th>
                                                    手机号码
                                                </th>
                                                <th>
                                                    积分
                                                </th>
                                                <th>
                                                    最后一次消费时间
                                                </th>
                                                <th>
                                                    消费总次数
                                                </th>
                                                <th>
                                                    所属店铺
                                                </th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="td">
                                            <td>
                                                <asp:Literal ID="lblNumber" runat="server" Text="1"></asp:Literal>
                                            </td>
                                            <td>
                                                <b>
                                                    <%# Eval("MemCard") %></b>
                                            </td>
                                            <td>
                                                <%# Eval("MemName") %>
                                            </td>
                                            <td>
                                                <%# Eval("LevelName") %>
                                            </td>
                                            <td>
                                                <%# Eval("MemMobile") %>
                                            </td>
                                            <td>
                                                <%# Eval("MemPoint") %>
                                            </td>
                                            <td>
                                                <%# Eval("MemConsumeLastTime","{0:yyyy-MM-dd}") %>
                                            </td>
                                            <td>
                                                <%# Eval("MemConsumeCount") %>
                                            </td>
                                            <td>
                                                <%# Eval("ShopName") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                        <div class="tab-pal">
                            <table class="table-style table-hover">
                                <asp:Repeater runat="server" ID="gvGoods">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    序号
                                                </th>
                                                <th>
                                                    商品名称
                                                </th>
                                                <th>
                                                    商品编码
                                                </th>
                                                <th>
                                                    商品简码
                                                </th>
                                                <th>
                                                    库存数量
                                                </th>
                                                <th>
                                                    创建时间
                                                </th>
                                                <th>
                                                    单价
                                                </th>
                                                <th>
                                                    所属店铺
                                                </th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="td">
                                            <td>
                                                <asp:Literal ID="lblNumber" runat="server" Text="1"></asp:Literal>
                                            </td>
                                            <td>
                                                <b>
                                                    <%# Eval("Name") %></b>
                                            </td>
                                            <td>
                                                <%# Eval("GoodsCode") %>
                                            </td>
                                            <td>
                                                <%# Eval("NameCode") %>
                                            </td>
                                            <td>
                                                <%# Eval("Number") %>
                                            </td>
                                            <td>
                                                <%# Eval("GoodsCreateTime","{0:yyyy-MM-dd}") %>
                                            </td>
                                            <td>
                                                <%# Eval("Price","{0:F2}") %>
                                            </td>
                                            <td>
                                                <%# Eval("ShopName") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                        <div class="tab-pal">
                            <table class="table-style table-hover">
                                <asp:Repeater runat="server" ID="gvCustomRemind">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    序号
                                                </th>
                                                <th>
                                                    提醒标题
                                                </th>
                                                <th>
                                                    提醒详情
                                                </th>
                                                <th>
                                                    提醒时间
                                                </th>
                                                <th>
                                                    发布提醒时间
                                                </th>
                                                <th>
                                                    发布提醒店铺
                                                </th>
                                                <th>
                                                    发布提醒操作员
                                                </th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="td">
                                            <td>
                                                <asp:Literal ID="lblNumber" runat="server" Text="1"></asp:Literal>
                                            </td>
                                            <td>
                                                <b>
                                                    <%# Eval("CustomRemindTitle") %></b>
                                            </td>
                                            <td>
                                                <%# Eval("CustomRemindDetail") %>
                                            </td>
                                            <td>
                                                <%# Eval("CustomRemindTime","{0:yyyy-MM-dd}") %>
                                            </td>
                                            <td>
                                                <%# Eval("CustomRemindCreateTime","{0:yyyy-MM-dd}") %>
                                            </td>
                                            <td>
                                                <%# Eval("ShopName") %>
                                            </td>
                                            <td>
                                                <%# Eval("UserName") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        jQuery(".system_Info").slide({ titCell: ".tab-hd li", mainCell: ".tab-bd", delayTime: 0 });
    </script>

    </form>
</body>
</html>
