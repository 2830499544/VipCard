console.log("%c技术支持: 成都智络科技有限公司", "color:#ddd");

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
    //            window.location.href = "login.html";
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
        $(".fix-ch").css("width", (width - homeWidth - 3) / num + "px");
    } ());



    //点击填写弹出城市选择模态框
    $(".queryCity,.show-address").click(function () {
        $(this).siblings(".city-mode").fadeIn();
    });
    $(".city-mode").height(windowH);

    //城市选择模态框添加关闭按钮
    $(".city-query").append('<a href="##" class="close"><img src="images/close.png"/></a>');
    $(".city-query .close").css({
        "position": "absolute",
        "right": "0.1rem",
        "top": "0.1rem",
        "width":"0.3rem"
    }).click(function () {
        $(this).parents(".city-mode").fadeOut();
    });

    //自适应改进
    if (windowW < 640) {
        var fontSize = windowW / 6.4 + "px";
        $("body,html").css("font-size", fontSize);
    }

});


// 验证手机号
function isMobile(phone) {
    var pattern = /^1[34578]\d{9}$/;
    return pattern.test(phone);
}

// 验证身份证 
function isCardNo(card) {
    var pattern = /(^\d{15}$)|(^\d{18}$)|(^\d{17}(\d|X|x)$)/;
    return pattern.test(card);
} 

$("#inWeb").click(function () {

    location.href = "../website/index.aspx";
});



// 注销登录
$(".logout").click(function () {
    var logout = confirm("确认退出当前账号？");
    if (logout == true) {
        $.get("../../../Service/AjaxService.ashx?Method=Wx_MemLoginOut",
        {}
        , function (text) {
            if (text == "1") {
                window.location.href = "login.aspx";
            }
        }, "text")
    }
});

// 登录
$(".loginBtn").click(function () {
    var memcard = $.trim($("#memcard").val());
    var pwd = $.trim($("#pwd").val());
    if (memcard == "") {
        alert("会员卡号不能为空！")
        return;
    }
    $.get("../../../Service/AjaxService.ashx?Method=Wx_MemLogin",
    {
        "memcard": memcard,
        "pwd": pwd
    }
    , function (text) {
        if (text == "-1") {
            $("#memcard").val("");
            $("#pwd").val("");
            alert("登录失败，用户名或密码错误！");
            return;
        } else if (text == "2") {
            alert('您的卡号已锁定，暂时无法登录！');
            return;
        } else if (text == "3") {
            alert('您的卡号已过期，暂时无法登录！');
            return;
        }
        else if (text == "1") {
            location.href = "index.aspx";
        }
    }, "text")
});



