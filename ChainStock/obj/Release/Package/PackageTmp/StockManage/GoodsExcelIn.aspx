<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsExcelIn.aspx.cs" Inherits="ChainStock.StockManage.GoodsExcelIn" %>

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
</head>
<body>
    <input type="hidden" runat="server" id="hdMainIndex" />
    <form id="frmGoodsExcelIn" runat="server">
    <div class="system_Info box_right" style="width: 99%;">
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
                    商品导入
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_right">
                            （1）点击按钮下载商品模板：
                            <asp:Button ID="btnGoodsTemplate" runat="server" class="common_Button common_ServiceButton"
                                Text="商品模板" OnClick="btnGoodsTemplate_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnGoodsNumberTemplate" runat="server" class="common_Button common_ServiceButton"
                                Text="库存模板" OnClick="btnGoodsNumberTemplate_Click" />
                            <input type="hidden" runat="server" id="strPath" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_right">
                            （2）点击按钮导入商品数据：
                            <asp:FileUpload ID="fileUploadGoods" runat="server" Width="210px" Height="20px" CssClass="common_Button common_ServiceButton" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnGoodsCheck" runat="server" Text="检验数据" class="common_Button common_ServiceButton"
                                OnClick="btnGoodsCheck_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnGoodsImport" runat="server" Text="导入商品" class="common_Button common_ServiceButton"
                                OnClick="btnGoodsImport_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_right">
                            （2）点击按钮导入库存数据：
                            <asp:FileUpload ID="fileUploadGoodsNumber" runat="server" Width="210px" Height="20px"
                                CssClass="common_Button common_ServiceButton" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnGoodsNumberCheck" runat="server" Text="检验数据" class="common_Button common_ServiceButton"
                                OnClick="btnGoodsNumberCheck_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnGoodsNumberImport" runat="server" Text="导入库存" class="common_Button common_ServiceButton"
                                OnClick="btnGoodsNumberImport_Click" />
                            <input type="hidden" runat="server" id="path" />
                        </td>
                    </tr>
                </table>
                <div style="padding-top: 20px">
                    <asp:GridView ID="gvErrorGoods" runat="server" AutoGenerateColumns="False" Width="100%" Visible="false"
                        Height="100%" CellPadding="4" ForeColor="#333333" BorderStyle="Double" EnableModelValidation="True"
                        EmptyDataText="未找到符合此条件的数据！" CssClass="table-style table-hover user_List_txt"
                        GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="GoodsCode" HeaderText="商品编码">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="商品名称">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NameCode" HeaderText="商品简码">
                                <ItemStyle HorizontalAlign="center" Width="40px" />
                            </asp:BoundField>
                           <asp:BoundField DataField="GoodsClassID" HeaderText="商品分类ID">
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Unit" HeaderText="计量单位">
                                <ItemStyle HorizontalAlign="center" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GoodsBidPrice" HeaderText="参考进价">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Price" HeaderText="零售单价">
                                <ItemStyle HorizontalAlign="left" Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Point" HeaderText="商品积分">
                                <ItemStyle HorizontalAlign="right" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MinPercent" HeaderText="最低折扣">
                                <ItemStyle HorizontalAlign="right" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CommissionType" HeaderText="提成类型">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CommissionNumber" HeaderText="提成金额">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GoodsRemark" HeaderText="商品简介">
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
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%" Visible="false"
                        Height="100%" CellPadding="4" ForeColor="#333333" BorderStyle="Double" EnableModelValidation="True"
                        EmptyDataText="未找到符合此条件的数据！" CssClass="table-style table-hover user_List_txt"
                        GridLines="None">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="GoodsCode" HeaderText="商品编码">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="商品名称">
                                <ItemStyle HorizontalAlign="left" Width="40px" />
                            </asp:BoundField> 
                             <asp:BoundField DataField="Number" HeaderText="商品库存">
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
