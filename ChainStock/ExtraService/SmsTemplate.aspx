<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SmsTemplate.aspx.cs" Inherits="ChainStock.ExtraService.SmsTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
 
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    
    <script src="../Scripts/Module/ExtraService/SmsTemplate.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmSmsTemplate" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                                <td class="tableStyle_left">
                                    所属商家：
                                </td>
                                <td class="tableStyle_right">
                                    <select id="sltShop" runat="server" class="selectWidth">
                                    </select>
                                    <input id="HDsltshop" runat="server" type="hidden" />
                                </td>                              
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnSmsTemplateSearch" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnSmsTemplateSearch_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    <div style="line-height: 24px; width: 700px; text-align: center; display: none;"
                        id="divAddOrEditTemplate">
                        <div style="padding: 5px;">
                            <table class="tableStyle" cellpadding="2" cellspacing="0" width="700" align="left">
                                <tr>
                                    <th style="text-align: left; padding-left: 10px; font-weight: bold; height: 20px;
                                        font-size: 14px; text-align: center;" class="th" colspan="5">
                                        短信模板通配符说明
                                    </th>
                                </tr>
                                <tr align="left">
                                    <td colspan="5">
                                        使用通配符功能可以使短信内容更加灵活，发送更加方便，请严格按照以下格式进行设置（通配符区分大小写）
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td>
                                        会员卡号：{CardID}
                                    </td>
                                    <td>
                                        卡片尾号：{LCardID}
                                    </td>
                                    <td>
                                        会员姓名：{Name}
                                    </td>
                                    <td>
                                        变动金额：{TempMoney}
                                    </td>
                                    <td>
                                        账户余额：{Money}
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td>
                                        当前日期：{Time}
                                    </td>
                                    <td>
                                        变动积分：{TempPoint}
                                    </td>
                                    <td>
                                        账户积分：{Point}
                                    </td>
                                    <td>
                                        旧的等级：{OldLevel}
                                    </td>
                                    <td>
                                        新的等级：{NewLevel}
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td colspan="2">
                                        会员生日：{MemBirthday}
                                    </td>
                                    <td colspan="2">
                                        过期时间：{MemPastTime}
                                    </td>
                                    <td colspan="3">
                                        
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="padding: 5px;">
                            <table class="tableStyle" cellspacing="0" cellpadding="2">
                                <tr>
                                    <th style="text-align: left; padding-left: 10px; font-weight: bold; height: 20px;
                                        font-size: 14px; text-align: center;" class="th" colspan="2">
                                        短信模板设置
                                    </th>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>模板名称：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtTemplateName" type="text" runat="server" class="border_radius" />
                                        <input id="txtTemplateID" type="hidden" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>模板内容：
                                    </td>
                                    <td class="tableStyle_right">
                                        <textarea id="txtTemplateContent" cols="20" rows="3" runat="server" style="width: 500px;
                                            word-wrap: break-word; outline: none; resize: none;"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center; height: 36px">
                                        <input id="btnTemplateSave" type="button" class="buttonColor" value="保   存 " />
                                        &nbsp;
                                        <input id="btnTemplateReset" type="button" class="buttonRest" value="重   置 " />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="user_List_All">
                        <div class="user_List_top" style="height: 34px;">
                            <table class="tableStyle" style="width: 100%">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <input id="btnAddTemplate" type="button" value="新增模板" class="common_Button" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvSmsTemplate">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                模板名称
                                            </th>
                                            <th>
                                                模板内容
                                            </th>
                                            <th>
                                                所属商家
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
                                            <%# Eval("TemplateName")%>
                                        </td>
                                        <td style="text-align: left; padding-left: 4px;">
                                            <%# Eval("TemplateContent")%>
                                        </td>
                                        <td >
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <a href="#" onclick='<%# string.Format(" EditTemplate(\"{0}\")",Eval("TemplateID")) %>'
                                                id="aEditTemplate" runat="server">
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" />
                                            </a><a href="#" onclick='<%# string.Format(" DeleteTemplate(\"{0}\",\"{1}\")",Eval("TemplateID"),Eval("TemplateName")) %>'
                                                id="aDeleteTemplate" runat="server" visible='<%#bool.Parse((int.Parse(Eval("TemplateID").ToString())>12).ToString())%>'>
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
