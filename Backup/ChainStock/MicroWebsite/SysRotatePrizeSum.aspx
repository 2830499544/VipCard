<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysRotatePrizeSum.aspx.cs" Inherits="ChainStock.MicroWebsite.SysRotatePrizeSum" %>
<%@ Register Src="../Controls/QuickSearch.ascx" TagName="QuickSearch" TagPrefix="uc1" %>
<%@ Register Src="../Controls/SysArea.ascx" TagName="SysArea" TagPrefix="uc2" %>
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
        <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
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
                       
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    所属店铺：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltShop" runat="server" class="selectWidth">
                                    </select>
                                    <input id="HDsltshop" runat="server" type="hidden" />
                                </td>
                                <td class="tableStyle_left">
                                    所在地址：
                                </td>
                                <td class="tableStyle_right">
                                       <uc2:SysArea ID="SysArea1" runat="server" />
                                </td>
                             
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnShopSearch" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnRptExpenseQuery_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                           
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                           
                                           
                                            <th>
                                                会员姓名
                                            </th>
                                            <th>
                                                会员卡号
                                            </th>
                                             <th>
                                                所属店铺
                                            </th> 
                                             <th>
                                                地址
                                            </th> 
                                            <th>
                                                会员等级
                                            </th>                                         
                                            <th>
                                                抽奖次数  
                                                
                                              
                                                 <asp:ImageButton ID="ImgArrow1" runat="server"  ImageUrl="~/images/Gift/arrow-down.gif" OnClick="imgArrow1_Click"/>
                                            </th>  
                                              <th>
                                                中奖次数   <asp:ImageButton ID="ImgArrow2" runat="server"  ImageUrl="~/images/Gift/arrow-down.gif" OnClick="imgArrow2_Click"/>
                                            </th>                                         
                                                   <th>
                                                中奖明细
                                            </th>                                          
                                           
                                        </tr>
                                    </thead>
                                     <asp:Repeater ID="gvSysRotatePrizeLog" runat="server" OnItemDataBound="rptExpenseHistory_ItemDataBound">
                               
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                            <asp:Label runat="server" ID="lblNumber"></asp:Label>
                                        </td>
                                       
                                     
                                        <td >
                                          <%#Eval("MemName")%>
                                        </td>
                                        <td>
                                            <%#Eval("MemCard")%>
                                        </td>
                                         <td>
                                            <%#Eval("ShopName")%>
                                        </td>
                                         <td>
                                            <%#Eval("MemProvinceName")%><%#Eval("MemCityName")%><%#Eval("MemCountyName")%><%#Eval("MemVillageName")%><%#Eval("MemAddress")%>
                                        </td>
                                         <td>
                                            <%#Eval("LevelName")%>
                                        </td>
                                         <td>
                                            <%#Eval("TotalCount")%>
                                          
                                        </td>
                                           <td>
                                            <%#Eval("WinCount")%>
                                        </td>
                                      
                                       <td>    <a  href='SysRotatePrizeLog.aspx?RotateID=<%=RotateID%>&MemID=<%#Eval("MemID") %>'  id="A1">
                                                    <img src="../images/ico/chart_bar.png" alt="中奖明细" title="中奖明细" />中奖明细</a></td>
                                       
                                      
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
        <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
