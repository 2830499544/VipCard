var indexApp = {
    //入口方法
    init: function (valueJson) {
        this.valueJson = valueJson; //获取前台页面传入的参数
        this.wheelInit(); //一些样式的初始化 如圆形的高度设置等
        this.resize(); //onresize 事件 重置样式
        this.cancel($('.false')); //注册取消按钮的点击事件
        this.cancel($('.close')); //注册再来一次按钮的点击事件
        return this; //返回对象本身,使其可以链式调用
    },
    //转盘初始化
    wheelInit: function () {
        var t = this;
        t.valueJson['wheelBody'].css('height', t.valueJson['wheelBody'].css('width'));
        t.valueJson['wheelSmall'].css('height', t.valueJson['wheelSmall'].css('width'));
        t.showStars(); //某几个小圆点高亮
    },
    //窗口改变时的重新设置样式
    resize: function () {
        var t = this;
        $(window).resize(function () {
            t.wheelInit(); //窗口发生变化的时候重置样式
        });
    },
    //计算并且排列小圆点
    showStars: function () {
        var t = this;
        for (var i = 0; i < t.valueJson['starsNum']; i++) {
            var oStar = document.createElement('div');

            if (i % 2 == 0) { //奇数的圆点增加高亮的效果(外阴影)
                oStar.style.boxShadow = '0 0 5px #fff';
            }
            oStar.className = 'stars';
            oStar.style.left = t.valueJson['starsPostion'][i][0] + '%';
            oStar.style.top = t.valueJson['starsPostion'][i][1] + '%';
            t.valueJson['wheelBody'].append(oStar);

        }
    },
    //取消按钮事件绑定
    cancel: function (obj) {
        obj.click(function () {
            $(this).parents('.dialog').css('display', 'none');
        });
    },
    //转盘开始的初始化函数 以及点击事件 通过链式调用加载 而非init()初始化加载,这样做,当未开始或者已结束页面不需要转动的时候,不链式调用此方法就行
    wheelStart: function () {
        var t = this;
        t.nowRan = 0; //当前弧度
        t.once = true; //是否第一次
        t.onStart = true; //是否开始了转动


        //点击事件
        t.valueJson['startBtn'].click(function () {

            if (t.onStart == true) { //只有 为 true 的 时候 才允许转动
                t.onStart = false;

                var OnePrizeCount = 0;
                var TwoPrizeCount = 0;
                var ThreePrizeCount = 0;
                var FourPrizeCount = 0;
                var FivePrizeCount = 0;
                var SixPrizeCount = 0;

                var OnePrizeWinCount = 0;
                var TwoPrizeWinCount = 0;
                var ThreePrizeWinCount = 0;
                var FourPrizeWinCount = 0;
                var FivePrizeWinCount = 0;
                var SixPrizeWinCount = 0;
                var totalPrizeCount = 0;
                var totalPrizeWinCount = 0;

                var OneRate = 0;
                var TwoRate = 0;
                var ThreeRate = 0;
                var FourRate = 0;
                var FiveRate = 0;
                var SixRate = 0;
                //{{




                $.get("../../Service/AjaxService.ashx?Method=GetSysRotateInfo",
                        {
                            "RotateID": $("#txtRotateID").val()
                        }
                        , function (json) {

                            $("#spIsWinOne").html(json[0].IsWinOne);

                            $("#spOnePrizeCount").html(json[0].OnePrizeCount);
                            $("#spTwoPrizeCount").html(json[0].TwoPrizeCount);
                            $("#spThreePrizeCount").html(json[0].ThreePrizeCount);
                            $("#spFourPrizeCount").html(json[0].FourPrizeCount);
                            $("#spFivePrizeCount").html(json[0].FivePrizeCount);
                            $("#spSixPrizeCount").html(json[0].SixPrizeCount);


                            $("#spOnePrizeWinCount").html(json[0].OnePrizeWinCount);
                            $("#spTwoPrizeWinCount").html(json[0].TwoPrizeWinCount);
                            $("#spThreePrizeWinCount").html(json[0].ThreePrizeWinCount);
                            $("#spFourPrizeWinCount").html(json[0].FourPrizeWinCount);
                            $("#spFivePrizeWinCount").html(json[0].FivePrizeWinCount);
                            $("#spSixPrizeWinCount").html(json[0].SixPrizeWinCount);

                            if ($("#spIsOne").html() != "1") {

                                $("#spOneRate").html(json[0].OneRate);
                            }
                            else {
                                $("#spOneRate").html(100);
                            }
                            if ($("#spIsTwo").html() != "1") {
                                $("#spTwoRate").html(json[0].TwoRate);
                            }
                            else {
                                $("#spTwoRate").html(100);
                            }
                            if ($("#spIsThree").html() != "1") {
                                $("#spThreeRate").html(json[0].ThreeRate);
                            }
                            else {
                                $("#spThreeRate").html(100);
                            }
                            if ($("#spIsFour").html() != "1") {
                                $("#spFourRate").html(json[0].FourRate);
                            }
                            else {
                                $("#spFourRate").html(100);
                            }
                            if ($("#spIsFive").html() != "1") {
                                $("#spFiveRate").html(json[0].FiveRate);
                            }
                            else {
                                $("#spFiveRate").html(100);
                            }
                            if ($("#spIsSix").html() != "1") {
                                $("#spSixRate").html(json[0].SixRate);
                            }
                            else {
                                $("#spSixRate").html(100);
                            }


                            OnePrizeCount = parseInt($("#spOnePrizeCount").html());
                            TwoPrizeCount = parseInt($("#spTwoPrizeCount").html());
                            ThreePrizeCount = parseInt($("#spThreePrizeCount").html());
                            FourPrizeCount = parseInt($("#spFourPrizeCount").html());
                            FivePrizeCount = parseInt($("#spFivePrizeCount").html());
                            SixPrizeCount = parseInt($("#spSixPrizeCount").html());

                            OnePrizeWinCount = parseInt($("#spOnePrizeWinCount").html());
                            TwoPrizeWinCount = parseInt($("#spTwoPrizeWinCount").html());
                            ThreePrizeWinCount = parseInt($("#spThreePrizeWinCount").html());
                            FourPrizeWinCount = parseInt($("#spFourPrizeWinCount").html());
                            FivePrizeWinCount = parseInt($("#spFivePrizeWinCount").html());
                            SixPrizeWinCount = parseInt($("#spSixPrizeWinCount").html());


                            OneRate = parseFloat($("#spOneRate").html());
                            TwoRate = parseFloat($("#spTwoRate").html());
                            ThreeRate = parseFloat($("#spThreeRate").html());
                            FourRate = parseFloat($("#spFourRate").html());
                            FiveRate = parseFloat($("#spFiveRate").html());
                            SixRate = parseFloat($("#spSixRate").html());




                            //-------判断奖品是否已经发放完毕---------------------------------------------------------
                            totalPrizeCount = OnePrizeCount + TwoPrizeCount + ThreePrizeCount + FourPrizeCount + FivePrizeCount + SixPrizeCount;
                            totalPrizeWinCount = OnePrizeWinCount + TwoPrizeWinCount + ThreePrizeWinCount + FourPrizeWinCount + FivePrizeWinCount + SixPrizeWinCount;

                            //                if (totalPrizeWinCount >= totalPrizeCount) {
                            //                    $("#spMsgInfo").html("本次活动奖品已经发放完，欢迎下次再来！");
                            //                    t.dialog($('.again'), data);  //执行带按钮的提示框
                            //                    return;
                            //                }


                            //-------判断是否还有抽奖次数---------------------------------------------------------
                            var NoUseCount = $("#spNoUseCount").html();

                            if (NoUseCount <= 0) {

                                $("#spMsgInfo").html("你的抽奖次数已用完，详询工作人员！");
                                t.dialog($('.again'), data);  //执行带按钮的提示框
                                return;

                            }
                            var result = -1;
                            result = getRandom(10000);

                            var hasWinCount = $("#spWinCount").html();
                            var spIsWinOne = $("#spIsWinOne").html();
                            var prizeLevel = "未中奖";

                            if (totalPrizeWinCount >= totalPrizeCount) {
                                result = -1;
                                prizeLevel = "未中奖";

                            }
                            else if (spIsWinOne == "1" && parseInt(hasWinCount) > 0) {
                                result = -1;
                                prizeLevel = "未中奖";
                            }
                            else {
                                if (result <= OneRate * 100 && OnePrizeCount > OnePrizeWinCount) {


                                    $("#spGetPrizeLevel").html("一等奖");
                                    $("#spGetPrizeName").html($("#spOnePrizeName").html());
                                    prizeLevel = "一等奖";
                                } else {
                                    result = getRandom(10000);

                                    if (result <= TwoRate * 100 && TwoPrizeCount > TwoPrizeWinCount) {

                                        $("#spGetPrizeLevel").html("二等奖");
                                        $("#spGetPrizeName").html($("#spTwoPrizeName").html());
                                        prizeLevel = "二等奖";
                                    }
                                    else {
                                        result = getRandom(10000);

                                        if (result <= ThreeRate * 100 && ThreePrizeCount > ThreePrizeWinCount) {
                                            $("#spGetPrizeLevel").html("三等奖");
                                            $("#spGetPrizeName").html($("#spThreePrizeName").html());
                                            prizeLevel = "三等奖";
                                        }
                                        else {
                                            result = getRandom(10000);

                                            if (result <= FourRate * 100 && FourPrizeCount > FourPrizeWinCount) {

                                                $("#spGetPrizeLevel").html("四等奖");
                                                $("#spGetPrizeName").html($("#spFourPrizeName").html());
                                                prizeLevel = "四等奖";
                                            }
                                            else {
                                                result = getRandom(10000);

                                                if (result <= FiveRate * 100 && FivePrizeCount > FivePrizeWinCount) {

                                                    $("#spGetPrizeLevel").html("五等奖");
                                                    $("#spGetPrizeName").html($("#spFivePrizeName").html());
                                                    prizeLevel = "五等奖";
                                                }
                                                else {
                                                    result = getRandom(10000);

                                                    if (result <= SixRate * 100 && SixPrizeCount > SixPrizeWinCount) {

                                                        $("#spGetPrizeLevel").html("六等奖");
                                                        $("#spGetPrizeName").html($("#spSixPrizeName").html());
                                                        prizeLevel = "六等奖";
                                                    }
                                                    else {

                                                        result = -1;
                                                        prizeLevel = "未中奖";

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            var level = 0;
                            var ran = 0;
                            var status = 0;
                            var actionStatus = 0;


                            var nowran = t.nowRan % 360;
                            var nowlevel = 0;
                            if (nowran >= 0 && nowran <= 50 && nowran >= 360) {
                                nowlevel = 7;
                            }
                            if (nowran > 50 && nowran <= 102) {
                                nowlevel = 6;
                            }
                            if (nowran > 102 && nowran <= 155) {
                                nowlevel = 5;
                            }
                            if (nowran > 155 && nowran <= 207) {
                                nowlevel = 4;
                            } if (nowran > 207 && nowran <= 258) {
                                nowlevel = 3;
                            }
                            if (nowran > 258 && nowran <= 310) {
                                nowlevel = 2;
                            }
                            if (nowran > 310 && nowran <= 359) {
                                nowlevel = 1;
                            }




                            switch (prizeLevel) {
                                case "一等奖":
                                    ran = 335;
                                    status = 1;
                                    actionStatus = 1;
                                    level = 1;
                                    break;
                                case "二等奖":
                                    status = 1;
                                    actionStatus = 1;
                                    ran = 283;
                                    level = 2;
                                    break;
                                case "三等奖":
                                    actionStatus = 1;
                                    status = 1;
                                    ran = 233;
                                    level = 3;
                                    break;
                                case "四等奖":
                                    actionStatus = 1;
                                    status = 1;
                                    ran = 181;
                                    level = 4
                                    break;
                                case "五等奖":
                                    actionStatus = 1;
                                    status = 1;
                                    ran = 128;
                                    level = 5;
                                    break;
                                case "六等奖":
                                    actionStatus = 1;
                                    status = 1;
                                    ran = 75;
                                    level = 6;
                                    break;
                                case "未中奖":
                                    actionStatus = 2;
                                    status = 1;
                                    ran = 25;
                                    level = 7;
                                    break;

                            }

                            if (nowlevel != 0) {

                                ran = (nowlevel - level) * 51;


                            }


                            //如果开启了关注 并且 当前 用户 没有关注
                            if (t.valueJson['is_gz'] == 1 && t.valueJson['is_follow'] == 2) {
                                t.dialog($('.gz')); //弹出关注提示框
                            } else {

                                //ajax 事件 获取
                                //得到的参数详细见交互文档

                                /*$.ajax({
                                'type' : 'POST',
                                'url' : t.valueJson['clickAjaxUrl'],
                                success : function (data) {*/

                                var data = { 'status': status, 'actionStatus': actionStatus, 'ran': ran, 'onceran': 51, 'num': 1, 'level': level }
                                if (data['status'] == 1) { //表示成功 
                                    t.showWheel(data); //执行转动效果


                                    $.get("../../Service/AjaxService.ashx?Method=SysRotatePrizeLogAdd",
                                {
                                    "MemID": $("#txtMemID").val(), "RotateID": $("#txtRotateID").val(), "PrizeLevel": prizeLevel
                                }
                                , function (json) {
                                    switch (json) {

                                        case -1:
                                            alert("系统错误，请联系管理员！");
                                            break;
                                        default:
                                         
                                          
                                            $("#spCode").html(json.prizecode);

                                            $("#spGetPrizeLevel").html(json.prizename);

                                            $.get("../../Service/AjaxService.ashx?Method=GetWinCount",
                                            {
                                                "MemID": $("#txtMemID").val(), "RotateID": $("#txtRotateID").val()
                                            }
                                            , function (json) {

                                                $("#spNoUseCount").html(json.noUseCount);
                                                $("#spWinCount").html(json.hasWinCount);
                                            }, "json")


                                            break;
                                    }

                                }, "json")


                                } else if (data['status'] == 2) { //金额不足 或者次数不足
                                    t.dialog($('.info'), data); //没有按钮的提示信息
                                } else {         //出现了异常错误
                                    t.dialog($('.again'), data);  //执行带按钮的提示框

                                }

                                /*}
                                });*/
                            }



                        }, "json")









                //}}


            }
        });
    },

    //转盘转动具体算法
    showWheel: function (data) {

        var t = this;
        //需要转动的值 等与当前值 + 默认转动7200度 + 后台计算传过来的度数

        var ra = t.nowRan + data['ran'] + 3600;



        //注意指针 和 转盘 反方向转动 来达到 指针 不动的效果

        t.valueJson['wheelBody'].css('webkitTransform', 'rotate(' + ra + 'deg)');
        t.valueJson['startBtn'].css('webkitTransform', 'rotate(' + (-ra) + 'deg)');




        //重新获取当前的度数
        t.nowRan = ra;

        if (data['level'] == 7) {
            t.nowRan += 51;
        }
        //转盘转动需要4S  这里 4.5S 后 执行 各种弹出提示信息框的事件
        setTimeout(function () {
            t.showDialog(data);
            t.onStart = true;
        }, 4500);




    },

    //根据各种不同的参数 显示弹出层的提示框
    showDialog: function (data) {
        var t = this;

        if (data['actionStatus'] == 1) {  //值为1 表示 抽取到了现金红包
            //  t.deduct(data); //扣除次数;
            t.dialog($('.theForm'), data); //获得奖品的 提示信息框



        } else if (data['actionStatus'] == 2) {   //值为2 表示 再来一次  再来一次不扣除次数
            // t.deduct(data); //扣除次数;

            $("#spMsgInfo").html("很遗憾,未抽中奖品,谢谢您的参与！");
            t.dialog($('.again'), data);  //执行带按钮的提示框
        }


    },

    //扣除次数的相关操作  次数的 参数 也是ajax 后台传递过来
    deduct: function (data) {
        $('.g-num').find('em').html(data['num']);
    },

    //弹出层
    dialog: function (obj, data, bl) {
        if (data && !bl) { //关注 再来一次  谢谢参与  系统异常 都是执行此处
            obj.find('d-main').children('p').html(data['mess']);
        }

        //打开弹出层
        obj.css('display', 'block');

    }

}