var InterValObj; //timer变量，控制时间
var count = 60; //间隔函数，1秒执行
var curCount; //当前剩余秒数
function sendmodifyPwdMessage() {

    var mobile = $.trim($("#mobile").val());
    if (mobile == "") { alert("请输入您的手机号码!"); return; }

    if (mobile != "" && mobile.length != 11) {
        alert("您输入的手机号码格式有误");
        return;
    }
    $.get("../../Service/AjaxService.ashx?Method=IsExitRegistMobile",
        {
            "mobile": mobile
        }, function (text) {

            switch (text) {
                case "-1":
                    alert("系统错误,请联系系统管理员！");
                    break;
                case "0":
                    alert("该手机号码未注册！请重新输入手机号码！");
                    break;
                default:
                    $("#txtMemID").val(text);
                    curCount = count;
                    //设置button效果，开始计时
                    $("#mobile").attr("disabled", "true");
                    $("#getCode").attr("disabled", "true");
                    $("#getCode").html(curCount + "s后再次发送"); ;
                    InterValObj = window.setInterval(SetRemainTime, 1000); //启动计时器，1秒执行一次 
                    $.get("../../Service/AjaxService.ashx?Method=WX_SendSMSCode",
                { "mobile": mobile
                }, function (text) {

                    switch (text) {
                        case "0":
                            alert("网络异常，请重新发送验证码！");
                            break;
                        case "-1":
                            alert("发送验证码失败！");
                            break;
                        default:
                            //alert("发送验证码成功，请查收短信！");
                            $("#spCode").html(text);
                            $("#smscode").val(text);
                            break;
                    }
                }, "text");
                    break;
            }
        }, "text");

}
//timer处理函数
function SetRemainTime() {

    if (curCount == 0) {
        window.clearInterval(InterValObj); //停止计时器
        $("#getCode").removeAttr("disabled"); //启用按钮
        $("#mobile").removeAttr("disabled"); //启用按钮
        $("#getCode").html("重新发送验证码");
    }
    else {
        curCount--;
        $("#getCode").html("" + curCount + "s后再次发送") ;
    }
}
$("#changePwd").click(function () {

    var pwd = $.trim($("#pwd").val());
    var pwdok = $.trim($("#pwdok").val());
    var memid = $("#txtMemID").val();
    var mobile = $.trim($("#mobile").val());
    var smscode = $.trim($("#smscode").val());
    var code = $("#spCode").html();
    if (mobile == "") { alert("请输入您的手机号码!"); return; }

    if (mobile != "" && mobile.length != 11) {
        alert("您输入的手机号码格式有误");
        return;
    }
    if (smscode == "") {
        alert("请输入验证码！");
        return;
    }
    if (code != smscode) {
        alert("验证码错误！");
        return;
    }
    if (pwd == "") {
        alert("请输入密码！");
        return;
    }
    if (pwd != pwdok) {
        alert("两次输入的密码不一致，请重新输入！");
        return;
    }
    $.get("../../Service/AjaxService.ashx?Method=Wx_UpdateMemPwd",
        { "memid": memid, "pwd": pwd
        }, function (text) {

            switch (text) {
                case "0":
                    alert("密码修改失败！");
                    break;

                default:
                    location.href = "modifyPasswordSuccess.aspx";
                    break;
            }
        }, "text");

    });
//注册会员
 $("#memRegister").click(function () {

        var pwd = $.trim($("#pwd").val());
        var pwdok = $.trim($("#pwdok").val());
        var mobile = $.trim($("#mobile").val());
        var smscode = $.trim($("#smscode").val());
        var memname = $.trim($("#memname").val());
        var code = $("#spCode").html();
        if (memname == "") {
            alert("请输入姓名！");
            return;
        }
        if (mobile == "") { alert("请输入您的手机号码!"); return; }

        if (mobile != "" && mobile.length != 11) {
            alert("您输入的手机号码格式有误");
            return;
        }
        if (smscode == "") {
            alert("请输入验证码！");
            return;
        }
        if (code != smscode) {
            alert("验证码错误！");
            return;
        }
        if (pwd == "") {
            alert("请输入密码！");
            return;
        }
        if (pwd != pwdok) {
            alert("两次输入的密码不一致，请重新输入！");
            return;
        }
        $.get("../../Service/AjaxService.ashx?Method=Wx_MemRegister",
        { "mobile": mobile, "pwd": pwd, "memname": memname
        }, function (text) {

            switch (text) {
                case "0":
                    alert("注册失败！");
                    break;

                default:
                    location.href = "registerSuccess.aspx";
                    break;
            }
        }, "text");

    });
 //发送注册验证码
function sendregisterMessage() {

    var mobile = $.trim($("#mobile").val());
    if (mobile == "") { alert("请输入您的手机号码!"); return; }

    if (mobile != "" && mobile.length != 11) {
        alert("您输入的手机号码格式有误");
        return;
    }
    $.get("../../Service/AjaxService.ashx?Method=IsExitRegistMobile",
    {
        "mobile": mobile
    }, function (text) {
        
        switch (text) {
            case "-1":
                alert("系统错误,请联系系统管理员！");
                break;
            case "0":
                curCount = count;
                //设置button效果，开始计时
                $("#mobile").attr("disabled", "true");
                $("#getCode").attr("disabled", "true");
                $("#getCode").html(curCount + "s后再次发送"); ;
                InterValObj = window.setInterval(SetRemainTime, 1000); //启动计时器，1秒执行一次 
                $.get("../../Service/AjaxService.ashx?Method=WX_SendSMSCode",
            { "mobile": mobile
            }, function (text) {

                switch (text) {
                    case "0":
                        alert("网络异常，请重新发送验证码！");
                        break;
                    case "-1":
                        alert("发送验证码失败！");
                        break;
                    default:
                        //alert("发送验证码成功，请查收短信！");
                        $("#spCode").html(text);
                        $("#smscode").val(text);
                        break;
                }
            }, "text");
            break;
            default:
                alert("该手机号码已注册！请重新输入手机号码！");
                break;
        }
    }, "text");
}
//绑定会员卡
$("#bindCard").click(function () {
    var url = $("#txtUrl").val();   
    if (url != "") {
        location.href = url;
    }

});
//在线充值

