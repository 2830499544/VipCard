<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendEmail.aspx.cs" Inherits="ChainStock.ExtraService.SendEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
 
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/ExtraService/SendEmail.js" type="text/javascript"></script>
    <script src="../Inc/xheditor/xheditor-1.1.14-zh-cn.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmSendEmail" runat="server">
    <div class="system_Info box_right" style="width: 99%;">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_List_All" style="width: 900px;">
            <div>
                <div class="user_regist_infor" style="width: 100%">
                    会员邮件
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            会员邮箱：
                        </td>
                        <td class="tableStyle_right" colspan="2">
                            点击‘选择会员’按钮可以选择一个或者多个会员。自定义邮箱 输入多个电子邮箱，每个电子邮箱用英文格式的逗号隔开。
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="vertical-align: top">
                            电子邮箱：
                        </td>
                        <td class="tableStyle_right" colspan="2" style="border: 0px;">
                            <input id="btnChoose" type="button" class="common_Button" value="选择会员" runat="server"
                                style="margin-top: 3px" />
                            <textarea id="txtMemEmail" cols="65" rows="3" runat="server" style="width: 98%; word-wrap: break-word;
                                outline: none; resize: none; display: none" disabled="disabled"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="vertical-align: top">
                            自定义邮箱：
                        </td>
                        <td class="tableStyle_right">
                            <textarea id="txtCustomEmail" cols="65" rows="3" runat="server" style="width: 98%;
                                word-wrap: break-word; outline: none; resize: none;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            邮件主题：
                        </td>
                        <td class="tableStyle_right" colspan="2">
                            <input id="txtTitle" runat="server" type="text" class="border_radius" title="请输入邮件主题"
                                style="width: 98%" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="vertical-align: top">
                            邮件内容：
                        </td>
                        <td style="padding: 2px 0px 2px 0px; border: 1px solid #e4e7ea;">
                            <textarea id="txtEmailContent" style="width: 98%; height: 250px;" runat="server"
                                name="txtEmailContent" class="xheditor-simple {urlType:'abs',upLinkUrl:'/service/xhEditorUpload.ashx',upImgUrl:'/service/xhEditorUpload.ashx',upFlashUrl:'/service/xhEditorUpload.ashx',upMediaUrl:'/service/xhEditorUpload.ashx'}"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_right" colspan="3">
                            <div class="buton" style="text-align: center;">
                                <input id="btnSendEmail" type="button" runat="server" class="buttonColor" value="发   送" />
                                <input id="btnReset" type="button" runat="server" class="buttonRest" value="重   置" />
                                <input type="hidden" id="isEmail" runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div style="margin-top: 5px; display: none;" id="divChooseMember">
        <table>
            <tr>
                <td class="tableStyle_left">
                    快速查找：
                </td>
                <td class="tableStyle_right">
                    <input id="txtQueryMem" type="text" runat="server" class="input_txt border_radius"
                        title="请输入卡号/卡面号码/姓名/手机号" />
                </td>
                <td class="tableStyle_left">
                    会员等级：
                </td>
                <td class="tableStyle_right">
                    <select id="sltMemLevelID" runat="server" class="selectWidth">
                    </select>
                </td>
                <td class="tableStyle_left">
                    所属商家：
                </td>
                <td class="tableStyle_right">
                    <select id="sltShop" runat="server" class="selectWidth">
                    </select>
                     <input  id="HDsltshop" runat="server" type="hidden" />
                </td>
                <td class="tableStyle_right">
                    <input id="btnChooseMember" type="button" class="common_Button" value="查   找" />
                </td>
            </tr>
            <tr>
                <td colspan="7" align="center">
                    <textarea id="txtEmail" cols="20" rows="2" style="width: 100%; height: 100px; word-wrap: break-word;
                        outline: none; resize: none;"></textarea>
                </td>
            </tr>
            <tr>
                <td class="tableStyle_right" colspan="7">
                    <div class="buton" style="text-align: center;">
                        <input id="btnOK" type="button" class="buttonColor" value="确   定" />
                        <input id="btnCancel" type="button" class="buttonRest" value="取   消" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
