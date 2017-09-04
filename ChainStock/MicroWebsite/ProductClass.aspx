<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductClass.aspx.cs"
    Inherits="ChainStock.MicroWebsite.ProductClass" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>

    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>

    <script src="../Scripts/Module/MicroWebsite/ProductClass.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmClass" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">

                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>

                    <div id="dvClassInfo" style="display: none">
                       <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 400px; margin: auto">
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>分类名称：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtClassName" type="text" class="input_txt border_radius" style="width: 250px;" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    描 &nbsp&nbsp&nbsp&nbsp; 述：
                                </td>
                                <td class="tableStyle_right">
                                    <textarea id="txtClassRemark" rows="4" runat="server" style="word-wrap: break-word;
                                        width: 250px;"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="text-align: center">
                                    <input id="btnClassSave" type="button" value="保   存" class="buttonColor" />&nbsp;
                                    <input id="btnClassReset" type="button" class="buttonRest" value="重   置" />
                                    <input type="hidden" id="txtClassID" />
                                    <input type="hidden" id="txtClassShopID" />
                                </td>
                            </tr>
                        </table>
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
                                                <input id="btnClassAdd" type="button" value="新增类别" class="common_Button" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvwClass">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                类别名称
                                            </th>
                                            <th>
                                                描述
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
                                            <%# Eval("ClassName")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("ClassRemark")%>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <a href="#" onclick='<%# string.Format(" btnClassEdit(\"{0}\",\"{1}\",\"{2}\")",Eval("ClassID"),Eval("ClassName"),Eval("ClassRemark")) %>' id="btnClassEdit" runat="server">
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" /></a>

                                            <a href="#" onclick='<%# string.Format("return btnClassDel(\"{0}\",\"{1}\")",Eval("ClassID"),Eval("ClassName")) %>' ID="btnClassDel" runat="server">
                                                <img src="../images/Gift/del.png" alt="删除" title="删除" /></a>
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
