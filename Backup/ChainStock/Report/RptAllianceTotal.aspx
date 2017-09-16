<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptAllianceTotal.aspx.cs"
    Inherits="ChainStock.Report.RptAllianceTotal" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="../Controls/QuickSearch.ascx" tagname="QuickSearch" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/highcharts.js" type="text/javascript"></script>
    <script src="../Scripts/grid.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>
    


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
    <form id="frmRptPointChange" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">

                  <%--打印的次数 --%>
                    <input type="hidden" value="" id="PointNum" runat="server"/>

                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div class="user_List_All">
                        <div id="ChartShow" style="width: 800px; display: none;">
                            <div id="ChartSerch">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                    class="tableStyle">
                                    <tr>
                                        <td class="tableStyle_left">
                                            变动时间：
                                        </td>
                                        <td class="tableStyle_right">
                                            <input id="txtMemStartTime" runat="server" type="text" class="Wdate border_radius" />
                                        </td>
                                        <td class="tableStyle_left">
                                            至&nbsp;&nbsp;
                                        </td>
                                        <td class="tableStyle_right">
                                            <input id="txtMemEndTime" runat="server" type="text" class="Wdate border_radius" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            所属商家：
                                        </td>
                                        <td class="tableStyle_right" colspan="2">
                                            <select id="sltShopChart" runat="server" class="selectWidth">
                                            </select>
                                            <input  id="HDsltshop" runat="server" type="hidden" />
                                        </td>
                                        <td class="tableStyle_right">
                                            <div class="user_List_Button">
                                             <asp:Button ID="btSerch" runat="server"
                                                    Text="查   询" class="common_Button" />
                                            
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="chart">
                                <div id="container" style="width: 800px;">
                                </div>
                            </div>
                        </div>
                        <div id="ReportSerch">
                            <div class="user_List_top">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                    class="tableStyle">
                                    <tr style="color: #333333; background-color: #F7F6F3;">
                                        <td class="user_List_styleLeft">
                                            快捷操作：
                                        </td>
                                        <td class="user_List_styleRight">
                                            <div class="user_List_Button">
                                              
                                                <asp:Button ID="btnRptPointChangeExcel" runat="server" OnClientClick="return btQuery();"
                                                    Text="导   出" class="common_Button" OnClick="btnRptPointChangeExcel_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr>
                                   
                                    <td class="tableStyle_left">
                                        所属商家：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltShop" runat="server" class="selectWidth">
                                        </select>
                                    </td>
                                    <td style=" border:none">
                                    </td>
                               
                                    <td class="tableStyle_left">
                                        加盟时间：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtStartTime" type="text" runat="server" class="Wdate border_radius" />
                                    </td>
                                    <td class="tableStyle_left">
                                        至&nbsp;&nbsp;
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtEndTime" type="text" runat="server" class="Wdate border_radius" />
                                    </td>
                                     <td class="tableStyle_right">
                                        <div class="user_List_Button">
                                            <asp:Button ID="btnRptPointChangeQuery"  runat="server"
                                                Text="查   询" class="common_Button" OnClick="btnRptPointChangeQuery_Click" />
                                        </div>
                                    </td>
                                    <td class="tableStyle_left">
                                    
                                    </td>
                                    <td class="tableStyle_right">
                                        
                                      
                                    </td>
                                   
                                </tr>
                            </table>
                        </div>
                        
                        <div id="report">
                            <table class="table-style table-hover user_List_txt" id="tbGoods" cellspacing="0"
                                cellpadding="2">
                                <asp:Repeater runat="server" ID="gvRptPointChange">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    序号
                                                </th>
                                                <th>
                                                    联盟商名称
                                                </th>
                                                <th>
                                                    积分余额
                                                </th>
                                                 <th>
                                                    短信余额
                                                </th>
                                                <th>
                                                     是否锁定
                                                </th>
                                                <th>
                                                    注册会员
                                                </th>
                                                <th>
                                                    注册商家
                                                </th>
                                                <th>
                                                    加盟时间
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
                                                <%# Eval("ShopName")%>
                                            </td>
                                          
                                             <td>
                                                <%# Eval("PointCount")%>
                                            </td>
                                              <td>
                                                <%# Eval("SmsCount")%>
                                            </td>
                                              <td>
                                              <%# Boolean.Parse(Eval("ShopState").ToString()) ? "是" : "否"%>
                                            </td>
                                            <td>
                                              <%# Eval("MemCount")%>
                                            </td>
                                            <td>
                                              <%# Eval("ShopCount")%>
                                            </td>
                                            <td>
                                              <%# Eval("ShopCreateTime")%>
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
                </div>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblPrintTitle" Style="display: none" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="lblPrintFoot" Style="display: none" runat="server" Text="Labe2"></asp:Label>
    <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
