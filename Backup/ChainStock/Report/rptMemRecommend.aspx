<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rptMemRecommend.aspx.cs"
    Inherits="ChainStock.Report.rptMemRecommend" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../Controls/SysArea.ascx" TagName="SysArea" TagPrefix="uc1" %>
<%@ Register src="../Controls/QuickSearch.ascx" tagname="QuickSearch" tagprefix="uc2" %>
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
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#SysArea1_sltCity").bind("change", { foo: "SysArea1" }, City);
            $("#SysArea1_sltCounty").bind("change", { foo: "SysArea1" }, County);
            $("#SysArea1_sltProvince").bind("change", { foo: "SysArea1" }, Province);
            $("#SysArea1_sltVillage").bind("change", { foo: "SysArea1" }, Village);
            $('#txtMemStartTime').bind("focus click", function () {
                WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
            });
            $('#txtMemEndTime').bind("focus click", function () {
                WdatePicker({ skin: 'ext', maxDate: '%y-%M-%d', isShowClear: true, readOnly: true });
            });
        });
        function detailshow(memid) {
            if ($(memid).css("display") == "none") $(memid).show();
            else $(memid).css("display", "none")
        }
        function cleatall() {
            var html = "<input type='text' id='text' name='text' class='input_txt border_radius'/>";
            $("#sltMemLevelID").val("");
            $("#sltShop").val("");
            $("#sltMemState").val("");
            $("#sltMemPoint").val("");
            $("#txtMemPoint").val("");
            $("#sltMemUserID").val("");
            $("#sltMemBirthday").val("");
            $("#sltMemPastTime").val("");
            $("#sltMemMoney").val("");
            $("#txtMemMoney").val("");
            $("#sltCustomField").val("");
            $("#Custom").html(html);
            $("#sltMemConsume").val("");
            $("#sltConsumeMoney").val("");
            $("#txtConsumeMoney").val("");
            $("#txtMemStartTime").val("");
            $("#txtMemEndTime").val("");
            $("#txtMemRecommendCard").val("");
            $("#SysArea1_sltProvince").val("");
            $("#SysArea1_sltCity").val("");
            $("#SysArea1_sltCounty").val("");
            $("#SysArea1_sltVillage").val("");
        }
    </script>
