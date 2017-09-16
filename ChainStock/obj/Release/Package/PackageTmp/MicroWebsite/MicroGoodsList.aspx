<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MicroGoodsList.aspx.cs"
    Inherits="ChainStock.MicroWebsite.MicroGoodsList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>

    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>

    <script src="../Scripts/Module/MicroWebsite/MicroGoodsList.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //BindNullList("gvGoodsList");
        })
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

                        <div id="ReportSerch">
                            <div class="user_List_top">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                    class="tableStyle">
                                    <tr style="color: #333333; background-color: #F7F6F3;">
                                        <td class="user_List_styleLeft">
                                            快捷操作：
                                        </td>
                                        <td class="user_List_styleRight">
                                            <div class="user_List_Button">
                                                <asp:Button ID="btnOut" runat="server" Text="导   出" UseSubmitBehavior="false"   class="common_Button" OnClick="btnOut_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                                <tr>
                                    <td class="tableStyle_left">
                                        快速查找：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtQueryGoods" type="text" runat="server" class="border_radius radius2"
                                            title="商品名称/编码/简码" />
                                    </td>
                                    <td class="tableStyle_left">
                                        商品售价：
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
                                    <td class="tableStyle_left">
                                        商品原价：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltMicroSalePriceSymbols" runat="server" style="height: 25px; outline: none;
                                            resize: none;">
                                            <option selected="selected" value="&gt;=">>=</option>
                                            <option value="&lt;="><=</option>
                                            <option value="=">=</option>
                                        </select>
                                        <input id="txtMicroSalePrice" type="text" style="float: none; width: 60px;" runat="server"
                                            class="Wdate border_radius" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        商品分类：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltGoodsClass" runat="server" name="sltGoodsClass" class="selectWidth"></select>
                                    </td>
                                    <td class="tableStyle_left"></td>
                                    <td class="tableStyle_right"></td>
                                    <td class="tableStyle_left"></td>
                                    <td class="tableStyle_right">
                                        <div class="user_List_Button">
                                            <asp:Button ID="btnGoodsListQuery" runat="server" Text="查   询" class="common_Button" OnClick="btnGoodsListQuery_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <table class="table-style table-hover user_List_txt">
                            <thead class="thead">
                                <tr class="th">
                                    <th>
                                        序号
                                    </th>
                                    <th>
                                        商品编号
                                    </th>
                                    <th>
                                        商品名称
                                    </th>
                                    <th>
                                        商品缩略图
                                    </th>
                                    <th>
                                        商品售价
                                    </th>
                                    <th>
                                        商品原价
                                    </th>
                                    <th>
                                        积分数量
                                    </th>
                                    <th>
                                        所属分类
                                    </th>
                                    <%--<asp:Literal runat="server" ID="ltlHeader"></asp:Literal>--%>
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
                                            <%--<asp:Literal runat="server" ID="ltlGoodsID" runat="server" Text='<%# Eval("GoodsID")%>' Visible="false"></asp:Literal>--%>
                                        </td>
                                        <td>
                                            <%# Eval("MicroGoodsCode")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("MicroGoodsName")%>
                                        </td>
                                        <td style="text-align: center">
                                            <span onclick='ShowPic("<%#Eval("MicroGoodsPicture") %>")' style="cursor: pointer;">
                                                <img id="imgPhoto" alt="" runat="server" src='<%#Eval("MicroGoodsPicture") %>'
                                                    style="width: 30px; height: 30px;" />
                                            </span>
                                        </td>
                                        <td>
                                            <%# Eval("MicroPrice","{0:0.00}")%>
                                        </td>
                                        <td>
                                            <%# Eval("MicroSalePrice","{0:0.00}")%>
                                        </td>
                                        <td style="text-align: right;">
                                            <%# Eval("MicroPoint")%>
                                        </td>
                                        <td>
                                            <%# Eval("MicroGoodsClassName")%>
                                        </td>
                                        <%--<asp:Literal runat="server" ID="ltlHtml"></asp:Literal>--%>
                                        <td class="listtd" style="width: 60px;">
                                            <a href='<%# "MicroGoodsInfo.aspx?PID=128&MicroGoodsID="+Eval("MicroGoodsID")+"&MicroGoodsShopID="+Eval("MicroGoodsShopID")%>'
                                                runat="server" id="hyEdit">
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" /></a>

                                            <a href="#" onclick='<%# string.Format(" DeleteGoods(\"{0}\",\"{1}\")",Eval("MicroGoodsID"),Eval("MicroGoodsName")) %>'
                                                id="hyDel" runat="server">
                                                <img src="../images/Gift/del.png" alt="删除" title="删除" /></a>
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
                <div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
