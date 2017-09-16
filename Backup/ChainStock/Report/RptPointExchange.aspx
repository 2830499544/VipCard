<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptPointExchange.aspx.cs"
    Inherits="ChainStock.Report.RptPointExchange" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>


<%@ Register src="../Controls/QuickSearch.ascx" tagname="QuickSearch" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script src="../Scripts/Module/Report/RptPointExchange.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>


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
    <form id="frmRptPointExchange" runat="server">
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
                        <div id="ChartShow" style="width: 800px; display: none;">
                            <div id="ChartSerch">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                    class="tableStyle">
                                    <tr>
                                        <td class="tableStyle_left">
                                            兑换时间：
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
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            所属商家：
                                        </td>
                                        <td class="tableStyle_right" colspan="2">
                                            <select id="sltShopChart" runat="server" class="selectWidth">
                                            </select>
                                        </td>
                                        <td td class="tableStyle_right">
                                            <div class="user_List_Button">
                                                <input id="btSerch" type="button" value="查   询" class="common_Button" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="chart">
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
                                                <asp:Button ID="btnRptPointExchangeExcel" runat="server" Text="导   出" class="common_Button"
                                                    OnClick="btnRptPointExchangeExcel_Click" />
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
                                        兑换平台：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="selectExchangeType" runat="server" style="outline: none; resize: none;">
                                            <option value="0" selected="selected">所有平台</option>
                                            <option value="1">主系统兑换</option>
                                            <option value="2">自助平台兑换</option>
                                            <option value="3">微信平台兑换</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        名称/简码：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtGift" type="text" title="礼品名称/简码" runat="server" class="border_radius radius2" />
                                    </td>
                                    <td class="tableStyle_left">
                                        兑换时间：
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
                                    <td class="tableStyle_right">
                                        <div class="user_List_Button">
                                            <asp:Button ID="btnRptPointExchangeQuery" runat="server" Text="查   询" class="common_Button"
                                                OnClick="btnRptPointExchangeQuery_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="tableStyle" id="tbtongji" style="width: 100%;">
                            <tr>
                                <th align="left">
                                    &nbsp;&nbsp;&nbsp;&nbsp; <font color="#00000">兑换礼品统计：<b>兑换总数量： <font color="red">
                                        <asp:Label ID="lblExchangeNumber" runat="server" Text="0"></asp:Label></font>个,&nbsp;&nbsp;消费总积分：
                                        <font color="red">
                                            <asp:Label ID="lblExchangePoint" runat="server" Text="0"></asp:Label></font>分
                                    </font>
                                </th>
                            </tr>
                        </table>
                        <div id="report">
                            <table class="table-style table-hover user_List_txt" id="tbGoods" cellspacing="0"
                                cellpadding="2">
                                <asp:Repeater ID="r_GiftExChange" runat="server" OnItemDataBound="r_GiftExChange_ItemDataBound">
                                    <HeaderTemplate>
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                兑换单号
                                            </th>
                                            <th>
                                                会员卡号
                                            </th>
                                            <th>
                                                会员姓名
                                            </th>
                                            <th>
                                                兑换数量
                                            </th>
                                            <th>
                                                消费积分
                                            </th>
                                            <th>
                                                兑换类型
                                            </th>
                                            <th>
                                                申请时间
                                            </th>
                                            <th>
                                                审核时间
                                            </th>
                                            <th>
                                                兑换商家
                                            </th>
                                            <th>
                                                审核员
                                            </th>
                                            <th>
                                                操作
                                            </th>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="td">
                                            <td>
                                                <asp:Label runat="server" ID="lblNumber"></asp:Label>
                                            </td>
                                            <td style="text-align: left; width: 200px">
                                                <img id="img<%# Eval("ExchangeID") %>" alt="" src="../Inc/Style/images/plus.gif"
                                                    style="vertical-align: text-bottom" onload="javascript:IsShow('<%# Eval("ExchangeID") %>','<%# Eval("ExchangeStatus")%>')" />
                                                <a id="a<%# Eval("ExchangeID") %>" href="javascript:ShowDetail('<%# Eval("ExchangeID") %>','<%# Eval("ExchangeStatus")%>')"
                                                    title="展开/收起详情">
                                                    <%# Eval("ExchangeAccount")%></a>
                                            </td>
                                            <td style="text-align: left;">
                                                <%#Eval("MemCard")%>
                                            </td>
                                            <td style="text-align: left;">
                                                <%#Eval("MemName")%>
                                            </td>
                                            <td style="text-align: right;">
                                                <%#Eval("ExchangeAllNumber")%>
                                            </td>
                                            <td style="text-align: right;">
                                                <%#Eval("ExchangeAllPoint")%>
                                            </td>
                                            <td style="text-align: left;">
                                                <%#Convert.ToInt32(Eval("ExchangeType")) == 1 ? "主系统兑换" : Convert.ToInt32(Eval("ExchangeType")) == 2 ? "自助平台兑换" : "微信平台兑换"%>
                                            </td>
                                            <td style="text-align: center;">
                                                <%#Eval("ApplicationTime")%>
                                            </td>
                                            <td style="text-align: center;">
                                                <%#Eval("ExchangeTime")%>
                                            </td>
                                            <td style="text-align: left;">
                                                <%#Eval("ShopName")%>
                                            </td>
                                            <td style="text-align: left;">
                                                <%#Eval("UserName")%>
                                            </td>
                                            <td class="listtd" style="width: 40px;">
                                                <a id="againPrint" runat="server" href='<%#"javascript:againPrint("+Eval("ExchangeID").ToString()+")" %>'>
                                                    <img src="../images/Gift/print.png" title="打印小票" alt="打印小票" /></a>
                                            </td>
                                        </tr>
                                        <tr style="display: none;" id="data<%# Eval("ExchangeID") %>">
                                            <td colspan="12" style="">
                                                <table class="table-style table-hover user_List_txt" cellspacing="0" cellpadding="2"
                                                    style="width: 80%; margin-left: 50px;">
                                                    <asp:Repeater ID="Repeater1" runat="server">
                                                        <HeaderTemplate>
                                                            <thead class="thead">
                                                                <tr class="th">
                                                                    <th>
                                                                        序号
                                                                    </th>
                                                                    <th>
                                                                        名称
                                                                    </th>
                                                                    <th>
                                                                        兑换数量
                                                                    </th>
                                                                    <th>
                                                                        积分
                                                                    </th>
                                                                    <th>
                                                                        积分小计
                                                                    </th>
                                                                </tr>
                                                            </thead>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr class="td">
                                                                <td>
                                                                    <asp:Label ID="lblNum" runat="server" Text="1"></asp:Label></label>
                                                                </td>
                                                                <td style="text-align: left;">
                                                                    <%#Eval("GiftName")%>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <%#Eval("ExchangeNumber")%>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <%#Convert.ToInt32(Eval("ExchangePoint").ToString()) / Convert.ToInt32(Eval("ExchangeNumber").ToString())%>
                                                                </td>
                                                                <td style="text-align: right;">
                                                                    <%#Eval("ExchangePoint")%>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </table>
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
    <asp:Label ID="lblPrintTitle" Style="display: none" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lblPrintFoot" Style="display: none" runat="server" Text="Labe2"></asp:Label>
      <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
