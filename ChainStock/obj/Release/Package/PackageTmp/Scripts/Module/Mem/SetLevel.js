$(document).ready(function () {
    //新增按键响应函数
    $("#btnAddLevel").bind("click", BtnAddLevel);
});

/*******************************************************************************
*新增等级按钮响应函数
*******************************************************************************/
function BtnAddLevel() {
    location.href = "EditLevel.aspx?PID=102";
}

/*******************************************************************************
*编辑等级响应函数
*******************************************************************************/
function EditLevel(levelID, ShopMemLevelID) {
    location.href = "EditLevel.aspx?PID=102&MemLevelID=" + levelID + "&ShopMemLevelID=" + ShopMemLevelID + "";
}

/*******************************************************************************
*删除等级响应函数
*******************************************************************************/
function DeleteLevel(levelID, levelName) {
    if (levelID == 0) {
        art.dialog({
            title: '系统提示',
            content: "普通会员是最基本会员等级，不能删除。",   //弹出层显示文本
            icon: 'error',  //图标
            lock: true      //是否锁定背景
        });
    }

    else {
        art.dialog({
            title: '系统提示',
            lock: true,
            content: '确定要删除【' + levelName + '】吗? 此操作不可恢复',
            ok: function () {
                this.close();
                doAjax("../",
                   "MemLevelDelete",
                   { "LevelID": levelID },
                   "json",
                   function (json) {
                       switch (json) {
                           case -1:
                               art.dialog({
                                   title: '系统提示',
                                   content: ("该等级已经存在会员，不能删除该等级！"),
                                   lock: true
                               });
                               break;
                           case -2:
                               art.dialog({
                                   title: '系统提示',
                                   content: ("未找到该会员等级！"),
                                   lock: true
                               });
                               break;
                           case 0:
                               art.dialog({
                                   title: '系统提示',
                                   content: ("系统异常，未删除等级，请再次点击删除！"),
                                   lock: true
                               });
                               break;
                           default:
                               art.dialog({
                                   title: '系统提示',
                                   time: 0.5,
                                   content: '删除成功！',
                                   close: function () { location.href = "./SetLevel.aspx?PID=5"; },
                                   lock: true
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
    return false;
}
