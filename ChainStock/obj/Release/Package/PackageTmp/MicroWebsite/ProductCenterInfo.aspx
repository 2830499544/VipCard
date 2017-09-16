<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductCenterInfo.aspx.cs" ValidateRequest="false"
    Inherits="ChainStock.MicroWebsite.ProductCenterInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商品登记</title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>

    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>

    <script src="../Scripts/Module/MicroWebsite/ProductCenterInfo.js" type="text/javascript"></script>
      <script src="../Inc/xheditor/xheditor-1.1.14-zh-cn.min.js" type="text/javascript"></script>
    <style type="text/css">
        .uploadifyQueueItem
        {
            width: 160px;
        }
        
        .uploadifyProgress
        {
            width: 80%;
        }
        
        .fileName
        {
            width: 0px;
            height: 0px;
            display: block;
            overflow: hidden;
        }
    </style>
</head>
<body>
    <form id="frmGoodsInfo" runat="server">
        <div class="system_Info box_right">
            <div class="system_Top">
                <div class="user_regist_title">
                    <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                </div>
            </div>

            <div class="user_regist_Allleft">
                <div class="user_regist_left">
                    <div class="user_regist_infor">
                        产品信息
                    </div>

                    <table width="720" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle" style="float:left;">
                         <tr>
                                            <td style="width: 400px">
                                                <table class="tableStyle" cellspacing="0" cellpadding="0" style="width: 100%; margin: auto;
                                                    border: 0px" rules="none">
                                                    <tr>
                                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                            <span style="color: #ff4800; vertical-align: middle">*</span>产品名称：
                                                        </td>
                                                        <td class="tableStyle_right" style="border: 0px">
                                                            <input id="txtProductName" type="text" runat="server" style=" width:350px;" class="border_radius" />
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                            <span style="color: #ff4800; vertical-align: middle">*</span>所属分类：
                                                        </td>
                                                        <td class="tableStyle_right" style="border: 0px">
                                                          <select id="sltClassID" runat="server" class="selectWidth">
                                                          </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                              <span style="color: #ff4800; vertical-align: middle">*</span>产品图片：
                                                        </td>
                                                        <td>
                                                        
                                                         <div style="width: 180px;margin-left: 0; margin-top:10px;">
                                                    <div class="user_regist_pic" style="height: 160px; border: 0px; width: 180px;">
                                                        <img alt="" id="imgProductPhoto" src="../images/Gift/nogift.jpg" style=" height:160px; width:160px;" runat="server" />
                                                    </div>
                                                    <p>
                                                        <span style="color:Gray; font-size: 12px; font-weight:bold;">相片支持大小为300*300</span>
                                                    </p>
                                                    <div class="common_Button_all" style="text-align: center; margin-top:2px;">
                                                        <input id="ProductPhoto_Uploadify" type="file" class="common_Button common_ServiceButton" />

                                                        <input id="btnGiftPhotoUpload" style="margin-left: 16px;" type="button" class="common_Button"
                                                            value="上传图片" onclick="javascript:$('#ProductPhoto_Uploadify').uploadifyUpload();" />
                                                    </div>
                                                    <div id="productCenter_fileQueue"></div>
                                                </div>
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                    <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                     <tr>
                                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                            产品描述：
                                                        </td>
                                                        <td>
                                                           <textarea id="txtProductDesc" style="width: 98%; height: 300px; "   runat="server"
                                name="txtProductDesc" class="xheditor-simple {urlType:'abs',upLinkUrl:'/service/xhEditorUpload.ashx',upImgUrl:'/service/xhEditorUpload.ashx',upFlashUrl:'/service/xhEditorUpload.ashx',upMediaUrl:'/service/xhEditorUpload.ashx',urlType:'root'}">
                                
                                </textarea>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                            备注：
                                                        </td>
                                                        <td class="tableStyle_right" style="vertical-align: top; border: 0px">
                                                           <input id="txtProductRemark" type="text" runat="server" style=" width:350px;" class="border_radius" />
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                           
                                        </tr>
                                   
                                      
                                    </table>
                    

                    <div class="user_regist_left">
                        <div style="text-align: center; height: 36px">
                            <input id="btnProductSave" type="button" value="保   存" class="buttonColor" />&nbsp;
                                                <input id="btnProductReset" type="button" class="buttonRest" value="重   置" />
                                                <input type="hidden" id="txtProductID"  runat="server"/>
                                                <input type="hidden" id="txtUpdateProductName"  runat="server"/>
                        </div>
                    </div>
                </div>
            </div>
      
        </div>
    </form>
</body>
</html>
