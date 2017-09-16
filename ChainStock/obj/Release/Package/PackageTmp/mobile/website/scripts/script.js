console.log("%c技术支持: 成都可百科技有限公司", "color:#ddd");

$(window).ready(function () {

    //设置全局变量
    var windowH = $(window).height();
    var windowW = $(window).width();
    var documentH = $(document).height();
    var documentW = $(document).width();

    /*返回上一页*/
    $(".back-btn").click(function (event) {
        event.preventDefault();
        history.back();
    });
    // 首页高度设置
    if (windowH > documentH) {
        $(".index").height(windowH);
    } else {
        $(".index").height(documentH);
    }

    // 底部浮动更多按钮
    $(".fix-more").click(function () {
        $(".fix-more-nav").fadeToggle();
    });
    $(".fix-more").mouseleave(function () {
        $(".fix-more-nav").fadeOut();
    });
    //底部浮动方格自动布局
    (function () {
        var num = $(".foot-nav .fix-ch").length;
        var width = $(".foot-nav").outerWidth();
        var homeWidth = $(".fix-home").outerWidth();
        var moreWidth = $(".fix-more").outerWidth();
        $(".fix-ch").css("width", (width - homeWidth - moreWidth - num) / num + "px");
    } ());

    //产品图片封面照片等宽高
     $(".product-list li").height($(".product-list li").width());
     $(".product-mode").height($(".product-list li").width());

    //相片列表布局设置
    (function () {
        var list = $(".photos-d-list").children("li").clone();
        $(".photos-d-list li").each(function () {
            $(this).click(function () {
                $(".photo-mode").animate({ opacity: 1, zIndex: 9999 }, 200);
                $("body,html").css("overflow", "hidden");
            })
        });
    } ());

    $("#photoPic ul").children("li").css("width", $("#photoPic").outerWidth());
    $("#photoPic ul").css({
        "width": $("#photoPic ul").children("li").length * $("#photoPic").outerWidth() + "px"
    });
    //关闭相片模态框
    $(".photo-close").click(function () {
        $(".photo-mode").animate({ opacity: 0, zIndex: -1 }, 300);
        $("body,html").css("overflow", "initial");
    });

    //相册封面图片等宽高
    $(".photo-img").height($(".photo-img").width());

    $(".photos-d-list li").height($(".photos-d-list li").width());

    //图片在父元素中垂直居中
    $("img.auto-top").each(function () {
        $(this).load(function () {
            $(this).css({
                "margin-top": ($(this).parent().height() - $(this).height()) / 2 + "px"
            });
        });
    });

    //自适应改进
    if (windowW < 640) {
        var fontSize = windowW / 6.4 + "px";
        $("body,html").css("font-size", fontSize);
    }
});


/*************************************************************************************************************
cookie操作 
**************************************************************************************************************/
function setCookie(name, value, exptime) {
    if (!exptime) {
        var days = 7; //此 cookie 将被保存 7 天
        exptime = new Date();    //new Date("December 31, 9998");
        exptime.setTime(exptime.getTime() + days * 24 * 60 * 60 * 1000);
    }
    document.cookie = name + "=" + escape(value) + ";path=/;expires=" + exptime.toGMTString();
}

function getCookie(name)//取cookies函数        
{
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;
}

