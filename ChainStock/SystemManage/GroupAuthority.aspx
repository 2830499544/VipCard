<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupAuthority.aspx.cs"
    Inherits="ChainStock.SystemManage.GroupAuthority" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/GroupAuthority.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmGroupAuthority" runat="server">
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
                        <div>
                            <table style="width: 100%; height: 100%;" border="1" cellpadding="0" cellspacing="0"
                                bordercolor="#434343" class="tableStyle">
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>角色名称：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtGroupName" type="text" runat="server" class="border_radius" style="line-height: " />
                                        <label for="lbltesttest" style="vertical-align: text-bottom;">
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <label id="lblIsPublic" runat="server" class="lbsetCk" style="vertical-align: middle; display:none;">
                                                <input id="chkIsPublic" type="checkbox" runat="server" checked="true" />所有商家可用</label>
                                        </label>
                                        <input id="txtGroupID" type="hidden" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>继承于：
                                    </td>
                                    <td class="tableStyle_right">
                                      <input type="text" id="txtGroupType" style="width: 33px; display: none" runat="server" />
                                        <select id="sltParentGroup" type="input" runat="server" class="selectWidth" onchange="javascript:changeParentGroupp()" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>说&nbsp;&nbsp;&nbsp;&nbsp;明：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtGroupRemark" type="text" runat="server" class="border_radius" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt" id="tbChick">
                            <asp:Repeater runat="server" ID="gdGroupPermission" OnItemDataBound="gdGroupPermission_ItemDataBound">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                菜单名称
                                            </th>
                                            <th>
                                                菜单权限
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                            <%# (Container.ItemIndex+1).ToString() %>
                                        </td>
                                        <td style="text-align: left; padding-left: 4px;">
                                            <%# Eval("ModuleCaption")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="lblMenuID" runat="server" Text='<%# Bind("ModuleID") %>' Visible="false"></asp:Label>
                                            <asp:CheckBoxList ID="ChkListPerm" runat="server">
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div style="text-align: center; padding-top: 8px">
                            <label style="vertical-align: text-bottom;">
                                <input id="chkAll" type="checkbox" />
                                <label style="vertical-align: middle;">
                                    全选 &nbsp;&nbsp;
                                </label>
                            </label>
                            <label style="vertical-align: text-bottom;">
                                <input id="Invert" type="checkbox" />
                                <label style="vertical-align: middle;">
                                    反选 &nbsp;&nbsp;
                                </label>
                            </label>
                            <asp:Button ID="btnAuthority" runat="server" OnClick="btnAuthority_Click" Text="保   存"
                                class="buttonColor" />
                            <asp:Button ID="btnAuthorityCancel" runat="server" Text="取   消" OnClick="btnAuthorityCancel_Click"
                                class="buttonRest" />
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HidGid" runat="server" />
    <asp:HiddenField ID="HidAcction" runat="server" />
    </form>
</body>
</html>
