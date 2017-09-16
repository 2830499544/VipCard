<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuickSearch.ascx.cs"
    Inherits="ChainStock.Controls.QuickSearch" %>
<script type="text/javascript">
    $(document).ready(function () {

        $(".sltAllianceID").bind("change", ShopListBind);
        $(".sltShopID").bind("change", GetShopInfo);
        $("#sltShop").attr("style", "display:none").after($("#SearchShop"));
        $("#btnShop").bind("click", CreateQuickSearchList);
        $("#btnSearch").bind("click", GetShop);
        $("#btnSearchAll").bind("click", function () {
            $("#txtShop").val("");
            $("#sltShop").get(0).selectedIndex = 0;
            quickSearch.close();
        });


        if ($(".sltAllianceID").find("option:selected").val() == "") {
            $("#txtAlliance").val("");

        }
        else {

            $("#txtAlliance").val($(".sltAllianceID").find("option:selected").text());

            ShopListBind();
            if ($("#sltShop").find("option:selected").val() == "") {
            
                $("#txtShop").val("");
                $("#txtQueryShop").val("");

            }
            else if ($("#sltShop").find("option:selected").val() != $(".sltAllianceID").find("option:selected").val()) {
             
                $("#txtShop").val($("#sltShop").find("option:selected").text());
                $(".sltShopID").val($("#sltShop").find("option:selected").val());
            }

        }
       
    });
    function GetShopInfo() {
        var shopid = $(".sltShopID").val();
        var shopname = $(".sltShopID").find("option:selected").text();
      
      
            $("#txtShop").val(shopname);
       
       $("#sltShop").val(shopid);

    

    }
    function ShopListBind() {
    

        var fid = -1;
        if ( $(".sltAllianceID").val() != "") {
            fid = $(".sltAllianceID").val();
        }
  

        GetShopList(fid);
      
    }
    function GetShopList(fid) {

      
        $(".sltShopID").empty();
        $(".sltShopID").append("<option value=''>=== 请选择 ===</option>");
        doAjax("../",
        "GetShopList",
        { "fid": fid, "shopid": $(".txtShopID").val() },
        "json",
        function (json) {
           
           
            if (json != "") {
                for (var i = 0; i < json.length; i++) {
                    $(".sltShopID").append("<option value='" + json[i].ShopID + "'>" + json[i].ShopName + "</option>");
                }
            }
        },
        false
       );
    }
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
<table>
<tr>

<td> 
 <select id="sltAlliance" runat="server" class="sltAllianceID"  style="   width: 100px;    height: 25px;       outline: none;    resize: none;">     </select>
  
</td>
<td>

 <select id="sltShop" runat="server" class="sltShopID"   style="  width: 100px;    height: 25px;       outline: none;    resize: none;">
<%-- <option value="" selected="selected">=== 请选择 ===</option>--%>
     </select>
</td>
<td>
  <input id="txtShop" type="text" class="border_radius" readonly="readonly"   style="display:none;"/>
    <input id="txtAlliance" type="text" class="border_radius" readonly="readonly"   style="display:none;"/>
    <input id="btnShop" type="button" value="选择" class="common_Button" style=" display:none;" />
      <input id="txtShopType" type="hidden" runat="server" />
         <input id="txtShopID" class="txtShopID" type="hidden" runat="server" />
</td>
</tr>
</table>



 
     
      
    
   

    </span>
    

<div id="divQuickSearch" style="display: none">
    <table class="table-style table-hover user_List_txt">
        <thead class="thead">
            <tr class="th">
                <th>
                    商家名称
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
