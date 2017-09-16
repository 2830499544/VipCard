<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberManipulate.aspx.cs"
    Inherits="ChainStock.WeiXin.MemberManipulate" %>

<!DOCTYPE html>
<html>
<head>
    <title>
        <%=GetCardDescription()%></title>
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
    <link href="../Inc/Style/jquery.mobile-1.2.0.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Module/WeiXin/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/WeiXin/jquery.mobile-1.2.0.min.js" type="text/javascript"></script>
    <style type="text/css">
        .ui-mobile img
        {
            max-width: 100%;
        }
    </style>
</head>
<body>
    <div style="margin: 10px; padding: 0 10px;">
        <img id="weixinCardImg" runat="server" src="http://www.vip5968.com/Upload/WeiXin/Images/odbeujid7Ka_bRz1UaOm4ijr6vS0.jpg" />
    </div>
    <div data-role="collapsible" style="margin: 5px 10px; padding: 5px 10px;" data-theme="b">
        <h3>
            我的信息</h3>
        <ul data-role="listview">
            <li><a href="#" id="weixinUpdateInfo" runat="server" rel="external">
                <img src="../images/ico/vcard_add.png" class="ui-li-icon" />修改个人信息</a></li>
        </ul>
    </div>
    <div data-role="collapsible" style="margin: 5px 10px; padding: 5px 10px;" data-theme="b">
        <h3>
            我的记录</h3>
        <ul data-role="listview">
            <li><a href="#" id="weixinRecharge" runat="server" rel="external">
                <img src="../images/ico/report.png" class="ui-li-icon" />查看充值记录</a></li>
            <li><a href="#" id="weixinExpense" runat="server" rel="external">
                <img src="../images/ico/report.png" class="ui-li-icon" />查看消费记录</a></li>
            <li><a href="#" id="weixinPointChange" runat="server" rel="external">
                <img src="../images/ico/report.png" class="ui-li-icon" />查看积分变动</a></li>
            <li><a href="#" id="weixinConvertHistory" runat="server" rel="external">
                <img src="../images/ico/report.png" class="ui-li-icon" />查看礼品兑换</a></li>
        </ul>
    </div>
    <div data-role="collapsible" style="margin: 5px 10px; padding: 5px 10px;" data-theme="b">
        <h3>
            礼品中心</h3>
        <ul data-role="listview">
            <li><a href="#" id="weixinPointExchange" runat="server" rel="external">
                <img src="../images/ico/card_gift.png" class="ui-li-icon" />兑换礼品</a></li>
        </ul>
    </div>
    <div data-role="collapsible" style="margin: 5px 10px; padding: 5px 10px;" data-theme="b">
        <h3>
            优惠活动</h3>
        <ul data-role="listview">
            <li><a href="#" runat="server" id="weixinLookCoupon" rel="external">
                <img src="../images/ico/privilege.png" class="ui-li-icon" />查看优惠券</a></li>
        </ul>
    </div>
</body>
</html>
