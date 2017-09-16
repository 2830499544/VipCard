<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="memberCard.aspx.cs" Inherits="ChainStock.mobile.business.memberCard" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="renderer" content="webkit|ie-comp|ie-stand" />
    <meta name="viewport" content="width=device-width,height=device-height,user-scalable=no" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <title>会员办卡</title>
    <link rel="stylesheet" type="text/css" href="css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="css/common.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link rel="stylesheet" type="text/css" href="css/media.css" />
    <link rel="stylesheet" type="text/css" href="css/color.css" />
</head>
<body>
    <form id="frmMemRegister" runat="server" name="user_form" method="post">
        <div class="section index" id="container">
            <div id="head" class="section">
                <div class="section header">
                    <h1>会员办卡</h1>
                    <a href="javascript:void(0);" class="back-btn">
                        <img src="images/prev.png" /></a>
                </div>
            </div>
            <div id="content" class="section">
                <div class="section line_box">
                    <div class="line_group">
                        <p class="f-left">会员卡号</p>
                        <div class="f-right">
                            <input type="text" placeholder="请输入会员卡号" id="txtMemCard" />
                        </div>
                    </div>
                    <div class="line_group">
                        <p class="f-left">卡面号码</p>
                        <div class="f-right">
                            <input type="text" placeholder="请输入卡面号码" id="txtCardNumber" />
                        </div>
                    </div>
                    <div class="line_group">
                        <p class="f-left">会员姓名</p>
                        <div class="f-right">
                            <input type="text" placeholder="请输入会员姓名" id="txtMemName" />
                        </div>
                    </div>
                    <div class="line_group">
                        <p class="f-left">会员性别</p>
                        <div class="f-right">
                            <a href="javascript:void(0);" class="line_btn active" onclick ="SexChose(this)" title="0">男</a>
                            <a href="javascript:void(0);" class="line_btn" onclick="SexChose(this)"  title="1" >女</a>
                        </div>
                    </div>
                    <div class="line_group">
                        <p class="f-left">会员生日</p>
                        <div class="f-right">
                            <input type="text" placeholder="请输入生日" id="txtMemBirthday" />
                        </div>
                    </div>
                    <div class="line_group">
                        <p class="f-left">手机号码</p>
                        <div class="f-right">
                            <input type="text" placeholder="请输入手机号码" id="txtMemMobile" maxlength="11" />
                        </div>
                    </div>

                    <div class="line_group">
                        <p class="f-left">会员等级</p>
                        <div class="f-right">
                            <!-- 会员选择的等级显示在这里 -->
                            <p class="show-address dis-n"></p>

                            <a href="javascript:void(0);" class="line_choice queryCity" id="">
                                <p>请选择</p>
                                <span>
                                    <img src="images/right_d.png" /></span>
                            </a>
                            <!-- 等级选择 -->
                            <div class="dis-n city-mode">
                                <div class="city-query">
                                    <select id="sltMemLevelID" runat="server" style="width: 100%; height: 0.6rem; line-height: 0.6rem;">
                                    </select>
                                    <div class="section ad-detail">
                                        <a href="##" class="ad-sure">确定</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="line_group">
                        <p class="f-left">推荐人卡号</p>
                        <div class="f-right">
                            <input type="text" placeholder="请输入推荐人卡号" id="txtMemRecommendCard" />
                        </div>
                    </div>
                    <div class="line_group" id="divRecommend" style="display:none;">
                       <p class="f-left">推荐人</p>
                        <div class="f-right">
                            <p id="txtMemRecommendMsg" ></p>
                       </div>
                        <input type="hidden" id="txtMemRecommendID" name="txtMemRecommendID" runat="server"
                            class="border_radius" />
                        <input type="hidden" id="txtMemRecommendName" name="txtMemRecommendName" runat="server"
                            class="border_radius" />                          
                    </div>
                    <div class="line_group">
                        <p class="f-left">过期时间</p>
                        <div class="f-right">
                            <%--  <p>永久有效</p>--%>
                            <input type="text" placeholder="请输入过期时间" id="txtMemPastTime" />
                        </div>
                    </div>
                    <div class="line_group">
                        <p class="f-left">办卡备注</p>
                        <div class="f-right">
                            <input type="text" id="txtMemRemark" placeholder="请输入备注" />
                        </div>
                    </div>
                    <div class="section form_btn" style="padding: 0 0.1rem; margin-top: 0.2rem;">
                        <a href="javascript:void(0)" id="btnMemSave">确定办卡</a>
                    </div>
                </div>
                <div>
                    <input type="hidden" id="txtMemIdentityCard" value="" />
                    <input type="hidden" id="sltMemSex" value="0" />
                    <input type="hidden" id="txtMemPassword" value="" />
                    <input type="hidden" id="txtMemPoint" value="0" />
                    <input type="hidden" id="txtMemMoney" value="0" />
                    <input type="hidden" id="txtMemEmail" value="" />
                    <input type="hidden" id="txtMemAddress" value="" />
                    <input type="hidden" id="sltMemState" value="0" />
                    <input type="hidden" id="sltShop" value="" runat="server" />
                    <input type="hidden" id="sltMemUserID" value="" runat="server" />
                    <input type="hidden" id="txtMemCreateTime" value="" runat="server" />
                    <input type="hidden" id="txtTelephone" value="" />
                    <input type="hidden" id="txtMemPhoto" value="" />
                    <input type="hidden" id="chkSMS" value="" />
                    <input type="hidden" id="ucSysArea$sltProvince" value="" />
                    <input type="hidden" id="ucSysArea$sltCity" value="" />
                    <input type="hidden" id="ucSysArea$sltCounty" value="" />
                    <input type="hidden" id="ucSysArea$sltVillage" value="" />
                    <input type="hidden" id="txtRegisterStaffMoney" value="" />
                    <input type="hidden" id="sltStaff" value="" />
                    <input type="hidden" id="txtMemID" value="" />
                </div>
            </div>
            <!-- 底部浮动导航 -->
            <div class="foot-nav">
                <div class="f-left fix-nav fix-home">
                    <a href="index.aspx">
                        <img src="images/home.png" /></a>
                </div>
          <%--      <div class="f-left fix-nav fix-ch">
                    <a href="memberCard.aspx">
                        <p>会员办卡</p>
                    </a>
                </div>--%>
               <%-- <div class="f-left fix-nav fix-ch">
                    <a href="##">
                        <p>收银记账</p>
                        <img src="images/icon.png" /></a>
                    <div class="foot-more">
                        <a href="fastConsumption.aspx">快速消费</a>
                        <a href="goodsConsumption.aspx">商品消费</a>
                        <a href="timesConsumption.aspx">计次消费</a>
                    </div>
                </div>--%>
                <div class="f-left fix-nav fix-ch">
                    <a href="bossCenter.aspx">
                        <p>老板中心</p>
                    </a>
                </div>
            </div>

        </div>

        <script type="text/javascript" src="scripts/jquery-2.1.4.min.js"type="text/javascript"></script>
        <link href="mobiscroll-v2.17/css/mobiscroll.custom-2.17.1.min.css" type="text/css" rel="stylesheet" />
        <script src="mobiscroll-v2.17/js/mobiscroll.custom-2.17.1.min.js" type="text/javascript"></script>
        <script type="text/javascript" src="scripts/script.js"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                var currYear = (new Date()).getFullYear();
                var currMonth = (new Date()).getMonth();
                var currDay = (new Date()).getDate();
                var curHour = (new Date()).getHours();

                var opt1 = {
                    preset: 'date', //日期
                    theme: 'sense-ui', //皮肤样式
                    display: 'modal', //显示方式 
                    mode: 'scroller', //日期选择模式
                    dateFormat: 'yy-mm-dd', // 日期格式
                    setText: '确定', //确认按钮名称
                    cancelText: '取消', //取消按钮名籍我
                    dateOrder: 'yymmdd', //面板中日期排列格式
                    dayText: '日', monthText: '月', yearText: '年', //面板中年月日文字
                    endYear: currYear + 10, //结束年份,
                    startYear: currYear-60, //开始年份
                    startMonth: currMonth, //开始年份\
                    startDay: currDay//开始年份
                };
                $("#txtMemBirthday").mobiscroll(opt1).date(opt1);
                var opt2 = {
                    preset: 'date', //日期
                    theme: 'sense-ui', //皮肤样式
                    display: 'modal', //显示方式 
                    mode: 'scroller', //日期选择模式
                    dateFormat: 'yy-mm-dd', // 日期格式
                    setText: '确定', //确认按钮名称
                    cancelText: '取消', //取消按钮名籍我
                    dateOrder: 'yymmdd', //面板中日期排列格式
                    dayText: '日', monthText: '月', yearText: '年', //面板中年月日文字
                    endYear: currYear + 10, //结束年份,
                    startYear: currYear-60, //开始年份
                    startMonth: currMonth, //开始年份\
                    startDay: currDay//开始年份
                };
                $("#txtMemPastTime").mobiscroll(opt1).date(opt1);
            });
        </script>
    </form>
</body>
</html>
