<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysNoticeShow.aspx.cs"
    Inherits="ChainStock.SystemManage.SysNoticeShow" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmNoticeShow" runat="server">
    <div class="divContentBox">
        <div style="width: 100%; text-align: center;">
            <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 600px; margin: auto">
                <tr>
                    <td colspan="4">
                        <span id="spNoticeTitle" runat="server" style="font-weight: bold;"></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        发布人：&nbsp;&nbsp; <span id="spRelaseName" runat="server" style="font-weight: bold;">
                        </span>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;发布时间：&nbsp;&nbsp; <span id="spRelaseTime"
                            runat="server" style="font-weight: bold;"></span>&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="left" valign="top" style="height:300px;">
                        <font size="4"><asp:Literal ID="ltNoticeDetail" runat="server"></asp:Literal></font>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
