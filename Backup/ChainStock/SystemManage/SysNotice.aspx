<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysNotice.aspx.cs" Inherits="ChainStock.SystemManage.SysNotice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/SysNotice.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Inc/xheditor/xheditor-1.1.14-zh-cn.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmSysNotice" runat="server">
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_regist_Allleft">
            <div class="user_regist_infor" style="width: 100%">
                公告号：<span id="spNoticeCode" runat="server" style="font-weight: bold; color: #000;"></span>
                <input id="NoticeID" type="hidden" runat="server" />
                &nbsp;&nbsp;&nbsp;&nbsp; 发布人：<span id="spRelaseName" runat="server" style="font-weight: bold;
                    color: #000;"></span> &nbsp;&nbsp;&nbsp;&nbsp; 发布时间：<span id="spRelaseTime" runat="server"
                        style="font-weight: bold; color: #000;"></span>
            </div>
            <div class="user_regist_left">
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>公告标题：
                        </td>
                        <td class="tableStyle_right" style="padding-top: 2px; text-align: center; padding-left: 0px;">
                            <input id="txtNoticeTitle" class="border_radius" runat="server" type="text" style="width: 98%;
                                float: none;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>公告内容：
                        </td>
                        <td style="padding: 2px 0px 2px 0px; text-align: center;">
                            <textarea id="txtNoticeDetail" style="width: 98%; height: 300px;" runat="server"
                                name="txtNoticeDetail" class="xheditor-simple {urlType:'abs',upLinkUrl:'/service/xhEditorUpload.ashx',upImgUrl:'/service/xhEditorUpload.ashx',upFlashUrl:'/service/xhEditorUpload.ashx',upMediaUrl:'/service/xhEditorUpload.ashx'}"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="tableStyle_right">
                            <div class="buton" style="text-align: center; width: 100%;">
                                <input type="button" id="btnNoticeSave" runat="server" class="buttonColor" value="保   存" />
                                &nbsp
                                <input type="button" id="btnNoticeReset" runat="server" class="buttonRest" value="重   置" />
                                <input id="NoticeaAddOrEdit" type="hidden" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
