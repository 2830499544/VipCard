<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptMemCount.aspx.cs" Inherits="ChainStock.Report.RptMemCount" %>

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
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/RptMemCount.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.onkeydown = function (event) {
                e = event ? event : (window.event ? window.event : null);
                if (e.keyCode == 13) {
                    return false;
                }
            };
        });
    </script>
</head>
<body>
    <form id="frmRptMemCount" runat="server">
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
                                            <asp:Button ID="btnMemCountExcel" runat="server" OnClientClick="return btQuery();"
                                                OnClick="btnMemCountExcel_Click" Text="导   出" class="common_Button" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    快速查找：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtQueryMem" type="text" runat="server" class="border_radius radius2"
                                        title="会员卡号/卡面号码/姓名/手机号" />
                                </td>
                                <td class="tableStyle_left">
                                    充次单号：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtCountAccount" type="text" runat="server" class="border_radius radius2"
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
                                    所属商家：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltShop" runat="server" class="selectWidth">
                                    </select>
                                      <input  id="HDsltshop" runat="server" type="hidden" />
                                </td>
                                <td class="tableStyle_left">
                                    充次金额：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltMoney" runat="server" style="height: 25px; outline: none; resize: none;">
                                        <option selected="selected" value="&gt;=">>=</option>
                                        <option value="&lt;="><=</option>
                                        <option value="=">=</option>
                                    </select>
                                    <input id="txtMoney" type="text" runat="server" class="border_radius radius2" style="width: 60px;
                                        float: none; clear: both;" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    充值时间：
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
                                <td style=" border:none">
                                </td>
                                <td class="tableStyle_right" style=" border:none">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnRptMemCountQuery" OnClientClick="return btQuery();" runat="server"
                                            Text="查   询" class="common_Button" OnClick="btnRptMemCountQuery_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="tableStyle" id="tbtongji" style="width: 100%;">
                            <tr>
                                <th align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp; <font color="#00000">会员充次统计：<b>充次总金额： <font color="red">
                                        <asp:Label ID="lblMoney" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;折后总金额：
                                        <font color="red">
                                            <asp:Label ID="lblDiscountMoney" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;充次总积分：
                                        <font color="red">
                                            <asp:Label ID="lblTotalPoint" runat="server" Text="0"></asp:Label></font>元,&nbsp;&nbsp;充次总次数：
                                        <font color="red">
                                            <asp:Label ID="lblToalCount" runat="server" Text="0"></asp:Label></font>次
                                    </font>,&nbsp;&nbsp;剩余总次数： <font color="red">
                                        <asp:Label ID="lblRemainCount" runat="server" Text="0"></asp:Label></font>次
                                    </font>
                                </th>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater ID="rptMemCountList" runat="server" OnItemDataBound="rptMemCountList_ItemDataBound">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                充次单号
                                            </th>
                                            <th>
                                                会员卡号
                                            </th>
                                            <th>
                                                会员姓名
                                            </th>
                                            <th>
                                                充次金额
                                            </th>
                                            <th>
                                                折后金额
                                            </th>
                                            <th>
                                                所得积分
                                            </th>
                                            <th>
                                                充次备注
                                            </th>
                                            <th>
                                                充次商家
                                            </th>
                                            <th>
                                                充次时间
                                            </th>
                                            <th>
                                                操作员
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td style="width: 50px;">
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <img id="img<%# Eval("CountID") %>" alt="" src="../Inc/Style/images/plus.gif"
                                                style="vertical-align: text-bottom" onload="javascript:IsShow('<%# Eval("CountID") %>','3')" />
                                            <a href="javascript:ShowDetail('<%# Eval("CountID") %>','3')" title="展开/收起详情">
                                                <%# Eval("CountAccount")%></a>
                                        </td>
                                        <td>
                                            <%# Eval("MemCard")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("MemName")%>
                                        </td>
                                        <td style="text-align: right; color: Red">
                                            <%# double.Parse(Eval("CountTotalMoney").ToString()).ToString("0.00")%>
                                        </td>
                                        <td style="text-align: right; color: Red">
                                            <%# double.Parse(Eval("CountDiscountMoney").ToString()).ToString("0.00")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("CountPoint")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%#Eval("CountRemark")%>
                                        </td>
                                        <td>
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td>
                                            <%# DateTime.Parse(Eval("CountCreateTime").ToString()).ToString("yyyy-MM-dd HH:mm:ss")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                    </tr>
                                    <tr style="display: none;" id="data<%# Eval("CountID") %>">
                                        <td colspan="10">
                                            <asp:Repeater ID="rptMemCountDetailList" runat="server">
                                                <HeaderTemplate>
                                                    <div style="padding-left: 55px;">
                                                        <table class="table-style table-hover user_List_txt" cellspacing="0" cellpadding="2"
                                                            style="width: 50%;">
                                                            <thead class="thead">
                                                                <tr class="th">
                                                                    <th>
                                                                        序号
                                                                    </th>
                                                                    <th>
                                                                        服务项目
                                                                    </th>
                                                                    <th>
                                                                        次数
                                                                    </th>
                                                                    <th>
                                                                        实付金额
                                                                    </th>
                                                                    <th>
                                                                        所得积分
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
                                                            <%# Eval("Name")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("CountDetailTotalNumber")%>
                                                            <td>
                                                                <%#  double.Parse(Eval("CountDetailDiscountMoney").ToString()).ToString("0.00")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("CountDetailPoint")%>
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
    <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
