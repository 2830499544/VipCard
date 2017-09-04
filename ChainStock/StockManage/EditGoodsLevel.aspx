<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditGoodsLevel.aspx.cs"
    Inherits="ChainStock.StockManage.EditGoodsLevel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/StockManage/EditGoodLevel.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmGoodsClass" runat="server">
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <%-- <div id="divSyncShopSelectPanel" style="display:none">
            <table class="table-style table-hover user_List_txt">
                <asp:Repeater runat="server" ID="rptSyncShopList">
                    <HeaderTemplate>
                        <thead class="thead">
                            <tr class="th">
                                <th style="width:60px">
                                    选择
                                </th>
                                <th>
                                    商家名称
                                </th>
                            </tr>
                        </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="td">
                            <td style="text-align: center; padding-left: 2px;">
                                <input type="checkbox" name="SyncShop" value="<%# Eval("ShopID") %>" />
                            </td>
                            <td style="text-align: left; padding-left: 10px;">
                                <%# Eval("ShopName") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td colspan="2" style="text-align: center; height: 36px">
                        <input type="button" id="btnShareShopOK" class="common_Button" style="float:inherit" value="确  认" />
                    </td>
                </tr>
            </table>
        </div>--%>
        <div id="divSyncShopSelectPanel" style="display: none">
            <table class="table-style table-hover user_List_txt">
                <tr class="th">
                    <th style="width: 60px">
                        选择
                    </th>
                    <th>
                        商家名称
                    </th>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="height: 380px; width: 280px; overflow: auto;">
                            <table class="table-style table-hover user_List_txt">
                                <asp:Repeater runat="server" ID="rptSyncShopList">
                                    <ItemTemplate>
                                        <tr class="td">
                                            <td style="text-align: center; width: 58px; padding-left: 0px">
                                                <input type="checkbox" name="SyncShop" value="<%# Eval("ShopID") %>" />
                                            </td>
                                            <td style="text-align: center; width: 58px; padding-left: 5px">
                                                <%# Eval("ShopName") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; height: 36px">
                        <input type="button" id="btnShareShopOK" class="common_Button" style="float: inherit"
                            value="确  认" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="user_regist_Allleft" id="divAddLevel">
            <div class="user_regist_left">
                <div class="user_regist_infor" style="text-align: left">
                    商品分类设置
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle"
                    id="divGoodsClassAddOreEdit">
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>所属分类：
                        </td>
                        <td class="tableStyle_right">
                            <select id="sltGoodsClass" runat="server" class="selectWidth" style="float: left">
                            </select>
                            <label id="lblShowSync" runat="server" class="lbsetCk" style="vertical-align: middle;
                                float: left;">
                                &nbsp;&nbsp;
                                <input id="chkSyncOtherShop" type="checkbox" runat="server" onclick="javascript:SelectAllShop()" />
                                同步到全部商家 &nbsp;</label>
                            <label id="lblShowSyncPartial" runat="server" class="lbsetCk" style="vertical-align: middle;
                                float: left;">
                                &nbsp;&nbsp;
                                <input id="chkSyncPartialShop" type="checkbox" runat="server" onclick="javascript:SelectPartailShop()" />
                                同步到部分商家 &nbsp;</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>创建商家：
                        </td>
                        <td class="tableStyle_right">
                            <select id="sltShop" runat="server" class="selectWidth" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>类别名称：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" id="txtClassName" runat="server" class="border_radius" />
                            <input type="hidden" id="txtClassID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            备注：
                        </td>
                        <td class="tableStyle_right">
                            <textarea id="txtGoodsClassRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                outline: none; resize: none;"></textarea>
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor" style="text-align: left">
                    商品分类等级折扣设置
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            等级名称
                        </td>
                        <td class="tableStyle_right">
                            商品消费折扣设置
                        </td>
                        <td class="tableStyle_left">
                        </td>
                        <td class="tableStyle_right">
                            商品消费积分设置
                        </td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptLevelList">
                        <ItemTemplate>
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span><%# Eval("LevelName") %>
                                    <asp:Literal runat="server" ID="ltLevelID" Text='<%# Eval("LevelID") %>' Visible="false"></asp:Literal>
                                    <asp:Literal runat="server" ID="ltClassDiscountID" Text="" Visible="true"></asp:Literal>
                                </td>
                                <td class="tableStyle_right">
                                    <label style="vertical-align: text-bottom;">
                                        <input runat="server" value='<%# GetDiscountPercent(Eval("LevelDiscountPercent").ToString())%>'
                                            gm="txtMyDiscountPercent" myname='<%# Eval("LevelName")%>' type="text" class="input_txt border_radius"
                                            id='txtDiscountPercent' />
                                        <label style="vertical-align: middle;">
                                            % <a href="#" title='即：会员在此等级时，商品消费所能享受的消费折扣'>参数说明</a>
                                        </label>
                                    </label>
                                </td>
                                <td class="tableStyle_left">
                                </td>
                                <td class="tableStyle_right">
                                    <label style="vertical-align: text-bottom;">
                                        <input runat="server" value='<%# Eval("LevelPointPercent").ToString()%>' gm="txtMyPointPercent"
                                            myname='<%# Eval("LevelName")%>' type="text" class="input_txt border_radius"
                                            id="txtPointPercent" />
                                        <label style="vertical-align: middle;">
                                            <a href="#" onclick="btnClassSave();" title='即：会员在此等级时，商品消费时自动兑换的积分比率，如：消费10元得1积分|消费20元得1积分|消费50元得1积分。' style="margin-left: 13px;">
                                                参数说明</a>
                                        </label>
                                    </label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 36px">
                            <asp:Button runat="server" ID="btnGoodsClassSave" class="buttonColor" OnClientClick="return btnClassSave();"
                                Text="保   存" OnClick="btnGoodsClassSave_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
