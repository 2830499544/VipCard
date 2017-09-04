<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="WeiXinCoupon.aspx.cs" Inherits="ChainStock.WeiXin.WeiXinCoupon" %>
<!DOCTYPE html>
<html>
<head>
    <title>优惠券</title>
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <link href="../Inc/Style/jquery.mobile-1.2.0.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Module/WeiXin/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/WeiXin/jquery.mobile-1.2.0.min.js" type="text/javascript"></script>
</head>
<body>
    <div data-role="page">
        <a href="#dialog" data-rel="dialog" id="showDialog" style="display:none;"></a>
        <asp:Repeater ID="Rpt_WeiXinCoupon" runat="server" onitemdatabound="Rpt_WeiXinCoupon_ItemDataBound">
            <HeaderTemplate><ul data-role="listview"></HeaderTemplate>
            <ItemTemplate>
                <li>
				    <a href="#" runat="server" id="application">
                        <h3>优惠券名：<%#Eval("CouponTitle")%></h3>
                        <p>使用说明：<span runat="server" id="spDesc"></span></p>
                        <p>有效期限：<span runat="server" id="spTime"></span></p>
                        <p><span runat="server" id="spCouPon"></span></p>
                    </a>
			    </li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
        <div id="noProduct" runat="server" style="text-align:center;display:none;"><h3>很抱歉，本店暂无优惠券活动！</h3></div>
        <a data-role="button" data-theme="c" href="MemberManipulate.aspx?MemWeiXinCard=<%=MemWeiXinCard %>&rc=<%=rc %>" rel="external" >返回</a>
    </div>
</body>
</html>
