<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupInfo.aspx.cs" Inherits="ChainStock.SystemManage.GroupInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmGroup" runat="server">
    <div class="divContentBox">
        <div class="divContentHead">
            <asp:Image ID="imgTitle" runat="server" align="absmiddle" />
            <asp:Label ID="lblFrmTitle" runat="server" Text="窗体标题" CssClass="lblFrmTitle" ForeColor="#3366FF"></asp:Label>
        </div>
        <div id="divErrorMsg" class="divErrorMsg">
        </div>
        <table class="tableBox" cellspacing="0" cellpadding="2">
            <tr>
                <td class="tableAlignRight">
                    角色名称：
                </td>
                <td class="tableAlignLeft">
                    <input id="txtGroupName" type="text" runat="server" class="input_txt border_radius" />
                    <input id="txtGroupID" type="hidden" runat="server" />
                </td>
                <td class="tableAlignRight">
                    说 明：
                </td>
                <td class="tableAlignLeft">
                    <input id="txtGroupRemark" type="text" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnGroupSave" runat="server" class="buttonColor" Text="保   存" OnClick="btnGroupSave_Click" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HidGid" runat="server" />
    <asp:HiddenField ID="HidAcction" runat="server" />
    </form>
</body>
</html>
