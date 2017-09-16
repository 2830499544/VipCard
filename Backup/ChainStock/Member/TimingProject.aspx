<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimingProject.aspx.cs"
    Inherits="ChainStock.Member.TimingProject" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
    <script src="../Scripts/Module/Mem/TimingProject.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="TimingProjectEdit" style="display: none;">
        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 400px; margin: auto">
            <tr>
                <td class="tableStyle_left">
                    <span style="color: #ff4800; vertical-align: middle">*</span>服务名称：
                </td>
                <td class="tableStyle_right">
                    <input id="txtProjectName" type="text" class="border_radius" maxlength="20" />
                </td>
            </tr>
            <tr>
                <td class="tableStyle_left">
                    <span style="color: #ff4800; vertical-align: middle">*</span>服务规则：
                </td>
                <td class="tableStyle_right">
                    <select id="sltProjectRulesID" runat="server" class="selectWidth">
                    </select>
                </td>
            </tr>
            <tr>
                <td class="tableStyle_left">
                    备注：
                </td>
                <td class="tableStyle_right">
                    <textarea id="txtProjectRemark" rows="3" style="width: 90%; word-wrap: break-word;
                        outline: none; resize: none;" title="请输入会员的备注"></textarea>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center; padding-top: 2px; padding-bottom: 2px;">
                    <input id="btnSaveform" type="button" onclick="SaveForm()" class="buttonColor" value="保  存" />
                    <input type="hidden" id="txtProjectID" value="" />
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
                                        <div class="user_List_Button">
                                            <input id="btnTimingProject" type="button" value="新增服务" runat="server" class="common_Button" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    服务名称：
                                </td>
                                <td class="tableStyle_right" style=" width:200px;">
                                    <input id="txtQuerProjectName" type="text" runat="server" class="border_radius" maxlength="20" />
                                </td>
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnUserSearch" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnUserSearch_Click1" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvTimingProject">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                服务名称
                                            </th>
                                            <th>
                                                计时规则
                                            </th>
                                            <th>
                                                添加时间
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
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td>
                                            <%# Eval("ProjectName")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("RulesName")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("ProjectAddTime", "{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <a href="#" onclick='<%# string.Format(" EditTimingProject(\"{0}\")",Eval("ProjectID")) %>'
                                                id="hyLevelEdit" runat="server">
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" />
                                            </a><a href="#" onclick='<%# string.Format(" DeleteTimingProject(\"{0}\",\"{1}\")",Eval("ProjectID"),Eval("ProjectName")) %>'
                                                id="hyLevelDelete" runat="server">
                                                <img src="../images/Gift/del.png" alt="删除" title="删除" />
                                            </a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <div class="user_List_page">
                            <table style="width: 100%" id="tabPager">
                                <tr>
                                    <td>
                                        <span id="spPageSize">&nbsp;每页记录数：</span>
                                        <asp:DropDownList ID="drpPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPageSize_SelectedIndexChanged">
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                        </asp:DropDownList>
                                        <webdiyer:AspNetPager ID="NetPagerParameter" runat="server" AlwaysShow="True" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条"
                                            CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页" LastPageText="尾页"
                                            NextPageText="下一页" OnPageChanging="NetPagerParameter_PageChanging" PrevPageText="上一页"
                                            ShowPageIndexBox="Always" PageSize="10" LayoutType="Table" PageIndexBoxType="DropDownList"
                                            ShowCustomInfoSection="Left" CustomInfoClass="paginator" CustomInfoSectionWidth="300px"
                                            SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" Direction="LeftToRight"
                                            HorizontalAlign="Right">
                                        </webdiyer:AspNetPager>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
