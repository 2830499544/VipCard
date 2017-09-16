<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditLevel.aspx.cs" Inherits="ChainStock.Member.EditLevel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" /> 
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Mem/EditLevel.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmSetLevel" runat="server">
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_regist_Allleft" id="divAddLevel">
            <div class="user_regist_left">
                <div class="user_regist_infor" style="text-align: left">
                    等级信息
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>等级名称：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtLevelName" type="text" class="border_radius" runat="server" />
                            <input id="txtLevelID" type="hidden" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>所需积分：
                        </td>
                        <td class="tableStyle_right">
                            <label style="vertical-align: text-bottom;">
                                <input id="txtLevelPoint" type="text" class="border_radius" runat="server"  />
                                <label style="vertical-align: middle;">
                                    <a href="#" title='即：此等级的最低积分标准' style="margin-left: 13px;">参数说明</a>
                                </label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            升级机制：
                        </td>
                        <td class="tableStyle_right">
                            <label class="lbsetCk" style="vertical-align: middle;">
                                <input id="chkLevellLock" type="checkbox" runat="server" />
                                等级锁定</label>&nbsp;&nbsp;&nbsp;<a href="#" title='即：启用等级锁定则此会员等级不受自动升级机制影响,保持等级不变 适用于 只积分不打折 类型会员等级'
                                    style="height: 34px">参数说明</a>
                        </td>
                    </tr>
                </table>
                 <div class="user_regist_infor" style="text-align: left">
                    会员充值等级信息
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                        </td>
                        <td class="tableStyle_right">
                            会员充值积分设置
                        </td>                        
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>会员充值：
                        </td>
                        <td class="tableStyle_right">
                            <label style="vertical-align: text-bottom;">
                                <input id="txtLevelRechargePointRate" type="text" class="border_radius" runat="server" />
                                <label style="vertical-align: middle;">
                                    <a href="#" title='即：会员在此等级时，会员充值时自动兑换的积分比率，如：充值10元得1积分|充值20元得1积分|充值50元得1积分。'>参数说明</a>
                                </label>
                            </label>
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor" style="text-align: left">
                    快速消费会员等级信息
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                        </td>
                        <td class="tableStyle_right">
                            快速消费折扣设置
                        </td>
                        <td class="tableStyle_left">
                        </td>
                        <td class="tableStyle_right">
                            快速消费积分设置
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>快速消费：
                        </td>
                        <td class="tableStyle_right">
                            <label style="vertical-align: text-bottom;">
                                <input id="txtLevelDiscountPercent" type="text" class="border_radius" runat="server" />
                                <label style="vertical-align: middle;">
                                    % <a href="#" title='即：会员在此等级时，快速消费所能享受的消费折扣'>参数说明</a>
                                </label>
                            </label>
                        </td>
                        <td class="tableStyle_left">
                        </td>
                        <td class="tableStyle_right">
                            <label style="vertical-align: text-bottom;">
                                <input id="txtLevelPointPercent" type="text" class="border_radius" runat="server" />
                                <label style="vertical-align: middle;">
                                    <a href="#" title='即：会员在此等级时，快速消费时自动兑换的积分比率，如：消费10元得1积分|消费20元得1积分|消费50元得1积分。' style="margin-left: 13px;">
                                        参数说明</a>
                                </label>
                            </label>
                        </td>
                    </tr>
                </table>
                <div id="divEnableGoodsSet" runat="server">
                <div class="user_regist_infor" style="text-align: left">
                    商品类别对应商品消费折扣
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
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
                    <asp:Repeater runat="server" ID="rptShopClassLevel">
                        <ItemTemplate>
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span><%# Eval("ClassName") %>：
                                    <asp:Literal runat="server" ID="ltClassID" Text='<%# Eval("ClassID") %>' Visible="false"></asp:Literal>
                                    <asp:Literal runat="server" ID="ltClassDiscountID" Text="" Visible="false"></asp:Literal>
                                </td>
                                <td class="tableStyle_right">
                                    <label style="vertical-align: text-bottom;">
                                        <input runat="server" gm="txtMyDiscountPercent" myname='<%# Eval("ClassName")%>'
                                            type="text" class="border_radius" id='txtDiscountPercent' />
                                        <label style="vertical-align: middle;">
                                            % <a href="#" title='即：会员在此等级时，商品消费所能享受的消费折扣'>参数说明</a>
                                        </label>
                                    </label>
                                </td>
                                <td class="tableStyle_left">
                                </td>
                                <td class="tableStyle_right">
                                    <label style="vertical-align: text-bottom;">
                                        <input runat="server" gm="txtMyPointPercent" myname='<%# Eval("ClassName")%>' type="text"
                                            class="border_radius" id="txtPointPercent" />
                                        <label style="vertical-align: middle;">
                                            <a href="#" title='即：会员在此等级时，商品消费时自动兑换的积分比率，如：消费10元得1积分|消费20元得1积分|消费50元得1积分。' style="margin-left: 13px;">
                                                参数说明</a>
                                        </label>
                                    </label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td colspan="4" style="text-align: center; height: 36px; border:none">
                            <asp:Button runat="server" ID="btnSetLevel" OnClientClick="return BtnSetLevel();"
                                Text="保   存" class="buttonColor" OnClick="btnSetLevel_Click" />
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>
    </form>
</body>
</html>
