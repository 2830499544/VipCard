<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptPointRate.aspx.cs" Inherits="ChainStock.Report.RptPointRate" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../Controls/QuickSearch.ascx" TagName="QuickSearch" TagPrefix="uc1" %>
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
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/Module/PointManage/RptPointRate.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script type="text/javascript">

        function ShowRecommend(MemCard, MemName) {
            var height = $(window).height();
            var width = $(window).width();
            art.dialog.open("PointManage/RptCustomerRecommend.aspx?PID=143&MemCard=" + MemCard + "&level=" + $('#hidLevel').val(), { title: "卡号【" + MemCard + "】姓名【" + MemName + "】" + $("#hidLevel").val() + "级推荐提成队列", lock: true, width: "100%", height: "100%" });
        }      

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidLevel" runat="server" />
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
                                            <asp:Button ID="btnRptPointRateExcel" runat="server" Text="导   出" UseSubmitBehavior="false"
                                                class="common_Button" OnClick="btnRptPointRateExcel_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="tableStyle" style="width: 100%">
                            <tr>
                                <td class="tableStyle_left">
                                    会员信息：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtQueryMem" type="text" runat="server" class="border_radius" title="会员卡号/卡面号码/姓名/手机号" />
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
                                <td>
                                </td>
                                <td class="tableStyle_right" colspan="2">
                                    <asp:Button ID="btnRptPointRateQuery" runat="server" Text="查   询" class="common_Button"
                                        OnClick="btnRptPointRateQuery_Click" />
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellspacing="0" cellpadding="0" width="100%" class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    积分提成统计：
                                </td>
                                <td class="tableStyle_right">
                                    今日： <font color="red">
                                        <asp:Literal ID="lblToday" runat="server" Text="0"></asp:Literal></font>分,&nbsp;&nbsp;本周：
                                    <font color="red">
                                        <asp:Literal ID="lblWeek" runat="server" Text="0"></asp:Literal></font>分,&nbsp;&nbsp;本月：
                                    <font color="red">
                                        <asp:Literal ID="lblMonth" runat="server" Text="0"></asp:Literal></font>分,&nbsp;&nbsp;共有：
                                    <font color="red">
                                        <asp:Literal ID="lblTotal" runat="server" Text="0"></asp:Literal></font>分
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater ID="rptRptPointRate" runat="server" OnItemDataBound="rptRptPointRate_ItemDataBound">
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
                                                会员等级
                                            </th>
                                            <th>
                                                会员积分
                                            </th>
                                            <th>
                                                提成积分
                                            </th>
                                            <th>
                                                商家
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td style="text-align: right">
                                            <img id="img<%# Eval("MEMID") %>" alt="" src="../Inc/Style/images/plus.gif" style="vertical-align: text-bottom"
                                                onload="javascript:IsShowPic('<%# Eval("MEMID") %>','<%# Eval("MemRecommend") %>')" />
                                            <a id="a<%# Eval("MEMID") %>" href="javascript:ShowPointDetail('<%# Eval("MEMID") %>','<%# Eval("MemRecommend") %>')"
                                                title="展开/收起详情">
                                                <%# Eval("MemCard")%></a>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("MemName")!=""?Eval("MemName"):"无"%>
                                        </td>
                                        <td>
                                            <%# Eval("LEVELNAME")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("MemPoint")%>
                                        </td>
                                        <td style="color: Red; text-align: right">
                                            <%# Eval("RatePointCount")%>&nbsp;&nbsp;<a href="javascript:ShowRecommend('<%# Eval("MEMCARD") %>','<%# Eval("MEMNAME") %>')">查看下级<a />
                                        </td>
                                        <td>
                                            <%# Eval("ShopName")%>
                                        </td>
                                    </tr>
                                    <tr style="display: none" id="data<%# Eval("MEMID") %>">
                                        <td colspan="9" style="padding-left: 50px;">
                                            <table class="table-style table-hover user_List_txt" cellspacing="0" cellpadding="2"
                                                style="width: 80%;">
                                                <asp:Repeater ID="rptPointDetails" runat="server">
                                                    <HeaderTemplate>
                                                        <thead class="thead">
                                                            <tr class="th">
                                                                <th>
                                                                    序号
                                                                </th>
                                                                <th>
                                                                    会员名称
                                                                </th>
                                                                <th>
                                                                    会员卡号
                                                                </th>
                                                                <th>
                                                                    提成积分
                                                                </th>
                                                                <th>
                                                                    详情
                                                                </th>
                                                                <th>
                                                                    订单编号
                                                                </th>
                                                                <th>
                                                                    时间
                                                                </th>
                                                            </tr>
                                                        </thead>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="td">
                                                            <td>
                                                                <asp:Label ID="lblDetails" runat="server" Text="1"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <%# Eval("MEMNAME") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("MEMCARD") %>
                                                            </td>
                                                            <td style="text-align: right">
                                                                <%# Eval("POINTNUMBER") %>
                                                            </td>
                                                            <td style="text-align: left">
                                                                <%# Eval("POINTREMARK") %>
                                                            </td>
                                                            <td>
                                                                <%# Eval("PointOrderCode")%>
                                                            </td>
                                                            <td>
                                                                <%# Eval("POINTCREATETIME")%>
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
    <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
