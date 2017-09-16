<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemSpecial.aspx.cs" Inherits="ChainStock.Member.MemSpecial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>优惠活动</title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/MemSpecial.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

    </script>
</head>
<body>
    <form id="frmSpecial" runat="server">
    <div class="system_Info box_right">
        <%--打印的次数 --%>
        <input type="hidden" value="" id="PointNum" runat="server"/>
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
 
        <div class="user_regist_Allleft">
            <div class="user_regist_left">
                <div class="user_regist_infor" style="text-align: left">
                    优惠活动
                </div>
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            活动名称：
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" name="name" id="txtSpecialName" class="border_radius" value=" " runat="server" />
                        </td>
                    </tr>
                  <tr>
                        <td class="tableStyle_left">
                            活动期限：
                        </td>
                        <td class="tableStyle_right">
                         <label class="lbsetCk" style="vertical-align: middle;">
                            <label style="vertical-align: middle;">
                                <input type="radio" name="rd_special" id="rdDate" checked="True" runat="server" value="Date"/>
                                固定期限
                            </label>
                        </label>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        <label class="lbsetCk" style="vertical-align: middle;">
                            <label style="vertical-align: middle;">
                                <input type="radio" name="rd_special" id="rdWeek" runat="server" value="Week"/>
                                每周
                            </label>
                        </label>
                          &nbsp;&nbsp;&nbsp;&nbsp;
                        <label class="lbsetCk" style="vertical-align: middle;">
                            <label style="vertical-align: middle;">
                                <input type="radio" name="rd_special" id="rdMonth" runat="server" value="Month"/>
                                每月
                            </label>
                        </label>
                          &nbsp;&nbsp;&nbsp;&nbsp;
                        <label class="lbsetCk" style="vertical-align: middle;">
                            <label style="vertical-align: middle;">
                                <input type="radio" name="rd_special" id="rdBirthday" runat="server" value="Birthday"/>
                                会员生日
                            </label>
                        </label>                     
                        </td>
                    </tr>

                    <tr id="trDate" style="display:;"  runat="server">
                        <td class="tableStyle_left">                            
                        </td>
                        <td class="tableStyle_right">
                           <input id="txtStartTime" runat="server" type="text" class="Wdate border_radius"  maxlength="20" /><span style="font-size:11px">&nbsp;&nbsp;至</span>
                           <input id="txtEndTime" runat="server" type="text" class="Wdate border_radius"  maxlength="20" />                           
                        </td>
                    </tr>
                    <tr id="trWeek" style="display:none;"  runat="server">
                        <td class="tableStyle_left">                            
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" name="name" id="txtWeek" class="border_radius" value=" " runat="server" />
                             <span style="color:#FF5809;font-size:11px">&nbsp;&nbsp;例如：要设置每周一、三、日开展活动，请填写1|3|7</span>
                        </td>
                    </tr>
                    <tr id="trMonth" style="display:none;" runat="server">
                        <td class="tableStyle_left">                            
                        </td>
                        <td class="tableStyle_right">
                            <input type="text" name="name" id="txtMonth" class="border_radius" value=" " runat="server" />
                             <span style="color:#FF5809; font-size:11px"> &nbsp;&nbsp;例如：要设置每月1日、16日、22日开展活动，请填写1|16|22</span>
                        </td>
                    </tr>

                    <tr>
                        <td class="tableStyle_left">
                            操作人员：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblSpecialUSer" runat="server" style="font-size: 14px; font-weight: bold;">
                            </label>
                            <input type="hidden"  value="" id="SpecialUSerID" runat="server" />
                        </td>
                    </tr>
                     
                    <tr>
                        <td class="tableStyle_left">
                            充值金额：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtMoney" type="text" runat="server" class="border_radius" value="0" maxlength="8" />
                        
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            赠送金额：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtGiveMoney" type="text" runat="server" class="border_radius" value="0"
                                maxlength="8" />
                        </td>
                    </tr>
               
                 
                    <tr>
                        <td class="tableStyle_left">
                            备注：
                        </td>
                        <td class="tableStyle_right" colspan="3">
                            <textarea id="txtSpecialRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                resize: none; outline: none;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 36px; border:none; " >
                           
                           
                            <input id="BtnSpecialSave" type="button" value="保   存" class="buttonColor" />
                            &nbsp
                            <input id="BtnSpecialReset" type="button" class="buttonRest" value="重   置" />
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblPrintTitle" Style="display: none" runat="server" Text="Label"></asp:Label>
                <asp:Label ID="lblPrintFoot" Style="display: none" runat="server" Text="Labe2"></asp:Label>
                <input id="txtSpecialID" type="hidden" runat="server" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
