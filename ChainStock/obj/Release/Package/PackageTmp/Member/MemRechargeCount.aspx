<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemRechargeCount.aspx.cs"
    Inherits="ChainStock.Member.MemRechargeCount" %>

<%@ Register Src="../Controls/MemberSearch.ascx" TagName="MemberSearch" TagPrefix="uc1" %>
<%@ Register Src="../Controls/Pay.ascx" TagName="Pay" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />

    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Mem/MemRechargeCount.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Print.js" type="text/javascript"></script>

</head>
<body>
    <form id="frmMemRechargeCount" runat="server">
        <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
            <tr>
                <td colspan="2" style="width: 100%;">
                    <div class="system_Info">

                        <%--打印的次数 --%>
                        <input type="hidden" value="" id="PointNum" runat="server" />

                        <div class="system_Top">
                            <div class="user_regist_title">
                                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                            </div>
                        </div>
                        <div style="margin: 10px;">
                            <uc3:Pay ID="ucPay" runat="server" />
                        </div>
                        <div class="user_List_All">

                            <%--<uc2:MemberSearch ID="ucMemberSearch" runat="server" />--%>
                            <uc1:MemberSearch ID="ucMemberSearch" runat="server" />
                            <table style="width: 44.5%; height: 100%; border: 1px #6eb5fb solid; text-align: center; float: left;"
                                cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="tableStyle_left">项目编码/简码：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input type="text" id="txtGoodsCode" class="border_radius" />
                                        <div class="user_List_Button">
                                            <input type="button" value="搜索" class="common_Button button_style" id="btnServiceSearch"
                                                runat="server" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="tab-body">
                                            <div class="tab-pal">
                                                <table class="table-style table-hover" style="width: 100%">
                                                    <thead class="thead">
                                                        <tr class="th">
                                                            <th>服务名称
                                                            </th>
                                                            <th>服务单价
                                                            </th>
                                                            <th>折后价格
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tbServing">
                                                        <tr class="td">
                                                            <td colspan="4" style="height: 25px; text-align: center; line-height: 25px; background-color: #FFF;"></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <div id="ServingPage" style="margin: 0px; border: solid 1px #ccc; height: 30px; text-align: right; padding-right: 2px; line-height: 30px;">
                                                    <div class="listTablePage_simple">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="background: #e0f0ff; height: 26px; border-top: 1px solid #adadad; color: #d38117;">点击列表中的商品即可，同时也可以通过简码、名称进行搜索或条码直接定位！
                                    </td>
                                </tr>
                            </table>
                            <table style="width: 55%; height: 100%; border: 1px #6eb5fb solid; text-align: center; float: right;"
                                cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="tableStyle_left">充次单号：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label id="spOrderAccount" runat="server" style="font-size: 14px; font-weight: bold;">
                                        </label>
                                    </td>
                                    <td class="tableStyle_left">操作人员：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label id="lblOrderUSer" runat="server" style="font-size: 14px; font-weight: bold;">
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">充次日期：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtOrderTime" type="text" class="Wdate border_radius" runat="server" />
                                    </td>
                                    <td class="tableStyle_left" id="tdStaff">员工提成：
                                    </td>
                                    <td class="tableStyle_right" id="tddStaff">
                                        <select id="sltStaff" runat="server" class="selectWidth" style="width: 130px">
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table class="table-style user_List_txt" style="width: 100%">
                                            <thead class="thead">
                                                <tr class="th">
                                                    <th>服务名称
                                                    </th>
                                                    <th>单价
                                                    </th>
                                                    <th>次数
                                                    </th>
                                                    <th>折后金额
                                                    </th>
                                                    <th>总积分
                                                    </th>
                                                    <th id="thStaff">提成员工
                                                    </th>
                                                    <th>操&nbsp;作
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody id="tbOrderTable">
                                                <tr class="td">
                                                    <td colspan="8" style="height: 25px; text-align: center; line-height: 25px; background-color: #FFF;">&nbsp;&nbsp;&nbsp;&nbsp;请点击左侧商品列表，选择需要充次的服务项目！
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <table style="width: 100%; height: 100%; text-align: center;" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td class="tableStyle_left">充次总次数：
                                                </td>
                                                <td class="tableStyle_right">
                                                    <div class="sum_num">
                                                        <label id="lblTotalNumber" runat="server" style="font-size: 14px; font-weight: bold;">
                                                        </label>
                                                    </div>
                                                </td>
                                                <td class="tableStyle_left">充次总金额：
                                                </td>
                                                <td class="tableStyle_right">
                                                    <div class="sum_num">
                                                        <label id="lblTotalMoney" runat="server" style="font-size: 14px; font-weight: bold;">
                                                        </label>
                                                    </div>
                                                </td>
                                                <td class="tableStyle_left">所得积分：
                                                </td>
                                                <td class="tableStyle_right">
                                                    <div class="sum_num">
                                                        <label id="lblTotalPoint" runat="server" style="font-size: 14px; font-weight: bold;">
                                                        </label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tableStyle_left">
                                                提成总金额：
                                            </td>
                                            <td class="tableStyle_right">
                                                <div class="sum_num">
                                                    <label id="lblStaffMoney" runat="server" style="font-size: 14px; font-weight: bold;">
                                                    </label>
                                                </div>
                                            </td>
                                            </tr>
                                            <tr>
                                                <td class="tableStyle_left">充次备注：
                                                </td>
                                                <td colspan="5" class="tableStyle_right">
                                                    <input type="text" id="tdRemark" class="border_radius border_radius4" maxlength="50" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="text-align: center; height: 36px">
                                                    <div class="submit_detail" style="text-align: center">
                                                        <label id="lblIsSms" runat="server" style="vertical-align: text-bottom;">
                                                            <label class="lbsetCk" style="vertical-align: middle;">
                                                                <input id="chkSMS" runat="server" type="checkbox" />
                                                                发送短信</label></label>&nbsp;&nbsp;
                                                    <label id="lblIsPrint" runat="server" style="vertical-align: text-bottom;">
                                                        <label class="lbsetCk" style="vertical-align: middle;">
                                                            <input id="chkPrint" runat="server" type="checkbox" />
                                                            打印小票</label></label>&nbsp;&nbsp;
                                                    <input id="btnMemCountSave" type="button" class="buttonColor" value="马上充次" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <input id="chkAllowPwd" type="checkbox" style="display: none" runat="server" />
                        <asp:Label ID="lblPrintTitle" Style="display: none" runat="server" Text="Label"></asp:Label>
                        <asp:Label ID="lblPrintFoot" Style="display: none" runat="server" Text="Labe2"></asp:Label>
                        <input id="chkStaff" type="checkbox" style="display: none" runat="server" />
                        <input id="txtStaffType" type="text" style="display: none" runat="server" />
                        <input id="MemCard" type="hidden" runat="server" />
                        <input id="ShopID" type="hidden" runat="server" />
                    </div>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
