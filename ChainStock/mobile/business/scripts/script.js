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
        window.history.back();
    });

//    // 注销登录
//    $(".logout").click(function () {
//        var logout = confirm("确认退出当前账号？");
//        if (logout == true) {
//            window.location.href = "login.aspx";
//        }
//    });

    //新增会员性别选择
    $(".line_group").find("a.line_btn").each(function () {
        $(this).click(function () {
            $(this).siblings("a.line_btn").removeClass("active");
            $(this).addClass("active");
        });
    });

    // 底部浮动更多按钮
    $(".fix-ch").children("a").click(function () {
        $(this).siblings(".foot-more").fadeToggle();
        $(this).parents(".fix-ch").siblings(".fix-ch").children(".foot-more").hide();
    });

    $(".fix-ch").children("a").mouseleave(function () {
        $(".foot-more").fadeOut();
    });
    //底部浮动方格自动布局
    (function () {
        var num = $(".foot-nav .fix-ch").length;
        var width = $(".foot-nav").outerWidth();
        var homeWidth = $(".fix-home").outerWidth();
        $(".fix-ch").css("width", (width - homeWidth-3) / num + "px");
    }());

    //积分兑换页面礼品导航交互
    //$(".gift-nav li").eq(0).find("a").addClass("active");
    $(".gift-nav li").click(function () {
        $(".gift-nav li").find("a").removeClass("active");
        $(this).find("a").addClass("active");
    });

    //积分兑换礼品增减控制器
    //(function () {
    //    var isBuy = false;
    //    var GoodsList = new Array(); 
    //    countGift();
    //}());

    //点击填写弹出城市选择模态框
    $(".queryCity,.show-address").click(function () {
        $(this).siblings(".city-mode").fadeIn();
        $("body,html").css("overflow", "hidden");
    });

    $(".city-mode").height(windowH);

    //城市选择模态框添加关闭按钮
    $(".city-query").append('<a href="##" class="close"><img src="images/close.png"/></a>');
    $(".city-query .close").css({
        "position": "absolute",
        "right": "0.1rem",
        "top": "0.1rem",
        "width": "0.3rem"
    }).click(function () {
        $(this).parents(".city-mode").fadeOut();
        $("body,html").css("overflow", "inherit");
    });

    //确认填写地址，将地址绑定到页面
    $(".ad-sure").click(function () {
        var ad = "";
        var detail = "";
        $(this).parents(".city-mode").find(".city-query select").each(function () {
            ad = ad + $(this).children("option:selected").text();
        });
        //将地址输入到页面
        $(this).parents(".city-mode").siblings(".show-address").text(ad).show();

        //隐藏“请选择”的按钮
        $(this).parents(".city-mode").siblings(".show-address").siblings("a").hide();
        $(this).parents(".city-mode").fadeOut();
        $("body,html").css("overflow", "inherit");
    });

    //自适应改进
    if (windowW < 640) {
        var fontSize = windowW / 6.4 + "px";
        $("body,html").css("font-size", fontSize);
    }



    //商家中心
    // $(".member-list img").load(function(){
    //     console.log($(window).height()+","+$("#content").height()+","+$(".header").height())
    //     $(".member-mesg").css("padding-bottom",$(window).height()-$("#content").height()-$(".header").height()-$(".member-mesg").height());
    // });

