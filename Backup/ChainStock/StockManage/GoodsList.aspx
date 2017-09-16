<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsList.aspx.cs" Inherits="ChainStock.StockManage.GoodsList" %>

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

    <script src="../Scripts/Module/StockManage/GoodsList.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            document.onkeydown = function (event) {
                e = event ? event : (window.event ? window.event : null);
                if (e.keyCode == 13) {
                    return false;
                }
            };
        });
    </script>
</head>
<body>
    <form id="frmGoodsList" runat="server">
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
                                       
                                                    <asp:Button ID="btnCopy" runat="server" Text="同步到所有店铺" Width="120px"  UseSubmitBehavior="false"   class="common_Button" OnClick="btnCopy_Click" />
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
                                    <input id="txtQueryGoods" type="text" runat="server" class="border_radius radius2"
                                        title="商品名称/编码/简码" />
                                </td>
                                <td class="tableStyle_left">
                                    自定义属性：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltCustomField" runat="server" class="selectWidth">
                                    </select>
                                </td>
                                <td class="tableStyle_left">
                                    属性值：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtCustomField" type="text" runat="server" class="border_radius radius2" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    商品分类：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltGoodsClass" name="sltGoodsClass" class="selectWidth" />
                                    <input id="txtGoodsClass" runat="server" type="hidden" />
                                </td>
                                <td class="tableStyle_left">
                                    商品类型：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltGoodsType" runat="server" class="selectWidth">
                                        <option selected="selected" value="">===== 请选择 =====</option>
                                        <option value="0">普通商品</option>
                                        <option value="1">服务商品</option>
                                    </select>
                                </td>
                                <td class="tableStyle_left">
                                    商品单价：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltGoodsPriceSymbols" runat="server" style="height: 25px; outline: none;
                                        resize: none;">
                                        <option selected="selected" value="&gt;=">>=</option>
                                        <option value="&lt;="><=</option>
                                        <option value="=">=</option>
                                    </select>
                                    <input id="txtGoodsPrice" type="text" style="float: none; width: 60px;" runat="server"
                                        class="Wdate border_radius" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    所属商家：
                                </td>
                                <td class="tableStyle_right">
                                   <%-- <select id="sltShopList" runat="server" class="selectWidth"  />--%>

                                      <select id="sltShop" runat="server" class="selectWidth" autocomplete="off">
                                        </select>
                                  <input  id="HDsltshop" runat="server" type="hidden" />
                                                 
                                </td>
                                <td colspan="3">
                                </td>
                                <td class="tableStyle_right" colspan="4">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnGoodsListQuery" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnGoodsListQuery_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <thead class="thead">
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
                                        商品简码
                                    </th>
                                    <th>
                                        商品类型
                                    </th>
                                    <th>
                                        计量单位
                                    </th>
                                    <th>
                                        商品单价
                                    </th>
                                    <th>
                                        所属分类
                                    </th>
                                    <asp:Literal runat="server" ID="ltlHeader"></asp:Literal>
                                    <th>
                                        操作
                                    </th>
                                </tr>
                            </thead>
                            <asp:Repeater runat="server" ID="gvGoodsList">
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                            <asp:Literal runat="server" ID="ltlGoodsID" runat="server" Text='<%# Eval("GoodsID")%>'
                                                Visible="false"></asp:Literal>
                                        </td>
                                        <td>
                                            <%# Eval("GoodsCode")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("Name")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("NameCode")%>
                                        </td>
                                        <td>
                                            <%# Eval("GoodsType").ToString()=="0"?"普通商品":"<span style='color:red;'>服务商品</span>"%>
                                        </td>
                                        <td>
                                            <%# Eval("Unit")%>
                                        </td>
                                        <td style="text-align: right;">
                                            <%# decimal.Parse(Eval("Price").ToString()).ToString("0.00")%>
                                        </td>
                                        <td>
                                            <%# Eval("ClassName")%>
                                        </td>
                                        <asp:Literal runat="server" ID="ltlHtml"></asp:Literal>
                                        <td class="listtd" style="width: 90px;">
                                            <a href='<%# "GoodsAdd.aspx?PID=61&GoodsID="+Eval("GoodsID")+"&ShopID="+Eval("ShopID")%>'
                                                runat="server" id="hyEdit">
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" />
                                            </a>
                                      <%-- <a href="#" onclick='<%# string.Format(" SyncGoods(\"{0}\",\"{1}\")",Eval("GoodsID"),CurShopID) %>'
                                                id="hySync" runat="server">
                                                <img src="../images/Gift/system.png" alt="同步到所有商家" title="同步到所有商家" />
                                            </a>--%>
                                            <a href="#" onclick='<%# string.Format(" DeleteGoods(\"{0}\",\"{1}\")",Eval("GoodsID"),CurShopID) %>'
                                                id="hyDel" runat="server" >
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
    <uc1:QuickSearch ID="QuickSearch2" runat="server" />
    </form>
</body>
</html>