$("#onlineRecharge").click(function () {

    var url = $("#txtRechargeUrl").val();

    if (url != "") {
        location.href = url;
    }

});
//确定绑定会员卡
$("#bindCardOk").click(function () {

    var openid = $("#txtOpenID").val();
    var memid = $("#txtMemID").val();
    if (openid == "") {
        alert("未获取到微信用户信息，无法进行绑定！");
        return;
    }
    if (memid == "") {
        alert("未获取到会员信息，无法进行绑定！");
        return;
    }
  
    $.get("../../Service/AjaxService.ashx?Method=UpdateMemWeiXinCard",
        { "openid": openid, "memid": memid
        }, function (text) {
     
            switch (text) {
                case "0":
                    alert("绑定失败！");
                    break;

                default:
                    location.href = "bindingSuccess.aspx";
                    break;
            }
        }, "text");

    });
//解绑
$("#cancelBindCard").click(function () {

    var memid = $("#txtMemID").val();
    if (memid == "") {
        alert("未获取到会员信息，无法进行解绑！");
        return;
    }
    var result = confirm("确定进行解绑操作吗？");
    if (result == true) {

        $.get("../../Service/AjaxService.ashx?Method=UpdateMemWeiXinCard",
        { "openid": "", "memid": memid
        }, function (text) {

            switch (text) {
                case "0":
                    alert("解绑失败！");
                    break;

                default:
                    location.href = "cancelBindingSuccess.aspx";
                    break;
            }
        }, "text");
    }

});


$(".moreGift").click(function (event) {
    location.href = "pointExchange.aspx?type=all";
});
$(".moreExchange").click(function (event) {
    location.href = "pointExchangeRecord.aspx?type=all";
});
//立即兑换
$("#pointExchangeOk").click(function () {
    var sumPoint = parseInt($(".gift-total").html());
    var memPoint = parseInt($(".in-total").html());

    if (memPoint < sumPoint) {
        alert("对不起，您的当前积分已不足！"); return;
    }
    var memid = $("#txtMemID").val();
    if (memid == "") {
        alert("未获取到会员信息，无法进行兑换礼品！");
        return;
    }
    if (GiftList.length == 0) {
        alert("请先添加礼品！");
        return;
    }
    var str = JSON.stringify(GiftList);
    str = escape(str);
    var url = "pointExchangeSure.aspx?GiftList=" + str + "";
    location.href = url;
});

var isBuy = false;
var GiftList = new Array();
$(".plusShow").click(function () {
    var value = 1;
    $(this).siblings(".giftTxt").val(value);
    $(this).hide();
    $(this).siblings(".reduceBtn,.plusBtn,.giftTxt").show();

    var id = $(this).siblings(".giftid").html();
    var name = $(this).siblings(".giftname").html();
    var point = $(this).siblings(".giftpoint").html();
    var image = $(this).siblings(".giftimage").html();
    var gift = new Object();
    gift.id = id;
    gift.count = value;
    gift.name = name;
    gift.point = point;
    gift.image = image;

    GiftList.push(gift);
  
});

$(".plusBtn").click(function () {
    var value = $(this).siblings(".giftTxt").val();
    var id = $(this).siblings(".giftid").html();

    value++;
    $(this).siblings(".giftTxt").val(value);

    var id = $(this).siblings(".giftid").html();

    for (var i = 0; i < GiftList.length; i++) {
        if (GiftList[i].id == id && value > 0) {
            GiftList[i].count = value;
        }
    }

});

