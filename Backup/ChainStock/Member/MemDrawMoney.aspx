<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemDrawMoney.aspx.cs" Inherits="ChainStock.Member.MemDrawMoney" %>
<%@ Register Src="../Controls/MemberSearch.ascx" TagName="MemberSearch" TagPrefix="uc1" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" /> 
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Mem/MemDrawMoney.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>

    </head>
<body>
    <form id="frmDrawMoney" runat="server">
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
                    延期信息
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <th style="text-align: center; font-weight: bold; border:none;" class="th" colspan="4">
                            会员提现信息
                        </th>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            会员提现单号：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblMemDrawMoney" runat="server" style="font-size: 14px; font-weight: bold;">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            会员提现日期：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblDrawMoneyTime" runat="server" style="font-size: 14px; font-weight: bold;">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            当前折损比率：
                        </td>
                        <td class="tableStyle_right">
                            <span id="spanDrawPercent" runat="server" style="font-size: 14px; font-weight: bold;
                                color: Red;"></span>
                            <input id="txtDrawPercent" type="hidden" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            当前账户余额：
                        </td>
                        <td class="tableStyle_right">
                            <span id="spanCurrentMoney" style="font-size: 14px; font-weight: bold; color: Red;">
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            此次提取金额：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtDrawMoney" type="text" runat="server" class="border_radius" maxlength="8" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            实际扣除金额：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtAccountMoney" type="text" runat="server" class="border_radius" maxlength="8" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            备注：
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <textarea id="txtDrawRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                outline: none; resize: none;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 36px; border:none;">
                            <label id="lblIsSMS" runat="server" style="vertical-align: text-bottom;">
                                <label class="lbsetCk" style="vertical-align: middle;">
                                    <input id="chkSMS" type="checkbox" runat="server" />
                                    发送短信 &nbsp;
                                </label>
                            </label>

                              <label id="lblIsPrint" style="vertical-align: text-bottom;">
                            <label class="lbsetCk" style="vertical-align: middle;">
                                <input id="chkPrint" type="checkbox" runat="server" />
                                打印小票 &nbsp;</label></label>


                            <input id="chkIsSMS" runat="server" type="checkbox" style="display: none" />
                            <input id="btnDrawSave" type="button" class="buttonColor" value="保    存" />
                            &nbsp
                            <input id="btnDrawMoneyReset" type="button" class="buttonRest" value="重   置" />
                                 <input id="lblOrderUSers" type="hidden" class="border_radius" runat="server" value="0" />
                       
                        </td>
                    </tr>
                </table>
            </div>
            <input id="MemCard" type="hidden" runat="server" />
        </div>
    </div>
    <%--打印的次数--%>
        <asp:Label ID="lblPrintTitle" Style="display: none" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="lblPrintFoot" Style="display: none" runat="server" Text="Labe2"></asp:Label>
        <input type="hidden" value="" id="PointNum" runat="server" />
    </form>
</body>
</html>
