$(document).ready(function () {
    //保存按钮响应函数
    $("#btnBakUp").bind("click", dataBakup);
})

function dataBakup() {
    if ($("#hidCount").val() >= 3) {
        art.dialog({ title: '系统提示',
            content: ("系统仅提供3次备份文件保存,需要更多备份次数请与服务商联系！"),
            lock: true
        });
        return false;
    }
    else {
        if ($.trim($("#txtBakName").val()) != "") {
            doAjax("../",
         'DataBakUp',
        {
            "bakUpName": $.trim($("#txtBakName").val())
        },
        "json",
         function (json) {
             if (json = 0) {
                 art.dialog
                          ({
                              title: '系统提示',
                              content: ("系统异常，未能正常备份数据，请再次重试备份！"),
                              lock: true
                          });
                 return false;
             }
             else {
                 art.dialog
                            ({
                                title: '系统提示',
                                time: 0.5,
                                content: '数据备份成功！',
                                close: function () { location.href = "SysDataBak.aspx?PID=83"; },
                                lock: true
                            });
             }
         });
        }
    }
}

function deletebak(fileName) {

    art.dialog({
        title: '系统提示',
        content: '您确定要删除此备份文件吗？此操作不可恢复，请慎重！ ',
        lock: true,
        ok: function () {
            this.close();
            doAjax("../",
          "DataBakUpDel",
          { "bakUpName": fileName },
          "json",
          function (json) {
              if (json == 1) {
                  art.dialog({
                      title: '系统提示',
                      time: 2,
                      content: ("备份文件删除成功！"),
                      close: function () { location.href = "SysDataBak.aspx?PID=83"; }
                  });

              }
          });
            return false;
        },
        cancelVal: '取消',
        cancel: true
    });
}


function ReductionDataBakUp(fileName) {

    art.dialog({
        title: '系统提示',
        content: '您确定要还原此数据库吗？恢复将覆盖当前数据，请慎重！ ',
        lock: true,
        ok: function () {
            this.close();
            doAjax("../",
          "ReductionDataBakUp",
          { "bakUpName": fileName },
          "json",
          function (json) {
              if (json == 1) {
                  art.dialog({
                      title: '系统提示',
                      time: 2,
                      content: ("数据库还原成功！"),
                      close: function () { location.href = "SysDataBak.aspx?PID=83"; }
                  });
              }
              else {
                  art.dialog({
                      title: '系统提示',
                      ioc: "warring",
                      time: 2,
                      content: ("数据库还原出现错误！"),
                      close: function () { location.href = "SysDataBak.aspx?PID=83"; }
                  });
              }
          });
            return false;
        },
        cancelVal: '取消',
        cancel: true
    });
}