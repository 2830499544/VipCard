<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXinConfig.aspx.cs" Inherits="ChainStock.SystemManage.WeiXinConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/WeiXinConfig.js" type="text/javascript"></script>
    <script type="text/jscript">
        $(document).ready(function () {
            $("#weixinPhoto_Uploadify").uploadify({
                'uploader': "../images/swf/uploadify.swf",
                'script': "../Service/MicroWebsiteUpload.ashx",
                'cancelImg': "../images/member/cancel.png",
                'folder': "../Upload/WeiXin/Images",
                'queueID': 'weixin_fileQueue',
                'buttonImg': "../images/member/selectImg.jpg",
                'height': 25,
                'width': 70,
                'fileExt': "*.jpg",
                'fileDesc': "请选择格式为JPG的图片",
                'fileDataName': "WeiXinPhoto",
                'auto': true,
                'multi': false,
                'method': 'get',
                'sizeLimit': 512000,
                'onError': function (event, ID, fileObj, errorObj) {
                    if (errorObj.type == "File Size")
                        alert("对不起，上传的图片不能超过500K");
                    else
                        alert(errorObj.type + ' Error: ' + errorObj.info);
                },
                'onComplete': function (event, ID, fileObj, response, data) {
                    if (response.length > 1) {
                        $("#txtUpdateGoodsName").html("上传完成！");
                        $("#weixinPhoto").attr("src", "../Upload/WeiXin/Images/" + response + "?" + GetGuid());
                    }
                },
                'onSelect': function (event, queueID, fileObj) {
                    $("#txtUpdateGoodsName").html("正在上传...");
                    $("#weixinPhoto_Uploadify").uploadifySettings('scriptData', { 'name': $("#txtWeiXinPhoteType").val() });
                }
            });
             $("#weixinCert_Uploadify").uploadify({
                'uploader': "../images/swf/uploadify.swf",
                'script': "../Service/MicroWebsiteUpload.ashx",
                'cancelImg': "../images/member/cancel.png",
                'folder': "../cert",
                'queueID': 'weixin_fileQueue',
                'buttonImg': "../images/member/selectCert.png",
                'height': 25,
                'width': 75,
                'fileExt': "*.p12",
                'fileDesc': "请选择格式为p23的证书文件",
                'fileDataName': "Cert",
                'auto': true,
                'multi': false,
                'method': 'get',
                'sizeLimit': 512000,
                'onError': function (event, ID, fileObj, errorObj) {
                    if (errorObj.type == "File Size")
                        alert("对不起，上传的证书不能超过500K");
                    else
                        alert(errorObj.type + ' Error: ' + errorObj.info);
                },
                'onComplete': function (event, ID, fileObj, response, data) {
                    if (response.length > 1) {
                        $("#txtCert").html("上传完成！");
                       // $("#weixinPhoto").attr("src", "../Upload/WeiXin/Images/" + response + "?" + GetGuid());
                    }
                },
                'onSelect': function (event, queueID, fileObj) {
                    $("#txtCert").html("正在上传...");
                    $("#weixinCert_Uploadify").uploadifySettings('scriptData', { 'name': $("#txtWeiXinPhoteType").val() });
                }
            });
        })
     
    </script>
    <style type="text/css">
        a:link
        {
            color: #0078b6;
        }
        a:visited
        {
            color: #0078b6;
        }
        a:active
        {
            color: #551A8B;
        }
        a:hover
        {
            color: #551A8B;
        }
        
        .spanCopy
        {
            border: 1px solid #ddd;
            padding: 0 8px;
        }
        
        .step1
        {
            display: block;
        }
        .step2
        {
            display: none;
        }
        .step3
        {
            display: none;
        }
        
        .nextHolder
        {
            border: 1px solid #e4e7ea;
            display: block;
            width: 90%;
            height: 36px;
            line-height: 36px;
            text-align: center;
        }
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
            <div class="step1" runat="server" id="step1">
                <div class="user_regist_infor" style="width: 100%">
                    第一步---设置微信参数
                </div>
                <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 90%;">
                    <tr>
                        <td class="tableStyle_left" style="text-align: right; width: 20%;">
                            公众号类型：
                        </td>
                        <td class="tableStyle_right">
                            <select runat="server" id="txtWeixXinType" class="selectWidth" style="width: 100px;
                                float: left;">
                                <option value="0">订阅号</option>
                                <option value="1">服务号</option>
                            </select>
                            <span style="line-height: 25px; margin-left: 15px;">选择您的微信公众账号类型。</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            是否已认证：
                        </td>
                        <td class="tableStyle_right">
                            <select runat="server" id="txtWeiXinVerified" class="selectWidth" style="width: 100px;
                                float: left;">
                                <option value="0">未认证</option>
                                <option value="1">已认证</option>
                            </select>
                            <span style="line-height: 25px; margin-left: 15px;">您的微信公众帐号是否已认证。</span>
                        </td>
                    </tr>
                    <tr id="trPay">
                        <td class="tableStyle_left" style="text-align: right;">
                            是否启用会员充值：
                        </td>
                        <td class="tableStyle_right">
                            <select runat="server" id="selPay" class="selectWidth" style="width: 100px; float: left;">
                                <option value="0">不启用</option>
                                <option value="1">启用</option>
                            </select>
                            <span style="line-height: 25px; margin-left: 15px;">您的微信公众帐号是否开通了微信支付功能。</span>
                        </td>
                    </tr>
                     
                    <tr id="trMchId">
                        <td class="tableStyle_left" style="text-align: right;">
                            微信商户号：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" class="border_radius" style="width: 380px;" maxlength="43" runat="server"
                                id="txtMchId" />   <span style="line-height:25px; margin-left:15px;">用于微信支付,请在微信商户平台获取！</span>
                        </td>
                    </tr>
                    <tr id="trAPI">
                        <td class="tableStyle_left" style="text-align: right;">
                             微信支付商户号密钥：
                        </td>
                        <td class="tableStyle_right">
                         <%--   <input type="password" class="border_radius" style="width: 380px;" maxlength="43"
                                runat="server" id="txtAPI" />--%>
                        <asp:TextBox ID="txtAPI"  class="border_radius" style="width: 380px;" 
                                maxlength="43" runat="server" TextMode="Password"></asp:TextBox>     
                                 <span style="line-height:25px; margin-left:15px;">用于微信支付,请在微信商户平台获取！</span>
                        </td>
                    </tr>
                    
                     <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            微信商户证书：    
                        </td>
                        <td class="tableStyle_right">
                          
                          <table>
                          <tr>
                          <td> <input id="weixinCert_Uploadify" type="file" style="display: none;" class="buttonColor" /></td>
                          <td>     <div id="txtCert" style="width: 80px;height: 25px;float: left;margin-left: 5px;text-align:left; color:Red;"></div></td>
                          <td>    <span style="line-height:25px; margin-left:15px; ">请上传apiclient_cert.p12证书文件！请到"微信商户平台-->账户设置-->API安全-->证书下载"下载证书文件</span><a href="https://pay.weixin.qq.com/" target="_blank">登录商户平台</a></span></td>
                          </tr>
                          </table>
                             
                       
                          
                        </td>
                    </tr>
                    <tr id="trtxtXiane">
                        <td class="tableStyle_left" style="text-align: right;">
                            单笔充值最大限额：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" class="border_radius" style="width: 100px;" maxlength="43" runat="server"
                                id="txtXiane" />
                            <span style="line-height: 25px; margin-left: 15px;">单位:元(必须是大于0的正整数) </span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            <span style="color: #ff4800; vertical-align: middle">*</span>Token：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" class="border_radius" style="width: 190px;" maxlength="32" runat="server"
                                id="txtWeiXinToken" />
                            <label style="line-height: 24px;">
                                <input type="button" id="btnCreateRandomToken" class="common_Button" style="margin-left: 10px;
                                    margin-top: 3px;" value="随机生成" runat="server" />
                            </label>
                            <span style="line-height: 25px; margin-left: 15px;">用于验证微信与系统的通讯安全，必须为英文或数字，长度为3-32字符。</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            <span style="color: #ff4800; vertical-align: middle">*</span>EncodingAESKey：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" class="border_radius" style="width: 380px;" maxlength="43" runat="server"
                                id="txtEncodingAESKey" />
                            <label style="line-height: 24px;">
                                <input type="button" id="btnCreateRandomAESKey" class="common_Button" style="margin-left: 10px;
                                    margin-top: 3px;" value="随机生成" runat="server" />
                            </label>
                            <span style="line-height: 25px; margin-left: 15px;">微信加密数据传输的密匙。</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            <span style="color: #ff4800; vertical-align: middle">*</span>微信名称：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" class="border_radius" style="width: 300px;" title="对微信会员卡所属对象的描述"
                                id="txtWeiXinShopName" runat="server" maxlength="100" />
                            <span style="line-height: 25px; margin-left: 15px;">微信会员卡的名称，将会显示在微信会员申请的会员卡上。</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            <span style="color: #ff4800; vertical-align: middle">*</span>欢迎词：
                        </td>
                        <td class="tableStyle_right">
                            <textarea id="txtWeiXinSalutatory" rows="3" runat="server" style="width: 550px; word-wrap: break-word;
                                outline: none; resize: none; float: left;" title="公众用户点击关注时要发送的欢迎词或商家的相关简介"></textarea>
                            <br />
                            <span style="line-height: 25px; margin-left: 15px;">微信用户首次关注微信将要接收到的文字信息。</span>
                        </td>
                    </tr>
                </table>
                <div class="nextHolder">
                    <input type="button" id="Step1Next" class="common_Button" style="float: none; margin-top: 8px;"
                        value="下一步" runat="server" />
                </div>
                <div class="nextHolder">
                    <asp:Label ID="lblMessage" runat="server" Text="在线试用版本此页面参数不提供修改" Visible="False"
                        Font-Bold="True" Font-Size="Larger" ForeColor="#CC0000"></asp:Label>
                </div>
            </div>
            <div class="step2">
                <div class="user_regist_infor" style="width: 100%">
                    第二步---验证微信接口
                </div>
                <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 90%;">
                    <tr>
                        <td class="tableStyle_left" style="text-align: left; height: 50px; padding-left: 15px;"
                            colspan="2">
                            <span style="color: Red">请到"微信公众平台--开发者中心--服务器配置"进行如下设置，并在微信公众平台提交设置。</span><a href="https://mp.weixin.qq.com/"
                                target="_blank">立即登录微信公众平台</a>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right; width: 20%;">
                            URL：
                        </td>
                        <td class="tableStyle_right">
                            <span id="spanUrl" class="spanCopy" runat="server"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            Token：
                        </td>
                        <td class="tableStyle_right">
                            <span id="spanToken" class="spanCopy" runat="server"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            EncodingAESKey：
                        </td>
                        <td class="tableStyle_right">
                            <span id="spanEncodingAESKey" class="spanCopy" runat="server"></span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            消息加解密方式：
                        </td>
                        <td class="tableStyle_right">
                            <span id="spanEncodingType" class="spanCopy">安全模式</span>
                        </td>
                    </tr>
                    <tr id="WebchatAppID">
                        <td class="tableStyle_left" style="text-align: left; height: 50px; padding-left: 15px;"
                            colspan="2">
                            <span style="color: Red">在微信公众平台上获取AppId和AppSecret并填入下方空白处。</span>在微信公众平台查看AppSecret需要绑定微信并验证，请自行操作！
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            AppId：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" class="border_radius" style="width: 300px;" title="开发者凭据AppId"
                                maxlength="50" runat="server" id="txtAppId" />
                            <span style="line-height: 25px; margin-left: 15px;">用于核对微信帐号与微信通讯，请在微信公众平台获取！</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right;">
                            AppSecret：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" class="border_radius" style="width: 300px;" title="开发者凭据AppSecret"
                                maxlength="50" runat="server" id="txtAppSecret" />
                            <span style="line-height: 25px; margin-left: 15px;">用于核对微信帐号与微信通讯，请在微信公众平台获取！</span>
                        </td>
                    </tr>

                </table>
                <div class="nextHolder">
                    <input type="button" id="Step2Next" class="common_Button" style="float: none; margin-top: 8px;"
                        value="验证微信" />
                </div>
            </div>
            <div class="step3">
                <div class="user_regist_infor" style="width: 100%">
                    第三步---设置微信功能
                </div>
                <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 90%;">
                    <tr style=" display:none">
                        <td class="tableStyle_left" style="text-align: right; width: 20%;">
                            图片上传：
                        </td>
                        <td class="tableStyle_right">
                            <a href="javascript:showDialog(1)">背景图</a>
                            
                            <a href="javascript:showDialog(3)">会员卡</a>

                            <a href="javascript:showDialog(2)">微网站</a>


                             <a href="javascript:showDialog(4)">二维码</a>

                            <input type="hidden" id="txtWeiXinPhoteType" />
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right; padding-right: 2px;width: 20%;">
                            微信签到赠送积分：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" class="border_radius" style="width: 80px;" title="微信签到赠送积分数量"
                                maxlength="5" runat="server" id="txtSignInPoint" />
                            <span style="line-height: 25px; margin-left: 15px;">填入0不启用签到送积分，填入任意数字表示赠送积分的数量！</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left" style="text-align: right; padding-right: 2px;">
                            <label class="lbsetCk" style="vertical-align: middle;">
                                <label style="vertical-align: middle;">
                                    <input id="chkWeiXinSMSVcode" runat="server" type="checkbox" />
                                    短信验证会员手机号
                                </label>
                            </label>
                        </td>
                        <td class="tableStyle_right">
                            启用该选项后，只有通过了短信验证才可以成为微信会员
                        </td>
                    </tr>
                </table>
                <div id="tableWeiXin" style="border: 1px solid #CCC; padding: 2px; display: none;">
                    <div>
                        <img alt="" id="weixinPhoto" src="../images/member/nophoto.gif" width="320" height="160" /><br />
                        <span id="weixinphotoremark"  style="color: gray; text-align: center">相片支持大小为640*320</span>
                    </div>
                    <div style="margin-top: 10px; height: 30px;">
                        <div id="txtUpdateGoodsName" style="width: 80px; height: 25px; float: left; margin-left: 5px;
                            text-align: left">
                        </div>
                        <div style="width: 83px; height: 25px; float: right; margin-left: 20px;">
                            <input id="weixinPhoto_Uploadify" type="file" style="display: none;" class="buttonColor" />
                        </div>
                    </div>
                </div>
                <div class="nextHolder">
                    <input type="button" id="Step3Next" class="common_Button" style="float: none; margin-top: 8px;"
                        value="完成" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
