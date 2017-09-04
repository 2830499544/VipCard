<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXinGiftExchangeDetail.aspx.cs" Inherits="ChainStock.WeiXin.WeiXinGiftExchangeDetail" %>
<!DOCTYPE html>

<html>
<head>
    <title></title>
</head>
<body>
    <div data-role="dialog">
        <asp:Repeater ID="Rpt_WeiXinGiftExchangeDetail" runat="server">
            <HeaderTemplate><ul data-role="listview"></HeaderTemplate>
            <ItemTemplate>
                <li>
                    <a href="#" data-role="button" data-icon="back" data-iconpos="right" data-rel="back">
                        <h3 style="text-align:left;">礼品名称：<%#Eval("GiftName")%></h3>
                        <p>兑换数量：<%#Eval("ExchangeNumber")%></p>
                        <p>单个积分：<%#Eval("GiftExchangePoint")%></p>
                        <p>消费积分：<%#Eval("ExchangePoint")%></p>
                    </a>
                </li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
    </div>
</body>
</html>
