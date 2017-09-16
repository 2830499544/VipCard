<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptTotal.aspx.cs" Inherits="ChainStock.Report.RptTotal" %>

<%@ Register src="../Controls/QuickSearch.ascx" tagname="QuickSearch" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/RptTotal.js" type="text/javascript"></script>
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
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_regist_Allleft">
            <div class="user_regist_left">
                <div class="user_regist_infor">
                    统计查询条件
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            选择时间范围：
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <label style="vertical-align: text-bottom;">
                                <input id="rdoToday" type="radio" value="1" checked="checked" name="group" />
                                <label for="lblrdoToday" style="vertical-align: middle;">
                                    今天</label>
                                <input id="rdoLastDay" type="radio" name="group" value="2" />
                                <label for="lbldoYesterday" style="vertical-align: middle;">
                                    昨天</label>
                                <input id="rdoWeek" type="radio" name="group" value="3" />
                                <label for="lblrdoWeek" style="vertical-align: middle;">
                                    近一周</label>
                                <input id="rdoMonth" type="radio" name="group" value="4" />
                                <label for="lblrdoMonth" style="vertical-align: middle;">
                                    本月</label>
                                <input id="rdoThreeDay" type="radio" name="group" value="5" />
                                <label for="lblrdoThreeDay" style="vertical-align: middle;">
                                    近30天</label>
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <label style="vertical-align: text-bottom;">
                                <input id="rdTimeDuan" type="radio" name="group" value="6" />
                                <label for="lblTimeDuan" style="vertical-align: middle;">
                                    按时间段</label>&nbsp;
                                <input id="txtStartTime" disabled="disabled" style="float: none;" type="text" runat="server"
                                    class="Wdate border_radius" />
                                <label style="vertical-align: middle;">
                                    至
                                </label>
                                <input id="txtEndTime" disabled="disabled" type="text" style="float: none;" runat="server"
                                    class="Wdate border_radius" />
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            按商家查询：
                        </td>
                        <td class="tableStyle_right">
                            <select id="sltShop" runat="server" class="selectWidth">
                            </select>
                        </td>
                        <td class="tableStyle_left">
                            按用户查询：
                        </td>
                        <td class="tableStyle_right">
                            <select id="sltUserID" runat="server" class="selectWidth">
                            </select>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            查询时间段：
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <label id="lblTime">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            查询操作员：
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <label id="lblMaster">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_right" colspan="4">
                            <div class="buton" style="text-align: center;">
                                <input id="btnSerach" type="button" class="buttonColor" value="查询数据" />
                            </div>
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
                        <td colspan="3" style="width: auto; border: 1px solid #e4e7ea; text-align: left;
                            padding-left: 8px;">
                            <label id="lblMemNumber">
                                0名</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            新增会员详细：
                        </td>
                        <td colspan="3" style="width: auto; border: 1px solid #e4e7ea; text-align: left;
                            padding-left: 8px;">
                            <label id="lblMemDetial">
                                无</label>
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
                    <tr>
                    <td class="tableStyle_left">
                           微信支付金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblExpenseWeiXinMoneys">
                                ￥0.00</label>
                        </td>
                        <td class="tableStyle_left">
                            积分抵用金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblExpensePointMoneys">
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
                            现金充次金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblCountCashMoney">
                                ￥0.00</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            银联充次金额：
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
                     <tr>
                    <td class="tableStyle_left">
                           微信支付金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblCountWeiXinMoney">
                                ￥0.00</label>
                        </td>
                        <td class="tableStyle_left">
                            积分抵用金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblCountPointMoney">
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
                       <tr>
                    <td class="tableStyle_left">
                           微信支付金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblStorageTimingPayWeiXin">
                                ￥0.00</label>
                        </td>
                        <td class="tableStyle_left">
                            积分抵用金额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblStorageTimingPayPoint">
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
                            初始赠送总额：
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
                            支付宝充值总额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblFRechargeWebMoney">
                                ￥0.00</label>
                        </td>
                     

                      <td class="tableStyle_left">
                            微信充值总额：
                        </td>
                        <td class="tableStyle_right" >
                            <label id="lblWXCZ">
                                ￥0.00</label>
                        </td>


                    </tr>


                    <tr>
                        <td class="tableStyle_left">
                            综合现金总收入：
                        </td>
                        <td class="tableStyle_right" colspan="3" style="text-align: left;">
                            <label id="lblAllMoneys">
                                ￥0.00</label>
                            &nbsp;&nbsp;&nbsp;&nbsp;(综合现金总收入=现金消费金额+现金充次金额+现金充值金额-提现金额)
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
