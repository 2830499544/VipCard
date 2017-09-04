<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysErrorList.aspx.cs" Inherits="ChainStock.SystemManage.SysErrorList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/style.css" rel="stylesheet" type="text/css" media="screen"
        id="skin" />
    <link href="../Inc/artDialogskins/aero.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/SysError.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            BindNullList("gvwSysLogList");
        });
    </script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" style="margin: 0 0 0 0; vertical-align: top;" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td style="vertical-align: top" id="tdbox">
             <div id="errorshow" style="display:none; width:500px;">
                <span id="sperror">
                </span>
             </div>
                <div class="divContentBox">
                    <div class="divContentHead">
                        <asp:Image ID="imgTitle" runat="server" align="absmiddle" />
                        <asp:Label ID="lblFrmTitle" runat="server" Text="窗体标题" CssClass="lblFrmTitle" ForeColor="#3366FF"></asp:Label>
                    </div>
                    <table class="tableStyle" style="width: 100%">
                        <tr style="color: #333333; background-color: #F7F6F3;">
                            <td align="right" style="width: 80px">
                                关键字：
                            </td>
                            <td style="width: 160px">
                                <input id="txtkeywords" type="text" runat="server" class="txtWidth" />
                            </td>
                            <td align="right" style="width: 80px">
                                按照时间：
                            </td>
                            <td style="width: 160px">
                                <input id="txtStartTime" type="text" runat="server" class="Wdate border_radius" />
                            </td>
                            <td style="width: 180px">
                                至
                                <input id="txtEndTime" type="text" runat="server" class="Wdate border_radius" />
                            </td>
                            <td align="left">
                                <asp:Button ID="btnMemListQuery" runat="server" Text="查   询" class="buttonColor"
                                    OnClick="btnMemListQuery_Click" />
                            </td>
                            <td align="right" style="width: 180px">
                                <select id="sltCleanTime" runat="server" class="selectWidth" name="D1">
                                    <option value="7">7天前日志</option>
                                    <option value="30">一个月前日志</option>
                                    <option value="90">三个月前日志</option>
                                </select>
                            </td>
                            <td align="left">
                                <input type="button" runat="server" id="btnCleanSysError" class="buttonColor" value="清理" />
                            </td>
                        </tr>
                        <tr style="color: #333333; background-color: #F7F6F3;">
                            <td align="right" style="width: 80px">
                                按商家：
                            </td>
                            <td style="width: 160px">
                                <select id="sltSysShopID" runat="server" class="selectWidth">
                                </select>
                            </td>
                            <td align="right" style="width: 80px">
                                按操作员：
                            </td>
                            <td style="width: 160px">
                                <select id="sltSysUserID" runat="server" class="selectWidth">
                                </select>
                            </td>
                        </tr>
                    </table>
                    <div>
                        <asp:GridView ID="gvwSysLogList" runat="server" AutoGenerateColumns="False" Width="100%"
                            Height="100%" CellPadding="4" ForeColor="#333333" BorderStyle="Double" EnableModelValidation="True"
                            DataKeyNames="ID" EmptyDataText="未找到符合此条件的数据！" CssClass="tableStyle" GridLines="None">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="" HeaderText="序号" HeaderStyle-Width="40px">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UserAccount" HeaderText="用户账号">
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="UserName" HeaderText="用户姓名">
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="查看详情" >
                                   <ItemStyle  HorizontalAlign="Center" Width="120px" />
                                    <ItemTemplate>
                                        <a href="#" onclick='<%# string.Format(" Show(\"{0}\")",Eval("ID")) %>'
                                            id="hyxq">查看详情 </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Ipaddress" HeaderText="IP地址">
                                    <ItemStyle HorizontalAlign="Center" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ShopName" HeaderText="所属商家">
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ErrorTime" HeaderText="操作时间">
                                    <ItemStyle HorizontalAlign="Center" Width="200px" />
                                </asp:BoundField>
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
                                    ShowCustomInfoSection="Left" CustomInfoClass="paginator" CustomInfoSectionWidth="300px"
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
