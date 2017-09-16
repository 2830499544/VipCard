var myProjectdata;
$(document).ready(function () {
    $("#txtOrderToken").attr("disabled", "disabled");
    $("#btnExpSave").bind("click", ExpSave);
    //选择服务项目
    $("#btnChooseProject").bind("click", ChooseProject);
});

var pageSize = 8;
var currentPage = 1;
var ProjectSearch;
function ChooseProject() {
    doAjax("../",
           "GetPrjectByPage",
           {
               "size": pageSize,
               "index": currentPage,
               "ProjectName": $("#txtProjectName").val(),
               "memID": global_mem.MemID
           },
           "json",
           function (json) {
               CreateProjectList(json.List);
               MyCreateProjectPager(json.RecordCount);
               $("#txtProjectName").focus();
               ProjectSearch = art.dialog({
                   title: '选择计时服务',
                   content: document.getElementById('TimingProjectList'),
                   id: 'TimingProjectList',
                   lock: true
               });
           }
    );
}
function MyCreateProjectPager(resCount) {
    var page = new CommonPager(
        "ProjectList",
        pageSize,
        resCount,
        currentPage,
        function (p) {
            currentPage = parseInt(p);
            ChooseProject();
        }
    );
    page.ShowSimple();
}

function CreateProjectList(obj) {
    var html = ''
    if (obj.length > 0) {
        $.each(obj, function (index, item) {
            html += "<tr class=\"td\" ondblclick=\"javascript:QuickProject(\'" + item.ProjectRulesID + "','" + item.ProjectName + "','" + item.ProjectID + "','" + item.AllCount + "\');\">"
                 + '<td style=" width:60px;">' + item.ProjectName + '</td>'
                 + '<td style=" width:60px;">' + item.AllCount + '</td>'
                 + '<td style=" width:60px">' + item.RulesName + '</td>'
                 + '<td style=" width:160px">' + item.RulesRemark + '</td>'
                 + '</tr>';
        });
    }
    else {
        html += '<td style="height:50px; line-height:50px;background-color:#fff;text-align:center;" colspan="4">未找到符合此条件的数据！请重试！</td>';
    }
    $("#tbProject").html(html);
}

function QuickProject(RulesID, ProjectName, ProjectID, time) {
    $("#txtProjectName").val("");
    ProjectSearch.close();
    doAjax("../",
           "GetRules",
           {
               "RulesID": RulesID
           },
           "json",
           function (json) {
               $("#txtProjectID").val(ProjectID);
               $("#lbProjectDescription").html("计时服务名称：" + ProjectName + "  规则描述：" + json.RulesRemark + "  剩余时间：" + time);
           }
    );
    doAjax("../",
           "GetProjectState",
           {
               "project": ProjectID
           },
           "json",
           function (json) {
               switch (json) {
                   case 0:
                       $("#spstatus").html("<b style='color:#217cb9;'>空闲中</b>");
                       break;
                   case 1:
                       $("#spstatus").html("<b style='color:red;'>正在消费</b>");
                       break;
                   default:
                       $("#spstatus").html("<b style='color:#217cb9;'>空闲中</b>");
                       break;
               }
           }
    );
}

var chkNoMember;
function ExpSave() {
    var OrderPredictTime = $("#txtOrderPredictTime").val();
    chkNoMember = $("#chkNoMember").attr("checked");
    var ProjectID = $("#txtProjectID").val();
    var isMem = false;
    if (!chkNoMember && $.isEmptyObject(global_mem)) {
        art.dialog({
            icon: 'error', //图标
            content: '请先选择会员!',
            lock: true
        });
        return false;
    }
    else if (chkNoMember && $("#txtOrderToken").val() == "") {
        art.dialog({
            icon: 'error', //图标
            content: '选择了散客，请输入散客令牌!',
            lock: true
        });
        return false;
    }
    if (ProjectID == "") {
        art.dialog({
            icon: 'error', //图标
            content: '选择计时服务!',
            lock: true
        });
        return false;
    }
    if (!chkNoMember) {
        strToken = global_mem.MemCard;
        isMem = true;
    }
    else {
        var strToken = $("#txtOrderToken").val();
        isMem = false;
    }
    if (OrderPredictTime != "" && !OrderPredictTime.IsNumber()) {
        art.dialog({
            icon: 'error', //图标
            content: '预定消费时长,格式错误!',
            lock: true
        });
        return false;
    }
    doAjax("../",
           "TimeExpenseStart",
           {
               "Token": strToken,
               "isMem": isMem,
               "Project": ProjectID,
               "OrderPredictTime": OrderPredictTime
           },
           "json",
            function (json) {
                switch (json.flag) {
                    case "0":
                        art.dialog
                             ({
                                 title: '系统提示',
                                 time: 4,
                                 content: json.strReturn,
                                 lock: true
                             });
                        break;
                    case "-1":
                        art.dialog
                           ({
                               title: '系统提示',
                               time: 4,
                               content: json.strReturn,
                               lock: true
                           });
                        break;
                    case "-2":
                        art.dialog
                           ({
                               title: '系统提示',
                               time: 4,
                               content: json.strReturn,
                               lock: true
                           });
                        break;
                    case "-3":
                        art.dialog
                           ({
                               title: '系统提示',
                               time: 4,
                               content: json.strReturn,
                               lock: true
                           });
                        break;
                    case "-4":
                        art.dialog
                           ({
                               title: '系统提示',
                               time: 4,
                               content: json.strReturn,
                               lock: true
                           });
                        break;
                    default:
                        art.dialog
                          ({
                              title: '系统提示',
                              time: 4,
                              content: json.strReturn,
                              close: function () { location.href = 'TimeList.aspx?PID=87'; }
                          });
                        break;
                }
            });
}

/****************************************************************************************************
*在选择好会员时可以执行回调函数
*****************************************************************************************************/
function FindMember_CallBack() {
    var strErrorMsg;
    if (global_mem.MemState != 0) {
        strErrorMsg = "当前会员卡处于锁定或者挂失状态，暂不允许进行消费。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    if (global_mem.MemIsPast == "True") {
        strErrorMsg = "当前会员卡已过期，暂不允许进行消费。";
        art.dialog({
            title: '系统提示',
            content: strErrorMsg,   //弹出层显示文本
            icon: 'error', //图标
            lock: true//是否锁定背景
        });
        return false;
    }
    $("#txtOrderToken").attr("disabled", "disabled");
    $("#txtOrderToken").val("");
    $("#lbProjectDescription").html("");
    $("#txtProjectID").val("");
    $("#spstatus").html("");
    //查找到会员后 所有控件解禁
    return true;
}

/****************************************************************************************************
*点散客选择框执行回调函数
*****************************************************************************************************/

var dilOrderToken;
function NoMember_CallBack(type) {
    $("#txtOrderToken").attr("disabled", "");
    $("#txtProjectID").val("");
    $("#lbProjectDescription").html("");
    $("#spstatus").html("");
}

