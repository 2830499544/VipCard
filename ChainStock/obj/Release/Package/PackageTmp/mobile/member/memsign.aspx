<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="memsign.aspx.cs" Inherits="ChainStock.mobile.member.memsign" %>
    <!DOCTYPE html>
    <html>
        
        <head>
            <meta http-equiv="Content-Type" content="text/html;charset=UTF-8">
            <meta name="viewport" content="width=device-width,initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
            <title>微会员-每日签到</title>
            <link rel="stylesheet" type="text/css" href="marketcss/public/reset.css">
            <link rel="stylesheet" type="text/css" href="marketcss/public/font-awesome.css">
            <link rel="stylesheet" type="text/css" href="marketcss/public/function.css">
            <link rel="stylesheet" type="text/css" href="marketcss/style.css">
        <script src="scripts/jquery-2.1.4.min.js" type="text/javascript"></script>
<script type="text/javascript" src="js/model/jquery.SuperSlide.2.1.1.source.js"></script>
<script type="text/javascript" src="js/model/html5.js"></script>
<script type="text/javascript" src="js/public.js"></script>

         </head>
        
        <body class="g-body-in" style="">
             <div style="width:100%; ">
                <img src="images/sign2.jpg"  style=" width:100%;" />
                </div>
            <div class="g-wrap" style="  background:white; " >

               
        
                <section class="h15"></section>
                <section class="main-sec pt5 main-wheel">
         
                    <div style=" width:100%; text-align:center;"> <img src="images/coin.png"  style=" width:100px;  height:100px;" /></div>



          
             <div style=" text-align:center; font-size:11pt; "> 每日签到，获得 <span id="spPoint" runat="server" style=" color:Red;">1</span> 积分</div>
                  
                </section>
           
           <section class="main-sec">
                <section class="main-sec loptop" >
                    <div class="g-num" id="btnSign" style=" background:#57bd59; border:1px solid #57bd59; color:White;" >
                       
                           <em> <span id="spSignInfo" runat="server" style="color:White;">今日签到</span></em>
                           
                           </div>
                </section>
        <%--            <div class="m-title">
                        <h3>签到记录</h3></div>
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
                                                <%# BindTime(Eval("SignTime")) %></h2>
                                            </em>
                                        </span>
                                        <p>
                                            <%#BindMobile(Eval("MemMobile")) %>签到获得
                                                <%#Eval("GivePoint") %>积分</p></div>
                                </dd>
                            </ItemTemplate>
                        </asp:Repeater>
                    </dl>--%>
                </section>
                        	
 <section class="main-sec">
                 
                    <div class="einfo" style=" background:white;">
                        &nbsp;
                    </div>
                </section>

        <section class="main-sec" >
                   
                </section>
                <section class="main-sec">
                    <footer>
                    <div>&nbsp;</div>
                        <a href="index.aspx" style="">返回首页</a></footer>
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
                <input id="txtMsg" type="hidden" runat="server" />
                     <input id="IsSign" type="hidden" runat="server" />                
                </div>
        </body>


        <script type="text/javascript">

            $("#btnSign").click(function () {
                var IsSign = $("#IsSign").val();
                if (IsSign != "1") {
                    var memid = $("#txtMemID").val();
                    if (memid == "") {
                        alert("未获取到登录会员信息，请重新登录！");
                        return;
                    }
                    var point = $("#spPoint").html(); 
                    $.get("../../Service/AjaxService.ashx?Method=Wx_MemSign",
                    { "memid": memid, "point": point }
                    , function (text) {
                        if (text == "1") {
                            alert("签到成功,恭喜您获取"+point+"积分！");
                            $("#spSignInfo").html("今日已签到");
                            $("#IsSign").val("1");
                        }
                         else if(text=="-1") {
                            alert("今日已签到，请明日再来！");
                        }
                        else {
                            alert("签到失败！");
                        }
                    }, "text")
                }
            });
        
                </script>
    
    </html>