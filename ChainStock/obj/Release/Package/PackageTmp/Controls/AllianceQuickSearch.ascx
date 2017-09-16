<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AllianceQuickSearch.ascx.cs"
    Inherits="ChainStock.Controls.AllianceQuickSearch" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#sltShop").attr("style", "display:none").after($("#SearchShop"));
        $("#btnShop").bind("click", CreateQuickSearchList);
        $("#btnSearch").bind("click", GetShop);
        $("#btnSearchAll").bind("click", function () {
            $("#txtShop").val("");
            $("#sltShop").get(0).selectedIndex = 0;
            quickSearch.close();
        });
 
        if ($("#sltShop").find("option:selected").val() == "") {
            $("#txtShop").val("");
            $("#txtQueryShop").val("");
        }
        else {
            $("#txtShop").val($("#sltShop").find("option:selected").text());
        }
    });
    //创建表格
    function CreateQuickSearchList() {
        $("#txtShop").val("");
        $("#txtQueryMem").focus();
        quickSearch = art.dialog({
            title: '快速查找',
            content: document.getElementById('divQuickSearch'),
            id: 'divQuickSearch',
            lock: true,
            close: function () {
                $set = $("#sltShop").find("option:selected");
                if ($set.attr("index") > 0) {
                    $("#txtShop").val($set.text());
                }
            }
        });
        GetShop();

    }

    function GetShop() {
        var html = "";
        var SearchShopName = $("#txtQueryShop").val();
        $('#sltShop option').each(function (index, item) {
            var $option = $(this);
            if (index > 0) {
                if (SearchShopName != "") {
                    if ($option.html().indexOf(SearchShopName) >= 0) {
                        html += "<tr class=\"td\" ondblclick=\"javascript:QuickSelectShop(\'" + $option.html() + "\','" + index + "');\">" + '<td style=" width:120px;text-align: left">' + $option.html() + '</td>' + '</tr>';
                    }
                }
                else {
                    html += "<tr class=\"td\" ondblclick=\"javascript:QuickSelectShop(\'" + $option.html() + "\','" + index + "');\">" + '<td style=" width:120px;text-align: left">' + $option.html() + '</td>' + '</tr>';
                       +'<td style=" width:120px;text-align: left">' + $option.val() + '</td>'
                 + '</tr>';
                }
            }
        });
        $("#tbQuickSearch").html(html);
    }
    function QuickSelectShop(ShopName, selectIndex) {
        $("#txtShop").val(ShopName);

        $("#HDsltshop").val(ShopName);

        $("#sltShop").get(0).selectedIndex = selectIndex;
        quickSearch.close();
        $("#txtQueryShop").val("");
    }
 
</script>
<span id="SearchShop">
    <input id="txtShop" type="text" class="border_radius" readonly="readonly" />
    <input id="btnShop" type="button" value="选择" class="common_Button" /></span>
<div id="divQuickSearch" style="display: none">
    <table class="table-style table-hover user_List_txt">
        <thead class="thead">
            <tr class="th">
                <th>
                    联盟商名称
                </th>
            </tr>
        </thead>
    </table>
    <div style="height: 260px; width: 600px; overflow: auto;">
        <table class="table-style table-hover user_List_txt" id="tbQuickSearch">
        </table>
    </div>
    <div style="height: 30px; line-height: 30px; text-align: center;">
        <input type="text" id="txtQueryShop" class="border_radius" style="clear: both; float: none" />
        <input type="button" id="btnSearch" class="common_Button common_ServiceButton" value="查找" />
        <input type="button" id="btnSearchAll" class="common_Button common_ServiceButton"
            value="全部" />
    </div>
</div>
