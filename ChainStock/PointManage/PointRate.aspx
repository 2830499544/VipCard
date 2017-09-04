<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PointRate.aspx.cs" Inherits="ChainPoint.PointManage.PointRate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Inc/Style/Style.css" rel="stylesheet" type="text/css" />
    
    <link href="../Inc/artDialogskins/blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.artDialog.basic.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog.iframeTools.js" type="text/javascript"></script>
    <script src="../Scripts/Module/PointManage/PointRate.js" type="text/javascript"></script>
    <script src="../Scripts/Module/Common/Common.js" type="text/javascript"></script>
    <style type="text/css">
        .tableStyle_left
        {
            width: 130px;
        }
        .tableStyle_right
        {
            width: 270px;
        }
    </style>
</head>
<body>
    <form id="frmPointRate" runat="server">
    <div class="system_Info box_right">
        <div class="system_Top">
            <div class="user_regist_title">
                <asp:Literal runat="server" ID="ltlTitle"></asp:Literal>
            </div>
        </div>
        <div class="user_regist_Allleft">
            <div class="user_regist_left">
                <div class="user_regist_infor" class="tableStyle_right">
                    积分提成设置
                </div>
                <table id="tb" border="1" cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle">
                    <tr>
                        <td class="tableStyle_left">
                            提成层级：
                        </td>
                        <td class="tableStyle_right">
                            <select name="select" id="sltPointRateLevel" class="selectWidth" onchange="sltChange(this)">
                                <option value="-1" id="opt0">====请选择====</option>
                                <option value="1" id="opt1">一 级</option>
                                <option value="2" id="opt2">二 级</option>
                                <option value="3" id="opt3">三 级</option>
                                <option value="4" id="opt4">四 级</option>
                                <option value="5" id="opt5">五 级</option>
                                <option value="6" id="opt6">六 级</option>
                                <option value="7" id="opt7">七 级</option>
                                <option value="8" id="opt8">八 级</option>
                                <option value="9" id="opt9">九 级</option>
                                <option value="10" id="opt10">十 级</option>
                                <option value="11" id="opt11">十一 级</option>
                                <option value="12" id="opt12">十二 级</option>
                                <option value="13" id="opt13">十三 级</option>
                                <option value="14" id="opt14">十四 级</option>
                                <option value="15" id="opt15">十五 级</option>
                            </select>
                        </td>
                        <td class="tableStyle_left">
                            提成方式：
                        </td>
                        <td class="tableStyle_right">
                            <label id="lblIsSMS" style="vertical-align: text-bottom;">
                                <input id="rdoPercent" type="radio" name="group" value="1" />
                                <label style="vertical-align: middle;">
                                    按百分比提成</label>
                                <input id="rdoNum" type="radio" name="group" value="2" />
                                <label style="vertical-align: middle;">
                                    按固定值提成
                                </label>
                            </label>
                        </td>
                    </tr>
                    <tr class="trPoint" id="tr1">
                        <td class="tableStyle_left" id="td1">
                            第一级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd1">
                            <input id="txtPointRateLevel1" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                        <td class="tableStyle_left" id="td2">
                            第二级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd2">
                            <input id="txtPointRateLevel2" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                    </tr>
                    <tr class="trPoint" id="tr2">
                        <td class="tableStyle_left" id="td3">
                            第三级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd3">
                            <input id="txtPointRateLevel3" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                        <td class="tableStyle_left" id="td4">
                            第四级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd4">
                            <input id="txtPointRateLevel4" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                    </tr>
                    <tr class="trPoint" id="tr3">
                        <td class="tableStyle_left" id="td5">
                            第五级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd5">
                            <input id="txtPointRateLevel5" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                        <td class="tableStyle_left" id="td6">
                            第六级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd6">
                            <input id="txtPointRateLevel6" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                    </tr>
                    <tr class="trPoint" id="tr4">
                        <td class="tableStyle_left" id="td7">
                            第七级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd7">
                            <input id="txtPointRateLevel7" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                        <td class="tableStyle_left" id="td8">
                            第八级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd8">
                            <input id="txtPointRateLevel8" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                    </tr>
                    <tr class="trPoint" id="tr5">
                        <td class="tableStyle_left" id="td9">
                            第九级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd9">
                            <input id="txtPointRateLevel9" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                        <td class="tableStyle_left" id="td10">
                            第十级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd10">
                            <input id="txtPointRateLevel10" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                    </tr>
                    <tr class="trPoint" id="tr6">
                        <td class="tableStyle_left" id="td11">
                            第十一级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd11">
                            <input id="txtPointRateLevel11" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                        <td class="tableStyle_left" id="td12">
                            第十二级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd12">
                            <input id="txtPointRateLevel12" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                    </tr>
                    <tr class="trPoint" id="tr7">
                        <td class="tableStyle_left" id="td13">
                            第十三级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd13">
                            <input id="txtPointRateLevel13" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                        <td class="tableStyle_left" id="td14">
                            第十四级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd14">
                            <input id="txtPointRateLevel14" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                    </tr>
                    <tr class="trPoint" id="tr8">
                        <td class="tableStyle_left" id="td15">
                            第十五级提成：
                        </td>
                        <td class="tableStyle_right" id="tdd15">
                            <input id="txtPointRateLevel15" type="text" class="border_radius" /><span class="RateLevel">%</span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: center; height: 36px; border: 0px">
                            <input id="btnpointRateSave" type="button" class="buttonColor" value="保   存" />&nbsp;
                            <input id="btnpointRateReset" type="button" class="buttonRest" value="重   置" />
                            <asp:CheckBox ID="chkPointLevel" runat="server" Style="display: none" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