//    //快速消费确定
//    $("#sureConsumption").click(function () {
//        var value = $("input[name='conMoney']").val();
//        if (value > 0 && value != "") {
//            alert("消费成功！");
//            window.location.reload();
//        } else {
//            alert("请输入正确的金额！")
//        }
//    });

    //会员选择
    (function () {
        var checked, value;
        $(".card-sure").click(function () {
            checked = $(".query-member input[type='radio']:checked");
            if (checked.length > 0) {
                value = checked.siblings("span").text();
                $(this).parents(".city-mode").siblings(".show-address").text(value).show();
                //隐藏“请选择”的按钮
                $(this).parents(".city-mode").siblings(".show-address").siblings("a").hide();
                $(this).parents(".city-mode").fadeOut();
            } else {
                alert("请选择一个会员！")
            }

        });
    }());

    //商品消费
    //(function () {
    //    var isMember = true;
    //    $("#nowBuy").click(function () {
    //        isMember = $("#fitCon").hasClass("active") ? false : true;
    //        if (isMember) {
    //            //会员消费
    //            //点击模态框内立即结算
    //            $(".settlement").click(function () {
    //                alert("消费成功！");
    //                window.location.reload();
    //            });
    //        } else {
    //            // 散客消费
    //            //alert("消费成功！")
    //            //window.location.reload();
    //            $(".settlement").click(function () {
    //                alert("消费成功！");
    //                window.location.reload();
    //            });

    //        }
    //    });
    //}());

    // 安全退出
    $(".logout").click(function () {
        var logout = confirm("确认退出当前账号？");
        if (logout == true) {
            $.get("../../Service/AjaxService.ashx?Method=Wx_UserLoginOut",
        {}
        , function (text) {
            if (text == "1") {
                window.location.href = "login.aspx";
            }
        }, "text")
        }
    });
    //判断散客消费
    $(".line_btn").click(function () {
        var isMember = $("#fitCon").hasClass("active") ? false : true;
        if (!isMember) {
            $(this).parents(".line_group").siblings("#queryMember").hide();
            $(this).parents(".line_group").siblings("#memberName").hide();
            $(this).parents(".line_group").siblings("#consumptionWay").find("a").removeClass("active");
            $(this).parents(".line_group").siblings("#consumptionWay").find("a.xianjin").addClass("active");
            $(this).parents(".line_group").siblings("#queryMember").find("input[type='text']").val("");
        } else {
            $(this).parents(".line_group").siblings("#queryMember").show();
        }
    });

//    //计次消费-立即消费
//    $("a.count-settlement").click(function () {
//        alert("消费成功！");
//        window.location.reload();
//    });

    //输入会员卡号后显示姓名
//    $("input[name='memberCard']").bind("blur", function () {
//        if ($(this).val().length > 0) {
//            $(this).parents(".line_group").siblings("#memberName").show();
//        } else {
//            $(this).parents(".line_group").siblings("#memberName").hide();
//        }
//    });
    Login_ChangeValImg();

});

//获取图片验证码
function ChangeValImg() {
    $("#Login_ValImg").attr("src", "../../Service/ValidateImage.ashx?" + GetGuid());
}
//验证码
function Login_ChangeValImg() {
    ChangeValImg();
    setTimeout("Login_ChangeValImg()", 60 * 1000 * 3);
}
function GetGuid() {
    var now = new Date();
    var year = now.getFullYear(); //getFullYear getYear
    var month = now.getMonth();
    var date = now.getDate();
    var day = now.getDay();
    var hour = now.getHours();
    var minu = now.getMinutes();
    var sec = now.getSeconds();
    var mill = now.getMilliseconds();
    month = month + 1;
    if (month < 10) month = "0" + month;
    if (date < 10) date = "0" + date;
    if (hour < 10) hour = "0" + hour;
    if (minu < 10) minu = "0" + minu;
    if (sec < 10) sec = "0" + sec;
    var guid = month + date + hour + minu + sec + mill;
    return guid;
}

//选择性别
function SexChose(a) {
    //alert($(a).attr("title"));
    $("#sltMemSex").val($(this).attr("title"));
}


//登录
$("#loginSumbit").click(function () {
    var uid = $.trim($("#username").val());
    var pwd = $.trim($("#pwd").val());
    var Yanzheng = $.trim($("#YanZhengMa").val());
    if (uid == "") {
        alert("卡号不能为空！")
        return;
    }
    $.get("../../Service/AjaxService.ashx?Method=WeiXinShopLogin",
{
    "userid": uid,
    "pwd": pwd,
    "Yanzheng": Yanzheng
}
, function (text) {
    if (text == "2") {
        location.href = "/mobile/business/index.aspx";
        return;
    }
    else if (text == "3") {
        alert("验证码错误！"); Login_ChangeValImg();
        return;
    }
    else if (text == "3") {
        alert("验证码错误！");
        Login_ChangeValImg();
        return;
    } else if (text == "4") {
        alert('您的卡号已锁定，暂时无法登录');
        return;
    }
    else {
        $("#memname").val("");
        $("#telnumber").val("");
        $("#YanZhengMa").val("");
        alert("登录失败，用户名或密码错误！");
        Login_ChangeValImg();
    }
    //$("#goBack").click();
}, "text")
})

