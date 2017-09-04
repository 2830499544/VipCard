<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="getmoney.aspx.cs" Inherits="ChainStock.mobile.member.getmoney" %>
    <!DOCTYPE html>
    <html>
        
        <head>
            <meta http-equiv="Content-Type" content="text/html;charset=UTF-8">
            <meta name="viewport" content="width=device-width,initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
            <title>红包大奖</title>
            <link rel="stylesheet" type="text/css" href="marketcss/public/reset.css">
            <link rel="stylesheet" type="text/css" href="marketcss/public/font-awesome.css">
            <link rel="stylesheet" type="text/css" href="marketcss/public/function.css">
            <link rel="stylesheet" type="text/css" href="marketcss/style.css">
        <script src="scripts/jquery-2.1.4.min.js" type="text/javascript"></script>
<script type="text/javascript" src="js/model/jquery.SuperSlide.2.1.1.source.js"></script>
<script type="text/javascript" src="js/model/html5.js"></script>
<script type="text/javascript" src="js/public.js"></script>

         </head>
        
        <body class="g-body-in">
            <div class="g-wrap">
                <section class="h15"></section>
                <section class="main-sec pt5 main-wheel">
                    <div id="divGet " style="padding-top:10%;">
                        <div id="divGive" style="margin:auto auto;color:black; background:#FFB90F; padding:30px;  height:80px; width:80px;      -moz-border-radius: 85px;     -webkit-border-radius: 85px;     border-radius:85px;  border:5px solid #FFD700;   display:block;">
                            <span style=" font-family:华文行楷; font-size:50pt; line-height:80px; font-weight:lighter;  width:80px; color:black; height:80px; ">抢</span></div>
                    </div>
                </section>
                <section class="main-sec">
                    <div class="g-num">您还有
                        <em>
                            <span id="spNoUseCount" runat="server">0</span></em>次抢红包机会</div>
                </section>

                <section class="main-sec loptop">
                    <div class="m-title">
                        <h3>红包记录</h3></div>
                    <dl class="peolist">
                        <asp:Repeater ID="rptWinList" runat="server">
                            <ItemTemplate>
                                <dd>
                                    <img src="<%#BindPhoto(Eval("MemPhoto")) %>" width="20%" id="imgPhoto">
                                    <div class="right">
                                        <span>
                                            <h2>
                                                <%#Eval("MemName") %></h2>
                                            <em>
                                                <%# BindTime(Eval("GiveTime")) %></h2>
                                            </em>
                                        </span>
                                        <p>恭喜
                                            <%#BindMobile(Eval("MemMobile")) %>抢到了
                                                <%#Eval("GiveMoney") %>元</p></div>
                                </dd>
                            </ItemTemplate>
                        </asp:Repeater>
                    </dl>
                </section>
                        	<script type="text/javascript">
                        	    jQuery(".loptop").slide({ mainCell: "dl", autoPage: true, effect: 'topLoop', autoPlay: true, scroll: 1, vis: 1, easing: 'swing', delayTime: 500, interTime: 3000, pnLoop: true });

	</script>
                <section class="main-sec">
                    <div class="m-title">
                        <h3>活动说明</h3></div>
                    <div class="einfo">
                        <p>活动时间：
                            <span id="spStartTime" runat="server"></span>-
                            <span id="spEndTime" runat="server"></span></p>
                        <p>
                            <span id="spDesc" runat="server">每日签到可获1次抽奖机会</span></p>
                    </div>
                </section>
                <section class="main-sec">
                    <footer>
                        <a href="index.aspx" style=" color:White;">返回首页</a></footer>
                </section>
                <input id="txtMemID" type="hidden" runat="server" />
                <input id="txtImageUrl" type="hidden" runat="server" />
                <input id="txtGetMoneyID" type="hidden" runat="server" />
                <input id="MemCount" type="hidden" runat="server" />
                <input id="MaxCount" type="hidden" runat="server" />
                <input id="MoneyRate" type="hidden" runat="server" />
                <input id="MemtotalCount" type="hidden" runat="server" />
                <input id="TotalMoney" type="hidden" runat="server" />
                <input id="GiveMoney" type="hidden" runat="server" />
                <input id="StartMoney" type="hidden" runat="server" />
                <input id="EndMoney" type="hidden" runat="server" />
                <input id="MoneyType" type="hidden" runat="server" />
                <input id="FixedMoney" type="hidden" runat="server" />
                <input id="IsWin" type="hidden" runat="server" />
                <input id="IsSuccess" type="hidden" runat="server" />
                <input id="OpenID" type="hidden" runat="server" />
                <input id="IsOwn" type="hidden" runat="server" />
                <input id="txtMsg" type="hidden" runat="server" /></div>
        </body>


        <script type="text/javascript">
            $(document).ready(function () {



                var bgImage = $("#txtImageUrl").val();
                if (bgImage != "") {
                    $(".g-wrap").attr("style", "width: 100%;overflow: hidden;background:url('../" +
                            $("#txtImageUrl").val() + "');min-height:100%;padding-bottom: 20px;background-size:cover;");
                }
            });
            $("#divGive").click(function () {
                var openid = $("#OpenID").val();
                if (openid == "") {
                    alert("您没有绑定微信，不能参与抢红包！");
                    location.href = "index.aspx ";
                }
                var isown = $("#IsOwn").val();
                if (isown != "1") {
                    alert("您不能参与该活动！");
                    location.href = "index.aspx ";
                }

                        var count = $("#MemCount").val();
                        if (count <= 0) {
                            alert("您的抢红包次数已用完！");
                            location.href = "index.aspx ";
                            return;
                        }
                        else {

                            location.href = "getmoney.aspx?Win=1&MoneyID=" + $("#txtGetMoneyID ").val();
                        }


                    });
          
            var openid = $("#OpenID").val();         
            if (openid != "") {
                var isown = $("#IsOwn").val();
              
                if (isown == "1") {

                    var count = $("#MemCount").val();

                    if (count > 0) {

                        var isWin = $("#IsWin").val();
                        if (isWin != "") {

                            if (isWin == "0") {
                                alert("很遗憾，您未抢到红包！");

                            } else if (isWin == "1") {
                                //   alert("抢到红包！");
                            location.href = "givemoney.aspx?MoneyID=" + $("#txtGetMoneyID").val();
                             
                                   $("#btnGoGive").removeAttr("href ");
                                  $("#MemCount").val((parseInt($("#MemCount ").val()) - 1));
                                  $("#spNoUseCount").html((parseInt($("#spNoUseCount ").html()) - 1));
                               }
                               else if (isWin == " - 1 ") {
                                   alert($("#txtMsg ").val());

                               }

                           }

                           var IsSuccess = $("#IsSuccess ").val();
                           if (IsSuccess != "") {

                             location.href="hasgetmoney.aspx";
                            


                           }

                       }
                       else {
                           alert("您的抢红包次数已用完！");
                           location.href = "index.aspx ";
                       }
                   }
                   else {
                       alert("您不能参与该活动！");
                       location.href = "index.aspx ";
                   }
               }

               else {
                   alert("您没有绑定微信，不能参与抢红包！");
                   location.href = "index.aspx ";
               }

       
                </script>
    
    </html>