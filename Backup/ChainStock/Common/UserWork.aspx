<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserWork.aspx.cs" Inherits="ChainStock.Common.UserWork" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
  
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/UserWork.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <style type="text/css">
        .tableStyle_left
        {
            width: 130px;
        }
        .tableStyle_right
        {
            width: 270px;
        }
    </style>
</head>
<body>
    <form id="frmTotal" runat="server">
    <input type="hidden" id="hdUserID" runat="server" />
    <input type="hidden" id="hdStartTime" runat="server" />
    <input type="hidden" id="hdEndTime" runat="server" />
    <input type="hidden" id="hdShopID" runat="server" />
    <input type="hidden" id="hdallmoney" runat="server" />
    <input type="hidden" id="hdyjjg" />
    <input type="hidden" id="hdye" />
    <input type="hidden" id="hdylcz" />
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_regist_Allleft">
            <div class="user_regist_left">
                <div class="user_regist_infor">
                    管理员换班
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            管理员：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblUserName" runat="server">
                            </label>
                        </td>
                        <td class="tableStyle_left">
                            交接时间：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblStartTime">
                            </label>
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor">
                    会员数量统计
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            新增会员数量：
                        </td>
                        <td class="tableStyle_right" colspan="3" style="width: auto;">
                            <label id="lblMemNumber">
                                0名</label>
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor">
                    会员提现数据统计
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            提现金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lbAllDrawMoney">
                                ￥0.00</label>
                        </td>
                        <td class="tableStyle_left">
                            扣除余额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblAllDrawActualMoney">
                                ￥0.00</label>
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor">
                    会员消费数据统计
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            余额支付金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblCardMoney">
                                ￥0.00</label>
                        </td>
                        <td class="tableStyle_left">
                            现金消费金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblExpenseSumMoneys">
                                ￥0.00</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            银联消费金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblExpenseBinkMoneys">
                                ￥0.00</label>
                        </td>
                        <td class="tableStyle_left">
                            优惠券金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblExpenseCouponMoneys">
                                ￥0.00</label>
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor">
                    会员充次数据统计
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            余额支付金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblCountCardMoney">
                                ￥0.00</label>
                        </td>
                        <td class="tableStyle_left">
                            现金消费金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblCountCashMoney">
                                ￥0.00</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            银联消费金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblCountBankMoney">
                                ￥0.00</label>
                        </td>
                        <td class="tableStyle_left">
                            优惠券金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblCountCouponMoney">
                                ￥0.00</label>
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor">
                    会员充时数据统计
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            余额支付金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblStorageTimingPayCard">
                                ￥0.00</label>
                        </td>
                        <td class="tableStyle_left">
                            现金充次金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblStorageTimingPayCash">
                                ￥0.00</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            银联充次金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblStorageTimingPayBink">
                                ￥0.00</label>
                        </td>
                        <td class="tableStyle_left">
                            优惠券金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblStorageTimingPayCoupon">
                                ￥0.00</label>
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor">
                    会员充值数据统计
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            初始充值总额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblSRechargeMoney">
                                ￥0.00</label>
                        </td>
                        <td class="tableStyle_left">
                            现金充值总额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblFRechargeMoney">
                                ￥0.00</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            银联充值总额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblRechargeBank">
                                ￥0.00</label>
                        </td>
                        <td class="tableStyle_left">
                            充值赠送总额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblFRechargeGiveMoney">
                                ￥0.00</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            现金总收入：
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <label id="lblAllMoneys">
                                ￥0.00</label>
                            &nbsp;&nbsp;(综合现金总收入=现金消费金额+现金充次金额+初始充值总额+现金充值金额+换班金额-提现金额)
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            实际现金收款：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" id="txtsjMoneys" class="input_txt border_radius" />
                        </td>
                        <td class="tableStyle_right" colspan="2">
                            <label id="lblArrearage" style="text-align: left;">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            接班员工编号：
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <input type="text" id="txtHandoverUserID" class="input_txt border_radius" /><label
                                id="lblname" style="margin-left: 3px;"></label><input type="hidden" id="hdjjid" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_right" colspan="4" style="text-align: center;">
                            <input id="btnSave" type="button" value="确定换班" class="buttonColor" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
