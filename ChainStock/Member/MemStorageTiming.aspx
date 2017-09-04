<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemStorageTiming.aspx.cs"
    Inherits="ChainStock.Member.MemStorageTiming" %>

<%@ Register Src="../Controls/Pay.ascx" TagName="Pay" TagPrefix="uc3" %>
<%@ Register Src="../Controls/MemberSearch.ascx" TagName="MemberSearch" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>  
    
    <script src="../Scripts/Module/Mem/MemStorageTiming.js" type="text/javascript"></script>    
</head>
<body>
    <form id="frmExpense" runat="server">
    <input type="hidden" id="txtProjectID" value="" />
    <div id="TimingProjectList" style="width: 600px; height:auto; display: none;">


     <%--打印的次数 --%>
    <input type="hidden" value="" id="PointNum" runat="server"/>


        <table class="table-style table-hover user_List_txt">
            <thead class="thead">
                <tr class="th">
                    <th style="width: 60px">
                        服务名称
                    </th>
                    <th style="width: 60px">
                        剩余时长
                    </th>
                    <th style="width: 60px">
                        计时规则
                    </th>
                    <th style="width: 160px">
                        规则描述
                    </th>
                </tr>
            </thead>
        </table>
        <div style="overflow: auto;">
            <table class="table-style table-hover user_List_txt" id="tbProject">
                <tr>
                    <td id="tdDetail" style="height: 20px; line-height: 50px; background-color: #fff;"
                        colspan="4" type="LoadingBar">
                        <script type="text/javascript">
                            ListLoading();
                        </script>
                    </td>
                </tr>
            </table>
        </div>
        <div id="ProjectList" style="margin: 0px; height: 30px; text-align: right;">
            <div class="listTablePage_simple">
            </div>
        </div>
        <div style="height: 20px; line-height: 20px; text-align: center; padding-top: 5px">
            计时服务名称：<input type="text" id="txtProjectName" class="border_radius" style="clear: both;
                float: none" />
            <input type="button" id="btnProjectSearch" onclick="ChooseProject()" class="common_Button common_ServiceButton"
                value="查找" />
        </div>
    </div>
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_List_All" style="margin-bottom: 0px">
            <uc1:MemberSearch ID="ucMemberSearch" runat="server" />
        </div>
        <div style="width: 350px">
            <uc3:Pay ID="ucPay" runat="server" />
        </div>
        <div class="user_regist_Allleft">
            <div class="user_regist_left">
                <div class="user_regist_infor" style="text-align: left">
                    消费信息
                </div>
                <input id="MemCard" type="hidden" runat="server" />
                <input id="chkAllowPwd" type="checkbox" style="display: none" runat="server" />
                <asp:Label ID="lblPrintTitle" Style="display: none" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblPrintFoot" Style="display: none" runat="server" Text=""></asp:Label>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            订单编号：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblOrderAccount" runat="server">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            消费日期：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblOrderCreateTime" runat="server">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            操作人员：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblOrderUSer" runat="server">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            选择服务：
                        </td>
                        <td class="tableStyle_right">
                            <input type="button" id="btnChooseProject" class="common_Button" value="选择服务" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            服务信息：
                        </td>
                        <td class="tableStyle_right">
                            <span id="lbProjectDescription"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            充值时间：
                        </td>
                        <td class="tableStyle_right">
                            <label style="vertical-align: text-bottom;">
                                <input id="txtRechargeTime" type="text" runat="server" class="border_radius" maxlength="8" />
                                <label style="vertical-align: middle; font-size: 12px; color: #C3C0B7;">
                                    &nbsp;&nbsp;单位分钟</label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            消费金额：
                        </td>
                        <td class="tableStyle_right">
                            <label style="vertical-align: text-bottom;">
                                <input id="txtExpMoney" type="text" runat="server" class="border_radius" maxlength="8" />
                                <label style="vertical-align: middle; font-size: 12px; color: #C3C0B7;">
                                    &nbsp;&nbsp;实际消费金额</label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            折后金额：
                        </td>
                        <td class="tableStyle_right">
                            <label style="vertical-align: text-bottom;">
                                <input id="txtDiscountMoney" type="text" runat="server" class="border_radius" maxlength="8" />
                                <label style="vertical-align: middle; font-size: 12px; color: #C3C0B7;">
                                    &nbsp;&nbsp;折后总金额，此金额可以手动修改</label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            可得积分：
                        </td>
                        <td class="tableStyle_right">
                            <label style="vertical-align: text-bottom;">
                                <input id="txtExpPoint" type="text" runat="server" class="border_radius" value="0" />
                                <label style="vertical-align: middle; font-size: 12px; color: #C3C0B7">
                                    &nbsp;&nbsp;按照折后总金额计算出的积分数量，可以手动修改</label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            备注：
                        </td>
                        <td class="tableStyle_right">
                            <textarea id="txtExpRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                outline: none; resize: none;"></textarea>
                        </td>
                    </tr>
                </table>
                <div class="user_regist_left">
                    <div style="text-align: center; height: 36px">
                        <label id="lblIsSMS" runat="server" style="vertical-align: text-bottom;">
                            <label class="lbsetCk" style="vertical-align: middle;">
                                <input id="chkSMS" type="checkbox" runat="server" />
                                发送短信 &nbsp;</label></label>
                        <input id="chkIsSMS" runat="server" type="checkbox" style="display: none" />
                        <label id="lblIsPrint" style="vertical-align: text-bottom;">
                            <label class="lbsetCk" style="vertical-align: middle;">
                                <input id="chkPrint" type="checkbox" runat="server" />
                                打印小票 &nbsp;</label></label>
                        <input id="btnExpSave" type="button" class="buttonColor" value="结   算" />&nbsp;
                        <input id="btnExpenseReset" type="button" class="buttonColor" value="重   置" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
