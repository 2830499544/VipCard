$(document).ready(function () {

    Login_ChangeValImg();
    if ($("#txtIsType").val() != 1) {
        $("#txtAccount").val("admin");
        $("#txtPassword").val("admin");
    }


    if ($("#txtAccount").val() != '') {
       // $('#login').attr('disabled', 0);
        $("#AccountLabel").hide();
    }
    $("#txtAccount").keydown(function () {
      //  $('#login').attr('disabled', 0);
        $("#AccountLabel").hide();
    }).focusout(function () {
        var value = $(this).val().replace(/(^\s*)|(\s*$)/, "");
        if ('' == value) {
            $("#AccountLabel").show();
        }
    });

    if ($("#txtPassword").val() != '') {
        $('#login').attr('disabled', 0);
        $("#passwordLabel").hide();
    }
    $("#txtPassword").keydown(function () {
        $('#login').attr('disabled', 0);
        $("#passwordLabel").hide();
    }).focusout(function () {
        var value = $(this).val().replace(/(^\s*)|(\s*$)/, "");
        if ('' == value) {
            $("#passwordLabel").show();
        }
    });

    $("#txtValCode").keydown(function () {
        $('#login').attr('disabled', 0);
        $("#valcodeLabel").hide();
    }).focusout(function () {
        var value = $(this).val().replace(/(^\s*)|(\s*$)/, "");
        if ('' == value) {
            $("#valcodeLabel").show();
        }
    });


 

    //回车响应函数
    $("#txtValCode").keyup(function (e) {
    if($("#txtValCode").val().length==4)
    {            
        Login_Submit();
    }
        var key = window.event ? e.keyCode : e.which;
        if (key == 13) {
            if ($("#txtISCheckKey").val() == "1") {
                LoginSystem();
            }
            else {
                Login_Submit();
            }
        }
    });
    $("#txtAccount").focus()
});

function Login_ChangeValImg() {
    /*自动刷新验证码*/
    ChangeValImg();
    setTimeout("Login_ChangeValImg()", 10 * 60 * 1000);
}

function ChangeValImg() {
    $("#Login_ValImg").attr("src", "Service/ValidateImage.ashx?" + GetGuid());
    $("#txtValCode").val("").focus();
}

function Login_Submit() {
    var valCode = $("#txtValCode").val();
    var type = $("#txtIsType").val();
    //var message = "";
    if ($("#txtAccount").val() == "") {
        message = '请输入登录帐号！';
        art.dialog.through({
            lock: true,
            title: '系统提示',
            content: '请输入登录帐号！',
            time:1
        });
      
        return;
    }


    if ($("#txtPassword").val() == "") {
        art.dialog.through({
            lock: true,
            title: '系统提示',
            content: '请输入登录密码！'
        });
        return;
    }
    if ($("#txtValCode").val() == "") {
        art.dialog.through({
            lock: true,
            title: '系统提示',
            content: '请输入登录验证！'
        });
        return;
    }
    if (!(/\d{4}/).test(valCode)) {
        art.dialog.through({
            lock: true,
            title: '系统提示',
            content: '请重新输入四位验证码后重试！',
            time:1.5
        });
        
        return;
    }
    doAjax("", "CheckUserLogin", { "account": $("#txtAccount").val(), "password": $("#txtPassword").val(), "valcode": valCode }, "json",
        function (json) {
            switch (json) {
                case 0:
                    ChangeValImg();
                    art.dialog
                          ({
                              title: '系统提示',
                              content: ("账号或密码错误 请检查！"),
                              lock: true,
                              time:1.5
                          });
                    break;
                case 1:
                    setTimeout("window.location = 'index.aspx'", 200);
                    break;
                case 2:
                    art.dialog
                          ({
                              title: '系统提示',
                              content: ("账号或商家处于锁定状态 请与系统管理员联系！"),
                              lock: true,
                              time: 2
                          });
                    break;
                case 3:
                    art.dialog
                          ({
                              title: '系统提示',
                              content: ("系统登陆发生错误 请与系统管理员联系！"),
                              lock: true
                          });
                    break;
                case 4:                    
                    art.dialog
                          ({
                              title: '系统提示',
                              content: ("验证码输入错误，请重新输入！"),
                              lock: true,
                              time: 1.5,
                          });
                           ChangeValImg();
                    break;
                case 5:
                    art.dialog
                          ({
                              title: '系统提示',
                              content: ("系统已过期，请联系软件提供商！"),
                              lock: true
                          });
                    break;
            }
        }
        )
}

function Login_Reset() {
    $("#txtAccount").val("");
    $("#txtPassword").val("");
    $("#txtValCode").val("");
}



function GetGuid() {
    var now = new Date();
    var year = now.getFullYear(); //getFullYear getYear
    var month = now.getMonth();
    var date = now.getDate();
    var day = now.getDay();
    var hour = now.getHours();
    var minu = now.getMinutes();
    var sec = now.getSeconds();
    var mill = now.getMilliseconds();
    month = month + 1;
    if (month < 10) month = "0" + month;
    if (date < 10) date = "0" + date;
    if (hour < 10) hour = "0" + hour;
    if (minu < 10) minu = "0" + minu;
    if (sec < 10) sec = "0" + sec;

    var guid = month + date + hour + minu + sec + mill;

    return guid;
}
 




//加入收藏
function bookmark() {
    var title = document.title
    var url = document.location.href
    if (window.sidebar) window.sidebar.addPanel(title, url, "");
    else if (window.opera && window.print) {
        var mbm = document.createElement('a');
        mbm.setAttribute('rel', 'sidebar');
        mbm.setAttribute('href', url);
        mbm.setAttribute('title', title);
        mbm.click();
    }
    else if (document.all) window.external.AddFavorite(url, title);
}