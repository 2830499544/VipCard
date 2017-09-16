<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysRotateCount.aspx.cs"
    Inherits="ChainStock.SystemManage.SysRotateCount" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="../Controls/QuickSearch.ascx" TagName="QuickSearch" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/MicroWebsite/SysRotateCount.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
        <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script> 
</head>
<body>
    <form id="frmStaffClassList" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div id="divSysRotateCount" style="display: none">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 400px; margin: auto">
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>消费额度：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtCostAmount" type="text" runat="server" class="border_radius" />
                                    <input id="txtID" type="hidden" runat="server" />
                                     <input id="txtRotateID" type="hidden" runat="server" />
                                </td>
                                </tr>
                                <tr>
                                <td class="tableStyle_left">
                                     <span style="color: #ff4800; vertical-align: middle">*</span>可抽奖次数：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtRotateCount" type="text" runat="server" class="border_radius" />
                                </td>
                            </tr>
                                                         <tr>
                                <td class="tableStyle_left">
                                     <span style="color: #ff4800; vertical-align: middle">*</span>开始时间：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtRotateStartTime" type="text" runat="server"   class="Wdate border_radius" onclick="WdatePicker({ skin: 'ext', isShowClear: true, readOnly: true})" />
                                </td>
                            </tr>
                                      <tr>
                                <td class="tableStyle_left">
                                     <span style="color: #ff4800; vertical-align: middle">*</span>结束时间：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtRotateEndTime" type="text" runat="server"   class="Wdate border_radius" onclick="WdatePicker({ skin: 'ext', isShowClear: true, readOnly: true})" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="tableStyle_right">
                                    <div class="buton" style="text-align: center;">
                                        <input id="btnSave" type="button" class="buttonColor" value="保   存 " />
                                        <input id="btnReset" type="button" class="buttonRest" value="重   置 " />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="user_List_All">
                        <div class="user_List_top">
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <input id="btnSysRotateCountAdd" type="button" value="新增" runat="server" class="common_Button" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                       
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvSysRotateCountList">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                消费金额
                                            </th>
                                            <th>
                                                可抽奖次数
                                            </th>
                                               <th>
                                                开始时间
                                            </th>
                                               <th>
                                                结束时间
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
                                        <td>
                                            <%# Eval("CostAmount")%>
                                        </td>
                                        <td >
                                            <%# Eval("RotateCount")%>
                                        </td>
                                         <td >
                                        
                                            <%#  string.Format("{0:yyyy-MM-dd}", Eval("StartTime"))%>
                                        </td>
                                          <td >
                                         <%#  string.Format("{0:yyyy-MM-dd}", Eval("EndTime"))%>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <a href="#" onclick='<%# string.Format(" SysRotateCountEdit(\"{0}\")",Eval("ID")) %>'
                                                id="btnSysRotateCountEdit" runat="server">
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" />
                                            </a><a href="#" onclick='<%# string.Format(" SysRotateCountDelete(\"{0}\",\"{1}\")",Eval("ID"),Eval("CostAmount")) %>'
                                                id="btnSysRotateCountDel" runat="server">
                                                <img src="../images/Gift/del.png" alt="删除" title="删除" />
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
