<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysRotateInfo.aspx.cs"
    Inherits="ChainPoint.MicroWebsite.SysRotate" %>

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
    <script src="/Scripts/Module/MicroWebsite/SysRotate.js" type="text/javascript"></script>
    <script src="/Scripts/Module/WeiXin/ckeditor/ckeditor.js" type="text/javascript"></script>

    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
                <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>
    <style type="text/css">
        #btnNewsPicUploadUploader
        {
            float: left;
            margin-left: 30px;
        }
        .tableStyle_left{ width:150px;}
        .th-child th{text-align: center;height: 24px; border-top:0; }
     
    </style>
</head>
<body>
    <form id="frmWeiXinRuleList" runat="server">

    <div class="system_Info box_right" style="width: 99%;">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_List_All">
            <div class="user_regist_infor" style="width: 100%">
                大转盘活动内容             
            </div>
           
            <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 100%; margin: auto">
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>活动名称：
                                    </td>
                                    <td class="tableStyle_right" colspan="9">
                                        <input id="txtRotateName" type="text" runat="server" class="input_txt border_radius" style="width:550px;" />
                                         <input id="txtRotateID" type="text" runat="server" class="input_txt border_radius" style=" display:none;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>活动说明：
                                    </td>
                                    <td class="tableStyle_right" colspan="9">
                                    <textarea id="txtRotateRemark" runat="server" rows="5" style="width: 550px; word-wrap: break-word;
                                        outline: none; resize: none;"></textarea>
                                    </td>
                                </tr>
                                  <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>活动图片：
                        </td>
                        <td class="tableStyle_right" align="left">
                        <div style="width: 180px; text-align:left; margin-right: auto; margin-top:10px;">
                                                    <div class="user_regist_pic" style="height: 160px; border: 0px; width: 180px;">
                                                        <img alt="" id="imgRotatePhoto" src="../images/Gift/nogift.jpg" style=" height:146px; width:84px;" runat="server" />
                                                 
                                                      
                                                    </div>
                                                    <p>
                                                        <span style="color:Gray; font-size: 12px; font-weight:bold;">相片支持大小为420*780</span>
                                                         <input type="hidden" id="txtRotatePhoto" runat="server" />
                                                    </p>
                                                    <div class="common_Button_all" style="text-align: center; margin-top:2px;">
                                                        <input id="RotatePhoto_Uploadify" type="file" class="common_Button common_ServiceButton" />

                                                        <input id="btnRotatePhotoUpload" style="margin-left: 16px;" type="button" class="common_Button"
                                                            value="上传图片" onclick="javascript:$('#RotatePhoto_Uploadify').uploadifyUpload();" />
                                                    </div>
                                                    <div id="Rotate_fileQueue"></div>
                                                </div>
                        </td>
                    </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span> 活动时间：
                                    </td>
                                    <td class="tableStyle_right" colspan="9">
                                    <table style="width:400px;" >
                                    <tr>
                                    <td>   <input id="txtStartTime" type="text" runat="server" class="Wdate border_radius" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" /> </td>
                                    <td>到</td>
                                    <td> <input id="txtEndTime" type="text" runat="server" class="Wdate border_radius" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"  /></td>
                                    </tr>
                                    </table>
                                      
                                        
                                          
                                    </td>
                                </tr>
                                 <tr>
                                    <td class="tableStyle_left">
                                         <span style="color: #ff4800; vertical-align: middle">*</span>活动指令：
                                    </td>
                                    <td class="tableStyle_right" colspan="9">
                                        <input id="txtRotateRegion" type="text" runat="server" class="input_txt border_radius" style="width:50px;"   value="1" />
                                      <%--  (预计活动人数直接影响抽奖概率 中奖率=奖品总数/预计活动人数 * 每人抽奖次数。如果中奖率为100%，建议设置1人参加)--%>
                                    </td>
                                </tr>
                                  <tr>
                                    <td class="tableStyle_left">
                                         <span style="color: #ff4800; vertical-align: middle">*</span>是否开启只能抽中一个奖品：
                                    </td>
                                    <td class="tableStyle_right" colspan="9">
                                        <input id="cbkIsWinOne" type="checkbox" runat="server"   />
                                      (当客户中了一个奖品之后,后面次数只能抽中谢谢参与)
                                    </td>
                                </tr>
                                                               <tr>
                       
                        <td class="tableStyle_right" colspan="8">
                           <label style="vertical-align: text-bottom;color:blue; font-size:8pt;">
                             特需号码请填写会员手机号，如果有多个手机号,以英文符号,隔开                                                   
                            </label>

                        </td>
                    </tr>
                                 <tr>
                                    <td class="tableStyle_left" style=" width:100px;">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>(奖项一)奖项名称：
                                    </td>
                                     <td class="tableStyle_right" style=" width:100px;">
                                        <input id="txtOneName" type="text" runat="server" class="input_txt border_radius" style="width:100px;"  value="一等奖"/>
                                    </td>
                                     <td class="tableStyle_left" style=" width:100px;">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>奖品名称：
                                    </td>
                                    <td class="tableStyle_right" style=" width:100px;">
                                        <input id="txtOnePrizeName" type="text" runat="server" class="input_txt border_radius" style="width:150px;"  value="奖品一"/>
                                    </td>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>奖品数量：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtOnePrizeCount" type="text" runat="server" class="input_txt border_radius" style="width:50px;"   value="0"/>
                                    </td>
                                      <td class="tableStyle_left">
                                       已中奖数：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtOneWinCount" type="text" runat="server"   disabled="disabled" class="input_txt border_radius" style="width:50px;"   value="0"/>
                                    </td>
                                     <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>中奖几率：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtOneRate" type="text" runat="server" class="input_txt border_radius" style="width:50px;"   value="0"/>%
                                    </td>
                                   
                                    </tr>
                                    <tr>
                                     <td class="tableStyle_left">
                                      内定号码：
                                    </td>

                                    <td class="tableStyle_right" colspan="9">
                                        <input id="txtOneMobile" type="text" runat="server" class="input_txt border_radius" style="width:200px;"   value=""/>
                                    </td>
                                </tr>
                                 <tr>
                                  <td class="tableStyle_left" style=" width:100px;">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>(奖项二)奖项名称：
                                    </td>
                                     <td class="tableStyle_right" style=" width:100px;">
                                        <input id="txtTwoName" type="text" runat="server" class="input_txt border_radius" style="width:100px;"  value="二等奖"/>
                                    </td>
                                    <td class="tableStyle_left">
                                           <span style="color: #ff4800; vertical-align: middle">*</span>奖品名称：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtTwoPrizeName" type="text" runat="server" class="input_txt border_radius" style="width:150px;"  value="奖品二"/>
                                    </td>
                                    <td class="tableStyle_left">
                                          <span style="color: #ff4800; vertical-align: middle">*</span>奖品数量：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtTwoPrizeCount" type="text" runat="server"  class="input_txt border_radius" style="width:50px;"  value="0"/>
                                    </td>
                                      <td class="tableStyle_left">
                                        已中奖数：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtTwoWinCount" type="text" runat="server"   disabled="disabled" class="input_txt border_radius" style="width:50px;"   value="0"/>
                                    </td>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>中奖几率：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtTwoRate" type="text" runat="server" class="input_txt border_radius" style="width:50px;"   value="0"/>%
                                    </td>
                                    
                                </tr>
                                <tr>
                                
                                <td class="tableStyle_left">
                                       内定号码：
                                    </td>

                                    <td class="tableStyle_right" colspan="9" >
                                        <input id="txtTwoMobile" type="text" runat="server" class="input_txt border_radius" style="width:200px;"   value=""/>
                                    </td>
                                </tr>
                                 <tr>
                                  <td class="tableStyle_left" style=" width:100px;">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>(奖项三)奖项名称：
                                    </td>
                                     <td class="tableStyle_right" style=" width:100px;">
                                        <input id="txtThreeName" type="text" runat="server" class="input_txt border_radius" style="width:100px;"  value="三等奖"/>
                                    </td>
                                    <td class="tableStyle_left">
                                           <span style="color: #ff4800; vertical-align: middle">*</span>奖品名称：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtThreePrizeName" type="text" runat="server" class="input_txt border_radius" style="width:150px;" value="奖品三"/>
                                    </td>
                                    <td class="tableStyle_left">
                                           <span style="color: #ff4800; vertical-align: middle">*</span>奖品数量：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtThreePrizeCount" type="text" runat="server"  class="input_txt border_radius" style="width:50px;"  value="0"/>
                                    </td>
                                        <td class="tableStyle_left">
                                       已中奖数：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtThreeWinCount" type="text" runat="server"   disabled="disabled" class="input_txt border_radius" style="width:50px;"   value="0"/>
                                    </td>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>中奖几率：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtThreeRate" type="text" runat="server" class="input_txt border_radius" style="width:50px;"   value="0"/>%
                                    </td>
                                   
                                </tr>
                                <tr>
                                 <td class="tableStyle_left">
                                    内定号码：
                                    </td>

                                    <td class="tableStyle_right" colspan="9" >
                                        <input id="txtThreeMobile" type="text" runat="server" class="input_txt border_radius" style="width:200px;"   value=""/>
                                    </td>
                                </tr>
                                 <tr>
                                  <td class="tableStyle_left" style=" width:100px;">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>(奖项四) 奖项名称：
                                    </td>
                                     <td class="tableStyle_right" style=" width:100px;">
                                        <input id="txtFourName" type="text" runat="server" class="input_txt border_radius" style="width:100px;"  value="四等奖"/>
                                    </td>
                                    <td class="tableStyle_left">
                                          <span style="color: #ff4800; vertical-align: middle">*</span>奖品名称：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtFourPrizeName" type="text"  runat="server" class="input_txt border_radius" style="width:150px;"   value="奖品四"/>
                                    </td>
                                    <td class="tableStyle_left">
                                            <span style="color: #ff4800; vertical-align: middle">*</span>奖品数量：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtFourPrizeCount" type="text" runat="server" class="input_txt border_radius" style="width:50px;"  value="0"  />
                                    </td>
                                        <td class="tableStyle_left">
                                        已中奖数：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtFourWinCount" type="text" runat="server"  disabled="disabled" class="input_txt border_radius" style="width:50px;"   value="0"/>
                                    </td>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>中奖几率：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtFourRate" type="text" runat="server" class="input_txt border_radius" style="width:50px;"   value="0"/>%
                                    </td>
                                   
                                </tr>
                                <tr>
                                 <td class="tableStyle_left">
                                 内定号码：
                                    </td>

                                    <td class="tableStyle_right" colspan="9" >
                                        <input id="txtFourMobile" type="text" runat="server" class="input_txt border_radius" style="width:200px;"   value=""/>
                                    </td>
                                </tr>
                                 <tr>
                                  <td class="tableStyle_left" style=" width:100px;">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>(奖项五)奖项名称：
                                    </td>
                                     <td class="tableStyle_right" style=" width:100px;">
                                        <input id="txtFiveName" type="text" runat="server" class="input_txt border_radius" style="width:100px;"  value="五等奖"/>
                                    </td>
                                    <td class="tableStyle_left">
                                            <span style="color: #ff4800; vertical-align: middle">*</span>奖品名称：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtFivePrizeName" type="text" runat="server" class="input_txt border_radius" style="width:150px;"  value="奖品五"/>
                                    </td>
                                    <td class="tableStyle_left">
                                         <span style="color: #ff4800; vertical-align: middle">*</span>奖品数量：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtFivePrizeCount" type="text" runat="server" class="input_txt border_radius" style="width:50px;"  value="0" />
                                    </td>
                                        <td class="tableStyle_left">
                                       已中奖数：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtFiveWinCount" type="text" runat="server" disabled="disabled"  class="input_txt border_radius" style="width:50px;"   value="0"/>
                                    </td>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>中奖几率：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtFiveRate" type="text" runat="server" class="input_txt border_radius" style="width:50px;"   value="0"/>
                                        <label>%</label>
                                    </td>
                                 
                                </tr>
                                <tr>
                                   <td class="tableStyle_left" >
                                  内定号码：
                                    </td>

                                    <td class="tableStyle_right" colspan="9" >
                                        <input id="txtFiveMobile" type="text" runat="server" class="input_txt border_radius" style="width:200px;"   value=""/>
                                    </td>
                                </tr>
                                 <tr>
                                  <td class="tableStyle_left" style=" width:200px;">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>(奖项六)奖项名称：
                                    </td>
                                     <td class="tableStyle_right" style=" width:100px;">
                                        <input id="txtSixName" type="text" runat="server" class="input_txt border_radius" style="width:100px;"  value="六等奖"/>
                                    </td>
                        

                                    <td class="tableStyle_left">
                                          <span style="color: #ff4800; vertical-align: middle">*</span>奖品名称：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtSixPrizeName" type="text" runat="server" class="input_txt border_radius" style="width:150px;"  value="奖品六"/>
                                    </td>
                                    <td class="tableStyle_left">
                                         <span style="color: #ff4800; vertical-align: middle">*</span>奖品数量：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtSixPrizeCount" type="text" runat="server"  class="input_txt border_radius" style="width:50px;" value="0" />
                                    </td>
                                        <td class="tableStyle_left">
                                       已中奖数：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtSixWinCount" type="text" runat="server" disabled="disabled"  class="input_txt border_radius" style="width:50px;"   value="0"/>
                                    </td>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>中奖几率：
                                    </td>

                                    <td class="tableStyle_right">
                                        <input id="txtSixRate" type="text" runat="server" class="input_txt border_radius" style="width:50px;"   value="0"/>%
                                    </td>
                                 
                                </tr>
                                <tr>
                                   <td class="tableStyle_left">
                                     内定号码：
                                    </td>

                                    <td class="tableStyle_right" colspan="9" >
                                        <input id="txtSixMobile" type="text" runat="server" class="input_txt border_radius" style="width:200px;"   value=""/>
                                    </td>
                                </tr>
    
                                <tr>
                                    <td colspan="9" style="text-align: center">
                                         <asp:Button runat="server" ID="btnMoneySave"  OnClick="btnSave_Click"
                                Text="保   存" class="buttonColor" />
                                        &nbsp
                                        <input id="btnRotateReset" type="button" class="buttonRest" value="重   置 " />
                                        <input type="hidden" id="txtType" />
                                    </td>
                                </tr>
                            </table>
       


            </div>
       
    </div>
    </form>
</body>
</html>
