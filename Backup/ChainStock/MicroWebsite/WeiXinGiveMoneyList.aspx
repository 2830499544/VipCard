<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXinGiveMoneyList.aspx.cs" Inherits="ChainStock.MicroWebsite.WeiXinGiveMoneyList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>

    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>

    <script src="../Scripts/Module/MicroWebsite/WeiXinMoneyList.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmPromotions" runat="server">
        <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
            <tr>
                <td colspan="2" style="width: 100%;">
                    <div class="system_Info">

                        <div class="system_Top">
                            <div class="user_regist_title">
                                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                            </div>
                        </div>

                        <div id="PromotionsInfo" style="display:none;">
                            <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 700px; margin: auto">
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>红包活动标题：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtPromotionsTitle" type="text" class="input_txt border_radius" style="width:550px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        有效期：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label style="vertical-align: text-bottom;">
                                            <input type="radio" name="radPromotionsYesOrNo" id="radPromotionsYes" value="0" checked="checked" />
                                                <label style="vertical-align: middle;" for="radPromotionsYes">永久有效</label>
                                        </label>

                                        <label style="vertical-align: text-bottom;">
                                                <input type="radio" name="radPromotionsYesOrNo" id="radPromotionsNo" value="1" />
                                                <input id="txtPromotionsStartTime" type="text" class="Wdate border_radius" style="float: none;" />
                                                <label style="vertical-align: middle;">&nbsp;至&nbsp;</label>
                                                <input id="txtPromotionsEndTime" type="text" class="Wdate border_radius" style="float: none;" />
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        优惠对象：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select name="select" id="sltPromotionsLevel" runat="server" class="selectWidth" style="width: 150px;">
                                            
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <input id="btnPromotionsSave" type="button" class="buttonColor" value="保   存 " />
                                        &nbsp
                                        <input id="btnPromotionsReset" type="button" class="buttonRest" value="重   置 " />
                                        <input type="hidden" id="txtType" />
                                    </td>
                                </tr>
                            </table>
                    </div>

                        <div class="user_List_All">
                            <div class="user_List_top">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                                    <tr style="color: #333333; background-color: #F7F6F3;">
                                        <td class="user_List_styleLeft">
                                            快捷操作：
                                        </td>
                                       <td class="user_List_styleRight">
                                            <div class="user_List_Button">
                                                  <asp:Button ID="btnGoodsExpenseExcel" runat="server" Text="导   出" class="common_Button"
                                                    OnClick="btnWeiXinGiveMoneyExcel_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <table class="table-style table-hover user_List_txt">
                                <asp:Repeater runat="server" ID="rptMoneyList">
                                    <HeaderTemplate>
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
                                                   所属店铺
                                                </th>
                                                <th>
                                                    领取金额
                                                </th>
                                                <th>
                                                    领取时间
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
                                                <%# Eval("MemCard")%>
                                            </td>
                                            <td style="text-align: center">
                                               <%# Eval("MemName")%>
                                            </td>
                                            <td style="text-align: center">
                                                <%# Eval("ShopName")%>
                                            </td>
                                            <td style="text-align: center">
                                                <%# Eval("GiveMoney")%>
                                            </td>
                                            <td>
                                                 <%# Eval("GiveTime")%>
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
