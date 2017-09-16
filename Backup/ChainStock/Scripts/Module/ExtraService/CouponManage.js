
$(document).ready(function () {
    //按钮响应函数
    $("#btnCouponAdd").bind("click", BtnCoupon);
    $("#btnCouponSave").bind("click", BtnCouponSave);
    $('#selCouponType').bind("change", setSpNumber);
    $("#btnCouponReset").bind("click", btnCouponReset);
    $("input[name='radCouponYesOrNo']:radio").bind("change", radCouponValidity);
  

    $('#txtEndTime').bind("focus", function () {
        WdatePicker({ skin: 'ext', minDate: '#F{$dp.$D(\'txtStartTime\')}', isShowClear: true, readOnly: true });

    });
    $('#txtStartTime').bind("focus", function () {
        var EndTime = $dp.$('txtEndTime');
        WdatePicker({ skin: 'ext', minDate: '%y-%M-%d', isShowClear: true, readOnly: true, onpicked: function () { EndTime.focus(); } });
    });
    //绑定日期控件
    $('#txtSendStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm', maxDate: '%y-%M-%d %H:%m:%s', isShowClear: true, readOnly: true });
    });
    $('#txtSendEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm', maxDate: '%y-%M-%d %H:%m:%s', isShowClear: true, readOnly: true });
    });
    $('#txtUseStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm', maxDate: '%y-%M-%d %H:%m:%s', isShowClear: true, readOnly: true });
    });
    $('#txtUseEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd HH:mm', maxDate: '%y-%M-%d %H:%m:%s', isShowClear: true, readOnly: true });
    });

});
function BtnCoupon() {
    $("#txtCouponContent").val("尊敬的会员,您获得{商家}优惠券{优惠券},券号{券号}，有效期至{有效期}，使用条件:需最低消费{最低消费}元。详情咨询:{店面电话}");
    art.dialog({
        title: '制作电子优惠券',
        content: document.getElementById('CouponInfo'),
        id: 'divAddOrEditCoupon',
        lock: true,
        close: function () {
            btnCouponReset();
        }
    });
}

function BtnCouponSave() {
    var strErrorMsg = "";
    var CouponTitle = $.trim($("#txtCouponTitle").val());
    var CouponType = $("#selCouponType").val();
    var CouponNumber = $("#txtCouponNumber").val();
    var CouponPredictNu = $("#txtCouponPredictNu").val();
    var CouponMinMoney = $("#txtCouponMinMoney").val();
    var CouponDayNum = $("#txtCouponDayNum").val();
    var CouponYF = $("#txtCouponYF").val();
    var CouponSY = $("#txtCouponSY").val();
    var CouponContent = $.trim($("#txtCouponContent").val());
    var spCouponID = $("#spCouponID").html();
    var CouPonYX = $('input[name=radCouponYesOrNo]:checked').val();
    var isGet = $('input[name=radGetYesOrNo]:checked').val();
    var type = (spCouponID == 0) ? "CouponAdd" : "CouponEdit";
    if (CouponTitle == "") strErrorMsg += "<li>请填写优惠券名称;</li>";
    switch ($('#selCouponType').val()) {
        case "0":
            if (!CouponPredictNu.IsNumber() || CouponNumber <= 0) { strErrorMsg += "<li>优惠金额必须大于0</li>"; }
            break;
        case "1":
            if (CouponNumber <= 0 || CouponNumber >= 10) { strErrorMsg += "<li>折扣比例必须在1-10之间</li>"; }
            break;
    }
    if (!CouponPredictNu.IsNumber() || parseInt(CouponPredictNu) <= 0) strErrorMsg += "<li>输入预发数量不正确,请重新输入!</li>";
    if (parseInt(CouponPredictNu) > 5000) strErrorMsg += "<li>预发数量不能超过5000!</li>";
 
    if ($("input[name='radCouponYesOrNo']:checked").val()>0) {
        
        if ($("#txtStartTime").val() == "" || $("#txtEndTime").val() == "") {
            strErrorMsg += "<li>请选择正确的优惠券有效期</li>";
        }
    }
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提醒',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }

    art.dialog({
        title: '系统提示',
        content: "将" + (type == "CouponAdd" ? "增加" : "修改") + "优惠券 [" + $.trim($("#txtCouponTitle").val()) + "]。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
     type,
     {
         "spCouponID": spCouponID,
         "CouponTitle": CouponTitle,
         "CouponType": CouponType,
         "CouponNumber": CouponNumber,
         "CouponPredictNu": CouponPredictNu,
         "CouponMinMoney": CouponMinMoney,
         "CouponDayNum": CouponDayNum,
         "CouponYF": CouponYF,
         "CouponSY": CouponSY,
         "CouPonYX": CouPonYX,
         "CouPonStart": $("#txtStartTime").val(),
         "CouPonEnd": $("#txtEndTime").val(),
         "CouponContent": CouponContent,
         "isGet": isGet
     },
      "json",
      function (json) {
          switch (json) {
              case 0:
                  art.dialog({
                      title: '系统提示',
                      time: 4,
                      content: ("系统异常，未保存数据，请再次点击保存！"),
                      lock: true
                  });
                  break;
              case -1:
                  art.dialog({
                      title: '系统提示',
                      time: 4,
                      content: ("系统错误，请与系统管理员联系！"),
                      lock: true
                  });
                  break;
              default:
                  art.dialog({
                      title: '系统提示',
                      time: 0.5,
                      content: '保存成功！',
                      close: function () { location.href = "CouponManage.aspx?PID=70"; },
                      lock: true
                  });
                  break;
          }
      });
        },
        cancelVal: '取消',
        cancel: true
    });
}

function setSpNumber() {
    switch ($('#selCouponType').val()) {
        case "0":
            $("#spNumber").html('优惠金额：');
            break;
        case "1":
            $("#spNumber").html('折扣比例：');
            break;
    }
}

function radCouponValidity() {
    if ($('input[name=radCouponYesOrNo]:checked').val() == "0") {
        $("#txtStartTime").val("");
        $("#txtEndTime").val("");
    }
    else {
        $("#txtStartTime").val("");
        $("#txtEndTime").val("");
    }
}

function btnCouponReset() {
    $("#txtCouponTitle").val("");
    $('#selCouponType')[0].selectedIndex = 0;
    $("#spNumber").html("优惠金额：");
    $("#txtCouponNumber").val("1");
    $("#txtCouponPredictNu").val("100");
    $("#txtCouponMinMoney").val("0");
    $("#txtCouponDayNum").val("0");
    $("#txtCouponYF").val("0");
    $("#txtCouponSY").val("0");
}