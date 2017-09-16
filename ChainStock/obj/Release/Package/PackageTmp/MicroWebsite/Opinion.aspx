<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Opinion.aspx.cs" Inherits="ChainStock.MicroWebsite.Opinion" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Report/Report.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
        <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/MicroWebsite/Proposal.js" type="text/javascript"></script>
     <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //$(function () {
        //BindNullList("gvOpinion");
        //})
    </script>
</head>
<body>
    <form id="frmOpinion" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                     <div id="dvMessageReply" style="display: none;">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 600px;">
                            <tr>
                                <td class="tableStyle_left">
                                    留言内容：
                                </td>
                                <td class="tableStyle_right">
                                    <span id="spMessage"></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    回复内容：<span id="MessageID" style="display: none"></span>
                                </td>
                                <td class="tableStyle_right">
                                
                                    <textarea name="content" " id="content" class="border_radius" style="width: 90%; height:100px; word-wrap: break-word;
                                                                outline: none; resize: none;"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" class="tableStyle_right">
                                    <div class="buton" style="text-align: center;">
                                        <input type="button" id="btReply" value="提交" class="buttonColor" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                             <div id="dvScanMessageReply" style="display: none;">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 600px;">
                            <tr>
                                <td class="tableStyle_left">
                                    回复内容：
                                </td>
                                <td class="tableStyle_right">
                                    <span id="spReplyContent"></span>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    回复时间：<span id="Span2" style="display: none"></span>
                                </td>
                                <td class="tableStyle_right"> <span id="spReplyTime"></span>
                                 
                                </td>
                            </tr>
                           
                        </table>
                    </div>
                    <div class="user_List_All">
                        <div class="user_List_top">
                            <table width="100%" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343"
                                class="tableStyle">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <asp:Button ID="btnOpinionSearch" runat="server" Text="查   询" class="common_Button"
                                                OnClick="btnOpinionSearch_Click" />
                                            <asp:Button ID="btnOpinionExcel" runat="server" Text="导   出" class="common_Button"
                                                OnClick="btnOpinionExcel_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="tableStyle" style="width: 100%">
                            <tr>
                                <td class="tableStyle_left">
                                    会员信息：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtQueryMem" type="text" runat="server" class="input_txt border_radius"
                                        title="会员卡号/姓名/手机号" />
                                </td>
                                <td class="tableStyle_left">
                                    消费时间：
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtStaffStartTime" type="text" runat="server" class="Wdate border_radius" />
                                </td>
                                <td class="tableStyle_left">
                                    至
                                </td>
                                <td class="tableStyle_right">
                                    <input id="txtStaffEndTime" type="text" runat="server" class="Wdate border_radius" />
                                </td>
                            </tr>
                        </table>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvOpinion">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                会员姓名
                                            </th>                                         
                                            <th>
                                                会员卡号
                                            </th>                                           
                                            
                                            <th>
                                                留言内容
                                            </th>                                          
                                          
                                            <th>
                                                留言时间
                                            </th>
                                            <th>状态</th>
                                            <th>操作</th>
                                        </tr>
                                    </thead>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="td">
                                        <td>
                                            <asp:Label ID="lblNumber" runat="server" Text="1"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <%# GetMemName(Eval("MemID"))%>
                                        </td>
                                       
                                        <td style="text-align: left">
                                               <%#Eval("MemCard")%>
                                        </td>
                                      
                                       
                                        <td style="text-align: left">
                                            <%# Eval("MessageContent")%>
                                        </td>
                                      
                                        
                                        <td style="text-align: center">
                                            <%# Eval("MessageTime")%>
                                        </td>
                                          <td <%# Eval("IsReply").ToString() == "0" ? "style='color:Red'" : "style='color:Blue'"%>>
                                            <%# Eval("IsReply").ToString() == "0"?"未回复":"已回复"%>
                                        </td>
                                        <td class="listtd" style="width: 60px;"> 
                                          <a  style=' <%# Eval("IsReply").ToString() == "0" ? "" : "display:none"%>'  href="#" onclick='<%# string.Format(" ReplyMsg(\"{0}\",\"{1}\")",Eval("MessageID"),Eval("MessageContent")) %>'
                                                    id="hyMessageReply" runat="server">
                                                    <img src="../images/Gift/manage.png" title="回复" alt="回复" />
                                                </a>
                                                       
                                                         <a   style=' <%# Eval("IsReply").ToString() == "1" ? "" : "display:none"%>' href="#" onclick='<%# string.Format(" ScanReplyMsg(\"{0}\",\"{1}\")",Eval("ReplyContent"),Eval("ReplyTime")) %>'
                                                    id="A1" runat="server">
                                                    <img src="../images/Gift/info.png" title="查看回复" alt="查看回复" />
                                                </a>

                                                <a href="#" runat="server" id="hyMessageDel" onclick='<%# string.Format("return btnMessageDel(\"{0}\",\"{1}\")",Eval("MessageID"),Eval("MessageID")) %>'>
                                                    <img alt="删除" title="删除" src="../images/Gift/del.png" />
                                                </a>
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
