$(document).ready(function () {
    $("#btnTimingProject").bind("click", TimingProject);
});

function SaveForm() {
    var ProjectName = $("#txtProjectName").val();
    var ProjectRulesID = $("#sltProjectRulesID").val();
    var ProjectRemark = $("#txtProjectRemark").val();
    var ProjectID = $("#txtProjectID").val();
    var MethodName = ProjectID == "" ? "TimingProjectAdd" : "TimingProjectEdit";
    var strErrorMsg = "";
    if (ProjectName == "") { strErrorMsg += "<li>服务名称不能为空！</li>"; }
    if (ProjectRulesID == "") { strErrorMsg += "<li>请选择服务规则！</li>"; }
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
        content: '将' + (ProjectID == "" ? "添加" : "编辑") + '服务[' + ProjectName + ']。\n确定操作吗？',
        lock: true,
        ok: function () {
            this.close();
            doAjax("../",
                    MethodName,
                   {
                       "ProjectName": ProjectName,
                       "ProjectRulesID": ProjectRulesID,
                       "ProjectRemark": ProjectRemark,
                       "ProjectID": ProjectID
                   },
                   "json",
                      function (json) {
                          switch (json) {
                              case 0:
                                  art.dialog
                                 ({
                                     title: '系统提示',
                                     time: 4,
                                     content: ("系统异常，未保存数据，请再次点击保存！"),
                                     lock: true
                                 });
                                  break;
                              default:
                                  art.dialog
                             ({
                                 title: '系统提示',
                                 time: 0.5,
                                 content: '保存成功！',
                                 close: function () { location.href = 'TimingProject.aspx?PID=111'; },
                                 lock: true
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

function TimingProject() {
    ResetForm();
    art.dialog({
        lock: true,
        title: '新增服务',
        content: $("#TimingProjectEdit").html(),
        id: 'TimingProjectEdit',
        close: function () { ResetForm(); }
    });
}

function EditTimingProject(ProjectID) {
    doAjax("../",
                     'GetTimingProject', { "ProjectID": ProjectID }, "json",
                         function (json) {
                             $("#txtProjectName").val(json.ProjectName);
                             $("#sltProjectRulesID").val(json.ProjectRulesID);
                             $("#txtProjectRemark").val(json.ProjectRemark);
                             $("#txtProjectID").val(json.ProjectID);
                         }
                       )
    art.dialog({
        lock: true,
        title: '编辑服务',
        content: $("#TimingProjectEdit").html(),
        id: 'TimingProjectEdit',
        close: function () { ResetForm(); }
    });
}

function DeleteTimingProject(ProjectID, ProjectName) {
    art.dialog({
        title: '系统提示',
        lock: true,
        content: '确定要删除服务【' + ProjectName + '】吗? 此操作不可恢复',
        ok: function () {
            this.close();
            doAjax("../",
                 'DelTimingProject', { "ProjectID": ProjectID }, "json",
                 function (json) {
                     switch (json) {
                         case 0:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("系统错误 请与系统管理员联系！"),
                                       lock: true
                                   });
                             break;
                         case -1:
                             art.dialog
                                   ({
                                       title: '系统提示',
                                       content: ("该服务已有使用记录无法删除！"),
                                       lock: true
                                   });
                             break;
                         default:
                             art.dialog
                                   ({
                                       time: 2,
                                       content: '删除成功！',
                                       close: function () { location.href = 'TimingProject.aspx?PID=111'; }
                                   });
                             break;
                     }
                 });

            return false;
        },
        cancelVal: '取消',
        cancel: true //为true等价于function(){}
    });
    return false;
}

function ResetForm() {
    $("#txtProjectName,#sltProjectRulesID,#txtProjectRemark,#txtProjectID").val("");
}