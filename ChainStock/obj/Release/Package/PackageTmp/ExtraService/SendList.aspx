<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendList.aspx.cs" Inherits="ChainStock.ExtraService.SendList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
 
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmSendList" runat="server">
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
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvSendList">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                手机号码
                                            </th>
                                            <th>
                                                号码数量
                                            </th>
                                            <th>
                                                短信内容
                                            </th>
                                            <th>
                                                发送状态
                                            </th>
                                            <th>
                                                发送时间
                                            </th>
                                            <th>
                                                商家名称
                                            </th>
                                            <th>
                                                操作员
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
                                            <asp:Label ID="lblSmsMobile" runat="server" Text='<%# Eval("SmsMobile").ToString().Length > 32? Eval("SmsMobile").ToString().Substring(0,32)+"......":Eval("SmsMobile") %>'
                                                ToolTip='<%# Eval("SmsMobile") %>' />
                                        </td>
                                        <td style="text-align: right">
                                            <%# (Eval("SmsAllAmount").ToString()!=""&&Eval("SmsAmount").ToString()!="")? int.Parse(Eval("SmsAllAmount").ToString()) / int.Parse(Eval("SmsAmount").ToString()):0%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("SmsContent")%>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSmsState" runat="server" Text='已发送' ToolTip='已发送' />
                                        </td>
                                        <td>
                                            <%# Eval("SmsTime","{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td>
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserName")%>
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
