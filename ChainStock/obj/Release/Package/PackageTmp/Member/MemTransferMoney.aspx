<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemTransferMoney.aspx.cs"
    Inherits="ChainStock.Member.MemTransferMoney" %>     
<%@ Register Src="../Controls/MemberSearch.ascx" TagName="MemberSearch" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员转账</title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Mem/MemTransferMoney.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>    
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmTransferMoney" runat="server">
    <div class="system_Info box_right">
        <%--打印的次数 --%>
        <input type="hidden" value="" id="PointNum" runat="server"/>
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
                    转账信息
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            转账单号：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblAccount" runat="server" style="font-size: 14px; font-weight: bold;">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            转账日期：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblTransferTime" runat="server" style="font-size: 14px; font-weight: bold;">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            操作人员：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblTransferUSer" runat="server" style="font-size: 14px; font-weight: bold;">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            收款会员卡号：
                        </td>
                        <td class="tableStyle_right">
                             <input id="txtTransferMemCard" type="text" runat="server" class="border_radius" />                         
                      </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            转账金额：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtMoney" type="text" runat="server" class="border_radius" value="0" maxlength="8" />                                              
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            密码验证：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtPassword" type="password" runat="server" class="border_radius" value="0"
                                maxlength="8" />   <span style="color:#FF5809;font-size:11px">&nbsp;&nbsp;密码为空请输入"#"键</span>      
                        </td>
                    </tr>                 
                    <tr>
                        <td class="tableStyle_left">
                            备注：
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <textarea id="txtTransferRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                resize: none; outline: none;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 36px; border:none; " >
                            <label id="lblIsSMS" style="vertical-align: text-bottom;" runat="server">
                                <label class="lbsetCk" style="vertical-align: middle;">
                                    <input id="chkSMS" type="checkbox" runat="server" />
                                    <input id="chkIsSMS" runat="server" type="checkbox" style="display: none" />
                                    发送短信 &nbsp;
                                </label>
                            </label>                            
                            <input id="btnTransferSave" type="button" value="转  账" class="buttonColor" />
                            &nbsp
                            <input id="btnTransferReset" type="button" class="buttonRest" value="重   置" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblPrintTitle" Style="display: none" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblPrintFoot" Style="display: none" runat="server" Text="Labe2"></asp:Label>
                <input id="MemCard" type="hidden" runat="server" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
