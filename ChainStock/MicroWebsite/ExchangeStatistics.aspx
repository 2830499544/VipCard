<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExchangeStatistics.aspx.cs"
    Inherits="ChainStock.MicroWebsite.ExchangeStatistics" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //BindNullList("rptRptTablePointRate");
        })

        function IsShow(id, arg2) {
            if (arg2 > 0) {
                $("#img" + id).css("display", "");
            }
            else {
                $("#img" + id).css("display", "none");
                $("#a" + id).css("padding-left", "22px");
            }
        }

        function ShowDetail(id, arg2) {
            if (arg2 > 0) {
                if ($("#data" + id).css("display") == "none") {
                    $("#data" + id).css("display", "");
                    $("#detail" + id).css("display", "");
                    $("#img" + id).attr("src", "../Inc/Style/images/minus.gif")
                }
                else {
                    $("#data" + id).css("display", "none");
                    $("#detail" + id).css("display", "none");
                    $("#img" + id).attr("src", "../Inc/Style/images/plus.gif")
                }
            }
            else {
                $("#data" + id).css("display", "none");
                $("#detail" + id).css("display", "none");
            }
        }
    </script>
</head>
<body>
    <form id="frmExchangeStatistics" runat="server">
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
                        <div id="ReportSerch">
                            <%--<div class="user_List_top">
                                    <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                        class="tableStyle">
                                        <tr style="color: #333333; background-color: #F7F6F3;">
                                            <td class="user_List_styleLeft">
                                                快捷操作：
                                            </td>
                                            <td class="user_List_styleRight">
                                                <div class="user_List_Button">
                                                    
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>--%>
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
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        名称/简码：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtGift" type="text" title="礼品名称/简码" runat="server" class="border_radius radius2" />
                                    </td>
                                    <td class="tableStyle_left">
                                        会员等级：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltMemLevelID" runat="server" class="selectWidth">
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
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
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater ID="r_GiftExChange" runat="server" OnItemDataBound="r_GiftExChange_ItemDataBound">
                                <HeaderTemplate>
                                    <thead class="thead">
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
                                        </tr>
                                    </thead>
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
                                    </tr>
                                    <tr style="display: none;" id="data<%# Eval("ExchangeID") %>">
                                        <td colspan="10" style="padding-left: 20px;">
                                            <table class="table-style table-hover user_List_txt">
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
