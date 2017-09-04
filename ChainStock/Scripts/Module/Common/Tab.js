// tab样式
var style = '<style>'
        + '.tabBox { margin:2px; border-bottom:1px solid #3EA0DD; height:25px;}'
        + '.tabBox .tab li{ list-style:none; float:left; width:auto; padding:0px 10px; height:24px; line-height:24px; cursor:pointer;}'
        + '.tabBox .tab .on{  border:1px solid #3EA0DD;border-bottom:2px solid #fff; z-index:1; margin-top:-1px;_margin-top:0px; color:#c00;}'
        + '</style>';
document.write(style);

// 自定义tab选项卡
// container：选项卡UL元素
// tabBefer：tab切换之前执行的函数（一般进行一些判断）
// tabCallback：tab切换之后执行的函数（一般用于数据的加载）
function CommonTab(container, tabBefer, tabCallback) {
    var tabs = $("#" + container + " > ul > li");
    // tab切换（此函数为内部调用）
    var ShowTab = function (index) {
        for (var i = 0; i < tabs.length; i++) {
            if (tabs[i].id == "tab" + index)
                tabs[i].className = "on";
            else
                tabs[i].className = "";

            if (i == parseInt(index))
                $("#MainContent" + i).css("display", "");
            else
                $("#MainContent" + i).css("display", "none");
        }
        return true;
    }
    // 切换到某一个选项卡（此函数供外部调用）
    this.GoTab = function (index) {
        ShowTab(index);
    }
    // 绑定
    for (var i = 0; i < tabs.length; i++) {
        $(tabs[i]).click(function () {
            var id = $(this).attr("id").replace("tab", "");
            if ((typeof (tabBefer) == "function" && tabBefer(id)) || tabBefer == "") {
                ShowTab(id);
                if (typeof (tabCallback) == "function")
                    tabCallback(id);
            }
        });
    }
}
