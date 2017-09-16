<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="givemoney.aspx.cs" Inherits="ChainStock.mobile.member.givemoney" %>
  <!DOCTYPE html>
  <html>
    
    <head>
      <meta http-equiv="Content-Type" content="text/html;charset=UTF-8">
      <meta name="viewport" content="width=device-width,initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
      <title>红包大奖</title>
      <link rel="stylesheet" type="text/css" href="marketcss/public/reset.css">
      <link rel="stylesheet" type="text/css" href="marketcss/public/font-awesome.css">
      <link rel="stylesheet" type="text/css" href="marketcss/public/function.css">
      <link rel="stylesheet" type="text/css" href="marketcss/style.css"></head>
    
    <body class="g-body-in">
      <div class="g-wrap">
        <section class="h15"></section>
        <section class="main-sec pt5 main-wheel" style="text-decoration:none;text-shadow:none;">
			   <div id="imggivemoneyTop" style=" text-decoration:none; font-weight:lighter; font-family:微软雅黑;color:white; clear:both; background-size:cover; vertical-align:middle;   text-align:center; height: 100%">
             	
                <div style=" text-align:left; color:gray; font-size:25pt;  text-decoration:none;"><a href="index.aspx" style=" text-decoration:none; color:white;">×</a></div>

            
                        <div  style=" margin:10px auto  "> 
                        
                            <img src="images/business-Logo.png" style="border-radius:50%; border:2px solid white; width:40% " />        
                  
                          </div>
                         <div style=" height:20px;"></div>
                              <div><span id="spResultInfo" runat="server">您抢到了一个红包！</span></div>
                                <div style=" height:20px;"></div>
                              
                                  <div id="moneyInfo" runat="server" visible="false"><span style=" font-size:50pt; "  id="spGetMoney" runat="server">0.01</span>&nbsp;元</div>
                               <div style=" height:40px;"></div>
                     
                          
                     
                               <div style=" height:80px;"></div>
            </div>
                  <input id="txtMoneyID" type="hidden"  runat="server"/>
            <input id="txtMemID" type="hidden"  runat="server"/>
          
            <input id="MemCount" type="hidden"   runat="server"/>
                <input id="MaxCount" type="hidden"   runat="server"/>
                 <input id="MoneyRate" type="hidden"  runat="server" />
                <input id="MemtotalCount" type="hidden"   runat="server"/>
                  <input id="TotalMoney" type="hidden"  runat="server"/>
                 <input id="StartMoney" type="hidden"  runat="server"/>
                  <input id="EndMoney" type="hidden"  runat="server" />
                  <input id="MoneyType" type="hidden"  runat="server" />
                    <input id="FixedMoney" type="hidden"  runat="server"/>
                     <input id="IsWin" type="hidden"  runat="server"/>
                    <input id="txtLinkUrl" type="hidden"  runat="server"/>
                   <input id="txtgetmoney" type="hidden"  runat="server"/>
                   <input id="txtRemainMoney" type="hidden" runat="server" />
                   <input id="txtRemainCount" type="hidden" runat="server"/>
            </div>
            
    </body>
  
  </html>