$(document).ready(function () {
    //保存按钮响应函数
    $("#BtnSpecialSave").bind("click", BtnSpecialSave);
    //重置按钮响应函数
    $("#BtnSpecialReset").bind("click", BtnSpecialReset);

    $('#txtStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', isShowClear: true, readOnly: true });
    });
    $('#txtEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', isShowClear: true, readOnly: true });
    });

    $('input:radio[name="rd_special"]').change(function () {
        var val = $('input:radio[name="rd_special"]:checked').val();      
        switch (val) {
            case "Date":
                $("#trDate").css("display", "");              
                $("#trWeek").css("display", "none");
                $("#trMonth").css("display", "none");
                break;
            case "Week":
                $("#trDate").css("display", "none");
                $("#trWeek").css("display", "");
                $("#trMonth").css("display", "none");
                break;
            case "Month":
                $("#trDate").css("display", "none");
                $("#trWeek").css("display", "none");
                $("#trMonth").css("display", "");
                break;
            case "Birthday":
                $("#trDate").css("display", "none");   
                $("#trWeek").css("display", "none");
                $("#trMonth").css("display", "none");
                break;
        }
    });

})


function BtnSpecialSave() {

    var txtSpecialName = $("#txtSpecialName").val()
    if (txtSpecialName == "") {

        strErrorMsg += "<li>活动名称不能为空</li>";
    }

    if (typeof ($("#txtMoney").val()) != "undefined") {
        if ($("#txtMoney").val() != "") {
            if (!$("#txtMoney").val().IsDecimal()) {
                strErrorMsg += "<li>请输入正确的数字，表示“充值金额”;</li>";
            }
        }
    }
    if (typeof ($("#txtGiveMoney").val()) != "undefined") {
        if ($("#txtGiveMoney").val() != "") {
            if (!$("#txtGiveMoney").val().IsDecimal()) {
                strErrorMsg += "<li>请输入正确的数字，表示“赠送金额”;</li>";
            }
        }
    }

    var strErrorMsg = "";
    var type = ($("#txtSpecialID").val() == "") ? "SpecialAdd" : "SpecialEdit";
    var val = $('input:radio[name="rd_special"]:checked').val();
    //alert(val);
    art.dialog({
        title: '系统提示',
        content: "将" + (type == "SpecialAdd" ? "增加" : "编辑") + "优惠活动 , \n确定操作吗？ ",
        lock: true,
        ok: function () {
            this.close();

            doAjax(
        "../",
        type,
            //$("#frmSpecial").serialize(),
        {
        "txtSpecialID": $("#txtSpecialID").val(),
        "txtSpecialName": $("#txtSpecialName").val(),
        "txtMoney": $("#txtMoney").val(),
        "txtGiveMoney": $("#txtGiveMoney").val(),
        "txtSpecialRemark": $("#txtSpecialRemark").val(),
        "SpecialUSerID": $("#SpecialUSerID").val(),
        "Type": val,
        "txtStartTime": $("#txtStartTime").val(),
        "txtEndTime": $("#txtEndTime").val(),
        "txtWeek": $("#txtWeek").val(),
        "txtMonth": $("#txtMonth").val()
    },
        "json",
         function (json) {

             switch (json) {
                 case '0':
                     art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统异常，未保存数据，请再次点击保存！"),
                                     lock: true
                                 });
                     break;
                 case '-1':
                     art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统错误，请联系管理员！"),
                                     lock: true
                                 });
                     break;
                 default:
                     art.dialog
                            ({
                                title: '系统提示',
                                time: 0.5,
                                content: '保存成功',
                                close:
                                function () {
                                    window.location.href = "../SystemManage/SpecialList.aspx";
                                },
                                lock: true
                            });
             }
         });
            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    });
}


function BtnSpecialReset() {

    $("#txtSpecialName").val("");
    $("#txtMoney").val(0);
    $("#txtGiveMoney").val(0);
    $("#txtSpecialRemark").val("");
    

}