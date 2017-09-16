<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChooseMember.aspx.cs" Inherits="ChainStock.ExtraService.ChooseMember" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/aero.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/ExtraService/ChooseMember.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmChooseMember" runat="server">
    <table width="100%" style="margin: 0 0 0 0; vertical-align: top;" border="0" cellpadding="0"
        cellspacing="0">
        <tr>
            <td style="vertical-align: top" id="tdbox">
                <div class="divContentBox">
                    <div class="divContentHead">
                        <asp:Image ID="imgTitle" runat="server" align="absmiddle" />
                        <asp:Label ID="lblFrmTitle" runat="server" Text="窗体标题" CssClass="lblFrmTitle" ForeColor="#3366FF"></asp:Label>
                    </div>
                    <div style="margin-top: 5px;">
                        <table>
                            <tr>
                                <td style="vertical-align: top;">
                                    <div style="text-align: left;">
                                        <table>
                                            <tr>
                                                <td align="right" style="width: 80">
                                                    快速查找：
                                                </td>
                                                <td>
                                                    <input id="txtQueryMem" type="text" runat="server" class="input_txt border_radius"
                                                        title="请输入卡号/姓名/手机号" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 80">
                                                    会员等级：
                                                </td>
                                                <td>
                                                    <select id="sltMemLevelID" runat="server" class="selectWidth">
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 80">
                                                    所属商家：
                                                </td>
                                                <td>
                                                    <select id="sltShop" runat="server" class="selectWidth">
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="right">
                                                    <input id="btnChooseMember" type="button" class="buttonColor" value="查   找" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table border="0" cellpadding="0" cellspacing="1" class="tableStyle" width="200px"
                                            align="center" id="tbMember">
                                            <tr>
                                                <td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;"
                                                    colspan="3">
                                                    <script type="text/javascript">
                                                        ListLoading();
                                                    </script>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td>
                                    <div style="text-align: center;">
                                        <input id="btnSift" type="button" value=">>" class="buttonColor" />
                                    </div>
                                </td>
                                <td>
                                    <div style="text-align: right;">
                                        <textarea id="txtMobile" cols="20" rows="2" style="width: 280px; height: 400px;"></textarea>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="text-align: center;">
                        <input id="btnOK" type="button" class="buttonColor" value="确   定" />
                        <input id="btnCancel" type="button" class="buttonColor" value="取   消" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
