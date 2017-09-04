<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="ChainStock.SystemManage.UserList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="../Controls/QuickSearch.ascx" tagname="QuickSearch" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/UserInfo.js" type="text/javascript"></script>
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
                    <div id="EditPwd" style="display: none;">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 400px; margin: auto">
                            <tr>
                                <td class="tableStyle_left">
                                    新密码：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtPwdOne" type="password" runat="server" class="border_radius" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    确定密码：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtPwdTwo" type="password" runat="server" class="border_radius" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <input type="button" id="btnSavePwd" class="buttonColor" value="确  定" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="UserInfo" style="display: none;">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 700px; margin: auto">
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>用户账号：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtUserAccount" type="text" runat="server" class="border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>用户名称：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtUserName" type="text" runat="server" class="border_radius" />
                                    <input id="txtUserID" type="hidden" runat="server" />
                                </td>
                            </tr>
                            <tr id="userInfoPwd">
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>登录密码：
                                </td>
                                <td class="tableStyle_right">
                                    <input type="password" id="txtPwd" runat="server" class="border_radius" maxlength="20" />
                                    <input id="txtPassword" type="hidden" runat="server" />
                                </td>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>确认密码：
                                </td>
                                <td class="tableStyle_right">
                                    <input type="password" id="txtRepwd" runat="server" class="border_radius" maxlength="20" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>用户编号：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtUserNumber" type="text" runat="server" class="border_radius" maxlength="20" />
                                </td>
                                <td class="tableStyle_left">
                                    联系电话：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtUserTel" type="text" runat="server" class="border_radius" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    所属商家：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltShopInfo" name="sltShop" runat="server" class="selectWidth">
                                    </select>
                                    <input type="text" id="txtShopId" style="width: 33px; display: none" runat="server" />
                                </td>
                                <td class="tableStyle_left">
                                    管理权限：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltGroupID" name="sltGroupID" runat="server" class="selectWidth">
                                    </select><input type="text" id="txtGroupID" style="width: 33px; display: none" runat="server" />
                                    <input type="text" id="txtEditGroupID" style="width: 33px; display: none" />
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
                                            不锁定<font color="red">&nbsp;&nbsp;&nbsp;(锁定则该员工不能登录)</font></label></label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    备注：
                                </td>
                                <td class="tableStyle_right" colspan="3">
                                    <textarea id="txtUserRemark" rows="3" runat="server" style="width: 520px; word-wrap: break-word;
                                        outline: none; resize: none;"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="tableStyle_right">
                                    <div class="buton" style="text-align: center;">
                                        <input type="button" id="btnUserSave" class="buttonColor" value="保   存" />
                                        &nbsp
                                        <input type="button" id="btnUserReset" class="buttonRest" value="重   置" />
                                        <input id="UserAddOrEdit" type="hidden" />
                                    </div>
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
                                            <input id="btnSysUserAdd" type="button" value="新增用户" class="common_Button" onclick="UserAdd()"
                                                runat="server" />
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
                                 
                              
                                <td class="tableStyle_left">
                                    所属权限组：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltUserGroupID" runat="server" class="selectWidth">
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
                                                用户账号
                                            </th>
                                            <th>
                                                用户名称
                                            </th>
                                            <th>
                                                用户编号
                                            </th>
                                            <th>
                                                联系方式
                                            </th>
                                            <th>
                                                所属商家
                                            </th>
                                            <th>
                                                所属权限组
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
                                        <td>
                                            <%# Eval("UserAccount")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("UserName")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserNumber")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserTelephone")%>
                                        </td>
                                        <td>
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td>
                                            <%# Eval("GroupName")%>
                                        </td>
                                        <td>
                                            <%# Boolean.Parse(Eval("UserLock").ToString()) ? "是" : "否"%>
                                        </td>
                                        <td style="text-align: left;">
                                            <%# Eval("UserRemark")%>
                                        </td>
                                        <td class="listtd" style="width: 90px;">
                                            <a href="#" onclick='<%# string.Format(" UserEdit(\"{0}\",\"{1}\")",Eval("UserName"),Eval("UserID")) %>'
                                                id="hyUserEdit" runat="server">
                                                <img src="../images/Gift/eit.png" title="编辑" alt="编辑" />
                                            </a><a href="#" onclick='<%# string.Format(" UserEditPwd(\"{0}\",\"{1}\")",Eval("UserName"),Eval("UserID")) %>'
                                                id="hyEditPwd" runat="server">
                                                <img src="../images/Gift/password.png" alt="修改密码" title="修改密码" />
                                            </a><a href="#" id="hyUserDel" runat="server" width="30px" onclick='<%# string.Format("return UserDel(\"{0}\",\"{1}\")",Eval("UserName"),Eval("UserID")) %>'>
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
    <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
