<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GiftList.aspx.cs" Inherits="ChainStock.Gift.GiftList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <link href="/Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/PointManage/GiftInfo.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ShowPic(path) {
            if (path != "") {
                var image = "<img src='" + path + "' width=\"379\" height=\"500\" />";
            }
            else {
                var image = "<img src='../images/Gift/nogift.jpg' width=\"379\" height=\"500\" />";
            }
            art.dialog({
                padding: 0,
                title: '照片',
                content: image,
                lock: true,
                width: 300,
                height: 300
            });
        }      

    </script>
    <style type="text/css">
        #GiftPhoto_UploadifyUploader
        {
            float: left;
            margin-left: 12px;
        }
        .uploadifyQueueItem
        {
            width:auto;
            }
    </style>
</head>
<body style="padding-right: 1px">
    <form id="frmGiftList" runat="server">
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">

                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>

                    <div id="DiveGiftInfo" style="display: none;">
                        <div class="user_regist_Allleft" style="width: 620px">
                            <div class="user_regist_left" style="width: auto">
                                <table class="tableStyle" cellspacing="0" cellpadding="0" style="width: 620px;">
                                    <tr>
                                        <td style="width: 400px">
                                            <table class="tableStyle" cellspacing="0" cellpadding="0" style="width: 100%; margin: auto;
                                                border: 0px" rules="none">
                                                <tr>
                                                    <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                        <span style="color: #ff4800; vertical-align: middle">*</span>礼品名称：
                                                    </td>
                                                    <td class="tableStyle_right" style="border: 0px">
                                                        <input id="txtGiftName" type="text" runat="server" class="border_radius" />
                                                        <input id="txtGiftID" type="hidden" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                        <span style="color: #ff4800; vertical-align: middle">*</span>礼品简码：
                                                    </td>
                                                    <td class="tableStyle_right" style="border: 0px">
                                                        <input id="txtGiftCode" type="text" runat="server" class="border_radius" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                        <span style="color: #ff4800; vertical-align: middle">*</span>礼品分类：
                                                    </td>
                                                    <td class="tableStyle_right" style="border: 0px">
                                                        <select id="sltGiftClass" runat="server" class="selectWidth">
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                        <span style="color: #ff4800; vertical-align: middle">*</span>库存数量：
                                                    </td>
                                                    <td class="tableStyle_right" style="border: 0px">
                                                        <input id="txtGiftStockNumber" type="text" runat="server" class="border_radius" />
                                                        <input id="txtGiftExchangeNumber" type="hidden" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                        <span style="color: #ff4800; vertical-align: middle">*</span>所需积分：
                                                    </td>
                                                    <td class="tableStyle_right" style="border: 0px">
                                                        <input id="txtGiftExchangePoint" type="text" runat="server" class="border_radius" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tableStyle_left" style="border: 0px; background-color: White">
                                                        备注：
                                                    </td>
                                                    <td class="tableStyle_right" colspan="2" style="vertical-align: top; border: 0px">
                                                        <textarea id="txtGiftRemark" rows="4" runat="server" style="width: 250px; word-wrap: break-word;
                                                            outline: none; resize: none;"></textarea>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 200px; background-color: #edf2f6; text-align: center;vertical-align: top; height:270px;">
                                            <div style="width: 180px;margin-left: auto; margin-right: auto; margin-top:0px;">
                                                <div class="user_regist_pic" style="height: 130px; border: 0px; width: 180px;">
                                                    <img alt="" id="imgGiftPhoto" src="../images/Gift/nogift.jpg" style=" height:130px; width:180px;" runat="server" />
                                                    <input type="hidden" id="txtGiftPhoto" runat="server" />
                                                </div>
                                                <p>
                                                    <span style="color: #DB9944; font-size: 12px; font-weight:bold;">相片支持大小为160*120</span>
                                                </p>
                                                <div class="common_Button_all" style="text-align: center; margin-top:2px;">
                                                    <input id="GiftPhoto_Uploadify" type="file" class="common_Button common_ServiceButton" />
                                                    <input id="btnGiftPhotoUpload" style="margin-left: 16px;" type="button" class="common_Button"
                                                        value="上传图片" onclick="javascript:$('#GiftPhoto_Uploadify').uploadifyUpload();" />
                                                </div>
                                                <div id="Gift_fileQueue">
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="text-align: center; height: 36px">
                                            <input id="btnGiftSave" type="button" value="保   存" class="buttonColor" />&nbsp;
                                            <input id="btnGiftReset" type="button" class="buttonRest" value="重   置" />
                                            <input id="GiftAddOrEdit" type="hidden" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
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
                                            <input id="btnGiftAdd" type="button" value="新增礼品" onclick="GiftAdd()" class="common_Button"
                                                runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvwGiftList">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                序号
                                            </th>
                                            <th>
                                                礼品名称
                                            </th>
                                            <th>
                                                礼品简码
                                            </th>
                                            <th>
                                                所属分类
                                            </th>
                                            <th>
                                                所需积分
                                            </th>
                                            <th>
                                                礼品缩略图
                                            </th>
                                            <th>
                                                库存数量
                                            </th>
                                            <th>
                                                兑换数量
                                            </th>
                                            <th>
                                                商家名称
                                            </th>
                                            <th>
                                                礼品备注
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
                                            <asp:Label ID="lblGIftID" runat="server" Text='<%# Bind("GiftID") %>' Visible="false"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("GiftName")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("GiftCode")%>
                                        </td>
                                        <td>
                                            <%# Eval("GiftClassName")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("GiftExchangePoint")%>
                                        </td>
                                        <td>
                                            <span onclick='return ShowPic("<%#Eval("GiftPhoto") %>");' style="cursor: pointer;">
                                                <img id="imgPhoto" alt="" runat="server" src='<%# GetPhoto(Eval("GiftPhoto").ToString()) %>'
                                                    style="width: 30px; height: 30px;" />
                                            </span>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("GiftStockNumber")%>
                                        </td>
                                        <td style="text-align: right">
                                            <%# Eval("GiftExchangeNumber")%>
                                        </td>
                                        <td>
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("GiftRemark")%>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <a href="#" onclick='<%# string.Format(" btnGiftEdit(\"{0}\",\"{1}\")",Eval("GiftName"),Eval("GiftID")) %>'
                                                id="hyGiftEdit" runat="server">
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" /></a> <a href="#" id="hyGiftDel"
                                                    runat="server" onclick='<%# string.Format("return btnGiftDel(\"{0}\",\"{1}\")",Eval("GiftName"),Eval("GiftID")) %>'>
                                                    <img src="../images/Gift/del.png" alt="删除" title="删除" />
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
