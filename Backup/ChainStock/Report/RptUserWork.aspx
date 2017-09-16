<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptUserWork.aspx.cs" Inherits="ChainStock.Report.RptUserWork" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register src="../Controls/QuickSearch.ascx" tagname="QuickSearch" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
     <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

      <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            document.onkeydown = function (event) {
                e = event ? event : (window.event ? window.event : null);
                if (e.keyCode == 13) {
                    return false;
                }
            };
        });
    </script>
</head>
<body>
    <form id="frmPointRankList" runat="server">
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
                            <table class="tableStyle" style="width: 100%">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <asp:Button ID="btnRptUserWorkExcel" runat="server" Text="导   出" class="common_Button"
                                                OnClick="btnPointRankExcel_Click" />
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
                                <td class="tableStyle_right" style="width: 200px;">
                                    <input id="txtUserName" type="text" runat="server" class="input_txt border_radius"
                                        title="管理员姓名/编号" />
                                </td>

                                <td class="tableStyle_left">
                                    所属商家：
                                </td>
                                <td class="tableStyle_right" style="width: 500px;">
                                    <select id="sltShop" runat="server" class="selectWidth">
                                    </select>
                                    <input  id="HDsltshop" runat="server" type="hidden" />
                                </td>

                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnUserSearch" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnUserSearch_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvPointRankList">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                交班人
                                            </th>
                                            <th>
                                                接班人
                                            </th>
                                            <th>
                                                交班时间
                                            </th>
                                            <th>
                                                新增会员数
                                            </th>
                                            <th>
                                                应收总金额
                                            </th>
                                            <th>
                                                实收总金额
                                            </th>
                                            <th>
                                                转入余额
                                            </th>
                                            <th>
                                                银联消费支付金额
                                            </th>
                                            <th>
                                                银联充值金额
                                            </th>
                                            <th>
                                                是否结余
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
                                            <%# Eval("UserName")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# GetUserName(Convert.ToInt32(Eval("HandoverUserID")))%>
                                        </td>
                                        <td>
                                            <%# Eval("EedTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("AddNewUser")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("AllMoneys","{0:F2}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("sjMoneys", "{0:F2}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("Arrearage", "{0:F2}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("ExpenseBinkMoneys", "{0:F2}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("RechargeBank", "{0:F2}")%>
                                        </td>
                                        <td>
                                            <%# Boolean.Parse(Eval("Ispay").ToString()) ? "是" : "否"%>
                                            <%--<asp:CheckBox runat="server" ID="ckbIspay" Checked='<%# Eval("Ispay") %>' />--%>
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
    <uc1:QuickSearch ID="QuickSearch2" runat="server" />
    </form>
</body>
</html>
