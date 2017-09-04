<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="WeiXinUpdateInfo.aspx.cs" Inherits="ChainStock.WeiXin.WeiXinUpdateInfo" %>
<!DOCTYPE html>
<html>
<head>
    <title>信息修改</title>
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <link href="../Inc/Style/jquery.mobile-1.2.0.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Module/WeiXin/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/WeiXin/jquery.mobile-1.2.0.min.js" type="text/javascript"></script>
    <link href="../Inc/Style/mobiscroll.custom-2.5.0.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Module/WeiXin/mobiscroll.custom-2.5.0.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var opt = {
                preset: 'date', //日期
                theme: 'sense-ui', //皮肤样式
                display: 'modal', //显示方式 
                mode: 'scroller', //日期选择模式
                dateFormat: 'yy-mm-dd', // 日期格式
                setText: '确定', //确认按钮名称
                cancelText: '取消', //取消按钮名籍我
                dateOrder: 'yymmdd', //面板中日期排列格式
                dayText: '日', monthText: '月', yearText: '年', //面板中年月日文字
                endYear: 2020 //结束年份
            };
            $("#txtMemBirthday").mobiscroll(opt).date(opt);

            $("#btnUpdateMemInfo").bind("click", btnUpdateMemInfo);
        });

        function btnUpdateMemInfo() {
            $.post("../Service/WeiXinService.ashx", {
                "Method": "UpdateMemInfo",
                "MemName": $("#txtMemName").val(),
                "MemSex": $("#sltGenderMan").val(),
                "MemBirthday": $("#txtMemBirthday").val(),
                "MemWeiXinCard": $("#txtMemWeiXinCard").val(),
                "MemCard": $("#txtMemCard").val()

            }, function (e) {
                if (e == "0") {
                    $("#newsInfo").text("系统发生了异常,请稍后重试！");
                } else if (e == "-1") {
                    $("#newsInfo").text("您填写的会员生日格式有误！");
                } else {
                    $("#newsInfo").text("您的会员信息修改成功！");
                }
                $("#showDialog").click();
            }, "text")
        }
    </script>
</head>
<body>
    <div data-role="page">
        <a href="#dialog" data-rel="dialog" id="showDialog" style="display:none;"></a>
        <div data-role="fieldcontain"  style="margin:5px 15px;padding:5px 15px;">
		<label for="txtMemCard">会员卡号:</label>
		<input type="text" value="<%= memModel.MemCard %>" readonly="readonly" id="txtMemCard" name="txtMemCard" />
	</div>

        <div data-role="fieldcontain" style="margin:5px 15px;padding:5px 15px;">
		<label for="txtMemName">会员姓名:</label>
		<input type="text" id="txtMemName" name="txtMemName" value="<%= memModel.MemName %>"  />
	</div>

        <div data-role="fieldcontain" style="margin:5px 15px;padding:5px 15px;">
		<label for="sltGenderMan">会员性别:</label>
		<select id="sltGenderMan" name="sltGenderMan" data-role="slider">
			<option value="1" <%=memModel.MemSex ?  "selected=\"selected\"":""  %>>男</option>
			<option value="0" <%=memModel.MemSex ? "":"selected=\"selected\""  %> >女</option>
		</select> 
	</div>

        <div data-role="fieldcontain" style="margin:5px 15px;padding:5px 15px;">
		<label for="txtMemBirthday">会员生日:</label>
		<input type="text" id="txtMemBirthday" name="txtMemBirthday" value="<%=memModel.MemBirthday.ToString() == "1900-1-1 0:00:00" ? "" : memModel.MemBirthday.ToString("yyyy-MM-dd") %>" />
	</div>

    <div data-role="fieldcontain" style="margin:5px 15px;padding:5px 15px;text-align:center;">
       <input type="submit" value="保存" data-theme="a" data-icon="check" data-iconpos="left" data-inline="true" id="btnUpdateMemInfo"  />
	    <a data-inline="true" data-role="button" data-theme="c" data-icon="back" data-iconpos="left" href="MemberManipulate.aspx?MemWeiXinCard=<%=MemWeiXinCard %>&rc=<%=rc %>" id="goBack" rel="external">返回</a>
        <input type="hidden" id="txtMemWeiXinCard" value="<%=MemWeiXinCard %>" />
    </div>
    </div>

    <div data-role="page" id="dialog">
        <div>
		    <div data-role="header" data-theme="d">
			    <h1>消息提示</h1>
		    </div>
		    <div data-role="content" data-theme="c">
			    <p id="newsInfo" style="text-indent:2em"></p>
			    <a href="" data-role="button" data-rel="back" data-theme="c">确定</a>
		    </div>
	    </div>
    </div>
</body>
</html>
