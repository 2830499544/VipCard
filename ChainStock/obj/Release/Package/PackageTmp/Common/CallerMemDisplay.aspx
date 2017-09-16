<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CallerMemDisplay.aspx.cs"
    Inherits="ChainStock.Common.CallerMemDisplay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/aero.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/ShopInfo.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/CallerMem/CallerMem.js" type="text/javascript"></script>
    <script type="text/javascript">
        var checkTime = true;
        var time_start = new Date();
        var clock_start = time_start.getTime();
        function get_time_spent() {
            var time_now = new Date();
            return ((time_now.getTime() - clock_start) / 1000);
        }
        function show_secs() {
            var i_total_secs = Math.round(get_time_spent());
            var i_secs_spent = i_total_secs % 60;
            var i_mins_spent = Math.round((i_total_secs - 30) / 60);
            var s_secs_spent = "" + ((i_secs_spent > 9) ? i_secs_spent : "0" + i_secs_spent);
            var s_mins_spent = "" + ((i_mins_spent > 9) ? i_mins_spent : "0" + i_mins_spent);
            // document.fm0.time_spent.value =
            $("#lblTime").html(s_mins_spent + ":" + s_secs_spent);
            if (checkTime) {
                window.setTimeout('show_secs()', 1000);
            }
        }
        function AppendStatus(szStatus) {
            qnviccub.QNV_Tool(QNV_TOOL_WRITELOG, 0, szStatus, NULL, NULL, 0); //写本地日志到控件注册目录的userlog目录下
            var t = $("#StatusArea").val();
            t += szStatus + "\r\n";
            $("#lblTelState").html(t);
        }
        function AppendStatusEx(uID, szStatus) {
            uID = uID + 1;
            AppendStatus("通道" + uID + ":" + szStatus);
        }
        function T_GetEvent(uID, uEventType, uHandle, uResult, szdata) {
          //var vValue = " type=" + uEventType + " Handle=" + uHandle + " Result=" + uResult + " szdata=" + szdata;
            var vValue = " type=" + uEventType + " Handle=" + uHandle + " Result=" + uResult + " szdata=" + szdata;
            switch (uEventType) {
                case BriEvent_PhoneHook: // 本地电话机摘机事件
                    AppendStatusEx(uID, "本地电话机摘机" + vValue); 3
                    $("#lblTelState").html("电话接通中.....");
                    if (checkTime) {
                        window.setTimeout('show_secs()', 1);
                    }
                    break;
                //                case BriEvent_PhoneDial: // 只有在本地话机摘机，没有调用软摘机时，检测到DTMF拨号            
                //                    AppendStatusEx(uID, "本地话机拨号" + vValue);            
                //                    break;            
                case BriEvent_PhoneHang: // 本地电话机挂机事件
                    // AppendStatusEx(uID, "本地电话机挂机" + vValue);
                    $("#lblTelState").html("本地电话机挂机.....");
                    checkTime = false;
                    break;
                case BriEvent_CallIn: // 外线通道来电响铃事件
                    AppendStatusEx(uID, "外线通道来电响铃事件" + vValue);
                    $("#lblTelState").html("来电振铃中.....");
                    break;
                //                case BriEvent_GetCallID: //得到来电号码            
                //                    AppendStatusEx(uID, "得到来电号码" + vValue);            
                //                    break;            
                case BriEvent_StopCallIn: // 对方停止呼叫(产生一个未接电话)
                    // AppendStatusEx(uID, "对方停止呼叫(产生一个未接电话)" + vValue);
                    $("#lblTelState").html("未接来电.....");
                    break;
                //                case BriEvent_DialEnd: // 调用开始拨号后，全部号码拨号结束            
                //                    AppendStatusEx(uID, "调用开始拨号后，全部号码拨号结束" + vValue);            
                //                    break;            
                //                case BriEvent_PlayFileEnd: // 播放文件结束事件            
                //                    AppendStatusEx(uID, "播放文件结束事件" + vValue);            
                //                    break;            
                //                case BriEvent_PlayMultiFileEnd: // 多文件连播结束事件            
                //                    AppendStatusEx(uID, "多文件连播结束事件" + vValue);            
                //                    break;            
                //                case BriEvent_PlayStringEnd: //播放字符结束            
                //                    AppendStatusEx(uID, "播放字符结束" + vValue);            
                //                    break            
                //                case BriEvent_RepeatPlayFile: // 播放文件结束准备重复播放            
                //                    AppendStatusEx(uID, "播放文件结束准备重复播放" + vValue);            
                //                    break;            
                //                case BriEvent_SendCallIDEnd: // 给本地设备发送震铃信号时发送号码结束            
                //                    AppendStatusEx(uID, "给本地设备发送震铃信号时发送号码结束" + vValue);            
                //                    break;            
                //                case BriEvent_RingTimeOut: //给本地设备发送震铃信号时超时            
                //                    AppendStatusEx(uID, "给本地设备发送震铃信号时超时" + vValue);            
                //                    break;            
                //                case BriEvent_Ringing: //正在内线震铃            
                //                    AppendStatusEx(uID, "正在内线震铃" + vValue);            
                //                    break;            
                //                case BriEvent_Silence: // 通话时检测到一定时间的静音.默认为5秒            
                //                    AppendStatusEx(uID, "通话时检测到一定时间的静音" + vValue);            
                //                    break;            
                //                case BriEvent_GetDTMFChar: // 线路接通时收到DTMF码事件            
                //                    AppendStatusEx(uID, "线路接通时收到DTMF码事件" + vValue);            
                //                    break;            
                //                case BriEvent_RemoteHook: // 拨号后,被叫方摘机事件            
                //                    AppendStatusEx(uID, "拨号后,被叫方摘机事件" + vValue);            
                //                    break;            
                case BriEvent_RemoteHang: //对方挂机事件
                    // AppendStatusEx(uID, "对方挂机事件" + vValue);
                    $("#lblTelState").html("对方挂机.....");
                    break;
                //                case BriEvent_Busy: // 检测到忙音事件,表示PSTN线路已经被断开            
                //                    AppendStatusEx(uID, "检测到忙音事件,表示PSTN线路已经被断开" + vValue);            
                //                    break;            
                //                case BriEvent_DialTone: // 本地摘机后检测到拨号音            
                //                    AppendStatusEx(uID, "本地摘机后检测到拨号音" + vValue);            
                //                    break;            
                //                case BriEvent_RingBack: // 电话机拨号结束呼出事件。            
                //                    AppendStatusEx(uID, "电话机拨号结束呼出事件" + vValue);            
                //                    break;            
                //                case BriEvent_MicIn: // MIC插入状态            
                //                    AppendStatusEx(uID, "MIC插入状态" + vValue);            
                //                    break;            
                //                case BriEvent_MicOut: // MIC拔出状态            
                //                    AppendStatusEx(uID, "MIC拔出状态" + vValue);            
                //                    break;            
                //                case BriEvent_FlashEnd: // 拍插簧(Flash)完成事件，拍插簧完成后可以检测拨号音后进行二次拨号            
                //                    AppendStatusEx(uID, "拍插簧(Flash)完成事件，拍插簧完成后可以检测拨号音后进行二次拨号" + vValue);            
                //                    break;            
                //                case BriEvent_RefuseEnd: // 拒接完成            
                //                    AppendStatusEx(uID, "拒接完成" + vValue);            
                //                    break;            
                //                case BriEvent_SpeechResult: // 语音识别完成             
                //                    AppendStatusEx(uID, "语音识别完成" + vValue);            
                //                    break;            
                //                case BriEvent_FaxRecvFinished: // 接收传真完成            
                //                    AppendStatusEx(uID, "接收传真完成" + vValue);            
                //                    break;            
                //                case BriEvent_FaxRecvFailed: // 接收传真失败            
                //                    AppendStatusEx(uID, "接收传真失败" + vValue);            
                //                    break;            
                //                case BriEvent_FaxSendFinished: // 发送传真完成            
                //                    AppendStatusEx(uID, "发送传真完成" + vValue);            
                //                    break;            
                //                case BriEvent_FaxSendFailed: // 发送传真失败            
                //                    AppendStatusEx(uID, "发送传真失败" + vValue);            
                //                    break;            
                //                case BriEvent_OpenSoundFailed: // 启动声卡失败            
                //                    AppendStatusEx(uID, "启动声卡失败" + vValue);            
                //                    break;            
                //                case BriEvent_UploadSuccess: //远程上传成功            
                //                    AppendStatusEx(uID, "远程上传成功" + vValue);            
                //                    break;            
                //                case BriEvent_UploadFailed: //远程上传失败            
                //                    AppendStatusEx(uID, "远程上传失败" + vValue);            
                //                    break;            
                //                case BriEvent_EnableHook: // 应用层调用软摘机/软挂机成功事件            
                //                    AppendStatusEx(uID, "应用层调用软摘机/软挂机成功事件" + vValue);            
                //                    break;            
                //                case BriEvent_EnablePlay: // 喇叭被打开或者/关闭            
                //                    AppendStatusEx(uID, "喇叭被打开或者/关闭" + vValue);            
                //                    break;            
                //                case BriEvent_EnableMic: // MIC被打开或者关闭            
                //                    AppendStatusEx(uID, "MIC被打开或者关闭" + vValue);            
                //                    break;            
                //                case BriEvent_EnableSpk: // 耳机被打开或者关闭            
                //                    AppendStatusEx(uID, "耳机被打开或者关闭" + vValue);            
                //                    break;            
                //                case BriEvent_EnableRing: // 电话机跟电话线(PSTN)断开/接通            
                //                    AppendStatusEx(uID, "电话机跟电话线(PSTN)断开/接通" + vValue);            
                //                    break;            
                //                case BriEvent_DoRecSource: // 修改录音源            
                //                    AppendStatusEx(uID, "修改录音源" + vValue);            
                //                    break;            
                //                case BriEvent_DoStartDial: // 开始软件拨号            
                //                    AppendStatusEx(uID, "开始软件拨号" + vValue);            
                //                    break;            
                //                case BriEvent_RecvedFSK: // 接收到FSK信号，包括通话中FSK/来电号码的FSK            
                //                    AppendStatusEx(uID, "接收到FSK信号，包括通话中FSK/来电号码的FSK" + vValue);            
                //                    break;            
                //                case BriEvent_DevErr: //设备错误            
                //                    AppendStatusEx(uID, "设备错误" + vValue);            
                //                    break;            
                //                default:            
                //                    if (uEventType < BriEvent_EndID)            
                //                        AppendStatusEx(uID, "忽略其它事件发生:ID=" + uEventType + vValue);            
                //                    break;            
            }
        }
    </script>
    <script type="text/javascript" for="qnviccub" event="OnQnvEvent(chID,type,handle,result,param,szdata,szdataex)">
	T_GetEvent(chID,type,handle,result,szdata)
    </script>
    <script src="../Scripts/CallerMem/qnvfunc.js" type="text/javascript"></script>
    <script src="../Scripts/CallerMem/qnviccub.js" type="text/javascript"></script>
    <asp:Literal ID="litObject" runat="server" />
    <object classid="clsid:F44CFA19-6B43-45EE-90A3-29AA08000510" id="qnviccub" data="DATA:application/x-oleobject;BASE64,GfpM9ENr7kWQoymqCAAFEAADAAD7FAAADhEAAA==
