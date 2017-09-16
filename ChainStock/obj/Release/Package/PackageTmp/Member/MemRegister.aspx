<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemRegister.aspx.cs" Inherits="ChainStock.Member.MemberRegister" %>

<%@ Register Src="../Controls/SysArea.ascx" TagName="SysArea" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员登记</title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/Style/calendar.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Mem/MemRegister.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.SuperSlide.2.1.js" type="text/javascript"></script>
    <object style='display:none;' id='CardReader' classid='clsid:27DD3937-FA54-45B2-9A51-64D58826AC01'></object>
    <style type="text/css">
        #MemPhoto_UploadifyUploader
        {
            float: left;
        }
    </style>
</head>
<body>
    <form id="frmMemRegister" runat="server" name="user_form" method="post">
    <input id="txtMemID" type="hidden" class="input_txt border_radius" runat="server" />
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div style="padding-left: 10px;">
            <table border="0">
                <tr>
                    <td style="width: 800px;">
                        <div style="width: 800px; font-size: 14px;">
                            <div class="user_regist_infor">
                                会员信息
                            </div>
                            <table width="720" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>会员卡号：
                                    </td>
                                    <td class="tableStyle_right" style="width: 270px">
                                        <input id="txtMemCard" runat="server" type="text" class="border_radius" title="请输入会员卡号"
                                            maxlength="20" />
                                        <label style="vertical-align: text-bottom; padding-left: 2px;">
                                            <input id="btnQRCode" runat="server" type="button" class="common_Button common_ServiceButton"
                                                value="二维码" />
                                            <input id="btnSendSenseICCard" runat="server" type="button" class="buttonColor" value="发   卡" /><%--感应式IC卡按钮响应函数--%>
                                            <input id="btnContactICCard" runat="server" type="button" class="buttonColor" value="发   卡" /><%--接触式IC卡按钮响应函数--%>
                                        </label>
                                    </td>
                                    <td class="tableStyle_left">
                                        会员姓名：
                                    </td>
                                    <td class="tableStyle_right" style="width: 270px">
                                        <input id="txtMemName" runat="server" type="text" class="border_radius" title="请输入会员卡号"
                                            maxlength="20" />
                                    </td>
                                </tr>
                                <tr id="trMemPassword" runat="server">
                                    <td class="tableStyle_left">
                                        <span runat="server" id="sppwd1" style="color: #ff4800; vertical-align: middle">*</span>会员密码：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtMemPassword" runat="server" type="password" class="border_radius" title="请输入会员卡号"
                                            maxlength="20" />
                                        <input type="checkbox" style="display: none;" runat="server" id="RegNullPwd" />
                                    </td>
                                    <td class="tableStyle_left">
                                        <span runat="server" id="sppwd2" style="color: #ff4800; vertical-align: middle">*</span>
                                        确认密码：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtMemPasswordCheck" runat="server" type="password" class="border_radius"
                                            title="请输入会员卡号" maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        手机号码：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtMemMobile" runat="server" type="text" class="border_radius" title="请输入会员卡号"
                                            maxlength="20" />
                                    </td>
                                    <td class="tableStyle_left">
                                        会员状态：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select name="user_state" id="sltMemState" runat="server" class="selectWidth">
                                            <option value="0">正常</option>
                                            <option value="1">锁定</option>
                                            <option value="2">挂失</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        会员等级：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select name="user_Level" id="sltMemLevelID" runat="server" class="selectWidth">
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        所属商家：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select id="sltShop" name="user_Level" runat="server" class="selectWidth">
                                        </select>
                                         <input  id="HDsltshop" runat="server" type="hidden" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        会员生日：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtMemBirthday" runat="server" type="text" class="Wdate border_radius"
                                            title="请输入会员生日" maxlength="20" />
                                    </td>
                                    <td class="tableStyle_left">
                                        会员性别：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select name="user_Sex" id="sltMemSex" runat="server" class="selectWidth">
                                            <option value="0">男士</option>
                                            <option value="1">女士</option>
                                        </select>
                                    </td>
                                </tr>
                                <tr  style="display:none;">
                                    <td class="tableStyle_left">
                                        赠送积分：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtMemPoint" runat="server" type="text" class="border_radius" title="请输入赠送积分(可在全局参数设置是否可以输入赠送积分)"
                                            maxlength="8" />
                                    </td>
                                    <td class="tableStyle_left" >
                                        赠送金额：
                                    </td>
                                    <td class="tableStyle_right" >
                                        <input id="txtMemMoney" runat="server" type="text" class="border_radius" title="请输入赠送余额(可在全局参数设置是否可以输入赠送余额)"
                                            maxlength="8" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        电子邮箱：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtMemEmail" runat="server" type="text" class="border_radius" title="请输入会员电子邮箱" />
                                    </td>
                                    <td class="tableStyle_left">
                                        固定电话：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtTelephone" runat="server" type="text" class="border_radius" title="请输入会员固定电话" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        身份证号：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtMemIdentityCard" runat="server" type="text" class="border_radius" title="请输入会员身份证" />
                                    </td>
                                    <td class="tableStyle_left">
                                        办卡日期：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtMemCreateTime" runat="server" type="text" class="Wdate border_radius" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        办卡人员：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select name="user_Card" id="sltMemUserID" runat="server" class="selectWidth">
                                        </select>
                                    </td>
                                    <td class="tableStyle_left">
                                        推荐人卡号：
                                    </td>
                                    <td class="tableStyle_right" colspan="1">
                                        <input id="txtMemRecommendCard" type="text" class="border_radius" runat="server"
                                            style="width: 120px" title="输入推荐人‘卡号’ 或‘姓名’ 或‘手机号码’，按回车键，可自定提取到推荐人信息；" />
                                        <label style="vertical-align: middle;">
                                            <span id="txtMemRecommendMsg" runat="server" style="margin-top: 0px; margin-left: 5px;
                                                margin-right: 0px"></span>
                                        </label>
                                        <input type="hidden" id="txtMemRecommendID" name="txtMemRecommendID" runat="server"
                                            class="border_radius" />
                                        <input type="hidden" id="txtMemRecommendName" name="txtMemRecommendName" runat="server"
                                            class="border_radius" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        过期时间：
                                    </td>
                                    <td colspan="3" class="tableStyle_right">
                                        <input id="txtMemPastTime" type="text" class="Wdate border_radius" runat="server" />
                                        <label style="vertical-align: text-bottom; padding-left: 6px">
                                            <label class="lbsetCk" style="vertical-align: middle;">
                                                <input id="chkMemIsPast" type="checkbox" runat="server" />
                                                永久有效（不设置过期时间）
                                            </label>
                                        </label>
                                        <label id="lblIsPast" style="display: none; vertical-align: middle">
                                            <input id="chkIsIsPast" runat="server" type="checkbox" /></label>
                                    </td>
                                </tr>
                                <tr id="Tr1" runat="server">
                                    <td class="tableStyle_left" id="tdStaff">
                                        员工提成：
                                    </td>
                                    <td class="tableStyle_right" id="tddStaff">
                                        <select id="sltStaff" runat="server" class="selectWidth" style="width: 120px">
                                        </select>
                                        <input id="txtRegisterStaffMoney" type="text" class="border_radius" runat="server"
                                            title="请输入提成金额" style="clear: both; float: none; width: 140px" />
                                        <input id="chkRegisterStaff" runat="server" type="checkbox" style="display: none" />
                                    </td>
                                    <td class="tableStyle_left" id="tdCardNumber">
                                        卡面号码：
                                    </td>
                                    <td class="tableStyle_right" id="tddCardNumber">
                                        <input id="txtCardNumber" runat="server" type="text" class="border_radius" title="请输入会员卡面号码"
                                            maxlength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        联系地址：
                                    </td>
                                    <td colspan="3" class="tableStyle_right">
                                        <uc1:SysArea ID="ucSysArea" runat="server" />
                                        <input id="txtMemAddress" type="text" class="border_radius" style="width: 520px;"
                                            runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        备 注：
                                    </td>
                                    <td colspan="3" class="tableStyle_right">
                                        <textarea id="txtMemRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                            outline: none; resize: none;" title="请输入会员的备注"></textarea>
                                    </td>
                                </tr>
                            </table>
                            <div class="user_regist_infor">
                                会员自定义属性
                            </div>
                            <table width="720" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tbody id="tbCustomField" runat="server">
                                    <tr>
                                        <td colspan="4" style="padding: 5px;">
                                            正在加载自定义属性，请稍候……
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div style="text-align: center; height: 36px">
                                <% if (curParameter.bolMoneySms && curParameter.bolAutoSendSMSByMemRegister)
                                   { %>
                                <label id="lblIsSMS" style="vertical-align: text-bottom;">
                                    <label class="lbsetCk" style="vertical-align: middle;">
                                        <input id="chkSMS" type="checkbox" runat="server" />
                                        发送短信 &nbsp;&nbsp;</label></label>
                                <%} %>
                                <% if (curParameter.bolMMS && curParameter.bolAutoSendMMSByMemRegister)
                                   { %>
                                <label id="lblIsMMS" style="vertical-align: text-bottom;">
                                    <label class="lbsetCk" style="vertical-align: middle;">
                                        <input id="chkMMS" runat="server" type="checkbox" />
                                        发送二维码 &nbsp;&nbsp;
                                    </label>
                                </label>
                                <%} %>
                                <input id="chkIsSMS" runat="server" type="checkbox" style="display: none" />
                                <input id="btnMemSave" type="button" class="buttonColor" value="保　存" />
                                <input id="btnMemReset" type="button" class="buttonRest" value="重　置" />
                            </div>
                        </div>
                    </td>
                    <td style="text-align: left;">
                        <div style="height: 600px; width: 224px; margin-left: 10px;">
                            <div class="user_regist_pic">
                                <img alt="" id="imgMemPhoto" src="../images/member/nophoto.gif" width="220" runat="server" /><br />
                                <input type="hidden" id="txtMemPhoto" runat="server" />
                            </div>
                            <p style="color: #DB9944; font-size: 12px;">
                                相片支持大小为200*200</p>
                            <div class="common_Button_all">
                                <input id="MemPhoto_Uploadify" type="file" class="common_Button" />
                                <input id="btnMemPhotoUpload" style="margin-left: 2px;" type="button" class="common_Button"
                                    value="上传照片" onclick="javascript:$('#MemPhoto_Uploadify').uploadifyUpload();" />
                                <input id="Button1" type="button" style="margin-left: 2px;" class="common_Button"
                                    value="在线拍摄" onclick="javascript:ShowOnlinePhoto();" />
                            </div>
                            <div id="Gift_fileQueue">
                            </div>
                            <div class="user_Notice" style="width: 210px; height: auto;">
                                注：<b>*</b> 号为系统必填项<br />
                                <span runat="server" id="isSendRWM">若要发送二维码，请启动服务参数中的"开启系统彩信功能"。</span>
                            </div>
                            <div id="trTitle" runat="server">
                                <p>
                                    二维码卡号</p>
                            </div>
                            <div id="trQrCode" runat="server">
                                <p>
                                    <img alt="" src="" id="imgQRCode" style="width: 140px; height: 140px" runat="server" />
                                    <input id="hidImgSrc" type="hidden" class="border_radius" runat="server" />
                                </p>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
