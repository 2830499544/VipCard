<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysNoticeList.aspx.cs"
    Inherits="ChainStock.SystemManage.SysNoticeList" %>

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
    <script src="../Scripts/Module/SystemManage/SysNotice.js" type="text/javascript"></script>
    <script src="../Scripts/Module/WeiXin/ckeditor/ckeditor.js" type="text/javascript"></script>
    <script>
        CKEDITOR.editorConfig = function (config) {
            config.toolbar_Full = [
                    ['Source'],
                    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
                    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
                    ['Undo', 'Redo']
            //                    ['Link', 'Unlink', 'Anchor']
                ];
        };   
    </script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmNoticeList" runat="server">
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
                                            <input id="btnNoticeAdd" type="button" value="新增公告" class="common_Button" onclick="NoticeAdd()"
                                                runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvNoticeList">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                公告号码
                                            </th>
                                            <th>
                                                发布人
                                            </th>
                                            <th>
                                                公告标题
                                            </th>
                                            <th>
                                                发布时间
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
                                            <%# Eval("SysNoticeCode")%>
                                        </td>
                                        <td>
                                            <%# Eval("SysNotieceName")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("SysNoticeTitle")%>
                                        </td>
                                        <td>
                                            <%# Eval("SysNoticeTime","{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <a href="#" onclick='<%# string.Format(" NoticeEdit(\"{0}\")",Eval("SysNoticeID")) %>'
                                                id="hyNoticeEdit" runat="server">
                                                <img src="../images/Gift/eit.png" title="编辑" alt="编辑" />
                                            </a><a href="#" id="hyNoticeDel" runat="server" onclick='<%# string.Format("return NoticeDel(\"{0}\")",Eval("SysNoticeID")) %>'>
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