//查询推荐人信息
$('#txtMemRecommendCard').change(function (e) {
    var memID = $("#txtMemID").val();     
    if ($(this).val() != "") {
        $.get("../../Service/AjaxService.ashx?Method=Wx_GetMem",
         {
             "memCard": $.trim($("#txtMemRecommendCard").val()),
             "userShopID": $.trim($("#sltShop").val())
         }, function (json) {
             if (json.length > 0) {
                 $.each(json, function (index,item) {
                     if (item.MemID == memID) {
                         $("#txtMemRecommendMsg").html("推荐人不能设置为自己");
                         $("#txtMemRecommendCard").val("");
                         $("#txtMemRecommendID").val("");
                         $("#txtMemRecommendName").val("");
                         $("#divRecommend").css("display", "none");
                         return;
                     }
                     if (item.MemRecommendID == memID) {
                         alert("卡号：" + $('#txtMemRecommendCard').val() + "的推荐人已设定为当前会员,不可互相推荐");
                         $("#txtMemRecommendMsg").html("");
                         $("#txtMemRecommendCard").val("");
                         $("#txtMemRecommendID").val("");
                         $("#txtMemRecommendName").val("");
                         $("#divRecommend").css("display", "none");
                         return;
                     }
                     $("#txtMemRecommendName").val(item.MemName);
                     $("#txtMemRecommendID").val(item.MemID);
                     $("#txtMemRecommendMsg").html("姓名：" + item.MemName);
                     $("#divRecommend").css("display", "block");
                 });              
             } else {
                 $("#txtMemRecommendMsg").html("未找到指定的会员！");
                 $("#txtMemRecommendName").val("");
                 ("#txtMemRecommendID").val("0");
                 $("#divRecommend").css("display", "block");
             }                      
            
         }, "json");
    } else {        
        $("#txtMemRecommendName").val("");
        $("#txtMemRecommendID").val("0");
        $("#txtMemRecommendMsg").html("");
        $("#divRecommend").css("display", "none");
    }
});

