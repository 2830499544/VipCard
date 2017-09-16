
$(document).ready(function () {
    //获取会员列表
    GetMemberTable();

    //查找按钮响应函数
    $("#btnChooseMember").bind("click", BtnChooseMember);

    //">>按钮"响应函数
    $("#btnSift").bind("click", BtnSift);

    //确定按钮响应函数
    $("#btnOK").bind("click", BtnOK);

    //取消按钮响应函数
    $("#btnCancel").bind("click", BtnCancel);

    //查找会员的回车事件
    $("#txtQueryMem").keypress(function (e) {
        var keyAscii = window.event ? e.keyCode : e.which;
        if (keyAscii == 13) {
            BtnChooseMember();
            return false;
        }
    });
});

/****************************************************************************************
*获取会员列表
****************************************************************************************/
function GetMemberTable() {
    doAjax("../",
           "GetMemList",
           { "type": "",
               "memCard": $("#txtQueryMem").val(),
               "memLevelID": $("#sltMemLevelID").val(),
               "memShopID": $("#sltShop").val()
           },
           "json",
           function (json) {
               CreateMemList(json);
           });
}

/****************************************************************************************
*创建会员列表
****************************************************************************************/
function CreateMemList(obj) {
    var html = ''
             + '<tr class=\"th\">'
             + '<td>会员姓名</td><td>手机号</td>'
             + '</tr>';
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            var bg = index % 2 == 0 ? "#eee" : "#fff";
            var strMobile = (item.MemMobile != "" && item.MemMobile != null) ? item.MemMobile : "没有登记手机号码";
            html += "<tr class=\"repeterItems\" style=\"background-color:" + bg + ";\" onmouseover=\"this.style.background='#ddeeff'\" onmouseout=\"this.style.background='" + bg + "'\" onclick=\"javascript:Selected('" + item.MemName + ':' + item.MemMobile + ';' + "\');\">"
                 + '<td style="width:100">' + item.MemName + '</td>'
                 + '<td style="width:100">' + strMobile + '</td>'
                 + '</tr>';
        })
    }
    else {
        html += '<tr><td style="height:50px; line-height:50px;padding-left:20px; background-color:#fff;" colspan="3">未找到符合此条件的数据！请重试！</td></tr>';
    }
    $("#tbMember").html(html);
}

/****************************************************************************************
*点击列表选择单个会员
****************************************************************************************/
function Selected(member) {
    var strMember = new Array();
    strMember = member.split(":");
    strMember = strMember[1].split(";");
    if (strMember[0] != "") {
        var strMobile = $.trim($("#txtMobile").val());
        strMobile += member;
        $("#txtMobile").val(strMobile);
    }
    else {
        art.dialog.alert("该会员没有登记手机号码！");
    }
}

/****************************************************************************************
*查找按钮响应函数
****************************************************************************************/
function BtnChooseMember() {
    doAjax("../",
           "GetMemList",
           { "type": "",
               "memCard": $("#txtQueryMem").val(),
               "memLevelID": $("#sltMemLevelID").val(),
               "memShopID": $("#sltShop").val()
           },
           "json",
           function (json) {
               CreateMemList(json);
           });
}

/****************************************************************************************
*">>按钮"响应函数
****************************************************************************************/
function BtnSift() {
    doAjax("../",
           "GetMemList",
           { "type": "sift",
               "memCard": $("#txtQueryMem").val(),
               "memLevelID": $("#sltMemLevelID").val(),
               "memShopID": $("#sltShop").val()
           },
           "json",
           function (json) {
               if (json.length > 0) {
                   $.each(json, function (index, item) {
                       if (item.MemMobile != "" && item.MemMobile != null) {
                           var strMobile = $.trim($("#txtMobile").val());
                           strMobile += (item.MemName + ":" + item.MemMobile + ";");
                           $("#txtMobile").val(strMobile);
                           $("#txtMobile").focus();
                       }
                   });
               }
           });
}

/****************************************************************************************
*确定按钮响应函数
****************************************************************************************/
function BtnOK() {
    window.returnValue = $.trim($("#txtMobile").val());
    window.close();
}

/****************************************************************************************
*取消按钮响应函数
****************************************************************************************/
function BtnCancel() {
    window.close();
}