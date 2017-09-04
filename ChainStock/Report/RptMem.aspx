<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptMem.aspx.cs" Inherits="ChainStock.Report.RptMem" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="../Controls/QuickSearch.ascx" tagname="QuickSearch" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/highcharts.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/RptMem.js" type="text/javascript"></script>


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
    <form id="frmRptMem" runat="server">
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
                        <div id="ChartShow" style="display: none; width: 800px;">
                            <div id="ChartSerch">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                    class="tableStyle">
                                    <tr>
                                        <td class="tableStyle_left">
                                            时间段：
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
                                            <input  id="HDsltshop" runat="server" type="hidden" />
                                         </td>
                                        <td class="tableStyle_right">
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
                                                <input type="button" value="统 计 图" id="btshowchart" class="common_Button" />
                                                <asp:Button ID="btnRptMemExcel" runat="server" Text="导   出" class="common_Button"
                                                    OnClick="btnRptMemExcel_Click" />
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
                                        <select id="sltShop" runat="server" class="selectWidth" autocomplete="off">
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        会员将近：
                                    </td>
                                    <td class="tableStyle_right" colspan="4">
                                        <input id="txtDay" type="text" runat="server" class="border_radius radius2" style="width: 100px" />
                                        <label style="line-height: 30px; height: 30px;">
                                            &nbsp;天没来消费</label>
                                    </td>
                                    <td class="tableStyle_right">
                                        <div class="user_List_Button">
                                            <asp:Button ID="btnRptMemQuery" runat="server" Text="查   询" class="common_Button"
                                                OnClick="btnRptMemQuery_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="tableStyle" id="tbtongji" style="width: 100%;">
                            <tr class="th">
                                <td align="left" style="font-size:3;font-weight:bold">
                                   会员信息统计：会员总数量： <font color="red">
                                        <asp:Label ID="lblMemCount" runat="server" Text="0" ></asp:Label></font>人
                                        会员余额汇总： <font color="red">
                                        <asp:Label ID="lblMemMoney" runat="server" Text="0" ></asp:Label></font>元
                                        会员积分汇总： <font color="red">
                                        <asp:Label ID="lblMemPoint" runat="server" Text="0" ></asp:Label></font>

                                        
                                </td>
                            </tr>
                        </table>
                        <div id="report">
                            <table class="table-style table-hover user_List_txt" cellspacing="0" cellpadding="2">
                                <asp:Repeater ID="gvRptMem" runat="server">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    序号
                                                </th>
                                                <th>
                                                    会员卡号
                                                </th>
                                                <th>
                                                    会员姓名
                                                </th>
                                                <th>
                                                    手机号码
                                                </th>
                                                <th>
                                                    账户余额
                                                </th>
                                                <th>
                                                    账户积分
                                                </th>
                                                <th>
                                                    会员等级
                                                </th>
                                                <th>
                                                    累计消费
                                                </th>
                                                <th>
                                                    上次消费日期
                                                </th>
                                                <th>
                                                    商家
                                                </th>
                                                <th>
                                                    办卡日期
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
                                            <td>
                                                <%# Eval("MemCard")%>
                                            </td>
                                            <td style="text-align: left">
                                                <%# Eval("MemName")%>
                                            </td>
                                            <td>
                                                <%# Eval("MemMobile")%>
                                            </td>
                                            <td style="text-align: right">
                                                <%# Eval("MemMoney","{0:F2}")%>
                                            </td>
                                            <td style="text-align: right">
                                                <%# Eval("MemPoint")%>
                                            </td>
                                            <td>
                                                <%# Eval("LevelName")%>
                                            </td>
                                            <td>
                                                <%# Eval("MemConsumeMoney", "{0:F2}")%>
                                            </td>
                                            <td>
                                                <%# Eval("MemConsumeLastTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                            </td>
                                            <td>
                                                <%# Eval("ShopName")%>
                                            </td>
                                            <td>
                                                <%# Eval("MemCreateTime", "{0:yyyy-MM-dd}")%>
                                            </td>
                                            <td>
                                                <%# Eval("UserName")%>
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
                </div>
            </td>
        </tr>
    </table>
    <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
