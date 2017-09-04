<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="ChainStock.SystemManage.UserInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/aero.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/UserInfo.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmUserInfo" runat="server">
    <table width="100%" style="margin: 0 0 0 0; vertical-align: top;" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td style="vertical-align: top" id="tdbox">
                <div class="divContentBox" style="width: 600px;">
                    <div class="divContentHead">
                        <asp:Image ID="imgTitle" runat="server" align="absmiddle" />
                        <asp:Label ID="lblFrmTitle" runat="server" Text="窗体标题" CssClass="lblFrmTitle" ForeColor="#3366FF"></asp:Label>
                    </div>
                    <table class="tableBox" cellspacing="0" cellpadding="2">
                        <tr>
                            <td class="tableAlignRight">
                                用户账号：
                            </td>
                            <td class="tableAlignLeft">
                                <input id="txtUserAccount" type="text" runat="server" class="input_txt border_radius" />
                            </td>
                            <td class="tableAlignRight">
                                用户名称：
                            </td>
                            <td class="tableAlignLeft">
                                <input id="txtUserName" type="text" runat="server" class="input_txt border_radius" />
                                <input id="txtUserID" type="hidden" runat="server" />
                            </td>
                        </tr>
                        <tr id="userInfoPwd">
                            <td class="tableAlignRight">
                                登录密码：
                            </td>
                            <td class="tableAlignLeft">
                                <input type="password" id="txtPwd" runat="server" class="input_txt border_radius" />
                                <input id="txtPassword" type="hidden" runat="server" />
                            </td>
                            <td class="tableAlignRight">
                                确认密码：
                            </td>
                            <td class="tableAlignLeft">
                                <input type="password" id="txtRepwd" runat="server" class="input_txt border_radius" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tableAlignRight">
                                联系电话：
                            </td>
                            <td class="tableAlignLeft" colspan="3">
                                <input id="txtUserTel" type="text" runat="server" class="input_txt border_radius" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tableAlignRight">
                                商家：
                            </td>
                            <td class="tableAlignLeft">
                                <select id="sltShop" name="sltShop" runat="server" class="selectWidth">
                                </select>
                                <input type="text" id="txtShopName" style="width: 33px; display: none" runat="server" />
                            </td>
                            <td class="tableAlignRight">
                                管理权限：
                            </td>
                            <td class="tableAlignLeft">
                                <select id="sltGroupID" name="sltGroupID" runat="server" class="selectWidth">
                                </select><input type="text" id="txtGroupName" style="width: 33px; display: none"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tableAlignRight">
                                是否锁定：
                            </td>
                            <td colspan="3" class="tableAlignLeft">
                                <label>
                                    <input type="radio" name="radChooseYesOrNo" id="radChooseYes" runat="server" value="0" />暂时锁定</label><label>
                                        <input type="radio" name="radChooseYesOrNo" id="radChooseNo" runat="server" value="1"
                                            checked="true" />不锁定<font color="red">&nbsp;&nbsp;&nbsp;(锁定则该员工不能登录)</font></label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px; text-align: right; vertical-align: top;">
                                备注：
                            </td>
                            <td class="tableAlignLeft" colspan="3">
                                <textarea id="txtUserRemark" rows="3" runat="server" style="width: 465px; margin-left: 5px;
                                    word-wrap: break-word;"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <input type="button" id="btnUserSave" class="buttonColor" value="保   存" />
                                &nbsp
                                <input type="button" id="btnUserReset" class="buttonColor" value="重   置" />
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:HiddenField ID="HidUid" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
