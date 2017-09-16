<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopPointSettlement.aspx.cs"
    Inherits="ChainStock.SystemManage.ShopPointSettlement" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
    <script src="../Scripts/Module/SystemManage/ShopPointSettlement.js" type="text/javascript"></script>

        <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmShopSettlement" runat="server">    
    <input type="hidden" id="hdShopID" runat="server" />
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
                        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr> <input id="txtShopType" type="hidden" runat="server" />
                                <td class="tableStyle_left">
                                    商家：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltShop" runat="server" class="selectWidth">
                                    </select>
                                </td>
                                <td class="tableStyle_left">
                                    状态：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltIsFinish" runat="server" class="selectWidth">
                                        <option value="">=====请选择=====</option>
                                        <option value="0">未结算</option>
                                        <option value="1">已结算</option>
                                    </select>
                                </td>
                                  <td class="tableStyle_left">
                                    结算时间：
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
                                        <asp:Button ID="btnRptMemQuery" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnRptMemQuery_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div id="EShopSettlement" style="display: none;">
                            <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 600px; margin: auto">
                                <tr>
                                    <td class="tableStyle_left" style="width: 150px;">
                                        商家名称：
                                    </td>
                                    <td class="tableStyle_right" style="width: 150px;">
                                        <label id="lbShopName">
                                        </label>
                                        <input type="hidden" id="MyID" />
                                    </td>
                                    <td class="tableStyle_left" style="width: 150px;">
                                        充值总积分：
                                    </td>
                                    <td class="tableStyle_right" style="width: 150px;">
                                        <label id="lblRechargePoint">
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left" style="width: 150px;">
                                        扣减总积分：
                                    </td>
                                    <td class="tableStyle_right" style="width: 150px;">
                                        <label id="lblDeductionPoint">
                                        </label>
                                    </td>
                                    <td class="tableStyle_left" style="width: 150px;">
                                       发放总积分：
                                    </td>
                                    <td class="tableStyle_right" style="width: 150px;">
                                       <label id="lblGivePoint">
                                        </label>
                                    </td>
                                </tr>
                                 <tr>
                                   
                                    <td class="tableStyle_left" style="width: 150px;">
                                      回收总积分:
                                    </td>
                                    <td class="tableStyle_right" style="width: 150px;">
                                        <label id="lblReturnPoint">
                                        </label>
                                    </td>
                                     <td class="tableStyle_left" style="width: 150px;">
                                         返利总积分：
                                    </td>
                                    <td class="tableStyle_right" style="width: 150px;">
                                         <label id="lblFanliPoint">
                                        </label>
                                    </td>
                            </tr>
                                <tr>
                                    <td class="tableStyle_left" style="width: 150px;">
                                        撤单返还总积分：
                                    </td>
                                    <td class="tableStyle_right" style="width: 150px;">
                                        <label id="lblReturnOrderPoint">
                                        </label>
                                    </td>
                                    <td class="tableStyle_left" style="width: 150px;">
                                        提现积分：
                                    </td>
                                    <td class="tableStyle_right" style="width: 150px;">
                                        <label id="lblDrawPoint">
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        开始时间：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label id="lbStartTime">
                                        </label>
                                    </td>
                                    <td class="tableStyle_left">
                                        结束时间：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label id="lbEndTime">
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        备注：
                                    </td>
                                    <td colspan="3" class="tableStyle_right">
                                        <textarea id="txtRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                        outline: none; resize: none;"></textarea>
                                    </td>                                    
                                </tr>
                                <tr id="btoperate">
                                    <td colspan="4" style="text-align: center;">
                                        <input type="button" id="btSave" value="结 算" class="buttonColor" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="button" id="btCancel" value="取 消 " class="buttonColor" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvShopSettlement" OnItemDataBound="gvShopSettlement_ItemDataBound">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                商家名称
                                            </th>
                                            <th>
                                                开始时间
                                            </th>
                                            <th>
                                                结束时间
                                            </th>
                                            <th>
                                                充值总积分
                                            </th>
                                            <th>
                                                扣减总积分
                                            </th>
                                              <th>
                                                发放总积分
                                            </th>
                                            <th>
                                                回收总积分
                                            </th>
                                              <th>
                                                返利总积分
                                            </th>
                                            <th>
                                                撤单返还总积分
                                            </th>
                                            <th>
                                                提现积分
                                            </th>
                                            
                                            <th>
                                                是否已结算
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
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("StartTime")%>
                                        </td>
                                        <td>
                                            <%# Eval("EndTime")%>
                                        </td>
                                        <td style="color:Green;">
                                           <%# Eval("RechargePoint")%>
                                        </td>
                                        <td style="color:Red;">
                                            <%# Eval("DeductionPoint")%>
                                        </td>
                                          <td style="color:Red;">
                                           <%# Eval("GivePoint")%>
                                        </td>
                                        <td style="color:Green;">
                                              <%# Eval("ReturnPoint")%>
                                        </td>
                                          <td style="color:Green;">
                                         <%# Eval("FanliPoint")%>
                                        </td>
                                        <td style="color:Green;">
                                      <%# Eval("ReturnOrderPoint")%>
                                        </td>
                                        <td style="color:Red;">
                                            <%# Eval("DrawPoint")%>
                                       
                                        </td>
                                   
                                        <td>
                                            <%# Boolean.Parse(Eval("IsFinish").ToString()) ? "是" : "否"%>
                                        </td>
                                        <td class="listtd" style="width: 90px;">
                                            <a href="#" runat="server" onclick='<%# string.Format(" ShopSettlement(\"{0}\",\"{1}\")",Eval("ShopName"),Eval("ID")) %>'
                                                id="hyShopSettlement">
                                                <img src="../images/Gift/eit.png" title="商家结算" alt="商家结算" />
                                            </a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="#" runat="server" style="color: Red;" onclick='<%# string.Format(" ShopSettlementLook(\"{0}\",\"{1}\")",Eval("ShopName"),Eval("ID")) %>'
                                                id="hyShopLook">
                                                <img src="../images/Gift/show.png" title="查看结算" alt="查看结算" />
                                            </a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>                        
                       <div class="user_List_page">                       
                            <table style="width: 100%" id="tabPager1">
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
