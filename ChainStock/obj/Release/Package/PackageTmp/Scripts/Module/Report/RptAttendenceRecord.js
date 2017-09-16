$(document).ready(function () {
    $('#txtStartTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd', maxDate: '2099-12-1', isShowClear: true, readOnly: true });
    });
    $('#txtEndTime').bind("focus click", function () {
        WdatePicker({ skin: 'ext', dateFmt: 'yyyy-MM-dd', maxDate: '2099-12-1', isShowClear: true, readOnly: true });
    });
    $("#btnAttendenceRecord").bind("click", AttendenceRecord);
});

function AttendenceRecord() {
    var txtStartTime = $("#txtStartTime").val();
    var txtEndTime = $("#txtEndTime").val();
    var shopID = $("#hidShopID").val();
    art.dialog({
        title: '系统提示',
        content: "将要生成[" + txtStartTime + "]至["+txtEndTime+"]之间的员工考勤记录。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
             "AttendenceRecord",
                {
                    "StartTime": txtStartTime,
                    "EndTime": txtEndTime,
                    "shopID": shopID
                },
                 "json",
                  function (json) {
                      switch (json) {
                          case 0:
                              art.dialog({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("系统异常，未生成成功，请再次点击！"),
                                  lock: true
                              });
                              break;
                          case -1:
                              art.dialog({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("暂无考勤记录可生成"),
                                  lock: true
                              });
                              break;
                          case -2:
                              art.dialog({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("系统错误，请与系统管理员联系！"),
                                  lock: true
                              });
                              break;
                          default:
                              art.dialog({
                                  title: '系统提示',
                                  time: 0.5,
                                  content: '生成成功！',
                                  close: function () { location.href = "/SystemManage/RptAttendenceRecord.aspx?PID=190"; },
                                  lock: true
                              });
                              break;
                      }
                  });
        },
        cancelVal: '取消',
        cancel: true
    });
}