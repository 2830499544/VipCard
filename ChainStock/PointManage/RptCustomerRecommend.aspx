<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptCustomerRecommend.aspx.cs"
    Inherits="ChainStock.PointManage.RptCustomerRecommend" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td align="left" style="width: 280px; vertical-align:top">
                <asp:TreeView ID="TreeView1" runat="server" CssClass="mytree" ShowLines="true" Width="20"
                    ForeColor="#4f4e4e" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
                </asp:TreeView>
            </td>
            <td valign="top">
                <table class="table-style table-hover user_List_txt" cellspacing="0" cellpadding="2"
                    style="width: 100%;">
                    <tr class="th">
                        <td>
                            会员层级:<asp:Label ID="lblMemCard" runat="server" Text=""></asp:Label>
                            会员姓名:<asp:Label ID="lblMemName" runat="server" Text=""></asp:Label>
                            <span style="margin-left:5px">队列内提成积分:</span><asp:Label ID="lblListin" runat="server" Text="0" ForeColor="Red"></asp:Label>
                            <span style="margin-left:5px">队列外提成积分:</span><asp:Label ID="lblListout" runat="server" Text="0" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table class="table-style table-hover user_List_txt" cellspacing="0" cellpadding="2"
                    style="width: 100%;">
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
    </table>
    <asp:HiddenField ID="hidLevel" runat="server" />
    </form>
</body>
</html>
