<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptExpense.aspx.cs" Inherits="ChainStock.Report.RptExpense" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="../Controls/QuickSearch.ascx" tagname="QuickSearch" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/style.css" rel="stylesheet" type="text/css"/>
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            BindNullList("gvRptExpense");
        });
    </script>
</head>
<body>
    <form id="frmRptExpense" runat="server">
    <table width="100%" style="margin: 0 0 0 0; vertical-align: top;" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td style="vertical-align: top" id="tdbox">
                <div class="divContentBox">
                    <div class="divContentHead">
                        <asp:Image ID="imgTitle" runat="server" align="absmiddle" />
                        <asp:Label ID="lblFrmTitle" runat="server" Text="窗体标题" CssClass="lblFrmTitle" ForeColor="#3366FF"></asp:Label>
                    </div>
                    <table class="tableStyle" style="width: 100%">
                        <tr style="color: #333333; background-color: #F7F6F3;">
                            <td style="width: 80px">
                                快速查找：
                            </td>
                            <td style="width: 160px">
                                <input id="txtQueryMem" type="text" runat="server" class="input_txt border_radius"
                                    title="会员卡号/姓名/手机号" />
                            </td>
                            <td style="width: 80px">
                                消费时间：
                            </td>
                            <td style="width: 160px">
                                <input id="txtStartTime" type="text" runat="server" class="Wdate border_radius" />
                            </td>
                            <td style="width: 180px">
                                至
                                <input id="txtEndTime" type="text" runat="server" class="Wdate border_radius" />
                            </td>
                            <td>
                                应收金额：
                            </td>
                            <td align="left" style="width: 210px">
                                <select id="sltTotalMoney" runat="server">
                                    <option selected="selected" value="&gt;=">>=</option>
                                    <option value="&lt;="><=</option>
                                    <option value="=">=</option>
                                </select>
                                <input id="txtTotalMoney" type="text" runat="server" class="input_txt border_radius" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btnRptExpenseQuery" runat="server" Text="查   询" class="buttonColor"
                                    OnClick="btnRptExpenseQuery_Click" Style="margin-bottom: 0px" />
                            </td>
                        </tr>
                        <tr style="color: #333333; background-color: #F7F6F3;">
                            <td style="width: 80px">
                                会员等级：
                            </td>
                            <td style="width: 160px">
                                <select id="sltMemLevelID" runat="server" class="selectWidth">
                                </select>
                            </td>
                            <td style="width: 80px">
                                所属商家：
                            </td>
                            <td>
                                <select id="sltShop" runat="server" class="selectWidth">
                                </select>
                            </td>
                            <td>
                            </td>
                            <td style="width: 80px">
                                实收金额：
                            </td>
                            <td align="left" style="width: 210px">
                                <select id="sltDiscountMoney" runat="server">
                                    <option selected="selected" value="&gt;=">>=</option>
                                    <option value="&lt;="><=</option>
                                    <option value="=">=</option>
                                </select>
                                <input id="txtDiscountMoney" type="text" runat="server" class="input_txt border_radius" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btnRptExpenseExcel" runat="server" Text="导   出" UseSubmitBehavior="false"
                                    class="buttonColor" OnClick="btnRptExpenseExcel_Click" />
                            </td>
                        </tr>
                    </table>
                    <table class="tableStyle" style="width: 100%">
                        <tr class="th">
                            <td>
                                数据统计
                            </td>
                        </tr>
                        <tr>
                            <th align="left">
                                <font color="#00000">应收金额统计：<b>今日： <font color="red">
                                    <asp:Label ID="lblToTalToday" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;<%--昨日：
                        <font color="red">
                            <asp:Label ID="lblToTalYesterday" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;--%>本周：
                                    <font color="red">
                                        <asp:Label ID="lblToTalWeek" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;<%--上周：
                        <font color="red">
                            <asp:Label ID="lblToTalLastWeek" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;--%>本月：
                                    <font color="red">
                                        <asp:Label ID="lblToTalMonth" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;<%--上月：
                        <font color="red">
                            <asp:Label ID="lblToTalLastMonth" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;--%>共有：
                                    <font color="red">
                                        <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label></font>元</b>
                                </font>
                                <%-- <tr class="th"><th align="left">--%>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <font color="#00000">实收金额统计：<b>今日：
                                    <font color="red">
                                        <asp:Label ID="lblDiscountToday" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;<%--昨日：
                        <font color="red">
                            <asp:Label ID="lblDiscountYesterday" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;--%>本周：
                                    <font color="red">
                                        <asp:Label ID="lblDiscountWeek" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;<%--上周：
                        <font color="red">
                            <asp:Label ID="lblDiscountLastWeek" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;--%>本月：
                                    <font color="red">
                                        <asp:Label ID="lblDiscountMonth" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;<%--上月：
                        <font color="red">
                            <asp:Label ID="lblDiscountLastMonth" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;--%>共有：
                                    <font color="red">
                                        <asp:Label ID="lblDiscountTotal" runat="server" Text="0"></asp:Label></font>元</b>
                                </font>
                            </th>
                        </tr>
                        <%--</th></tr>--%>
                    </table>
                    <div>
                        <asp:GridView ID="gvRptExpense" runat="server" AutoGenerateColumns="False" Width="100%"
                            Height="100%" CellPadding="4" ForeColor="#333333" BorderStyle="Double" EnableModelValidation="True"
                            DataKeyNames="OrderID" EmptyDataText="未找到符合此条件的数据！" CssClass="tableStyle" GridLines="None"
                            OnRowDataBound="gvRptExpense_RowDataBound">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="" HeaderText="序号" HeaderStyle-Width="25px">
                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OrderAccount" HeaderText="帐单号">
                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MemCard" HeaderText="会员卡号">
                                    <ItemStyle HorizontalAlign="left" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="MemName" HeaderText="会员姓名">
                                    <ItemStyle HorizontalAlign="left" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OrderTotalMoney" HeaderText="总金额" DataFormatString="{0:F2}">
                                    <ItemStyle HorizontalAlign="right" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OrderDiscountMoney" HeaderText="折扣后金额" DataFormatString="{0:F2}">
                                    <ItemStyle HorizontalAlign="right" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OrderPoint" HeaderText="积分">
                                    <ItemStyle HorizontalAlign="right" Width="30px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OrderRemark" HeaderText="备注">
                                    <ItemStyle HorizontalAlign="left" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ShopName" HeaderText="商家">
                                    <ItemStyle HorizontalAlign="left" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OrderCreateTime" HeaderText="日期" DataFormatString="{0:yyyy-MM-dd}">
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UserName" HeaderText="操作员">
                                    <ItemStyle HorizontalAlign="left" Width="30px" />
                                </asp:BoundField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle Font-Bold="True" CssClass="th" Height="20px" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </div>
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
                                    ShowCustomInfoSection="Left" CustomInfoClass="paginator" CustomInfoSectionWidth="200px"
                                    SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" Direction="LeftToRight"
                                    HorizontalAlign="Right">
                                </webdiyer:AspNetPager>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
