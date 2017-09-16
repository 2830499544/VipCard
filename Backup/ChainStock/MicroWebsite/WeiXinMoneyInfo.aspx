<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXinMoneyInfo.aspx.cs" Inherits="ChainStock.MicroWebsite.WeiXinMoneyInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>

    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>

    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/Module/MicroWebsite/WeiXinMoneyInfo.js" type="text/javascript"></script>
        <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
            <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmSetLevel" runat="server">
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_regist_Allleft" id="divAddLevel">
            <div class="user_regist_left">
                <div class="user_regist_infor" style="text-align: left">
                    红包活动
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>红包活动名称：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtMoneyTitle" type="text" class="border_radius" runat="server" style=" width:400px;"/>
                            <input id="txtMoneyID" type="hidden" runat="server" />
                        </td>
                        <td  rowspan="16" valign="top" style="color:Black; text-align:left; ">
                        <div  style=" margin-left:10px;"> 
                        <br/>
                        <p><b> 用户收到红包后的消息</b></p><br/>
                         <p>你收到了一个红包</p>
                          <p> <span style="color:gray;">XX月XX日</span></p><br>
                           <p>你参加<span style="color:green; " id="spMoneyTitle">活动名称</span>，成功获得商户赠送的红包。</p><br/>
                            <p><span style="color:gray;"  id="spMoneyDesc">点击消息拆开红包即可获得现金</span></p>
                              <p>详情</p><br/>
                              <br/>
                              <p style=" border-top:1px solid #EDEDED;">&nbsp;</p>
                              <br/>
                             <p><b> 用户收到红包后的消息</b></p><br/>
                              <p>你领取了一个红包</p>
                               <p><span style="color:gray;">XX月XX日</span></p><br/>
                                <p>你成功领取了<span style="color:green;">活动名称</span>发放的红包。红包金额<span style="color:green;">1.00元</span></p><br>
                                 <p><span style="color:gray;"  id="spMoneyWish">祝福语</span></p>
                                  <p>详情</p>
                        </div>
                      
                         

                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>红包活动说明：
                        </td>
                        <td class="tableStyle_right">
                            <label style="vertical-align: text-bottom;">
                                <input id="txtMoneyDesc" type="text" class="border_radius" runat="server" style=" width:400px;" />
                                <label style="vertical-align: middle;">
                                   
                                </label>
                            </label>
                        </td>
                    </tr>
                  
                     <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>红包祝福语：
                        </td>
                        <td class="tableStyle_right">
                         <label style="vertical-align: text-bottom;">
                <textarea id="txtMoneyWish"  style=" width:400px;  height:50px;" runat="server"></textarea>
                                <label style="vertical-align: middle;">
                                   
                                </label>
                            </label>
                        </td>
                    </tr>
                     <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>活动图片：
                        </td>
                        <td class="tableStyle_right" align="left">
                        <div style="width: 180px; text-align:left; margin-right: auto; margin-top:10px;">
                                                    <div class="user_regist_pic" style="height: 160px; border: 0px; width: 180px;">
                                                        <img alt="" id="imgMoneyPhoto" src="../images/Gift/nogift.jpg" style=" height:146px; width:84px;" runat="server" />
                                                 
                                                      
                                                    </div>
                                                    <p>
                                                        <span style="color:Gray; font-size: 12px; font-weight:bold;">相片支持大小为420*780</span>
                                                         <input type="hidden" id="txtMoneyPhoto" runat="server" />
                                                    </p>
                                                    <div class="common_Button_all" style="text-align: center; margin-top:2px;">
                                                        <input id="MoneyPhoto_Uploadify" type="file" class="common_Button common_ServiceButton" />

                                                        <input id="btnMoneyPhotoUpload" style="margin-left: 16px;" type="button" class="common_Button"
                                                            value="上传图片" onclick="javascript:$('#MoneyPhoto_Uploadify').uploadifyUpload();" />
                                                    </div>
                                                    <div id="Money_fileQueue"></div>
                                                </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>红包指令：
                        </td>
                        <td class="tableStyle_right">
                         <label style="vertical-align: text-bottom;">
                          <input id="txtMoneyRegion" type="text" class="border_radius" runat="server"  />
         
                                <label style="vertical-align: middle;">
                                   
                                </label>
                            </label>
                        </td>
                    </tr>
                     <tr>
                        <td class="tableStyle_left">
                            <span style="color: #ff4800; vertical-align: middle">*</span>活动结束时间：
                        </td>
                        <td class="tableStyle_right">
                         <label style="vertical-align: text-bottom;">
                       
                                <input id="txtEndTime" type="text" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" class="Wdate"  />
                              
                            
                            </label>
                        </td>
                    </tr>
                       <tr>
                        <td class="tableStyle_left" rowspan="2">
                            <span style="color: #ff4800; vertical-align: middle">*</span>活动总预算：
                        </td>
                        <td class="tableStyle_right">
                         <label style="vertical-align: text-bottom;">
                                <input id="txtTotalMoney" type="text" class="border_radius" runat="server"  />
                                <label style="vertical-align: middle;">
                                   （元）
                                </label>
                            </label>
                        </td>
                    </tr>
                      <tr>
                       
                        <td class="tableStyle_right">
                           <label style="vertical-align: text-bottom;color:Gray; font-size:8pt;">
                             当领取的红包总金额达到预算后，会员将无法再领取红包（0为不设预算,请确保支付商户账户余额充足                                                               
                            </label>

                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" rowspan="3">
                           <span style="color: #ff4800; vertical-align: middle">*</span> 红包金额(元)：
                        </td>
                        <td class="tableStyle_right">
                         <label style="vertical-align: text-bottom;">
                              <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radMoneyType" id="radMoneyTypeOne" value="1"   runat="server"/>
                                        <label style="vertical-align: middle;">
                                            随机金额</label></label>
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radMoneyType" id="radMoneyTypeTwo"  value="2"  runat="server" />
                                        <label style="vertical-align: middle;">
                                            固定金额</label></label>
                             
                            </label>
                        </td>
                     </tr>
                     
                     <tr >
                       
                        <td class="tableStyle_right"  runat="server">
                           <label style="vertical-align: text-bottom;"  runat="server"  id="trMoneyTypeOne" >
                          
                               <label style="vertical-align: middle; float:left;">
                                            金额</label>
                                        
                                        <input id="txtStartMoney" type="text" class="border_radius" runat="server"   style=" width:50px;"/>
                                        <label style="vertical-align: middle; float:left;">
                                            至</label>
                                    <label style="vertical-align: text-bottom;">
                                         <input id="txtEndMoney" type="text" class="border_radius" runat="server"   style=" width:50px;"/>
                                       <label style="vertical-align: middle; float:left;">
                                            (元)</label>
                             
                            </label>
                            </label>
                            <label style="vertical-align: text-bottom;" id="trMoneyTypeTwo" runat="server">
                                <input id="txtFixedMoney" type="text" class="border_radius" runat="server"  style=" width:50px;" />
                                <label style="vertical-align: middle;">
                                   (元)
                                </label>
                            </label>
                        </td>
                    </tr>
                    
                     <tr>
                       
                        <td class="tableStyle_right">
                           <label style="vertical-align: text-bottom; color:Gray; font-size:8pt;">
                             单个红包多限定在1-200元，若选择"随机金额",系统将在您设置的金额区间内产生一个随机金额                                       
                             
                            </label>

                        </td>
                    </tr>
                        <tr>
                        <td class="tableStyle_left" rowspan="2">
                            <span style="color: #ff4800; vertical-align: middle">*</span>最多可领取：
                        </td>
                        <td class="tableStyle_right">
                         <label style="vertical-align: text-bottom;">
                                <input id="txtMaxCount" type="text" class="border_radius" runat="server"     style=" width:50px;"/>
                                <label style="vertical-align: middle;">
                                   
                                </label>
                            </label>
                        </td>
                    </tr>
                       <tr>
                       
                        <td class="tableStyle_right">
                           <label style="vertical-align: text-bottom;color:Gray; font-size:8pt;">
                             每个会员最多可领的红包个数                                           
                            </label>

                        </td>
                    </tr>
                     <tr>
                        <td class="tableStyle_left">
                           <span style="color: #ff4800; vertical-align: middle">*</span> 领取红包机率：
                        </td>
                        <td class="tableStyle_right">
                         <label style="vertical-align: text-bottom;">
                                <input id="txtMoneyRate" type="text" style=" width:50px;"  class="border_radius" runat="server"  />
                                <label style="vertical-align: middle;">
                                  <input type="text"  style="width:20px" class="border_radius" runat="server" value="%"  readonly="readonly" />
                                </label>
                            </label>
                        </td>
                    </tr>
                      <tr>
                        <td class="tableStyle_left">
                           <span style="color: #ff4800; vertical-align: middle">*</span> 活动对象：
                        </td>
                        <td class="tableStyle_right">
                         <label style="vertical-align: text-bottom;">
                             <input id="txtQuerySql" type="hidden" runat="server" />
                     
                             <textarea id="txtMemList" style=" width:400px; height:50px" readonly="readonly" runat="server"></textarea>
                                   <textarea id="txtMemIDList" style=" width:400px; height:50px; display:none;" readonly="readonly" runat="server"></textarea>
                            </label>
                        </td>
                    </tr>
                        <tr>
                        <td class="tableStyle_left" >
                            <span style="color: #ff4800; vertical-align: middle">*</span>启用红包：
                        </td>
                        <td class="tableStyle_right">
                         <label style="vertical-align: text-bottom;" id="lblStartTime" runat="server">
                              <label style="vertical-align: text-bottom; float:left"  >
                                        <input type="radio" name="radStartType" id="radStartTypeOne" runat="server" value="1" checked="true" />
                                        <label style="vertical-align: middle;">
                                            立即开始</label></label>
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radStartType" id="radStartTypeTwo" runat="server" value="2" />
                                        <label style="vertical-align: middle;">
                                            定时开始</label>
                                           
                                            </label>
                             
                            </label>
                             <label style="vertical-align: text-bottom;">                               
                           
                                   <input id="txtStartTime" type="text" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})" class="Wdate"  />
                            </label>

                        </td>
                    </tr>
                    
                </table>
          
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td colspan="4" style="text-align: center; height: 36px; border:none">
                            <asp:Button runat="server" ID="btnMoneySave"  OnClick="btnMoneySave_Click"
                                Text="保   存" class="buttonColor" />
                            <input id="btntest" type="button" value="返回" class="buttonColor" />
                        
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>
    </form>
</body>
</html>
