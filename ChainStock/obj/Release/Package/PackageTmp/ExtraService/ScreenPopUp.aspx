<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScreenPopUp.aspx.cs" Inherits="ChainStock.ExtraService.ScreenPopUp" %>

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
    <form id="frmScreenPopUp" runat="server">
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
                                    电话来源：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltCallerIsMem" runat="server" class="selectWidth">
                                        <option value="-1">===== 请选择 =====</option>
                                        <option value="1">会员来电</option>
                                        <option value="2">非会员来电</option>
                                    </select>
                                </td>
                                <td class="tableStyle_left">
                                    来电状态：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltCallerState" runat="server" class="selectWidth">
                                        <option value="-1">===== 请选择 =====</option>
                                        <option value="1">未接来电</option>
                                        <option value="2">已接来电</option>
                                    </select>
                                </td>
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnCallerQuery" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnCallerQuery_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvScreenPopUp">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                手机号码
                                            </th>
                                            <th>
                                                电话来源
                                            </th>
                                            <th>
                                                会员姓名
                                            </th>
                                            <th>
                                                来电状态
                                            </th>
                                            <th>
                                                来电时长
                                            </th>
                                            <th>
                                                来电备注
                                            </th>
                                            <th>
                                                来电时间
                                            </th>
                                            <th>
                                                接电操作员
                                            </th>
                                            <th>
                                                接电商家
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                            <%# Eval("CallerMobile")%>
                                        </td>
                                        <td>
                                            <%# Eval("CallerIsMem")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("MemName")%>
                                        </td>
                                        <td>
                                            <%# Eval("CallerState")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("CallerDuration")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("CallerRemark")%>
                                        </td>
                                        <td>
                                            <%# Eval("CallerCreateTime", "{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                        <td>
                                            <%# Eval("ShopName")%>
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
