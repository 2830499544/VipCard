<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXinRuleList.aspx.cs"
    Inherits="ChainPoint.SystemManage.WeiXinRuleList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="/Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <link href="/Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="/Scripts/Module/Common/Tab.js" type="text/javascript"></script>
    <script src="/Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="/Scripts/Module/SystemManage/WeiXinRuleList.js" type="text/javascript"></script>
    <script src="/Scripts/Module/WeiXin/ckeditor/ckeditor.js" type="text/javascript"></script>
    <style type="text/css">
        #btnNewsPicUploadUploader
        {
            float: left;
            margin-left: 30px;
        }
        .th-child th{text-align: center;height: 24px; border-top:0; }
    </style>
</head>
<body>
    <form id="frmWeiXinRuleList" runat="server">
    <input type="text" runat="server" id="txtSystemDomain" style="display: none" />
    <div class="system_Info box_right" style="width: 99%;">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_List_All">
            <div class="user_regist_infor" style="width: 100%">
                微信规则设置
                <div style="float:right;"><input class="common_Button" style="letter-spacing:normal;margin-right: 12px;" type="button" value="参数设置" onclick="javascript:location.href='/SystemManage/WeiXinConfig.aspx?PID=138'" /></div>
            </div>
            <div style="text-align: left;">
                <div class="tabBox" id="RemindTab">
                    <ul class="tab">
                        <li id="tab0" class="on">文本消息</li>
                        <li id="tab1">图文消息</li>
                        <li id="tab2">自定义菜单</li>
                        <div id="btnAddRule" style="float: right; margin-right: 10px;">
                            <input type="button" id="btnAdd" value="添　加" class="common_Button" /></div>
                    </ul>
                </div>
                <div style="margin: 5px 10px 10px 0px;" id="MainContent0">
                    <table class="table-style table-hover">
                        <asp:Repeater runat="server" ID="gvTextRule">
                            <HeaderTemplate>
                                <thead class="thead">
                                    <tr class="th">
                                        <th>
                                            回复序号
                                        </th>
                                        <th>
                                            规则描述
                                        </th>
                                        <th>
                                            回复内容
                                        </th>
                                        <!--
                                        <th>
                                            操作员
                                        </th>
                                        <th>
                                            创建时间
                                        </th>
                                        -->
                                        <th>
                                            操 作
                                        </th>
                                    </tr>
                                </thead>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="td">
                                    <td>
                                        <%# Eval("RuleNUmber") %>
                                    </td>
                                    <td style="text-align: left;">
                                        <%# Eval("RuleDesc") %>
                                    </td>
                                    <td style="text-align: left;">
                                        <%# Eval("RuleContent") %>
                                    </td>
                                    <!--
                                    <td>
                                        <%# Eval("UserName") %>
                                    </td>
                                    <td>
                                        <%# Eval("RuleCreateTime","{0:yyyy-MM-dd}") %>
                                    </td>
                                    -->
                                    <td class="listtd" style="width: 60px;">
                                        <a runat="server" id="edit" href="#" onclick='<%#"textRuleEdit("+Eval("RuleID")+")" %>'>
                                            <img src="../images/Gift/eit.png" alt="编辑" title="编辑" />
                                        </a><a runat="server" id="del" href="#" onclick='<%#"delRule("+Eval("RuleID")+","+"1)" %>'>
                                            <img src="../images/Gift/del.png" alt="删除" title="删除" /></a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div id="textRuleDialog" style="display: none;">
                    <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 600px; margin: auto">
                        <tr>
                            <td colspan="2" style="color: #DB9944; text-align: center;" class="tableStyle_right">
                                提示：发送内容的长度最好不要超过200个长度
                            </td>
                        </tr>
                        <tr>
                            <td class="tableStyle_left">
                                <span style="color: #ff4800; vertical-align: middle">*</span>回复序号：
                            </td>
                            <td class="tableStyle_right">
                                <input type="text" id="txtNumber" class="border_radius" />
                                <input type="text" id="textRuleType" style="display: none;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tableStyle_left">
                                <span style="color: #ff4800; vertical-align: middle">*</span>规则描述：
                            </td>
                            <td class="tableStyle_right">
                                <input id="txtDescRule" class="border_radius" type="text" style="width: 433px;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tableStyle_left">
                                <span style="color: #ff4800; vertical-align: middle">*</span>发送内容：
                            </td>
                            <td class="tableStyle_right">
                                <textarea id="txtSendContent" rows="3" runat="server" style="width: 433px; word-wrap: break-word;"
                                    class="border_radius"></textarea>
                            </td>
                        </tr>
                        <tr style="text-align: center;">
                            <td colspan="2" class="tableStyle_right " style="text-align: center;">
                                <input id="btnTextRuleSave" type="button" class="buttonColor" value="保   存 " />
                                &nbsp
                                <input id="btnTextRuleReset" type="button" class="buttonRest" value="重   置 " />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="MainContent1" style="margin: 5px 10px 10px 0px; display: none;">
                    <asp:Repeater ID="r_NewsRule" runat="server" OnItemDataBound="r_NewsRule_ItemDataBound">
                        <HeaderTemplate>
                            <table class="table-style table-hover" cellspacing="0" cellpadding="2" style="width: 100%;"
                                id='rptNewsRuleTable'>
                        </HeaderTemplate>
                        <ItemTemplate>
                        <thead class="thead">
                            <tr class="th">
                                <th style="text-align: left;">
                                    <%--<div style="background: url(../Inc/Style/images/plus.gif) no-repeat; float: left;
                                        width: 19px; height: 19px;">
                                    </div>
                                    <div class="parent" style="float: left; cursor: pointer; color: #AF0081">
                                        <%#Eval("RuleNUmber")%></div>--%>
                                        回复序号:<%#Eval("RuleNUmber")%>
                                </th>
                                <th style="text-align: left;">
                                    规则描述:<%#Eval("RuleDesc")%>
                                </th>
                                <%--<th style="text-align: center;">
                                    消息个数:<%#Eval("NewsThisCount")%>
                                </th>--%>
                                <th class="listtd" style="width: 90px;">
                                    <a runat="server" id="add" href='<%# string.Format("WeiXinRule.aspx?PID=107&RuleID={0}", Eval("RuleID"))%>'>
                                        <img src="../images/Gift/add.png" alt="添加" title="添加" />
                                    </a><a runat="server" id="edit" href='<%#"javascript:newsRuleEdit("+Eval("RuleID")+")" %>'>
                                        <img src="../images/Gift/eit.png" alt="编辑" title="编辑" />
                                    </a><a runat="server" id="del" href='<%#"javascript:delRule("+Eval("RuleID")+",2)" %>'>
                                        <img src="../images/Gift/del.png" alt="删除" title="删除" />
                                    </a>
                                </th>
                            </tr>
                        </thead>
                            <tr>
                                <td colspan="6">
                                    <asp:Repeater ID="r_NewsDetail" runat="server">
                                        <HeaderTemplate>
                                            <table class="table-style table-hover" cellspacing="0" cellpadding="2" style="width: 92%; margin:3px 0 15px 20px;">
                                                <tr class="th th-child">
                                                    <th>
                                                        图文标题
                                                    </th>
                                                    <th>
                                                        图文描述
                                                    </th>
                                                    <th>
                                                        图片
                                                    </th>
                                                    <%--<th>
                                                        跳转链接
                                                    </th>--%>
                                                    <%--<th>
                                                        创建时间
                                                    </th>--%>
                                                    <th>
                                                        操作
                                                    </th>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="td">
                                                <td style="text-align: left;">
                                                    <%#Eval("NewsTitle")%>
                                                </td>
                                                <td style="text-align: left;">
                                                    <%#Eval("NewsDesc")%>
                                                </td>
                                                <td style="text-align: center;">
                                                    <img id="Img1" src=' <%#"../"+System.Text.RegularExpressions.Regex.Replace(Eval("NewsUrlFirst").ToString(), @"[\S\s]+?//[\S\s]+?/", "")%>'
                                                        runat="server" width="30" height="30" />
                                                </td>
                                                <%--<td style="text-align: left;">
                                                    <%#System.Text.RegularExpressions.Regex.Replace(Eval("NewsUrlSecond").ToString(), "WeiXin/WeiXinNewsLink.aspx", "...")%>
                                                </td>--%>
                                                <%--<td style="text-align: center;">
                                                    <%#Eval("NewsCreateTime")%>--%>
                                                </td>
                                                <td class="listtd" style="width: 60px;">
                                                    <a runat="server" id="edit" href='<%# string.Format("WeiXinRule.aspx?PID=107&NewsID={0}", Eval("NewsID"))%>'>
                                                        <img src="../images/Gift/eit.png" alt="编辑" title="编辑" /></a> <a runat="server" id="newsDel"
                                                            href='<%#"javascript:newsDel("+Eval("NewsID")+")" %>'>
                                                            <img src="../images/Gift/del.png" alt="删除" title="删除" /></a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div id="newsRuleDialog" style="display: none;">
                    <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 720px;">
                        <tr>
                            <td colspan="2" style="color: #DB9944; text-align: center;" class="tableStyle_right ">
                                提示：图文描述内容的长度最好不要超过200个长度
                            </td>
                        </tr>
                        <tr>
                            <td class="tableStyle_left">
                                <span style="color: #ff4800; vertical-align: middle">*</span>回复序号：
                            </td>
                            <td class="tableStyle_right">
                                <input type="text" id="txtNewsNumber" class="border_radius" />
                                <input type="text" id="txtNewsType" style="display: none;" />
                                <input type="text" id="txtNewsRule" style="display: none;" />
                            </td>
                            <td rowspan="6" style="width: 250px; text-align: center; vertical-align: top;">
                                <div>
                                    <img src="" width="180" height="150" id="imgNewsPhoto" /><br />
                                    <span style="color: #DB9944; font-size: 12px;">图片支持大小为180*150</span>
                                </div>
                                <div style="text-align: center; width: 180px">
                                    <input id="btnNewsPicUpload" type="file" class="buttonColor" style="display: none;" />
                                    <input type="button" value="上传图片" class="common_Button" style="float: right;" onclick="javascript:$('#btnNewsPicUpload').uploadifyUpload();" />
                                </div>
                                <div id="divNewsPic_fileQueue" style="height: 60px;">
                                </div>
                            </td>
                        </tr>
                        <tr id="trNewsRuleDescAdd">
                            <td class="tableStyle_left">
                                <span style="color: #ff4800; vertical-align: middle">*</span>规则描述：
                            </td>
                            <td class="tableStyle_right">
                                <input id="txtNewsRuleDescAdd" class="border_radius" type="text" style="width: 300px;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tableStyle_left">
                                图文标题：
                            </td>
                            <td class="tableStyle_right">
                                <input id="txtNewsTitle" class="border_radius" type="text" style="width: 300px;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tableStyle_left">
                                图文描述：
                            </td>
                            <td class="tableStyle_right">
                                <textarea id="txtNewsDesc" rows="3" runat="server" class="border_radius" style="width: 300px;
                                    word-wrap: break-word; height: 50px;"></textarea>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="tableStyle_left">
                                图片链接：
                            </td>
                            <td class="tableStyle_right">
                                <input id="txtPicUrl" type="text" class="border_radius" style="width: 300px; display: none;" />
                                <input type="text" id="txtNewsID" class="border_radius" style="display: none;" />
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="tableStyle_left">
                                跳转链接：
                            </td>
                            <td class="tableStyle_right">
                                <input id="txtUrl" class="border_radius" type="text" style="width: 300px;" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <textarea id="txtLinkContent" name="txtLinkContent" class="ckeditor"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center; height: 36px">
                                <input id="btnNewsRuleSave" type="submit" class="buttonColor" value="保   存 " />
                                &nbsp
                                <input id="btnNewsRuleReset" type="button" class="buttonRest" value="重   置 " />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="newsRuleDialogByRuleNUmber" style="display: none;">
                    <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 600px; margin: auto">
                        <tr>
                            <td class="tableStyle_left">
                                <span style="color: #ff4800; vertical-align: middle">*</span>回复序号：
                            </td>
                            <td class="tableStyle_right">
                                <input type="text" id="txtUpdateNewRuleByRuleNUmber" class="border_radius" />
                                <input type="text" id="txtNewsRuleID" style="display: none;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tableStyle_left">
                                <span style="color: #ff4800; vertical-align: middle">*</span>规则描述：
                            </td>
                            <td class="tableStyle_right">
                                <input id="txtNewsDescRule" type="text" class="border_radius" style="width: 433px;" />
                            </td>
                        </tr>
                        <tr style="text-align: center;">
                            <td colspan="2" class="tableStyle_right " style="text-align: center;">
                                <input id="btnUpdateNewRuleByRuleNUmber" type="button" class="buttonColor" value="保   存 " />
                            </td>
                        </tr>
                    </table>
                </div>

                <div id="MainContent2" style="margin: 5px 10px 10px 0px; display: none;">
                    <table class="table-style table-hover user_List_txt">
                        <asp:Repeater ID="r_menu" runat="server">
                        <HeaderTemplate>
                            <thead class="thead">
                                <tr class="th">
                                    <th>
                                        菜单名称
                                    </th>
                                    <th>
                                        子菜单个数
                                    </th>
                                    <th>
                                        菜单类型
                                    </th>
                                    <th>
                                        Key
                                    </th>
                                    <th>
                                        Url
                                    </th>
                                </tr>
                            </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="td">
                                <td style="text-align:left;">
                                    <%#this.GetMenuName(Eval("MenuName"), Eval("parentMenuID"))%>
                                </td>
                                <td style="text-align:center;">
                                    <%#Eval("childNum")%>
                                </td>
                                <td style="text-align:center;">
                                    <%#this.GetMenuTypeName( Eval("MenuType"))%>
                                </td>
                                <td style="text-align:left;">
                                    <%#Eval("MenuKey")%>
                                </td>
                                <td style="text-align:left;">
                                    <%#Eval("MenuUrl")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    </table>
                </div>

            </div>
        </div>
    </div>
    </form>
</body>
</html>
