<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainList.aspx.cs" Inherits="ChainStock.SystemManage.MainList" %>

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
    <script src="../Scripts/Module/SystemManage/ShopInfo.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>
    <style>
    .user_List_top label.tips { position: absolute; top:70px;_top:22px; right: 130px;*top: 22px; color:#767676}
   
    </style>
</head>
<body>
    <form id="frmShopList" runat="server">
    <input id="txtIsSendCard" type="hidden" runat="server" />
    <input id="txtSmsManage" type="hidden" runat="server" />
    <input id="txtPointManage" type="hidden" runat="server" />
    <input id="txtIsSettlement" type="hidden" runat="server" />
    <input type="hidden" id="hdShopID" runat="server" />
    <input type="hidden" id="union" runat="server" />
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div id="ShopSms" style="display: none;">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 300px; margin: auto">
                            <tr>
                                <td class="tableStyle_left">
                                    运营商名称：
                                </td>
                                <td class="tableStyle_right">
                                    <label id="txtSmsShopName">
                                    </label>
                                    <input type="hidden" id="hdSmsShopID" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    联盟商剩余短信量：
                                </td>
                                <td class="tableStyle_right">
                                    <label id="txtSmsNumber">
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    操作类型：
                                </td>
                                <td class="tableStyle_right">
                                    <label>
                                        <input type="radio" name="radSmsType" value="0" checked="checked" />增加</label>&nbsp;&nbsp;
                                    <label>
                                        <input type="radio" name="radSmsType" value="1" />扣除</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    短信数量：
                                </td>
                                <td class="tableStyle_right">
                                    <input type="text" class="input_txt border_radius" id="txtSmsCount" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <input id="btSaveSms" type="button" class="buttonColor" value="确   定" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divForeverTicketUrl" style="display: none;">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 500px; margin: auto">
                            <tr>
                                <td id="tdForeverTicketUrl" class="tableStyle_right">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="ShopPoint" style="display: none;">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 300px; margin: auto">
                            <tr>
                                <td class="tableStyle_left">
                                    联盟商名称：
                                </td>
                                <td class="tableStyle_right">
                                    <label id="txtShopPointName">
                                    </label>
                                    <input type="hidden" id="hdPointShopID" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    联盟商剩余积分量：
                                </td>
                                <td class="tableStyle_right">
                                    <label id="txtShopPointNumber">
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    操作类型：
                                </td>
                                <td class="tableStyle_right">
                                    <label>
                                        <input type="radio" name="radPointType" value="0" checked="checked" />增加</label>&nbsp;&nbsp;
                                    <label>
                                        <input type="radio" name="radPointType" value="1" />扣除</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    积分数量：
                                </td>
                                <td class="tableStyle_right">
                                    <input type="text" class="input_txt border_radius" id="txtcount" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <input id="btSavePoint" type="button" class="buttonColor" value="确   定" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="ShopInfo" style="display: none">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 700px; margin: auto">
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>运营商名称：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopName" type="text" runat="server" class="border_radius" />
                                    <input id="txtShopID" type="hidden" runat="server" />
                                </td>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>运营商联系人：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopContactMan" type="text" runat="server" class="border_radius" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    联系电话：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopTelephone" type="text" runat="server" class="border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>短信后缀：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopSmsName" type="text" runat="server" class="border_radius" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    打印标题：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopPrintTitle" type="text" runat="server" class="border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    打印脚注：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopPrintFoot" type="text" runat="server" class="border_radius" />
                                </td>
                            </tr>
                              <tr runat="server" id="trSettlement">
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>结算周期：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtSettlementInterval" type="text" title="总店与分店多少天进行一次结算" runat="server"
                                        class="border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>提成比例：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopProportion" type="text" runat="server" title="请输入0~1之间的数值" class="border_radius" />
                                </td>
                            </tr>
                        
                                  <input id="txtTotalRate" type="text"  style=" display:none;" runat="server" title="请输入0~1之间的数值" class="border_radius" />
                             
                          
                          <tr id="trAlliance" runat="server" style=" display:none;">
                                <td class="tableStyle_left">
                                    联盟商类型：
                                </td>
                                <td class="tableStyle_right">
                                    <label>
                                        <input type="radio" name="isLms" id="rdislms" runat="server" value="1" checked="true" />联盟商</label><label>
                                            <input type="radio" name="isLms" id="rdisnotlms" runat="server" value="0" />联盟商</label>
                                </td>
                                <td colspan="2" style="text-align: left;">
                                    <font style="color: Red;">（此设置保存之后将无法更改）</font>
                                </td>
                            </tr>
                            <tr runat="server" id="trSltShop">
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>所属商家：
                                </td>
                                <td class="tableStyle_right">
                                    <select runat="server" id="sltShopList" class="selectWidth">
                                    </select>
                                </td>
                                <td colspan="2" style="text-align: left;">
                                    <font style="color: Red;">（此设置保存之后将无法更改）</font>
                                </td>
                            </tr>
                            <tr runat="server" id="trShopSms">
                                <td class="tableStyle_left">
                                    短信不足发送应按：
                                </td>
                                <td colspan="3" class="tableStyle_right">
                                    <label>
                                        <input type="radio" id="rbbzfs" name="SmsType" value="1" runat="server" enableviewstate="True" />不准发送</label>
                                    <label>
                                        <input type="radio" id="rbkytz" name="SmsType" value="2" runat="server" />短信透支（短信量变负数）</label>
                                </td>
                            </tr>
                            <tr runat="server" id="trShopPoint">
                                <td class="tableStyle_left">
                                    积分不足消费应按：
                                </td>
                                <td colspan="3" class="tableStyle_right">
                                    <label>
                                        <input type="radio" id="rdbzxf" value="1" name="PointType" runat="server" />不准消费</label>
                                    <label>
                                        <input type="radio" id="rdjfgl" value="2" name="PointType" runat="server" />会员消费时，积分给零</label>
                                    <label>
                                        <input type="radio" id="rdtz" value="3" name="PointType" runat="server" />积分透支（积分量变负数）</label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    是否锁定：
                                </td>
                                <td colspan="3" class="tableStyle_right">
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radChooseYesOrNo" id="radChooseYes" runat="server" value="0" />
                                        <label style="vertical-align: middle;">
                                            暂时锁定</label></label>
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radChooseYesOrNo" id="radChooseNo" runat="server" value="1" />
                                        <label style="vertical-align: middle;">
                                            不锁定<font color="red">&nbsp;&nbsp;&nbsp;(锁定则该联盟商的所有员工不能登录)</font></label></label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    联系地址：
                                </td>
                                <td class="tableStyle_right" colspan="3">
                                    <input id="txtShopAddress" type="text" runat="server" class="border_radius" style="width: 520px;" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    备注：
                                </td>
                                <td class="tableStyle_right" colspan="3">
                                    <textarea id="txtShopRemark" rows="3" runat="server" style="width: 520px; word-wrap: break-word;
                                        outline: none; resize: none;"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="tableStyle_right ">
                                    <div class="buton" style="text-align: center;">
                                        <input id="btnShopSave" type="button" class="buttonColor" value="保   存 " />
                                        <input id="btnShopReset" type="button" class="buttonRest" value="重   置 " />
                                        <input id="ShopAddOrEdit" type="hidden" />
                                        <input id="chkSMS" type="checkbox" runat="server" style="display: none" />
                                         
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="ShopBuyCard" style="display: none">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 300px; margin: auto">
                            <tr>
                                <td class="tableStyle_left">
                                    购卡联盟商：
                                </td>
                                <td class="tableStyle_right">
                                    <label id="lblShopName">
                                    </label>
                                    <input type="hidden" id="hdCardShopID" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    卡片起始号：
                                </td>
                                <td class="tableStyle_right">
                                    <input type="text" class="input_txt border_radius" id="txtStartCardNumber" title="请输入6位数字" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    卡片截止号：
                                </td>
                                <td class="tableStyle_right">
                                    <input type="text" class="input_txt border_radius" id="txtEndCardNumber" title="请输入6位数字" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    购卡总金额：
                                </td>
                                <td class="tableStyle_right">
                                    <input type="text" class="input_txt border_radius" id="txtBuyCardMoney" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    备注：
                                </td>
                                <td class="tableStyle_right">
                                    <textarea id="txtRemark" class="input_txt border_radius" rows="3" runat="server"
                                        style="height: 40px; word-wrap: break-word; outline: none; resize: none;"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;" class="tableStyle_right ">
                                    <input id="btSaveCardLog" type="button" class="buttonColor" value="确   定" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="user_List_All">
                    <div class="user_List_top" style="height: 34px;">
                            <table class="tableStyle" style="width: 100%">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="tableStyle_left">
                                        快捷操作：
                                    </td>
                                    <td class="tableStyle_right">
                                     <input type="text" id="txtShopType" style="width: 33px; display: none" runat="server" />
                                  <%--      <input id="btnShopAdd" type="button" value="新增联盟商" class="common_Button_new" runat="server" />--%>
                                        <input id="btnShopBuyCard" type="button" value="购卡记录" class="common_Button" runat="server" />
                                      <%--  <input id="btnSettlement" type="button" value="结算管理" class="common_Button" runat="server" />
                                        <input id="btnPointRecord" type="button" value="积分记录" class="common_Button" runat="server" />
                                        <input id="btnMsgRecord" type="button" value="短信记录" class="common_Button" runat="server" />--%>
                                    </td>
                                <%--    <td class="tableStyle_left" style="width: 120px">
                                        <label id="lblsearch" for="txtSearch" class="tips">
                                            运营商名称/联系人/联系电话/联盟商地址</label>
                                        <asp:TextBox ID="txtSearch" runat="server" class="border_radius" Width="220px"></asp:TextBox>
                                    </td>
                                    <td class="tableStyle_right" style="width: 80px">
                                        <asp:Button ID="btnSearch" type="button" Text="查询" class="common_Button" runat="server"
                                            OnClick="btnSearch_Click" />
                                    </td>--%>
                                </tr>
                            </table>
                        </div>
            <table class="table-style table-hover user_List_txt">
                <asp:Repeater runat="server" ID="gvShopListUnion">
                    <HeaderTemplate>
                        <thead class="thead">
                            <tr class="th">
                                <th>
                                    序号
                                </th>
                                <th>
                                    运营商名称
                                </th>
                               
                                <th>
                                    联系人
                                </th>
                                <th>
                                    联系电话
                                </th>
                                <th>
                                    联盟商地址
                                </th>
                                <th class="settlement">
                                    <!-- style="<%= isopen?"":"display:none" %>"-->
                                    结算周期
                                </th>
                                  <th class="settlement">
                                    <!-- style="<%= isopen?"":"display:none" %>"-->
                                    提成比例
                                </th>
                              
                                <th>
                                    是否锁定
                                </th>
                                <th class="tPoint">
                                    剩余积分
                                </th>
                                <th class="tSms">
                                    剩余短信
                                </th>
                                <th>
                                    操作
                                </th>
                            </tr>
                        </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="td">
                            <!-- showAlready标识是否已展开子联盟商   shopId存储联盟商id  union是否是联盟商 这样子搞重用性貌似没了-->
                           <%-- <td class="tdZoom" showalready="0" shopid='<%# Eval("ShopID")%>' union='<%# Eval("IsAllianceProgram")%>'>--%>
                            <td>
                                <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <%# Eval("ShopName")%>
                            </td>
                          
                            <td>
                                <%# Eval("ShopContactMan")%>
                            </td>
                            <td>
                                <%# Eval("ShopTelephone")%>
                            </td>
                            <td style="text-align: left; padding-left: 4px;">
                                <%# Eval("ShopAddress")%>
                            </td>
                            <td class="settlement" style="text-align: left; padding-left: 4px;">
                                <%# Eval("SettlementInterval")%>
                            </td>
                            <td class="settlement" style="text-align: left; padding-left: 4px;">
                                <%# Eval("ShopProportion","{0:F2}")%>
                            </td>
                           
                            <td>
                                <%# Boolean.Parse(Eval("ShopState").ToString()) ? "是" : "否"%>
                            </td>
                            <td style="text-align: left; padding-left: 4px;" class="tPoint">
                                <%# Eval("PointCount")%>
                            </td>
                            <td style="text-align: left; padding-left: 4px;" class="tSms">
                                <%# Eval("SmsCount")%>
                            </td>
                            <td class="listtd" style="width: 250px; ">
                                <asp:Label ID="lblShopID" runat="server" Text='<%# Bind("ShopID") %>' Visible="false"></asp:Label>
                                <a href="#" onclick='<%# string.Format(" ShopEdit(\"{0}\",\"{1}\")",Eval("ShopName"),Eval("ShopID")) %>'
                                    id="hyShopEdit" runat="server">
                                    <img src="../images/Gift/eit.png" alt="编辑" title="编辑" />
                                </a>
                        
                                <a href="#" onclick='<%# string.Format(" btnShopBuyCards(\"{0}\",\"{1}\")",Eval("ShopName"),Eval("ShopID")) %>'
                                    id="hySendCard" runat="server">
                                   &nbsp;&nbsp;<img src="../images/ico/privilege.png" alt="运营商购卡" title="运营商购卡" />运营商购卡
                                </a>
                                
                                <span <%# Eval("ShopID").ToString()=="1"?"style='display:none;'":""%> class="pointmanage">
                                    <a href="#" onclick='<%# string.Format(" ShopEditPoint(\"{0}\",\"{1}\",\"{2}\")",Eval("ShopName"),Eval("ShopID"),Eval("ShopType")) %>'
                                        id="hyShopPointEdit" runat="server">  &nbsp;  <img src="../images/ico/point.png" alt="积分管理"
                                            title="积分管理" />
                                    </a></span>
                                
                                    <span <%# Eval("ShopID").ToString()=="1"?"style='display:none;'":""%>
                                        class="smsmanage"><a href="#" onclick='<%# string.Format(" ShopEditSms(\"{0}\",\"{1}\")",Eval("ShopName"),Eval("ShopID")) %>'
                                            id="hyShopSmsEdit" runat="server">&nbsp;  &nbsp;<img src="../images/ico/mobile.png" alt="短信管理"
                                                title="短信管理" />
                                        </a></span>
                                        
                                       
                                  <a href="#" onclick='<%# string.Format(" ForeverTicketUrl(\"{0}\")",Eval("ShopID")) %>'>
                                            &nbsp;   &nbsp;二维码</a>
                                <%--<span <%# Eval("ShopID").ToString()=="1"?"style='margin-left:105px;'":""%>> </span>--%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater runat="server" ID="gvShopListProfession">
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
                                    联系人
                                </th>
                                <th>
                                    联系电话
                                </th>
                                <th>
                                    联盟商地址
                                </th>
                                <th>
                                    是否锁定
                                </th>
                                <th>
                                    备注
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
                            <td style="text-align: left">
                                <%# Eval("ShopName")%>
                            </td>
                            <td>
                                <%# Eval("ShopContactMan")%>
                            </td>
                            <td>
                                <%# Eval("ShopTelephone")%>
                            </td>
                            <td style="text-align: left; padding-left: 4px;">
                                <%# Eval("ShopAddress")%>
                            </td>
                            <td>
                                <%# Boolean.Parse(Eval("ShopState").ToString()) ? "是" : "否"%>
                            </td>
                            <td style="text-align: left; padding-left: 4px;">
                                <%# Eval("ShopRemark")%>
                            </td>
                            <td class="listtd" style="width: 100px;">
                                <asp:Label ID="lblShopID" runat="server" Text='<%# Bind("ShopID") %>' Visible="false"></asp:Label>
                                <a href="#" onclick='<%# string.Format(" ShopEdit(\"{0}\",\"{1}\")",Eval("ShopName"),Eval("ShopID")) %>'
                                    id="hyShopEdit" runat="server">
                                  <img src="../images/Gift/eit.png" alt="编辑" title="编辑" /></a> 
                                  <%--  <a id="hyAuthority"
                                        runat="server" href='<%#string.Format("ShopAuthority.aspx?PID=38&SID={0}", Eval("ShopID"))%>'>
                                        <img src="../images/Gift/system.png" alt="权限设定" title="权限设定" />
                                    </a>
                                    --%>
                                    <a href="#" onclick='<%# string.Format(" ForeverTicketUrl(\"{0}\")",Eval("ShopID")) %>'>
                                        二维码</a>
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
