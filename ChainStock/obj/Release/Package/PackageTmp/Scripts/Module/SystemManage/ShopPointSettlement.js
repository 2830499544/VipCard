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
             'GetShopPointSettlement', { "ID": id }, "json",
                 function (json) {
                     $("#lbShopName").html(shopname);
                     $("#MyID").val(json.ID);
                     $("#lblRechargePoint").html(parseFloat(json.RechargePoint).toFixed(2));
                     $("#lblDeductionPoint").html(parseFloat(json.DeductionPoint).toFixed(2));
                     $("#lblGivePoint").html(parseFloat(json.GivePoint).toFixed(2));
                     $("#lblReturnPoint").html(json.ReturnPoint);
                     $("#lblFanliPoint").html(json.FanliPoint);
                     $("#lblReturnOrderPoint").html(parseFloat(json.ReturnOrderPoint).toFixed(2));
                     $("#lblDrawPoint").html(parseFloat(json.DrawPoint).toFixed(2));
                     $("#lbStartTime").html(json.StartTime);
                     $("#lbEndTime").html(json.EndTime);
                     $("#txtRemark").val(json.Remark);
                   
                 });
    $("#txtRemark").focus();
}

function btSave() {
    doAjax("../",
             'SetShopPointSettlement',
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
                                close: function () {
                                    var type = $("#txtShopType").val();
                                    var strtitle = "";
                                    if (type == "2") {
                                        location.href = "AlliancePointSettlement.aspx?PID=171"; 
                                    }
                                    if (type == "3") {
                                        location.href = "ShopPointSettlement.aspx?PID=170"; 
                                    }
                                 },
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
             'GetShopPointSettlement', { "ID": id }, "json",
                 function (json) {
                     $("#lbShopName").html(shopname);
                     $("#MyID").val(json.ID);
                     $("#lblRechargePoint").html(parseFloat(json.RechargePoint).toFixed(2));
                     $("#lblDeductionPoint").html(parseFloat(json.DeductionPoint).toFixed(2));
                     $("#lblGivePoint").html(parseFloat(json.GivePoint).toFixed(2));
                     $("#lblReturnPoint").html(json.ReturnPoint);
                     $("#lblFanliPoint").html(json.FanliPoint);
                     $("#lblReturnOrderPoint").html(parseFloat(json.ReturnOrderPoint).toFixed(2));
                     $("#lblDrawPoint").html(parseFloat(json.DrawPoint).toFixed(2));
                     $("#lbStartTime").html(json.StartTime);
                     $("#lbEndTime").html(json.EndTime);
                     $("#txtRemark").val(json.Remark);
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
    $("#lblRechargePoint").html("");
    $("#lblDeductionPoint").html("");
    $("#lblGivePoint").html("");
    $("#lblReturnPoint").html("");
    $("#lblFanliPoint").html("");
    $("#lblReturnOrderPoint").html("");
    $("#lblDrawPoint").html("");
    $("#lbStartTime").html("");
    $("#lbEndTime").html("");
    $("#txtRemark").val("");
}