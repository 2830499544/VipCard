<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MicroWebsiteIndex.aspx.cs" Inherits="ChainStock.MicroWebsite.MicroWebsiteIndex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>

    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/MicroWebsite/MicroWebsiteIndex.js" type="text/javascript"></script>
    <style type="text/css">
        .uploadifyQueueItem{width:140px;}
        .uploadifyProgress{width:80%;}
        .fileName{width:0px;height:0px;display:block;overflow:hidden;}
    </style>
</head>
<body>
    <form id="frmMicroWebsiteIndex" runat="server">
        <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
            <tr>
                <td colspan="2" style="width: 100%;">
                    <div class="system_Info">
                        <div class="system_Top">
                            <div class="user_regist_title">
                                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                            </div>
                        </div>

                        <div id="DiveModuleInfo" style="display: none;">
                        <div class="user_regist_Allleft" style="width: 620px">
                            <div class="user_regist_left" style="width: auto">
                                <table class="tableStyle" cellspacing="0" cellpadding="0" style="width: 620px;">
                                    <tr>
                                        <td style="width: 400px">
                                            <table class="tableStyle" cellspacing="0" cellpadding="0" style="width: 100%; margin: auto;
                                                border: 0px" rules="none">
                                                <tr>
                                                    <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                        <span style="color: #ff4800; vertical-align: middle">*</span>模块名称：
                                                    </td>
                                                    <td class="tableStyle_right" style="border: 0px">
                                                        <input id="txtMerchantDesc" type="text" runat="server" class="border_radius" maxlength="4"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                        备注：
                                                    </td>
                                                    <td class="tableStyle_right" colspan="2" style="vertical-align: top; border: 0px">
                                                        <textarea id="txtMerchantRemark" rows="13" runat="server" style="width: 250px; word-wrap: break-word;
                                                            outline: none; resize: none;"></textarea>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 200px; background-color: #edf2f6; text-align: center;vertical-align: top; height:270px;">
                                            <div style="width: 180px;margin-left: auto; margin-right: auto; margin-top:10px;">
                                                <div class="user_regist_pic" style="height: 130px; border: 0px; width: 180px;">
                                                    <img alt="" id="imgModulePhoto" src="../images/Gift/nogift.jpg" style=" height:130px; width:180px;" runat="server" />
                                                </div>
                                                <p>
                                                    <span id="imageSizeDesc" style="color: #DB9944; font-size: 12px; font-weight:bold;">相片支持大小为160*160</span>
                                                </p>
                                                <div class="common_Button_all" style="text-align: center; margin-top:2px;">
                                                    <input id="ModulePhoto_Uploadify" type="file" class="common_Button common_ServiceButton" />

                                                    <input id="btnGiftPhotoUpload" style="margin-left: 16px;" type="button" class="common_Button"
                                                        value="上传图片" onclick="javascript:$('#ModulePhoto_Uploadify').uploadifyUpload();" />
                                                </div>
                                                <div id="module_fileQueue">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center">
                                            <input id="btnModuleSave" type="button" value="保   存" class="buttonColor" />&nbsp;
                                            <input id="btnModuleReset" type="button" class="buttonRest" value="重   置" />
                                            <input type="hidden" id="MerchantPhoto" />
                                            <input type="hidden" id="MerchantID" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    
                        <div class="user_List_All">
                            <table class="table-style table-hover user_List_txt">
                                <asp:Repeater runat="server" ID="gvwMicroWebsiteIndex">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    序号
                                                </th>
                                                <th>
                                                    模块名称
                                                </th>
                                                <th>
                                                    模块缩略图
                                                </th>
                                                <th>
                                                    备注
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
                                            <td style="text-align: left">
                                                <%# Eval("MerchantDesc")%>
                                            </td>
                                            <td>
                                                <span onclick='ShowPic("<%#Eval("MerchantPhoto") %>?vi=<%=r.Next(10000, 99999) %>")' style="cursor: pointer;">
                                                    <img alt="" src="<%#Eval("MerchantPhoto").ToString().Trim() %>?vi=<%=r.Next(10000, 99999) %>"
                                                        style="width: 30px; height: 30px;" />
                                                </span>
                                            </td>
                                            <td style="text-align: left">
                                                <%# Eval("MerchantRemark").ToString() %>
                                            </td>
                                            <td class="listtd" style="width: 60px;">
                                                <input type="hidden" id="MerchantRemark<%#Eval("MerchantID") %>" value="<%# Chain.Common.StringPlus.HtmlDecode(Eval("MerchantRemark").ToString()) %>" />
                                                <a href='javascript:btnModuleEdit("<%#Eval("MerchantID") %>","<%#Eval("MerchantDesc") %>","<%#Eval("MerchantPhoto") %>")'>
                                                    <img src="../images/Gift/eit.png" alt="编辑" title="编辑" /></a>
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
