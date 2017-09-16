<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptGoodsExpense.aspx.cs"
    Inherits="ChainStock.Report.RptGoodsExpense" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../Controls/QuickSearch.ascx" TagName="QuickSearch" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/highcharts.js" type="text/javascript"></script>
    <script src="../Scripts/grid.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/RptGoodsExpense.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
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
    <form id="frmGoodsExpense" runat="server">
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
                        <div id="ChartShow" style="width: 800px; display: none;">
                            <div id="ChartSerch">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                    class="tableStyle">
                                    <tr>
                                        <td class="tableStyle_left">
                                            商品编码：
                                        </td>
                                        <td class="tableStyle_right">
                                            <input id="txtgoodcode" type="text" runat="server" class="border_radius radius2"
                                                title="商品编码" />
                                        </td>
                                        <td class="tableStyle_left">
                                            所属商家：
                                        </td>
                                        <td class="tableStyle_right">
                                            <select id="sltShopChart" runat="server" class="selectWidth">
                                            </select>
                                            
  <input  id="HDsltshop" runat="server" type="hidden" />
                                        </td>
                                        <td class="tableStyle_right">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            消费时间：
                                        </td>
                                        <td class="tableStyle_right">
                                            <input id="txtMemStartTime" runat="server" type="text" class="Wdate border_radius" />
                                        </td>
                                        <td class="tableStyle_left">
                                            至&nbsp;&nbsp;
                                        </td>
                                        <td class="tableStyle_right">
                                            <input id="txtMemEndTime" runat="server" type="text" class="Wdate border_radius" />
                                        </td>
                                        <td class="tableStyle_right" colspan="4">
                                            <div class="user_List_Button">
                                                <input id="btSerch" type="button" value="查   询" class="common_Button" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="chart" runat="server">
                                <div id="container" style="width: 800px;">
                                </div>
                            </div>
                        </div>
                        <div id="ReportSerch">
                            <div class="user_List_top">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                    class="tableStyle">
                                    <tr style="color: #333333; background-color: #F7F6F3;">
                                        <td class="user_List_styleLeft">
                                            快捷操作：
                                        </td>
                                        <td class="user_List_styleRight">
                                            <div class="user_List_Button">
                                                <input type="button" id="btshowchart" value="统 计 图" class="common_Button" />
                                                <asp:Button ID="btnGoodsExpenseExcel" runat="server" Text="导   出" class="common_Button"
                                                    OnClick="btnGoodsExpenseExcel_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr>
                                    <td class="tableStyle_left" style="width: 8%">
                                        快速查找：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtGoodsName" type="text" runat="server" class="border_radius radius2"
                                            title="商品编码/名称/简码" />
                                    </td>
                                    <td class="tableStyle_left" style="width: 8%">
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
                                    <td style="border: none">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left" style="width: 8%">
                                        商品类型：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltGoodsType" runat="server" class="selectWidth">
                                            <option selected="selected" value="">===== 请选择 =====</option>
                                            <option value="0">普通商品</option>
                                            <option value="1">服务商品</option>
                                        </select>
                                    </td>
                                    <td class="tableStyle_left" style="width: 8%">
                                        消费商家：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltShop" runat="server" class="selectWidth">
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        商品分类：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltGoodsClass" name="sltGoodsClass" class="selectWidth" />
                                        <input id="txtGoodsClass" runat="server" type="hidden" />
                                    </td>
                                    <td class="tableStyle_right">
                                        <div class="user_List_Button">
                                            <asp:Button ID="Button1" runat="server" Text="查   询" class="common_Button" OnClick="Button1_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="report">
                            <table class="table-style table-hover user_List_txt" id="tbGoods" cellspacing="0"
                                cellpadding="2">
                                <asp:Repeater ID="rptGoods" runat="server">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    序号
                                                </th>
                                                <th style="width: 170px">
                                                    商品编码
                                                </th>
                                                <th>
                                                    商品名称
                                                </th>
                                                <th>
                                                    所属分类
                                                </th>
                                                <th>
                                                    商品类型
                                                </th>
                                                <th>
                                                    商品单价
                                                </th>
                                                <th>
                                                    销售数量
                                                </th>
                                                <th>
                                                    销售总金额
                                                </th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="td">
                                            <td style="width: 50px;">
                                                <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 170px">
                                                <img id="img<%# Eval("GoodsID") %>" alt="" src="../Inc/Style/images/plus.gif" style="vertical-align: text-bottom"
                                                    onload="javascript:IsShow('<%# Eval("GoodsID") %>','4')" />
                                                <a id="a<%# Eval("GoodsID") %>" href="javascript:GetGoodsExpense('<%# Eval("GoodsID") %>','<%# Eval("Name") %>')"
                                                    title="展开/收起详情">
                                                    <%# Eval("GoodsCode")%></a>
                                            </td>
                                            <td style="text-align: left">
                                                <%#Eval("Name")%>
                                            </td>
                                            <td>
                                                <%#Eval("ClassName")%>
                                            </td>
                                            <td>
                                                <%# this.GetGoodsType(int.Parse(Eval("GoodsType").ToString()),float.Parse(Eval("totalNumber").ToString()))%>
                                            </td>
                                            <td style="text-align: right">
                                                <%#Eval("Price")%>
                                            </td>
                                            <td style="text-align: right">
                                                <%# int.Parse(Eval("totalNumber").ToString())%>
                                            </td>
                                            <td style="text-align: right; color: Red">
                                                <%# decimal.Parse(Eval("totalMoney").ToString()).ToString("0.00")%>
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
                </div>
            </td>
        </tr>
    </table>
    <div id="dvDetail" style="display: none;">
        <table class="table-style table-hover user_List_txt" id="tbRptGoodsExpense" cellspacing="0"
            cellpadding="2">
            <tr>
                <td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;"
                    colspan="6" type="LoadingBar">
                    <script type="text/javascript">
                        ListLoading();
                    </script>
                </td>
            </tr>
        </table>
        <div id="RptGoodsExpensePage" style="margin: 0px; border: solid 1px #ccc; height: 30px;
            text-align: right;">
            <div class="listTablePage_simple">
            </div>
        </div>
    </div>
    <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
