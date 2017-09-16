<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberExplanation.aspx.cs" Inherits="ChainStock.MicroWebsite.MemberExplanation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>

    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>

    <script src="../Scripts/Module/MicroWebsite/MemberExplanation.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmMemberExplanation" runat="server">
        <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
            <tr>
                <td colspan="2" style="width: 100%;">
                    <div class="system_Info">
                        
                        <div class="system_Top">
                            <div class="user_regist_title">
                                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                            </div>
                        </div>

                        <div id="MemberExplanationInfo" style="display:none;">
                            <div class="user_regist_Allleft" style="width: 500px">
                                <div class="user_regist_left" style="width: auto">
                                    <table class="tableStyle" cellspacing="0" cellpadding="0" style="width: 500px;">
                                        <tr>
                                            <td class="tableStyle_left">
                                                公布内容：
                                            </td>
                                            <td class="tableStyle_right">
                                               <textarea id="txtMemberExplanationDesc" style="width: 410px; margin-left: 5px;
                                                word-wrap: break-word;height:80px;"></textarea>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="text-align: center">
                                                <input id="btnMemberExplanationSave" type="button" class="buttonColor" value="保   存 " />
                                                &nbsp
                                                <input id="btnMemberExplanationReset" type="button" class="buttonRest" value="重   置 " />
                                                <input type="hidden" id="txtType" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div class="user_List_All">
                            <div class="user_List_top">
                                <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                                    <tr style="color: #333333; background-color: #F7F6F3;">
                                        <td class="user_List_styleLeft">
                                            快捷操作：
                                        </td>
                                       <td class="user_List_styleRight">
                                            <div class="user_List_Button">
                                                <input id="btnMemberExplanationAdd" type="button" value="新增公布" class="common_Button" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <table class="table-style table-hover user_List_txt">
                                <asp:Repeater runat="server" ID="gvwMemberExplanation">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    序号
                                                </th>
                                                <th>
                                                    公布内容
                                                </th>
                                                <th>
                                                    公布时间
                                                </th>
                                                <th>
                                                    操作
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
                                                <%# Eval("MemberExplanationDesc")%>
                                            </td>
                                            <td style="text-align:center;">
                                                <%#Eval("MemberExplanationTime") %>
                                            </td>
                                            <td class="listtd" style="width: 60px;">
                                                <a href='javascript:btnMemberExplanationEdit("<%#Eval("MemberExplanationID") %>","<%#Eval("MemberExplanationDesc") %>")'>
                                                    <img src="../images/Gift/eit.png" alt="编辑" title="编辑" /></a> 
                                                <a href='javascript:btnMemberExplanationDel(<%#Eval("MemberExplanationID") %>)'>
                                                    <img src="../images/Gift/del.png" alt="删除" title="删除" /></a>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>

                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