" width="0" height="0">
    </object>
    <style type="text/css">
        #TextArea1
        {
            height: 242px;
            width: 357px;
        }
        #StatusArea
        {
            width: 658px;
            height: 278px;
        }
    </style>
</head>
<body onunload="btnSave()">
    <form id="form1" runat="server">
    <div style="width: 100%; text-align: center;">
        <table style="margin: auto" id="findTable">
            <tr>
                <td align="center">
                    <table style="width: 600px" class="tableStyle">
                        <tr id="trTitle">
                            <th style="text-align: center; font-weight: bold;" class="th" colspan="2">
                                会员信息
                            </th>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <span id="spTel" runat="server" />
                                <asp:Label ID="lblTel" runat="server" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                                &nbsp;&nbsp; <font color="red">
                                    <label id="lblIsMem" runat="server" />
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                来电状态：<asp:Label ID="lblTelState" runat="server" Font-Size="Medium" ForeColor="Red"></asp:Label>
                            </td>
                            <td align="center">
                                通话时长：<asp:Label ID="lblTime" runat="server" Text="Label" ForeColor="Red">暂未接通</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divMemberInfo">
                        <table style="width: 600px" class="tableStyle">
                            <tr id="trCardAndName">
                                <td style="width: 120px; text-align: right">
                                    会员卡号：
                                </td>
                                <td>
                                    <input id="txtFMemID" type="hidden" runat="server" />
                                    <input id="txtFMemCard" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                                <td style="width: 120px; text-align: right">
                                    会员姓名：
                                </td>
                                <td>
                                    <input id="txtFMemName" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                            </tr>
                            <tr id="trMoneyAndPoint">
                                <td style="width: 120px; text-align: right">
                                    会员余额：
                                </td>
                                <td>
                                    <input id="txtFMemMoney" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                                <td style="width: 120px; text-align: right;">
                                    会员积分：
                                </td>
                                <td>
                                    <input id="txtFMemPoint" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                            </tr>
                            <tr id="trLevelAndShop">
                                <td style="width: 120px; text-align: right">
                                    会员等级：
                                </td>
                                <td>
                                    <input id="txtFMemLevelName" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                                <td style="width: 120px; text-align: right;">
                                    所属商家：
                                </td>
                                <td>
                                    <input id="txtFMemShopName" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                            </tr>
                            <tr id="trMobileAndState">
                                <td style="width: 120px; text-align: right">
                                    手机号码：
                                </td>
                                <td>
                                    <input id="txtFMemMobile" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                                <td style="width: 120px; text-align: right;">
                                    会员状态：
                                </td>
                                <td>
                                    <input id="txtFMemState" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                            </tr>
                            <tr id="trBirthdayAndSex">
                                <td style="width: 120px; text-align: right">
                                    会员生日：
                                </td>
                                <td>
                                    <input id="txtFMemBirthday" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                                <td style="width: 120px; text-align: right;">
                                    会员性别：
                                </td>
                                <td>
                                    <input id="txtFMemSex" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                            </tr>
                            <tr id="trIdentityAndPastTime">
                                <td style="width: 120px; text-align: right;">
                                    身份证号：
                                </td>
                                <td>
                                    <input id="txtFMemIdentityCard" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                                <td style="width: 120px; text-align: right;">
                                    过期时间：
                                </td>
                                <td>
                                    <input id="txtFMemPastTime" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                            </tr>
                            <tr id="trCreateTimeAndUser">
                                <td style="width: 120px; text-align: right;">
                                    办卡时期：
                                </td>
                                <td>
                                    <input id="txtFMemCreateTime" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                                <td style="width: 120px; text-align: right;">
                                    办卡人：
                                </td>
                                <td>
                                    <input id="txtFMemUserName" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                            </tr>
                            <tr id="trEmail">
                                <td style="width: 120px; text-align: right;">
                                    电子邮箱：
                                </td>
                                <td colspan="3" style="text-align: left;">
                                    &nbsp;<input id="txtFMemEmail" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" />
                                </td>
                            </tr>
                            <tr id="trAddress">
                                <td style="width: 120px; text-align: right;">
                                    联系地址：
                                </td>
                                <td colspan="3" style="text-align: left;">
                                    &nbsp;<input id="txtFMemAddress" type="text" runat="server" class="input_txt border_radius"
                                        readonly="readonly" disabled="disabled" style="width: 90%;" />
                                </td>
                            </tr>
                            <tr>
                                <th style="text-align: center; font-weight: bold;" class="th" colspan="4">
                                    来电备注
                                </th>
                            </tr>
                            <tr>
                                <td style="width: 120px; text-align: right; vertical-align: top;">
                                    来电备注：
                                </td>
                                <td class="tableAlignLeft" colspan="3">
                                    <textarea id="txTelRemark" rows="4" runat="server" style="width: 90%; margin-left: 5px;"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="tableBox">
                                    <input id="btnSave" type="button" class="buttonColor" value="保   存" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
