<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysDataBak.aspx.cs" Inherits="ChainStock.SystemManage.SysDataBak" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/DataBakUp.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/SysDataInit.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="system_Info box_right" style="width: 99%;">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_List_All">
            <div>
                <div class="user_regist_infor" style="width: 100%">
                    注意事项
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_right">
                            （1）以下操作在执行之后将不可恢复，请慎重操作！
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_right">
                            （2）如果您不是初次使用本系统，建议您首先将当前数据库备份，这样可以降低风险！
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor" style="width: 100%">
                    系统数据初始化
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="user_List_styleLeft">
                            清除全部数据：
                        </td>
                        <td class="user_List_styleRight" style="width: 200px;">
                            <input id="btnAll" runat="server" type="button" value="清   除" class="common_Button" />
                        </td>
                        <td class="user_List_styleLeft">
                            还原测试数据：
                        </td>
                        <td class="user_List_styleRight">
                            <input id="btnRestore" runat="server" type="button" value="还   原" class="common_Button" />
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor" style="width: 100%">
                    系统数据备份
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="user_List_styleLeft">
                            备份文件名称：
                        </td>
                        <td class="user_List_styleRight">
                            <div class="user_List_Button">
                                <input type="text" class="border_radius" id="txtBakName" style="width: 200px;" runat="server" />
                                <input type="button" id="btnBakUp" runat="server" class="common_Button" value="马上备份" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor" style="width: 100%">
                    系统备份列表
                </div>
                <div style="padding-top: 20px">
                    <asp:GridView ID="gdvBakList" runat="server" AutoGenerateColumns="False" Width="100%"
                        Height="100%" CellPadding="4" ForeColor="#333333" EnableModelValidation="True"
                        EmptyDataText="未找到符合此条件的数据！" CssClass="table-style table-hover user_List_txt"
                        GridLines="None" OnRowDataBound="gdvBakList_RowDataBound" OnRowCommand="GV_CommandItem">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="" HeaderText="序号" HeaderStyle-Width="20px">
                                <HeaderStyle Width="50px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FullName" HeaderText="备份文件名">
                                <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LastWriteTime" HeaderText="备份时间">
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Length" HeaderText="文件大小">
                                <ItemStyle HorizontalAlign="Left" Width="80px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemStyle HorizontalAlign="Left" Width="180px" />
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lbtn_RDB" OnClientClick="return confirm('确定要还原到该文件来覆盖系统分数据吗');"
                                        Text="还原此备份" CommandName="RETDB" Visible="false"></asp:LinkButton>&nbsp;&nbsp; <a id="dloadfile"
                                            runat="server">下载备份</a>&nbsp;&nbsp;
                                    <asp:LinkButton runat="server" ID="lbtn_del" Text="删除" CommandName="DELDB"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="" HeaderText="操作">

                            </asp:BoundField>--%>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle Font-Bold="True" CssClass="th" Height="20px" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="td" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                    <asp:HiddenField ID="hidCount" runat="server" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
