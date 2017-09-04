<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataExcelIn.aspx.cs" Inherits="ChainStock.Common.DataExcelIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
 
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmDataExcelIn" runat="server">
    <asp:Literal runat="server" ID="ltMainIndex" Visible="false"></asp:Literal>
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_List_All">
            <div>
                <div class="user_regist_infor" style="width: 100%">
                    导入说明
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_right">
                            （1）先下载模板,严格按模板文件里面的说明进行EXCEL内容整理。
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_right">
                            （2）点击浏览,选择整理好的EXCEL文件。
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_right">
                            （3）点击"检验数据",验证要导入的数据是否正确。
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_right">
                            （4）点击"导入",导入成功后系统会提示上传成功。
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor" style="width: 100%">
                    会员导入
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_right">
                            （1）点击按钮下载会员模板：
                            <asp:Button ID="btnMemTemplate" runat="server" class="common_Button common_ServiceButton"
                                Text="会员模板" OnClick="btnMemTemplate_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_right">
                            （2）点击按钮导入会员数据：
                            <asp:FileUpload ID="fileUploadMem" runat="server" Width="210px" CssClass="common_Button common_ServiceButton" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnMemCheck" runat="server" Text="检验数据" class="common_Button common_ServiceButton"
                                OnClick="btnMemCheck_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnMemImport" runat="server" Text="导入会员" class="common_Button common_ServiceButton"
                                OnClick="btnMemImport_Click" />
                            <%-- <span id="spnMemImport" runat="server" style="color: Red; font-weight: bold;"></span>--%>
                            <input type="hidden" runat="server" class="common_ServiceButton" id="strPath" />
                        </td>
                    </tr>
                </table>
                <div style="padding-left: 10px; padding-right: 10px; padding-top: 20px">
                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                    <asp:GridView ID="gvErrorMem" runat="server" AutoGenerateColumns="False" Width="100%"
                        Height="100%" CellPadding="4" ForeColor="#333333" BorderStyle="Double" EnableModelValidation="True"
                        EmptyDataText="未找到符合此条件的数据！" CssClass="table-style table-hover user_List_txt"
                        GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="MemCard" HeaderText="会员卡号">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemName" HeaderText="姓名">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemSex" HeaderText="性别">
                                <ItemStyle HorizontalAlign="center" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemIdentityCard" HeaderText="身份证号码">
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemMobile" HeaderText="手机号码">
                                <ItemStyle HorizontalAlign="center" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemTelePhone" HeaderText="固定电话">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemBirthday" HeaderText="生日">
                                <ItemStyle HorizontalAlign="left" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemPoint" HeaderText="积分">
                                <ItemStyle HorizontalAlign="right" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemMoney" HeaderText="余额">
                                <ItemStyle HorizontalAlign="right" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemEmail" HeaderText="电子邮箱">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemAddress" HeaderText="地址">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemLevelID" HeaderText="会员等级ID">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemShopID" HeaderText="开卡商家ID">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemCreateTime" HeaderText="办卡日期">
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MemRemark" HeaderText="备注">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Error" HeaderText="错误提示">
                                <ItemStyle HorizontalAlign="left" Width="60px" ForeColor="Red" />
                            </asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle Font-Bold="True" CssClass="th" Height="20px" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" CssClass="td" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
