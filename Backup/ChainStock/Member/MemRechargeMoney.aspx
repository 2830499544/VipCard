<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemRechargeMoney.aspx.cs"
    Inherits="ChainStock.Member.MemRechargeMoney" %>     
<%@ Register Src="../Controls/MemberSearch.ascx" TagName="MemberSearch" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Mem/MemRechageMoney.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>
    
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmRechargeMoney" runat="server">
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
                    充值信息
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            充值单号：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblAccount" runat="server" style="font-size: 14px; font-weight: bold;">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            充值日期：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblRechargeTime" runat="server" style="font-size: 14px; font-weight: bold;">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            操作人员：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblRechargeUSer" runat="server" style="font-size: 14px; font-weight: bold;">
                            </label>
                        </td>
                    </tr>
                   
                    <tr>
                        <td class="tableStyle_left">
                            充值金额：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtMoney" type="text" runat="server" class="border_radius" value="0" maxlength="8" />
                            &nbsp;&nbsp;
                            <label style="vertical-align: text-bottom;">
                                <label class="lbsetCk" style="vertical-align: middle;">
                                    <input id="chkBank" type="checkbox" title="若为银联卡充值行为时请勾选此项,系统统计收入时将单独统计此项" />
                                    银联充值
                                </label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            赠送金额：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtGiveMoney" type="text" runat="server" class="border_radius" value="0"
                                maxlength="8" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            充值合计：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtTotal" type="text" runat="server" class="border_radius" value="0" maxlength="8" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            可得积分：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtPoint" type="text" runat="server" class="border_radius" value="0" maxlength="8" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            备注：
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <textarea id="txtRechangeRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
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
                            <label id="lblIsPrint" style="vertical-align: text-bottom;">
                                <label class="lbsetCk" style="vertical-align: middle;">
                                    <input id="chkPrint" type="checkbox" runat="server" />
                                    打印小票 &nbsp;</label></label>
                            <input id="btnRechageSave" type="button" value="保   存" class="buttonColor" />
                            &nbsp
                            <input id="btnRechargeReset" type="button" class="buttonRest" value="重   置" />
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
