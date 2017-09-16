<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiftExchange.aspx.cs" Inherits="ChainStock.PointGift.GiftExchange" %>
 
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
    <script src="../Scripts/Module/PointManage/GiftExchange.js" type="text/javascript"></script>
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script> 
</head>
<body>
    <form id="frmGift" runat="server">
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
                    <div class="user_List_All">
                        <uc1:MemberSearch ID="ucMemberSearch" runat="server" />
                        <table style="width: 44.5%; height: 100%; border: 1px #6eb5fb solid; text-align: center;
                            float: left;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="tableStyle_left">
                                    商品名称或简码:
                                </td>
                                <td class="tableStyle_right">
                                    <input type="text" id="txtGiftCode" class="border_radius" title="请输入会员卡号" maxlength="20" />
                                    <div class="user_List_Button">
                                        <input type="button" id="btnGiftSearch" class="common_Button button_style" value="搜　索"
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
                                                        <th>
                                                            礼品名称
                                                        </th>
                                                        <th>
                                                            简码
                                                        </th>
                                                        <th>
                                                            所需积分
                                                        </th>
                                                        <th>
                                                            库存数量
                                                        </th>
                                                    </tr>
                                                </thead>
                                                <tbody id="GiftList">
                                                    <tr class="td">
                                                        <td colspan="4" style="height: 25px; text-align: center; line-height: 25px; background-color: #FFF;">
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" id="GoodsProductPage" style="height: 30px;">
                                    <div class="listTablePage_simple">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="background: #e0f0ff; height: 26px; border-top: 1px solid #adadad;
                                    color: #d38117;">
                                    点击列表中的商品即可轻松选定，同时也可以通过简码、名称进行搜索或条码直接定位！
                                </td>
                            </tr>
                        </table>
                        <table style="width: 55%; height: 100%; border: 1px #6eb5fb solid; text-align: center;
                            float: right;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="tableStyle_left">
                                    兑换日期：
                                </td>
                                <td class="tableStyle_right">
                                    <label id="lblExchangeTime" runat="server" style="font-size: 14px; font-weight: bold;">
                                    </label>
                                </td>
                                <td class="tableStyle_left">
                                    操作人员：
                                </td>
                                <td class="tableStyle_right">
                                    <label id="lblExchangeUSer" runat="server" style="font-size: 14px; font-weight: bold;">
                                    </label>
                                    <input id="MemCard" type="hidden" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    兑换单号：
                                </td>
                                <td class="tableStyle_right">
                                    <label id="lblExchangePrefix" runat="server" style="font-size: 14px; font-weight: bold;
                                        margin-left: 5px">
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table class="table-style user_List_txt" style="width: 100%">
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    礼品名称
                                                </th>
                                                <th>
                                                    礼品简码
                                                </th>
                                                <th>
                                                    所需积分
                                                </th>
                                                <th>
                                                    兑换数量
                                                </th>
                                                <th>
                                                    扣除积分
                                                </th>
                                                <th>
                                                    操&nbsp;作
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody id="GiftExchangeTable">
                                            <tr class="td">
                                                <td colspan="6" style="height: 25px; text-align: center; line-height: 25px; background-color: #FFF;">
                                                    &nbsp;&nbsp;&nbsp;&nbsp;请点击左侧礼品列表，选择需要兑换的礼品！
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    兑换总数量：
                                </td>
                                <td class="tableStyle_right">
                                    <label id="lblTotalNumber" runat="server" style="font-size: 14px; font-weight: bold;">
                                    </label>
                                </td>
                                <td class="tableStyle_left">
                                    兑换总积分:
                                </td>
                                <td class="tableStyle_right">
                                    <label id="lblTotalPoint" runat="server" style="font-size: 14px; font-weight: bold;">
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" class="tableStyle_right">
                                    <div class="submit_detail">
                                        <label id="lblIsSMS" runat="server" style="vertical-align: text-bottom;">
                                            <input id="chkSMS" type="checkbox" runat="server" />
                                            <label style="vertical-align: middle;">
                                                发送短信 &nbsp;
                                            </label>
                                        </label>
                                        <input id="chkIsSMS" runat="server" type="checkbox" style="display: none" />
                                        <label id="lblIsPrint" style="vertical-align: text-bottom;">
                                            <input id="chkPrint" type="checkbox" runat="server" />
                                            <label style="vertical-align: middle;">
                                                打印小票 &nbsp;
                                            </label>
                                        </label>
                                        <input id="btnExchangeSave" type="button" class="buttonColor" value="保存" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Label ID="lblPrintTitle" Style="display: none" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="lblPrintFoot" Style="display: none" runat="server" Text="Labe2"></asp:Label>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
