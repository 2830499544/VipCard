var locationUrl = "RegisterOK.aspx";
$(document).ready(function () {
    //"注册"按键响应函数
    $("#btnSubmit").bind("click", Register);

});


function Register() {
    var strErrorMsg = "";

    //企业代码校验
    var txtShopCode = $("#txtShopCode").val();
    if (txtShopCode == "" ) {
        strErrorMsg += "<li>企业代码不能为空，请重新输入;</li>";
    }

    //企业名称校验
    var txtShopName = $("#txtShopName").val();
    if (txtShopName == "") {
        strErrorMsg += "<li>企业名称不能为空，请重新输入;</li>";
    }


    //联系电话校验
    var txtTel = $("#txtTel").val();
    if (txtTel == "") {
        strErrorMsg += "<li>联系电话不能为空，请重新输入;</li>";
    }

    //详细地址校验
    var txtAddress = $("#txtAddress").val();
    if (txtTel == "") {
        strErrorMsg += "<li>详细地址不能为空，请重新输入;</li>";
    }

    var selectVer = $("#selectVer").val();

    //激活码校验
    var txtSN = $("#txtSN").val();
    if (txtTel == "") {
        strErrorMsg += "<li>激活码不能为空，请重新输入;</li>";
    }


    //密码校验
    var newPwdOne = $("#txtPasswordOne").val();
    var newPwdTwo = $("#txtPasswordTwo").val();
    if (newPwdOne == "" || newPwdTwo == "") {
        strErrorMsg += "<li>密码不能为空，请重新输入;</li>";
    }
    else {
        if (newPwdOne != newPwdTwo) {
            strErrorMsg += "<li>两次密码输入不同，请重新输入;</li>";
        }
    }
    
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
    art.dialog({
        title: '系统提示',
        content: '将为企业用户 [' + txtShopName + '],进行注册。\n确定操作吗？',
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
               "RegiserShop",
               {
                   "shopName": txtShopName,
                   "shopCode": txtShopCode,
                   "shopTelephone": txtTel,
                   "shopAddress": txtAddress,
                   "userPassword": newPwdOne,
                   "SerailNumber": txtSN,
                   "SelectVer":selectVer
               },
               "json",
                function (json) {
                    switch (json) {
                        case 0:
                            art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统异常，未保存数据，请再次点击保存！"),
                                     lock: true
                                 });
                            break;
                        case -1:
                            art.dialog
                                ({
                                    title: '系统提示',
                                    time: 4,
                                    content: ("会员旧密码输入不正确,请重新输入！"),
                                    lock: true
                                });

                            break;
                        case -2:
                        case -3:
                            art.dialog
                             ({
                                 title: '系统提示',
                                 time: 4,
                                 content: ("系统错误 请与系统管理员联系！"),
                                 lock: true
                             });
                            break;
                        case 2:
                            art.dialog
                             ({
                                 title: '系统提示',
                                 time: 4,
                                 content: ("序列号已经使用！"),
                                 lock: true
                             });
                            break;
                        case 3:
                            art.dialog
                             ({
                                 title: '系统提示',
                                 time: 4,
                                 content: ("序列号已被锁定！"),
                                 lock: true
                             });
                            break;
                        case 4:
                            art.dialog
                             ({
                                 title: '系统提示',
                                 time: 4,
                                 content: ("序列号还未制卡！"),
                                 lock: true
                             });
                            break;
                        case 5:
                            art.dialog
                             ({
                                 title: '系统提示',
                                 time: 4,
                                 content: ("序列号不存在！"),
                                 lock: true
                             });
                            break;
                        case 6:
                            art.dialog
                             ({
                                 title: '系统提示',
                                 time: 4,
                                 content: ("企业代码已存在！"),
                                 lock: true
                             });
                            break;
                        case 7:
                            art.dialog
                             ({
                                 title: '系统提示',
                                 time: 4,
                                 content: ("企业名称已存在！"),
                                 lock: true
                             });
                            break;
                        default:
                            art.dialog
                             ({
                                 title: '系统提示',
                                 time: 0.5,
                                 content: '企业已经创建成功，请牢记您的密码！',
                                 close: function () { location.href = locationUrl + "?ShopName=" + txtShopName + "&ShopCode=" + txtShopCode + "&Password=" + newPwdOne; },
                                 lock: true
                             });
                            break;
                    }
                });
            return false;
        },
        cancelVal: '取消',
        cancel: true
    });
}