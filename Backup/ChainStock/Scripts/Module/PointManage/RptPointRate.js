
$(document).ready(function(){
    $("#txtShop").val($("#HDsltshop").val());
});

/***********************************************************
*展开详情
arg2为 是否展开的条件参数
************************************************************/
function ShowPointDetail(id, arg2) {
    if (arg2 > 0) {
        if ($("#data" + id).css("display") == "none") {
            $("#data" + id).css("display", "");
            $("#img" + id).attr("src", "../Inc/Style/images/minus.gif")
        }
        else {
            $("#data" + id).css("display", "none");
            $("#img" + id).attr("src", "../Inc/Style/images/plus.gif")
        }
    }
    else {
        $("#data" + id).css("display", "none");
    }
}

function IsShowPic(id, arg2) {
    if (arg2 > 0) {
        $("#img" + id).css("display", "");
    }
    else {
        $("#img" + id).css("display", "none");
        $("#a" + id).css("padding-left", "22px");
    }
}
/***********************************************************
*展开详情
arg2为 是否展开的条件参数    
************************************************************/
function sltShopBind() {
    alert(sltbin);
    var sltbin = $("$sltShop").val();
    $("$HDsltshop").val(sltbin);
    alert($("$HDsltshop").val(sltbin));
}
