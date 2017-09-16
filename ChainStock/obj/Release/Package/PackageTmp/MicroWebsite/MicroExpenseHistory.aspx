<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MicroExpenseHistory.aspx.cs"
    Inherits="ChainStock.MicroWebsite.MicroExpenseHistory" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    <link href="../Inc/Skin/default/formMain.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/Skin/default/newstyle.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/Module/MicroWebsite/MicroExpenseHistory.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmMicroExpHistory" runat="server">
    <input type="hidden" runat="server" id="txtUser" />
    <input type="hidden" runat="server" id="txtShopid" />
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
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <asp:Button ID="btnExpenseExcel" OnClientClick="return btQuery();" runat="server"
                                                Text="导   出" class="common_Button" OnClick="btnExpenseExcel_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="tableStyle" style="width: 100%">
                            <tr>
                                <td class="tableStyle_left">
                                    快速查找：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtQueryMem" type="text" runat="server" class="input_txt border_radius"
                                        title="会员卡号/姓名/手机号" />
                                </td>
                                <td class="tableStyle_left">
                                    会员等级：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltMemLevelID" runat="server" class="selectWidth">
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
                                    <input id="txtTotalMoney" type="text" style="float: none; width: 60px;" runat="server"
                                        class="Wdate border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    订单编号：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtOrderAccount" type="text" runat="server" class="input_txt border_radius"
                                        title="订单编号" />
                                </td>
                            </tr>
                            <tr>
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
                                    <input id="txtDiscountMoney" type="text" style="float: none; width: 60px;" runat="server"
                                        class="Wdate border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                </td>
                                <td>
                                    <div class="user_List_Button">
                                        <asp:Button ID="Button1" OnClientClick="return btQuery();" runat="server" Text="查   询"
                                            class="common_Button" OnClick="btnRptExpenseQuery_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater ID="rptExpenseHistory" runat="server" OnItemDataBound="rptExpenseHistory_ItemDataBound">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                账单号
                                            </th>
                                            <th>
                                                会员姓名
                                            </th>
                                            <th>
                                                会员卡号
                                            </th>
                                            <th>
                                                消费总额
                                            </th>
                                            <th>
                                                折后总价
                                            </th>
                                            <th>
                                                优惠劵金额
                                            </th>
                                            <th>
                                                获得积分
                                            </th>
                                            <th>
                                                支付后余额
                                            </th>
                                            <th>
                                                消费时间
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                            <asp:Label runat="server" ID="lblNumber"></asp:Label>
                                        </td>
                                        <td style="text-align: left; width: 200px">
                                            <img id="img<%# Eval("MicroOrderID") %>" alt="" src="../Inc/Skin/default/images/plus.gif"
                                                style="vertical-align: text-bottom" onload="javascript:IsShowPic('<%# Eval("MicroOrderID") %>','<%# Eval("Count")%>')" />
                                            <a id="a<%# Eval("MicroOrderID") %>" href="javascript:ShowExpenseDetail('<%# Eval("MicroOrderID") %>','<%# Eval("Count")%>')"
                                                title="展开/收起详情">
                                                <%# Eval("MicroOrderAccount")%></a>
                                        </td>
                                        <td style="text-align: left;">
                                            <%#Eval("MemName")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# this.GetMemCard(Eval("MemCard").ToString())%>
                                        </td>
                                        <td style="text-align: right;">
                                            <%# decimal.Parse(Eval("MicroOrderTotalMoney").ToString()).ToString("0.00")%>
                                        </td>
                                        <td style="text-align: right;">
                                            <%# decimal.Parse(Eval("MicroOrderDiscountMoney").ToString()).ToString("0.00")%>
                                        </td>
                                        <td style="text-align: right;">
                                            <%# decimal.Parse(Eval("MicroOrderPayCoupon").ToString()).ToString("0.00")%>
                                        </td>
                                        <td style="text-align: right;">
                                            <%# Convert.ToInt32(Eval("MicroOrderPoint"))%>
                                        </td>
                                        <td style="text-align: right;">
                                            <%#Eval("MicroOrderCardBalance", "{0:F2}")%>
                                        </td>
                                        <td style="text-align: right;">
                                            <%# DateTime.Parse(Eval("MicroOrderCreateTime").ToString()).ToString()%>
                                        </td>
                                    </tr>
                                    <tr style="display: none;" id="msg<%# Eval("MicroOrderID") %>">
                                        <td colspan="8" style="padding-left: 20px; height: 30px;">
                                            <div>
                                                会员姓名：<b><%# Eval("MicroOrderName")%></b> &nbsp;&nbsp;&nbsp;&nbsp; 联系电话：<b><%# Eval("MicroOrderMobile")%></b>
                                                &nbsp;&nbsp;&nbsp;&nbsp;配送地址：<b>
                                                    <%# Eval("MicroOrderAdress")%></b> &nbsp;&nbsp;&nbsp;&nbsp;会员备注：<b>
                                                        <%# Eval("MicroOrderRemark")%></b>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="display: none;" id="data<%# Eval("MicroOrderID") %>">
                                        <td colspan="10" style="padding-left: 20px;">
                                            <table class="table-style table-hover user_List_txt">
                                                <asp:Repeater ID="rptExpenseDetail" runat="server">
                                                    <HeaderTemplate>
                                                        <thead class="thead">
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
                                                                    商品原价
                                                                </th>
                                                                <th>
                                                                    销售数量
                                                                </th>
                                                                <th>
                                                                    获得积分
                                                                </th>
                                                                <th>
                                                                    折后总价
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="td">
                                                            <td>
                                                                <asp:Label ID="lblDetailNumber" runat="server" Text="1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%# Eval("MicroGoodsCode")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("MicroGoodsName")%>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <%# decimal.Parse(Eval("MicroSalePrice").ToString()).ToString("0.00")%>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <%# int.Parse(Eval("MicroOrderDetailNumber").ToString())%>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <%# Convert.ToInt32(Eval("MicroOrderDetailPoint"))%>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <%# decimal.Parse(Eval("MicroOrderDetailDiscountPrice").ToString()).ToString("0.00")%>
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
