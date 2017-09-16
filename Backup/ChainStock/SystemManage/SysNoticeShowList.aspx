<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysNoticeShowList.aspx.cs"
    Inherits="ChainStock.SystemManage.SysNoticeShowList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/aero.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <%--<script src="../Scripts/Module/SystemManage/SysNotice.js" type="text/javascript"></script>--%>
    <script>
        $(document).ready(function () {
            //绑定空列表
            BindNullList("gvwNoticeList");
        })

        //查看公共信息 供startPage上的更多实用
        function NoticeLook(noticeID) {
            art.dialog.data('noticeID', noticeID);
            art.dialog.open('SystemManage/SysNoticeShow.aspx?NoticeID=' + noticeID, { id: "topCaller", title: '系统公告详情', lock: false }, false);
        }
    </script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmNoticeList" runat="server">
    <table width="100%" style="margin: 0 0 0 0; vertical-align: top;" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td style="vertical-align: top" id="tdbox">
                <div class="divContentBox">
                    <div class="divContentHead">
                        <asp:Image ID="imgTitle" runat="server" align="absmiddle" />
                        <asp:Label ID="lblFrmTitle" runat="server" Text="窗体标题" CssClass="lblFrmTitle" ForeColor="#3366FF"></asp:Label>
                    </div>
                    <div id="NoticeInfo" style="display: none">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 600px; margin: auto">
                            <tr>
                                <th style="text-align: center; font-weight: bold; height: 25px" class="th" colspan="6">
                                    系统公告
                                </th>
                            </tr>
                            <tr>
                                <td class="tableAlignRight" colspan="6" style="text-align: center">
                                    公告号：<span id="spNoticeCode" runat="server" style="font-weight: bold;"></span>
                                    <input id="NoticeID" type="hidden" />
                                    &nbsp;&nbsp;&nbsp;&nbsp; 发布人：<span id="spRelaseName" runat="server" style="font-weight: bold;"></span>
                                    &nbsp;&nbsp;&nbsp;&nbsp; 发布时间：<span id="spRelaseTime" runat="server" style="font-weight: bold;"></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableAlignRight">
                                    公告标题：
                                </td>
                                <td class="tableAlignLeft" colspan="5">
                                    <input id="txtNoticeTitle" type="text" style="width: 90%; margin-left: 5px;" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableAlignRight">
                                    公告内容：
                                </td>
                                <td class="tableAlignLeft" colspan="5">
                                    <textarea id="txtNoticeDetail" cols="20" rows="6" style="width: 90%; margin-left: 5px;
                                        word-wrap: break-word;"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7" style="text-align: center">
                                    <input type="button" id="btnNoticeSave" runat="server" class="buttonColor" value="保   存" />
                                    &nbsp
                                    <input type="button" id="btnNoticeReset" runat="server" class="buttonColor" value="重   置" />
                                    <input id="NoticeaAddOrEdit" type="hidden" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="padding: 10px;">
                        <asp:GridView ID="gvwNoticeList" runat="server" AutoGenerateColumns="False" Width="100%"
                            Height="100%" CellPadding="4" ForeColor="#333333" BorderStyle="Double" EnableModelValidation="True"
                            DataKeyNames="SysNoticeID" EmptyDataText="未找到符合此条件的数据！" CssClass="tableStyle" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="" HeaderText="序号">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SysNoticeCode" HeaderText="公告号码">
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SysNotieceName" HeaderText="发布人" />
                                <asp:BoundField DataField="SysNoticeTitle" HeaderText="公告标题">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SysNoticeTime" HeaderText="发布时间" />
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <a href="#" onclick='<%# string.Format(" NoticeLook(\"{0}\")",Eval("SysNoticeID")) %>'
                                            id="hyNoticeEdit" runat="server">查看</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle Font-Bold="True" CssClass="th" Height="20px" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </div>
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
                                    ShowCustomInfoSection="Left" CustomInfoClass="paginator" CustomInfoSectionWidth="200px"
                                    SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" Direction="LeftToRight"
                                    HorizontalAlign="Right">
                                </webdiyer:AspNetPager>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