$(".reduceBtn").click(function () {
    var value = $(this).siblings(".giftTxt").val();
    var id = $(this).siblings(".giftid").val();
    if (value > 0) {
        value = $(this).siblings(".giftTxt").val()
        value--;
        $(this).siblings(".giftTxt").val(value);

        for (var i = 0; i < GiftList.length; i++) {
            if (GiftList[i].id == id) {
                GiftList[i].count = value;
            }
        }

        if (value == 0) {
            $(this).siblings("a,input").hide();
            $(this).hide();
            $(this).siblings(".plusShow").show();
        }
    }
    if (value == 0) {
        for (var i = 0; i < GiftList.length; i++) {
            if (GiftList[i].id == id) {
                GiftList = remove(GiftList, "id", id);
            }
        }
    }
 

});

$(".buy-ctrl a").click(function () {
    countGift();
});



countGift();

function countGift() {
    var num = 0;
    var total = 0;
    $(".gift-list li").each(function (index, element) {
        var danNum = $(this).find(".buy-ctrl input").val();
        var danIntegral = $(this).find(".gift-integral").text();
        num = num + parseInt(danNum);
        total = total + parseInt(danIntegral * danNum);
    });
    // console.log(num)
    $(".gift-num").text(num);
    $(".gift-total").text(total);

    if ($(".in-total").text() - total < 0) {
       // alert("对不起，您的当前积分已不足！")
       // return;
    }
    else {
        //$(".in-balance").text($(".in-total").text() - total);
    }
   

}



