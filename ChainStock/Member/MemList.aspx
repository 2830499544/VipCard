<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemList.aspx.cs" EnableEventValidation="false"
    Inherits="ChainStock.Member.MemList" %>

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
    <script src="../Scripts/Module/Mem/MemList.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
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
                        <div class="user_List_top" style="height: 67px;">
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
                                            <asp:Button ID="btnMemListQuery" runat="server" Text="查   询" class="common_Button"
                                                OnClick="btnMemListQuery_Click" />
                                            <input id="btnhigthSearch" type="button" class="common_Button" value="高级查询" />
                                            <asp:Button ID="btnOut" runat="server" Text="导   出" UseSubmitBehavior="false" class="common_Button"
                                                OnClick="BtnMemExcel_Click" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <input id="btnMemRecharge" runat="server" cls="4" type="button" class="common_Button"
                                                value="会员充值" />
                                            <input id="btnMemChangeCard" runat="server" cls="6" type="button" class="common_Button"
                                                value="会员换卡" />
                                            <input id="btnMemDelay" runat="server" cls="7" type="button" class="common_Button"
                                                value="会员延期" />
                                            <input id="btnMemLock" runat="server" cls="8" type="button" class="common_Button"
                                                value="挂失锁定" />
                                            <input id="btnMemChangePwd" cls="9" runat="server" type="button" class="common_Button"
                                                value="修改密码" />
                                            <input id="btnMemDrawMoney" cls="10" runat="server" type="button" class="common_Button"
                                                value="帐户提现" />
                                            <input id="btnSenndSMS" cls="46" runat="server" type="button" class="common_Button"
                                                value="发送短信" />
                                            <input id="btnMemPointReset" runat="server" type="button" class="common_Button" value="积分清零" />
                                            <input id="btnAllMemPointReset" runat="server" type="button" class="common_Button"
                                                value="全分清零"  visible="false" />
                                            <input id="btnCoupon" runat="server" type="button" class="common_Button" value="发优惠券" />
                                            <asp:Button ID="btnMoney" runat="server" Text="发放红包" class="common_Button" OnClick="btnMoney_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:CheckBox ID="chkSms" runat="server" Style="display: none" />
                        <div id="divCoupon" style="display: none; width: 600px; height: 300px">
                            <table class="tableStyle">
                                <tr>
                                    <th style="text-align: center; font-weight: bold; height: 25px" class="th" colspan="4">
                                        优惠券信息
                                    </th>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        优惠券名称：
                                    </td>
                                    <td class="tableStyle_right" colspan="3">
                                        <select id="sltCoupon" runat="server" class="selectWidth" onchange="CouponChange();">
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        优惠有效期：
                                    </td>
                                    <td class="tableStyle_right" colspan="3">
                                        <asp:Label ID="lblCouponTime" runat="server" Text="永久有效"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        最低消费：
                                    </td>
                                    <td class="tableStyle_right">
                                        <asp:Label ID="lblCouponMinMoney" runat="server" Text="0"></asp:Label>&nbsp;
                                    </td>
                                    <td class="tableStyle_left">
                                        单日限用：
                                    </td>
                                    <td class="tableStyle_right">
                                        <asp:Label ID="lblCouponDayNum" runat="server" Text="0"></asp:Label>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        已发数量：
                                    </td>
                                    <td class="tableStyle_right">
                                        <asp:Label ID="lblCouponYF" runat="server" Text="0"></asp:Label>&nbsp;
                                    </td>
                                    <td class="tableStyle_left">
                                        剩余数量：
                                    </td>
                                    <td class="tableStyle_right">
                                        <asp:Label ID="lblCouponNu" runat="server" Text="0"></asp:Label>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <th style="text-align: center; font-weight: bold; height: 25px" class="th" colspan="4">
                                        会员信息
                                    </th>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        已选会员：
                                    </td>
                                    <td class="tableStyle_right">
                                        <asp:Label ID="lblChoosedMem" runat="server" Text="0"></asp:Label>&nbsp;
                                    </td>
                                    <td class="tableStyle_left">
                                        挂失锁定会员：
                                    </td>
                                    <td class="tableStyle_right">
                                        <asp:Label ID="lblLockMem" runat="server" Text="0"></asp:Label>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        无手机号码：
                                    </td>
                                    <td class="tableStyle_right">
                                        <asp:Label ID="lblNoMobile" runat="server" Text="0"></asp:Label>&nbsp;
                                    </td>
                                    <td class="tableStyle_left">
                                        预发会员数量：
                                    </td>
                                    <td class="tableStyle_right">
                                        <asp:Label ID="lblSendNumber" runat="server" Text="0"></asp:Label>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Button ID="btnSendCoupon" runat="server" Text="发送" class="buttonColor" />
                                        <asp:Button ID="btnCloseCoupon" runat="server" Text="关闭" class="buttonRest" />
                                        <input id="counponType" type="hidden" />
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
                                          <input  id="HDsltshop" runat="server" type="hidden" />
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
                                        <asp:Literal ID="ltCustom" runat="server">
                                        </asp:Literal>
                                        <%-- <input id="txtCustomField" type="text" runat="server" class="input_txt border_radius" />--%>
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
                                    <td colspan="5" class="user_List_styleRight" style="vertical-align: text-bottom;">
                                        <uc1:SysArea ID="SysArea1" runat="server" />
                                    </td>
                                </tr>

                                 <tr>
                                    <td class="tableStyle_left">
                                        微信绑定：
                                    </td>
                                    <td class="user_List_styleRight" style="vertical-align: text-bottom;">
                                        <select id="selMemWeiXinCard" runat="server" class="selectWidth" name="D4">
                                            <option value="0">===== 请选择 =====</option>
                                            <option value="1">已绑定</option>
                                            <option value="2">未绑定</option>
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        微信关注：
                                    </td>
                                    <td class="user_List_styleRight" style="vertical-align: text-bottom;">
                                        <select id="selMemAttention" runat="server" class="selectWidth" name="D4">
                                            <option value="0">===== 请选择 =====</option>
                                            <option value="1">已关注</option>
                                            <option value="2">未关注</option>
                                        </select>
                                    </td>
                                    <td colspan="2">
                                        <input id="btnMemReset" type="button" value="重   置"  />
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
                                        <input id="chkAll" runat="server" type="checkbox" class="chkAll" onclick="SelectAll()" />
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
                                        剩余计次
                                    </th>
                                    <th>
                                        历史消费金额
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
                                    <th>
                                        办卡人员
                                    </th>
                                     <th>
                                        微信绑定
                                    </th>
                                    <th>
                                    微信关注
                                    </th>
                                    <asp:Literal runat="server" ID="ltlHeader"></asp:Literal>
                                    <th>
                                        操作
                                    </th>
                                </tr>
                            </thead>
                            <asp:Repeater runat="server" ID="gvMemList" OnItemDataBound="gvMemList_ItemDataBound">
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                            <asp:Literal runat="server" ID="ltlMemID" runat="server" Text='<%# Eval("MemID")%>'
                                                Visible="false"></asp:Literal>
                                        </td>
                                        <td>
                                            <input id="chkItem" runat="server" type="checkbox" class="chk" />
                                        </td>
                                        <td>
                                            <a href='<%#"MemInfo.aspx?PID=42&MemCard="+Eval("MemCard")%>' runat="server">
                                                <%# Eval("MemCard")%>
                                            </a>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("MemName")%>
                                        </td>
                                        <td>
                                            <%# Eval("MemMobile")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("MemMoney", "{0:F2}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("MemPoint")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("MemCountNumber")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# decimal.Parse(Eval("ConsumeMoney").ToString()).ToString("0.00")%>
                                        </td>
                                        <td>
                                            <%# Eval("LevelName")%>
                                        </td>
                                        <td>
                                            <%# this.GetMemState(int.Parse(Eval("MemState").ToString()))%>
                                        </td>
                                        <td>
                                            <%# this.GetMemBirthday(DateTime.Parse(Eval("MemBirthday").ToString())) %>
                                        </td>
                                        <td>
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td>
                                            <%# Eval("MemCreateTime", "{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                        <td>
                                            <%# Eval("MemWeiXinCard").ToString() == "" ? "未绑定" : "已绑定"%>
                                        </td>
                                        <td>
                                        <%# (string.IsNullOrEmpty(Eval("MemAttention").ToString())?0:Convert.ToInt32(Eval("MemAttention").ToString()))==1?"已关注":"未关注"%>
                                        </td>

                                        <asp:Literal runat="server" ID="ltlHtml"></asp:Literal>
                                        <td class="listtd" style="width: 60px;">
                                            <a href='<%#"MemRegister.aspx?PID=2&MemID="+Eval("MemID")%>' id="hyEdit" runat="server">
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" />
                                            </a><a href="#" id="hyDel" runat="server" onclick='<%# string.Format("return DeleteMem(\"{0}\",\"{1}\")",Eval("MemName"),Eval("MemID")) %>'>
                                                <img src="../images/Gift/del.png" alt="删除" title="删除" /></a>
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
