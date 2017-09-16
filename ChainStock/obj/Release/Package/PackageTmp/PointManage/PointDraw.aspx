<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PointDraw.aspx.cs" Inherits="ChainStock.PointManage.PointDraw" %>
    <%@ Register Src="../Controls/MemberSearch.ascx" TagName="MemberSearch" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/PointManage/PointDraw.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>
    


</head>
<body>
    <form id="frmPointChange" runat="server">
    <div class="system_Info box_right">

    <%--打印份数--%>
        <input type="hidden" value="" id="PointNum" runat="server"/>

        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div>
        </div>
        <div class="user_regist_Allleft">
            <div class="user_regist_left">
                <div class="user_regist_infor" style="text-align: left">
                    申请积分提现
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            提现单号： <input type="text" id="txtDrawType" style="width: 33px; display: none" runat="server" />
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblDrawAccount" runat="server" style="font-size: 14px; font-weight: bold;">
                            </label>
                             <input type="text" id="txtShopID" style="width: 33px; display: none" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            提现日期：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblDrawTime" runat="server" style="font-size: 14px; font-weight: bold;">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            操作人员：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblDrawUser" runat="server" style="font-size: 14px; font-weight: bold;
                                margin-left: 5px">
                            </label>   <input type="text" id="txtDrawUserID" style="width: 33px; display: none" runat="server" />
                        </td>
                    </tr>
                     <tr>
                        <td class="tableStyle_left">
                            可用提现积分：
                        </td>
                        <td class="tableStyle_right">
                         <input id="txtTotalPoint" type="text" runat="server" class="border_radius" maxlength="5"   readonly="true" />
                      
                           (当前提现比率为  <label id="lbl_PointDrawPercent" runat="server" style="font-size: 14px; font-weight: bold;">
                            </label> ）
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            本次提现积分：
                        </td>
                        <td class="tableStyle_right">
                           <input id="txtDrawPoint" type="text" runat="server" class="border_radius" maxlength="5" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="tableStyle_left">
                            提现金额：
                        </td>
                        <td class="tableStyle_right">
                           <input id="txtDrawAmount" type="text" runat="server" class="border_radius" maxlength="5" readonly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            备注：
                        </td>
                        <td class="tableStyle_right">
                            <textarea id="txtDrawRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                resize: none; outline: none;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center; height: 36px">
                            <% if (curParameter.bolSms && curParameter.bolAutoSendSMSByMemPointChange)
                               { %>
                            <label class="lbsetCk" style="vertical-align: middle;">
                                <label style="vertical-align: middle;">
                                    <input id="chkSMS" runat="server" type="checkbox" />
                                    发送短信</label></label>&nbsp;&nbsp;
                            <%} %>
                            <% if (curParameter.bolAutoPrint)
                               {%>
                            <label class="lbsetCk" style="vertical-align: middle;">
                                <label style="vertical-align: middle;">
                                    <input id="chkPrint" runat="server" type="checkbox" />
                                    打印小票</label></label>&nbsp;&nbsp;
                            <% } %>
                            <input id="btnChangeSave" type="button" value="保   存" class="buttonColor" />&nbsp;
                            <input id="btnChangeReset" type="button" class="buttonRest" value="重   置" />
                            <asp:Label ID="lblPrintTitle" Style="display: none" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblPrintFoot" Style="display: none" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
