<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXinMenuManager.aspx.cs" Inherits="ChainStock.SystemManage.WeiXinMenuManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>自定义菜单管理</title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>

    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/WeiXinMenuManager.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmWeiXinMenuManager" runat="server">
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
                            <div class="user_List_top">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                                    <tr style="color: #333333; background-color: #F7F6F3;">
                                        <td class="user_List_styleLeft">
                                            快捷操作：
                                        </td>
                                        <td class="user_List_styleRight">
                                            <div class="user_List_Button">
                                                <input id="btnCreateMenu" type="button" value="同步菜单" class="common_Button" runat="server" />
                                                <input id="btnAddMenu" type="button" value="新增菜单" class="common_Button" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <table class="table-style table-hover user_List_txt">
                                <asp:Repeater runat="server" ID="rpt_MenuManager">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    菜单名称
                                                </th>
                                                <th>
                                                    子菜单个数
                                                </th>
                                                <th>
                                                    菜单类型
                                                </th>
                                                <th>
                                                    Key
                                                </th>
                                                <th>
                                                    Url
                                                </th>
                                                <th>
                                                    操　作
                                                </th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="td">
                                            <td style="text-align:left;">
                                                <%#this.GetMenuName(Eval("MenuName"), Eval("parentMenuID"))%>
                                            </td>
                                            <td style="text-align:center;">
                                                <%#Eval("childNum")%>
                                            </td>
                                            <td style="text-align:center;">
                                                <%#this.GetMenuTypeName( Eval("MenuType"))%>
                                            </td>
                                            <td style="text-align:left;">
                                                <%#Eval("MenuKey")%>
                                            </td>
                                            <td style="text-align:left;">
                                                <%#Eval("MenuUrl")%>
                                            </td>
                                             <td style="width:150px;">
                                                <%#this.MenuEdit(Eval("MenuID"), Eval("parentMenuID"), Eval("MenuName"), Eval("parentMenuID"))%>
                                                <%#this.MenuDel(Eval("parentMenuID"), Eval("MenuID"), Eval("MenuName"))%>
                                             </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>




                            <div id="divAddMenuInfo" style="display: none">
                               <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 450px; margin: auto">
                                    <tr>
                                        <td class="tableStyle_left">
                                            <span style="color: #ff4800; vertical-align: middle">*</span>菜单名称：
                                        </td>
                                        <td class="tableStyle_right">
                                            <input id="txtMenuName" type="text" class="input_txt border_radius" style="width: 350px;" maxlength="7" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <span style="color: #ff4800; vertical-align: middle">*</span>菜单类型：
                                        </td>
                                        <td class="tableStyle_right">
                                            <select id="sltType"  class="selectWidth" style="width: 150px;">
                                                <option value="1">按钮类型</option>
                                                <option value="2">链接类型</option>
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <span style="color: #ff4800; vertical-align: middle">*</span>所属菜单：
                                        </td>
                                        <td class="tableStyle_right" id="td_sltMenuType">
                                            <select id="sltMenuType" runat="server" class="selectWidth" style="width: 150px;">
                                                
                                            </select>
                                        </td>
                                    </tr>
                                    <tr id="tr_url">
                                        <td class="tableStyle_left">
                                            <span style="color: #ff4800; vertical-align: middle">*</span>链接地址：
                                        </td>
                                        <td class="tableStyle_right">
                                            <input type="text" class="input_txt border_radius" style="width:350px;" id="txtUrl" />
                                        </td>
                                    </tr>
                                    <tr id="tr_rule">
                                        <td class="tableStyle_left">
                                            <span style="color: #ff4800; vertical-align: middle">*</span>回复规则：
                                        </td>
                                        <td class="tableStyle_right">
                                            <select id="sltReply" runat="server" class="selectWidth" style="width: 350px;">
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <input id="btnAddMenuSave" type="button" value="保   存" class="buttonColor" />&nbsp;
                                            <input id="btnAddMenuReset" type="button" class="buttonRest" value="重   置" />
                                            <input type="hidden" id="txtMenuIDForEdit" />
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div id="divMenuFirst" style="display: none">
                                <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 400px; margin: auto">
                                    <tr>
                                        <td class="tableStyle_left">
                                            <span style="color: #ff4800; vertical-align: middle">*</span>菜单名称：
                                        </td>
                                        <td class="tableStyle_right">
                                            <input id="txtMenuNameFirst" type="text" class="input_txt border_radius" style="width: 300px;" maxlength="4" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <input id="btnAddMenuFirstSave" type="button" value="保   存" class="buttonColor" />&nbsp;
                                            <input id="btnAddMenuFirstReset" type="button" class="buttonRest" value="重   置" />
                                            <input type="hidden" id="txtMenuIDFirts"  />
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div id="divMenuFirstBySystem" style="display: none">
                                <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 400px; margin: auto">
                                    <tr>
                                        <td class="tableStyle_left">
                                            <span style="color: #ff4800; vertical-align: middle">*</span>菜单名称：
                                        </td>
                                        <td class="tableStyle_right">
                                            <span id="sp_MenuName"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tableStyle_left">
                                            <span style="color: #ff4800; vertical-align: middle">*</span>所属菜单：
                                        </td>
                                        <td class="tableStyle_right" id="td_sltMenuTypeBySystem">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <input id="btnAddMenuBySystemSave" type="button" value="保   存" class="buttonColor" />&nbsp;
                                            <input id="btnAddMenuBySystemReset" type="button" class="buttonRest" value="重   置" />
                                            <input type="hidden" id="txtMenuIDBySystem"  />
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
