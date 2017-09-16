<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExchangeVerify.aspx.cs"
    Inherits="ChainStock.PointManage.ExchangeVerify" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/PointManage/ExchangeVerify.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmExchangeVerify" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div class="user_List_All">
                        <div style="margin-top: 5px; display: none;" id="divBackExchange">
                            <table>
                                <tr>
                                    <td class="tableStyle_left">
                                        退回备注：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtExchangeRemark" type="text" runat="server" class="border_radius" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <input id="btnOK" type="button" class="buttonColor" value="确   定" />
                                        <input id="btnCancel" type="button" class="buttonRest" value="取   消" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater ID="rptExchangeVerify" runat="server" OnItemDataBound="rptMemGiftList_ItemDataBound">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                订单编号
                                            </th>
                                            <th>
                                                会员卡号
                                            </th>
                                            <th>
                                                礼品总数量
                                            </th>
                                            <th>
                                                所需总积分
                                            </th>
                                            <th>
                                                申请时间
                                            </th>
                                            <th>
                                                兑换状态
                                            </th>
                                            <th>
                                                操作
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td>
                                            <img id="img<%# Eval("ExchangeID") %>" alt="" src="../Inc/Style/images/plus.gif"
                                                style="vertical-align: text-bottom" onload="javascript:IsShow('<%# Eval("ExchangeID") %>','1')" />
                                            <a href="javascript:ShowDetail('<%# Eval("ExchangeID") %>','1')" title="展开/收起详情">
                                                <%# Eval("ExchangeAccount")%></a>
                                        </td>
                                        <td>
                                            <%# Eval("MemCard") %>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("ExchangeAllNumber")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("ExchangeAllPoint")%>
                                        </td>
                                        <td>
                                            <%# Eval("ApplicationTime")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblExchangeStatus" runat="server" Text="申请中..."></asp:Label>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <a id="AllowExchange" runat="server" href='#' onclick='<%# string.Format(" AllowExchange(\"{0}\",\"{1}\")",Eval("ExchangeID"),Eval("ExchangeAccount")) %>'>
                                                <img src="../images/Gift/isok.png" alt="通过审核" title="通过审核" />
                                            </a><a id="NoExchange" runat="server" href='#' onclick='<%# string.Format(" NoExchange(\"{0}\")",Eval("ExchangeID")) %>'>
                                                <img src="../images/Gift/back.png" alt="退回" title="退回" /></a>
                                        </td>
                                    </tr>
                                    <tr style="display: none;" id="detail<%# Eval("ExchangeID") %>">
                                        <td colspan="8" style="padding-left: 20px; height: 30px;">
                                            <div>
                                                联系电话：<b><%# Eval("ExchangeTelePhone")%></b> &nbsp;&nbsp;&nbsp;&nbsp;配送地址：<b>
                                                    <%# Eval("ExchangeAddress")%></b> &nbsp;&nbsp;&nbsp;&nbsp;会员备注：<b>
                                                        <%# Eval("ApplicationRemark")%></b>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="display: none;" id="data<%# Eval("ExchangeID") %>">
                                        <td colspan="8" style="padding-left: 20px;">
                                            <table class="table-style table-hover user_List_txt">
                                                <asp:Repeater ID="rptExchangeVerifyDetail" runat="server">
                                                    <HeaderTemplate>
                                                        <thead class="thead">
                                                            <tr class="th">
                                                                <th>
                                                                    序号
                                                                </th>
                                                                <th>
                                                                    礼品名称
                                                                </th>
                                                                <th>
                                                                    兑换数量
                                                                </th>
                                                                <th>
                                                                    兑换总积分
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="td">
                                                            <td>
                                                                <asp:Label ID="lblDetailNumber" runat="server" Text="1"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <%# Eval("GiftName")%>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <%# Eval("ExchangeNumber")%>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <%# Eval("ExchangePoint")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="user_List_page">
                            <table style="width: 100%" id="tabPager">
                                <tr>
                                    <td>
                                        <span id="spPageSize">&nbsp;每页记录数：</span>
                                        <asp:DropDownList ID="drpPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPageSize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                        </asp:DropDownList>
                                        <webdiyer:AspNetPager ID="NetPagerParameter" runat="server" AlwaysShow="True" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条"
                                            CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页" LastPageText="尾页"
                                            NextPageText="下一页" OnPageChanging="NetPagerParameter_PageChanging" PrevPageText="上一页"
                                            ShowPageIndexBox="Always" PageSize="10" LayoutType="Table" PageIndexBoxType="DropDownList"
                                            ShowCustomInfoSection="Left" CustomInfoClass="paginator" CustomInfoSectionWidth="300px"
                                            SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" Direction="LeftToRight"
                                            HorizontalAlign="Right">
                                        </webdiyer:AspNetPager>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
