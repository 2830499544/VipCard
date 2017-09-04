<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffList.aspx.cs" Inherits="ChainStock.SystemManage.StaffList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/StaffList.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/mktree.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmStaffList" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div id="divStaff" style="display: none">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 700px; margin: auto">
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>员工编号：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtStaffNumber" type="text" runat="server" class="border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>员工姓名：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtStaffName" type="text" runat="server" class="border_radius" />
                                    <input id="txtStaffID" type="hidden" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    员工性别：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltStaffSex" runat="server" class="selectWidth">
                                        <option value="0">男</option>
                                        <option value="1">女</option>
                                    </select>
                                </td>
                                <td class="tableStyle_left">
                                    手机号码：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtStaffMobile" type="text" runat="server" class="border_radius" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>所属部门：
                                </td>
                                <td class="tableStyle_right" colspan="3">
                                    <select id="sltStaffClass" runat="server" class="selectWidth">
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    联系地址：
                                </td>
                                <td class="tableStyle_right" colspan="3">
                                    <input id="txtStaffAddress" type="text" runat="server" class="border_radius" style="width: 520px;" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    备注：
                                </td>
                                <td class="tableStyle_right" colspan="3">
                                    <textarea id="txtStaffRemark" rows="3" runat="server" style="width: 520px; word-wrap: break-word;
                                        outline: none; resize: none;"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="tableStyle_right ">
                                    <div class="buton" style="text-align: center;">
                                        <input id="btnStaffSave" type="button" class="buttonColor" value="保   存 " />
                                        <input id="btnStaffReset" type="button" class="buttonRest" value="重   置 " />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="user_List_All">
                        <div class="user_List_top">
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <input id="btnStaffAdd" type="button" value="新增员工" runat="server" class="common_Button" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    员工信息：
                                </td>
                                <td class="tableStyle_right" style="width: 200px;">
                                    <input id="txtQuery" type="text" class="border_radius" title="员工编号 / 姓名 / 手机" runat="server" />
                                </td>
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <input id="btnStaffQuery" type="button" value="查   询" runat="server" class="common_Button" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td style="width: 200px; vertical-align: top; text-align: left;">
                                    <div id="StaffList_ClassTree" runat="server">
                                    </div>
                                </td>
                                <td style="vertical-align: top;">
                                    <table border="0" cellpadding="0" cellspacing="1" class="table-style table-hover user_List_txt"
                                        width="100%" id="StaffList">
                                        <tr>
                                            <td>
                                                <script type="text/javascript">
                                                    ListLoading();
                                                </script>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
