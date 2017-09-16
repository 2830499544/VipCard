<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsStockTotal.aspx.cs"
    Inherits="ChainStock.StockManage.GoodsStockTotal" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register src="../Controls/QuickSearch.ascx" tagname="QuickSearch" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

      <link href="../Inc/Style/Style.css" rel="stylesheet" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

     <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmPointRankList" runat="server">
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
                        <div class="user_List_top">
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <asp:Button ID="btnOut" runat="server" Text="导   出"  UseSubmitBehavior="false"   class="common_Button" OnClick="btnOut_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    商品信息：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtQuery" type="text" title="商品编码 / 名称 / 简码" class="border_radius radius2"
                                        runat="server" />
                                </td>
                                <td class="tableStyle_left">
                                    分店商家：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltShop" runat="server" class="selectWidth">
                                    </select>
                                      <input  id="HDsltshop" runat="server" type="hidden" />
                                </td>
                                <td class="tableStyle_left">
                                    商品分类：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltGoodsClass" runat="server" name="sltGoodsClass" class="selectWidth" />
                                </td>
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnStockQuery" Text="查   询" runat="server" class="common_Button"
                                            OnClick="btnStockQuery_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvPointRankList">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                商品编码
                                            </th>
                                            <th>
                                                商品名称
                                            </th>
                                            <th>
                                                商品简码
                                            </th>
                                            <th>
                                                参考价
                                            </th>
                                            <th>
                                                销售价
                                            </th>
                                            <th>
                                                库存数量
                                            </th>
                                            <th>
                                                所属商家
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                            <%# Eval("GoodsCode")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("Name")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("NameCode")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("GoodsBidPrice","{0:F2}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("Price","{0:F2}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("Number")%>
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
        <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
