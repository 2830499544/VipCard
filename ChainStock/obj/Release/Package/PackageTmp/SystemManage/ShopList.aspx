<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopList.aspx.cs" Inherits="ChainStock.SystemManage.ShopList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
    <script src="../Scripts/Module/SystemManage/ShopInfo.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>
    <style type="text/css">
        .user_List_top label.tips {
            position: absolute;
            top: 70px;
            _top: 22px;
            right: 130px;
            *top: 22px;
            color: #767676;
        }
        .uploadifyProgress{
            margin-top:27px;
        }
    </style>
</head>
<body>
    <form id="frmShopList" runat="server">
        <input id="txtIsSendCard" type="hidden" runat="server" />
        <input id="txtSmsManage" type="hidden" runat="server" />
        <input id="txtPointManage" type="hidden" runat="server" />
        <input id="txtIsSettlement" type="hidden" runat="server" />
        <input type="hidden" id="hdShopID" runat="server" />
        <input type="hidden" id="union" runat="server" />
        <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
            <tr>
                <td colspan="2" style="width: 100%;">
                    <div class="system_Info">
                        <div class="system_Top">
                            <div class="user_regist_title">
                                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                            </div>
                        </div>
                        <div id="ShopSms" style="display: none;">
                            <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 300px; margin: auto">
                                <tr>
                                    <td class="tableStyle_left">商家名称：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label id="txtSmsShopName">
                                        </label>
                                        <input type="hidden" id="hdSmsShopID" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">商家剩余短信量：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label id="txtSmsNumber">
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">操作类型：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label>
                                            <input type="radio" name="radSmsType" value="0" checked="checked" />增加</label>&nbsp;&nbsp;
                                    <label>
                                        <input type="radio" name="radSmsType" value="1" />扣除</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">短信数量：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input type="text" class="input_txt border_radius" id="txtSmsCount" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <input id="btSaveSms" type="button" class="buttonColor" value="确   定" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divForeverTicketUrl" style="display: none;">
                            <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 500px; margin: auto">
                                <tr>
                                    <td id="tdForeverTicketUrl" class="tableStyle_right"></td>
                                </tr>
                            </table>
                        </div>
                        <div id="ShopPoint" style="display: none;">
                            <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 300px; margin: auto">
                                <tr>
                                    <td class="tableStyle_left">商家名称：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label id="txtShopPointName">
                                        </label>
                                        <input type="hidden" id="hdPointShopID" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">商家剩余积分量：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label id="txtShopPointNumber">
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">操作类型：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label>
                                            <input type="radio" name="radPointType" value="0" checked="checked" />增加</label>&nbsp;&nbsp;
                                    <label>
                                        <input type="radio" name="radPointType" value="1" />扣除</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">积分数量：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input type="text" class="input_txt border_radius" id="txtcount" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;">
                                        <input id="btSavePoint" type="button" class="buttonColor" value="确   定" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="ShopInfo" style="display: none">
                            <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 700px; margin: auto">
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>商家名称：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtShopName" type="text" runat="server" class="border_radius" />
                                        <input id="txtShopID" type="hidden" runat="server" />
                                    </td>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>企业代码：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtShopContactMan" readonly type="text" runat="server" class="border_radius" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">联系电话：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtShopTelephone" type="text" runat="server" class="border_radius" />
                                    </td>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>短信后缀：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtShopSmsName" type="text" runat="server" class="border_radius" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">打印标题：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtShopPrintTitle" type="text" runat="server" class="border_radius" />
                                    </td>
                                    <td class="tableStyle_left">打印脚注：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtShopPrintFoot" type="text" runat="server" class="border_radius" />
                                    </td>
                                </tr>
                                <tr runat="server" id="trSettlement">
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>商家结算周期：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtSettlementInterval" type="text" title="总店与分店多少天进行一次结算" runat="server"
                                            class="border_radius" />
                                    </td>
                                    <td class="tableStyle_right" colspan="2">系统自动生成结算记录的周期
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>销售提成比例：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtShopProportion" type="text" runat="server" title="请输入0~1之间的数值" class="border_radius" />
                                    </td>
                                    <td class="tableStyle_right" colspan="2">商家销售金额按比例提成给运营商
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>充值提成比例：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtRechargeProportion" type="text" runat="server" title="请输入0~1之间的数值" class="border_radius" />
                                    </td>
                                    <td class="tableStyle_right" colspan="2">商家充值,运营商按比例提成给商家
                                    </td>
                                </tr>
                                <tr runat="server" id="tr1">
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>返利总比例系数：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtTotalRate" type="text" runat="server" title="请输入0~1之间的数值" class="border_radius" />
                                    </td>
                                    <td class="tableStyle_right" colspan="2">会员消费时，商家的积分返利比例。返利总比例系统设置1，代表会员消费100元，商家总返利100积分。
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">商家储值功能：
                                    </td>
                                    <td colspan="3" class="tableStyle_right">
                                        <label style="vertical-align: text-bottom;">
                                            <input type="radio" name="radRechargeYesOrNo" id="radRechargeYes" runat="server" value="1" />
                                            <label style="vertical-align: middle;">
                                                开启</label></label>
                                        <label style="vertical-align: text-bottom;">
                                            <input type="radio" name="radRechargeYesOrNo" id="radRechargeNo" runat="server" value="0" />
                                            <label style="vertical-align: middle;">
                                                不开启<font color="red">&nbsp;&nbsp;&nbsp;(开启则该商家可以给会员充值金额)</font></label></label>
                                    </td>
                                </tr>
                                 <tr  runat="server" id="trShopRecharge">
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>最大充值金额：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input id="txtRechargeMaxMoney" type="text" runat="server"  class="border_radius" />
                                    </td>
                                    <td class="tableStyle_right" colspan="2">(单位:元)&nbsp;在商家结算周期内，该商家允许给会员充值的最大金额
                                    </td>
                                </tr>
                                   <tr style=" display:none;">
                                <td class="tableStyle_left">
                                   
                                </td>
                                <td colspan="3" class="tableStyle_right">
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radMainShopYesOrNo" id="radMainShopYes" runat="server" value="1" />
                                        <label style="vertical-align: middle;">
                                            直营店</label></label>
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radMainShopYesOrNo" id="radMainShopNo" runat="server" value="0" />
                                        <label style="vertical-align: middle;">
                                            商家<font color="red">&nbsp;&nbsp;&nbsp;(锁定则该联盟商的所有员工不能登录)</font></label></label>
                                </td>
                            </tr>
                                <tr id="trAlliance" runat="server" style="display: none;">
                                    <td class="tableStyle_left">商家类型：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label>
                                            <input type="radio" name="isLms" id="rdislms" runat="server" value="1" />联盟商</label><label>
                                                <input type="radio" name="isLms" id="rdisnotlms" runat="server" value="0" checked="true" />商家</label>
                                    </td>
                                    <td colspan="2" style="text-align: left;">
                                        <font style="color: Red;">（此设置保存之后将无法更改）</font>
                                    </td>
                                </tr>
                                <tr runat="server" id="trSltShop">
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>选择联盟商：
                                    </td>
                                    <td class="tableStyle_right">
                                        <select runat="server" id="sltShopList" class="selectWidth">
                                        </select>
                                    </td>
                                    <td colspan="2" style="text-align: left;">
                                        <font style="color: Red;">（此设置保存之后将无法更改）</font>
                                    </td>
                                </tr>
                                <tr runat="server" id="trShopSms">
                                    <td class="tableStyle_left">短信不足发送应按：
                                    </td>
                                    <td colspan="3" class="tableStyle_right">
                                        <label>
                                            <input type="radio" id="rbbzfs" name="SmsType" value="1" runat="server" enableviewstate="True" />不准发送</label>
                                        <label>
                                            <input type="radio" id="rbkytz" name="SmsType" value="2" runat="server" />短信透支（短信量变负数）</label>
                                    </td>
                                </tr>
                                <tr runat="server" id="trShopPoint">
                                    <td class="tableStyle_left">积分不足消费应按：
                                    </td>
                                    <td colspan="3" class="tableStyle_right">
                                        <label>
                                            <input type="radio" id="rdbzxf" value="1" name="PointType" runat="server" />不准消费</label>
                                        <label>
                                            <input type="radio" id="rdjfgl" value="2" name="PointType" runat="server" />会员消费时，积分给零</label>
                                        <label>
                                            <input type="radio" id="rdtz" value="3" name="PointType" runat="server" />积分透支（积分量变负数）</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">是否锁定：
                                    </td>
                                    <td colspan="3" class="tableStyle_right">
                                        <label style="vertical-align: text-bottom;">
                                            <input type="radio" name="radChooseYesOrNo" id="radChooseYes" runat="server" value="0" />
                                            <label style="vertical-align: middle;">
                                                暂时锁定</label></label>
                                        <label style="vertical-align: text-bottom;">
                                            <input type="radio" name="radChooseYesOrNo" id="radChooseNo" runat="server" value="1" />
                                            <label style="vertical-align: middle;">
                                                不锁定<font color="red">&nbsp;&nbsp;&nbsp;(锁定则该商家的所有员工不能登录)</font></label></label>
                                    </td>
                                </tr>
                                 
                                <tr>
                                    <td class="tableStyle_left">联系地址：
                                    </td>
                                    <td class="tableStyle_right" colspan="3">
                                        <table cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle" style="text-align: left">
                                            <tr>
                                                <td>
                                                    <select id="sltProvince" runat="server" class="selectWidth" style="width: 120px">
                                                    </select>省
                                                </td>
                                                <td>
                                                    <select id="sltCity" runat="server" class="selectWidth" style="width: 120px">
                                                        <option value="">=== 请选择 ===</option>
                                                    </select>市
                                                </td>
                                                <td>
                                                    <select id="sltCounty" runat="server" class="selectWidth" style="width: 120px">
                                                        <option value="">=== 请选择 ===</option>
                                                    </select>
                                                </td>
                                                

                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <input id="txtShopAddress" type="text" runat="server" class="border_radius" style="width: 520px;" /></td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>
                                <tr >
                                    <td class="tableStyle_left">
                                        <span style="color: #ff4800; vertical-align: middle">*</span>商家图片：
                                    </td>
                                    <td class="tableStyle_right">
                                        <img alt="" id="imgShopPhoto" src="../images/Gift/nogift.jpg" style="width: 25px; height: 25px;" runat="server" /><br />

                                        <input type="hidden" id="txtShopPhoto" runat="server" />

                                    </td>
                                    <td class="tableStyle_left">
                                        <table>
                                            <tr>
                                                <td>
                                                    <input id="ShopPhoto_Uploadify" type="file" style="display: none;" class="buttonColor" /></td>
                                                <td>
                                                    <input id="btnShopPhotoUpload" type="button" class="common_Button" value="上传图片" onclick="javascript: $('#ShopPhoto_Uploadify').uploadifyUpload();" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="tableStyle_right">
                                        <div>图片大小建议尺寸：150*150</div>
                                        <div id="ShopPhoto_fileQueue">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">备注：
                                    </td>
                                    <td class="tableStyle_right" colspan="3">
                                        <textarea id="txtShopRemark" rows="3" runat="server" style="width: 520px; word-wrap: break-word; outline: none; resize: none;"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="tableStyle_right ">
                                        <div class="buton" style="text-align: center;">
                                            <input id="btnShopSave" type="button" class="buttonColor" value="保   存 " />
                                            <input id="btnShopReset" type="button" class="buttonRest" value="重   置 " />
                                            <input id="ShopAddOrEdit" type="hidden" />
                                            <input id="chkSMS" type="checkbox" runat="server" style="display: none" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="ShopBuyCard" style="display: none">
                            <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 300px; margin: auto">
                                <tr>
                                    <td class="tableStyle_left">购卡商家：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label id="lblShopName">
                                        </label>
                                        <input type="hidden" id="hdCardShopID" value=""/>
                                        <input type="hidden" id="hdCardShopType" value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">卡片起始号：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input type="text" class="input_txt border_radius" id="txtStartCardNumber" title="请输入6位数字" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">卡片截止号：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input type="text" class="input_txt border_radius" id="txtEndCardNumber" title="请输入6位数字" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">购卡总金额：
                                    </td>
                                    <td class="tableStyle_right">
                                        <input type="text" class="input_txt border_radius" id="txtBuyCardMoney" />
                                    </td>
                                </tr>
                                   <tr id="trBuyType">
                                    <td class="tableStyle_left">操作类型：
                                    </td>
                                    <td class="tableStyle_right">
                                        <label>
                                            <input type="radio" name="radBuyType" value="0" checked="checked" />线下购卡</label>&nbsp;&nbsp;
                                    <label>
                                        <input type="radio" name="radBuyType" value="1" />线上购卡</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tableStyle_left">备注：
                                    </td>
                                    <td class="tableStyle_right">
                                        <textarea id="txtRemark" class="input_txt border_radius" rows="3" runat="server"
                                            style="height: 40px; word-wrap: break-word; outline: none; resize: none;"></textarea>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center;" class="tableStyle_right ">
                                        <input id="btSaveCardLog" type="button" class="buttonColor" value="确   定" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="user_List_All">
                            <div class="user_List_top" style="height: 34px;">
                                <table class="tableStyle" style="width: 100%">
                                    <tr style="color: #333333; background-color: #F7F6F3;">
                                        <td class="tableStyle_left">快捷操作：
                                        </td>
                                        <td class="tableStyle_right">
                                            <input type="text" id="txtShopType" style="width: 33px; display: none" runat="server" />
                                            <input id="btnShopAdd" type="button" value="新增商家" class="common_Button" runat="server" />&nbsp;
                                             <input id="btnMainShopAdd" type="button" value="新增直营店" class="common_Button" runat="server" style=" width:120px" />
                                            <input id="btnShopBuyCard" type="button" value="购卡记录" class="common_Button" runat="server" />
                                            <input id="btnSettlement" type="button" value="结算管理" class="common_Button" runat="server" style="display: none;" />
                                            <input id="btnPointRecord" type="button" value="积分记录" class="common_Button" runat="server" style="display: none;" />
                                            <input id="btnMsgRecord" type="button" value="短信记录" class="common_Button" runat="server" style="display: none;" />
                                        </td>
                                        <td class="tableStyle_left" style="width: 120px">
                                            <label id="lblsearch" for="txtSearch" class="tips">
                                                商家名称/联系人/联系电话/商家地址</label>
                                            <asp:TextBox ID="txtSearch" runat="server" class="border_radius" Width="220px"></asp:TextBox>
                                        </td>
                                        <td class="tableStyle_right" style="width: 80px">
                                            <asp:Button ID="btnSearch" type="button" Text="查询" class="common_Button" runat="server"
                                                OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                </table>
          
                            </div>
                                                       <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                            class="tableStyle">
                            <tr>
                              
                                <td class="tableStyle_left">
                                    状态：
                                </td>
                                <td class="tableStyle_right" style=" width:200px">
                                    <select id="sltShopState" runat="server" class="selectWidth">
                                     <option value="">全部</option>
                                    <option  value="0">未锁定</option>
                                       <option  value="1">锁定</option>
                                    </select>
                                </td>
                                <td class="tableStyle_right">
                                    <div class="user_List_Button">
                                        <asp:Button ID="btnShopSearch" runat="server" Text="查   询" class="common_Button"
                                            OnClick="btnShopSearch_Click" />
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                            <table class="table-style table-hover user_List_txt">
                                <asp:Repeater runat="server" ID="gvShopList" OnItemDataBound="gvShopList_ItemDataBound">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>序号
                                                </th>
                                                <th>缩略图
                                                </th>
                                                <th>商家名称
                                                </th>
                                                <th>所属联盟商
                                                </th>
                                                <th>联系人
                                                </th>
                                                <th>联系电话
                                                </th>
                                                <th>商家地址
                                                </th>
                                                <th class="settlement">
                                                    <!-- style="<%= isopen?"":"display:none" %>"-->
                                                    结算周期
                                                </th>
                                                <th class="settlement">
                                                    <!-- style="<%= isopen?"":"display:none" %>"-->
                                                    提成比例
                                                </th>
                                                <th>积分返利总比例
                                                </th>
                                                <th>是否锁定
                                                </th>
                                                   <th>是否储值
                                                </th>
                                                   <th>商家/直营店
                                                </th>
                                                <th class="tPoint">剩余积分
                                                </th>
                                                <th class="tSms">剩余短信
                                                </th>
                                                <th>操作
                                                </th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="td">
                                            <!-- showAlready标识是否已展开子商家   shopId存储商家id  union是否是联盟商 这样子搞重用性貌似没了-->
                                            <td class="tdZoom" showalready="0" shopid='<%# Eval("ShopID")%>' union='<%# Eval("IsAllianceProgram")%>'>
                                                <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                            </td>
                                            <td>
                                                <span onclick='ShowPic("<%#Eval("ShopImageUrl") %>")' style="cursor: pointer;">
                                                    <img id="imgPhoto" alt="" runat="server" src='<%#Eval("ShopImageUrl") %>'
                                                        style="width: 30px; height: 30px;" />
                                                </span>
                                            </td>
                                            <td style="text-align: left">
                                                <%# Eval("ShopName")%>
                                            </td>
                                            <td style="text-align: left">
                                                <%# BindShopName(Eval("FatherShopID"))%>
                                            </td>
                                            <td>
                                                <%# Eval("ShopContactMan")%>
                                            </td>
                                            <td>
                                                <%# Eval("ShopTelephone")%>
                                            </td>
                                            <td style="text-align: left; padding-left: 4px;">
                                                <%#BindAddress(Eval("ShopID"))%>
                                            </td>
                                            <td class="settlement" style="text-align: left; padding-left: 4px;">
                                                <%# Eval("SettlementInterval")%>
                                            </td>
                                            <td class="settlement" style="text-align: left; padding-left: 4px;">
                                                <%# Eval("ShopProportion","{0:F2}")%>
                                            </td>
                                            <td style="text-align: left; padding-left: 4px;">
                                                <%# Eval("TotalRate","{0:F2}")%>
                                            </td>
                                            <td>
                                                <%# Boolean.Parse(Eval("ShopState").ToString()) ? "是" : "否"%>
                                            </td>
                                             <td>
                                                <%# Boolean.Parse(Eval("IsRecharge").ToString()) ? "是" : "否"%>
                                            </td>
                                              <td>
                                                <%# Boolean.Parse(Eval("IsMain").ToString()) ? "直营店" : "商家"%>
                                            </td>
                                            <td style="text-align: left; padding-left: 4px;" class="tPoint">
                                                <%# Eval("PointCount")%>
                                            </td>
                                            <td style="text-align: left; padding-left: 4px;" class="tSms">
                                                <%# Eval("SmsCount")%>
                                            </td>
                                            <td class="listtd" style="width: 150px;">
                                                <asp:Label ID="lblShopID" runat="server" Text='<%# Bind("ShopID") %>' Visible="false"></asp:Label>
                                                <a href="#" onclick='<%# string.Format(" ShopEdit(\"{0}\",\"{1}\")",Eval("ShopName"),Eval("ShopID")) %>'
                                                    id="hyShopEdit" runat="server">
                                                    <img src="../images/Gift/eit.png" alt="编辑" title="编辑" />
                                                </a>

                                                <a href="#" onclick='<%# string.Format(" btnShopBuyCards(\"{0}\",\"{1}\",\"{2}\",\"{3}\")",Eval("ShopName"),Eval("ShopID"),Eval("IsAllianceProgram"),Eval("IsMain")) %>'
                                                    id="hySendCard" runat="server">&nbsp;&nbsp;<img src="../images/ico/privilege.png" alt="商家购卡" title="商家购卡" />商家购卡
                                                </a>

                                                &nbsp;

                                                <a href="#" onclick='<%# string.Format(" ShopEditPoint(\"{0}\",\"{1}\")",Eval("ShopName"),Eval("ShopID")) %>'
                                                    id="hyShopPointEdit" runat="server">&nbsp;&nbsp;<img src="../images/ico/point.png" alt="积分管理"
                                                        title="积分管理" />
                                                </a>&nbsp;
                                       <a href="#" onclick='<%# string.Format(" ShopEditSms(\"{0}\",\"{1}\")",Eval("ShopName"),Eval("ShopID")) %>'
                                           id="hyShopSmsEdit" runat="server">&nbsp;&nbsp;<img src="../images/ico/mobile.png" alt="短信管理"
                                               title="短信管理" />
                                       </a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater runat="server" ID="gvShopListProfession">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>序号
                                                </th>
                                                <th>商家名称
                                                </th>
                                                <th>联系人
                                                </th>
                                                <th>联系电话
                                                </th>
                                                <th>商家地址
                                                </th>
                                                <th>是否锁定
                                                </th>
                                                <th>备注
                                                </th>
                                                <th>操作
                                                </th>
                                            </tr>
                                        </thead>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="td">
                                            <td>
                                                <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                            </td>
                                            <td style="text-align: left">
                                                <%# Eval("ShopName")%>
                                            </td>
                                            <td>
                                                <%# Eval("ShopContactMan")%>
                                            </td>
                                            <td>
                                                <%# Eval("ShopTelephone")%>
                                            </td>
                                            <td style="text-align: left; padding-left: 4px;">
                                                <%# Eval("ShopAddress")%>
                                            </td>
                                            <td>
                                                <%# Boolean.Parse(Eval("ShopState").ToString()) ? "是" : "否"%>
                                            </td>
                                            <td style="text-align: left; padding-left: 4px;">
                                                <%# Eval("ShopRemark")%>
                                            </td>
                                            <td class="listtd" style="width: 100px;">
                                                <asp:Label ID="lblShopID" runat="server" Text='<%# Bind("ShopID") %>' Visible="false"></asp:Label>
                                                <a href="#" onclick='<%# string.Format(" ShopEdit(\"{0}\",\"{1}\")",Eval("ShopName"),Eval("ShopID")) %>'
                                                    id="hyShopEdit" runat="server">
                                                    <img src="../images/Gift/eit.png" alt="编辑" title="编辑" /></a>
                                                <%--  <a id="hyAuthority"
                                        runat="server" href='<%#string.Format("ShopAuthority.aspx?PID=38&SID={0}", Eval("ShopID"))%>'>
                                        <img src="../images/Gift/system.png" alt="权限设定" title="权限设定" />
                                    </a>
                                                --%>
                                                <a href="#" onclick='<%# string.Format(" ForeverTicketUrl(\"{0}\")",Eval("ShopID")) %>'>二维码</a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                            <div class="user_List_page">
                                <table style="width: 100%" id="tabPager">
                                    <tr>
                                        <td>
                                            <span id="spPageSize">&nbsp;每页记录数：</span>
                                            <asp:DropDownList ID="drpPageSize" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpPageSize_SelectedIndexChanged">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                            </asp:DropDownList>
                                            <webdiyer:AspNetPager ID="NetPagerParameter" runat="server" AlwaysShow="True" CustomInfoHTML="共%PageCount%页，当前为第%CurrentPageIndex%页，每页%PageSize%条"
                                                CssClass="paginator" CurrentPageButtonClass="cpb" FirstPageText="首页" LastPageText="尾页"
                                                NextPageText="下一页" OnPageChanging="NetPagerParameter_PageChanging" PrevPageText="上一页"
                                                ShowPageIndexBox="Always" PageSize="10" LayoutType="Table" PageIndexBoxType="DropDownList"
                                                ShowCustomInfoSection="Left" CustomInfoClass="paginator" CustomInfoSectionWidth="300px"
                                                SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" Direction="LeftToRight"
                                                HorizontalAlign="Right">
                                            </webdiyer:AspNetPager>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
