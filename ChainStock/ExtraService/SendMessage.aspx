<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMessage.aspx.cs" Inherits="ChainStock.ExtraService.SendMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
 
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/ExtraService/SendMessage.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmSendMessage" runat="server">
    <div class="system_Info box_right" style="width: 99%;">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_List_All" style="width: 900px;">
            <div>
                <div class="user_regist_infor" style="width: 100%">
                    会员短信 <span style="padding-left: 40px; color: #DB9944; border-collapse: collapse;
                        font: 13px/1.5 Microsoft YaHei;">温馨提示：当短信内容长度大于60个字(包括符号)，系统将自动分成2条(120字以内)或多条(120字以上)发送。</span>
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            会员短信：
                        </td>
                        <td class="tableStyle_right" colspan="2">
                            给会员发送一条或者多条短信。点击‘选择会员’按钮可以选择一个或者多个会员。
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            手机号码：
                        </td>
                        <td class="tableStyle_right" colspan="2" style="border: 0px;">
                            <input id="btnChoose" type="button" class="common_Button" value="选择会员" style="margin-top: 3px" />
                            <textarea id="txtMemReceiver" cols="65" rows="3" runat="server" style="width: 98%;
                                word-wrap: break-word; outline: none; resize: none; " disabled="disabled"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            短信模板：
                        </td>
                        <td class="tableStyle_right" colspan="2">
                            <select id="sltMemSmsTemplate" runat="server" class="selectWidth" onchange="MemSmsTemplate();">
                            </select>
                            <label id="txtMessageLen" style="color: #ff4800; vertical-align: middle;">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            短信内容：
                        </td>
                        <td class="tableStyle_right" colspan="2">
                            <textarea id="txtMemContent" cols="65" rows="3" runat="server" style="width: 98%;
                                word-wrap: break-word; outline: none; resize: none;" onkeyup="checkMsgLen()"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_right" colspan="3">
                            <div class="buton" style="text-align: center;">
                                <input id="btnMemSendMessage" type="button" class="buttonColor" value="发   送" />
                                <input id="btnMemReset" type="button" class="buttonRest" value="重   置" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor" style="width: 100%">
                    自定义短信
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            自定义短信：
                        </td>
                        <td class="tableStyle_right" colspan="2">
                            手动输入手机号码发送一条或者多条短信。输入多个手机号码，每个手机号码用英文格式的逗号隔开。
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            手机号码：
                        </td>
                        <td class="tableStyle_right" colspan="2">
                            <textarea id="txtCustomReceiver" cols="65" rows="3" runat="server" style="width: 98%;
                                word-wrap: break-word; outline: none; resize: none;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            导入号码：
                        </td>
                        <td class="tableStyle_right" colspan="2">
                            <asp:FileUpload ID="btnUploadMobile" CssClass="border_radius common_ServiceButton"
                                runat="server" Style="width: 200px; height: 20px;" />
                            &nbsp; &nbsp;
                            <asp:Button ID="btnImport" runat="server" Text="导入号码" class="common_Button common_ServiceButton"
                                OnClick="btnImport_Click" />
                            &nbsp;从记事本文件导入手机号码，文件里必须每个手机号码一行。
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            短信模板：
                        </td>
                        <td class="tableStyle_right" colspan="2">
                            <select id="sltCustomSmsTemplate" runat="server" class="selectWidth" onchange="CustomSmsTemplate();">
                            </select>
                            <label id="txtNotifyLen" style="color: red;">
                            </label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            短信内容：
                        </td>
                        <td class="tableStyle_right" colspan="2">
                            <textarea id="txtCustomContent" cols="65" rows="3" runat="server" style="width: 98%;
                                word-wrap: break-word; outline: none; resize: none;" onkeyup="checkNotifyLen()"  ></textarea>
                                                </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_right" colspan="3">
                            <div class="buton" style="text-align: center;">
                                <input id="btnSendMessage" type="button" class="buttonColor" value="发   送" />
                                <input id="btnReset" type="button" class="buttonRest" value="重   置" />
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
                    <textarea id="txtMobile" cols="20" rows="2" style="width: 100%; height: 100px; word-wrap: break-word;
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
    <asp:CheckBox ID="chkSms" runat="server" Text="启用系统短信功能的全局参数" Style="display: none;" />
    <%--  <table style="width: 100%; height: 100%; word-wrap: break-word;">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
             
                    <div class="user_List_All">
                   
                            <tr>
                                <th style="text-align: left; padding-left: 10px; font-weight: bold; height: 35px;
                                    font-size: 14px; text-align: center;" class="th" colspan="3">
                                   
                                </th>
                            </tr>
                      
                    </div>
                </div>
            </td>
        </tr>
    </table>--%>
    </form>
</body>
</html>
