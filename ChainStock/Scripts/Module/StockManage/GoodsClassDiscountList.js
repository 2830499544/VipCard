$(document).ready(function () {
    $("#btnSetGoodsClassDiscount").bind("click", btnSetGoodsClassDiscountClick);
})
function EditGoodsClassLevelDiscount(ClassDiscountID) {
    art.dialog({
        title: '编辑会员等级',
        content: document.getElementById('divGoodsClassLevelDiscount'),
        id: 'divGoodsClassLevelDiscount',
        lock: true,
        close: function () {
            $("#spClassName").text("");
            $("#spLevelPoint").text("");
            $("#txtClassDiscountPercent").val("");
            $("#txtClassPointPercent").val("");
            $("#txtGoodsClassDiscountID").val("");
        }
    });
    doAjax("../", "GetGoodsClassDiscount", { "ClassDiscountID": ClassDiscountID }, "json", function (json) {
        $("#spClassName").text(json[0].ClassName);
        $("#spLevelPoint").text(json[0].LevelPoint);
        $("#txtClassDiscountPercent").val(accMul(json[0].ClassDiscountPercent, 100));
        $("#txtClassPointPercent").val(parseFloat(accDiv(1, json[0].ClassPointPercent)).toFixed(0));
        $("#txtGoodsClassDiscountID").val(json[0].ClassDiscountID);
    })
}

function btnSetGoodsClassDiscountClick() {
    var strErrorMsg = "";

    var strClassDiscountPercent = $.trim($("#txtClassDiscountPercent").val());
    if (strClassDiscountPercent.IsEmpty()) {
        strErrorMsg += "<li>必须输入分类等级折扣率;</li>";
    }
    else {
        if (!strClassDiscountPercent.IsNumber() || parseInt(strClassDiscountPercent) > 100 || parseInt(strClassDiscountPercent) <= 0) {
            strErrorMsg += "<li>等级折扣输入错误，必须为1-100之间的正整数</li>";
        }
    }

    var strClassPointPercent = $.trim($("#txtClassPointPercent").val());
    if (strClassPointPercent.IsEmpty()) {
        strErrorMsg += "<li>必须输入积分比率;</li>";
    }
    else {
        if (!strClassPointPercent.IsNumber() || parseInt(strClassPointPercent) < 0) {
            strErrorMsg += "<li>积分比率输入错误，必须为大于等于0的正整数;</li>";
        }
    }

    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,
            icon: 'error',
            lock: true
        });
        return false;
    }
    var strGoodsClassDiscountID = $("#txtGoodsClassDiscountID").val();
    var location = window.location.href.replace("#", "");
    art.dialog({
        title: '系统提示',
        content: "将编辑商品分类 [" + $("#spClassName").text() + "]。\n确定操作吗？",
        lock: true,
        ok: function (json) {
            this.close();
            doAjax("../", "UpdateGoodsClassDiscount", {
                "ClassDiscountID": strGoodsClassDiscountID,
                "ClassDiscountPercent": strClassDiscountPercent,
                "ClassPointPercent": strClassPointPercent
            }, "json", function (json) {
                if (json == "-1") {
                    art.dialog({
                        title: '系统提示',
                        time: 4,
                        content: ("系统错误 请与系统管理员联系！"),
                        lock: true
                    });
                } else {
                    art.dialog({
                        title: '系统提示',
                        time: 0.5,
                        content: '保存成功！',
                        close: function () { window.location = location; },
                        lock: true
                    });
                }
            })
        },
        cancelVal: '取消',
        cancel: true
    })
}