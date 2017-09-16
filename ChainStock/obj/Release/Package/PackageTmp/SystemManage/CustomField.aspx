<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomField.aspx.cs" Inherits="ChainStock.SystemManage.CustomField" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/Module/SystemManage/CustomField.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="frmCustomField" runat="server">
    <input type="hidden" id="txtCustomID" runat="server" />
    <table style="width: 100%; height: 100%; word-wrap: break-word;" cellspacing="7">
        <tr>
            <td colspan="2" style="width: 100%;">
                <div class="system_Info">
                    <div class="system_Top">
                        <div class="user_regist_title">
                            <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
                        </div>
                    </div>
                    <div id="CustomFieldAddOrEdit" style="display: none;">
                        <table class="tableStyle" cellspacing="0" cellpadding="2" style="width: 600px; margin: auto">
                            <tr>
                                <td class="tableStyle_left">
                                    请&nbsp;&nbsp;选&nbsp;&nbsp;择：
                                </td>
                                <td class="tableStyle_right">
                                    <label style="vertical-align: text-bottom;">
                                        <input id="MemCustom" type="radio" value="1" name="radCustomType" />
                                        <label class="lbsetCk" style="vertical-align: middle;">
                                            会员表自定义属性</label></label>
                                    <label style="vertical-align: text-bottom;">
                                        <input id="GoodCustom" type="radio" value="2" name="radCustomType" />
                                        <label class="lbsetCk" style="vertical-align: middle;">
                                            产品表自定义属性</label></label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>属性名称：
                                </td>
                                <td class="tableStyle_right">
                                    <input type="text" class="border_radius" id="txtCustomName" runat="server" />
                                    <label style="vertical-align: text-bottom;">
                                        &nbsp;
                                        <label class="lbsetCk" style="vertical-align: middle;">
                                            <input type="checkbox" id="chkIsNull" />值可以为空</label>
                                    </label>
                                    <label style="vertical-align: text-bottom;">
                                        <label class="lbsetCk" style="vertical-align: middle;">
                                            <input type="checkbox" id="chkIsShow" />显示到列表</label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>属性代码：
                                </td>
                                <td class="tableStyle_right">
                                    <label style="vertical-align: text-bottom;">
                                        <input type="text" class="border_radius" id="txtCustomCode" runat="server" />
                                        <label class="lbsetCk" style="vertical-align: middle;">
                                            &nbsp;&nbsp;属性英文名，用于系统识别
                                        </label>
                                    </label>
                                </td>
                            </tr>
                            <tr>
                                <td class="tableStyle_left">
                                    数据类型：
                                </td>
                                <td id="trCustomFieleType" runat="server" class="tableStyle_right">
                                    <label style="vertical-align: text-bottom;">
                                        <input id="radText" runat="server" type="radio" name="radCustomFieldType" value="text" />
                                        <label class="lbsetCk" style="vertical-align: middle;">
                                            文本</label></label>&nbsp;
                                    <label style="vertical-align: text-bottom;">
                                        <input id="radDate" runat="server" type="radio" name="radCustomFieldType" value="date" />
                                        <label class="lbsetCk" style="vertical-align: middle;">
                                            日期</label></label>&nbsp;
                                    <label style="vertical-align: text-bottom;">
                                        <input id="radSelect" runat="server" type="radio" name="radCustomFieldType" value="select" />
                                        <label class="lbsetCk" style="vertical-align: middle;">
                                            选择项</label></label>
                                </td>
                            </tr>
                            <tr id="trCustomSelectData" runat="server" style="display: none;">
                                <td class="tableStyle_left">
                                    属&nbsp;&nbsp;性&nbsp;&nbsp;值：
                                </td>
                                <td class="tableStyle_right">
                                    <input type="text" id="txtCustomSelectData" class="border_radius" runat="server" />&nbsp;&nbsp;多个选项以<b>“|”</b>分隔
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="tableStyle_right">
                                    <div class="buton" style="text-align: center;">
                                        <input type="button" id="btnCustomSave" class="buttonColor" value="保   存" />
                                        &nbsp
                                        <input type="button" id="btnCustomReset" class="buttonRest" value="重   置" />
                                        <input id="CustomEditOrAdd" type="hidden" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="user_List_All">
                        <div class="user_List_top" style="height: 34px;">
                            <table class="tableStyle" style="width: 100%">
                                <tr style="color: #333333; background-color: #F7F6F3;">
                                    <td class="user_List_styleLeft">
                                        快捷操作：
                                    </td>
                                    <td class="user_List_styleRight">
                                        <div class="user_List_Button">
                                            <input id="btnCustomFieldAdd" type="button" value="新增属性" class="common_Button" runat="server" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnIsShowAll" runat="server" Text="显示全部" class="common_Button" OnClick="btnIsShowAll_Click" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table class="table-style table-hover user_List_txt">
                            <asp:Repeater runat="server" ID="gvCustomFieldList">
                                <HeaderTemplate>
                                    <thead class="thead">
                                        <tr class="th">
                                            <th>
                                                显示到列表
                                            </th>
                                            <th>
                                                自定义字段名称
                                            </th>
                                            <th>
                                                自定义字段
                                            </th>
                                            <th>
                                                数据类型
                                            </th>
                                            <th>
                                                属性详情
                                            </th>
                                            <th>
                                                会员/商品
                                            </th>
                                            <th>
                                                允许为空
                                            </th>
                                            <th>
                                                日期
                                            </th>
                                            <th>
                                                商家
                                            </th>
                                            <th>
                                                操作员
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
                                            <asp:CheckBox ID="ckbCustomFieldShow" AutoPostBack="True" runat="server" OnCheckedChanged="ckbCustomFieldShow_CheckedChanged"
                                                Checked='<%# Eval("CustomFieldIsShow") %>' />
                                            <asp:Literal runat="server" ID="ltrID" Text='<%# Eval("CustomFieldID") %>' runat="server"
                                                Visible="false"></asp:Literal>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("CustomFieldName")%>
                                        </td>
                                        <td style="text-align: left; padding-left: 2px;">
                                            <%# Eval("CustomField")%>
                                        </td>
                                        <td>
                                            <%# GetCustomFieldType(Eval("CustomFieldType").ToString())%>
                                        </td>
                                        <td style="text-align: left">
                                            <%# Eval("CustomFieldInfo")%>
                                        </td>
                                        <td <%# Eval("CustomType").ToString() == "1" ? "style='color:Red'" : "style='color:Blue'"%>>
                                            <%# Eval("CustomType").ToString() == "1" ? "会员" : "产品"%>
                                        </td>
                                        <td>
                                            <%# Boolean.Parse(Eval("CustomFieldIsNull").ToString()) ? "是" : "否"%>
                                            <%--<asp:CheckBox ID="ckbCustomFieldIsNull" AutoPostBack="false" runat="server" Checked='<%# Eval("CustomFieldIsNull")%>' />--%>
                                        </td>
                                        <td>
                                            <%# Eval("CustomFieldCreateTime","{0:yyyy-MM-dd}")%>
                                        </td>
                                        <td>
                                            <%# Eval("ShopName")%>
                                        </td>
                                        <td>
                                            <%# Eval("UserName")%>
                                        </td>
                                        <td class="listtd" style="width: 60px;">
                                            <a href="#" onclick='<%# string.Format(" btnCustomFieldEdit(\"{0}\",\"{1}\")",Eval("CustomFieldName"),Eval("CustomFieldID")) %>'
                                                id="hyCustomEdit" runat="server">
                                                <img src="../images/Gift/eit.png" alt="编辑" title="编辑" />
                                            </a><a href="#" id="hyCustomDel" runat="server" onclick='<%# string.Format("return btnCustomFieldDel(\"{0}\",\"{1}\")",Eval("CustomFieldName"),Eval("CustomFieldID")) %>'>
                                                <img src="../images/Gift/del.png" alt="删除" title="删除" />
                                            </a>
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
