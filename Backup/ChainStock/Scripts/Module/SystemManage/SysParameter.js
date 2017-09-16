$(document).ready(function () {

    AutoPrint()
    $("#chkAutoPrint").bind("click", AutoPrint);
    //统一打印小票标题和脚注
    $("#chkAccordPrint").bind("click", AccordPrint);
    $("#chkStaff").bind("click", chkStaff);




    //感应式IC卡初始化 按钮响应函数
    $("#btnSenseICCardInit").bind("click", btnSenseICCardInit)
    //接触式IC卡初始化 按钮响应函数
    $("#btnContactICCardInit").bind("click", btnContactICCardInit);
    //感应式IC卡复选框点击事件     感应式IC卡和接触式IC卡只能二选一或不选
    $("#chkSenseiccard").bind("click", chkSenseiccardClick);
    //接触式IC卡复选框点击事件     感应式IC卡和接触式IC卡只能二选一或不选
    $("#chkContacticcard").bind("click", chkContacticcardClick);

    //标准打印机  A4试纸     三者只可选择一种试纸
    $("#A4ShiZhi").bind("click", A4ShiZhi_Click);
    //三联打印机   打印试纸
    $("#SanLianShiZhi").bind("click", SanLianShiZhi_Click);
    //pos58打印机  打印试纸
    $("#POs58ShiZhi").bind("click", POs58ShiZhi_Click);
    $("#POs80ShiZhi").bind("click", POs80ShiZhi_Click);


    if (!$("#chkStaff").attr("checked")) {
        $('#rdStaff').attr("disabled", "disabled");
        $('#rdGoods').attr("disabled", "disabled");
    }
    if (!$("#chkAccordPrint").attr("checked")) {
        $('#txtPrintTitle').css("background-color", "#eee");
        $('#txtPrintTitle').attr("readonly", true);
        $('#txtPrintFootNote').css("background-color", "#eee");
        $('#txtPrintFootNote').attr("readonly", true);
    }
    var strEmailPwd = $("#lblEmailPwd").html();
    if (strEmailPwd != "") {
        $("#txtEmailPwd").val(strEmailPwd);
    }
    var strPartnerKey = $("#lblPartnerKey").html();
    if (strPartnerKey != "") {
        $("#txtPartnerKey").val(strPartnerKey);
    }




});

/*************************************************************************
*统一打印小票标题和脚注
*************************************************************************/
function AccordPrint() {
    if ($('#chkAccordPrint').attr("checked")) {
        $('#txtPrintTitle').css("background-color", "");
        $('#txtPrintTitle').attr("readonly", false);

        $('#txtPrintFootNote').css("background-color", "");
        $('#txtPrintFootNote').attr("readonly", false);
    }
    else {
        $('#txtPrintTitle').val("");
        $('#txtPrintTitle').css("background-color", "#eee");
        $('#txtPrintTitle').attr("readonly", true);

        $('#txtPrintFootNote').val("");
        $('#txtPrintFootNote').css("background-color", "#eee");
        $('#txtPrintFootNote').attr("readonly", true);
    }
}
/*************************************************************************
*启动员工提成响应事件
*************************************************************************/
function chkStaff() {
    if ($("#chkStaff").attr("checked")) {
        $('#rdStaff').attr("disabled", "");
        $('#rdGoods').attr("disabled", "");
    }
    else {
        $('#rdStaff').attr("disabled", "disabled");
        $('#rdGoods').attr("disabled", "disabled");
    }
}

function checkPayType() {
    if (!$("#chkPayCard").attr("checked") && !$("#chkPayCash").attr("checked") && !$("#chkPayBink").attr("checked") && !$("#chkPayCoupon").attr("checked")) {
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: '请选择付费找零方式，否则无法进行付费！',
            lock: true
        });
        return false;
    }
}