</head>
<body style="padding-right: 1px">
    <form id="form1" runat="server">
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
                        <div class="user_List_top" style="height: 34px;">
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft" style="width: 8%">
                                        快速查找：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <input id="txtQueryMem" runat="server" type="text" class="border_radius radius2"
                                            title="请输入卡号/卡面号码/姓名/手机号" maxlength="20" />
                                        <div class="user_List_Button">
                                            <asp:Button ID="btnListQuery" runat="server" Text="查   询" class="common_Button" OnClick="btnQuery_Click" />
                                            <input id="btnhigthSearch" type="button" class="common_Button" value="高级查询" onclick="detailshow('#divHightSearch')" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divHightSearch" style="display: none">
                            <table class="tableStyle" style="width: 100%;">
                                <tr>
                                    <td class="tableStyle_left">
                                        会员等级：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <select id="sltMemLevelID" runat="server" class="selectWidth">
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        所属商家：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <select id="sltShop" runat="server" class="selectWidth" name="D8">
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        会员积分：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <select id="sltMemPoint" runat="server" name="D7" style="outline: none;">
                                            <option selected="selected" value="&gt;=">>=</option>
                                            <option value="&lt;="><=</option>
                                            <option value="=">=</option>
                                        </select>
                                        <input id="txtMemPoint" type="text" runat="server" class="border_radius" style="width: 100px;
                                            clear: both; float: none" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        办卡人员：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <select id="sltMemUserID" runat="server" class="selectWidth" name="D6">
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        生日会员：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <select id="sltMemBirthday" runat="server" class="selectWidth" name="D3">
                                            <option value="">===== 请选择 =====</option>
                                            <option value="0">本日生日会员</option>
                                            <option value="1">本周生日会员</option>
                                            <option value="2">本月生日会员</option>
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        会员余额：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <select id="sltMemMoney" runat="server" style="outline: none;">
                                            <option selected="selected" value="&gt;=">>=</option>
                                            <option value="&lt;="><=</option>
                                            <option value="=">=</option>
                                        </select>
                                        <input id="txtMemMoney" type="text" runat="server" class="border_radius" style="width: 100px;
                                            clear: both; float: none" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        会员状态：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <select id="sltMemState" runat="server" class="selectWidth" name="D1">
                                            <option value="">===== 请选择 =====</option>
                                            <option value="0">正常</option>
                                            <option value="1">锁定</option>
                                            <option value="2">挂失</option>
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        过期时间：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <select id="sltMemPastTime" runat="server" class="selectWidth" name="D2">
                                            <option value="">===== 请选择 =====</option>
                                            <option value="0">已过期</option>
                                            <option value="1">未来7天</option>
                                            <option value="2">未来30天</option>
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        累计消费：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <select id="sltConsumeMoney" runat="server" name="D5" style="outline: none;">
                                            <option selected="selected" value="&gt;=">>=</option>
                                            <option value="&lt;="><=</option>
                                            <option value="=">=</option>
                                        </select>
                                        <input id="txtConsumeMoney" type="text" runat="server" class="border_radius" style="width: 100px;
                                            clear: both; float: none" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        自定义属性：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <select id="sltCustomField" runat="server" class="selectWidth">
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        属&nbsp;&nbsp;性&nbsp;&nbsp;值：
                                    </td>
                                    <td id="Custom" class="user_List_styleRight">
                                        <asp:Literal ID="ltCustom" runat="server" Visible="false">
                                        </asp:Literal>
                                        <input id="txtCustomField" type="text" runat="server" class="input_txt border_radius" />
                                    </td>
                                    <td class="tableStyle_left">
                                        未&nbsp;&nbsp;消&nbsp;&nbsp;费：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <select id="sltMemConsume" runat="server" class="selectWidth" name="D4">
                                            <option value="">===== 请选择 =====</option>
                                            <option value="0">三个月未消费会员</option>
                                            <option value="1">半年未消费会员</option>
                                            <option value="2">一年未消费会员</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        登记时间：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <input id="txtMemStartTime" type="text" class="Wdate border_radius" runat="server" />
                                    </td>
                                    <td class="tableStyle_left">
                                        至
                                    </td>
                                    <td class="user_List_styleRight">
                                        <input id="txtMemEndTime" type="text" class="Wdate border_radius" runat="server" />
                                    </td>
                                    <td class="tableStyle_left">
                                        推&nbsp;&nbsp;荐&nbsp;&nbsp;人：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <input id="txtMemRecommendCard" type="text" runat="server" class="border_radius"
                                            style="clear: both; float: none" />
                                    </td>
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        所属区域：
                                    </td>
                                    <td colspan="3" class="user_List_styleRight" style="vertical-align: text-bottom;">
                                        <uc1:SysArea ID="SysArea1" runat="server" />
                                    </td>
                                    <td>
                                    </td>
                                    <td class="tableStyle_right" colspan="3">
                                        <input id="btnMemReset" type="button" value="重   置" class="common_Button" onclick="cleatall()" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt">
                            <thead class="thead">
                                <tr class="th">
                                    <th>
                                        序号
                                    </th>
                                    <th>
                                        会员卡号
                                    </th>
                                    <th>
                                        会员姓名
                                    </th>
                                    <th>
                                        手机号码
                                    </th>
                                    <th>
                                        会员金额
                                    </th>
                                    <th>
                                        账户积分
                                    </th>
                                    <th>
                                        历史消费金额
                                    </th>
                                    <th>
                                        推荐人数
                                    </th>
                                    <th>
                                        获得积分
                                    </th>
                                    <th>
                                        会员等级
                                    </th>
                                    <th>
                                        会员状态
                                    </th>
                                    <th>
                                        会员生日
                                    </th>
                                    <th>
                                        所属商家
                                    </th>
                                    <th>
                                        办卡日期
                                    </th>
                                </tr>
                            </thead>
                            <asp:Repeater runat="server" ID="gvMemList" OnItemDataBound="gvMemList_ItemDataBound">
                                <ItemTemplate>
                                    <tr class="td" onclick="detailshow('#data<%# Eval("MemID") %>')">
                                        <td>
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <a id="linkmemdetail" href='<%#"../member/MemInfo.aspx?PID=42&MemCard="+Eval("MemCard")%>'
                                                runat="server">
                                                <%# Eval("MemCard")%>
                                            </a>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("MemName")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("MemMobile")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# getMoneyFormat(Eval("MemMoney"))%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# this.getPointNum(Eval("MemPoint"))%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# this.getMoneyFormat(Eval("ConsumeMoney"))%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# this.GetValueString(Eval("RecommendCount"), "{0:F0}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# this.getPointNum(Eval("RecommendPoint"))%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("LevelName")%>
                                        </td>
                                        <td style="text-align: center">
                                            <%# this.GetMemState(int.Parse(Eval("MemState").ToString()))%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# this.GetValueString(Eval("MemBirthday"), "{0:yyyy-MM-dd}") == this.GetValueString(DateTime.Parse("1900-01-01"), "{0:yyyy-MM-dd}") ? "<font color='red'>未设置</font>" : this.GetValueString(Eval("MemBirthday"), "{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# this.GetValueString(Eval("MemCreateTime"), "{0:yyyy-MM-dd}")%>
                                        </td>
                                    </tr>
                                    <tr style="display: none;" id="data<%# Eval("MemID") %>">
                                        <td colspan="14">
                                            <asp:Repeater ID="rptExpenseDetail" runat="server">
                                                <HeaderTemplate>
                                                    <div style="padding-left: 50px;">
                                                        <table class="table-style table-hover user_List_txt" cellspacing="0" cellpadding="2"
                                                            style="width: 80%;">
                                                            <tr class="th">
                                                                <th>
                                                                    序号
                                                                </th>
                                                                <th>
                                                                    会员卡号
                                                                </th>
                                                                <th>
                                                                    会员名称
                                                                </th>
                                                                <th>
                                                                    手机号码
                                                                </th>
                                                                <th>
                                                                    贡献积分
                                                                </th>
                                                            </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr class="td">
                                                        <td style="text-align: center">
                                                            <%# Eval("Rowid")%>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%# Eval("MemCard")%>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%# Eval("MemName")%>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <%# Eval("MemMobile")%>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <%# this.getPointNum(Eval("SumNumber"))%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table></div>
                                                </FooterTemplate>
                                            </asp:Repeater>
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
    <uc2:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
