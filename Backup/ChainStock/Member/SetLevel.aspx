<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetLevel.aspx.cs" Inherits="ChainStock.Member.SetLevel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Mem/SetLevel.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmSetLevel" runat="server">
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
                        <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                                <td class="user_List_styleLeft">
                                    快捷操作：
                                </td>
                                <td class="user_List_styleRight">
                                    <input id="btnAddLevel" type="button" value="新增等级" class="common_Button" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvMemLevel">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                等级名称
                                            </th>
                                            <th>
                                                所需积分
                                            </th>
                                            <th>
                                                快速消费等级折扣
                                            </th>
                                            <th>
                                                快速消费积分比率
                                            </th>
                                            <th>
                                                 会员充值积分比率
                                            </th>
                                            <th>
                                                升级机制
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
                                            <%# Eval("LevelName")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("LevelPoint")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# (Convert.ToDecimal(Eval("ClassDiscountPercent", "{0:F2}")) * 100)%>%
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("ClassPointPercent", "{0:F2}")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("ClassRechargePointRate", "{0:F2}")%>
                                        </td>
                                        <td>
                                            <%# this.GetLevellLock(Boolean.Parse(Eval("LevellLock").ToString()))%>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <a href="#" onclick='<%# string.Format(" EditLevel(\"{0}\",\"{1}\")",Eval("LevelID"),Eval("ShopMemLevelID")) %>'
                                                id="hyLevelEdit" runat="server">
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" />
                                            </a><a href="#" onclick='<%# string.Format(" DeleteLevel(\"{0}\",\"{1}\")",Eval("LevelID"),Eval("LevelName")) %>'
                                                id="hyLevelDelete" runat="server">
                                                <img src="../images/Gift/del.png" alt="删除" title="删除" />
                                            </a>
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
    </form>
</body>
</html>