//会员办卡
$("#btnMemSave").click(function () {
   // alert(1);
    var strErrorMsg = "";
    var type = "MemAdd";
    var memid = $("#txtMemID").val();
    var memCard = $.trim($("#txtMemCard").val());
    var memName = $("#txtMemName").val();
    var memCardNumber = $.trim($("#txtCardNumber").val());
    if (memCard=="") {
        alert("请输入会员卡号!");
        return;
    }
    //if (!memCard.IsNumber()) {
    //    strErrorMsg += "<li>会员卡号应该是由数字组成的一个字符串;</li>";
    //}
    //if (memCard.length < 4) {
    //    strErrorMsg += "<li>会员卡号必须4~20位;</li>";
    //}
    //if (type == "MemAdd" && memCardNumber != "" && !memCardNumber.IsNumber()) {
    //    strErrorMsg += "<li>会员卡面号码应该是由数字组成的一个字符串;</li>";
    //}
    //if (type == "MemAdd" && memCardNumber != "" && memCardNumber.length < 4) {
    //    strErrorMsg += "<li>会员卡面号码必须4~20位;</li>";
    //}   
    //var memMobile = $.trim($("#txtMemMobile").val());

    //if (memMobile != "" && !memMobile.IsMobile()) {
    //    strErrorMsg += "<li>手机号码格式输入错误;</li>";
    //}

    var memBirthday = $.trim($("#txtMemBirthday").val());
    if (memBirthday != "") {
        memBirthday = memBirthday.replace(/-/g, '/');
        memBirthday = new Date(memBirthday);
        var nowDate = new Date();
        if (memBirthday == null) {
            strErrorMsg += "输入的生日格式不正确;";
        }
        else {
            if (memBirthday > nowDate) {
                strErrorMsg += "输入的生日不可大于当期日期;";
            }
        }
    }
    if ($("#sltMemLevelID").val() == "") {
        strErrorMsg += "必须选择会员等级;";
    }
    var memPastTime = $.trim($("#txtMemPastTime").val());
    if (memPastTime != "") {
        memPastTime = memPastTime.replace(/-/g, '/');
        memPastTime = new Date(memPastTime);
        var nowDate = new Date();
        if (memPastTime == null) {
            strErrorMsg += "输入的过期日期格式不正确;";
        }
        else {
            if (memPastTime < nowDate) {
                strErrorMsg += "输入的过期日期不可小于或等于当前日期;";
            }
        }
    }
    var RecommendCard = $("#txtMemRecommendCard").val();
    var RecommendCardID = $("#txtMemRecommendID").val();
    if (RecommendCard != "" && RecommendCardID == "") {
        strErrorMsg += "输入的推荐人不存在，请重新输入;";
    }
    if (strErrorMsg != "") {
        strErrorMsg = "操作出现以下错误，请核查后重试！\r\n" + strErrorMsg + "";
        alert(strErrorMsg);
    }
    $.post("../../Service/AjaxService.ashx?Method=MemAdd",
          {
              "txtMemID": memid,
              "txtMemCard": memCard,
              "txtMemName": memName,
              "txtMemPassword": "123456",
              "sltMemSex": $("#sltMemSex").val(),
              "txtMemIdentityCard": $("#txtMemIdentityCard").val(),
              "txtMemBirthday": $("#txtMemBirthday").val(),
              "txtMemMobile": $("#txtMemMobile").val(),
              "txtMemPoint": $("#txtMemPoint").val(),
              "txtMemMoney": $("#txtMemMoney").val(),
              "txtMemEmail": $("#txtMemEmail").val(),
              "txtMemAddress": "",
              "sltMemState": "0",
              "sltMemLevelID": $("#sltMemLevelID").val(),
              "sltShop": $("#sltShop").val(),
              "txtMemRecommendID": $("#txtMemRecommendID").val(),
              "txtMemCreateTime": $("#txtMemCreateTime").val(),
              "txtMemPastTime": $("#txtMemPastTime").val(),
              "txtMemPhoto": "",
              "txtMemRemark": $("#txtMemRemark").val(),
              "sltMemUserID": $("#sltMemUserID").val(),
              "txtTelephone": $("#txtTelephone").val(),
              "chkSMS": $("#chkSMS").val(),
              "ucSysArea$sltProvince": "",
              "ucSysArea$sltCity": "",
              "ucSysArea$sltCounty": "",
              "ucSysArea$sltVillage": "",
              "txtCardNumber": $("#txtCardNumber").val(),
              "txtRegisterStaffMoney": "",
              "sltStaff": "",
              "hidImgSrc":""
          },
         function (json) {
             switch (json) {
                 case -1:
                     alert("此卡号已经在系统中存在，请重新输入卡号，然后重试！");
                     break;
                 case -2:
                     alert("此手机号码已经在系统中存在，请重新输入手机号，然后重试！");
                     break;
                 case -6:
                     alert("此卡面号码已经在系统中存在，请重新输入卡面号码，然后重试！");
                     break;
                 case -3:
                     alert("保存成功，短信余额不足，不能发送短信，请充值短信！");
                     location.href = "../business/index.aspx";
                     break;
                 case 0:
                     alert("系统异常，未保存数据，请再次点击保存！");
                     break;
                 case -4:
                     alert("该会员已经存在其他数据异动，不能修改创建时间！");
                     break;
                 case -5:
                     alert("系统错误 请与系统管理员联系！");
                     break;

                 case -7:
                     alert("该卡不在发卡范围内，请与总店联系！");
                     break;
                 case -8:
                     alert("该店铺剩余可用积分不足，请重新填写！");
                     break;
                 default:
                     alert("办卡成功！会员初始密码为：123456，请尽快前往会员中心修改密码!")
                     location.href = "../business/index.aspx";
                     break;
             }
         }, "json");

});

//获取会员
$("#goodsConsumptionMemberCard").change(function () {
    $("#goodsConsumptionMemName").html("");
    $("#goodsConsumptionMemMoney").html("");
    $("#hidMemMoney").val("");
    $("#hidMemisPast").val("");
    $("#hidMemState").val("");
    $("#hidMemID").val("");
    var membercard = $("#goodsConsumptionMemberCard").val();
    var shopID = $("#hidShopID").val();
    if (membercard == "") {
        alert("请输入会员卡号");
        $("#memberName").css("display", "none");
        return;
    } else {
        $.post("../../Service/AjaxService.ashx?Method=GetQueryMem",
       {
           "size": 10,
           "index": 1,
           "memCard": membercard,
           "shopID": shopID
       },
       function (json) {
           if (json.RecordCount > 0) {
               $.each(json.List, function (index, item) {
                   $("#goodsConsumptionMemName").html(item.MemName);
                   $("#goodsConsumptionMemMoney").html(item.MemMoney);
                   $("#hidMemMoney").val(item.MemMoney);
                   $("#hidMemisPast").val(item.MemIsPast);
                   $("#hidMemState").val(item.MemState);
                   $("#hidMemID").val(item.MemID);
               })
           }
           else {
               $("#goodsConsumptionMemName").html("未找到该会员");
               $("#goodsConsumptionMemMoney").html("0");
               $("#hidMemMoney").val("0");
               $("#hidMemisPast").val("");
               $("#hidMemState").val("");
               $("#hidMemID").val("0");
           }
       }, "json");
    }   
    $("#memberName").css("display","block");
});

