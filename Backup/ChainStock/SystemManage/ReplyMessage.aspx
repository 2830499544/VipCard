<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReplyMessage.aspx.cs" Inherits="ChainStock.SystemManage.ReplyMessage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/ReplyMessage.js" type="text/javascript"></script>
</head>
<body style="overflow: auto;">
    <form id="frmReply" runat="server">
    <input id="txtMemID" type="hidden" runat="server" />
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
                        <table class="table-style table-hover user_List_txt" id="MsgList_Table">
                            <asp:Repeater runat="server" ID="rptMsgList">
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
                                                状态
                                            </th>
                                            <th>
                                                留言时间
                                            </th>
                                            <th>
                                                回复时间
                                            </th>
                                            <th>
                                                留言内容
                                            </th>
                                            <th>
                                                回复内容
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
                                            <%# Eval("MemCard")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("MemName")%>
                                        </td>
                                        <td <%# Eval("MessageIsReply").ToString() == "0" ? "style='color:Red'" : "style='color:Blue'"%>>
                                            <%# Eval("MessageIsReply").ToString() == "0"?"未回复":"已回复"%>
                                        </td>
                                        <td>
                                            <%# Eval("MessageTime", "{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td>
                                            <%# string.IsNullOrEmpty(Eval("MessageReplyTime").ToString()) ? "" : Convert.ToDateTime(Eval("MessageReplyTime")).ToString("yyyy-MM-dd")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("MessageContent") %>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("MessageReplyContent")%>
                                        </td>
                                        <td class="listtd" style="width: 40px;">
                                            <%# BindHtml(Convert.ToInt32(Eval("MessageID")), Convert.ToInt32(Eval("MessageMemID")), Eval("MessageContent").ToString())%>
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
                    <div id="dvMessageReply" style="display: none;">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 600px;">
                            <tr>
                                <td class="tableStyle_left">
                                    留言内容：
                                </td>
                                <td class="tableStyle_right">
                                    <span id="spMessage"></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    回复内容：<span id="MessageID" style="display: none"></span>
                                </td>
                                <td class="tableStyle_right">
                                    <textarea name="content" rows="4" id="content" class="border_radius" style="width: 90%"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" class="tableStyle_right">
                                    <div class="buton" style="text-align: center;">
                                        <input type="button" id="btReply" value="提交" class="buttonColor" />
                                    </div>
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
