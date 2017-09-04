<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SerialNumberList.aspx.cs" Inherits="ChainStock.SystemManage.SerialNumberList" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/SerialNumber.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmUser" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div id="MaskSN" style="display: none;">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 400px; margin: auto">
                            <tr>
                                <td class="tableStyle_left">
                                    生成数量：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtNumber" type="text" runat="server" class="border_radius" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    是否锁定：
                                </td>
                                <td class="tableStyle_right">
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radChooseYesOrNo" id="radChooseYes" runat="server" value="1" />
                                        <label style="vertical-align: middle;">
                                            暂时锁定</label></label>
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radChooseYesOrNo" id="radChooseNo" checked runat="server" value="0" />
                                        <label style="vertical-align: middle;">
                                            不锁定<font color="red">&nbsp;&nbsp;&nbsp;(锁定则序列号不能注册)</font></label></label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <input type="button" id="btnSNSave" class="buttonColor" value="确  定" />
                                </td>
                            </tr>
                        </table>
                    </div>
             
                    <div class="user_List_All">
                        <div class="user_List_top" style="height: 34px;">
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <input id="btnSysUserAdd" type="button" value="新增序列号" class="common_Button_new" onclick="SerialNumberAdd()"
                                                runat="server" />
                                                  <input type="text" id="txtUserType" style="width: 33px; display: none" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    注册码：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtSerialNumber"  runat="server" type="text" runat="server" class="border_radius" />
                                </td>
                                  <td class="tableStyle_left">
                                    注册商家：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopName"  runat="server" type="text" runat="server" class="border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    是否注册：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltIsUse" runat="server" class="selectWidth">
                                    </select>
                                </td>
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnUserSearch" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnUserSearch_Click1" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvSysUserList" OnItemDataBound="gvSysUserList_ItemDataBound">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                             <th>
                                                注册码
                                            </th>
                                            <th>
                                                注册商家
                                            </th>
                                            <th>
                                                是否锁定
                                            </th>
                                           
                                            <th>
                                                是否注册
                                            </th>
                                            <th>
                                                是否制卡
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
                                            <%# Eval("SerialNumber")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("ShopName")%>
                                        </td>
                                       
                                        <td>
                                            <%# Boolean.Parse(Eval("IsLock").ToString()) ? "是" : "否"%>
                                        </td>
                                        <td>
                                            <%# Boolean.Parse(Eval("IsUse").ToString()) ? "是" : "否"%>
                                        </td>
                                        <td >
                                             <%# Boolean.Parse(Eval("IsCard").ToString()) ? "是" : "否"%>
                                        </td>
                                        <td class="listtd" style="width: 90px;">
                                            <a href="#" onclick='<%# string.Format(" Card(\"{0}\",\"{1}\",\"{2}\")",Eval("ID"),Eval("SerialNumber"),Eval("IsCard")) %>'
                                                id="hyCard" runat="server">
                                                <img src="../images/Gift/card.png" title="制卡" alt="制卡" />
                                            </a><a href="#" onclick='<%# string.Format(" Lock(\"{0}\",\"{1}\",\"{2}\")",Eval("ID"),Eval("SerialNumber"),Eval("IsLock")) %>'
                                                id="hyLock" runat="server">
                                                <img src="../images/Gift/password.png" alt="锁定" title="锁定" />
                                            </a><a href="#" id="hyUserDel" runat="server" width="30px" onclick='<%# string.Format("return UserDel(\"{0}\")",Eval("ID")) %>'>
                                                <img src="../images/Gift/del.png" title="删除" alt="删除" />
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
