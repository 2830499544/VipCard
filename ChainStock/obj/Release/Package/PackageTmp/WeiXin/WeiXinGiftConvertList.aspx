<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXinGiftConvertList.aspx.cs" Inherits="ChainStock.WeiXin.WeiXinGiftConvertList" %>
<!DOCTYPE html>

<html>
<head>
    <title>礼品提交</title>
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <link href="../Inc/Style/jquery.mobile-1.2.0.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Module/WeiXin/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/WeiXin/jquery.mobile-1.2.0.min.js" type="text/javascript"></script>
    <style type="text/css">
		#divFirst{margin:5px 15px;padding:5px 15px;}
	</style>
    <script type="text/javascript">
        $(function () {
            $("#giftSubmit").bind("click", giftSubmit);
        })

        function giftSubmit() {
            var lbSumExchangePoint = parseInt($("#lbSumExchangePoint").text());
            var lbUsePoint = parseInt($("#lbUsePoint").text());
            var telNumber = $.trim($("#txtdivMemMobile").val());
            var txtreaAddress = $.trim($("#txtreaAddress").val());
            var errorText = "";
            if (lbUsePoint < lbSumExchangePoint) {
                errorText = "您当前的积分不足以兑现这些礼品！";
                $("#newsInfo").text(errorText);
                $("#showDialog").click();
                return;
            }

            if (telNumber == "") {
                errorText = "手机号码不能为空,请填写！";
                $("#newsInfo").text(errorText);
                $("#showDialog").click();
                return;
            } else {
                var result = /(^1[3|4|5|8]\d{9}$)|(^((0\d{2,3})-)?(\d{7,8})(-(\d{3,}))?$)/.test(telNumber);
                if (!result) {
                    errorText = "您填写的手机号码格式不正确,请重新填写！";
                    $("#txtdivMemMobile").val("");
                    $("#newsInfo").text(errorText);
                    $("#showDialog").click();
                    return;
                }
            }

            if (txtreaAddress == "") {
                errorText ="邮寄地址不能为空，否则无法送货！"
                $("#newsInfo").text(errorText);
                $("#showDialog").click();
                return;
            }

            $.post("../Service/WeiXinService.ashx", {
                "Method": "ConvertGift",
                "MemWeiXinCard": $("#MemWeiXinCard").val(),
                "GiftID": $("#GiftID").val(),
                "Num": $("#Num").text(),
                "memAddress": txtreaAddress,
                "telNumber": telNumber,
                "SumExchangePoint": $("#lbSumExchangePoint").text()
            }, function (e) {
                if (e == "0") {
                    errorText = "系统发生了异常,请稍后重试！"
                } else {
                    errorText = "礼品兑换申请成功！"
                    $("#btnSure").removeAttr("data-rel");
                    $("#btnSure").attr("rel","external");
                    $("#btnSure").attr("href", ("MemberManipulate.aspx?MemWeiXinCard=" + $("#MemWeiXinCard").val()));
                }
                $("#newsInfo").text(errorText);
                $("#showDialog").click();
            }, "text")
        }
    </script>
</head>
<body>
     <div data-role="page">
        <div id="divFirst">
            <a href="#dialog" data-rel="dialog" id="showDialog" style="display:none;"></a>
            <h2>礼品信息</h2> 
            <ul data-role="listview" data-inset="true">
                <li>礼品名称：<%=giftModel.GiftName%></li>
                <li>单个积分：<%=giftModel.GiftExchangePoint%></li>
                <li>兑换数量：<label id="Num"><%=Num%></label></li>
                <li>总计积分：<label id="lbSumExchangePoint"><%=giftModel.GiftExchangePoint * Num%></label></li>
                <li>可用积分：<label id="lbUsePoint"><%= mem.MemPoint %></label></li>
            </ul>
            <h2>收货信息</h2> 
            <div data-role="fieldcontain">
			    <label for="txtdivMemMobile">手机号码:</label>
			    <input type="text" name="txtdivMemMobile" id="txtdivMemMobile" value="<%=mem.MemMobile %>" />
		    </div>
            <div data-role="fieldcontain">
			    <label for="txtreaAddress">邮寄地址:</label>
			    <textarea cols="40" rows="8" name="txtreaAddress" id="txtreaAddress"></textarea>
		    </div>
            <div data-role="fieldcontain" style="text-align:center;">
			    <a data-inline="true" data-role="button" data-icon="check" data-iconpos="left" href="" data-theme="a" id="giftSubmit">提交</a>
		　      <a data-inline="true" data-role="button" data-icon="back"  data-rel="back" data-iconpos="left" data-theme="c" href="">返回</a>
                <input type="hidden" value="<%=MemWeiXinCard %>" id="MemWeiXinCard" />
                <input type="hidden" value="<%=GiftID %>" id="GiftID" />
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
                <a href="" data-role="button" data-rel="back" data-theme="c" id="btnSure">确定</a>
		    </div>
	    </div>
    </div>
</body>
</html>
