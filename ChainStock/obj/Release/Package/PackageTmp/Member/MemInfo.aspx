<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemInfo.aspx.cs" Inherits="ChainStock.Member.MemInfo" %>
 
<%@ Register Src="../Controls/MemberSearch.ascx" TagName="MemberSearch" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Mem/MemInfo.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Tab.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#findTable").removeAttr("style");
            $("#trTitle").css("display", "none");
            $("#divMemberInfo").css("display", "none");
        });
    </script>
</head> 
<body>
    <form id="frmMemInfo" runat="server">
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_List_All" style="margin-bottom: 0px">
      
            <uc1:MemberSearch ID="ucMemberSearch" runat="server" />
        </div>
        <div class="user_regist_Allleft" style="clear: both; float: none;">
            <div class="user_regist_left">
                <div class="user_regist_infor" style="text-align: left">
                    快速菜单
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="user_List_styleRight">
                            <div class="user_List_Button">
                                <input id="btnEditMem" runat="server" cls="64" type="button" value="会员编辑" class="common_Button" />
                                <input id="btnExpense" runat="server" cls="17" type="button" value="快速消费" class="common_Button" />
                                <input id="btGoodsExpense" cls="67" runat="server" type="button" value="商品消费" class="common_Button" />
                                <input id="btTimeExpense" cls="87" runat="server" type="button" value="计时消费" class="common_Button" />
                                <input id="btConsumeMemCount" cls="118" runat="server" type="button" value="计次消费" class="common_Button" />
                                <input id="btnRechargeMoney" cls="4" runat="server" type="button" value="会员充值" class="common_Button" />
                                <input id="btnRechargCount" cls="66" runat="server" type="button" value="会员充次" class="common_Button" />
                                <input id="btnExchangeGift" cls="14" runat="server" type="button" value="兑换礼品" class="common_Button" />
                                <input id="btnSendSMS" cls="46" runat="server" type="button" value="发送短信" class="common_Button" />
                            </div>
                        </td>
                    </tr>
                </table>

                <div style="padding-top: 3px">
                    <!--菜单-->
                    <div class="tabBox" id="ExchangeTab">
                        <ul class="tab">
                            <li id="tab0" class="on">会员基本资料</li>
                            <li id="tab1">会员消费记录</li>
                            <li id="tab2">会员充值记录</li>
                            <li id="tab3">会员充次记录</li>
                            <li id="tab4">礼品兑换记录</li>
                            <li id="tab5">会员优惠券记录</li>
                            <li id="tab6">会员来电记录</li>
                        </ul>
                    </div>
                    <!--会员资料-->
                    <div>
                        <table id="MainContent0" style="margin: 10px 10px 10px 2px;">
                            <tr>
                                <td style="vertical-align: top">
                                    <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle"
                                        style="width: 600px;">
                                        <tr>
                                            <td class="tableStyle_left">
                                                会员卡号：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemCard" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="tableStyle_left">
                                                会员姓名：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemName" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tableStyle_left">
                                                卡面号码：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemCardNumber" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="tableStyle_left">
                                                手机号码：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemMobile" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tableStyle_left">
                                                会员余额：
                                            </td>
                                            <td class="tableStyle_right">
                                                <font color="red">
                                                    <asp:Label ID="txtMemMoney" runat="server" Text=""></asp:Label></font>
                                            </td>
                                            <td class="tableStyle_left">
                                                会员积分：
                                            </td>
                                            <td class="tableStyle_right">
                                                <font color="red">
                                                    <asp:Label ID="txtMemPoint" runat="server" Text=""></asp:Label></font>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tableStyle_left">
                                                会员等级：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemLevel" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="tableStyle_left">
                                                会员状态：
                                            </td>
                                            <td class="tableStyle_right">
                                                <font color="red">
                                                    <asp:Label ID="txtMemState" runat="server" Text=""></asp:Label></font>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tableStyle_left">
                                                所属商家：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemShop" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="tableStyle_left">
                                                过期时间：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemPastTime" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tableStyle_left">
                                                会员生日：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemBirthday" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="tableStyle_left">
                                                会员性别：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemSex" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tableStyle_left">
                                                身份证号：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemIdentityCard" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="tableStyle_left">
                                                电子邮箱：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemEmail" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tableStyle_left">
                                                办卡时间：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemCreateTime" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="tableStyle_left">
                                                办卡人员：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemUserName" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tableStyle_left">
                                                固定电话：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtTelephone" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td class="tableStyle_left">
                                                推荐人卡号：
                                            </td>
                                            <td class="tableStyle_right">
                                                <asp:Label ID="txtMemRecommendCard" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr id="trIsPast">
                                        </tr>
                                        <tr>
                                            <td class="tableStyle_left">
                                                联系地址：
                                            </td>
                                            <td class="tableStyle_right" colspan="3">
                                                <asp:Label ID="txtMemAddress" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tableStyle_left">
                                                备注：
                                            </td>
                                            <td class="tableStyle_right" colspan="3">
                                                <%--<asp:Label ID="txtMemRemark" runat="server" Text=""></asp:Label>--%>                                                
                                                <textarea id="txtMemRemark" rows="3" runat="server" style="width: 100%; word-wrap: break-word;outline: none; resize: none;" ></textarea>
                                                <input id="MemCard" type="hidden" runat="server" />
                                            </td>
                                            <td style="display: none">
                                                <input id="chkIsPast" runat="server" type="checkbox" />
                                            </td>
                                        </tr>
                                        <tbody id="MemInfoCustomField" runat="server">
                                            <tr>
                                                <td class="tableStyle_right" colspan="4" style="padding: 5px;">
                                                    正在加载自定义属性，请稍候……
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                                <td align="left" valign="top">
                                    <table class="tableStyle" cellspacing="0" cellpadding="2">
                                        <tr>
                                            <td>
                                                <img alt="" id="imgMemPhoto" src="../images/member/nophoto.gif" width="180" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--消费记录-->
                    <div style="margin: 10px 10px 10px 2px; display: none;" id="MainContent1">
                        <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle"
                            style="width: 100%;" id="MemInfoExpense">
                            <tr>
                                <td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;"
                                    colspan="6" type="LoadingBar">
                                    <script type="text/javascript">
                                        ListLoading();
                                    </script>
                                </td>
                            </tr>
                        </table>
                        <div id="MemInfoExpenseListPage" style="margin: 0px; border: solid 1px #ccc; height: 30px;">
                            <div class="listTablePage_simple">
                            </div>
                        </div>
                    </div>
                    <div style="margin: 10px 10px 10px 2px; display: none;" id="MainContent2">
                        <table border="0" cellpadding="0" cellspacing="1" class="table-style table-hover user_List_txt"
                            width="100%" id="MemInfoRecharge">
                            <tr>
                                <td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;"
                                    colspan="6" type="LoadingBar">
                                    <script type="text/javascript">
                                        ListLoading();
                                    </script>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="margin: 10px 10px 10px 2px; display: none;" id="MainContent3">
                        <table border="0" cellpadding="0" cellspacing="1" class="table-style table-hover user_List_txt"
                            width="100%" id="MemInfoRechargeCount">
                            <tr>
                                <td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;"
                                    colspan="6" type="LoadingBar">
                                    <script type="text/javascript">
                                        ListLoading();
                                    </script>
                                </td>
                            </tr>
                        </table>
                        <div id="MemInfoRechargeCountListPage" style="margin: 0px; border: solid 1px #ccc;
                            height: 30px;">
                            <div class="listTablePage_simple">
                            </div>
                        </div>
                    </div>
                    <div style="margin: 10px 10px 10px 2px; display: none;" id="MainContent4">
                        <table border="0" cellpadding="0" cellspacing="1" class="table-style table-hover user_List_txt"
                            width="100%" id="MemInfoExchangeGift">
                            <tr>
                                <td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;"
                                    colspan="6" type="LoadingBar">
                                    <script type="text/javascript">
                                        ListLoading();
                                    </script>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="margin: 10px 10px 10px 2px; display: none;" id="MainContent5">
                        <table border="0" cellpadding="0" cellspacing="1" class="table-style table-hover user_List_txt"
                            width="100%" id="MemInfoCoupon">
                            <tr>
                                <td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;"
                                    colspan="6" type="LoadingBar">
                                    <script type="text/javascript">
                                        ListLoading();
                                    </script>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="margin: 10px 10px 10px 2px; display: none;" id="MainContent6">
                        <table border="0" cellpadding="0" cellspacing="1" class="table-style table-hover user_List_txt"
                            width="100%" id="MemInfoMobile">
                            <tr>
                                <td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;"
                                    colspan="6" type="LoadingBar">
                                    <script type="text/javascript">
                                        ListLoading();
                                    </script>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
