<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExtraParameter.aspx.cs"
    Inherits="ChainStock.ExtraService.ExtraParameter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/ExtraService/ExtraParameter.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Tab.js" type="text/javascript"></script>
    <script type="text/jscript">
        $(document).ready(function () {
            var exchangeTab = new CommonTab("RemindTab", "", function (index) { });
        })
    </script>
    <style type="text/css">
        .tableStyle_left
        {
            width: 200px;
            text-align: left;
            padding-left: 4px;
        }
    </style>
</head>
<body>
    <form id="frmExtraParameter" runat="server">
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
                        <table class="table-style table-hover user_List_txt">
                            <div>
                                <div class="tabBox" id="RemindTab" style="width: 90%">
                                    <ul class="tab">
                                        <li id="tab0" class="on">短信参数设置</li>
                                        <li id="tab1">来电参数设置</li>
                                        <li id="tab2" style="display: none">二维码参数设置</li>
                                        <li id="tab3">信息发送参数设置</li>
                                    </ul>
                                </div>
                                <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 90%;" id="MainContent0">
                                    <%--   <tr>
                                        <th style="text-align: left; padding-left: 10px; font-weight: bold; height: 35px;
                                            font-size: 14px; text-align: center;" class="th" colspan="4">
                                            短信参数设置
                                        </th>
                                    </tr>--%>
                                    <tr>
                                        <td class="tableStyle_left" style="width: 210px">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="chkSms" runat="server" type="checkbox" onclick="IsMoneySMS()" />
                                                    启用系统通知短信功能
                                                </label>
                                            </label>
                                            <%--    <asp:CheckBox ID="chkSms" runat="server" Text="启用系统短信功能" onclick="IsMoneySMS()" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            如具备短信服务，可启用该选项进行相关系统发送通知短信功能
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="chkMoneySms" runat="server" type="checkbox" />
                                                    启用系统通知短信自动发送功能
                                                </label>
                                            </label>
                                            <%--     <asp:CheckBox ID="chkMoneySms" runat="server" Text="启用系统短信自动发送功能" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            启动该选项则在会员账户信息变动时自动为会员发送通知短信，如[消费][充值][积分变动]
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="chkIsSmsShopName" runat="server" type="checkbox" />
                                                    启用统一所有商家短信后缀名
                                                </label>
                                            </label>
                                            <%--   <asp:CheckBox ID="chkIsSmsShopName" runat="server" Text="启用统一所有商家短信后缀名" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            不启用统一所有商家短信后缀名时，发送信默认用各个商家的短信后缀名
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            统一商家短信后缀名：
                                        </td>
                                        <td class="tableStyle_right">
                                            <asp:TextBox ID="txtSmsShopName" CssClass="border_radius" runat="server" Style="width: 450px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            通知短信序列号：
                                        </td>
                                        <td class="tableStyle_right">
                                            <label style="vertical-align: text-bottom;">
                                                <asp:TextBox ID="txtNotificationSMS" CssClass="border_radius" runat="server" Width="150px"></asp:TextBox>
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;如具备短信服务且启用系统短信功能，则在此处输入短信的序列号
                                                </label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            通知序列号密码：
                                        </td>
                                        <td class="tableStyle_right">
                                            <label style="vertical-align: text-bottom;">
                                                <asp:Label ID="lblNotificationSMSPwd" runat="server" Style="display: none;" />
                                                <asp:TextBox ID="txtNotificationSMSPwd" CssClass="border_radius" runat="server" TextMode="Password"
                                                    Width="150px" MaxLength="20">*</asp:TextBox>
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;如具备短信服务且启用系统短信功能，则在此处输入短信的序列号密码
                                                </label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="tableStyle_left">
                                            <a href="#" onclick="GetSmsBalance(false)">通知短信余额查询</a>&nbsp;&nbsp; <font color="red">
                                                <asp:Label runat="server" ID="lblNotificationSmsOverNumber" Text=""></asp:Label></font>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left" style="width: 210px">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="chkMarketingSMS" runat="server" type="checkbox" onclick="IsMarketingSMS()" />
                                                    启用营销短信功能
                                                </label>
                                            </label>
                                            <%--    <asp:CheckBox ID="chkSms" runat="server" Text="启用系统短信功能" onclick="IsMoneySMS()" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            如具备营销短信服务，可启用该选项在‘增值服务’--->‘发送短信’发送营销短信
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            营销短信序列号：
                                        </td>
                                        <td class="tableStyle_right">
                                            <label style="vertical-align: text-bottom;">
                                                <asp:TextBox ID="txtMarketingSMS" CssClass="border_radius" runat="server" Width="150px"></asp:TextBox>
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;如具备短信服务且启用系统短信功能，则在此处输入短信的序列号
                                                </label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            营销序列号密码：
                                        </td>
                                        <td class="tableStyle_right">
                                            <label style="vertical-align: text-bottom;">
                                                <asp:Label ID="lblMarketingSMSPwd" runat="server" Style="display: none;" />
                                                <asp:TextBox ID="txtMarketingSMSPwd" CssClass="border_radius" runat="server" TextMode="Password"
                                                    Width="150px" MaxLength="20">*</asp:TextBox>
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;如具备短信服务且启用系统短信功能，则在此处输入短信的序列号密码
                                                </label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="tableStyle_left">
                                            <a href="#" onclick="GetSmsBalance(true)">营销短信余额查询</a>&nbsp;&nbsp; <font color="red">
                                                <asp:Label runat="server" ID="lblMarketingSMSOverNumber" Text=""></asp:Label></font>
                                        </td>
                                    </tr>
                                </table>
                                <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 90%; display: none;"
                                    id="MainContent1">
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="chkTel" runat="server" type="checkbox" onclick="IsTelNoMember()" />
                                                    启用来电提醒功能
                                                </label>
                                            </label>
                                            <%--  <asp:CheckBox ID="chkTel" runat="server" Text="启用来电提醒功能" onclick="IsTelNoMember()" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            如具备来电弹屏设备，可启用该选项进行相关来电弹屏提醒功能
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="chkTelNoMember" runat="server" type="checkbox" />
                                                    启用非会员来电提醒功能
                                                </label>
                                            </label>
                                            <%--   <asp:CheckBox ID="chkTelNoMember" runat="server" Text="启用非会员来电提醒功能" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            如具备来电弹屏设备且启用来电提醒功能，可启用该选项使来电弹屏接收非会员信息
                                        </td>
                                    </tr>
                                </table>
                                <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 90%; display: none;"
                                    id="MainContent2">
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="chkMMS" runat="server" type="checkbox" onclick="IsMMS()" />
                                                    启用系统彩信功能
                                                </label>
                                            </label>
                                            <%--   <asp:CheckBox ID="chkMMS" runat="server" Text="启用系统彩信功能" onclick="IsMMS()" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            如具备彩信服务，可启用该选项进行相关系统发送彩信功能
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            彩信序列号：
                                        </td>
                                        <td class="tableStyle_right">
                                            <label style="vertical-align: text-bottom;">
                                                <asp:TextBox ID="txtMMSSeries" runat="server" CssClass="border_radius" Width="150px"></asp:TextBox>
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;如具备彩信服务且启用系统彩信功能，则在此处输入彩信的序列号
                                                </label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            序列号密码：
                                        </td>
                                        <td class="tableStyle_right">
                                            <label style="vertical-align: text-bottom;">
                                                <asp:TextBox ID="txtMMSSeriesPwd" runat="server" CssClass="border_radius" TextMode="Password"
                                                    Width="150px" MaxLength="20"></asp:TextBox>
                                                <asp:Label ID="lblMMSSerialPwd" runat="server" Style="display: none;" />
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;如具备彩信服务且启用系统彩信功能，则在此处输入彩信的序列号密码
                                                </label>
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="tableStyle_left">
                                            <a href="#">彩信套餐资费说明</a>&nbsp;&nbsp; <a href="#" onclick="GetMMSBalance()">彩信余额查询</a>&nbsp;&nbsp;
                                            <font color="red">
                                                <asp:Label runat="server" ID="lblMMSOverNumber" Text=""></asp:Label></font>
                                        </td>
                                    </tr>
                                </table>
                                <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 90%; display: none;"
                                    id="MainContent3">
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="ckbIsAutoSendSMSByMemRegister" runat="server" type="checkbox" />
                                                    启用会员登记发送短信
                                                </label>
                                            </label>
                                            <%--  <asp:CheckBox ID="ckbIsAutoSendSMSByMemRegister" runat="server" Text="启用会员登记发送短信" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            启动该选项则在会员登记时自动发送短信
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="ckbIsAutoSendMMSByMemRegister" runat="server" type="checkbox" />
                                                    启用会员登记发送二维码
                                                </label>
                                            </label>
                                            <%--    <asp:CheckBox ID="ckbIsAutoSendMMSByMemRegister" runat="server" Text="启用会员登记发送二维码" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            启动该选项则在会员登记时自动发送二维码
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="ckbIsAutoSendSMSByMemRecharge" runat="server" type="checkbox" />
                                                    启用会员充值发送短信
                                                </label>
                                            </label>
                                            <%-- <asp:CheckBox ID="ckbIsAutoSendSMSByMemRecharge" runat="server" Text="启用会员充值发送短信" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            启动该选项则在会员充值时自动发送短信
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="ckbIsAutoSendSMSByMemWithdraw" runat="server" type="checkbox" />
                                                    启用会员账户提现发送短信
                                                </label>
                                            </label>
                                            <%--  <asp:CheckBox ID="ckbIsAutoSendSMSByMemWithdraw" runat="server" Text="启用会员账户提现发送短信" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            启动该选项则在会员账户提现时自动发送短信
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="ckbIsAutoSendSMSByMemGiftExchange" runat="server" type="checkbox" />
                                                    启用会员积分兑换发送短信
                                                </label>
                                            </label>
                                            <%--   <asp:CheckBox ID="ckbIsAutoSendSMSByMemGiftExchange" runat="server" Text="启用会员积分兑换发送短信" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            启动该选项则在会员积分兑换时自动发送短信
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="ckbIsAutoSendSMSByMemPointChange" runat="server" type="checkbox" />
                                                    启用会员积分变动发送短信
                                                </label>
                                            </label>
                                            <%--   <asp:CheckBox ID="ckbIsAutoSendSMSByMemPointChange" runat="server" Text="启用会员积分变动发送短信" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            启动该选项则在会员积分变动时自动发送短信
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="ckbIsAutoSendSMSByCommodityConsumption" runat="server" type="checkbox" />
                                                    启用商品消费发送短信
                                                </label>
                                            </label>
                                            <%-- <asp:CheckBox ID="ckbIsAutoSendSMSByCommodityConsumption" runat="server" Text="启用商品消费发送短信" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            启动该选项则在商品消费时自动发送短信
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="ckbIsAutoSendSMSByFastConsumption" runat="server" type="checkbox" />
                                                    启用快速消费发送短信
                                                </label>
                                            </label>
                                            <%--    <asp:CheckBox ID="ckbIsAutoSendSMSByFastConsumption" runat="server" Text="启用快速消费发送短信" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            启动该选项则在快速消费时自动发送短信
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="ckbIsAutoSendSMSByMemRedTimes" runat="server" type="checkbox" />
                                                    启用会员冲次发送短信
                                                </label>
                                            </label>
                                            <%-- <asp:CheckBox ID="ckbIsAutoSendSMSByMemRedTimes" runat="server" Text="启用会员冲次发送短信" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            启动该选项则在会员冲次时自动发送短信
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="ckbIsAutoSendSMSByTimingConsumption" runat="server" type="checkbox" />
                                                    启用会员计时消费发送短信
                                                </label>
                                            </label>
                                            <%--  <asp:CheckBox ID="ckbIsAutoSendSMSByTimingConsumption" runat="server" Text="启用会员计时消费发送短信" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            启动该选项则在会员计时消费时自动发送短信
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <label style="vertical-align: text-bottom;">
                                                <label class="lbsetCk" style="vertical-align: middle;">
                                                    <input id="ckbIsAutoSendSMSByStorageTiming" runat="server" type="checkbox" />
                                                    启用会员充时消费发送短信
                                                </label>
                                            </label>
                                            <%--  <asp:CheckBox ID="ckbIsAutoSendSMSByTimingConsumption" runat="server" Text="启用会员计时消费发送短信" />--%>
                                        </td>
                                        <td class="tableStyle_right">
                                            启动该选项则在会员充时消费时自动发送短信
                                        </td>
                                    </tr>
                                </table>
                                <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 90%;">
                                    <tr>
                                        <td style="text-align: center; height: 36px" colspan="2">
                                            <asp:Button ID="btnExtraParameter" runat="server" Text="保   存" class="buttonColor"
                                                OnClick="btnExtraParameter_Click" OnClientClick="return  checkForm();" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <%--    <table style="width: 100%; height: 100%; word-wrap: break-word;">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div class="user_List_All">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 800px; margin: auto">
                          
                            <tr>
                                <th style="text-align: left; padding-left: 10px; font-weight: bold; height: 35px;
                                    font-size: 14px; text-align: center;" class="th" colspan="4">
                                    来电参数设置
                                </th>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    <asp:CheckBox ID="chkTel" runat="server" Text="启用来电提醒功能" onclick="IsTelNoMember()" />
                                </td>
                                <td class="tableStyle_right">
                                    如具备来电弹屏设备，可启用该选项进行相关来电弹屏提醒功能
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    <asp:CheckBox ID="chkTelNoMember" runat="server" Text="启用非会员来电提醒功能" />
                                </td>
                                <td class="tableStyle_right">
                                    如具备来电弹屏设备且启用来电提醒功能，可启用该选项使来电弹屏接收非会员信息
                                </td>
                            </tr>
                            <tr>
                                <th style="text-align: left; padding-left: 10px; font-weight: bold; height: 35px;
                                    font-size: 14px; text-align: center;" class="th" colspan="4">
                                    二维码参数设置
                                </th>
                            </tr>
                            
                            <tr>
                                <th style="text-align: left; padding-left: 10px; font-weight: bold; height: 35px;
                                    font-size: 14px; text-align: center;" class="th" colspan="4">
                                    信息发送参数设置
                                </th>
                            </tr>
                      
                            <tr>
                                <td style="text-align: center; height: 36px" colspan="2">
                                    <asp:Button ID="btnExtraParameter" runat="server" Text="保   存" class="buttonColor"
                                        OnClick="btnExtraParameter_Click" OnClientClick="return  checkForm();" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>--%>
    </form>
</body>
</html>
