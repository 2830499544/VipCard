<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysRotatePrizeLog.aspx.cs" Inherits="ChainStock.MicroWebsite.SysRotatePrizeLog" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/MicroWebsite/SysRotatePrizeLog.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmMicroExpHistory" runat="server">
    <input type="hidden" runat="server" id="txtUser" />
    <input type="hidden" runat="server" id="txtShopid" />
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
                        <div style="display: none;" id="divBackExchange">
                            <div class="user_regist_Allleft" style="width: 500px">
                                <div class="user_regist_left" style="width: auto">
                                    <table class="tableStyle" cellspacing="0" cellpadding="0" style="width: 500px;">
                                        <tr>
                                            <td class="tableStyle_left">
                                                退回备注：
                                            </td>
                                            <td class="tableStyle_right">
                                                <textarea id="txtExchangeRemark" style="width: 410px; margin-left: 5px; word-wrap: break-word;
                                                    height: 80px;"></textarea>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: center">
                                                <input id="btnOK" type="button" class="buttonColor" value="确   定" />&nbsp;&nbsp;
                                                <input id="btnCancel" type="button" class="buttonRest" value="取   消" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
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
                                                  <asp:Button ID="btnExpenseExcel" runat="server" Text="导   出" class="common_Button"
                                                    OnClick="btnExpenseExcel_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
   
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater ID="gvSysRotatePrizeLog" runat="server" OnItemDataBound="rptExpenseHistory_ItemDataBound">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                中奖单号
                                            </th>
                                           
                                            <th>
                                                会员姓名
                                            </th>
                                            <th>
                                                会员卡号
                                            </th>
                                            <th>
                                                会员等级
                                            </th>
                                             <th>
                                                所属店铺
                                            </th>
                                            <th>
                                                中奖等级
                                            </th>
                                             <th>
                                                中奖时间
                                            </th>
                                            <th>
                                                获得奖品
                                            </th>  
                                              <th>
                                                兑换码
                                            </th>                                         
                                            <th>
                                                奖品领取状态
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
                                            <asp:Label runat="server" ID="lblNumber"></asp:Label>
                                        </td>
                                       
                                           <td>    
                                             <%#Eval("PrizeAccount")%>
                                        </td>
                                        <td >
                                          <%#Eval("MemName")%>
                                        </td>
                                        <td>
                                            <%#Eval("MemCard")%>
                                        </td>
                                         <td>
                                            <%#Eval("LevelName")%>
                                        </td>
                                         <td>
                                            <%#Eval("ShopName")%>
                                        </td>
                                        <td style="color: Red;">
                                            <%#Eval("PrizeLevel")%>
                                        </td>
                                        <td>
                                            <%#Eval("CreateTime")%>
                                        </td>
                                        <td>
                                       <%#BindPrizeName(Eval("PrizeLevel"), Eval("RotateID"))%>
                                        </td>
                                        <td style=" color:Red">
                                          <%#(Eval("PrizeCode"))%>
                                        </td>
                                        <td style=" color:Red">
                                          <%#BindStatus(Eval("PrizeStatus"))%>
                                        </td>
                                       
                                       
                                        <td class="listtd" style="width: 60px;">
                                       
                                              <span style='display: <%#Convert.ToInt32(Eval("PrizeStatus"))==0?"":"none" %>'> <a href="#" id="hyGive" runat="server"   onclick='<%# string.Format(" GivePrize(\"{0}\",\"{1}\")",Eval("PrizeLogID"),Eval("MemName")) %>'>
                                        
                                                <img src="../images/Gift/isok.png" alt="发放奖品" title="发放奖品" /></a> </span>
                                               
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
                        <input id="chkSMS" runat="server" type="checkbox" style="display: none;" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
