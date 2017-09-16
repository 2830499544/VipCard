<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainInfo.aspx.cs"
    Inherits="ChainStock.SystemManage.MainInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>运营商信息</title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>

    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
 
    <script src="../Scripts/Module/SystemManage/ShopInfo.js" type="text/javascript"></script>

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
                        运营商信息
                    </div>

                    <table width="720" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle" style="float:left;">
             

          <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>运营商名称：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopName" type="text" runat="server" class="border_radius" />
                                    <input id="txtShopID" type="hidden" runat="server" />
                                </td>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>运营商联系人：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopContactMan" type="text" runat="server" class="border_radius" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    联系电话：
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
                                <td class="tableStyle_left">
                                    打印标题：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopPrintTitle" type="text" runat="server" class="border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    打印脚注：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopPrintFoot" type="text" runat="server" class="border_radius" />
                                </td>
                            </tr>
                              <tr runat="server" id="trSettlement">
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>结算周期：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtSettlementInterval" type="text" title="总店与分店多少天进行一次结算" runat="server"
                                        class="border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>提成比例：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtShopProportion" type="text" runat="server" title="请输入0~1之间的数值" class="border_radius" />
                                </td>
                            </tr>
                        
                                  <input id="txtTotalRate" type="text"  style=" display:none;" runat="server" title="请输入0~1之间的数值" class="border_radius" />
                             
                          
                          <tr id="trAlliance" runat="server" style=" display:none;">
                                <td class="tableStyle_left">
                                    联盟商类型：
                                </td>
                                <td class="tableStyle_right">
                                    <label>
                                        <input type="radio" name="isLms" id="rdislms" runat="server" value="1" checked="true" />联盟商</label><label>
                                            <input type="radio" name="isLms" id="rdisnotlms" runat="server" value="0" />联盟商</label>
                                </td>
                                <td colspan="2" style="text-align: left;">
                                    <font style="color: Red;">（此设置保存之后将无法更改）</font>
                                </td>
                            </tr>
                            <tr runat="server" id="trSltShop" style=" display:none;">
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>所属商家：
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
                                <td class="tableStyle_left">
                                    短信不足发送应按：
                                </td>
                                <td colspan="3" class="tableStyle_right">
                                    <label>
                                        <input type="radio" id="rbbzfs" name="SmsType" value="1" runat="server" enableviewstate="True" />不准发送</label>
                                    <label>
                                        <input type="radio" id="rbkytz" name="SmsType" value="2" runat="server" />短信透支（短信量变负数）</label>
                                </td>
                            </tr>
                            <tr runat="server" id="trShopPoint">
                                <td class="tableStyle_left">
                                    积分不足消费应按：
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
                            <tr style=" display:none;">
                                <td class="tableStyle_left" >
                                    是否锁定：
                                </td>
                                <td colspan="3" class="tableStyle_right">
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radChooseYesOrNo" id="radChooseYes" runat="server" value="0" />
                                        <label style="vertical-align: middle;">
                                            暂时锁定</label></label>
                                    <label style="vertical-align: text-bottom;">
                                        <input type="radio" name="radChooseYesOrNo" id="radChooseNo" runat="server" value="1" />
                                        <label style="vertical-align: middle;">
                                            不锁定<font color="red">&nbsp;&nbsp;&nbsp;(锁定则该联盟商的所有员工不能登录)</font></label></label>
                                </td>
                            </tr>
                             <tr>
                                    <td class="tableStyle_left"  rowspan="2">联系地址：
                                    </td>
                                    <td class="tableStyle_right" colspan="3" style=" text-align:left">
                                     <select id="sltProvince" runat="server" class="selectWidth" style="width: 120px">
                                                    </select>省<select id="sltCity" runat="server" class="selectWidth" style="width: 120px">
                                                        <option value="">=== 请选择 ===</option>
                                                    </select>市 <select id="sltCounty" runat="server" class="selectWidth" style="width: 120px">
                                                        <option value="">=== 请选择 ===</option>
                                                    </select>
         

                                    </td>
                                    </tr>
                                    <tr>
                                      <td colspan="3" class="tableStyle_right">
                                                    <input id="txtShopAddress" type="text" runat="server" class="border_radius" style="width: 520px;" /></td>
                                    </tr>
                                </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    备注：
                                </td>
                                <td class="tableStyle_right" colspan="3">
                                    <textarea id="txtShopRemark" rows="3" runat="server" style="width: 520px; word-wrap: break-word;
                                        outline: none; resize: none;"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" class="tableStyle_right ">
                                    <div class="buton" style="text-align: center;">
                                        <input id="btnShopSave" type="button" class="buttonColor" value="保   存 " />
                                  <%--      <input id="btnShopReset" type="button" class="buttonRest" value="重   置 " />--%>
                                        <input id="ShopAddOrEdit" type="hidden" />
                                        <input id="txtShopType" type="hidden" runat="server" />
                                        <input id="chkSMS" type="checkbox" runat="server" style="display: none" />
                                            <input type="hidden" id="hdShopID" runat="server" />
                                    </div>
                                </td>
                            </tr>
                    </table>

                  
                </div>
            </div>
          
        </div>
    </form>
</body>
   <script type="text/javascript" >

       var shopName = $("#txtShopName").val();
     
       var shopID = $("#txtShopID").val();
       MainInfoBind(shopName, shopID);
    </script>
</html>
