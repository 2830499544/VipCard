<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopAuthority.aspx.cs"
    Inherits="ChainStock.SystemManage.ShopAuthority" %>

<%--<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    
    <script src="../Scripts/Module/SystemManage/ShopAuthority.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
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
                        <div class="user_List_top" style="height: 34px;">
                            <table class="tableStyle" style="width: 100%">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <input id="btnShopAuthority" type="button" value="保   存" class="common_Button" />&nbsp;
                                            <input id="btnShopReset" type="button" value="取   消" class="common_Button" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt" id="tbChick">
                            <asp:Repeater runat="server" ID="gvShopAuthorityList">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                <input type="checkbox" id="chkAll" class="chkAll" onclick="SelectAll()" />
                                            </th>
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                商家名称
                                            </th>
                                            <th>
                                                联系人
                                            </th>
                                            <th>
                                                联系电话
                                            </th>
                                            <th>
                                                详细地址
                                            </th>
                                            <th>
                                                备注
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                          <input type="checkbox" class="chk" value='<%# Eval("ShopID") %>' id="chkitem" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td>
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td>
                                            <%# Eval("ShopContactMan")%>
                                        </td>
                                        <td>
                                            <%# Eval("ShopTelephone")%>
                                        </td>
                                        <td style="text-align: left; padding-left: 4px;">
                                            <%# Eval("ShopAddress")%>
                                        </td>
                                        <td>
                                            <%# Eval("ShopRemark")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="ShopID" runat="server" />
    </form>
</body>
</html>
