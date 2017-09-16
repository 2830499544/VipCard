<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Promotions.aspx.cs" Inherits="ChainStock.MicroWebsite.Promotions" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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

    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../Scripts/Module/MicroWebsite/Promotions.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmPromotions" runat="server">
        <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
            <tr>
                <td colspan="2" style="width: 100%;">
                    <div class="system_Info">

                        <div class="system_Top">
                            <div class="user_regist_title">
                                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
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
                                             <asp:Button ID="btnPromotionsAdd" runat="server" Text="新增活动" class="common_Button" onclick="btnPromotionsAdd_Click" 
                                               />
                                            
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <table class="table-style table-hover user_List_txt">
                                <asp:Repeater runat="server" ID="gvPromotionsList">
                                    <HeaderTemplate>
                                        <thead class="thead">
                                            <tr class="th">
                                                <th>
                                                    序号
                                                </th>
                                                <th>
                                                缩略图
                                                 </th>
                                                <th>
                                                  活动标题
                                                </th>
                                                <th>
                                                    起始时间
                                                </th>
                                                <th>
                                                    结束时间
                                                </th>
                                                <th>
                                                    优惠对象
                                                </th>
                                                 <th>
                                                    创建人
                                                </th>
                                                <th>
                                                    创建时间
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
                                             <td>
                                                 <span onclick='ShowPic("<%#Eval("PromotionsPhoto") %>")' style="cursor: pointer;">
                                                    <img id="imgPhoto" alt="" runat="server" src='<%#Eval("PromotionsPhoto") %>'
                                                        style="width: 80px; height: 35px;" />
                                                </span>
                                            </td>
                                            <td style="text-align: left">
                                                <%# Eval("PromotionsTitle")%>
                                            </td>
                                            <td style="text-align: center">
                                                <%# Convert.ToInt32(Eval("PromotionsType"))==0?"永久有效":Eval("PromotionsStart") %>
                                            </td>
                                            <td style="text-align: center">
                                                <%# Convert.ToInt32(Eval("PromotionsType"))==0?"永久有效":Eval("PromotionsEnd") %>
                                            </td>
                                            <td>
                                                <%# Convert.ToInt32(Eval("PromotionsMemLevel"))==-1?"所有会员":Eval("LevelName") %>
                                            </td>
                                             <td style="text-align: center">
                                                <%#Eval("UserName") %>
                                            </td>
                                            <td style="text-align: center">
                                                <%#Eval("PromotionsTime") %>
                                            </td>
                                            <td class="listtd" style="width: 60px;">
                                                <a href='javascript:btnPromotionsEdit(<%#Eval("PromotionsID") %>)'>
                                                    <img src="../images/Gift/eit.png" alt="编辑" title="编辑" /></a> 
                                                           <a  id="hyDel" href='javascript:btnPromotionsDel(<%#Eval("PromotionsID") %>,<%#Eval("PromotionsTitle","\"{0}\"") %>)'>
                                                    <img src="../images/Gift/del.png" alt="删除" title="删除" /></a>
                                              
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
