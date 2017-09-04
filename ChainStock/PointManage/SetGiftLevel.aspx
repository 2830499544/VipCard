<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetGiftLevel.aspx.cs" Inherits="ChainStock.PointManage.SetGiftLevel" %>

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
    <script src="../Scripts/Module/PointManage/GiftClass.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmGiftLevel" runat="server">
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
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <input id="Button1" type="button" value="新增分类" class="common_Button" runat="server"
                                                onclick="javascript:GiftClassAdd(0)" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divGiftClassAddOreEdit" style="display: none">
                            <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 400px; margin: auto">
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>所属分类：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltGiftClass" runat="server" class="selectWidth">
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>类别名称：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input type="text" id="txtClassName" runat="server" class="border_radius" />
                                        <input type="hidden" id="txtClassID" />
                                        <input type="hidden" id="txtParentID" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        备注：
                                    </td>
                                    <td class="tableStyle_right">
                                        <textarea id="txtGiftClassRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                            outline: none; resize: none;"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center; height: 36px">
                                        <input type="button" id="btnGiftClassSave" class="buttonColor" value="保   存" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="rpGiftClass">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                类别名称
                                            </th>
                                            <th>
                                                类别备注
                                            </th>
                                            <th>
                                                详细操作
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td style="text-align: left; padding-left: 10px;">
                                            <%#Eval("GiftClassName")%>
                                        </td>
                                        <td style="text-align: left; padding-left: 10px;">
                                            <%#Eval("GiftClassRemark")%>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <a id="aGidtEdit" runat="server" href='#' onclick='<%# string.Format(" GiftClassEdit(\"{0}\")",Eval("GiftClassID")) %>'>
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" /></a><a id="aGiftDel" runat="server"
                                                    href='#' onclick='<%# string.Format(" GiftClassDel(\"{0}\",\"{1}\")",Eval("GiftClassName"),Eval("GiftClassID")) %>'>
                                                    <img src="../images/Gift/del.png" alt="删除" title="删除" /></a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="td">
                                        <td style="text-align: left; padding-left: 10px;">
                                            <%#Eval("GiftClassName")%>
                                        </td>
                                        <td style="text-align: left; padding-left: 10px;">
                                            <%#Eval("GiftClassRemark")%>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <a id="aGidtEdit" runat="server" href='#' onclick='<%# string.Format(" GiftClassEdit(\"{0}\")",Eval("GiftClassID")) %>'>
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" /></a><a id="aGiftDel" runat="server"
                                                    href='#' onclick='<%# string.Format(" GiftClassDel(\"{0}\",\"{1}\")",Eval("GiftClassName"),Eval("GiftClassID")) %>'>
                                                    <img src="../images/Gift/del.png" alt="删除" title="删除" /></a>
                                        </td>
                                </AlternatingItemTemplate>
                                <%--                                <FooterTemplate>
                                    <tr id="Tr1" runat="server" visible='<%#bool.Parse((rpGiftClass.Items.Count==0).ToString())%>'>
                                        <td colspan="3" style="text-align: center;">
                                            暂无分类显示
                                        </td>
                                    </tr>
                                </FooterTemplate>--%>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
