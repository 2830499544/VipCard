<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptShop.aspx.cs" Inherits="ChainStock.Report.RptShop" %>

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
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/RptShop.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //绑定日期控件
            $('#txtStartTime').bind("focus click", function () {
                WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });

            });
            //绑定日期控件
            $('#txtEndTime').bind("focus click", function () {
                WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });

            });
        })      
    </script>
</head>
<body>
    <form id="frmRptShop" runat="server">
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
                                            <asp:Button ID="btnShopExcel" runat="server" Text="导   出" UseSubmitBehavior="false"
                                                class="common_Button" OnClick="btnShopExcel_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    所属商家：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltShop" runat="server" class="selectWidth">
                                    </select>
                                    <input id="HDsltshop" runat="server" type="hidden" />
                                </td>
                                <td class="tableStyle_left">
                                    时间查询：
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
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnShopSearch" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnShopSearch_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvRptShop" OnItemDataBound="gvRptShop_ItemDataBound">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                商家名称
                                            </th>
                                            <th>
                                                消费应收总金额(折前)
                                            </th>
                                            <th>
                                                消费实收总金额(折后)
                                            </th>
                                            <th>
                                                初始充值总金额
                                            </th>
                                            <th>
                                                常规充值总金额
                                            </th>
                                            <th>
                                                消费得积分
                                            </th>
                                            <th>
                                                消费使用积分
                                            </th>
                                            <th>
                                                变动总积分
                                            </th>
                                            <th>
                                                会员总数量
                                            </th>
                                            <th>
                                                会员账户总金额
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
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("sumOrderTotalMoney", "{0:F2}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("sumOrderDiscountMoney", "{0:F2}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("sumSRechargeMoney", "{0:F2}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("sumFRechargeMoney", "{0:F2}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("sumOrderPoint")%>
                                        </td>
                                         <td style="text-align: right">
                                            <%# Eval("sumUsePoint")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("sumPointNumber")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("MemCount")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("MemMoney", "{0:F2}")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <tr class="td">
                                        <td style="width: 50px;">
                                        </td>
                                        <td style="color: Red">
                                            商家汇总
                                        </td>
                                        <td style="text-align: right; color: Red">
                                            <asp:Label ID="lblSumOrderTotalMoney" runat="server" Text="0.00"></asp:Label>
                                        </td>
                                        <td style="text-align: right; color: Red">
                                            <asp:Label ID="lblSumOrderDiscountMoney" runat="server" Text="0.00"></asp:Label>
                                        </td>
                                        <td style="text-align: right; color: Red">
                                            <asp:Label ID="lblSumSRechargeMoney" runat="server" Text="0.00"></asp:Label>
                                        </td>
                                        <td style="text-align: right; color: Red">
                                            <asp:Label ID="lblSumFRechargeMoney" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td style="text-align: right; color: Red">
                                            <asp:Label ID="lblSumOrderPoint" runat="server" Text="0"></asp:Label>
                                        </td>
                                         <td style="text-align: right; color: Red">
                                            <asp:Label ID="lblSumUsePoint" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td style="text-align: right; color: Red">
                                            <asp:Label ID="lblSumPointNumber" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td style="text-align: right; color: Red">
                                            <asp:Label ID="lblMemCount" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td style="text-align: right; color: Red">
                                            <asp:Label ID="lblMemMoney" runat="server" Text="0.00"></asp:Label>
                                        </td>
                                    </tr>
                                </FooterTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
