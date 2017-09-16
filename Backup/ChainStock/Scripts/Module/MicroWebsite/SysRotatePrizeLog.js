$(document).ready(function () {
    $("#SysArea1_sltCity").bind("change", { foo: "SysArea1" }, City);
    $("#SysArea1_sltCounty").bind("change", { foo: "SysArea1" }, County);
    $("#SysArea1_sltProvince").bind("change", { foo: "SysArea1" }, Province);
    $("#SysArea1_sltVillage").bind("change", { foo: "SysArea1" }, Village);

});
function GivePrize(PrizeLogID, memName) {
  
    art.dialog({
        title: '系统提示',
        content: '确定给会员 [' + memName + '] 发放奖品吗？',
        lock: true,
        ok: function () {
            this.close();
         
            doAjax("../",
                           "SysRotatePrizeLogUpdate",
                           {
                               "PrizeLogID": PrizeLogID
                              
                           },
                           "text",
                            function (text) {
                                switch (text) {
                                 case "-1":
                                        art.dialog
                                             ({
                                                 title: '系统提示',
                                                 time: 4,
                                                 content: ("系统错误，请稍后重试！"),
                                                 lock: true
                                             });
                                        break;
                                    case "0":
                                        art.dialog
                                             ({
                                                 title: '系统提示',
                                                 time: 4,
                                                 content: ("系统繁忙，请稍后重试！"),
                                                 lock: true
                                             });
                                        break;
                                    
                                    default:
                                        art.dialog
                                        ({
                                            title: '系统提示',
                                            time: 1,
                                            content: ("奖励发放成功！"),
                                            lock: true,
                                            close: function () { location.href = "SysRotatePrizeLog.aspx?PID=145"; }
                                        });
                                        break;
                                }
                            });
            return false;
        },
        cancelVal: '取消',
        cancel: true
    });
}

