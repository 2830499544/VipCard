<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CouponManage.aspx.cs" Inherits="ChainStock.ExtraService.CouponManage" %>

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
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div id="CouponInfo" style="display: none">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 700px; margin: auto">
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>优惠券名称：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtCouponTitle" type="text" runat="server" class="border_radius" /><span
                                        id="spCouponID" style="display: none;">0</span>
                                </td>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>优惠类型：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="selCouponType" style="outline: none; resize: none;">
                                        <option selected="selected" value="0">代金券</option>
                                        <option value="1">折扣券</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span><span id="spNumber">优惠金额：</span>
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtCouponNumber" type="text" class="border_radius" value="1" />
                                </td>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>预发数量：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtCouponPredictNu" type="text" class="border_radius" value="100" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>单日限用：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtCouponDayNum" type="text" class="border_radius" value="0" />
                                </td>
                                <td class="tableStyle_right" colspan="2">
                                    单个会员每日限用几张优惠券,填写0则不受限制
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>最低消费：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtCouponMinMoney" type="text" class="border_radius" value="0" />
                                </td>
                                <td class="tableStyle_right" colspan="2">
                                    单笔消费满多少元允许使用该优惠券
                                </td>
                            </tr>
                            <tr id="trCouponNumber" style="display: none">
                                <td class="tableStyle_left">
                                    已发数量：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtConponYF" type="text" class="border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    已用数量：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtConponSY" type="text" class="border_radius" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    有效期：
                                </td>
                                <td colspan="3" class="tableStyle_right">
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radCouponYesOrNo" id="radCouponYes" value="0" checked="checked" />
                                        <label style="vertical-align: middle;">
                                            永久有效</label></label>
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radCouponYesOrNo" id="radCouponNo" value="1" />&nbsp;<input
                                            id="txtStartTime" type="text" class="border_radius" style="float: none;" />
                                        <label style="vertical-align: middle;">
                                            &nbsp;至&nbsp;</label>
                                        <input id="txtEndTime" type="text" class="border_radius" style="float: none;" /></label>
                                </td>
                            </tr>
                             <tr>
                                <td class="tableStyle_left">
                                    是否可领取：
                                </td>
                                <td colspan="3" class="tableStyle_right">
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radGetYesOrNo" id="radGetYes" value="1" checked="checked" />
                                        <label style="vertical-align: middle;">
                                            是</label></label>
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radGetYesOrNo" id="radGetNo" value="0" />&nbsp; 否
                                        </label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    发送内容：
                                </td>
                                <td class="tableStyle_right" colspan="3">
                                    <textarea id="txtCouponContent" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                        outline: none; resize: none;"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center; height: 36px">
                                    <div class="buton" style="text-align: center;">
                                        <input id="btnCouponSave" type="button" class="buttonColor" value="保   存 " />
                                        &nbsp
                                        <input id="btnCouponReset" type="button" class="buttonRest" value="重   置 " />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="user_List_All">
                        <div class="user_List_top" style="height: 34px;">
                            <table class="tableStyle" style="width: 100%">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <input id="btnCouponAdd" type="button" value="新优惠券" class="common_Button" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvCouponList">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                优惠券名称
                                            </th>
                                            <th>
                                                优惠券类型
                                            </th>
                                            <th>
                                                折扣比例
                                            </th>
                                            <th>
                                                优惠金额
                                            </th>
                                            <th>
                                                预发数量
                                            </th>
                                            <th>
                                                有效类型
                                            </th>
                                            <th>
                                                单日限用(张)
                                            </th>
                                            <th>
                                                最低消费
                                            </th>
                                            <th>
                                                已发数量
                                            </th>
                                            <th>
                                                已用数量
                                            </th>
                                            <th>
                                                是否可领取
                                            </th>
                                            <th>
                                                操作
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("CouponTitle")%>
                                        </td>
                                        <td>
                                            <%# Convert.ToInt32(Eval("CouponType")) == 0 ? "代金券" : "折扣券"%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Convert.ToInt32(Eval("CouponType")) != 0 ?Eval("CouponNumber") : "-"%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Convert.ToInt32(Eval("CouponType")) == 0 ?Eval("CouponNumber") : "-"%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("CouponPredictNu")%>
                                        </td>
                                        <td>
                                            <%# GetCouponEffective(Eval("CouponEffective").ToString(), Eval("CouponStart").ToString(), Eval("CouponEnd").ToString())%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("CouponDayNum")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("CouponMinMoney","{0:F2}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("CouponYF")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("CouponSY")%>
                                        </td>
                                          <td style="text-align: right">
                                                <%# Eval("IsGet").ToString() == "0" ? "否" : "是"%>
                                        </td>
                                        <td class="listtd" style="width: 40px;">
                                            <a id="hyInfo" runat="server" href='<%# string.Format("CouponList.aspx?PID=74&GID={0}", Eval("ID"))%>'>
                                                <img src="../images/Gift/info.png" alt="查看详情" title="查看详情" />
                                            </a>
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
    </form>
</body>
</html>