var isBuy = false;
var GoodsList = new Array();

$(".plusShow").click(function () {

    var shopID = $("#txtShopID").val();
    var member = $("#ismember").attr("class");
    var nomember = $("#nomember").attr("class");

    var ismember = 1;
    if (member == "line_btn active") {
        ismember = 1; //是会员
    }
    else {
        ismember = 0;
    }

    var memid = $("#txtMemID").val();
    if (memid == "") {
        memid = 0;
    }

    if (ismember == 1 && (memid == "" || memid == "0")) {
        alert("请先查询出会员信息！");
        return;
    }
    else {
        var value = 1;
        $(this).siblings(".giftTxt").val(value);



        $(this).hide();
        $(this).siblings(".reduceBtn,.plusBtn,.giftTxt").show();
        var pointPercent = 0;
        var discount = 0;
        var point = 0;
        var discountmoney = 0;
        var goodsid = $(this).siblings(".goodsid").text();
        var price = $(this).siblings(".goodsPrice").text();
        var goodspoint = parseInt($(this).siblings(".goodsPoint").text());
        var goodsType = $(this).siblings(".goodsType").text();

        if (memid != 0) {
            $.get("../../Service/AjaxService.ashx?Method=Wx_GetGoodsInfo",
         { "goodsid": goodsid,
             "memid": memid,
             "shopid": shopID
         }, function (json) {

             if (json != null) {
                 if (json == "0") {
                     alert("系统错误！");
                 }
                 else {

                     pointPercent = json.pointPercent;
                     discount = json.discount;
                   
                     discountmoney = parseFloat(parseFloat(price * value) * parseFloat(discount)).toFixed(2);
                     if (goodspoint == 0) {
                         point = parseInt(parseFloat(discountmoney) / parseFloat(pointPercent));
                     }
                     else {
                         point = parseInt(goodspoint) * value;
                     }
                 


                     var goods = new Object();
                     goods.goodsid = goodsid;
                     goods.count = value;
                     goods.price = price;
                     goods.pointPercent = pointPercent;
                     goods.discount = discount;
                     goods.discountmoney = discountmoney;
                     goods.goodspoint = goodspoint;
                     goods.point = point;
                     goods.goodsType = goodsType;
                     GoodsList.push(goods);

                     countGoods();
                     ShowMsg(GoodsList)
                 }
             }

         }, "json");
        }
        else {
            point = 0;
            pointPercent = 0;
            discount = 1;
            discountmoney = parseFloat(parseFloat(price * value) * parseFloat(discount)).toFixed(2);

            var goods = new Object();
            goods.goodsid = goodsid;
            goods.count = value;
            goods.price = price;
            goods.pointPercent = pointPercent;
            goods.discount = discount;
            goods.discountmoney = discountmoney;
            goods.goodspoint = goodspoint;
            goods.point = point;
            goods.goodsType = goodsType;
            GoodsList.push(goods);

            countGoods();
            ShowMsg(GoodsList)
        }

    }


});

$(".plusBtn").click(function () {
    var memid = $("#txtMemID").val();
    if (memid == "") {
        memid = 0;
    }
    //将要消费的数量
    var value = parseInt($(this).siblings(".giftTxt").val());
    var goodsid = $(this).siblings(".goodsid").text();

    value++;
    $(this).siblings(".giftTxt").val(value);


    for (var i = 0; i < GoodsList.length; i++) {
        if (GoodsList[i].goodsid == goodsid && value > 0) {
            GoodsList[i].count = value;
            GoodsList[i].discountmoney = parseFloat(parseFloat(GoodsList[i].price * value) * parseFloat(GoodsList[i].discount)).toFixed(2);
            if (memid != 0) {
                if (GoodsList[i].goodspoint == 0) {
                    GoodsList[i].point = parseInt(parseFloat(GoodsList[i].discountmoney) / parseFloat(GoodsList[i].pointPercent));
                }
                else {
                    GoodsList[i].point = parseInt(GoodsList[i].goodspoint * value);
                }
            }


        }
    }
    countGoods();
    ShowMsg(GoodsList)
});



