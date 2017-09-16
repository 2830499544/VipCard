<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXinRule.aspx.cs" Inherits="ChainStock.SystemManage.WeiXinRule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/WeiXinRule.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Inc/xheditor/xheditor-1.1.14-zh-cn.min.js" type="text/javascript"></script>
    <style type="text/css">
        #btnNewsPicUploadUploader
        {
            float: left;
            margin-left: 40px;
        }
    </style>
</head>
<body>
    <form id="frmWeiXinRule" runat="server">
    <input type="hidden" runat="server" id="txtNewsID" value="0" />
    <input type="hidden" runat="server" id="txtRuleID" value="" />
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_regist_Allleft">
            <div class="user_regist_left">
                <div class="user_regist_infor" style="width: 100%">
                    注意事项
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_right">
                            图文描述内容的长度最好不要超过200个长度
                        </td>
                    </tr>
                </table>
                <div class="user_regist_infor" style="width: 100%">
                    编辑图文
                </div>
                <table class="tableStyle" cellspacing="0" cellpadding="2">
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>回复序号：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" id="txtNewsNumber" class="border_radius" />
                            <input type="text" id="txtNewsType" style="display: none;" />
                            <input type="text" id="txtNewsRule" style="display: none;" />
                        </td>
                        <td rowspan="5" style="width: 250px; vertical-align: top; border: 1px solid #cccccc;">
                            <div>
                                <img src="" width="180" height="150" id="imgNewsPhoto" /><br />
                                <span style="color: #DB9944; font-size: 12px; height: 30px; line-height: 30px;">图片支持大小为640*320</span>
                            </div>
                            <div style="text-align: center; width: 210px; text-align: center;">
                                <input id="btnNewsPicUpload" type="file" class="buttonColor" style="display: none;" />
                                <input type="button" value="上传图片" class="common_Button" style="float: right;" onclick="javascript:$('#btnNewsPicUpload').uploadifyUpload();" />
                            </div>
                            <div id="divNewsPic_fileQueue" style="height: 60px;">
                            </div>
                        </td>
                    </tr>
                    <tr id="trNewsRuleDescAdd">
                        <td class="tableStyle_left">
                            规则描述：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtNewsRuleDescAdd" class="border_radius" type="text" style="width: 330px;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>图文标题：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtNewsTitle" class="border_radius" type="text" style="width: 330px;" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>图文描述：
                        </td>
                        <td class="tableStyle_right">
                            <textarea id="txtNewsDesc" rows="3" runat="server" class="border_radius" style="width: 330px;
                                word-wrap: break-word; height: 50px;"></textarea>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="tableStyle_left">
                            图片链接：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtPicUrl" type="text" class="border_radius" style="width: 330px; display: none;" />
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td class="tableStyle_left">
                            跳转链接：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtUrl" class="border_radius" type="text" style="width: 330px;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea id="txtNoticeDetail" style="width: 100%; height: 200px;" runat="server"
                                name="txtNoticeDetail" class="xheditor-simple {urlType:'abs',upLinkUrl:'/service/xhEditorUpload.ashx',upImgUrl:'/service/xhEditorUpload.ashx',upFlashUrl:'/service/xhEditorUpload.ashx',upMediaUrl:'/service/xhEditorUpload.ashx'}"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center; height: 36px">
                            <input id="btnNewsRuleSave" type="button" class="buttonColor" value="保   存 " />
                            &nbsp
                            <input id="btnNewsRuleReset" type="button" class="buttonRest" value="重   置 " />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
    <input type="text" runat="server" id="txtSystemDomain" style="display:none" />
</body>
</html>
