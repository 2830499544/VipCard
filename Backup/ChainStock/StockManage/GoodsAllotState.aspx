<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsAllotState.aspx.cs"
    Inherits="ChainStock.StockManage.GoodsAllotState" %>

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
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/StockManage/GoodsAllotState.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <%--打印的次数 --%>
                    <input type="hidden" value="" id="PointNum" runat="server" />
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
                                        调拨类型：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <input type="button" value="申请调入" id="Allot_In" style="width: 100px; height: 24px;
                                                text-align: center;" class="common_Button button_style" />
                                            <input type="button" value="申请调出" id="Allot_Out" style="width: 100px; height: 24px;
                                                text-align: center;" class="common_Button button_style" />
                                            <label style="float: right; margin-right: 20px; margin-top: 4px;">
                                                <span style="color: red;">注意：调拨时请先选择调拨类型,然后再进行调拨操作！</span></label>
                                            <input type="hidden" id="HiddenShopID" value="" runat="server" />
                                            <input type="hidden" id="HiddenType" value="" runat="server" />
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
                                    审核状态：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="SelAllotZT" runat="server" class="selectWidth">
                                        <option value="0">===请选择===</option>
                                        <option value="1">申请状态</option>
                                        <option value="2">发货状态</option>
                                        <option value="3">已收货状态</option>
                                        <option value="4">已撤单状态</option>
                                    </select>
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
                                                调拨单号
                                            </th>
                                            <th>
                                                调出商家
                                            </th>
                                            <th>
                                                调入商家
                                            </th>
                                            <th>
                                                调拨总数
                                            </th>
                                            <th>
                                                创建时间
                                            </th>
                                            <th>
                                                调拨状态
                                            </th>
                                            <th>
                                                操作人员
                                            </th>
                                            <th>
                                                调拨备注
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
                                            <img id="img<%# Eval("AllotID") %>" alt="" src="../Inc/Style/images/plus.gif"
                                                style="vertical-align: text-bottom" onload="javascript:IsShowPic('<%# Eval("AllotID") %>','<%# Eval("AllotState")%>')" />
                                            <a id="a<%# Eval("AllotID") %>" href="javascript:ShowGoodsLogDetail('<%# Eval("AllotID") %>','<%# Eval("AllotState")%>')"
                                                title="展开/收起详情">
                                                <%# Eval("AllotAccount")%></a>
                                        </td>
                                        <td style="color: Red;">
                                            <%#Eval("AllotOutShopName")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%#Eval("AllotInShopName")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%#Eval("AllotTotalNumber")%>
                                        </td>
                                        <td>
                                            <%# DateTime.Parse(Eval("AllotCreateTime").ToString()).ToString()%>
                                        </td>
                                        <td>
                                            <%# this.GetStateType(int.Parse(Eval("AllotState").ToString()))%>
                                        </td>
                                        <td>
                                            <%#Eval("UserName")%>
                                        </td>
                                        <td>
                                            <%#Eval("AllotRemark")%>
                                        </td>
                                        <td class="listtd" style="width: 90px;">
                                            <span id="dsa" runat="server" visible='<%#bool.Parse((int.Parse(Eval("AllotState").ToString())==1).ToString())%>'>
                                                <a id="aPrint" runat="server" href='#' onclick='<%# string.Format(" AllotEdit(\"{0}\",\"{1}\")",Eval("AllotID"),Eval("AllotState")) %>'>
                                                    <img src="../images/Gift/eit.png" alt="修改订单" title="修改订单" />
                                                </a></span>
                                            <asp:Panel ID="PLfahuo" runat="server">
                                                <a id="a1" runat="server" href='#' onclick='<%# string.Format(" AllotFahuo(\"{0}\",\"{1}\")",Eval("AllotID"),Eval("AllotState")) %>'>
                                                    <img src="../images/Gift/abolish.png" alt="确认发货" title="确认发货" />
                                                </a>
                                            </asp:Panel>
                                            <asp:Panel ID="PLshouhuo" runat="server">
                                                <a id="a2" runat="server" href='#' onclick='<%# string.Format(" AllotShouhuo(\"{0}\",\"{1}\")",Eval("AllotID"),Eval("AllotState")) %>'>
                                                    <img src="../images/Gift/isok.png" alt="确认收货" title="确认收货" />
                                                </a>
                                            </asp:Panel>
                                            <asp:Panel ID="PanCedans" runat="server">
                                                <a id="a3" runat="server" href='#' onclick='<%# string.Format(" AllotCedan(\"{0}\",\"{1}\")",Eval("AllotID"),Eval("AllotState")) %>'>
                                                    <img src="../images/Gift/revoke.png" alt="撤销订单" title="撤销订单" /></a>
                                               </asp:Panel>

                                        </td>
                                    </tr>
                                    <tr style="display: none;" id="data<%# Eval("AllotID") %>">
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
                                                                    调拨数量
                                                                </th>
                                                            </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="td" id="drDataDetail<%# Eval("AllotDetailID")%>">
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
                                                            <%# Eval("AllotDetailNumber").ToString()%>
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
    </form>
</body>
</html>
