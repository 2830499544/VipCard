function EmailResend(emailID) {
    doAjax(
            "../",
            "EmailResend",
            { "emailID": emailID },
            "json",
            function (json) {
                window.location.href = "../ExtraService/EmailList.aspx?PID=117";
            });
}