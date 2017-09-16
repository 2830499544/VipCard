<%@ Page validateRequest="false" Language="C#" AutoEventWireup="true" CodeBehind="BackgroundSet.aspx.cs"
    Inherits="ChainStock.MicroWebsite.BackgroundSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Inc/xheditor/xheditor-1.1.14-zh-cn.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/MicroWebsite/Background.js" type="text/javascript"></script>
    
    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>

</head>
<body>
    <form id="frmMemberExplanation" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div class="user_List_All">
                        <div style="width: 180px; margin-top:10px; text-align:left;">
                                                    <div class="user_regist_pic" style="height: 280px; border: 0px; width: 160px;">
                                                        <img alt="" id="imgBackgroundPhoto" src="../images/Gift/nogift.jpg" style=" height:280px; width:160px;" runat="server" />
                                                    </div>
                                                    <p>
                                                        <span style="color:Gray; font-size: 12px; font-weight:bold;">相片支持大小为640*1130</span>
                                                    </p>
                                                    <div class="common_Button_all" style="text-align: center; margin-top:2px;">
                                                        <input id="BackgroundPhoto_Uploadify" type="file" class="common_Button common_ServiceButton" />

                                                        <input id="btnBackgroundPhotoUpload" style="margin-left: 16px;" type="button" class="common_Button"
                                                            value="上传图片" onclick="javascript:$('#BackgroundPhoto_Uploadify').uploadifyUpload();" />
                                                    </div>
                                                    <div id="Background_fileQueue"></div>
                                                      <input type="hidden" id="txtUpdateBackgroundName" runat="server" />
                                                </div>
                       
                    </div>
                
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
