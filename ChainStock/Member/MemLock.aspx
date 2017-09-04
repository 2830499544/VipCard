<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemLock.aspx.cs" Inherits="ChainStock.Member.MemLock" %>
 
<%@ Register src="../Controls/MemberSearch.ascx" tagname="MemberSearch" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />  
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Mem/MemLock.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmMemLock" runat="server">
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
                    挂失锁定
                </div>
                <div style="width: 100%; text-align: center;">
                    <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                        <tr>
                            <td class="tableStyle_left">
                                当前会员状态：
                            </td>
                            <td class="tableStyle_right">
                                <label id="MemCurrentState" runat="server" style="font-size: 14px; font-weight: bold;
                                    color: Red;">
                                </label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tableStyle_left">
                                将状态修改为：
                            </td>
                            <td class="tableStyle_right">
                                <select id="sltMemNewState" class="selectWidth">
                                    <option value="0">正常</option>
                                    <option value="1">锁定</option>
                                    <option value="2">挂失</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="tableStyle_left">
                                备注：
                            </td>
                            <td class="tableStyle_right" colspan="3">
                                <textarea id="txtMemLockRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                    outline: none; resize: none;"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center; height: 36px;  border:none;">
                                <input id="btnMemLockSave" type="button" class="buttonColor" value="保    存" />
                                &nbsp
                                <input id="btnMemLockReset" type="button" class="buttonRest" value="重   置" />
                            </td>
                        </tr>
                    </table>
                </div>
                <input id="MemCard" type="hidden" runat="server" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
