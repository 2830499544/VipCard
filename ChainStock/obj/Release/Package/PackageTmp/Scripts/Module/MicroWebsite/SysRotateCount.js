
/*!
* 名    称：SLBVip客户端 
* 功能描述：商联币有效期
* 创 建 者：ldf
* 创建时间：2015-12-08 17:14:00
* 版 本 号：v1.0
* 所属代号：SLBVip
* 修改作者：
* 修改时间：
* 修改理由：
* 修改版本：
*/
 
$(document).ready(function () {
  

    //新增有效期
    $("#btnSysRotateCountAdd").bind("click", SysRotateCountAdd);

    //"保存"按钮响应函数
    $("#btnSave").bind("click", SysRotateCountSave);

    //"重置"按钮响应函数
    $("#btnReset").bind("click", SysRotateCountReset);


  
});



/**
* 新增
* @method SysRotateCountAdd
* @return {void} 
*/
function SysRotateCountAdd() {
    art.dialog({
        title: '新增规则',
        content: document.getElementById('divSysRotateCount'),
        id: 'divSysRotateCount',
        lock: true,
        close: function () {
            $("#txtCostAmount").val("");
       
            $("#txtRotateCount").val("");
        }
    });
    $("#txtCostAmount").focus();
}

/**
* 保存
* @method SysRotateCountSave
* @return {void} 
*/
function SysRotateCountSave() {
    var strErrorMsg = "";

    var strID = $.trim($("#txtID").val());
    var CostAmount = $.trim($("#txtCostAmount").val());
    var RotateCount = $.trim($("#txtRotateCount").val());
    var StartTime = $.trim($("#txtRotateStartTime").val());
    var EndTime = $.trim($("#txtRotateEndTime").val());


    var type = (strID == "") ? "SysRotateCountAdd" : "SysRotateCountEdit";
    if (CostAmount == "") {
        strErrorMsg += "<li>消费额度不能为空;</li>";
    }
    if (RotateCount == "") {
        strErrorMsg += "<li>抽奖次数不能为空;</li>";
    }
    if (StartTime == "") {
        strErrorMsg += "<li>消费开始时间不能为空;</li>";
    }
    if (EndTime == "") {
        strErrorMsg += "<li>消费结束时间不能为空;</li>";
    }

    if (strErrorMsg != "") {
        strErrorMsg = "<div>操作出现以下错误，请核查后重试！</div><ul>" + strErrorMsg + "</ul>";
        art.dialog({
            title: '系统提示',
            icon: 'error', //图标
            content: strErrorMsg,
            lock: true
        });
        return false;
    }
    art.dialog({
        title: '系统提示',
        content: "将要" + (type == "SysRotateCountAdd" ? "增加" : "编辑") + "[" + $.trim($("#txtCostAmount").val()) + "]元消费额度的有效期 。\n确定操作吗？",
        lock: true,
        ok: function () {
            this.close();
            //            this.lock();
            doAjax("../",
             type,
                {
                    "ID": strID,
                    "CostAmount": CostAmount,
                    "RotateCount": RotateCount,
                    "StartTime": StartTime,
                    "EndTime": EndTime,
                    "RotateID":$("#txtRotateID").val()

                },
                 "json",
                  function (json) {
                      switch (json) {
                          case 0:
                              art.dialog({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("系统异常，未保存数据，请再次点击保存！"),
                                  lock: true
                              });
                              break;
//                          case -1:
//                              art.dialog({
//                                  title: '系统提示',
//                                  time: 4,
//                                  content: ("该消费额度已经存在！"),
//                                  lock: true
//                              });
//                              break;
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
                                  content: '保存成功！',
                                  close: function () {
                                      $("#txtCoinID").val("");
                                  
                                   location.href = "./SysRotateCount.aspx?RotateID="+$("#txtRotateID").val() },
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


/**
* 重置
* @method SysRotateCountReset
* @return {void} 
*/
function SysRotateCountReset() {
    if ($("#txtID").val() == "") {
        $("#txtCostAmount").val("");
        $("#txtRotateCount").val("");


    }
    else {
        SysRotateCountEdit($("#txtID").val());
    }
}

/**
* 编辑
* @method SysRotateCountEdit
* @param {int} coinID 编号
* @return {void} 
*/
function SysRotateCountEdit(ID) {
    $("#txtID").val(ID);

    art.dialog({
        title: '编辑规则',
        content: document.getElementById('divSysRotateCount'),
        id: 'divSysRotateCountCoin',
        lock: true,
        close: function () {
            $("#txtID").val("");
            $("#txtCostAmount").val("");
      
            $("#txtRotateCount").val("");
        }
    });

    doAjax("../",
           "GetSysRotateCount",
           { "ID": ID },
           "json",
           function (json) {
               if (json != null) {
                   $("#txtID").val(json[0].ID);
                   $("#txtCostAmount").val(json[0].CostAmount);
                   $("#txtRotateCount").val(json[0].RotateCount);
                   $("#txtRotateStartTime").val(json[0].StartTime);
                   $("#txtRotateEndTime").val(json[0].EndTime);
               }
           });
}

/**
* 删除
* @method SysRotateCountDelete
* @param {int} coinID 编号
* @param {int} coinAmount 消费金额
* @return {void} 
*/
function SysRotateCountDelete(ID, coinAmount) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除【' + coinAmount + '】元消费额吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
                   "SysRotateCountDelete",
                   { "ID": ID },
                   "json",
                   function (json) {
                       switch (json) {
                         
                           case 0:
                               art.dialog({
                                   title: '系统提示',
                                   time: 4,
                                   content: ("系统异常，未删除，请再次点击删除！"),
                                   lock: true
                               });
                               break;
                           default:
                               art.dialog({
                                   title: '系统提示',
                                   time: 2,
                                   content: '删除成功！',
                                   close: function () { location.href = "./SysRotateCount.aspx?PID=144"; }
                               });
                               break;
                       }
                   });
            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    });
}

