<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pay.ascx.cs" Inherits="ChainStock.Controls.Pay" %>
<link href="<%= WebRoot %>/Inc/Style/Style.css" rel="stylesheet" type="text/css" />
<link href="<%= WebRoot %>/Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
<script src="<%= WebRoot %>/Scripts/Module/Common/Pay.js" type="text/javascript"></script>
<script src="<%= WebRoot %>/Scripts/Module/System/Top.js" type="text/javascript"></script>
<script src="<%= WebRoot %>/Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
<script src="<%= WebRoot %>/Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
<script src="<%= WebRoot %>/Scripts/jquery-common.js" type="text/javascript"></script>
<div style="width: 100%; text-align: center;">
    <table style="margin: auto; display: none" id="tbCheckPwd">
        <tr>
            <td>
                请输入会员卡密码：
                <input type="password" id="txtPwd" class="border_radius common_ServiceButton" name="txtPwd" />
                <input type="button" id="btnCheckPwd" class="buttonColor common_ServiceButton" value="确定"
                    style="margin-left: 10px;" />
            </td>
        </tr>
    </table>
    <table style="margin: auto; display: none;" id="tbPay">
        <tr>
            <td>
                <table class="tableStyle">
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            订单编号：
                        </td>
                        <td class="tableStyle_right">
                            <label id="spOrderCode" style="font-size: 14px" class="border_radius_FinMember">
                            </label>
                        </td>
                    </tr>
                    <tr id="trIsNoMember">
                  
                        <td class="tableStyle_left" style="text-align: right;">
                            账户余额：
                        </td>
                        <td class="tableStyle_right">
                            <label id="spMemMoney" style="font-size: 18px;" class="border_radius_FinMember">
                            </label>
                        </td>
                    </tr>
                        
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            应付金额：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" style="font-size: 18px;" readonly="readonly" class="border_radius_FinMember"
                                id="txtTotalMoney" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            优惠金额：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" readonly="readonly" style="font-size: 18px;" class="border_radius_FinMember"
                                id="txtCouponMoney" />
                        </td>
                    </tr>
                    <tr id="trByCard" runat="server">
                        <td class="tableStyle_left" style="text-align: right;">
                            <label>
                                <input id="chkCard" name="PayCard" type="checkbox" runat="server" style="width: 20px" />
                                余额支付：</label>
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" style="font-size: 20px; float: none;" id="txtByCardMoney" class="border_radius" />
                        </td>
                    </tr>
                    <tr id="trByCash" runat="server">
                        <td class="tableStyle_left" style="text-align: right;">
                            <label>
                                <input id="chkCash" name="PayCash" type="checkbox" runat="server" style="width: 20px" />
                                现金支付：</label>
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" style="font-size: 20px; float: none;" id="txtByCashMoney" class="border_radius" />
                        </td>
                    </tr>
                     <tr id="trByBink" runat="server">
                        <td class="tableStyle_left" style="text-align: right;">
                            <label>
                                <input id="chkBink" name="PayBink" type="checkbox" runat="server" style="width: 20px" />
                                银联支付：</label>
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" style="font-size: 20px; float: none;" id="txtByBankMoney" class="border_radius" />
                        </td>
                    </tr>
                     
               
                
                    <tr class="usepoint" id="trByPoint" runat="server">
                        <td class="tableStyle_left" style="text-align: right;">
                            <label>
                                <input id="chkPoint" name="PayPoint" type="checkbox" runat="server" style="width: 20px" />
                                积分支付：</label>
                        </td>
                        <td class="tableStyle_right">
                            <input type="text"  style="font-size: 18px;" class="border_radius" value="0"
                                id="txtUsePoint" />
                         

                            <input id="txtPointUsePercent" type="hidden" runat="server" />
                                 <input id="txtPointDiscountPercent" type="hidden" runat="server" />
                        </td>
                    </tr>
                    
                       <tr class="usepoint" id="trPoint"  style="display:none; height:30px;">
                       <td colspan="2" >
                        积分最高抵用<span style="color:Red; display:inline"  id="txtMaxPointAmount" >0</span>元,账户积分<span style="color:Red;"  id="txtTotalPoint" >0</span>,本次积分抵用<span style="color:Red;"  id="txtUsePointAmount" >0.00</span>元
                   
                       
                       </td>
                    </tr>

                   
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            实际支付：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" id="txtActualTotal" readonly="readonly" style="font-size: 18px;"
                                class="border_radius_FinMember" />
                        </td>
                    </tr>
                    <tr id="trByCash2">
                        <td class="tableStyle_left" style="text-align: right;">
                            找零金额：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" id="txtChangeMoney" readonly="readonly" style="font-size: 18px"
                                class="border_radius_FinMember" />
                        </td>
                    </tr>
                    <tr id="trPayCoupon" runat="server">
                        <td class="tableStyle_left" style="text-align: right;">
                            优惠券号：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" style="font-size: 15px; float: none;" class="border_radius" id="txtCoupon" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tableStyle_right">
                            <span id="spCoupanResult" style=""></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_right" colspan="2">
                            <div class="buton" style="text-align: center;">
                         
                                <input type="button" value="马上结算" class="buttonColor" id="btnPayChange" />
                                &nbsp;&nbsp;&nbsp;
                                <input type="button" value="取   消" class="buttonRest" id="btnCancel" />
                                </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divCouponDetail" style="display: none">
        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 300px; margin: auto">
            <tr>
                <td class="tableAlignRight">
                    优惠券号：
                </td>
                <td class="tableAlignLeft">
                    <span id="spCouPon"></span>
                </td>
            </tr>
            <tr>
                <td class="tableAlignRight">
                    优惠券名称：
                </td>
                <td class="tableAlignLeft">
                    <span id="spCouponTitle"></span>
                </td>
            </tr>
            <tr>
                <td class="tableAlignRight">
                    优惠类型：
                </td>
                <td class="tableAlignLeft">
                    <span id="spCouponType"></span>
                </td>
            </tr>
            <tr>
                <td class="tableAlignRight">
                    <span id="spCouponNumName">优惠金额：</span>
                </td>
                <td class="tableAlignLeft">
                    <span id="spCouponNumber"></span>
                </td>
            </tr>
            <tr>
                <td class="tableAlignRight">
                    单日限用：
                </td>
                <td class="tableAlignLeft">
                    <span id="spCouponDayNum"></span>
                </td>
            </tr>
            <tr>
                <td class="tableAlignRight">
                    最低消费：
                </td>
                <td class="tableAlignLeft">
                    <span id="spCouponMinMoney"></span>
                </td>
            </tr>
            <tr>
                <td class="tableAlignRight">
                    已发时间：
                </td>
                <td class="tableAlignLeft">
                    <span id="spConponSendTime"></span>
                </td>
            </tr>
            <tr>
                <td class="tableAlignRight">
                    发送对象：
                </td>
                <td class="tableAlignLeft">
                    <span id="spConponSendMem"></span>
                </td>
            </tr>
            <tr>
                <td class="tableAlignRight">
                    有效期：
                </td>
                <td class="tableAlignLeft">
                    <span id="spCouponTime"></span>
                </td>
            </tr>
            <tr>
                <td class="tableAlignRight">
                    已用时间：
                </td>
                <td class="tableAlignLeft">
                    <span id="spCouponUseTime"></span>
                </td>
            </tr>
            <tr>
                <td class="tableAlignRight">
                    使用订单号：
                </td>
                <td class="tableAlignLeft">
                    <span id="spCouponAccount"></span>
                </td>
            </tr>
        </table>
    </div>
</div>
