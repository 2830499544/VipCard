<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptEmptyBills.aspx.cs"
    Inherits="ChainStock.StockManage.RptEmptyBills" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../Controls/Pay.ascx" TagName="Pay" TagPrefix="uc3" %>
<%@ Register Src="../Controls/QuickSearch.ascx" TagName="QuickSearch" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/StockManage/GoodsEmptyBills.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="ftmEmptyBills" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div class="user_List_All">
                        <div class="user_List_top">
                            <table class="tableStyle" style="width: 100%">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <asp:Button ID="btnRptEmptyBillsExcel" runat="server" Text="导   出" UseSubmitBehavior="false"
                                                class="common_Button" OnClick="btnRptEmptyBillsExcel_Click" />
                                            <input id="txtUser" type="hidden" class="input_txt border_radius" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    会员信息：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtQueryMem" type="text" runat="server" class="border_radius radius2"
                                        title="会员卡号/卡面号码/姓名/手机号" />
                                </td>
                                <td class="tableStyle_left">
                                    订单编号：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtOrderAccount" type="text" runat="server" class="border_radius radius2"
                                        title="会员卡号/姓名/手机号" />
                                </td>
                                <td class="tableStyle_left">
                                    备注查询：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtRemark" type="text" runat="server" class="border_radius radius2" title="输入备注关键字查询" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    会员等级：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltMemLevelID" runat="server" class="selectWidth">
                                    </select>
                                </td>
                                <td class="tableStyle_left">
                                    消费时间：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtStartTime" type="text" runat="server" class="Wdate border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    至&nbsp;&nbsp;
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtEndTime" type="text" runat="server" class="Wdate border_radius" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    所属商家：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltShop" runat="server" class="selectWidth">
                                    </select>
                                      <input  id="HDsltshop" runat="server" type="hidden" />
                                </td>
                                <td colspan="3">
                                </td>
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnRptEmptyBillsQuery" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnRptEmptyBillsQuery_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt" cellspacing="0" cellpadding="2"
                            style="width: 100%;">
                            <asp:Repeater ID="rptEmptyBillsList" runat="server" OnItemDataBound="rptEmptyBillsList_ItemDataBound">
                                <HeaderTemplate>
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
                                            会员姓名
                                        </th>
                                        <th>
                                            应付金额
                                        </th>
                                        <th>
                                            实付金额
                                        </th>
                                        <th>
                                            应获积分
                                        </th>
                                        <th>
                                            挂单备注
                                        </th>
                                        <th>
                                            挂单商家
                                        </th>
                                        <th>
                                            操作人员
                                        </th>
                                        <th>
                                            挂单时间
                                        </th>
                                        <th>
                                            操&nbsp;&nbsp;&nbsp;&nbsp;作
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td style="width: 50px;">
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <img id="img<%# Eval("OrderID") %>" alt="" src="../Inc/Style/images/plus.gif" style="vertical-align: text-bottom"
                                                onload="javascript:IsShow('<%# Eval("OrderID") %>','3')" />
                                            <a href="javascript:ShowDetail('<%# Eval("OrderID") %>','3')" title="展开/收起详情">
                                                <%# Eval("OrderAccount")%></a>
                                        </td>
                                        <td>
                                            <%# Eval("MemCard")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("MemName")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# double.Parse(Eval("OrderTotalMoney").ToString()).ToString("0.00")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# double.Parse(Eval("OrderDiscountMoney").ToString()).ToString("0.00")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("OrderPoint")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%#Eval("OrderRemark")%>
                                        </td>
                                        <td>
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                        <td>
                                            <%# DateTime.Parse(Eval("OrderCreateTime").ToString()).ToString()%>
                                        </td>
                                        <td class="listtd" style="width: 90px;">
                                            <a id="aEditExpense" runat="server" href='<%# string.Format("~/StockManage/GoodsExpense.aspx?PID=67&OID={0}&MemCard={1}", Eval("OrderID"),Eval("MemCard"))  %>'>
                                                <img src="../images/Gift/eit.png" title="编辑" alt="编辑" />
                                            </a><a id="submint" runat="server" href='#' onclick='<%# string.Format(" SubmitEmptyBills(\"{0}\",\"{1}\",\"{2}\",\"{3}\")",Eval("MemCard"),Eval("OrderID"),Eval("OrderAccount"),Eval("OrderDiscountMoney")) %>'>
                                                <img src="../images/Gift/settle.png" alt="结算" title="结算" /></a> <a id="Cancel" runat="server"
                                                    href='#' onclick='<%# string.Format(" CancelEmptyBills(\"{0}\",\"{1}\")",Eval("OrderID"),Eval("MemID")) %>'>
                                                    <img src="../images/Gift/del.png" alt="撤销" title="撤销" /></a>
                                        </td>
                                    </tr>
                                    <tr style="display: none;" id="data<%# Eval("OrderID") %>">
                                        <td colspan="12">
                                            <asp:Repeater ID="rptEmptyBillsDetailList" runat="server">
                                                <HeaderTemplate>
                                                    <div style="padding-left: 55px;">
                                                        <table class="table-style table-hover user_List_txt" cellspacing="0" cellpadding="2"
                                                            style="width: 80%;">
                                                            <tr class="th">
                                                                <th>
                                                                    序号
                                                                </th>
                                                                <th>
                                                                    商品编码
                                                                </th>
                                                                <th>
                                                                    商品名称
                                                                </th>
                                                                <th>
                                                                    商品类型
                                                                </th>
                                                                <th>
                                                                    商品单价
                                                                </th>
                                                                <th>
                                                                    商品数量
                                                                </th>
                                                                <th>
                                                                    商品积分
                                                                </th>
                                                                <th>
                                                                    折后总价
                                                                </th>
                                                            </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="td">
                                                        <td>
                                                            <asp:Label ID="lblDetailNumber" runat="server" Text="1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%# Eval("GoodsCode")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Name")%>
                                                        </td>
                                                        <td>
                                                            <%# this.GetGoodsType(int.Parse(Eval("GoodsType").ToString()),float.Parse(Eval("OrderDetailNumber").ToString()))%>
                                                        </td>
                                                        <td>
                                                            <%# decimal.Parse(Eval("OrderDetailPrice").ToString()).ToString("0.00")%>
                                                        </td>
                                                        <td>
                                                            <%# Math.Abs(decimal.Parse(Eval("OrderDetailNumber").ToString()))%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("OrderDetailPoint")%>
                                                        </td>
                                                        <td>
                                                            <%# decimal.Parse(Eval("OrderDetailDiscountPrice").ToString()).ToString("0.00")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table> </div>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
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
    <input id="chkAllowPwd" type="checkbox" style="display: none" runat="server" />
    <uc3:Pay ID="ucPay" runat="server" />
    <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
