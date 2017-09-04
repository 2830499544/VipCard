$(document).ready(function () {

    $("#btnAddTiming").bind("click", AddTiming);
});

function AddTiming() {
    window.location = 'TimeExpense.aspx?PID=115';
}

//出场
function btnEndExpense(memID, token) {
    if (memID == 0) {
        isMem = false;
    }
    else {
        isMem = true;
    }

    doAjax("../",
           "GetTimeExpense",
           {
               "Token": token,
               "isMem": isMem
           },
           "json",
            function (json) {
                switch (json.flag) {
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
                               content: ("系统错误 请与系统管理员联系！"),
                               lock: true
                           });
                        break;
                    default:
                        art.dialog
                          ({
                              title: '系统提示',
                              time: 2,
                              content: "正在转到结账",
                              close: function () { location.href = "../MemExpense/Expense.aspx?PID=17&MemCard=" + json.memCard + "&IsMem=" + isMem + "&OrderCode=" + json.OrderCode; }
                          });
                        break;
                }
            });
}
