<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsAdd.aspx.cs" Inherits="ChainStock.StockManage.GoodsAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商品登记</title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/StockManage/GoodsInfo.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    </script>
</head>
<body>
    <form id="frmGoodsAdd" runat="server">
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
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
                        <div style="height: 380px; width:280px; overflow:scroll;">
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
        <div class="user_regist_Allleft">
            <div class="user_regist_left">
                <div class="user_regist_infor">
                    商品信息
                </div>
                <table width="720" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                    class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>商品编码：
                        </td>
                        <td colspan="3" class="tableStyle_right">
                            <input id="txtGoodsCode" type="text" class="border_radius" runat="server" title="商品编号是唯一的,有条件的商家最好使用商品条码编号"
                                maxlength="25" onclick="document.frmGoodsAdd.txtGoodsCode.select()" />
                            <input id="txtGoodsID" type="hidden" runat="server" />
                            <input id="txtCode" type="hidden" runat="server" />
                            <%--                            <label id="lblAutoGoodsCode" runat="server" style="vertical-align: text-bottom;">
                                &nbsp;&nbsp;
                                <label class="lbsetCk" style="vertical-align: middle;">
                                    <input id="chkAutoGoodsCode" type="checkbox" />
                                    自动创建商品编码</label>
                                
                            </label>--%>
                            <label for="chkService" style="vertical-align: text-bottom;">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <label class="lbsetCk" style="vertical-align: middle;">
                                    <input id="chkService" type="checkbox" runat="server" />
                                    服务项目</label></label>
                            <label for="chkSyncOtherShop" style="vertical-align: text-bottom;">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <label id="lblShowSync" runat="server" class="lbsetCk" style="vertical-align: middle;">
                                    <input id="chkSyncOtherShop" type="checkbox" runat="server" onclick="javascript:SelectAllShop()" />
                                    同步到所有商家</label></label>
                            <label for="chkSyncPartialShop" style="vertical-align: text-bottom;">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <label id="lblShowSyncPartial" runat="server" class="lbsetCk" style="vertical-align: middle;">
                                    <input id="chkSyncPartialShop" type="checkbox" runat="server" onclick="javascript:SelectPartailShop()" />
                                    同步到部分商家</label></label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>商品名称：
                        </td>
                        <td class="tableStyle_right" style="width: 270px">
                            <input id="txtGoodsName" type="text" class="border_radius" runat="server" title="请输入商品名称"
                                maxlength="20" />
                        </td>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>商品简码：
                        </td>
                        <td class="tableStyle_right" style="width: 270px">
                            <input id="txtGoodsNameCode" type="text" class="border_radius" runat="server" title="请输入商品名称"
                                maxlength="20" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            商品分类：
                        </td>
                        <td class="tableStyle_right">
                            <select id="sltGoodsClass" runat="server" class="selectWidth">
                            </select>
                        </td>
                        <td class="tableStyle_left">
                            计量单位：
                        </td>
                        <td class="tableStyle_right">
                            <select id="sltjldw" runat="server" class="selectWidth">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>零售单价：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtGoodsPrice" type="text" runat="server" class="border_radius" />
                        </td>
                        <td class="tableStyle_left">
                            参考进价：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtGoodsBidPrice" type="text" runat="server" class="border_radius" title="商品入库时使用参考进价" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            特价折扣：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtGoodsSalePercent" type="text" runat="server" class="border_radius"
                                title="输入特价折扣，则商品消费时商品折扣按照（商品拥有的特价折扣和会员拥有的登记折扣两者较低的那个）计算,输入1则此商品不打折,输入0，则按照会员折扣计算"
                                value="0" />
                        </td>
                        <td class="tableStyle_left">
                            最低折扣：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtGoodsMinPercent" type="text" runat="server" class="border_radius" title="输入最低折扣，则商品消费时商品折扣按照（商品拥有的特价折扣和会员拥有的登记折扣两者较高的那个）计算,输入1则此商品不打折,输入0，则按照会员折扣计算。保价折扣"
                                value="0" />
                        </td>
                    </tr>
                    <tr id="trCommission" runat="server">

                        <td class="tableStyle_left">
                            提成类型：
                        </td>
                        <td class="tableStyle_right">
                            <select id="sltCommissionType" runat="server" class="selectWidth">
                                <option selected="selected" value="0">===== 请选择 =====</option>
                                <option value="1">按商品固定比例提成</option>
                                <option value="2">按商品固定金额提成</option>
                            </select>
                        </td>
                        <td class="tableStyle_left">
                            <span id="spCommission">提成系数：</span>
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtCommissionNumber" type="text" runat="server" class="border_radius"
                                value="0" title="通过商品的提成类型，输入提成的规则。" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            所属商家：
                        </td>
                        <td class="tableStyle_right">
                            <select id="sltShopList" runat="server" class="selectWidth">
                            </select>
                            <input id="txtShopID" runat="server" type="hidden" />
                        </td>
                        <td class="tableStyle_left">
                            积分数量：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtGoodsPoint" type="text" runat="server" class="border_radius" title="输入积分数量，则商品消费时获得的积分按照商品拥有的积分计算；如果积分数量为0，按照商品的折后价格*会员的积分比例计算积分，如果积分数量为空，则该商品不积分"
                                value="0" />
                        </td>
                    </tr>
                    <tr id="trGoodsNumber" runat="server" style="display: none">
                        <td class="tableStyle_left">
                            商品库存：
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <input id="txtGoodsNumber" type="text" runat="server" class="border_radius" title="商品的库存数量" />
                            <input id="hdShopID" type="hidden" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            商品简介：
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <textarea id="txtGoodsRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                outline: none; resize: none;" title="请输入商品简介"></textarea>
                        </td>
                    </tr>
                </table>
                <div class="user_regist_left">
                    <div class="user_regist_infor">
                        商品自定义属性
                    </div>
                    <table width="720" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                        class="tableStyle">
                        <tbody id="tbCustomField" runat="server">
                            <tr>
                                <td colspan="4" style="padding: 5px;">
                                    正在加载自定义属性，请稍候……
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div style="text-align: center; height: 36px">
                        <input id="btnGoodsSave" type="button" class="buttonColor" value="保　存" />
                        <input id="btnGoodsReset" type="button" class="buttonRest" value="重　置" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