/******************************************************************
*不选择“启用系统邮件功能”时，不能选择“启用系统邮件(账户变动信息)自动发送功能”
******************************************************************/
function IsMoneyEmail() {
    if ($("#chkEmail").attr("checked") == false) {
        $('#chkMoneyEmail').attr("checked", false);
        $('#chkMoneyEmail').attr("disabled", true);

        $('#chkEnterpriseEmailEnableSSL').attr("checked", false);
        $('#chkEnterpriseEmailEnableSSL').attr("disabled", true);

        $('#chkEnterpriseEmailUseDefaultCredentials').attr("checked", false);
        $('#chkEnterpriseEmailUseDefaultCredentials').attr("disabled", true);

        $('#txtEmailAdress').attr("readonly", "readonly");
        $('#txtEmailPwd').attr("readonly", "readonly");
        $('#txtEmailSMTP').attr("readonly", "readonly");
        $('#txtEnterpriseEmailPort').attr("readonly", "readonly");
        $('#txtEnterpriseEmailDisplayName').attr("readonly", "readonly");

        $('#txtEmailAdress').val("");
        $('#txtEmailPwd').val("");
        $('#txtEmailSMTP').val("");
        $('#txtEnterpriseEmailPort').val("25");
        $('#txtEnterpriseEmailDisplayName').val("");

        $('#txtEmailAdress').attr("disabled", "disabled");
        $('#txtEmailPwd').attr("disabled", "disabled");
        $('#txtEmailSMTP').attr("disabled", "disabled");
        $('#txtEnterpriseEmailPort').attr("disabled", "disabled");
        $('#txtEnterpriseEmailDisplayName').attr("disabled", "disabled");
    }
    else {
        $('#chkMoneyEmail').attr("disabled", false);
        $('#chkEnterpriseEmailEnableSSL').attr("disabled", false);
        $('#chkEnterpriseEmailUseDefaultCredentials').attr("disabled", false);

        $('#txtEmailAdress').attr("readonly", "");
        $('#txtEmailPwd').attr("readonly", "");
        $('#txtEmailSMTP').attr("readonly", "");
        $('#txtEnterpriseEmailPort').attr("readonly", "");
        $('#txtEnterpriseEmailDisplayName').attr("readonly", "");

        $('#txtEmailAdress').attr("disabled", "");
        $('#txtEmailPwd').attr("disabled", "");
        $('#txtEmailSMTP').attr("disabled", "");
        $('#txtEnterpriseEmailPort').attr("disabled", "");
        $('#txtEnterpriseEmailDisplayName').attr("disabled", "");
    }
}


function AutoPrint() {
    if ($('#chkAutoPrint').attr("checked")) {
        $('#chkAccordPrint').attr("disabled", false);
        $('#PrintPreview').attr("disabled", false);
        //打印纸类型
        $('#SanLianShiZhi').attr("disabled", false);
        $('#POs58ShiZhi').attr("disabled", false);
        $('#POs80ShiZhi').attr("disabled", false);
        //打印份数
        $('#Txthycz').attr("disabled", false);
        $('#Txtjfbd').attr("disabled", false);
        $('#Txtjfdh').attr("disabled", false);
        $('#Txtsprk').attr("disabled", false);
        $('#Txtspxf').attr("disabled", false);
        $('#Txtjcxf').attr("disabled", false);
        $('#Txtksxf').attr("disabled", false);
        $('#Txthycc').attr("disabled", false);
        $('#Txthycs').attr("disabled", false);
        $('#Txtxfjl').attr("disabled", false);
        $('#Txthyczbb').attr("disabled", false);
        $('#Txtjfbdbb').attr("disabled", false);
        $('#Txtjfdhbb').attr("disabled", false);
        $('#Txtckrkmx').attr("disabled", false);
    }
    else {
        $('#chkAccordPrint').attr("checked", false);
        $('#chkAccordPrint').attr("disabled", true);
        $('#PrintPreview').attr("disabled", true);
        //打印纸类型
        $('#SanLianShiZhi').attr("disabled", true);
        $('#POs58ShiZhi').attr("disabled", true);
        $('#POs80ShiZhi').attr("disabled", true);
        //打印份数
        $('#Txthycz').attr("disabled", true);
        $('#Txtjfbd').attr("disabled", true);
        $('#Txtjfdh').attr("disabled", true);
        $('#Txtsprk').attr("disabled", true);
        $('#Txtspxf').attr("disabled", true);
        $('#Txtjcxf').attr("disabled", true);
        $('#Txtksxf').attr("disabled", true);
        $('#Txthycc').attr("disabled", true);
        $('#Txthycs').attr("disabled", true);
        $('#Txtxfjl').attr("disabled", true);
        $('#Txthyczbb').attr("disabled", true);
        $('#Txtjfbdbb').attr("disabled", true);
        $('#Txtjfdhbb').attr("disabled", true);
        $('#Txtckrkmx').attr("disabled", true);
    }
    AccordPrint();
}

