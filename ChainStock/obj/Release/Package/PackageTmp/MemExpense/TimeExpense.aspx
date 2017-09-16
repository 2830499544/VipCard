<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeExpense.aspx.cs" Inherits="ChainStock.MemExpense.TimeExpense" %>

<%@ Register Src="../Controls/FindMember.ascx" TagName="FindMember" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Skin/default/formMain.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/Skin/default/newstyle.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/Skin/default/jqModal.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Print.js" type="text/javascript"></script>
    <script src="../Scripts/jqModal.js" type="text/javascript"></script>
    <script src="../Scripts/Module/MemExpense/TimeExpense.js" type="text/javascript"></script>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmExpense" runat="server">
    <div id="TimingProjectList" style="width: 600px; display: none;">
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
    <input type="hidden" id="txtProjectID" value="" />
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_List_All" style="margin-bottom: 0px">
            <uc1:FindMember ID="ucMemberSearch" runat="server" />
        </div>
        <div class="user_regist_Allleft">
            <div class="user_regist_left">
                <div class="user_regist_infor" style="text-align: left">
                    选择服务
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            散客令牌：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtOrderToken" type="text" maxlength="20" class="border_radius radius2" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            预定消费时长：
                        </td>
                        <td class="tableStyle_right">
                            <label style="vertical-align: text-bottom;">
                                <input id="txtOrderPredictTime" type="text" maxlength="8" class="border_radius radius2" />
                                <label style="vertical-align: middle; font-size: 12px; color: #C3C0B7">
                                    &nbsp;&nbsp;单位分钟</label>
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
                            服务状态：
                        </td>
                        <td class="tableStyle_right">
                            <span id="spstatus"></span>
                        </td>
                    </tr>
                </table>
                <div class="user_regist_left">
                    <div style="text-align: center; height: 36px">
                        <input id="btnExpSave" type="button" class="buttonColor" value="消费开始" />&nbsp;
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
