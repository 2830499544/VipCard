<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemChangePwd.aspx.cs" Inherits="ChainStock.Member.MemChangePwd" %>

<%@ Register Src="../Controls/MemberSearch.ascx" TagName="MemberSearch" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
 
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Mem/MemChangePwd.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script> 
</head>
<body>
    <form id="frmChangePwd" runat="server">
    <input type="hidden" id="hdisOldPwd" runat="server" />
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div>
            <uc1:MemberSearch ID="ucMemberSearch" runat="server" />
        </div>
        <div class="user_regist_Allleft">
            <div class="user_regist_left">
                <div class="user_regist_infor" style="text-align: left">
                    会员修改密码信息
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr id="trOldPwd" runat="server">
                        <td class="tableStyle_left">
                            原始密码：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtOldPwd" type="password" class="border_radius" maxlength="20" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            新密码：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtNewPwd" type="password" class="border_radius" maxlength="20" />
                            <input type="checkbox" runat="server" id="RegNullPwd" style="display: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            确定密码：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtRePwd" type="password" class="border_radius" maxlength="20" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            备注：
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <textarea id="txtChangePwdRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                outline: none; resize: none;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 36px; border:none;">
                            <label id="lblOldPwd" style="display: none">
                                <input id="chkIsOldPwd" runat="server" type="checkbox" /></label>
                            <input id="btnChangePwdSave" type="button" value="保   存" class="buttonColor" />
                            &nbsp
                            <input id="btnChangePwdReset" type="button" class="buttonRest" value="重   置" />
                        </td>
                    </tr>
                </table>
            </div>
            <input id="MemCard" type="hidden" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
