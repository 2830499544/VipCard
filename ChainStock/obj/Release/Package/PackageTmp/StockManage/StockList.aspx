<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockList.aspx.cs" Inherits="ChainStock.StockManage.StockList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />

  
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/StockManage/StockList.js" type="text/javascript"></script>
    <style type="text/css">
        .fenye
        {
            background-position: initial initial;
            background-repeat: initial initial;
        }
        .mytree
        {
        }
        .mytree img
        {
            padding-right: 4px;
        }
    </style>
</head>
<body>
    <form id="frmStaffList" runat="server">
    <input type="hidden" id="otherStock" runat="server" />
    <asp:Label runat="server" ID="lbValue" Visible="false"></asp:Label>
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
                                <tr>
                                    <td class="user_List_styleLeft">
                                        快速查找：
                                    </td>
                                    <td class="user_List_styleRight" style="text-align: left;">
                                        <input id="txtQuery" type="text" title=" 商品编码 / 姓名 / 简码" class="border_radius" runat="server" onkeypress="getKey()"  
                                            style="float: left;" />&nbsp;
                                        <asp:Button ID="btnUserSearch" runat="server" Text="查   询" class="common_Button common_ServiceButton"
                                            OnClick="btnUserSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="main" style="width: 100%;">
                            <div id="main_left" style="width: 15%; float: left; vertical-align: top; text-align: left;">
                                <div id="StockList_ClassTree">
                                    <asp:TreeView runat="server" ID="tvGoodsClass" CssClass="mytree" NoExpandImageUrl="~/images/ico/dot.gif"
                                        CollapseImageUrl="~/images/ico/open.png" ExpandImageUrl="~/images/ico/close.png"
                                        Width="20" ForeColor="#4f4e4e"  OnSelectedNodeChanged="tvGoodsClass_SelectedNodeChanged"
                                        Font-Bold="False" ExpandDepth="1">
                                        <LevelStyles>
                                            <asp:TreeNodeStyle HorizontalPadding="0" VerticalPadding="0" ChildNodesPadding="1"
                                                Font-Size="13px" />
                                        </LevelStyles>
                                    </asp:TreeView>
                                </div>
                            </div>
                            <div id="main_right" style="float: left; width: 85%;">
                                <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 100%;" class="table-style table-hover user_List_txt">
                                    <asp:Repeater runat="server" ID="rptStockList">
                                        <HeaderTemplate>
                                            <tbody>
                                                <tr class="th">
                                                    <th style="width: 20%;">
                                                        商品编码
                                                    </th>
                                                    <th>
                                                        商品名称
                                                    </th>
                                                    <th style="width: 20%;">
                                                        商品简码
                                                    </th>
                                                    <th style="width: 20%;">
                                                        库存数量
                                                    </th>
                                                    <th>
                                                        操作
                                                    </th>
                                                </tr>
                                            </tbody>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="td">
                                                <td align="left">
                                                    <%# Eval("GoodsCode")%>
                                                </td>
                                                <td style="text-align: left">
                                                    <%# Eval("Name") %>
                                                </td>
                                                <td style="text-align: left">
                                                    <b>
                                                        <%# Eval("NameCode") %></b>
                                                </td>
                                                <td style="text-align: right">
                                                    <%# Eval("Number") %>
                                                </td>
                                                <td class="listtd" style="width: 40px;">
                                                    <a runat="server" id="aStock" href="#" onclick='<%# string.Format("GetGoodsAllStock(\"{0}\",\"{1}\")", Eval("GoodsID"), Eval("Name"))%>'>
                                                        <img src="../images/Gift/show.png" alt="查看异店库存" title="查看异店库存" />
                                                    </a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td colspan="5" style="line-height: 30px; height: 30px; padding-top: 5px;">
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
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <table id="tbAllStock" border="0" cellpadding="0" cellspacing="1" class="table-style table-hover user_List_txt"
        width="600px" style="display: none">
        <tr>
            <td>
                <script type="text/javascript">
                    ListLoading();
                </script>
            </td>
        </tr>
    </table>
    <input id="shopID" type="hidden" runat="server" />
    </form>
</body>
</html>
