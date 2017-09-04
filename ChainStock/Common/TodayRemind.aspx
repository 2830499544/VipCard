<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TodayRemind.aspx.cs" Inherits="ChainStock.Common.TodayRemind" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>今日提醒</title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
  
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/TodayRemind.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Tab.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.SuperSlide.2.1.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmRemind" runat="server">
    <input type="hidden" id="txthidden" runat="server" />
    <input type="hidden" id="txtGoods" runat="server" />
    <div class="system_Info box_right" style="width: 99%;">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_List_All">
            <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
                <tr style="vertical-align: top">
                    <td colspan="2" style="width: 100%; vertical-align: top">
                        <!--系统提醒-->
                        <div class="system_Info notice">
                            <div class="system_Top tab-hd">
                                <ul class="tab-nav">
                                    <li>会员生日提醒</li><b>|</b>
                                    <li>会员期限提醒</li><b>|</b>
                                    <li>积分清零提醒</li><b>|</b>
                                    <li>库存不足提醒</li><b>|</b>
                                    <li>自定义提醒</li>
                                </ul>
                            </div>
                            <div class="tab-bd">
                                <div class="tab-pal">
                                    <table border="0" cellpadding="0" cellspacing="1" class="table-style table-hover"
                                        width="100%" id="MemBirthdayListTable">
                                        <tr class="th">
                                            <td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;"
                                                type="LoadingBar">
                                                <script type="text/javascript">
                                                    ListLoading();
                                                </script>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="height: 30px; line-height: 30px;">
                                        <div class="listTableBtn" style="width: 100%; text-align: left">
                                            <a href="javascript:void(0);" onclick="javascript:Remind_ShowLoading();MemBirthdayRemind(0);">
                                                今天过生日的会员</a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="javascript:void(0);" onclick="javascript:Remind_ShowLoading();MemBirthdayRemind(3);">
                                                    未来三天过生日的会员</a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="javascript:void(0);" onclick="javascript:Remind_ShowLoading();MemBirthdayRemind(7);">
                                                        未来七天过生日的会员</a>&nbsp;&nbsp;&nbsp;&nbsp; <a runat="server" href="javascript:void(0);"
                                                            id="MemBirthdaySendSMS" onclick="MemListSendSMS(this)">批量发送信息</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pal">
                                    <table border="0" cellpadding="0" cellspacing="1" class="table-style table-hover"
                                        width="100%" id="MemPassTime">
                                        <tr>
                                            <td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;"
                                                type="LoadingBar">
                                                <script type="text/javascript">
                                                    ListLoading();
                                                </script>
                                            </td>
                                        </tr>
                                    </table>
                                    <div style="height: 30px; line-height: 30px;">
                                        <div class="listTableBtn" style="width: 100%; text-align: left">
                                            <a href="javascript:void(0);" onclick="javascript:MemPassTime_ShowLoading();MemPastTime(0);">
                                                今日过期</a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="javascript:void(0);" onclick="javascript:MemPassTime_ShowLoading();MemPastTime(7);">
                                                    未来七天过期</a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="javascript:void(0);" onclick="javascript:MemPassTime_ShowLoading();MemPastTime(15);">
                                                        未来十五天过期</a>&nbsp;&nbsp;&nbsp;&nbsp; <a runat="server" href="javascript:void(0);"
                                                            id="MemPastTimeSendSMS" onclick="MemPassTimeListSendSMS(this)">批量发送信息</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pal">
                                    <table border="0" cellpadding="0" cellspacing="1" class="table-style table-hover"
                                        width="100%" id="MemPontResetList">
                                        <tr>
                                            <td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;"
                                                type="LoadingBar">
                                                <script type="text/javascript">
                                                    ListLoading();
                                                </script>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="tab-pal">
                                    <table border="0" cellpadding="0" cellspacing="1" class="table-style table-hover"
                                        width="100%" id="GoodsNumberList">
                                        <tr>
                                            <td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;"
                                                type="LoadingBar">
                                                <script type="text/javascript">
                                                    ListLoading();
                                                </script>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="tab-pal">
                                    <table border="0" cellpadding="0" cellspacing="1" class="table-style table-hover"
                                        width="100%" id="CustomRemindList">
                                        <tr>
                                            <td style="height: 20px; line-height: 50px; padding-left: 20px; background-color: #fff;"
                                                type="LoadingBar">
                                                <script type="text/javascript">
                                                    ListLoading();
                                                </script>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <script type="text/javascript">
                jQuery(".system_Info").slide({ titCell: ".tab-hd li", mainCell: ".tab-bd", delayTime: 0 });
            </script>
        </div>
    </div>
    </form>
</body>
</html>
