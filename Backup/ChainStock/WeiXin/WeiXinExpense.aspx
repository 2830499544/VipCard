<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="WeiXinExpense.aspx.cs" Inherits="ChainStock.WeiXin.WeiXinExpense" %>
<!DOCTYPE html>
<html>
<head>
    <title>消费记录</title>
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <link href="../Inc/Style/jquery.mobile-1.2.0.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Module/WeiXin/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/WeiXin/jquery.mobile-1.2.0.min.js" type="text/javascript"></script>
	<style>
	    p{text-align:left;}
	</style>
    <script type="text/javascript">
        var pageIndex = 1; //页码
        var pageSize = 10; //页大小
        var loading = false; //当前是否处于加载状态
        var isData = true; //是否还有数据可供加载
        $(function () {

            $("body").bind("scrollstop", function () {
                if (isData) {//有数据
                    if (!loading) {//处于未加载状态
                        var windowHeight = parseInt($(window).height());
                        var scrollHeight = parseInt($(window).scrollTop());
                        var documentHeight = parseInt($(document).height());
                        if (documentHeight - (windowHeight + scrollHeight) <= 100) { //是否达到可加载的条件
                            load();
                        }
                    }
                }
            })
        })

        function load() {
            loading = true;
            $("#loadingImg").css("display", "");
            pageIndex = pageIndex + 1;
            $.post("../Service/WeiXinService.ashx", {
                "Method": "GetExpense",
                "pageIndex": pageIndex,
                "pageSize": pageSize,
                "MemWeiXinCard": $("#MemWeiXinCard").val()
            }, function (e) {
                addChilds(e);
            }, "json")
        }

        function addChilds(e) {
            if (e.length > 0) {
                $.each(e, function (index, item) {
                    var child = '';
                    child += '<li>';
                    child += '<a href="MemberManipulate.aspx?MemWeiXinCard=' + $("#MemWeiXinCard").val() + '&rc=' + $("#rc").val() + '" data-role="button" data-icon="back" data-iconpos="right" data-theme="c" rel="external">';
                    child += '<p>订单编号：' + item.OrderAccount + '</p>';
                    child += '<p>消费金额：' + parseFloat(item.OrderTotalMoney).toFixed(2) + '</p>';
                    child += '<p>实际金额：' + parseFloat(item.OrderDiscountMoney).toFixed(2) + '</p>';
                    child += '<p>优惠券额：' + parseFloat(item.OrderPayCoupon).toFixed(2) + '</p>';
                    child += '<p>赠送积分：' + parseFloat(item.OrderPoint).toFixed(0) + '</p>';
                    child += '<p>消费时间：' + item.OrderCreateTime.split(" ")[0] + '</p>';
                    child += '</a>';
                    child += '</li>';
                    $("#listview").append(child).listview("refresh").trigger('create');
                })
            }
            if (e.length < pageSize) { isData = false; }
            loading = false;
            $("#loadingImg").css("display", "none");
        }
    </script>
</head>
<body>

    <div data-role="page">
        <a href="#dialog" data-rel="dialog" id="showDialog" style="display:none;"></a>
        <asp:Repeater ID="Rpt_WeiXinExpense" runat="server">
            <HeaderTemplate><ul data-role="listview" id="listview"></HeaderTemplate>
            <ItemTemplate>
                <li>
                    <a href="MemberManipulate.aspx?MemWeiXinCard=<%=MemWeiXinCard %>&rc=<%=rc %>" data-role="button" data-icon="back" data-iconpos="right" data-theme="c" rel="external">
                        <p>订单编号：<%#Eval("OrderAccount")%></p>
                        <p>消费金额：<%#Eval("OrderTotalMoney","{0:F2}")%></p>
                        <p>实际金额：<%#Eval("OrderDiscountMoney", "{0:F2}")%></p>
                        <p>优惠券额：<%#Eval("OrderPayCoupon", "{0:F2}")%></p>
                        <p>赠送积分：<%#Eval("OrderPoint", "{0:F2}")%></p>
                        <p>消费时间：<%#Eval("OrderCreateTime", "{0:yyyy-MM-dd}")%></p>
                    </a>
                </li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
        <div style="text-align:center;display:none;" id="loadingImg">
            <img src="../images/ico/download.gif" />
        </div>
        <div id="noProduct" runat="server" style="text-align:center;display:none;"><h3>暂无消费记录！</h3></div>
        <a data-role="button" data-theme="c" href="MemberManipulate.aspx?MemWeiXinCard=<%=MemWeiXinCard %>&rc=<%=rc %>" rel="external">返回</a>
        <input type="hidden" value="<%=MemWeiXinCard %>" id="MemWeiXinCard" />
        <input type="hidden" value="<%=rc %>" id="rc" />
    </div>
</body>
</html>
