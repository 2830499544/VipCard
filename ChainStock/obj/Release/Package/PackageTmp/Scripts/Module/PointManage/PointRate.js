/// <reference path="../../jquery-1.4.1.min.js" />
//页面加载时执行
//method


$(document).ready(function () {
    if (!$("#chkPointLevel").attr("checked")) {
        $("input").attr("disabled", "disabled");
        $("select").attr("disabled", "disabled");
        $("#frmPointRate").attr("disabled", "disabled");
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: "未启用多级会员积分提成功能，不能进行设置！",
            lock: true
        });
        return false;

    }
    else {
        //重置按钮响应函数
        $("#btnpointRateReset").bind("click", BtnPointRateReset);
        //保存按钮响应函数
        $("#btnpointRateSave").bind("click", BtnPointRateSave);
        GetPointRate();

    }
    $("#rdoPercent").click(function () {
        $("input[type='text']").val(0);
        $("span[class!='lblFrmTitle']").html("%");

    });
    $("#rdoNum").click(function () {
        $("input[type='text']").val(0);
        $("span[class!='lblFrmTitle']").html("");
    });
});


function GetPointRate() {
    doAjax(
            "../",
            "GetPointRate", {},
            "json",
            function (json) {
                if (json.PointRateType == 'True') {
                    $("#rdoPercent").attr("checked", "checked");
                    $("#rdoPercent").click();
                } else {
                    $("#rdoNum").attr("checked", "checked");
                    $("#rdoNum").click();
                }
                $("#txtPointRateLevel1").val(json.MemLevel1);
                $("#txtPointRateLevel2").val(json.MemLevel2);
                $("#txtPointRateLevel3").val(json.MemLevel3);
                $("#txtPointRateLevel4").val(json.MemLevel4);
                $("#txtPointRateLevel5").val(json.MemLevel5);
                $("#txtPointRateLevel6").val(json.MemLevel6);
                $("#txtPointRateLevel7").val(json.MemLevel7);
                $("#txtPointRateLevel8").val(json.MemLevel8);
                $("#txtPointRateLevel9").val(json.MemLevel9);
                $("#txtPointRateLevel10").val(json.MemLevel10);
                $("#txtPointRateLevel11").val(json.MemLevel11);
                $("#txtPointRateLevel12").val(json.MemLevel12);
                $("#txtPointRateLevel13").val(json.MemLevel13);
                $("#txtPointRateLevel14").val(json.MemLevel14);
                $("#txtPointRateLevel15").val(json.MemLevel15);

                $("#opt" + json.PointRateLevel).attr("selected", "selected");
                //找到提成等级后，禁用提成等级后的提成输入框。
                forbiddenInpLev(json.PointRateLevel);
            },
            false
    );
}

/*******************************************************************************
*重置按钮响应函数
*******************************************************************************/
function BtnPointRateReset() {
    GetPointRate();
}

/*******************************************************************************
*保存按钮响应函数
*******************************************************************************/
function BtnPointRateSave() {
    var strErrorMsg = "";
    if ($("#sltPointRateLevel").val() == -1) {
        strErrorMsg += "<li>请选择提成级数</li>";
    }
    if ($("#rdoPercent").attr("checked")) {
        $("input[type='text']").each(function (i, val) {
            if (!this.disabled) {
                if (!this.value.IsNumber() || this.value == 0 || this.value > 100) {
                    strErrorMsg += "<li>请输入1-100的之间的数字、表示第[" + (i + 1) + "]级提成</li>";
                }
            }
        });
    }
    else {
        $("input[type='text']").each(function (i, val) {
            if (!this.disabled) {
                if (!this.value.IsNumber() || this.value == 0 || this.value > 10000) {
                    strErrorMsg += "<li>请输入1-10000的之间的数字、表示第[" + (i + 1) + "]级提成</li>";
                }
            }
        });

    }
    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误、请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    doAjax(
        "../",
        'PointRateEdit',
        {
            "PointRateID": 0,
            "PointRateType": $("#rdoPercent").attr("checked"),
            "PointRateLevel": $("#sltPointRateLevel").val(),
            "MemLevel1": $("#txtPointRateLevel1").val(),
            "MemLevel2": $("#txtPointRateLevel2").val(),
            "MemLevel3": $("#txtPointRateLevel3").val(),
            "MemLevel4": $("#txtPointRateLevel4").val(),
            "MemLevel5": $("#txtPointRateLevel5").val(),
            "MemLevel6": $("#txtPointRateLevel6").val(),
            "MemLevel7": $("#txtPointRateLevel7").val(),
            "MemLevel8": $("#txtPointRateLevel8").val(),
            "MemLevel9": $("#txtPointRateLevel9").val(),
            "MemLevel10": $("#txtPointRateLevel10").val(),
            "MemLevel11": $("#txtPointRateLevel11").val(),
            "MemLevel12": $("#txtPointRateLevel12").val(),
            "MemLevel13": $("#txtPointRateLevel13").val(),
            "MemLevel14": $("#txtPointRateLevel14").val(),
            "MemLevel15": $("#txtPointRateLevel15").val()
        },
        "json",
        function (json) {
            if (json == "1") {
                art.dialog({ title: '系统提示', time: 2, content: '设置成功！' });

            } else {
                art.dialog({ title: '系统提示', time: 2, content: '系统错误，请重试！' });
                lock: true;
            }
        }
    )
}

/*******************************************************************************
*禁用，解禁文本输入框
*******************************************************************************/
var hid = 0;
function forbiddenInpLev(obj) {
    for (var i = obj; i < 15; i++) {
        var j = i * 1 + 1;
        $("#txtPointRateLevel" + j).val(0);
        $("#txtPointRateLevel" + j).attr("disabled", "disabled");
        $("#td" + j).css("display", "none");
        $("#tdd" + j).css("display", "none");
    }
    $("#apptd" + hid).remove();
    $("#apptdd" + hid).remove();
    if (obj != 0) {
        if (obj % 2 == 1) {
            hid = accDiv((obj * 1 + 1), 2);
            $("#tr" + hid).append("<td id='apptd" + hid + "' class='tableStyle_left' ></td><td id='apptdd" + hid + "' class='tableStyle_right' ></td>")
        }
    }
    for (var i = 1; i <= obj; i++) {
        $("#txtPointRateLevel" + i).attr("disabled", "");
        $("#td" + i).css("display", "");
        $("#tdd" + i).css("display", "");
    }
}

/*******************************************************************************
*下拉列表改变函数
*******************************************************************************/
function sltChange(obj) {
    forbiddenInpLev($(obj).val());
}

/*******************************************************************************
*清零禁用文本框
*******************************************************************************/
function CleartTbx(obj) {
    for (var i = obj; i < 15; i++) {
        var j = i * 1 + 1;
        $("#txtPointRateLevel" + j).val("0");
        $("#txtPointRateLevel" + j).attr("disabled", "disabled");
    }
}


/***********************************************************
*展开详情
arg2为 是否展开的条件参数
************************************************************/
function ShowPointDetail(id) {
    if ($("#img" + id).css("display") != "none") {
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

function IsShowPic(id) {
    doAjax("../",
            "GetPointIsShow",
            {
                "id": id
            },
            "json",
            function (json) {
                if (json == 1) {
                    $("#img" + id).css("display", "");
                }
                else {
                    $("#img" + id).css("display", "none");
                    $("#a" + id).css("padding-left", "22px");
                }

            });

}
