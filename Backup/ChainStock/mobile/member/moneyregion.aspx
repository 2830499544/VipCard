<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="moneyregion.aspx.cs" Inherits="ChainStock.mobile.member.moneyregion" %>
  <!DOCTYPE html>
  <html>
    
    <head>
      <meta http-equiv="Content-Type" content="text/html;charset=UTF-8">
      <meta name="viewport" content="width=device-width,initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
      <title>微会员-现金红包</title>
      <link rel="stylesheet" type="text/css" href="marketcss/public/reset.css">
      <link rel="stylesheet" type="text/css" href="marketcss/public/font-awesome.css">
      <link rel="stylesheet" type="text/css" href="marketcss/public/function.css">
      <link rel="stylesheet" type="text/css" href="marketcss/style.css"></head>
    
    <body class="g-body-in">
      <div class="g-wrap">
        <section class="h15"></section>
        <section class="main-sec pt5 main-wheel" style="text-decoration:none;text-shadow:none;">
          <div style=" text-decoration:none; font-weight:lighter; font-family:微软雅黑;color:white; clear:both; text-decoration:none;text-shadow:none; background-size:cover; vertical-align:middle;   text-align:center; height: 100%">
            <div style=" text-align:right; color:gray; font-size:25pt;  text-decoration:none;margin-bottom:20px;">
              <a href="index.aspx" style=" text-decoration:none;color:white; ">×</a>
            </div>

            <div style="width:100%;float:left;text-align:center;margin-bottom:20px;">
              <img src="images/business-Logo.png" style="border-radius:50%; border:2px solid  white; width:40%;" />
            </div>

            <div style="margin-bottom:10px;width:100%;float:left;text-align:center;">
              <span id="spText" runat="server">请输入红包指令:</span>
            </div>
            
            <div style=" font-size:25pt; text-align:center;width:100%;float:left;margin-bottom:40px;">
              <div style=" width:50%; margin:auto auto;overflow:hidden;">
                <input id="txtMoneyRegion" type="text" runat="server" style=" height:40px;width:100%;float:left;" />
              </div>
            </div>
            <section class="main-sec">
              <div class="g-num" style=" color:White;  ">
                <a href="#" id="btnOk" style=" color:White;">开始抢红包</a>
              </div>
            </section>
            <div style=" font-size:10pt; color:white;"></div>
          </div>
          <input id="txtMemID" type="hidden" runat="server" /></section>
        <script src="scripts/jquery-2.1.4.min.js" type="text/javascript"></script>
        <script type="text/javascript">        
            $("#btnOk").click(function () {
                if ($("#txtMoneyRegion").val() == "") {
                    alert("请输入红包指令！");
                } else {

                    $.get("../../Service/AjaxService.ashx?Method=GetMoneyRegion", {
                        "MoneyRegion": $.trim($("#txtMoneyRegion").val())
                    },
              function (text) {
                  if (text == "0") {
                      alert("红包指令不正确！");
                  } else if (text == "-1") {
                      alert("红包活动还未开始！");
                  } else if (text == "-2") {
                      alert("红包活动已结束！");
                  } else {

                      alert("指令输入成功，马上开始抢红包……");
                      location.href = "getmoney.aspx?MoneyID=" + text;

                  }

              },
              "text");
                }
            });</script>
            </div>
            
    </body>
  
  </html>