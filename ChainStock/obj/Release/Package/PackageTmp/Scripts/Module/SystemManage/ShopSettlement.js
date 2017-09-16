$(document).ready(function () {
    BindNullList("gvShopSettlement", false);
    $("#btSave").bind("click", btSave);
    $("#btCancel").bind("click", btCancel);   
});

function ShopSettlement(shopname, id) {
    $("#btoperate").css("display", "");
    $("#txtRemark").attr("readonly", false);
    var type = $("#txtShopType").val();
    var strtitle = "";
    if (type == "2") {
        strtitle = "联盟商";
    }
    if (type == "3") {
        strtitle = "商家";
    }
    art.dialog({
        title: strtitle+'结算-' + shopname,
        content: document.getElementById('EShopSettlement'),
        id: 'D345',
        lock: true,
        close: function () { ShopReset(); }
    });
    doAjax("../",
             'GetShopSettlement', { "ID": id }, "json",
                 function (json) {
                     $("#lbShopName").html(shopname);
                     $("#MyID").val(json.ID);
                     $("#lbRechargeMoney").html(parseFloat(json.RechargeMoney).toFixed(2));
                     $("#lbRechargeProportion").html(parseFloat(json.RechargeProportion).toFixed(2));
                     $("#lbRechargeMoneySum").html(parseFloat(json.RhgPropMoney).toFixed(2));
                     $("#lbDrawMoney").html(parseFloat(json.DrawMoney).toFixed(2));
                     $("#lbPayCard").html(parseFloat(json.PayCard).toFixed(2));
                     $("#lbStartTime").html(json.StartTime);
                     $("#lbEndTime").html(json.EndTime);
                     $("#lbAllExpenseMoney").html(parseFloat(json.AllExpenseMoney).toFixed(2));
                     $("#lbProportionMoney").html(parseFloat(json.ProportionMoney).toFixed(2));
                     $("#lbPointDrawMoney").html(parseFloat(json.PointDrawMoney).toFixed(2));
                     $("#lbPointAmount").html(parseFloat(json.PointAmount).toFixed(2));
                 });
    $("#txtRemark").focus();
}

function btSave() {
    doAjax("../",
             'SetShopSettlement',
             {
                 "ID": $("#MyID").val(),
                 "Remark": $("#txtRemark").val()
             },
              "json",
            function (json) {
                switch (json) {
                    case 0:
                        art.dialog
                            ({
                                time: 4,
                                title: '系统提示',
                                content: ("系统异常 未保存，请重试！"),
                                lock: true
                            });
                        break;
                    default:
                        art.dialog
                            ({
                                title: '系统提示',
                                time: 1,
                                content: '保存成功！',
                                close: function () { location.href = "ShopSettlement.aspx?PID=135"; },
                                lock: true
                            });
                        break;
                }
            });
}

function ShopSettlementLook(shopname, id) {
    var type = $("#txtShopType").val();
    var strtitle = "";
    if (type == "2") {
        strtitle = "联盟商";
    }
    if (type == "3") {
        strtitle = "商家";
    }
    art.dialog({
        title: strtitle+'结算查看',
        content: document.getElementById('EShopSettlement'),
        id: 'D345',
        lock: true,
        close: function () { ShopReset(); }
    });
    doAjax("../",
             'GetShopSettlement', { "ID": id }, "json",
                 function (json) {
                     $("#lbShopName").html(shopname);
                     $("#MyID").val(json.ID);
                     $("#lbRechargeMoney").html(parseFloat(json.RechargeMoney).toFixed(2));
                     $("#lbDrawMoney").html(parseFloat(json.DrawMoney).toFixed(2));
                     $("#lbPayCard").html(parseFloat(json.PayCard).toFixed(2));
                     $("#lbStartTime").html(json.StartTime);
                     $("#lbEndTime").html(json.EndTime);
                     $("#txtRemark").val(json.Remark);
                     $("#lbAllExpenseMoney").html(parseFloat(json.AllExpenseMoney).toFixed(2));
                     $("#lbProportionMoney").html(parseFloat(json.ProportionMoney).toFixed(2));
                     $("#lbPointDrawMoney").html(parseFloat(json.PointDrawMoney).toFixed(2));
                     $("#lbPointAmount").html(parseFloat(json.PointAmount).toFixed(2));
                 });
    $("#btoperate").css("display", "none");
    $("#txtRemark").attr("readonly", true);
}

function btCancel() {
    ShopReset();
    art.dialog({ id: 'D345' }).close();
}

function ShopReset() {
    $("#lbShopName").html("");
    $("#MyID").val("");
    $("#lbRechargeMoney").html("");
    $("#lbDrawMoney").html("");
    $("#lbPointDrawMoney").html("");
    $("#lbPointAmount").html("");
    $("#lbPayCard").html("");
    $("#lbStartTime").html("");
    $("#lbEndTime").html("");
    $("#txtRemark").val("");
    $("#lbProportionMoney").html("");
    $("#lbAllExpenseMoney").html("");
}