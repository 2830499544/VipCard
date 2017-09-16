



//文本消息重置按钮点击事件  响应函数
function btnSysRotateEdit(rotateID) {

    location.href = "SysRotateInfo.aspx?rotateID=" + rotateID;
}

function btnSysRotateDel(rotateID) {
 
    doAjax("../",
               "SysRotateDelete",
               {
                   "RotateID": rotateID
                 
               },
               "json",
                function (json) {
                    switch (json) {
                        case -2:
                            art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统提示，该活动已经有会员参与，无法删除！")
                                 });
                            break;
                        case -1:
                            art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统错误，请再次点击删除！")
                                 });
                            break;
                        case 0:
                            art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统异常，未删除数据，请再次点击删除！")
                                 });
                                 break;
                        default:
                            art.dialog
                                ({
                                    title: '系统提示',
                                    time: 2,
                                    content: ("删除成功！"),
                                    lock: true,
                                    close: function () { location.href = "SysRotateList.aspx?PID=144"; }
                                });
                            break;
                    }
                });
}
