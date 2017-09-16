<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RechargeJsApiPayPage.aspx.cs" Inherits="ChainStock.mobile.member.RechargeJsApiPayPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title></title>

</head>

 

     
           <script type="text/javascript">


               //调用微信JS api 支付
               function jsApiCall()
               {
                   WeixinJSBridge.invoke(
                   'getBrandWCPayRequest',
                   <%=wxJsApiParam%>,//josn串
                    function (res)
                    {
                
                        //WeixinJSBridge.log(res.err_msg);
                         if(res.err_msg == "get_brand_wcpay_request:ok" ) {   
                               location.href="rechargereturn_url.aspx";     
                        // location.href="return_url.aspx?out_trade_no="+document.getElementById("sp_out_trade_no").innerHTML;
                         }   
                         else if(res.err_msg == "get_brand_wcpay_request:fail" ) {                         
                         alert("支付失败!");
                         location.href="index.aspx";
                         }   
                       else if(res.err_msg == "get_brand_wcpay_request:cancel" ) {                         
                         alert("支付过程中用户取消!");
                         location.href="index.aspx";
                         }   
                     }
                    );
               }

               function callpay()
               {
                   if (typeof WeixinJSBridge == "undefined")
                   {
                       if (document.addEventListener)
                       {
                           document.addEventListener('WeixinJSBridgeReady', jsApiCall, false);
                       }
                       else if (document.attachEvent)
                       {
                           document.attachEvent('WeixinJSBridgeReady', jsApiCall);
                           document.attachEvent('onWeixinJSBridgeReady', jsApiCall);
                       }
                   }
                   else
                   {
                       jsApiCall();
                   }
               }
               
     </script>
<body onload="callpay()">
    <form id="Form1" runat="server">

   <span id="sp_out_trade_no" runat="server" style="display:none"></span>

    </form>
  
</body>

</html>