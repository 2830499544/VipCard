<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSubMem.aspx.cs"
    Inherits="ChainStock.Member.AddSubMem" %>     
<%@ Register Src="../Controls/MemberSearch.ascx" TagName="MemberSearch" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加子卡</title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Mem/AddSubMem.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/LodopFuncs.js" type="text/javascript"></script>    
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmTransferMoney" runat="server">
    <div class="system_Info box_right">
        <%--打印的次数 --%>
        <input type="hidden" value="" id="PointNum" runat="server"/>
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div>           
            <uc1:MemberSearch ID="ucMemberSearch" runat="server" />
        </div>                   
        <div class="user_regist_Allleft">
         <div class="user_List_top" style="height: 34px;">
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <input id="btnSubMemAdd" type="button" value="添加子卡" class="common_Button_new" onclick="SubMemAdd()"
                                                runat="server" />
                                                  <input type="text" id="txtUserType" style="width: 23px; display: none" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>       
             <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <tr>                   
                        <td style="vertical-align: top;">
                            <table border="0" cellpadding="0" cellspacing="1" class="table-style table-hover user_List_txt"
                                width="100%" id="SubMemList">
                                <tr>
                                    <td>
                                        <script type="text/javascript">
                                            ListLoading();
                                        </script>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
             <div id ="divAddSubMem" class="user_regist_left" style="display:none;">             
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">                 
                    <tr>
                        <td class="tableStyle_left">
                            子卡卡号：
                        </td>
                        <td class="tableStyle_right">
                             <input id="txtSubCardNumber" type="text" runat="server" class="border_radius" />                         
                      </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            子卡姓名：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtSubName" type="text" runat="server" class="border_radius"  />                                              
                        </td>
                    </tr>
                    <tr>
                        <td class="tableStyle_left">
                            子卡手机号码：
                        </td>
                        <td class="tableStyle_right">
                            <input id="txtSubMemMobile" type="text" runat="server" class="border_radius"  />   
                        </td>
                    </tr>  
                    <tr>
                        <td class="tableStyle_left">
                            是否启用：
                        </td>
                        <td class="tableStyle_right">
                               <label class="lbsetCk" style="vertical-align: middle;">
                            <label style="vertical-align: middle;">
                                <input type="radio" name="rd_used" id="rdusedT" checked="True" runat="server" value="True"/>
                                启用
                            </label>
                        </label>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        <label class="lbsetCk" style="vertical-align: middle;">
                            <label style="vertical-align: middle;">
                                <input type="radio" name="rd_used" id="rdusedF" runat="server" value="False"/>
                                停用
                            </label>
                        </label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 36px; border:none; " >                                
                            <input id="btnSave" type="button" value="提   交" class="buttonColor" />
                            &nbsp
                            <input id="btnReset" type="button" class="buttonRest" value="重   置" />
                        </td>
                    </tr>
                </table>              
                <input id="MemCard" type="hidden" runat="server" />
                <input id="SubMemID" type="hidden" runat="server" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
