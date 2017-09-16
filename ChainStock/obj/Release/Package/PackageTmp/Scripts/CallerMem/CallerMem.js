$(document).ready(function () {
    $("#btnSave").bind("click", 1, btnSave);
})
var t = 0
function btnSave(type) {
    if (type == undefined) {
        t = 2;
    }
    if (t == 2) {
        return false;
    }
    doAjax("../",
           "CarllerMem",
           {
               "Mobile": $("#lblTel").html(),
               "MemID": $("#txtFMemID").val(),
               "Mobile": $("#lblTel").html(),
               "State": $("#lblTelState").html(),
               "Time": $("#lblTime").html(),
               "Remark": $("#txTelRemark").val()
           },
           "json",
           function (json) {
               if (json == 1) {
                   art.dialog({ title: '系统提示', time: 2, content: '添加成功！', close: function () {
                       var win = art.dialog.open.origin; //来源页面  

                       win.location.reload();
                   }
                   });
               }
               else if (json = -1) {
                   art.dialog({ title: '系统提示', time: 2, content: '系统错误，请联系管理员！', close: function () {
                       var win = art.dialog.open.origin; //来源页面  

                       win.location.reload();
                   }
                   });
               }
               else {
                   art.dialog({ title: '系统提示', time: 2, content: '系统异常，未保存数据，请再次点击保存！', close: function () {
                       var win = art.dialog.open.origin; //来源页面  

                       win.location.reload();
                   }
                   });
               }
           }

    )
}