<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeList.aspx.cs" Inherits="ChainStock.MemExpense.TimeList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Skin/default/formMain.css" rel="stylesheet" />
    <link href="../Inc/Skin/default/newstyle.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/Skin/default/jqModal.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Skins.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/MemExpense/TimeList.js" type="text/javascript"></script>
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>
    <script src="../Scripts/jqModal.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="frmTiemExpense" runat="server">
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
                        <div class="user_List_top">
                            <div class="user_List_top">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                    class="tableStyle">
                                    <tr style="color: #333333; background-color: #F7F6F3;">
                                        <td class="user_List_styleLeft">
                                            快捷操作：
                                        </td>
                                        <td class="user_List_styleRight">
                                            <div class="user_List_Button">
                                                <input id="btnAddTiming" type="button" value="新增计时" runat="server" class="common_Button" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>                            
                        </div>
                        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    请输入卡号：
                                </td>
                                <td class="tableStyle_right" style=" width:200px;">
                                    <input id="txtQueryTiming" type="text" runat="server" class="border_radius" maxlength="20" title="会员卡号/令牌"/>
                                </td>
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnQueryTiming" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnQueryTiming_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvTimeExpense" OnItemCommand="gvTimeExpense_ItemCommand">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                订单编号
                                            </th>
                                            <th>
                                                消费类型
                                            </th>
                                            <th>
                                                消费服务
                                            </th>
                                            <th>
                                                会员姓名
                                            </th>
                                            <th>
                                                卡号/令牌
                                            </th>
                                            <th>
                                                开始操作员
                                            </th>
                                            <th>
                                                消费状态
                                            </th>
                                            <th>
                                                预定时长
                                            </th>
                                            <th>
                                                剩余时长
                                            </th>
                                            <th>
                                                开始时间
                                            </th>
                                            <th>
                                                结束时间
                                            </th>
                                            <th>
                                                消费详情
                                            </th>
                                            <th>
                                                结束操作员
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
                                            <%# Eval("OrderTimeCode")%>
                                            <asp:Literal runat="server" ID="ltOrderTimeID" Visible="false" Text='<%# Eval("OrderTimeID") %>'></asp:Literal>
                                            <asp:Literal runat="server" ID="ltProjectName" Visible="false" Text='<%# Eval("ProjectName") %>'></asp:Literal>
                                        </td>
                                        <td>
                                            <%# Eval("OrderMemID").ToString()=="0"?"散客消费":"会员消费"%>
                                        </td>
                                        <td>
                                            <%# Eval("ProjectName")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("MemName")%>
                                        </td>
                                        <td>
                                            <%# Eval("OrderToken")%>
                                        </td>
                                        <td>
                                            <%# Eval("StartUserName")%>
                                        </td>
                                        <td>
                                            <%# Convert.ToBoolean(Eval("OrderState")) ? "<p style='color:red;'>消费结束</p>" : "正在消费"%>
                                        </td>
                                        <td>
                                            <%# Convert.ToInt32(Eval("OrderPredictTime")) == 0 ? "未设置" : Convert.ToInt32(Eval("OrderPredictTime")).ToString()+"分钟"%>
                                        </td>
                                        <td style="width: 120px;"><!---- *********************剩余时间*******************---->
                                            <%# GetState(Convert.ToBoolean(Eval("OrderState")), Convert.ToDecimal(Eval("OrderPredictTime")), Convert.ToDateTime(Eval("OrderMarchTime"))) %>
                                        </td>
                                        <td>
                                            <%# Eval("OrderMarchTime")%>
                                        </td>
                                        <td>
                                            <%# Eval("OrderOutTime")%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("OrderRemark")%>
                                        </td>
                                        <td>
                                            <%# Eval("EndUserName")%>
                                        </td>
                                        <td class="listtd" style="width: 40px; text-align: center;">
                                            <%# Convert.ToBoolean(Eval("OrderState")) ? "<p style='color:red;'>   <img src=\"../images/Gift/isok.png\" title=\"已结算\" alt=\"已结算\" /></p>" : ""%>
                                            <asp:LinkButton runat="server" CommandName="SettleAccounts" ID="hyEndExpense" Visible='<%# !Convert.ToBoolean(Eval("OrderState"))%>'>&nbsp;<img src="../images/Gift/goexpense.png" title="转到结算"
                                                    alt="转到结算" /></asp:LinkButton>
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
    <asp:Label ID="lblPrintTitle" Style="display: none" runat="server" Text=""></asp:Label>
    <asp:Label ID="lblPrintFoot" Style="display: none" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
