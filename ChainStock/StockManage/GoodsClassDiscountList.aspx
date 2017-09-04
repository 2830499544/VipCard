<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsClassDiscountList.aspx.cs"
    Inherits="ChainStock.StockManage.GoodsClassDiscountList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/aero.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/StockManage/GoodsClassDiscountList.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmSetLevel" runat="server">
    <table width="100%" style="margin: 0 0 0 0; vertical-align: top;" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td style="vertical-align: top" id="tdbox">
                <div class="divContentBox">
                    <div class="divContentHead">
                        <asp:Image ID="imgTitle" runat="server" align="absmiddle" />
                        <asp:Label ID="lblFrmTitle" runat="server" Text="窗体标题" CssClass="lblFrmTitle" ForeColor="#3366FF"></asp:Label>
                    </div>
                    <%--弹出层--%>
                    <div style="width: 100%; text-align: center; display: none;" id="divGoodsClassLevelDiscount">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="text-align: center;
                            width: 100%">
                            <tr>
                                <th style="text-align: center; font-weight: bold; height: 25px" class="th" colspan="4">
                                    商品分类等级折扣信息
                                </th>
                            </tr>
                            <tr>
                                <td style="width: 120px; text-align: right">
                                    分类名称：
                                </td>
                                <td style="text-align: left" align="left">
                                    <span id="spClassName"></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px; text-align: right">
                                    所需积分：
                                </td>
                                <td style="text-align: left" align="left">
                                    <span id="spLevelPoint"></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px; text-align: right">
                                    商品消费等级折扣：
                                </td>
                                <td style="text-align: left" align="left">
                                    <input id="txtClassDiscountPercent" type="text" class="input_txt border_radius" runat="server"
                                        style="margin-left: 5px;" />% <a href="#" title='即：会员在此等级时，所能享受的消费折扣'>参数说明</a>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px; text-align: right">
                                    商品消费积分比例：
                                </td>
                                <td style="text-align: left">
                                    <input id="txtClassPointPercent" type="text" class="input_txt border_radius" runat="server"
                                        style="margin-left: 5px;" />
                                    <a href="#" title='即：会员在此等级时，消费时自动兑换的积分比率，如：消费10元得1积分|消费20元得1积分|消费50元得1积分。' style="margin-left: 13px;">
                                        参数说明</a>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <input type="text" id="txtGoodsClassDiscountID" style="display: none" />
                                    &nbsp;<input id="btnSetGoodsClassDiscount" type="submit" class="buttonColor" value="保   存"
                                        runat="server" />
                                    &nbsp;<input id="btnMemLevelReset" type="button" class="buttonColor" value="重   置" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%--数据展现--%>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <a href="../StockManage/SetGoodsLevel.aspx?PID=73">返回类别</a>
                                    <%-- <input id="btnRuturn" type="button" value="返回类别" class="buttonColor" runat="server"
                                        onclick="javascript:GoodsClassReturn()" />--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div>
                        <asp:GridView ID="gvMemLevel" runat="server" AutoGenerateColumns="False" Width="100%"
                            Height="100%" CellPadding="4" ForeColor="#333333" BorderStyle="Double" EnableModelValidation="True"
                            DataKeyNames="LevelID" EmptyDataText="未找到符合此条件的数据！" CssClass="tableStyle" GridLines="None"
                            OnRowDataBound="gvMemLevel_RowDataBound">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="" HeaderText="序号" HeaderStyle-Width="25px">
                                    <HeaderStyle Width="25px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LevelName" HeaderText="等级名称">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LevelPoint" HeaderText="所需积分">
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ClassDiscountPercent" HeaderText="商品消费等级折扣">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ClassPointPercent" HeaderText="商品消费积分比率">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ClassName" HeaderText="商品分类名称">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LevellLock" HeaderText="升级机制">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="操作">
                                    <ItemTemplate>
                                        <a href="#" onclick='<%# string.Format(" EditGoodsClassLevelDiscount(\"{0}\")",Eval("ClassDiscountID")) %>'
                                            id="hyLevelEdit" runat="server">编辑 </a>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" />
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
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
