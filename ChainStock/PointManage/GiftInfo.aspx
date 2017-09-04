<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiftInfo.aspx.cs" Inherits="ChainStock.PointGift.GiftInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/aero.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/PointManage/GiftInfo.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmGiftAdd" runat="server">
    <table width="100%" style="margin: 0 0 0 0; vertical-align: top;" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td style="vertical-align: top" id="tdbox">
                <div class="divContentBox" style="width: 850px;">
                    <div class="divContentHead">
                        <asp:Image ID="imgTitle" runat="server" align="absmiddle" />
                        <asp:Label ID="lblFrmTitle" runat="server" Text="窗体标题" CssClass="lblFrmTitle" ForeColor="#3366FF"></asp:Label>
                    </div>
                    <div id="divErrorMsg" class="divErrorMsg">
                    </div>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <div>
                                        <table class="tableBox" cellspacing="0" cellpadding="2">
                                            <tr>
                                                <td class="tableAlignRight">
                                                    礼品名称：
                                                </td>
                                                <td class="tableAlignLeft">
                                                    <input id="txtGiftName" type="text" runat="server" class="input_txt border_radius" />
                                                    <input id="txtGiftID" type="hidden" runat="server" />
                                                </td>
                                                <td class="tableAlignRight">
                                                    礼品简码：
                                                </td>
                                                <td class="tableAlignLeft">
                                                    <input id="txtGiftCode" type="text" runat="server" class="input_txt border_radius" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tableAlignRight">
                                                    库存数量：
                                                </td>
                                                <td class="tableAlignLeft">
                                                    <input id="txtGiftStockNumber" type="text" runat="server" class="input_txt border_radius" />
                                                    <input id="txtGiftExchangeNumber" type="hidden" runat="server" />
                                                </td>
                                                <td class="tableAlignRight">
                                                    所需积分：
                                                </td>
                                                <td class="tableAlignLeft">
                                                    <input id="txtGiftExchangePoint" type="text" runat="server" class="input_txt border_radius" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tableAlignRight">
                                                    备注：
                                                </td>
                                                <td class="tableAlignLeft" colspan="3">
                                                    <textarea id="txtGiftRemark" rows="4" runat="server" style="width: 440px; word-wrap: break-word;"></textarea>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <input id="btnGiftSave" type="button" value="保   存" class="buttonColor" />&nbsp;
                                                    <input id="btnGiftReset" type="button" class="buttonColor" value="重   置" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td>
                                    <div class="imageBox">
                                        <div>
                                            <img alt="" id="imgGiftPhoto" src="../images/gift/nophoto.gif" width="150" runat="server" /><br />
                                            <span style="color: Red">相片支持大小为150*150</span>
                                            <input type="hidden" id="txtGiftPhoto" runat="server" />
                                        </div>
                                        <div style="margin-top: 10px; height: 30px;">
                                            <div style="width: 83px; height: 25px; float: left; _margin-left: 20px;">
                                                <input id="GiftPhoto_Uploadify" type="file" style="display: none;" class="buttonColor" />
                                            </div>
                                            <div>
                                                <input id="btnGiftPhotoUpload" type="button" class="buttonColor" value="上传图片" onclick="javascript:$('#GiftPhoto_Uploadify').uploadifyUpload();" />
                                            </div>
                                        </div>
                                        <div id="GIftRegister_fileQueue">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:HiddenField ID="HidGiftID" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
