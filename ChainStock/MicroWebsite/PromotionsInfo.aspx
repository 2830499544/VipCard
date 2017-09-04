<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PromotionsInfo.aspx.cs"  ValidateRequest="false"
    Inherits="ChainStock.MicroWebsite.PromotionsInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>优惠活动</title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>

    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
        <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/MicroWebsite/PromotionsInfo.js" type="text/javascript"></script>
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
                    <div id="divShopSelect" style="display: none">
            <table class="table-style table-hover user_List_txt">
                <tr class="th">
                    <th style="width: 58px">
                        选择
                    </th>
                    <th>
                        商家名称
                    </th>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="height: 380px; width:280px; overflow:scroll;">
                            <table class="table-style table-hover user_List_txt">
                                <asp:Repeater runat="server" ID="rptShop">
                                    <ItemTemplate>
                                        <tr class="td">
                                      
                                            <td style="text-align: center; width: 58px; padding-left: 0px">
                                                <input type="checkbox" name="cbkSelectShop" class="cbkSelectShop" value="<%# Eval("ShopID") %>" />

                                              
                                            </td>
                                            <td style="text-align: center;  padding-left: 5px">
                                                <span class="shopname"><%# Eval("ShopName") %></span>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center; height: 36px">
                        <input type="button" id="btnSelectShopOK" class="common_Button"  style="float: inherit"
                            value="确  认" />
                    </td>
                </tr>
            </table>
        </div>
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
                                                        <td class="tableStyle_left" style="border: 0px; ">
                                                            <span style="color: #ff4800; vertical-align: middle">*</span>活动标题：
                                                        </td>
                                                        <td class="tableStyle_right" style="border: 0px">
                                                       <input id="txtPromotionsTitle" runat="server" type="text" class="input_txt border_radius" style="width:550px;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                    <td class="tableStyle_left">
                                        有效期：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label style="vertical-align: text-bottom;">
                                            <input type="radio" name="radPromotionsYesOrNo" id="radPromotionsYes" value="0" checked="checked" />
                                                <label style="vertical-align: middle;" for="radPromotionsYes">永久有效</label>
                                        </label>

                                        <label style="vertical-align: text-bottom;">
                                                <input type="radio" name="radPromotionsYesOrNo" id="radPromotionsNo" value="1" />
                                                <input id="txtPromotionsStartTime" type="text" class="Wdate border_radius" style="float: none;" />
                                                <label style="vertical-align: middle;">&nbsp;至&nbsp;</label>
                                                <input id="txtPromotionsEndTime" type="text" class="Wdate border_radius" style="float: none;" />
                                        </label>
                                    </td>
                                </tr>
                                                    <tr>
                                                        <td class="tableStyle_left" style="border: 0px; ">
                                                              <span style="color: #ff4800; vertical-align: middle">*</span>活动图片：
                                                        </td>
                                                        <td>
                                                        
                                                         <div style="width: 180px;margin-left: 0; margin-top:10px;">
                                                    <div class="user_regist_pic" style="height: 70px; border: 0px; width: 180px;">
                                                        <img alt="" id="imgPromotionsPhoto" src="../images/Gift/nogift.jpg" style=" height:70px; width:160px;" runat="server" />
                                                    </div>
                                                    <p>
                                                        <span style="color:Gray; font-size: 12px; font-weight:bold;">相片支持大小为640*280</span>
                                                    </p>
                                                    <div class="common_Button_all" style="text-align: center; margin-top:2px;">
                                                        <input id="PromotionsPhoto_Uploadify" type="file" class="common_Button common_ServiceButton" />

                                                        <input id="btnGiftPhotoUpload" style="margin-left: 16px;" type="button" class="common_Button"
                                                            value="上传图片" onclick="javascript:$('#PromotionsPhoto_Uploadify').uploadifyUpload();" />
                                                    </div>
                                                    <div id="PromotionsCenter_fileQueue"></div>
                                                </div>
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                    <td colspan="2">&nbsp;</td>
                                                    </tr>
                                                      <tr>
                                    <td class="tableStyle_left">
                                        优惠对象：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select name="select" id="sltPromotionsLevel" runat="server" class="selectWidth" style="width: 150px;">
                                            
                                        </select>
                                    </td>
                                </tr>
                                 <tr>
                                     <td class="tableStyle_left">
                                        参与活动商家：
                                    </td>
                                    <td class="tableStyle_right">
                                    <table>
                                    <tr>
                                    <td>  
                                      <textarea id="txtShopNameList" rows="3" runat="server" readonly="readonly" style="width: 70%; word-wrap: break-word;
                                outline: none; resize: none;" ></textarea>
                                      <textarea id="txtShopList" rows="3" runat="server" readonly="readonly" style="width: 70%; word-wrap: break-word; display:none;
                                outline: none; resize: none;" ></textarea>
                                
                                
                                </td>
                                       <td align="left">       <input id="btnSelectShop" type="button" value="选择商家"  /></td>
                                    </tr>
                                    </table>
                                    </td>
                                    </tr>
                                                     <tr>
                                                        <td class="tableStyle_left" style="border: 0px; ">
                                                            活动说明：
                                                        </td>
                                                        <td>
                                                           <textarea id="txtPromotionsDesc" style="width: 98%; height: 300px; "   runat="server"
                                name="txtPromotionsDesc" class="xheditor-simple {urlType:'abs',upLinkUrl:'/service/xhEditorUpload.ashx',upImgUrl:'/service/xhEditorUpload.ashx',upFlashUrl:'/service/xhEditorUpload.ashx',upMediaUrl:'/service/xhEditorUpload.ashx' ,urlType:'root'}">
                                
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
                                                           <input id="txtPromotionsRemark" type="text" runat="server" style=" width:350px;" class="border_radius" />
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                           
                                        </tr>
                                   
                                      
                                    </table>
                    

                    <div class="user_regist_left">
                        <div style="text-align: center; height: 36px">
                            <input id="btnPromotionsSave" type="button" value="保   存" class="buttonColor" />&nbsp;
                                                <input id="btnPromotionsReset" type="button" class="buttonRest" value="重   置" />
                                                <input type="hidden" id="txtPromotionsID"  runat="server"/>
                                                <input type="hidden" id="txtUpdatePromotionsName"  runat="server"/>
                        </div>
                    </div>
                </div>
            </div>
      
        </div>

    </form>
</body>
</html>
