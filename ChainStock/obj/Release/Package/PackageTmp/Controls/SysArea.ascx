<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SysArea.ascx.cs" Inherits="ChainStock.Controls.SysArea" %>
<script src="../Scripts/Module/Common/Area.js" type="text/javascript"></script>
<div style="width: 100%;">
    <table cellpadding="0" cellspacing="0" bordercolor="#434343" class="tableStyle" style="text-align: left">
        <tr>
            <td>
                <select id="sltProvince" runat="server" class="selectWidth" style="width: 120px">
                </select>省
            </td>
            <td>
                <select id="sltCity" runat="server" class="selectWidth" style="width: 120px">
                    <option value="">=== 请选择 ===</option>
                </select>市
            </td>
            <td>
                <select id="sltCounty" runat="server" class="selectWidth" style="width: 120px">
                    <option value="">=== 请选择 ===</option>
                </select>(区/县)
            </td>
            <td>
                <select id="sltVillage" runat="server" class="selectWidth" style="width: 120px">
                    <option value="">=== 请选择 ===</option>
                </select>(乡/镇)
            </td>
        </tr>
    </table>
</div>