$("#searchGift").click(function () {
    var key = $("#txtKey").val();
    location.href = "pointExchange.aspx?Key=" + key;
});
//积分兑换
$("#pointExchangeSure").click(function () {

   
    var memid = $("#txtMemID").val();
    if (memid == "") {
        alert("未获取到会员信息，无法进行兑换礼品！");
        return;
    }
    if (GiftList.length == 0) {
        alert("请先添加礼品！");
        return;
    }
    var addressID = $("#txtAddressID").val();
    if (addressID == "") {
        alert("请添加收货地址！");
        return;
    }
    var memname = $("#memname").html();
    var mobile = $("#mobile").html();
    var address = $("#address").html(); 
    var result = confirm("确定兑换礼品吗？");

    if (result == true) {

        $.get("../../Service/AjaxService.ashx?Method=Wx_GiftExchange",
        { "memid": memid, "memname": memname, "mobile": mobile, "address": address, "orderAccount": $("#spOrderAccount").html(), "addressID": addressID, "sumPoint": $(".gift-total").html(), "sumNumber": $(".gift-num").html(), "giftcount": GiftList.length, "GiftList": GiftList
        }, function (text) {

            switch (text) {
                case "0":
                    alert("兑换失败！");
                    break;
                case "-1":
                    alert("当前会员卡处于锁定或挂失状态，暂不允许进行兑换！");
                    break;
                case "-2":
                    alert("礼品库存不足，无法进行兑换，请返回重新选择礼品！");
                    location.href = "pointExchange.aspx";
                    break;
                default:
                    location.href = "pointExchangeSuccess.aspx";
                    break;
            }
        }, "text");
    }

});
//确认填写地址，将地址绑定到页面
$(".ad-sure").click(function () {
    var province = $("#sltProvince").find("option:selected").text();
    var city = $("#sltCity").find("option:selected").text();
    var county = $("#sltCounty").find("option:selected").text();
    var village = $("#sltVillage").find("option:selected").text();
    var detailAddress = $("#detailAddress").val();

    if (province == "请选择" || province == "") {
        alert("请选择省份！");
        return;
    }
    if (city == "请选择" || city == "") {
        alert("请选择市区！");
        return;
    }
    if (county == "请选择" || county == "") {
        alert("请选择区域！");
        return;
    }
    if (village == "请选择" || village == "") {
        alert("请选择街道！");
        return;
    }
    if (detailAddress == "") {
        alert("请填写详细地址！");
        return;
    }

    var ad = province + "省" + city + "市" + county + village + detailAddress;

  
    $("#address").text(ad);
    //将地址输入到页面
    $(this).parents(".city-mode").siblings(".show-address").text(ad).show();

    //隐藏“请选择”的按钮
    $(this).parents(".city-mode").siblings(".show-address").siblings("a").hide();
    $(this).parents(".city-mode").fadeOut();

});
//确认填写地址，将地址绑定到页面
$("#ad-cancel").click(function () {

    var address = $("#address").text();
    //将地址输入到页面
    $(this).parents(".city-mode").siblings(".show-address").text(address).show();

    //隐藏“请选择”的按钮
    $(this).parents(".city-mode").siblings(".show-address").siblings("a").hide();
    $(this).parents(".city-mode").fadeOut();

});
//添加收货地址
$(".addMemAdddress").click(function () {

    var memid = $("#txtMemID").val();
    if (memid == "") {
        alert("未获取到会员信息，无法进行兑换礼品！");
        return;
    }
    var mobile = $("#mobile").val();
    if (mobile == "") {
        alert("请填写手机号码！");
        return;
    }
    var memname = $("#memname").val();
    if (memname == "") {
        alert("请填写收货人！");
        return;
    }
    var address = $("#address").text();

    if (address == "" || address == "请选择") {
        alert("请填写地址！");
        return;
    }
    var url = "";

    var id = $("#txtAddressID").val();

    if (id == "") {
        url = "../../Service/AjaxService.ashx?Method=AddMemAddress"; ;
    }
    else {
        url = "../../Service/AjaxService.ashx?Method=EditMemAddress"; ;
    }
    var yes = $("#yes").attr("class");
    var no = $("#no").attr("class");
    var isDefault = 1;
    var sex = 1;
    if (yes == "line_btn active") {
        isDefault = 1; //男
    }
    else {
        isDefault = 0;
    }

    var detailAddress = $("#detailAddress").val();
    var province = $("#sltProvince").val();
    var city = $("#sltCity").val();
    var county = $("#sltCounty").val();
    var village = $("#sltVillage").val();


    $.get(url,
        { "id": id, "memid": memid, "mobile": mobile, "memname": memname, "province": province, "city": city, "county": county, "village": village, "address": detailAddress, "isDefault": isDefault
        }, function (text) {

            switch (text) {
                case "0":
                    if (id == "") {
                        alert("添加失败！");
                    }
                    else {
                        alert("修改失败！");
                    }
                    break;

                default:
                    if (id == "") {
                        alert("添加成功！");
                    }
                    else {
                        alert("修改成功！");
                    }
                    location.href = "memAddressManage.aspx";
                    break;
            }
        }, "text");


});
    $("#identityCard").keypress(function () {
        var identity = $("#identityCard").val();
        var result = isCardNo(identity);
        if (result) {
            var birth = identity.substring(6, 10) + "-" + identity.substring(10, 12) + "-" + identity.substring(12, 14);
            $("#birthday").html(birth);
        }
    });


    function check(obj) {       
        $('.setDefautAddress').each(function () {
            if (this != obj)
                $(this).attr("checked", false);
            else {
                if ($(this).prop("checked"))
                    $(this).attr("checked", true);
                else
                    $(this).attr("checked", false);
            }
        });


        var id = $(obj).val();

        var memid = $("#txtMemID").val();
        if (memid == "") {
            alert("未获取到会员信息，无法进行兑换礼品！");
            return;
        }
        $.get("../../Service/AjaxService.ashx?Method=SetMemDefaultAddress",
        { "id": id, "memid": memid
        }, function (text) {

            switch (text) {
                case "0":
                    alert("设置失败！");
                    break;

                default:
                    
                    break;
            }
        }, "text");
    }


    //更新会员信息
    $(".sureBtnEditMemInfo").click(function () {


        var memid = $("#txtMemID").val();
        if (memid == "") {
            alert("未获取到会员信息，无法进行兑换礼品！");
            return;
        }
        var boy = $("#boy").attr("class");
        var girl = $("#girl").attr("class");

        var sex = 1;
        if (boy == "line_btn active") {
            sex = 1; //男
        }
        else {
            sex = 0;
        }


        var mobile = $("#mobile").val();
        if (mobile == "") {
            alert("请填写手机号码！");
            return;
        }
        var memphoto = $("#imgShow").attr("src");

        var memname = $("#memname").val();
        if (memname == "") {
            alert("请填写收货人！");
            return;
        }
        var address = $("#address").text();

        if (address == "" || address == "请选择") {
            alert("请填写地址！");
            return;
        }
        var identityCard = $("#identityCard").val();
        if (identityCard == "") {
            alert("请填写身份证号码！");
            return;
        }
        var birthday = $("#birthday").html();
        var email = $("#email").val();
        var province = $("#sltProvince").val();
        var city = $("#sltCity").val();
        var county = $("#sltCounty").val();
        var village = $("#sltVillage").val();
        var detailAddress = $("#detailAddress").val();

        $.get("../../Service/AjaxService.ashx?Method=Wx_UpdateMemInfo",
        { "memid": memid, "mobile": mobile, "memname": memname, "province": province,
            "city": city, "county": county, "village": village, "address": detailAddress,
            "email": email, "birthday": birthday, "imgShow": memphoto, "sex": sex, "identityCard": identityCard
        }, function (text) {

            switch (text) {
                case "0":
                    alert("修改失败！");
                    break;

                default:
                    alert("修改成功！");
                    location.href = "myMember.aspx";
                    break;
            }
        }, "text");


    });


    //删除地址

    function DeleteAddressInfo(id) {
        $.get("../../Service/AjaxService.ashx?Method=DelMemAddress",
        {  "id": id
        }, function (text) {

            switch (text) {
                case "0":
                    alert("删除失败！");
                    break;

                default:
                    alert("删除成功！");
                    location.href = "memAddressManage.aspx";
                    break;
            }
        }, "text");
    }
        //更新会员头像
    $(".sureBtnEditMemPhoto").click(function () {
        var imageUrl = $("#txtMemPhoto").val();
        if (imageUrl != "") {
            location.href = "editMemberData.aspx?imageurl=" + imageUrl;
        }
        else {
            location.href = "editMemberData.aspx";
        }
    });
    $(".divEditMemPhoto").click(function () {
       
        location.href = "uploadImage.aspx";
    });
    $(".divMemAddressList").click(function () {
   
        location.href = "memAddressManage.aspx";
    });
    $(".divMemCouponList").click(function () {
      
        location.href = "coupon.aspx";
    });
    //触发浏览文件按钮
    $("#btnAddMemPhoto").click(function () {


        $("#UploadMemPhoto").click();


    });

    $("#UploadMemPhoto").change(function () {
        var value = $("#UploadMemPhoto").val();
      
        $("#btn_Upload").click();

    });
    function Province() {
    
        $("#sltCity").empty();
        $("#sltCounty").empty();
        $("#sltVillage").empty();
        $("#sltCity").append("<option value=''>请选择</option>");
        $("#sltCounty").append("<option value=''>请选择</option>");
        $("#sltVillage").append("<option value=''>请选择</option>");
        var provinceID = $("#sltProvince").val();
        GetNextName(provinceID, "sltCity");
    }
    function City() {

        $("#sltCounty").empty();
        $("#sltVillage").empty();
        $("#sltCounty").append("<option value=''>请选择</option>");

        var CityID = $("#sltCity").val();
        GetNextName(CityID, "sltCounty");
    }
    function County() {
    
        $("#sltVillage").empty();
        $("#sltVillage").append("<option value=''>请选择</option>");

        var CountyID = $("#sltCounty").val();
   
        GetNextName(CountyID, "sltVillage");
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

function GetMemCoupon(CouPonID) {


    var memid = $("#txtMemID").val();
   
    if (memid == "") {
        alert("未获取到会员信息，无法进行绑定！");      
    }

    $.get("../../Service/AjaxService.ashx?Method=Wx_GetMemCoupon",
        { "CouPonID": CouPonID, "memid": memid
        }, function (text) {

            switch (text) {
                case "0":
                    alert("领取失败！");
                    break;
                case "-1":
                    alert("该优惠券已领取完！");
                    break;
                default:
                    location.href = "Coupon.aspx";
                    break;
            }
        }, "text");
}