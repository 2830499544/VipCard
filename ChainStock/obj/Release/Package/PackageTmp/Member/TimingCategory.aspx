<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimingCategory.aspx.cs"
    Inherits="ChainStock.Member.TimingCategory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Mem/TimingCategory.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="TimingCategoryEdit" style="display: none;">
        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 400px; margin: auto">
            <tr>
                <td class="tableStyle_left">
                    所属类别：
                </td>
                <td class="tableStyle_right">
                    <select id="sltCategoryFather" runat="server" class="selectWidth">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="tableStyle_left">
                    <span style="color: #ff4800; vertical-align: middle">*</span>类别名称：
                </td>
                <td class="tableStyle_right">
                    <input id="txtCategoryName" type="text" class="border_radius" maxlength="20" />
                </td>
            </tr>
            <tr>
                <td class="tableStyle_left">
                    备注：
                </td>
                <td class="tableStyle_right">
                    <textarea id="txtCategoryrRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                        outline: none; resize: none;" title="请输入会员的备注"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; padding-top: 2px; padding-bottom: 2px;">
                    <input id="btnSaveform" type="button" onclick="SaveForm()" class="buttonColor" value="保  存" />
                    <input type="hidden" id="txtCategoryID" value="" />
                </td>
            </tr>
        </table>
    </div>
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
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
                                        <input id="btnTimingCategory" runat="server" type="button" value="新增类别" class="common_Button" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvTimingCategory">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                类别名称
                                            </th>
                                            <th>
                                                备注
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
                                        <td style=" text-align:left;">
                                            <%# Eval("CategoryName")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("CategoryrRemark")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <a href="#" onclick='<%# string.Format(" EditCategory(\"{0}\")",Eval("CategoryID")) %>'
                                                id="hyLevelEdit" runat="server">
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" />
                                            </a><a href="#" onclick='<%# string.Format(" DeleteCategory(\"{0}\",\"{1}\")",Eval("CategoryID"),Eval("CategoryName")) %>'
                                                id="hyLevelDelete" runat="server">
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
