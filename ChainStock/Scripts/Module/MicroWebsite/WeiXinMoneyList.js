
function btnMoneyDel(MoneyID, MoneyTitle) {

    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除红包活动【' + MoneyTitle + '】吗? 此操作不可恢复',
        ok: function () {
            this.lock();
            doAjax("../", "DelWeiXinMoney", { "MoneyID": MoneyID }, "text", function (text) {
                if (text == "0") {
                    art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("系统异常 请与系统管理员联系！"),
                                  lock: true
                              });
                          }
                          else if (text == "-1") {
                              art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("系统错误 请与系统管理员联系！"),
                                  lock: true
                              });
                          } 
                         else if (text == "-2") {
                              art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("该红包活动已有红包领取记录，无法删除！"),
                                  lock: true
                              });
                          } 
                else {
                    art.dialog
                                ({
                                    title: '系统提示',
                                    time: 0.5,
                                    content: '删除成功！',
                                    close: function () { window.location = window.location; },
                                    lock: true
                                });
                }
            })
            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    })
}