//感应式IC卡初始化
//需要调用浏览器插件的初始化方法，然后等待数毫秒，输出浏览器插件的方法调用结果
//如果出现找不到对象等错误，可能是因为浏览器插件没有正确安装、浏览器插件没有执行权限等原因
//插件为智络公司根据刷卡器厂商提供的接口编写
function btnSenseICCardInit() {
    art.dialog({
        title: '系统提示',
        content: "确定要初始化感应式IC卡吗?",
        lock: true,
        ok: function () {
            this.close();
            CardReader.IniCard(); //初始化
            setTimeout("showMsg()", 500);
        },
        cancelVal: '关闭',
        cancel: true
    })
}
function showMsg() {
    var aa = CardReader.errMsg;
    art.dialog({
        title: '系统提示',
        content: aa,
        time: 1.5,
        lock: true
    })
}


function btnContactICCardInit() {
    art.dialog({
        title: '系统提示',
        content: "确定要初始化接触式IC卡吗?",
        lock: true,
        ok: function () {
            this.close();
            setTimeout("InitContacticcard()", 200);
        },
        cancelVal: '关闭',
        cancel: true
    })
}
//接触式IC卡初始化
//需要调用机器厂商提供的浏览器插件的方法，并且必须按顺序执行厂商提供的一系列方法并检查执行结果
//出现错误 第一时间检查是否是浏览器插件没有正确安装、浏览器插件没有执行权限、机器没有连接等
function InitContacticcard() {
    var status = null;
    var setValue = "";
    MWReaderCtl.MWic_init(0, 9600);
    status = MWReaderCtl.LastRet;
    if (status == 0) {
        MWReaderCtl.MWcsc_4442(6, "ffffff");
        status = MWReaderCtl.LastRet;
        if (status != 0) {
            art.dialog({
                title: '系统提示',
                content: '密码验证错误，请检查设备的LED灯是否处于绿色状态，若不是请将IC卡插好，否则请与管理员联系！！',
                time: 3
            })
            MWReaderCtl.MWic_exit();
            return;
        }
        for (var i = 0; i < 20; i++) {
            setValue += "F";
        }
        MWReaderCtl.MWswr_4442(60, 20, setValue);
        status = MWReaderCtl.LastRet;
        if (status == 0) {
            art.dialog({
                title: '系统提示',
                content: '接触式IC卡初始化成功！！',
                time: 3
            })
        }
        else {
            art.dialog({
                title: '系统提示',
                content: '接触式IC卡初始化失败，请与管理员联系！！',
                time: 3
            })
            MWReaderCtl.MWic_exit();
            return;
        }
        MWReaderCtl.MWic_exit();
    }
    else {
        art.dialog({
            title: '系统提示',
            content: '接触式IC卡设备打开失败，请检查设备连接！',
            time: 3
        });
    }
}

//感应式IC卡复选框
//选择感应式IC卡时要关闭接触式IC卡，2种卡不能同时使用
function chkSenseiccardClick() {
    if ($(this).attr("checked")) { $("#chkContacticcard").attr("checked", false); }
}

//接触式IC卡复选框
//选择接触式IC卡时要关闭感应式IC卡，2种卡不能同时使用
function chkContacticcardClick() {
    if ($(this).attr("checked")) { $("#chkSenseiccard").attr("checked", false); }
}

//A4
function A4ShiZhi_Click() {
    if ($(this).attr("checked")) { $("#SanLianShiZhi").attr("checked", false); $("#POs58ShiZhi").attr("checked", false); $("#POs80ShiZhi").attr("checked", false); }
    else { $(this).attr("checked", true); }
}
//三联
function SanLianShiZhi_Click() {
    if ($(this).attr("checked")) { $("#A4ShiZhi").attr("checked", false); $("#POs58ShiZhi").attr("checked", false); $("#POs80ShiZhi").attr("checked", false); }
    else { $(this).attr("checked", true); }
}
//POS 58
function POs58ShiZhi_Click() {
    if ($(this).attr("checked")) { $("#SanLianShiZhi").attr("checked", false); $("#A4ShiZhi").attr("checked", false); $("#POs80ShiZhi").attr("checked", false); }
    else { $(this).attr("checked", true); }
}

//POS 80
function POs80ShiZhi_Click() {
    if ($(this).attr("checked")) { $("#SanLianShiZhi").attr("checked", false); $("#A4ShiZhi").attr("checked", false); $("#POs58ShiZhi").attr("checked", false); }
    else { $(this).attr("checked", true); }
}