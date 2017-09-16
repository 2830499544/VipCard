<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rotateregion.aspx.cs" Inherits="ChainStock.mobile.member.rotateregion" %>
  <!DOCTYPE html>
  <html>
    
    <head>
      <meta http-equiv="Content-Type" content="text/html;charset=UTF-8">
      <meta name="viewport" content="width=device-width,initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
      <title>微会员-幸运转盘</title>
      <link rel="stylesheet" type="text/css" href="marketcss/public/reset.css">
      <link rel="stylesheet" type="text/css" href="marketcss/public/font-awesome.css">
      <link rel="stylesheet" type="text/css" href="marketcss/public/function.css">
      <link rel="stylesheet" type="text/css" href="marketcss/style.css"></head>
    
    <body class="g-body-in">
      <div class="g-wrap">
        <section class="h15"></section>
        <section class="main-sec pt5 main-wheel" style="text-decoration:none;text-shadow:none;">
          <div style=" text-decoration:none; font-weight:lighter; font-family:微软雅黑;color:white; clear:both; text-decoration:none;text-shadow:none; background-size:cover; vertical-align:middle;   text-align:center; height: 100%">
            <div style=" text-align:right; color:gray; font-size:25pt;  text-decoration:none;width:100%;float:left;margin-bottom:20px;">
              <a href="index.aspx" style=" text-decoration:none;color:white; ">×</a>
            </div>

            
            <div style="width:100%;float:left;margin-bottom:20px;text-align:center;">
              <img src="images/business-Logo.png" style="border-radius:50%; border:2px solid  white; width:40%;" />
            </div>
            
            <div style="width:100%;float:left;margin-bottom:10px;text-align:center;">
              <span id="spText" runat="server">请输入大转盘指令:</span>
            </div>

            <div style=" font-size:25pt; text-align:center;width:100%;float:left;margin-bottom:40px;">
              <div style=" width:50%; margin:auto auto;overflow:hidden;">
                <input id="txtRotateRegion" type="text" runat="server" style=" height:40px;float:left;width:100%;" />
              </div>
            </div>
            
            <section class="main-sec">
              <div class="g-num" style=" color:White;  ">
                <a href="#" id="btnOk" style=" color:White;">开始转盘游戏</a></div>
            </section>
            <div style=" font-size:10pt; color:white;"></div>
        
            <div style=" height:80px;"></div>
          </div>
          <input id="txtMemID" type="hidden" runat="server" /></section>
        <script src="scripts/jquery-2.1.4.min.js" type="text/javascript"></script>
        <script type="text/javascript">        
            $("#btnOk").click(function () {
                if ($("#txtMoneyRegion").val() == "") {
                    alert("请输入转盘活动指令！");
                } else {

                    $.get("../../Service/AjaxService.ashx?Method=GetRotateRegion", {
                        "RotateRegion": $.trim($("#txtRotateRegion").val())
                    },
              function (text) {
                  if (text == "0") {
                      alert("转盘指令不正确！");
                  } else if (text == "-1") {
                      alert("转盘活动还未开始！");
                  } else if (text == "-2") {
                      alert("转盘活动已结束！");
                  } else {

                      alert("指令输入成功，马上开始转盘游戏……");
                      location.href = "rotate.aspx?RotateID=" + text;

                  }

              },
              "text");
                }
            });</script>
            </div>
            
    </body>
  
  </html>