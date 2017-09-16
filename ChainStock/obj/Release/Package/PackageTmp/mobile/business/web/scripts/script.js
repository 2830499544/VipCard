console.log("%c技术支持: 成都可百科技有限公司","color:#ddd");

$(window).ready(function(){

	//设置全局变量
	var windowH = $(window).height();
    var windowW = $(window).width();
	var documentH = $(document).height();
	var documentW = $(document).width();

	/*返回上一页*/ 
	$(".back-btn").click(function(event){
	    event.preventDefault();
	    window.history.back();
	});

	// 注销登录
	$(".logout").click(function(){
		var logout = confirm("确认退出当前账号？");
		if (logout ==true) {
			window.location.href = "login.html";
		}
	});

	//新增会员性别选择
    $(".line_group").find("a.line_btn").each(function(){
        $(this).click(function(){
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
    (function(){
    	var num =  $(".foot-nav .fix-ch").length;
    	var width = $(".foot-nav").outerWidth();
    	var homeWidth = $(".fix-home").outerWidth();
    	$(".fix-ch").css("width",(width-homeWidth)/num + "px");
    }());

    //积分兑换页面礼品导航交互
    $(".gift-nav li").eq(0).find("a").addClass("active");
    $(".gift-nav li").click(function(){
        $(".gift-nav li").find("a").removeClass("active");
        $(this).find("a").addClass("active");
    });

    //积分兑换礼品增减控制器
    
    (function(){
        var isBuy = false;

        $(".plusShow").click(function(){
            var value = 1;
            $(this).siblings(".giftTxt").val(value);
            $(this).hide();
            $(this).siblings(".reduceBtn,.plusBtn,.giftTxt").show();
        });

        $(".plusBtn").click(function(){
            //将要消费的数量
            var value = parseInt($(this).siblings(".giftTxt").val());
            // 记次消费单个服务拥有总次数
            var count = parseInt($(this).parents("li").find(".count-num").text());
            if (count) {
                if (value<count) {
                    value++;
                }
            }else{
               value++; 
            }
            
            $(this).siblings(".giftTxt").val(value);
        });

        $(".reduceBtn").click(function(){
            var value = $(this).siblings(".giftTxt").val();
            if (value>0) {
                value = $(this).siblings(".giftTxt").val();
                value--;
                $(this).siblings(".giftTxt").val(value);
                if (value == 0) {
                    $(this).siblings("a,input").hide();
                    $(this).hide();
                    $(this).siblings(".plusShow").show();
                }
            }
        });

        $(".buy-ctrl a").click(function(){
            countGift();
        });

        countGift();

        function countGift(){
            var num = 0;
            var total = 0;
            $(".gift-list li").each(function(index,element){
                var danNum = $(this).find(".buy-ctrl input").val();
                var danIntegral = $(this).find(".gift-integral").text();
                num = num + parseInt(danNum);
                total = total + parseInt(danIntegral*danNum);
            });
            // console.log(num)
            $(".gift-num").text(num);
            $(".gift-total").text(total);
            $(".count-total").text(num);
        }
    }());

    //点击填写弹出城市选择模态框
    $(".queryCity,.show-address").click(function(){
        $(this).siblings(".city-mode").fadeIn();
        $("body,html").css("overflow","hidden");
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
        $("body,html").css("overflow","inherit");
    });

    //确认填写地址，将地址绑定到页面
    $(".ad-sure").click(function(){
        var ad = "";
        var detail = "";
        $(this).parents(".city-mode").find(".city-query select").each(function(){
            ad = ad + $(this).children("option:selected").text();
        });
        //将地址输入到页面
        $(this).parents(".city-mode").siblings(".show-address").text(ad).show();

        //隐藏“请选择”的按钮
        $(this).parents(".city-mode").siblings(".show-address").siblings("a").hide();
        $(this).parents(".city-mode").fadeOut();
        $("body,html").css("overflow","inherit");
    });

    //自适应改进
    if (windowW < 640) {
        var fontSize = windowW / 6.4 + "px";
        $("body,html").css("font-size", fontSize);
    }

    //快速消费确定
    $("#sureConsumption").click(function(){
        var value = $("input[name='conMoney']").val();
        if (value>0&&value!="") {
           alert("消费成功！");
            window.location.reload();
        }else{
            alert("请输入正确的金额！")
        }
    });

    //会员选择
    (function(){
        var checked,value;
        $(".card-sure").click(function(){
            checked = $(".query-member input[type='radio']:checked");
            if (checked.length > 0) {
                value = checked.siblings("span").text();
                $(this).parents(".city-mode").siblings(".show-address").text(value).show();
                //隐藏“请选择”的按钮
                $(this).parents(".city-mode").siblings(".show-address").siblings("a").hide();
                $(this).parents(".city-mode").fadeOut();
            }else{
                alert("请选择一个会员！")
            }
            
        });
    }());

    //商品消费
    (function(){
        var isMember = true;
        $("#nowBuy").click(function(){
            isMember = $("#fitCon").hasClass("active") ? false : true;
            if (isMember) {
                //会员消费
                //点击模态框内立即结算
                $(".settlement").click(function(){
                    alert("消费成功！");
                    window.location.reload();
                });
            }else{
                // 散客消费
                alert("消费成功！")
                window.location.reload();
            }
        });
    }());
    

    //判断散客消费
    $(".line_btn").click(function(){
        var isMember = $("#fitCon").hasClass("active") ? false : true;
        if (!isMember) {
            $(this).parents(".line_group").siblings("#queryMember").hide();
            $(this).parents(".line_group").siblings("#memberName").hide();
            $(this).parents(".line_group").siblings("#consumptionWay").find("a").removeClass("active");
            $(this).parents(".line_group").siblings("#consumptionWay").find("a.xianjin").addClass("active");
            $(this).parents(".line_group").siblings("#queryMember").find("input[type='text']").val("");
        }else{
            $(this).parents(".line_group").siblings("#queryMember").show();
        }
    });

    //计次消费-立即消费
    $("a.count-settlement").click(function(){
        alert("消费成功！");
        window.location.reload();
    });

    //输入会员卡号后显示姓名
    $("input[name='memberCard']").bind("blur",function(){
        if ($(this).val().length>0) {
            $(this).parents(".line_group").siblings("#memberName").show();
        }else{
            $(this).parents(".line_group").siblings("#memberName").hide();
        }
    });
    
});


