<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsLog.aspx.cs" Inherits="ChainStock.StockManage.GoodsLog" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>


<%@ Register src="../Controls/QuickSearch.ascx" tagname="QuickSearch" tagprefix="uc1" %>
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
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/Module/StockManage/GoodsLog.js" type="text/javascript"></script>
    
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>    
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">

                    <%--打印的次数 --%>
                    <input type="hidden" value="" id="PointNum" runat="server"/>


                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div class="user_List_All">
                        <div class="user_List_top">
                            <table class="tableStyle" style="width: 100%">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <asp:Button ID="btnRptExpenseExcel" runat="server" Text="导   出" UseSubmitBehavior="false"
                                                class="common_Button" OnClick="btnRptExpenseExcel_Click" />
                                            <input id="txtUser" type="hidden" class="input_txt border_radius" runat="server" />
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
                                    <input id="txtQuery" type="text" runat="server" class="input_txt border_radius" title="账单号/商品名称" />
                                </td>
                                <td class="tableStyle_left">
                                    操作人员：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltUserID" runat="server" class="selectWidth">
                                    </select>
                                </td>
                                <td class="tableStyle_left">
                                    所属商家：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltShop" runat="server" class="selectWidth">
                                    </select>
                                    <input  id="HDsltshop" runat="server" type="hidden" />
                                </td>
                                <td class="tableStyle_right">
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    时间：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtStartTime" type="text" runat="server" class="Wdate border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    至&nbsp;&nbsp;
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtEndTime" type="text" runat="server" class="Wdate border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    出入库类型：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltChangeType" class="selectWidth" runat="server">
                                    </select>
                                </td>
                                <td class="tableStyle_right" colspan="2">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnGoodsLogQuery" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnGoodsLogQuery_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater ID="rptGoodsLog" runat="server" OnItemDataBound="rptGoodsLog_ItemDataBound">
                                <HeaderTemplate>
                                    <table class="tableStyle" id="tbGoodsLog" cellspacing="0" cellpadding="2" style="width: 100%;">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                单号
                                            </th>
                                            <th>
                                                出入库方式
                                            </th>
                                            <th>
                                                总价格
                                            </th>
                                            <th>
                                                备注
                                            </th>
                                            <th>
                                                时间
                                            </th>
                                            <th>
                                                变更商家
                                            </th>
                                            <th>
                                                操作商家
                                            </th>
                                            <th>
                                                操作人员
                                            </th>
                                            <th>
                                                操&nbsp;&nbsp;&nbsp;&nbsp;作
                                            </th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td style="width: 50px;">
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <img id="img<%# Eval("ID") %>" alt="" src="../Inc/Style/images/plus.gif" style="vertical-align: text-bottom"
                                                onload="javascript:IsShowPic('<%# Eval("ID") %>','<%# Eval("Type")%>')" />
                                            <a id="a<%# Eval("ID") %>" href="javascript:ShowGoodsLogDetail('<%# Eval("ID") %>','<%# Eval("Type")%>')"
                                                title="展开/收起详情">
                                                <%# Eval("GoodsAccount")%></a>
                                        </td>
                                        <td style="color: Red;">
                                            <%#this.GetType(int.Parse(Eval("Type").ToString()))%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# decimal.Parse(Eval("TotalPrice").ToString()).ToString("0.00")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%#Eval("Remark")%>
                                        </td>
                                        <td>
                                            <%# DateTime.Parse(Eval("CreateTime").ToString()).ToString()%>
                                        </td>
                                        <td>
                                            <%#Eval("ChangeShopName")%>
                                        </td>
                                        <td>
                                            <%#Eval("ShopName")%>
                                        </td>
                                        <td>
                                            <%#Eval("UserName") %>
                                        </td>
                                        <td class="listtd" style="width: 40px;">
                                            <a id="aPrint" runat="server" href='#' onclick='<%# string.Format(" GoodsLogPrint(\"{0}\",\"{1}\")",Eval("ID"),Eval("Type")) %>'>
                                                <img src="../images/Gift/print.png" alt="重打小票" title="重打小票" />
                                            </a><a id="aGoodsRevoke" runat="server" visible='<%# Convert.ToInt32(Eval("Type"))==1%>'
                                                href="#" onclick='<%# string.Format("aGoodsRevoke(\"{0}\",\"{1}\")", Eval("ID"), Eval("ChangeShopID"))%>'>
                                                <img src="../images/Gift/revoke.png" alt="入库撤单" title="入库撤单" />
                                            </a>
                                        </td>
                                    </tr>
                                    <tr style="display: none;" id="data<%# Eval("ID") %>">
                                        <td colspan="15">
                                            <asp:Repeater ID="rptGoodsLogDetail" runat="server">
                                                <HeaderTemplate>
                                                    <div style="padding-left: 50px;">
                                                        <table class="table-style table-hover user_List_txt" cellspacing="0" cellpadding="2"
                                                            style="width: 80%;">
                                                            <tr class="th">
                                                                <th>
                                                                    序号
                                                                </th>
                                                                <th>
                                                                    商品编码
                                                                </th>
                                                                <th>
                                                                    商品名称
                                                                </th>
                                                                <th>
                                                                    商品入库单价
                                                                </th>
                                                                <th>
                                                                    商品出库单价
                                                                </th>
                                                                <th>
                                                                    商品数量
                                                                </th>
                                                            </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="td" id="drDataDetail<%# Eval("GoodsLogDetailID")%>">
                                                        <td>
                                                            <asp:Label ID="lblDetailNumber" runat="server" Text="1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <%# Eval("GoodsCode")%>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%# Eval("Name")%>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <%# decimal.Parse(Eval("GoodsInPrice").ToString()).ToString("0.00")%>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <%# decimal.Parse(Eval("GoodsOutPrice").ToString()).ToString("0.00")%>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <%--<%# Math.Abs(int.Parse(Eval("GoodsNumber").ToString()))%>--%>
                                                            <%# Eval("GoodsNumber").ToString()%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table></div>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
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
    <asp:Label ID="lblPrintTitle" Style="display: none" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lblPrintFoot" Style="display: none" runat="server" Text="Labe2"></asp:Label>
    <%--    <table width="100%" style="margin: 0 0 0 0; vertical-align: top;" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td style="vertical-align: top" id="tdbox">
                <div class="divContentBox">
                    <div class="divContentHead">
                        <asp:Image ID="imgTitle" runat="server" align="absmiddle" />
                        <asp:Label ID="lblFrmTitle" runat="server" Text="窗体标题" CssClass="lblFrmTitle" ForeColor="#3366FF"></asp:Label>
                    </div>
                    <table class="tableStyle" style="width: 100%">
                     
                    </table>
                    <div>
                      
                    </div>
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
                                    ShowCustomInfoSection="Left" CustomInfoClass="paginator" CustomInfoSectionWidth="200px"
                                    SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" Direction="LeftToRight"
                                    HorizontalAlign="Right">
                                </webdiyer:AspNetPager>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>--%>
        <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
