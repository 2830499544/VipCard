<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberSearch.ascx.cs"
    Inherits="ChainStock.Controls.MemberSearch" %>
<script src="<%= WebRoot %>/Scripts/Module/Common/MemberSearch.js" type="text/javascript"></script>
<script src="<%= WebRoot %>/Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
<script src="<%= WebRoot %>/Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
<asp:Literal ID="litControl" runat="server"></asp:Literal>
 
<input type="hidden" runat="server" id="txtIsCanSlotCard" />
<table style="height: 100%" cellpadding="0" cellspacing="0" class="foods_consume_head">
    <tr style="height: 40">
        <td class="tableStyle_left">
            请输入卡号：
        </td>
        <td class="user_List_styleRight">
            <input type="text" id="txtFindMember" class="border_radius" title="请输入会员卡号/卡面号码/手机号码"
                maxlength="20"   /><input type="button" id="btnClearCard"  class="common_Button common_ServiceButton" value="清 空" style="display:none;vertical-align: middle;" />
            <label id="lblNoMember" class="common_CheckBox">            
                <input id="chkNoMember" type="checkbox" onclick='NoMemberChoosed(0);' />
                散客
            </label>
            &nbsp;
            <input style="vertical-align: middle" id="btnSenseReadCard" type="button" runat="server"
                class="common_Button common_ServiceButton" value="读   卡" />
            <input style="vertical-align: middle;display:none" id="btnContactReadCard" type="button" runat="server"
                class="common_Button common_ServiceButton" value="读   卡" />
            <label id="lblFindMember">
                <input id="btnFindMember" type="button" class="common_Button common_ServiceButton"
                    value="查　找" style="" />
            </label>
            <label id="lblQuickSearch">
                <input id="btnQuickSearch" type="button" class="common_Button common_ServiceButton"
                    value="快速查找" />
                <label style="vertical-align: middle;">
                    输入卡号或者卡面号码或者手机号码，按回车即可定位到此会员！
                </label>
            </label>
        </td>
    </tr>
    <tr style="height: 40px; padding-top: 5px" id="trMemAll">
        <td colspan="2" style="border-top: 1px solid #e4e7ea">
            <p>
                姓名：</p>
            <h3>
                <span id="spName"></span>
            </h3>
            <p>
                会员等级：
            </p>
            <h3>
                <span id="spLevelName"></span>
            </h3>
            <p>
                余额：
            </p>
            <h3>
                <span id="spMoney"></span>
            </h3>
            <p>
                积分数量：
            </p>
            <h3>
                <span id="spPoint"></span>
            </h3>
            <p id="IsDetail">
                <a href="javascript:void(0);" onclick="ShowMemDetail(1);" style="color: #0066CC">↓显示会员详细资料↓</a>
            </p>
        </td>
    </tr>
    <tr id="Panel_MemAllMsg" style="display: none;">
        <td colspan="2">
            <div style="width: 600px; padding-top: 10px;">
                <table cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle" id="findTable">
                    <tr>
                        <td class="tableStyle_left">
                            会员卡号：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtFMemID" type="hidden" />
                            <input id="txtFMemCard" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                        <td class="tableStyle_left">
                            会员姓名：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtFMemName" type="text" class=" border_radius_FinMember" readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            会员余额：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemMoney" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                        <td class="tableStyle_left">
                            会员积分：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemPoint" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            剩余计次：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemCountNumber" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                        <td class="tableStyle_left">
                            累积消费：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFConsumeMoney" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            会员等级：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemLevelName" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                        <td class="tableStyle_left">
                            会员状态：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemState" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            卡面号码：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemCardNumber" type="text" class=" border_radius_FinMember" readonly="readonly" />
                        </td>
                        <td class="tableStyle_left">
                            手机号码：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemMobile" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            所属商家：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemShopName" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                        <td class="tableStyle_left">
                            过期时间：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemPastTime" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            会员生日：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemBirthday" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                        <td class="tableStyle_left">
                            会员性别：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemSex" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            身份证号：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemIdentityCard" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                        <td class="tableStyle_left">
                            电子邮箱：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemEmail" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            办卡时期：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemCreateTime" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                        <td class="tableStyle_left">
                            办卡人员：
                        </td>
                        <td class="tableStyle_right" style="padding-left: 8px">
                            <input id="txtFMemUserName" type="text" class="border_radius_FinMember" readonly="readonly" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center">
                            <a href="javascript:void(0);" onclick="ShowMemDetail(0);" style="color: #0066CC">↑收起会员详细资料↑</a>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<div id="divQuickSearch" style="display: none">
    <table class="table-style table-hover user_List_txt">
        <thead class="thead">
            <tr class="th">
                <th style="width: 120px">
                    会员卡号
                </th>
                <th style="width: 120px">
                    会员姓名
                </th>
                <th style="width: 120px">
                    会员等级
                </th>
                <th style="width: 120px">
                    账户余额
                </th>
                <th>
                    积分数量
                </th>
            </tr>
        </thead>
    </table>
    <div style="height: 260px; width: 600px; overflow: auto;">
        <table class="table-style table-hover user_List_txt" id="tbQuickSearch">
            <tr>
                <td id="tdDetail" style="height: 20px; line-height: 50px; background-color: #fff;"
                    colspan="6" type="LoadingBar">
                    <script type="text/javascript">
                        ListLoading();
                    </script>
                </td>
            </tr>
        </table>
    </div>
    <div id="MemPageList" style="margin: 0px; height: 30px; text-align: right;">
        <div class="listTablePage_simple">
        </div>
    </div>
    <div style="height: 30px; line-height: 30px; text-align: center;">
        卡号/卡面号码/姓名/手机：
        <input type="text" id="txtQueryMem" class="border_radius" style="clear: both; float: none" />
        <input type="button" id="btnSearch" class="common_Button common_ServiceButton" value="查找" />
    </div>
</div>
