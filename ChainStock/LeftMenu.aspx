<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.aspx.cs" Inherits="ChainStock.LeftMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-common.js" type="text/javascript"></script>
    <link href="Inc/Style/Style.css" rel="stylesheet" />
    <asp:Literal ID="litObject" runat="server" />
</head>
<body style="">
    <form id="form1" runat="server">
    <div class="left_List">
        <h2>
            常用功能</h2>
        <div class="func" id="div1" runat="server">
            <a href="StartPage.aspx" target="mainFrame">
                <img src="Inc/Style/images/list_home.gif">系统首页</a></div>
        <div class="func" id="div10" runat="server" visible="false" style="display:none;">
            <a href="Member/MemInfo.aspx?PID=42" target="mainFrame">
                <img src="Inc/Style/images/card.png">会员刷卡</a></div>
        <div class="func" id="div3" runat="server">
            <a href="Member/MemRegister.aspx?PID=2" target="mainFrame">
                <img src="Inc/Style/images/list_user.gif">会员登记</a></div>
        <div class="func" id="div4" runat="server">
            <a href="StockManage/GoodsExpense.aspx?PID=67" target="mainFrame">
                <img src="Inc/Style/images/list_shoppping.gif">商品消费</a></div>
        <div class="func" id="div5" runat="server">
            <a href="MemExpense/Expense.aspx?PID=17" target="mainFrame">
                <img src="Inc/Style/images/list_fast.gif">快速消费</a></div>
        <div class="func" id="div11" runat="server">
            <a href="Member/MemRechargeCount.aspx?PID=66" target="mainFrame">
                <img src="Inc/Style/images/memcount.gif">会员充次</a></div>
        <div class="func" id="div6" runat="server">
            <a href="Member/MemRechargeMoney.aspx?PID=4" target="mainFrame">
                <img src="Inc/Style/images/list_recharge.gif">会员充值</a></div>
        <div class="func" id="div7" runat="server">
            <a href="PointManage/GiftExchange.aspx?PID=14" target="mainFrame">
                <img src="Inc/Style/images/list_gift.gif">积分兑换</a></div>
        <div class="func" id="div8" runat="server">
            <a href="ExtraService/SendMessage.aspx?PID=46" target="mainFrame">
                <img src="Inc/Style/images/list_mess.gif">短信发送</a></div>
        <div class="func" id="div2" runat="server">
            <a href="Common/UserWork.aspx?PID=106" target="mainFrame">
                <img src="Inc/Style/images/list_swich.gif">快速交班</a></div>
        <div class="func" id="div9" runat="server" style="display: none">
            <a href="Common/DataExcelIn.aspx?PID=11" target="mainFrame">
                <img src="Inc/Style/images/list_backup.gif">会员导入</a></div>
    </div>
    </form>
</body>
</html>
