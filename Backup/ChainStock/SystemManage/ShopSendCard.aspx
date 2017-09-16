<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopSendCard.aspx.cs" Inherits="ChainStock.SystemManage.ShopSendCard" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/ShopSendCard.js" type="text/javascript"></script>
</head>

<body>
    <form id="frmShopList" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <!--站点地图-->
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                        <input id="txtShopType" type="hidden"  runat="server"/>

                    </div>
                    <div class="user_List_All">
                        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                             <tr> 
                                <td class="tableStyle_left">
                                    商家：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltShop" runat="server" class="selectWidth">
                                    </select>
                                </td>
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnShopBuyCardQuery" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnShopBuyCardQuery_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>                    
                        <!--列表展示-->
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvShopBuyCard">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                购卡商家
                                            </th>
                                            <th>
                                                卡片起始号
                                            </th>
                                            <th>
                                                卡片截止号
                                            </th>
                                            <th>
                                                购卡总金额
                                            </th>
                                            <th>
                                                购卡时间
                                            </th>
                                            <th>
                                                备注
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
                                        <td style="text-align: left">
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td>
                                            <%# Eval("StartCardNumber")%>
                                        </td>
                                        <td>
                                            <%# Eval("EndCardNumber")%>
                                        </td>
                                        <td style="text-align: left; padding-left: 4px;">
                                            <%# Eval("BuyCardMoney","{0:F2}")%>
                                        </td>
                                        <td>
                                            <%# Eval("BuyCardTime")%>
                                        </td>
                                        <td style="text-align: left; padding-left: 4px;">
                                            <%# Eval("Remark")%>
                                        </td>
                                          <td style="text-align: left; padding-left: 4px;">
                                           <a href="#" onclick='<%# string.Format(" ShopBuyCardDelete(\"{0}\",\"{1}\",\"{2}\")",Eval("BuyCardID"),Eval("StartCardNumber"),Eval("EndCardNumber")) %>'
                                                id="aStaffClassDelete" runat="server">
                                                <img src="../images/Gift/del.png" alt="撤销" title="撤销" />
                                            </a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <!--分页-->
                        <div class="user_List_page">
                            <table style="width: 100%">
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
