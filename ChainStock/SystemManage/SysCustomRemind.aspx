<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysCustomRemind.aspx.cs"
    Inherits="ChainStock.SystemManage.SysCustomRemind" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/CustomRemind.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <style type="text/css">
        #tdauttable table tr td
        {
            border: 0px;
        }
    </style>
</head>
<body>
    <form id="frmCustomRemind" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div style="width: 100%; text-align: center; display: none;" id="DIVCustomRemind">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 450px;">                            
                            <tr>
                                <td class="tableStyle_left">
                                    操作人员：
                                </td>
                                <td class="tableStyle_right">
                                    <label id="lblCustomRemindUSer" runat="server" style="font-size: 14px; font-weight: bold;
                                        margin-left: 5px">
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>提醒日期：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtCustomRemindTime" type="text" class="border_radius" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>提醒标题：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtCustomRemindTitle" type="text" class="border_radius" runat="server" />
                                    <input id="txtCustomRemindID" type="hidden" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>提醒用户：
                                </td>
                                <td class="tableStyle_right" id="tdauttable">
                                    <asp:CheckBoxList ID="cblCustomReminder" runat="server" RepeatDirection="Horizontal"
                                        RepeatLayout="Table" RepeatColumns="4" EnableTheming="True">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    提醒详情：
                                </td>
                                <td class="tableStyle_right" colspan="3">
                                    <textarea id="txtCustomRemindDetail" class="border_radius" rows="3" runat="server"
                                        style="width: 90%; height: 40px; word-wrap: break-word;"></textarea>
                                </td>
                            </tr>
                            <tr align="center">
                                <td colspan="4" class="tableStyle_right ">
                                    <div class="buton" style="text-align: center;">
                                        &nbsp;<input id="btnCustomRemindSave" type="submit" class="buttonColor" value="保   存"
                                            runat="server" />
                                        &nbsp;<input id="btnCustomRemindReset" type="button" class="buttonRest" value="重   置" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="user_List_All">
                        <div class="user_List_top" style="height: 34px;">
                            <table class="tableStyle" style="width: 100%">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <input id="btnCustomRemindAdd" type="button" runat="server" value="新增提醒" class="common_Button"
                                                onclick="CustomRemind(0)" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvCustomRemind">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                提醒人
                                            </th>
                                            <th>
                                                提醒标题
                                            </th>
                                            <th>
                                                提醒详情
                                            </th>
                                            <th>
                                                提醒时间
                                            </th>
                                            <th>
                                                商家
                                            </th>
                                            <th>
                                                操作员
                                            </th>
                                            <th>
                                                操作
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                            <%# Eval("CustomReminder")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("CustomRemindTitle")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("CustomRemindDetail")%>
                                        </td>
                                        <td>
                                            <%# Eval("CustomRemindTime", "{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td>
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                        <td class="listtd" style="width: 40px;">
                                            <a id="hyCustomRemindDel" runat="server" href="#" onclick='<%# string.Format("return btnCustomRemindDel(\"{0}\",\"{1}\")",Eval("CustomRemindTitle"),Eval("CustomRemindID")) %>'>
                                                <img src="../images/Gift/del.png" alt="删除" title="删除" />
                                            </a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