$(".reduceBtn").click(function () {

    var value = $(this).siblings(".giftTxt").val();
    var goodsid = $(this).siblings(".goodsid").text();

    var memid = $("#txtMemID").val();
    if (memid == "") {
        memid = 0;
    }

    if (value > 0) {
        value = $(this).siblings(".giftTxt").val();
        value--;

        $(this).siblings(".giftTxt").val(value);
        for (var i = 0; i < GoodsList.length; i++) {
            if (GoodsList[i].goodsid == goodsid) {
            
                GoodsList[i].count = value;
                GoodsList[i].discountmoney = parseFloat(parseFloat(GoodsList[i].price * value) * parseFloat(GoodsList[i].discount)).toFixed(2);
                if (memid != 0) {
                    if (GoodsList[i].goodspoint == 0) {
                        GoodsList[i].point = parseInt(parseFloat(GoodsList[i].discountmoney) / parseFloat(GoodsList[i].pointPercent));
                    }
                    else {
                        GoodsList[i].point = parseInt(GoodsList[i].goodspoint * value);
                    }
                }


            }
        }

        if (value == 0) {
            var newGoodsList = new Array();

            for (var i = 0; i < GoodsList.length; i++) {
                
                if (GoodsList[i].goodsid != goodsid) {
                    var goods = new Object();
                    goods.goodsid = GoodsList[i].goodsid;
                    goods.count = GoodsList[i].count;
                    goods.price = GoodsList[i].price;
                    goods.pointPercent = GoodsList[i].pointPercent;
                    goods.discount = GoodsList[i].discount;
                    goods.discountmoney = GoodsList[i].discountmoney;
                    goods.goodspoint = GoodsList[i].goodspoint;
                    goods.point = GoodsList[i].point;
                    goods.goodsType = GoodsList[i].goodsType;
                    newGoodsList.push(goods);
                }
               
            }
            GoodsList = newGoodsList;
          
            $(this).siblings("a,input").hide();
            $(this).hide();
            $(this).siblings(".plusShow").show();


        }
        
    }




    countGoods();
    ShowMsg(GoodsList)

});

function ShowMsg(GoodsList) {
    var info = "";
    if (GoodsList != null) {
        for (var i = 0; i < GoodsList.length; i++) {
            info += " id:" + GoodsList[i].goodsid + " count:" + GoodsList[i].count + ";point:" + GoodsList[i].point + " money:" + GoodsList[i].discountmoney;
        }
    }
  
   
}
$("#nomember").click(function () {
    $("#memCard").attr("disabled", "disabled");
    $("#spMemName").html("");
    $("#memberName").hide();
    $("#memCard").val("");
    $("#memberCard").hide();

    $("#cash").removeAttr("class");
    $("#cash").attr("class", "line_btn active");
    $("#balance").removeAttr("class");
    $("#balance").attr("disabled", "disabled");


    //  $(".reduceBtn").siblings("a,input").hide();
    $(".reduceBtn").hide();
    $(".plusBtn").hide();
    $(".plusShow").show();
    $(".giftTxt").hide();
    $(".giftTxt").html(0);
    GoodsList = new Array();
    countGoods();
});
$("#ismember").click(function () {
    $("#memCard").removeAttr("disabled");
    $("#spMemName").html("");
    $("#memberName").hide();
    $("#memCard").val("");
    $("#memberCard").show();
    $("#cash").removeAttr("class");
    $("#cash").attr("class", "line_btn");
    $("#balance").removeAttr("class");
    $("#balance").attr("class", "line_btn active");

    $("#memCard").focus();

    // $(".reduceBtn").siblings("a,input").hide();
    $(".reduceBtn").hide();
    $(".plusBtn").hide();
    $(".plusShow").show();
    $(".giftTxt").hide();
    $(".giftTxt").html(0);
    GoodsList = new Array();
    countGoods();
});

$(".buy-ctrl a").click(function () {

    countGoods();
});

