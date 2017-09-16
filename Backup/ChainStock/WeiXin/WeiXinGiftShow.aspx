<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="WeiXinGiftShow.aspx.cs" Inherits="ChainStock.WeiXin.WeiXinGiftShow" %>
<!DOCTYPE html>
<html>
<head>
    <title>礼品详细</title>
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <link href="../Inc/Style/jquery.mobile-1.2.0.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Module/WeiXin/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/WeiXin/jquery.mobile-1.2.0.min.js" type="text/javascript"></script>
        <style type="text/css">
			.ui-mobile img {max-width: 100%;}
			#divFirst{margin:5px 15px;padding:5px 15px;}
		 </style>
         <script type="text/javascript">
             $(function () {
                 $("#sureSubmit").bind("click", sureSubmit);
             })

             function sureSubmit() {
                 var txtConvertNumber = $("#txtConvertNumber").val();
                 var lbStockNumber = $("#lbStockNumber").text();
                 var result = /^\d+$/.test(txtConvertNumber);
                 if (result) {
                     var convertNumber = parseInt(txtConvertNumber);
                      if (convertNumber > parseInt(lbStockNumber)) {
                          $("#newsInfo").text("兑换数量不能大于剩余数量！");
                          $("#btnSure").attr("data-rel", "back");
                         $("#showDialog").click();
                     } else {
                         $("#newsInfo").text("确定要提交吗？");
                         $("#btnSure").removeAttr("data-rel");
                         $("#btnSure").attr("href", ("WeiXinGiftConvertList.aspx?MemWeiXinCard=" + $("#MemWeiXinCard").val() + "&GiftID=" + $("#GiftID").val() + "&Num=" + convertNumber));
                         $("#btnSure").attr("rel","external");
                         $("#showDialog").click();
                     }
                 } else {
                     $("#newsInfo").text("兑换数量只能是大于0的数字！");
                     $("#btnSure").attr("data-rel", "back");
                     $("#showDialog").click();
                 }

             }
         </script>
</head>
<body>
    <div data-role="page">
        <div id="divFirst">
            <a href="#dialog" data-rel="dialog" id="showDialog" style="display:none;"></a>
            <div>
                <span><strong id="giftName"><%=giftModel.GiftName%></strong>:</span>
                <span id="divDesc"><%=giftModel.GiftRemark%></span>
            </div>

            <div>
                <img id="imgGift" src="<%= this.GetPhoto() %>" />
            </div>

            <div>
                <span style="margin-right:10px;">所需积分：<label id="lbPoint"><%=giftModel.GiftExchangePoint%></label></span>
                <span>剩余数量：<label id="lbStockNumber"><%=giftModel.GiftStockNumber%></label></span>
            </div>

            <div data-role="fieldcontain">
			    <label for="txtConvertNumber">兑换数量:</label>
			    <input type="text" name="txtConvertNumber" value="1" maxlength="4" runat="server" id="txtConvertNumber"/>

		    </div>

            <div data-role="fieldcontain" style="text-align:center;">
                 <a data-role="button"  data-inline="true" data-icon="check" data-iconpos="left" data-theme="a" id="sureSubmit" href="#" >确定</a>
		　       <a data-inline="true" data-role="button" data-icon="back" data-rel="back" data-iconpos="left" data-theme="c" href="#" id="agoback">返回</a>
                <input type="hidden" value="<%=MemWeiXinCard %>" id="MemWeiXinCard"/>
                <input type="hidden" value="<%=GiftID %>" id="GiftID"/>
		    </div>
        </div>
    </div>

    <div data-role="page" id="dialog">
        <div>
		    <div data-role="header" data-theme="d">
			    <h1>消息提示</h1>
		    </div>
		    <div data-role="content" data-theme="c">
			    <p id="newsInfo" style="text-indent:2em"></p>
                <a href="" data-role="button" data-rel="back" data-theme="a" id="btnSure">确定</a>
                <a href="" data-role="button" data-rel="back" data-theme="c" id="btnCancel">取消</a>
		    </div>
	    </div>
    </div>
</body>
</html>
