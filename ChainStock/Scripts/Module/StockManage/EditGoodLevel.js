$(document).ready(function () {

})

//选择所有商家
function SelectAllShop(){
    $("#chkSyncPartialShop").attr("checked", false);
}

//选择同步的商家
function SelectPartailShop() {
    $("#chkSyncOtherShop").attr("checked", false);
    if($("#chkSyncPartialShop").attr("checked")==true){
        var dialogSelectShop = art.dialog({
            title: '选择同步到哪些商家',
            content: document.getElementById('divSyncShopSelectPanel'),
            id: 'divSyncShopSelectPanel',
            lock: true,
        });

        $("#btnShareShopOK").bind("click",function(){
            dialogSelectShop.close();
        });
    }
}

//新增分类保存
function btnClassSave() {
    $("#txtClassName").focus();
    var strErrorMsg = "";
    if ($("#txtClassName").val() == "") {
        strErrorMsg += "类别名称不能为空;";
    }
    if ($("#sltShop").val() == "") {
        strErrorMsg += "所属商家不能为空;";
    }
    $.each($("input[gm='txtMyDiscountPercent']"), function (i, item) {
        if (item.value.IsEmpty()) {
            strErrorMsg += "<li>" + $(item).attr("myname").toString() + "-必须输入等级所需积分;</li>";
        }
        else {
            if (!item.value.IsNumber() || parseInt(item.value) > 100 || parseInt(item.value) <= 0) {
                strErrorMsg += "<li>" + $(item).attr("myname").toString() + "-等级折扣输入错误，必须为1-100之间的正整数</li>";
            }
        }
    });
    $.each($("input[gm='txtMyPointPercent']"), function (i, item) {
        if (item.value.IsEmpty()) {
            strErrorMsg += "<li>" + $(item).attr("myname").toString() + "-必须输入等级折扣率;</li>";
        }
        else {
            if (!item.value.IsNumber() || parseInt(item.value) < 0) {
                strErrorMsg += "<li>" + $(item).attr("myname").toString() + "-积分比率输入错误，必须为大于等于0的正整数;</li>";
            }
        }
    });
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        //var throughBox = art.dialog.through;
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    return true;
}