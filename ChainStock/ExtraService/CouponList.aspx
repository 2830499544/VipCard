<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CouponList.aspx.cs" Inherits="ChainStock.ExtraService.CouponList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" /> 
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/ExtraService/CouponManage.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmCouponList" runat="server">
    <div class="system_Info box_right">
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
                            <div>
                                优惠券名称：<b><span>
                                    <asp:Literal runat="server" ID="txtCouponTitle" Text="Label"></asp:Literal></span><span
                                        id="spCouponID" runat="server" visible="false"></span>&nbsp; </b>优惠券类型：<b><span>
                                            <asp:Literal runat="server" ID="txtCouponType" Text="Label"></asp:Literal></span></b>&nbsp;
                                <span id="spNumber" runat="server">优惠金额：</span><b><span>
                                    <asp:Literal runat="server" ID="txtCouponNumber" Text="Label"></asp:Literal></span></b>&nbsp;
                                预发数量：<b><span>
                                    <asp:Literal runat="server" ID="txtCouponPredictNu" Text="Label"></asp:Literal></span></b>&nbsp;
                                单日限用：<b><span>
                                    <asp:Literal runat="server" ID="txtCouponDayNum" Text="Label"></asp:Literal></span></b>&nbsp;
                                最低消费：<b><span>
                                    <asp:Literal runat="server" ID="txtCouponMinMoney" Text="Label"></asp:Literal></span></b>&nbsp;
                                已发数量：<b><span>
                                    <asp:Literal runat="server" ID="txtCouponYF" Text="Label"></asp:Literal></span></b>&nbsp;
                                已用数量：<b><span>
                                    <asp:Literal runat="server" ID="txtCouponSY" Text="Label"></asp:Literal></span></b>&nbsp;
                                有效期限：<b><span>
                                    <asp:Literal runat="server" ID="txtCouponStart" Text="Label"></asp:Literal></span></b>&nbsp;
                                <label id="lblCouponYX" runat="server" visible="false">
                                    至：
                                </label>
                                <b><span>
                                    <asp:Literal runat="server" ID="txtCouponEnd" Text="Label" Visible="false"></asp:Literal></span>&nbsp;</b>
                            </div>
                            <div class="user_List_top">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                    class="tableStyle">
                                    <tr style="color: #333333; background-color: #F7F6F3;">
                                        <td class="user_List_styleLeft">
                                            快捷操作：
                                        </td>
                                        <td class="user_List_styleRight">
                                            <div class="user_List_Button">
                                                <asp:Button ID="btnCouponExcel" runat="server" Text="导   出"  UseSubmitBehavior="false"   
                                                    class="common_Button" onclick="btnCouponExcel_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr>
                                    <td class="tableStyle_left">
                                        快速查找：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtQueryCoupon" type="text" runat="server" class="border_radius radius2"
                                            title="优惠券名称" />
                                    </td>
                                    <td class="tableStyle_left">
                                        发送状态：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltSendType" runat="server" class="selectWidth">
                                            <option value="">===== 请选择 =====</option>
                                            <option value="0">未发送</option>
                                            <option value="1">已发送</option>
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        使用状态：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltUserType" runat="server" class="selectWidth">
                                            <option value="">===== 请选择 =====</option>
                                            <option value="0">未使用</option>
                                            <option value="1">已使用</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        发送时间：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtSendStartTime" runat="server" type="text" class="Wdate border_radius" />
                                    </td>
                                    <td class="tableStyle_left">
                                        至&nbsp;&nbsp;
                                    </td>
                                    <td class="tableStyle_right" colspan="3">
                                        <input id="txtSendEndTime" runat="server" type="text" class="Wdate border_radius" />
                                    </td>
                                </tr>
                                <tr >
                                    <td class="tableStyle_left">
                                        使用时间：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtUseStartTime" runat="server" type="text" class="Wdate border_radius" />
                                    </td>
                                    <td class="tableStyle_left">
                                        至&nbsp;&nbsp;
                                    </td>
                                    <td class="tableStyle_right" colspan="2">
                                        <input id="txtUseEndTime" runat="server" type="text" class="Wdate border_radius" />
                                    </td>
                                    <td style=" border:none">
                                        <asp:Button ID="btSerch" runat="server" Text="查   询" class="common_Button" OnClick="btSerch_Click" />
                                    </td>
                                </tr>
                            </table>
                            <%--<div id="CouponInfo">
                                <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 700px; padding-left: 10px;
                                    padding-right: 10px; border: 0px" rules="none">
                                    <tr>
                                        <th style="text-align: left; padding-left: 10px; font-weight: bold; height: 35px;
                                            font-size: 14px; text-align: center; background-color: White" class="th" colspan="4">
                                            优惠券参数
                                        </th>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                            优惠券名称：
                                        </td>
                                        <td class="tableStyle_right" style="border: 0px; background-color: White">
                                            <input id="txtCouponTitle" type="text" runat="server" class="border_radius" disabled="disabled" /><span
                                                id="spCouponID" runat="server" visible="false"></span>
                                        </td>
                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                            优惠类型：
                                        </td>
                                        <td class="tableStyle_right" style="border: 0px; background-color: White">
                                            <input id="txtCouponType" type="text" runat="server" class="border_radius" disabled="disabled" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                            <span id="spNumber" runat="server">优惠金额：</span>
                                        </td>
                                        <td class="tableStyle_right" style="border: 0px; background-color: White">
                                            <input id="txtCouponNumber" type="text" class="border_radius" value="1" runat="server"
                                                disabled="disabled" />
                                        </td>
                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                            预发数量：
                                        </td>
                                        <td class="tableStyle_right" style="border: 0px; background-color: White">
                                            <input id="txtCouponPredictNu" type="text" class="border_radius" value="100" runat="server"
                                                disabled="disabled" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                            单日限用：
                                        </td>
                                        <td class="tableStyle_right" style="border: 0px; background-color: White">
                                            <input id="txtCouponDayNum" type="text" class="border_radius" value="0" runat="server"
                                                disabled="disabled" />
                                        </td>
                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                            最低消费：
                                        </td>
                                        <td class="tableStyle_right" style="border: 0px; background-color: White">
                                            <input id="txtCouponMinMoney" type="text" class="border_radius" runat="server" value="0"
                                                disabled="disabled" />
                                        </td>
                                    </tr>
                                    <tr id="trCouponNumber">
                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                            已发数量：
                                        </td>
                                        <td class="tableStyle_right" style="border: 0px; background-color: White">
                                            <input id="txtCouponYF" type="text" class="border_radius" runat="server" disabled="disabled" />
                                        </td>
                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                            已用数量：
                                        </td>
                                        <td class="tableStyle_right" style="border: 0px; background-color: White">
                                            <input id="txtCouponSY" type="text" class="border_radius" runat="server" disabled="disabled" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                            有效期限：
                                        </td>
                                        <td colspan="3" class="tableStyle_right" style="border: 0px; background-color: White">
                                            <input id="txtCouponStart" type="text" class="border_radius" runat="server" disabled="disabled"
                                                style="float: none;" />
                                            <label id="lblCouponYX" runat="server" visible="false">
                                                &nbsp;&nbsp;至&nbsp;&nbsp;</label><input id="txtCouponEnd" type="text" class="border_radius"
                                                    runat="server" disabled="disabled" visible="false" style="float: none;" />
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                            <table class="table-style table-hover user_List_txt" style="margin-top: 10px;">
                                <asp:Repeater runat="server" ID="gvCouponList">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    序号
                                                </th>
                                                <th>
                                                    优惠券号
                                                </th>
                                                <th>
                                                    发送状态
                                                </th>
                                                <th>
                                                    使用状态
                                                </th>
                                                <th>
                                                    发送对象
                                                </th>
                                                <th>
                                                    发送时间
                                                </th>
                                                <th>
                                                    使用时间
                                                </th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="td">
                                            <td>
                                                <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                            </td>
                                            <td>
                                                <%# Eval("CouPon")%>
                                            </td>
                                            <td>
                                                <%# Boolean.Parse(Eval("CouPonYF").ToString()) ? "是" : "否"%>
                                                <%--<asp:CheckBox runat="server" Checked='<%# Eval("CouPonYF")%>' />--%>
                                            </td>
                                            <td>
                                                <%# Boolean.Parse(Eval("CouPonSY").ToString()) ? "是" : "否"%>
                                                <%--<asp:CheckBox runat="server" Checked='<%# Eval("CouPonSY")%>' />--%>
                                            </td>
                                            <td>
                                                <%# Eval("MemCard")%>
                                            </td>
                                            <td>
                                                <%# Eval("ConPonSendTime")%>
                                            </td>
                                            <td>
                                                <%# Eval("ConPonUseTime")%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="user_List_page">
                                <table style="width: 100%" id="tabPager">
                                    <tr>
                                        <td>
                                            <span id="spPageSize">&nbsp;每页记录数：</span>
                                            <asp:DropDownList ID="drpPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPageSize_SelectedIndexChanged">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                            </asp:DropDownList>
                                            <webdiyer:AspNetPager ID="NetPagerParameter" runat="server" AlwaysShow="True" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条"
                                                CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页" LastPageText="尾页"
                                                NextPageText="下一页" OnPageChanging="NetPagerParameter_PageChanging" PrevPageText="上一页"
                                                ShowPageIndexBox="Always" PageSize="10" LayoutType="Table" PageIndexBoxType="DropDownList"
                                                ShowCustomInfoSection="Left" CustomInfoClass="paginator" CustomInfoSectionWidth="300px"
                                                SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" Direction="LeftToRight"
                                                HorizontalAlign="Right">
                                            </webdiyer:AspNetPager>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
