console.log("%c技术支持: 成都智络科技有限公司", "color:#ddd");


$(window).ready(function () {
    
    changeSize();

    // 屏幕尺寸改变时自适应
    window.onresize = changeSize;

    //设置全局变量
   var winHeight = $(window).height();
   var winWidth = $(window).width();
   var docHeight = $(document).height();
   var docWidth = $(document).width();

   function changeSize() {
       //设置全局变量
       winHeight = $(window).height();
       winWidth = $(window).width();
       docHeight = $(document).height();
       docWidth = $(document).width();

       $("body,html").css("font-size", winWidth / 14.4 + "px");

       $("#container").css("height", winHeight);
       $("#content").css({
           "width": winWidth - $("#aside").width() + "px",
           "height": winHeight + "px",
           "min-width": 720 + "px"
       });
       $(".header").css("width", winWidth - $("#aside").width() + "px");
       $(".admin-txt").css({
           "width": $("#content").width() - 180 + "px",
           "margin-top": (100 - $(".admin-txt").height()) / 2 + "px"
       });
       // 系统公告
       $(".system-notice li").each(function () {
           $(this).find("a").css("width", $(this).width() - $(this).find("span").width() - 5)
       });
   }
    
   (function(){
        $(".aside-nav li").each(function(){
            var slide = false;
            $(this).find(".main-nav").click(function(){
                if (!slide) {
                    $(this).css("background-color","#1b2428");
                    $(this).find("span img").attr("src","images/icon (14).png");
                    $(this).siblings(".min-nav").slideDown(300,function(){
                        slide = true;
                    });
                }else{
                    $(this).css("background-color","#273238");
                    $(this).find("span img").attr("src","images/icon (3).png");
                    $(this).siblings(".min-nav").slideUp(300,function(){
                        slide = false;
                    });
                }
            });
        });
   }());
    
});