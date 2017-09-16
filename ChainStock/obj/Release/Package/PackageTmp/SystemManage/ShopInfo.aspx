<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopInfo.aspx.cs" Inherits="ChainStock.SystemManage.ShopInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/aero.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/ShopInfo.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
             <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>

</head>
<body>
    <form id="frmShop" runat="server">
    <table width="100%" style="margin: 0 0 0 0; vertical-align: top;" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td style="vertical-align: top" id="tdbox">
                <div class="divContentBox" style="width: 700px;">
                    <div class="divContentHead">
                        <asp:Image ID="imgTitle" runat="server" align="absmiddle" />
                        <asp:Label ID="lblFrmTitle" runat="server" Text="窗体标题" CssClass="lblFrmTitle" ForeColor="#3366FF"></asp:Label>
                    </div>
                    <div style="width: 100%; text-align: center;">
                        <table class="tableStyle" cellspacing="0" cellpadding="2">
                            <tr>
                                <td class="tableAlignRight">
                                    商家名称：
                                </td>
                                <td class="tableAlignLeft">
                                    <input id="txtShopName" type="text" runat="server" class="input_txt border_radius" />
                                    <input id="txtShopID" type="hidden" runat="server" />
                                </td>
                                <td class="tableAlignRight">
                                    商家联系人：
                                </td>
                                <td class="tableAlignLeft">
                                    <input id="txtShopContactMan" type="text" runat="server" class="input_txt border_radius" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableAlignRight">
                                    联系电话：
                                </td>
                                <td class="tableAlignLeft">
                                    <input id="txtShopTelephone" type="text" runat="server" class="input_txt border_radius" />
                                </td>
                                <td class="tableAlignRight">
                                    <%--   所属区域：--%>
                                </td>
                                <td class="tableAlignLeft">
                                    <select id="sltShopAreaID" name="sltShopAreaID" runat="server" class="selectWidth"
                                        style="display: none">
                                    </select>
                                    <input type="text" id="txtAreaName" style="width: 33px; display: none" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableAlignRight">
                                    打印标题：
                                </td>
                                <td class="tableAlignLeft">
                                    <input id="txtShopPrintTitle" type="text" runat="server" class="input_txt border_radius" />
                                </td>
                                <td class="tableAlignRight">
                                    打印脚注：
                                </td>
                                <td class="tableAlignLeft">
                                    <input id="txtShopPrintFoot" type="text" runat="server" class="input_txt border_radius" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableAlignRight">
                                    是否锁定：
                                </td>
                                <td colspan="3" class="tableAlignLeft">
                                    <label>
                                        <input type="radio" name="radChooseYesOrNo" id="radChooseYes" runat="server" value="0" />暂时锁定</label><label>
                                            <input type="radio" name="radChooseYesOrNo" id="radChooseNo" runat="server" value="1"
                                                checked="true" />不锁定<font color="red">&nbsp;&nbsp;&nbsp;(锁定则该商家的所有员工不能登录)</font></label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableAlignRight">
                                    联系地址：
                                </td>
                                <td class="tableAlignLeft" colspan="3">
                                    <input id="txtShopAddress" type="text" runat="server" style="width: 433px; margin-left: 5px;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px; text-align: right; vertical-align: top;">
                                    备注：
                                </td>
                                <td class="tableAlignLeft" colspan="3">
                                    <textarea id="txtShopRemark" rows="3" runat="server" style="width: 433px; margin-left: 5px;
                                        word-wrap: break-word;"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <input id="btnShopSave" type="button" class="buttonColor" value="保   存 " />
                                    &nbsp
                                    <input id="btnShopReset" type="button" class="buttonColor" value="重   置 " />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:HiddenField ID="HidSid" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
