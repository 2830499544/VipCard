<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetGoodsLevel.aspx.cs"
    Inherits="ChainStock.StockManage.SetGoodsLevel" %>

<%@ Register src="../Controls/QuickSearch.ascx" tagname="QuickSearch" tagprefix="uc1" %>

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
    <script src="../Scripts/Module/StockManage/GoodsClass.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmGoodsClass" runat="server">
    <input type="hidden" id="share" runat="server" />
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
                                            <input id="Button1" type="button" value="新增类别" class="common_Button" runat="server"
                                                onclick="javascript:GoodsClassAdd(0)" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" style=" display:none;"
                            class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    所属商家：
                                </td>
                                <td class="tableStyle_right" style="width: 500px;">
                                  <%--  <select id="sltShopList" runat="server" class="selectWidth" />--%>
                                    <select id="sltShop" runat="server" class="selectWidth">
                                        </select>
                                         <input  id="HDsltshop" runat="server" type="hidden" />

                                </td>
                                <td class="tableStyle_right">
                                    <asp:Button ID="btnGoodsClassQuery" runat="server" Text="查   询" class="common_Button common_ServiceButton"
                                        OnClick="btnGoodsClassQuery_Click" />
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="rpGoodsClass">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                类别名称
                                            </th>
                                            <th>
                                                类别备注
                                            </th>
                                            <%--                                            <th>
                                                商品折扣说明
                                            </th>--%>
                                            <th>
                                                详细操作
                                            </th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td style="text-align: left; padding-left: 2px;">
                                            <%# Eval("ClassName")%>
                                        </td>
                                        <td style="text-align: left; padding-left: 2px;">
                                            <%# Eval("ClassRemark")%>
                                        </td>
                                        <%--                                        <td runat="server" id="tdDiscoundExplanation" style="word-break: keep-all; white-space: nowrap;
                                            overflow: hidden; text-overflow: ellipsis; width: 50%; text-align: left; padding-left: 2px;">
                                        </td>--%>
                                        <td class="listtd" style="width: 90px;">
                                            <a id="ClassEdit" runat="server" href='#' onclick='<%# string.Format(" GoodsClassEdit(\"{0}\")",Eval("ClassID")) %>'>
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" /></a>
                                               <%-- <a id="ClassSync" runat="server" href='#' onclick='<%# string.Format(" GoodsClassSync(\"{0}\",\"{1}\")",Eval("ClassID"),CurShopID) %>'>
                                                <img src="../images/Gift/system.png" alt="共享到所有商家" title="共享到所有商家" /></a>--%>
                                                 <a id="ClassDel" runat="server"
                                                    href='#' onclick='<%# string.Format(" GoodsClassDel(\"{0}\",\"{1}\",\"{2}\")",Eval("ClassName"),Eval("ClassID"),CurShopID) %>'>
                                                    <img src="../images/Gift/del.png" alt="删除" title="删除" /></a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <uc1:QuickSearch ID="QuickSearch1" runat="server" />
    </form>
</body>
</html>
