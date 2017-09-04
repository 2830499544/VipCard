<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MicroGoodsInfo.aspx.cs"
    Inherits="ChainStock.MicroWebsite.MicroGoodsInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商品登记</title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    

    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>

    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>

    <link href="../Inc/Style/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.uploadify.swfobject.js" type="text/javascript"></script>

    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>

    <script src="../Scripts/Module/MicroWebsite/MicroGoodsInfo.js" type="text/javascript"></script>
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
                        商品信息
                    </div>

                    <table width="720" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle" style="float:left;">
                        <tr>
                            <td class="tableStyle_left">
                                <span style="color: #ff4800; vertical-align: middle">*</span>商品编码：
                            </td>

                            <td colspan="3" class="tableStyle_right">
                                <input id="txtGoodsCode" type="text" class="border_radius" runat="server" title="商品编号是唯一的,有条件的商家最好使用商品条码编号" maxlength="25" />
                                <input id="txtGoodsID" type="hidden" runat="server" />

                                <label id="lblAutoGoodsCode" runat="server" style="vertical-align: text-bottom;">
                                    &nbsp;&nbsp;
                                    <label class="lbsetCk" style="vertical-align: middle;">
                                        <input id="chkAutoGoodsCode" type="checkbox" />
                                        自动创建商品编码
                                    </label>
                                    <input id="txtCode" type="hidden" runat="server" />
                                </label>

                            </td>
                        </tr>

                        <tr>
                            <td class="tableStyle_left">
                                <span style="color: #ff4800; vertical-align: middle">*</span>商品名称：
                            </td>
                            <td class="tableStyle_right">
                                <input id="txtGoodsName" type="text" class="border_radius" runat="server" title="请输入商品名称"
                                    maxlength="20" />
                            </td>
                            <td class="tableStyle_left">
                                商品分类：
                            </td>
                            <td class="tableStyle_right">
                                <select id="sltGoodsClass" runat="server" class="selectWidth">
                                </select>
                            </td>
                        </tr>

                        <tr>
                            <td class="tableStyle_left">
                                <span style="color: #ff4800; vertical-align: middle">*</span>零售金额：
                            </td>
                            <td class="tableStyle_right">
                                <input id="txtMicroPrice" type="text" runat="server" class="border_radius" />
                            </td>
                            <td class="tableStyle_left">
                                    <span style="color: #ff4800; vertical-align: middle">*</span>商品原价：
                            </td>
                            <td class="tableStyle_right">
                                <input id="txtMicroSalePrice" type="text" runat="server" class="border_radius" />
                            </td>
                        </tr>

                        <tr>
                            <td class="tableStyle_left">
                                参考进价：
                            </td>
                            <td class="tableStyle_right">
                                <input id="txtGoodsBidPrice" type="text" runat="server" class="border_radius" 
                                    title="商品入库时使用参考进价" value="0" />
                            </td>
                            <td class="tableStyle_left">
                                积分数量：
                            </td>
                            <td class="tableStyle_right">
                                <input id="txtGoodsPoint" type="text" runat="server" class="border_radius" title="输入积分数量，则商品消费时获得的积分按照商品拥有的积分计算，如果积分数量为空，则该商品不积分"
                                    value="0" />
                            </td>
                        </tr>

                        <tr>
                            <td class="tableStyle_left">
                                商品简介：
                            </td>
                            <td class="tableStyle_right" colspan="3">
                                <textarea id="txtGoodsRemark" rows="3" runat="server" style="width: 90%; word-wrap: break-word;
                                    outline: none; resize: none;" title="请输入商品简介"></textarea>
                            </td>
                        </tr>
                    </table>

                    <div class="user_regist_left">
                        <div style="text-align: center; height: 36px">
                            <input id="btnGoodsSave" type="button" class="buttonColor" value="保　存" />
                            <input id="btnGoodsReset" type="button" class="buttonRest" value="重　置" />
                            <input type="hidden" name="txtUpdateGoodsName" id="txtUpdateGoodsName" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div style="float:left;margin-left:10px;">
                <table border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <th class="tableStyle_left" style="text-align:center;">
                            商品相片
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <div>
                                <img alt="" id="imgGoodsPhoto" src="../images/Gift/nogift.jpg" style="width:200px; height:200px;" runat="server" /><br />
                                <span style="color: gray">相片支持大小为150*150</span>
                                <input type="hidden" id="txtGoodsPhoto" runat="server" />
                            </div>
                            <div style="margin-top: 10px; height: 30px;">
                                <div style="width: 83px; height: 25px; float: left; margin-left: 20px;">
                                    <input id="GoodsPhoto_Uploadify" type="file" style="display: none;" class="buttonColor" />
                                </div>
                                <div>
                                    <input id="btnGoodsPhotoUpload" type="button" class="common_Button" value="上传图片" onclick="javascript:$('#GoodsPhoto_Uploadify').uploadifyUpload();" />
                                </div>
                            </div>
                            <div id="Goods_fileQueue">
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
