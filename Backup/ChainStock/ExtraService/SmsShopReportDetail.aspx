<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsShopReportDetail.aspx.cs"
    Inherits="ChainStock.ExtraService.SmsShopReportDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
 
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/ExtraService/SmsShopReportDetail.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmSmsShopReportDetail" runat="server">
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
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvSmsShopReportDetail">
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
                                                发送短信时间（年-月）
                                            </th>
                                            <th>
                                                发短信数量
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td>
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td>
                                            <%# Eval("SmsYearMonth","{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("SmsNumber")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <%--                        <div style="padding: 10px; text-align: center;">
                            <a href="javascript:void(0);" onclick="GoBack();" title="后退"><b>返 回</b></a>
                        </div>--%>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
