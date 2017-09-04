<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FindMember.ascx.cs"
    Inherits="ChainStock.Controls.FindMember" %>
<link href="<%= WebRoot %>/Inc/Skin/default/formMain.css" rel="stylesheet" type="text/css" />
<link href="<%= WebRoot %>/Inc/Skin/default/newstyle.css" rel="stylesheet" type="text/css" />
<link href="<%= WebRoot %>/Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
<script src="<%= WebRoot %>/Scripts/Module/Common/FindMember.js" type="text/javascript"></script>
<script src="<%= WebRoot %>/Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
<script src="<%= WebRoot %>/Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
<div class="user_regist_Allleft">
    <input type="hidden" runat="server" id="txtIsCanSlotCard" />
    <div class="user_regist_left">
        <div class="user_regist_infor" style="text-align: left">
            会员信息
        </div>
        <table cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle" id="findTable">
            <tr>
                <td class="tableStyle_left">
                    快速查找：
                </td>
                <td class="user_List_styleRight" colspan="3">
                    <input id="txtFindMember" type="text" class="border_radius" title="请输入会员卡号/卡面号码/手机号码" />
                    <label id="lblNoMember" class="common_CheckBox">
                        <input id="chkNoMember" type="checkbox" onclick='NoMemberChoosed(0);' />
                        散客</label>
                        &nbsp;
                         <input id="btnSenseReadCard"   runat="server" classStyle="btnSenseReadCard"   type="button" class="common_Button common_ServiceButton" value="读   卡"/>
                         <input id="btnContactReadCard" runat="server" classStyle="btnContactReadCard" type="button" class="common_Button common_ServiceButton" value="读   卡" />

                    &nbsp;<input id="btnFindMember" type="button" class="common_Button common_ServiceButton"
                        value="查   找" />&nbsp;
                    <input id="btnQuickSearch" type="button" class="common_Button common_ServiceButton"
                        value="快速查找" />&nbsp;
                    <input id="btnQueryMemCount" type="button" class="common_Button common_ServiceButton"
                        value="已有充次" />
                </td>
            </tr>
            <tr>
                <td class="tableStyle_left">
                    会员卡号：
                </td>
                <td class="tableStyle_right" style="padding-left: 8px">
                    <input id="txtFMemID" type="hidden" />
                    <input id="txtFMemCard" type="text" class="border_radius_FinMember" readonly="readonly" />
                </td>
                <td class="tableStyle_left">
                    会员姓名：
                </td>
                <td class="tableStyle_right" style="padding-left: 8px">
                    <input id="txtFMemName" type="text" class=" border_radius_FinMember" readonly="readonly" />
                </td>
            </tr>
            <tr id="trMoneyAndPoint">
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
            <tr id="trDisplayAll">
                <td colspan="4" style="text-align: center">
                    <a href="javascript:void(0);" onclick="ShowAll(1);" style="color: #0066CC">↓显示会员详细资料↓</a>
                </td>
            </tr>
            <tr id="trCardNumberAndMobile" style="display: none">
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
            <tr id="trPastTimeAndShop" style="display: none;">
                <td class="tableStyle_left">
                    所属店铺：
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
            <tr id="trBirthdayAndSex" style="display: none;">
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
            <tr id="trIdentityAndEmai" style="display: none;">
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
            <tr id="trCreateTimeAndUser" style="display: none;">
                <td class="tableStyle_left">
                    办卡日期：
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
            <tr id="trNoDisplayAll" style="display: none;">
                <td colspan="4" style="text-align: center">
                    <a href="javascript:void(0);" onclick="ShowAll(0);" style="color: #0066CC">↑收起会员详细资料↑</a>
                </td>
            </tr>
        </table>
        <div id="divQuickSearch" style="display: none;">
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
            <div id="MemPageList" style="margin: 0px; height: 30px; text-align:right;">
                <div class="listTablePage_simple">
                </div>
            </div>
            <div style="height: 20px; line-height: 20px; text-align: center; padding-top: 5px">
                卡号/卡面号码/姓名/手机：
                <input type="text" id="txtQueryMem" class="border_radius" style="clear: both; float: none" />
                <input type="button" id="btnSearch" class="common_Button common_ServiceButton" value="查找" />
            </div>
        </div>
    </div>
</div>
