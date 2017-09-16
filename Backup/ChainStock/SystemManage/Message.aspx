<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="ChainStock.SystemManage.Message" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/Message.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmMessage" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div class="user_List_All">
                        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    会员信息：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtQueryMem" type="text" runat="server" class="border_radius" title="会员卡号/姓名/手机号" />
                                </td>
                                <td class="tableStyle_left">
                                    留言时间：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtStartTime" type="text" runat="server" class="Wdate border_radius" style="float: none;
                                        clear: both;" />
                                </td>
                                <td class="tableStyle_left">
                                    &nbsp;至&nbsp;
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtEndTime" type="text" runat="server" class="Wdate border_radius" style="float: none;
                                        clear: both;" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    所属商家：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltShop" runat="server" class="selectWidth" name="D2">
                                    </select>
                                </td>
                                <td colspan="3" style="border: none">
                                </td>
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btQueryMemSearch" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btQueryMemSearch_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvMessageList">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                会员卡号
                                            </th>
                                            <th>
                                                会员姓名
                                            </th>
                                            <th>
                                                手机号码
                                            </th>
                                            <th>
                                                所属商家
                                            </th>
                                            <th>
                                                留言日期
                                            </th>
                                            <th>
                                                留言总数
                                            </th>
                                            <th>
                                                是否留言
                                            </th>
                                            <th>
                                                操作
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tbody>
                                        <tr class="td">
                                            <td>
                                                <%# Eval("MemCard") %>
                                            </td>
                                            <td>
                                                <%# Eval("MemName") %>
                                            </td>
                                            <td>
                                                <%# Eval("MemMobile") %>
                                            </td>
                                            <td>
                                                <%# Eval("ShopName") %>
                                            </td>
                                            <td>
                                                <%# Eval("MessageTime","{0:yyyy-MM-dd}") %>
                                            </td>
                                            <td style="text-align: right">
                                                <%# Eval("MessageCount") %>
                                            </td>
                                            <td>
                                                <%# Eval("MessageIsReply") %>
                                            </td>
                                            <td class="listtd" style="width: 60px;">
                                                <a href="#" onclick='<%# string.Format(" btnMessageReply(\"{0}\")",Eval("MemID")) %>'
                                                    id="hyMessageReply" runat="server">
                                                    <img src="../images/Gift/manage.png" title="管理" alt="管理" />
                                                </a><a href="#" runat="server" id="hyMessageDel" onclick='<%# string.Format("return btnMessageDel(\"{0}\",\"{1}\")",Eval("MemID"),Eval("MemName")) %>'>
                                                    <img alt="删除" title="删除" src="../images/Gift/del.png" />
                                                </a>
                                            </td>
                                        </tr>
                                    </tbody>
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
