<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PointDrawVerify.aspx.cs"
    Inherits="ChainStock.PointManage.PointDrawVerify" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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

    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
        <script src="../Scripts/Module/PointManage/PointDrawVerify.js" type="text/javascript"></script>
               <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmExchangeVerify" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div class="user_List_All">
                        <div style="margin-top: 5px; display: none;" id="divBackExchange">
                            <table>
                                <tr>
                                    <td class="tableStyle_left">
                                        退回备注：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtExchangeRemark" type="text" runat="server" class="border_radius" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <input id="btnOK" type="button" class="buttonColor" value="确   定" />
                                        <input id="btnCancel" type="button" class="buttonRest" value="取   消" />
                                    </td>
                                </tr>
                            </table>
                        </div>  <input type="text" id="txtDrawType" style="width: 33px; display: none" runat="server" />

                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    单号：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopSmsAccount" type="text" runat="server" class="input_txt border_radius"
                                        title="提现单号" />
                                </td>
                                <td class="tableStyle_left">
                                    提现商家：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltShop" runat="server" class="selectWidth">
                                    </select>
                                </td>
                               <td class="tableStyle_left">
                                    提现状态：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltCzlx" runat="server" class="selectWidth">
                                        <option title="请选择" value="">=====请选择=====</option>
                                        <option title="待审核" value="0">待审核</option>
                                        <option title="审核通过" value="1">审核通过</option>
                                    
                                    </select>
                                </td>
                                 <tr>
                                  <td class="tableStyle_left">
                                    申请时间：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtStartTime" type="text" runat="server" class="Wdate border_radius" onclick="WdatePicker({ skin: 'ext', isShowClear: true, readOnly: true})" />
                                </td>
                                <td class="tableStyle_left">
                                    &nbsp;至&nbsp;&nbsp;&nbsp;
                                </td>
                                  <td class="tableStyle_right">
                                    <input id="txtEndTime" type="text" runat="server" class="Wdate border_radius"  onclick="WdatePicker({ skin: 'ext', isShowClear: true, readOnly: true})" />
                                </td>
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnShopSmsQuery" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnPointDrawQuery_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>                    
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater ID="rptExchangeVerify" runat="server" OnItemDataBound="rptMemGiftList_ItemDataBound">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号   
                                              
                                            </th>
                                            <th>
                                                提现单号
                                            </th>
                                          
                                            <th>
                                               提现对象
                                            </th>
                                            <th>
                                                提现积分
                                            </th>
                                            <th>
                                                提现金额
                                            </th>
                                              <th>
                                                状态
                                            </th>
                                            <th>
                                                申请人
                                            </th>
                                            <th>
                                                申请日期
                                            </th>
                                            <th style="display:none;">
                                                审核人
                                            </th>
                                            <th style="display:none;">
                                                审核日期
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
                                            <%# Eval("DrawAccount") %>
                                        </td>
                                     
                                        <td>
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td style="color:Red;">
                                            <%# Eval("DrawPoint")%>
                                        </td>
                                        <td>
                                         <%# Eval("DrawAmount")%>
                                           
                                        </td>
                                        <td>
                                     <%--   <%# Eval("DrawStatus")%>--%>
                                        <asp:Label ID="lblDrawStatus" runat="server" Text="申请中..."></asp:Label>
                                        </td>
                                         <td><%# Eval("UserName")%></td>
                                        <td><%# Eval("DrawCreateTime")%></td>
                                       <%--  <td><%# Eval("VerifyName")%></td>
                                        <td><%# Eval("DrawVerifyTime")%></td>--%>
                                        
                                        <td class="listtd" style="width: 60px;" id="tdHandle" runat="server">                                          
                                        <span <%# Eval("DrawStatus").ToString()=="1"?"style='display:none;'":""%> >
                                            <a id="AllowExchange" runat="server" href='#' onclick='<%# string.Format(" AllowExchange(\"{0}\",\"{1}\")",Eval("DrawID"),Eval("DrawAccount")) %>'>
                                                <img src="../images/Gift/isok.png" alt="通过审核" title="通过审核" />
                                            </a>
                                            </span>                                                
                                     <%--       <a id="NoExchange" runat="server" href='#' onclick='<%# string.Format(" NoExchange(\"{0}\")",Eval("DrawID")) %>'>
                                                <img src="../images/Gift/back.png" alt="退回" title="退回" /></a>--%>
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
