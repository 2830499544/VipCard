<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Photo.aspx.cs" Inherits="ChainStock.MicroWebsite.Photo" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>

    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>

    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>

    <script src="../Scripts/Module/MicroWebsite/Photo.js" type="text/javascript"></script>

    <style type="text/css">
         .uploadifyQueueItem
        {
                width:140px;
            }
            
        .uploadifyProgress
        {
                width:80%;
            }
            
        .fileName
        {
                width:0px;
                height:0px;
                display:block;
                overflow:hidden;
            }
    </style>
</head>
<body>
    <form id="frmPhotoShow" runat="server">
        <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
            <tr>
                <td colspan="2" style="width: 100%;">
                    <div class="system_Info">

                        <div class="system_Top">
                            <div class="user_regist_title">
                                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                            </div>
                        </div>

                        <div id="DivePhotoInfo" style="display: none;">
                            <div class="user_regist_Allleft" style="width: 620px">
                                <div class="user_regist_left" style="width: auto">
                                    <table class="tableStyle" cellspacing="0" cellpadding="0" style="width: 620px;">
                                        <tr>
                                            <td style="width: 400px">
                                                <table class="tableStyle" cellspacing="0" cellpadding="0" style="width: 100%; margin: auto;
                                                    border: 0px" rules="none">
                                                    <tr>
                                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                            <span style="color: #ff4800; vertical-align: middle">*</span>照片名称：
                                                        </td>
                                                        <td class="tableStyle_right" style="border: 0px">
                                                            <input id="txtPhotoName" type="text" class="border_radius" />
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                            <span style="color: #ff4800; vertical-align: middle">*</span>所属相册：
                                                        </td>
                                                        <td class="tableStyle_right" style="border: 0px">
                                                               <select id="sltAlbumID" runat="server" class="selectWidth">
                                </select>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                            照片描述：
                                                        </td>
                                                        <td class="tableStyle_right" colspan="2" style="vertical-align: top; border: 0px">
                                                            <textarea id="txtPhotoRemark" rows="13" runat="server" style="width: 250px; word-wrap: break-word;
                                                                outline: none; resize: none;"></textarea>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 200px; background-color: #edf2f6; text-align: center;vertical-align: top; height:270px;">
                                                <div style="width: 180px;margin-left: auto; margin-right: auto; margin-top:10px;">
                                                    <div class="user_regist_pic" style="height: 180px; border: 0px; width: 180px;">
                                                        <img alt="" id="imgPhotoPhoto" src="../images/Gift/nogift.jpg" style=" height:160px; width:160px;" runat="server" />
                                                    </div>
                                                    <p>
                                                        <span style="color:Gray; font-size: 12px; font-weight:bold;">相片支持大小为600*600</span>
                                                    </p>
                                                    <div class="common_Button_all" style="text-align: center; margin-top:2px;">
                                                        <input id="PhotoPhoto_Uploadify" type="file" class="common_Button common_ServiceButton" />

                                                        <input id="btnPhotoPhotoUpload" style="margin-left: 16px;" type="button" class="common_Button"
                                                            value="上传图片" onclick="javascript:$('#PhotoPhoto_Uploadify').uploadifyUpload();" />
                                                    </div>
                                                    <div id="Photo_fileQueue"></div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: center">
                                                <input id="btnPhotoSave" type="button" value="保   存" class="buttonColor" />&nbsp;
                                                <input id="btnPhotoReset" type="button" class="buttonRest" value="重   置" />
                                                <input type="hidden" id="txtPhotoID" />
                                                <input type="hidden" id="txtUpdatePhotoName" />
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
                                                <input id="btnPhotoAdd" type="button" value="新增照片" class="common_Button" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <table class="table-style table-hover user_List_txt">
                                <asp:Repeater runat="server" ID="rptPhoto">
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
                                                    照片名称
                                                </th>  
                                                 <th>
                                                    所属相册
                                                </th>                                               
                                                <th>
                                                    照片描述
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
                                                 <span onclick='ShowPic("<%#Eval("PhotoPhoto") %>")' style="cursor: pointer;">
                                                    <img id="imgPhoto" alt="" runat="server" src='<%#Eval("PhotoPhoto") %>'
                                                        style="width: 30px; height: 30px;" />
                                                </span>
                                            </td>
                                            <td>
                                                <%# Eval("PhotoName")%>
                                            </td>
                                           <td>
                                                <%# Eval("AlbumName")%>
                                            </td>
                                            <td>
                                                <%# Eval("PhotoDesc")%>
                                            </td>
                                             <td>
                                                <%# Eval("UserName")%>
                                            </td>
                                              <td>
                                                <%# Eval("PhotoCreateTime")%>
                                            </td>
                                            <td class="listtd" style="width: 120px;">
                                        
                                                <a  id="hyEdit" href='javascript:btnPhotoEdit("<%#Eval("PhotoID") %>","<%#Eval("PhotoName") %>","<%#Eval("PhotoPhoto") %>","<%#Eval("PhotoDesc") %>","<%#Eval("AlbumID") %>")'>
                                                    <img src="../images/Gift/eit.png" alt="编辑" title="编辑" /></a> 
                                                <a  id="hyDel" href='javascript:btnPhotoDel(<%#Eval("PhotoID") %>,<%#Eval("PhotoName","\"{0}\"") %>)'>
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