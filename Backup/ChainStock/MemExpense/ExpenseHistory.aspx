<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpenseHistory.aspx.cs"
    Inherits="ChainStock.MemExpense.ExpenseHistory" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="../Controls/QuickSearch.ascx" tagname="QuickSearch" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/Module/MemExpense/ExpenseHistory.js" type="text/javascript"></script>
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>
    <style type="text/css">


p.MsoNormal{
margin-bottom:.0001pt;
text-align:justify;
text-justify:inter-ideograph;
font-family:Calibri;
font-size:10.5000pt;
            margin-left: 0pt;
            margin-right: 0pt;
            margin-top: 0pt;
        }

    </style>
</head>
<body>
    <form id="frmExpHistory" runat="server">
    <input type="hidden" runat="server" id="txtUser" />
    <input type="hidden" runat="server" id="txtShopid" />
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                 <%--打印的次数 --%>
                 <input type="hidden" value="" id="PointNum" runat="server"/>
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div class="user_List_All">
                        <div class="user_List_top">
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <input id="txtQueryMem" type="text" runat="server" class="border_radius radius2"
                                            title="会员卡号/卡面号码/姓名/手机号" />
                                        <div class="user_List_Button">
                                            <asp:Button ID="Button1" OnClientClick="return btQuery();" runat="server" Text="查   询"
                                                class="common_Button" OnClick="btnRptExpenseQuery_Click" />
                                            <input id="btnhigthSearch" type="button" class="common_Button" value="高级查询" />
                                            <asp:Button ID="btnExpenseExcel" OnClientClick="return btQuery();" runat="server"
                                                Text="导   出" TabIndex="0" class="common_Button" OnClick="btnExpenseExcel_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divHightSearch" style="display: none">
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr>
                                    <td class="tableStyle_left">
                                        订单编号：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtOrderAccount" type="text" runat="server" class="border_radius radius2"
                                            title="订单编号" />
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
                                    <td class="tableStyle_left">
                                        消费商家：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltConsumptionShop" runat="server" class="selectWidth">
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        实收金额：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltDiscountMoney" runat="server" style="height: 25px; outline: none;
                                            resize: none;">
                                            <option selected="selected" value="&gt;=">>=</option>
                                            <option value="&lt;="><=</option>
                                            <option value="=">=</option>
                                        </select>
                                        <input id="txtDiscountMoney" type="text" runat="server" class="border_radius radius2"
                                            style="width: 122px; float: none;" />
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
                                        消费类型：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltOrderType" runat="server" class="selectWidth">
                                            <option value="">===== 请选择 =====</option>
                                            <option value="-1">散客消费</option>
                                            <option value="0">快速消费</option>
                                            <option value="1">计时消费</option>
                                            <option value="7">计次消费</option>
                                            <option value="2">商品消费</option>
                                            <option value="4">商品撤单</option>
                                            <option value="6">商品退货</option>
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        应收金额：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltTotalMoney" runat="server" style="height: 25px; outline: none; resize: none;">
                                            <option selected="selected" value="&gt;=">>=</option>
                                            <option value="&lt;="><=</option>
                                            <option value="=">=</option>
                                        </select>
                                        <input id="txtTotalMoney" type="text" runat="server" class="border_radius radius2"
                                            style="width: 122px; float: none;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        备注查询：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtRemark" type="text" runat="server" class="border_radius radius2" title="输入备注关键字查询" />
                                    </td>
                                    <td colspan="3">
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="btnMemReset" type="button" value="重   置" class="common_Button" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt" cellspacing="0" cellpadding="2"
                            style="width: 100%;">
                            <asp:Repeater ID="rptExpenseHistory" runat="server" OnItemDataBound="rptExpenseHistory_ItemDataBound">
                                <HeaderTemplate>
                                    <tr class="th">
                                        <th>
                                            序号
                                        </th>
                                        <th style="width: 170px">
                                            订单编号
                                        </th>
                                        <th>
                                            会员姓名
                                        </th>
                                        <th>
                                            会员卡号
                                        </th>
                                        <th>
                                            消费类型
                                        </th>
                                        <th>
                                            消费总额
                                        </th>
                                        <th>
                                            折后总价
                                        </th>
                                        <th>
                                            优惠券金额
                                        </th>
                                         <th>
                                            抵用积分
                                        </th>
                                          <th>
                                            抵用金额
                                        </th>
                                        <th>
                                            获得积分
                                        </th>
                                        <th>
                                            卡内余额
                                        </th>
                                        <th>
                                            消费时间
                                        </th>
                                        <th>
                                            消费商家
                                        </th>
                                        <th>
                                            操作人员
                                        </th>
                                        <th>
                                            备注
                                        </th>
                                        <th>
                                            操&nbsp;&nbsp;作
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td style="width: 50px;">
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 170px">
                                            <img id="img<%# Eval("OrderID") %>" alt="" src="../Inc/Style/images/plus.gif"
                                                style="vertical-align: text-bottom" onload="javascript:IsShowPic('<%# Eval("OrderID") %>','<%# Eval("Count")%>')" />
                                            <a id="a<%# Eval("OrderID") %>" href="javascript:ShowExpenseDetail('<%# Eval("OrderID") %>','<%# Eval("Count")%>')"
                                                title="展开/收起详情">
                                                <%# Eval("OrderAccount")%></a>
                                        </td>
                                        <td style="text-align: left">
                                            <%#Eval("MemName")%>
                                        </td>
                                        <td>
                                            <%# this.GetMemCard(Eval("MemCard").ToString())%>
                                        </td>
                                        <td style="color: Red">
                                            <%# this.GetOrderType(int.Parse(Eval("OrderType").ToString()))%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# decimal.Parse(Eval("OrderTotalMoney").ToString()).ToString("0.00")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# decimal.Parse(Eval("OrderDiscountMoney").ToString()).ToString("0.00")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# decimal.Parse(Eval("OrderPayCoupon").ToString()).ToString("0.00")%>
                                        </td>
                                         <td style="text-align: right">
                                            <%# decimal.Parse(Eval("UsePoint").ToString()).ToString("0")%>
                                        </td>
                                         <td style="text-align: right">
                                            <%# decimal.Parse(Eval("UsePointAmount").ToString()).ToString("0.00")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Convert.ToInt32(Eval("OrderPoint"))%>
                                        </td>
                                        <td style="text-align: right">
                                            <%#Eval("OrderCardBalance","{0:F2}")%>
                                        </td>
                                        <td>
                                            <%# DateTime.Parse(Eval("OrderCreateTime").ToString()).ToString()%>
                                        </td>
                                        <td>
                                            <%#Eval("ShopName") %>
                                        </td>
                                        <td>
                                            <%#Eval("UserName") %>
                                        </td>
                                        <td>
                                            <a href="#" title='<%# Eval("OrderRemark") == "" ? "无" : Eval("OrderRemark")%>'>备注详情</a>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <label id="tdUser" runat="server" visible='<%# GetPrint(Convert.ToInt32(Eval("OrderType")))%>'>
                                                <span id="print" runat="server"><a href="javascript:Print('<%# Eval("OrderID") %>',<%# Eval("OrderType") %>,<%# Eval("OrderMemID") %>)">
                                                    <img src="../images/Gift/print.png" alt="重打小票" />
                                                </a></span><span id="spRevoke" runat="server" visible='<%# GetExpenseIsReturn(Eval("OrderAccount").ToString(), Convert.ToInt32(Eval("OrderType")))%>'>
                                                    <a href="javascript:Revoke('<%# Eval("OrderID") %>','<%# Eval("OrderMemID") %>','<%# Eval("OrderShopid") %>')"
                                                        title="撤销整单">
                                                        <img src="../images/Gift/revoke.png" alt="撤单" title="撤单" />
                                                    </a></span>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr style="display: none;" id="data<%# Eval("OrderID") %>">
                                        <td colspan="15">
                                            <asp:Repeater ID="rptExpenseDetail" runat="server">
                                                <HeaderTemplate>
                                                    <div style="padding-left: 50px;">
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
                                                                    获得积分
                                                                </th>
                                                                <th>
                                                                    折后总价
                                                                </th>
                                                            </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="td" id="drDataDetail<%# Eval("OrderID")%>">
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
                                                            <%# (Eval("OrderDetailNumber").ToString())%>
                                                        </td>
                                                        <td>
                                                            <%# Convert.ToInt32(Eval("OrderDetailPoint"))%>
                                                        </td>
                                                        <td>
                                                            <%# decimal.Parse(Eval("OrderDetailDiscountPrice").ToString()).ToString("0.00")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table></div>
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
    <asp:Label ID="lblPrintTitle" Style="display: none" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lblPrintFoot" Style="display: none" runat="server" Text="Labe2"></asp:Label>    
    <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