function countGoods() {
    var memid = $("#txtMemID").val();
    var num = 0;
    var total = 0;
    var point = 0;

    if (GoodsList != null) {
        for (var i = 0; i < GoodsList.length; i++) {
            num = parseInt(num) + parseInt(GoodsList[i].count);
            total = parseFloat(total) + parseFloat(GoodsList[i].discountmoney);
            point = parseInt(point) + parseInt(GoodsList[i].point);
           
        }
    }
    $(".gift-num").text(num);
    $(".gift-total").text(total);
    $(".count-total").text(num);
    $(".goods-point").text(point);
}
function GetGoodsPoint() {
    var shopID = $("#hidShopID").val();
    var memid = $("#txtMemID").val();
    if (memid == "") {
        memid = 0;
    }

    $.get("../../Service/AjaxService.ashx?Method=Wx_GetGoodsPoint",
         {
             "memid": memid,
             "data": GoodsList,
             "count": GoodsList.length,
             "shopid": shopID
         }, function (json) {
             $(".goods-point").text(json);


         }, "json");

}
$(".moreGoods").click(function (event) {
    location.href = "goodsConsumption.aspx?type=all";
});

//点击填写弹出城市选择模态框
$("#nowBuy").click(function () {
    var member = $("#ismember").attr("class");
    var nomember = $("#nomember").attr("class");

    var ismember = 1;
    if (member == "line_btn active") {
        ismember = 1; //是会员
    }
    else {
        ismember = 0;
    }

    var memID = $("#txtMemID").val();

    if ((memID == "" || memID == "0") && ismember == 1) {
        alert("未获取到会员信息，无法进行商品购买！");
        return;
    }
    if (GoodsList.length == 0) {
        alert("请先选择商品，再进行结算！");
        return;
    }
    if (memID == "" || memID == "0") {
        $("#balance").attr("style", "display:none");
    }

    $(this).siblings(".city-mode").fadeIn();
    $("body,html").css("overflow", "hidden");


});
//商品购买
$("#goodsExpenseSure").click(function () {
    var member = $("#ismember").attr("class");
    var nomember = $("#nomember").attr("class");

    var ismember = 1;
    if (member == "line_btn active") {
        ismember = 1; //是会员
    }
    else {
        ismember = 0;
    }

 
    var memID = $("#txtMemID").val();
    var memMoney = $("#txtMemMoney").val();
    var totalMoney = $(".gift-total").text();
    var shopID = $("#txtShopID").val();
    var userID = $("#hidUserID").val();
    var PayType = $("#hidPayType").val();
    if ((memID == "" || memID == "0") && ismember == 1) {
        alert("未获取到会员信息，请先查询出会员信息！");
        return;
    }
    if (GoodsList.length == 0) {
        alert("请先添加商品，再进行购买！");
        return;
    }

    var balance = $("#balance").attr("class");
    var cash = $("#cash").attr("class");


    if (balance == "line_btn active") {
        PayType = "0"; //余额消费
    }
    else {
        PayType = "1";
    }


    $.post("../../Service/AjaxService.ashx?Method=Wx_GoodsExpense",
          {

              "memid": memID,
              "orderAccount": $("#spOrderAccount").html(),
              "totalMoney": totalMoney,
              "sumNumber": $(".gift-num").html(),
              "count": GoodsList.length,
              "data": GoodsList,
              "shopID": shopID,
              "userID": userID,
              "payType": PayType,
              "point": $(".goods-point").html(),
              "remark": ""
          }, function (json) {

              //var json = JSON.parse(text);


              switch (json) {
                  case 0:
                      alert("系统异常，未保存数据，请再次点击保存！");

                      break;
                  case -1:
                      alert("系统错误 请与系统管理员联系！");

                      break;
                  case -2:
                      alert("消费成功！短信余额不足，不能发送短信，请充值短信！");
                      location.href = "goodsConsumption.aspx";
                      break;
                  case -3:
                      alert("挂单成功！");

                      break;
                  case -4:
                      alert("请勿重复提交订单！");

                      break;
                  case -5:
                      alert("发送短信失败,本店拥有的短信量不足请与总店联系！");

                      break;
                  case -6:
                      alert("本店积分不足无法消费，请与总店联系！");

                      break;
                  case -7:
                      alert("会员余额不足！");

                      break;
                  default:
                      alert("消费成功！" + json.strUpdateMemLevel);
                      location.href = "goodsConsumption.aspx";
                      break;

              }
          }, "json");
    // }
});