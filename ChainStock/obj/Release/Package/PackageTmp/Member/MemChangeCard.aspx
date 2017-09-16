<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemChangeCard.aspx.cs"
    Inherits="ChainStock.Member.MemChangeCard" %>
    <%@ Register Src="../Controls/MemberSearch.ascx" TagName="MemberSearch" TagPrefix="uc1" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Mem/MemChangeCard.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmChangeCard" runat="server">
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
                    换卡信息
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
                            新会员卡号：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtNewCard" type="text" class="border_radius" maxlength="20" />

                            &nbsp;
                            <input type="button" id="btnSenseICCardInit" runat="server"  value="感应式IC卡初始化"/>
                            <input type="button" id="btnContactICCardInit" runat="server" value="接触式IC卡初始化"/>

                            <input id="btnSendSenseICCard" type="button"  value="  发 卡  " runat="server"/>
                            <input id="btnContactICCard" type="button" value=" 发   卡 " runat="server" class="buttonColor" />
                        </td>
                    </tr>
                   <%-- <tr >
                        <td class="tableStyle_left">
                            卡面号码：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtCardNumber" type="text" class="input_txt border_radius" maxlength="20" />
                        </td>
                    </tr>--%>
                    <tr id="trNewPwd" runat="server">
                        <td class="tableStyle_left">
                            新卡密码：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtNewPwd" type="password" class="border_radius" maxlength="20" />
                            <input type="checkbox" runat="server" id="RegNullPwd" style="display: none;" />
                            <label id="lblchkPwd" runat="server" style="vertical-align: text-bottom;">
                                &nbsp;&nbsp;
                                <label class="lbsetCk" style="vertical-align: middle;">
                                    <input id="chkPwd" runat="server" type="checkbox" />
                                    密码与旧卡保持一致</label></label>
                        </td>
                    </tr>
                    <tr id="trReNewPwd" runat="server">
                        <td class="tableStyle_left">
                            确认密码：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtReNewPwd" type="password" class="border_radius" maxlength="20" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            备注：
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <textarea id="txtCgCardRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                outline: none; resize: none;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 36px; border:none; ">
                            <input id="btnChangeCardSave" type="button" value="保   存" class="buttonColor" />
                            &nbsp
                            <input id="btnChangeCardReset" type="button" class="buttonRest" value="重   置" />
                            <input id="MemCard" type="hidden" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
