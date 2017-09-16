// 退出登录
function LoginOut(compel) {
    var logout = confirm("您确定要退出商家联盟系统吗？？");
            if (logout == true) {
                window.location.href = "login.aspx?type=loginout";
            }

    return false;
}
