<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupList.aspx.cs" Inherits="ChainStock.SystemManage.GroupList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
    <script src="../Scripts/Module/SystemManage/GroupAuthority.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GroupAdd() {
            window.location.href = "../SystemManage/GroupAuthority.aspx?PID=36&Gid=0";
        }
    </script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmGroup" runat="server">
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
                        <div class="user_List_top" style="height: 34px;">
                            <table class="tableStyle" style="width: 100%">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <input id="btnGroupAdd" type="button" class="common_Button" value="新增角色" onclick="GroupAdd()"
                                                runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvGroupList" OnItemDataBound="gvGroupList_ItemDataBound">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                角色名称
                                            </th>
                                            <th>
                                                说明
                                            </th>
                                            <th>
                                                有效范围
                                            </th>
                                            <th>
                                                创建者
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
                                            <%# Eval("GroupName")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("GroupRemark")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("Ispublic").ToString() == "True"?"所有商家":"本商家" %>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("UserName")%>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <a href='<%#string.Format("GroupAuthority.aspx?PID=36&GID={0}&ParentGroupID={1}", Eval("GroupID"),Eval("ParentGroupID"))%>'
                                                id="hyGroupPermission" runat="server" title="权限设定">
                                                <img src="../images/Gift/system.png" title="权限设定" alt="权限设定" />
                                            </a><a href="#" id="hyGroupel" runat="server" width="30px" onclick='<%# string.Format("return btnGroupDel(\"{0}\",\"{1}\")",Eval("GroupName").ToString().Replace("　","").Replace("|---",""),Eval("GroupID")) %>'>
                                                <img src="../images/Gift/del.png" alt="删除" title="删除" /></a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        <%--<div class="user_List_page">
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
                        </div>--%>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