function delCookie(name)//删除cookie
{
    var exp = new Date();
    exp.setTime(exp.getTime() - 5);
    var cval = getCookie(name);
    if (cval != null) document.cookie = name + "=" + cval + ";path=/;expires=" + exp.toGMTString();
}
$("#inMember").click(function () {

    location.href = "../member/index.aspx";
});
/*发表留言*/
$(".sendMessage").click(function (event) {
    var message = $.trim($("#txtMessage").val());
    var MemID = $("#txtMemID").val();
    var MemCard = $("#txtMemCard").val();
    if (message == "") {
        return;
    }
    //提交留言
    $.get("../../Service/AjaxService.ashx?Method=OnlineAsk",
    {
        "MemID": MemID,
        "MemCard": MemCard,
        "message": message
    }
    , function (text) {
        if (text == 0) {
            alert("系统错误，留言失败！");
        }
        else {


            var strHtml = "";

            strHtml += '<div class="t-center ask-time"><span>' + current() + '</span></div>'
                 + '<div class="ask-box customer-ask">'
                 + '<div class="f-right ask-img customer-img"><img src="images/head.png"/></div>'
                 + '<div class="f-right ask-talk customer-talk">'
                 + '<p>' + message + '</p>'
                 + '</div>'
                 + '</div>';

            $(".spHtml").append(strHtml);
            $("#txtMessage").val("");
            var content = document.getElementById('content');
         
            content.scrollTop = windowH;

        }

    }, "text")

});
function myrefresh() {
   
 var MemCard = $("#txtMemCard").val();
    //提交留言
 $.get("../../Service/AjaxService.ashx?Method=GetReplyInfo",
    {
        "MemCard": MemCard
    }
    , function (text) {

        if (text != "")
         {
            var json = JSON.parse(text);
            if (json.length > 0) {
                $.each(json, function (index, item) {
                    var strHtml = "";
                    strHtml += '<div class="t-center ask-time"><span>' + item.MessageTime + '</span></div>'
                                 + '<div class="ask-box self-ask">'
                                 + '<div class="f-left ask-img self-img"><img src="images/head01.png"/></div>'
                                 + '<div class="f-left ask-talk self-talk">'
                                 + '<p>' + item.MessageContent + '</p>'
                                 + '</div>'
                                 + '</div>';
                    $(".spHtml").append(strHtml);
                   
                    
                });
            }
        }
    }, "text")
    setTimeout('myrefresh()', 5000); 
}
function current() {
    var date = new Date();
    var year=date.getFullYear();
    var month = date.getMonth() + 1;
    if (month < 10) {
        month="0"+ month;
    }
    var day = date.getDate();
    if (day < 10) {
        day = "0" + day;
    }
    var hour = date.getHours();
    if (hour < 10) {
        hour = "0" + hour;
    }
    var min = date.getMinutes();
    if (min < 10) {
        min = "0" + min;
    }
    var sec = date.getSeconds();
    if (sec < 10) {
        sec = "0" + sec;
    }
    var datetime = year + "-" + month + "-" + day + ' ' + hour + ":" + min + ":" + sec;              
    return datetime;
}
$(".moreMessage").click(function (event) {
    location.href = "OnlineAsk.aspx?type=all";
});
$(".moreActive").click(function (event) {
    location.href = "Active.aspx?type=all";
});
$(".moreAlbum").click(function (event) {
    location.href = "photos.aspx?type=all";
});
$(".moreShop").click(function (event) {
    location.href = "queryStore.aspx?type=all";
});
$(".moreNews").click(function (event) {
    location.href = "News.aspx?type=all";
});
$(".moreProduct").click(function (event) {
    var ClassID = $("#ClassID").val();
    if (ClassID != "") {
        location.href = "productShow.aspx?type=all&ClassID="+ClassID;
    }
    else {
        location.href = "productShow.aspx?type=all";
    }
});
$(".morePhoto").click(function (event) {
    var AlbumID = $("#txtAlbumID").val();
    location.href = "photosDetail.aspx?type=all&AlbumID=" + AlbumID;
});
$(".searchShop").click(function (event) {
    var key = $.trim($("#txtKey").val());
    location.href = "queryStore.aspx?key=" + key;
});

$(".okSearchShop").click(function (event) {
    var pid = $(".province").val();
    var cid = $(".city1").val();
    var cyid = $(".city2").val();
    var key = $.trim($("#txtKey").val());
    location.href = "queryStore.aspx?key=" + key + "&pid=" + pid + "&cid=" + cid + "&cyid=" + cyid;
});
function Province() {

    $("#sltCity").empty();
    $("#sltCounty").empty();
    
    $("#sltCity").append("<option value=''>请选择</option>");
    $("#sltCounty").append("<option value=''>请选择</option>");
    var provinceID = $("#sltProvince").val();
    GetNextName(provinceID, "sltCity");
}
function City() {

    $("#sltCounty").empty();

    $("#sltCounty").append("<option value=''>请选择</option>");

    var CityID = $("#sltCity").val();
    GetNextName(CityID, "sltCounty");
}

function GetNextName(pid, controlID) {
   

    $.get("../../Service/AjaxService.ashx?Method=GetNextName",
    { "pid": pid }
    , function (text) {
        var json = JSON.parse(text);
        if (json != "") {
            for (var i = 0; i < json.length; i++) {
                $("#" + controlID).append("<option value='" + json[i].ID + "'>" + json[i].Name + "</option>");
            }
        }
    }, "text")



}

