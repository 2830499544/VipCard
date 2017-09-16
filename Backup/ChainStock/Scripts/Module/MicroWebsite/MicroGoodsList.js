$(document).ready(function () {

})

function ShowPic(path) {
    if (path != "") {
        var image = "<img src='" + path + "' width=\"379\" height=\"500\" />";
    }
    else {
        var image = "<img src='../images/Gift/nogift.jpg' width=\"379\" height=\"500\" />";
    }
    art.dialog({
        padding: 0,
        title: '商品',
        content: image,
        lock: true,
        width: 300,
        height: 300
    });
}

function DeleteGoods(goodsID, goodsName) {
    art.dialog({
        title: "系统提示",
        lock: true,
        content: '确定要删除商品【' + goodsName + '】吗? 此操作不可恢复',
        ok: function () {
            this.lock();
            doAjax("../", "DelMicroGoods", { "goodsID": goodsID }, "text", function (text) {
                if (text == "0") {
                    art.dialog
                              ({
                                  title: '系统提示',
                                  time: 4,
                                  content: ("系统错误 请与系统管理员联系！"),
                                  lock: true
                              });
                } else {
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