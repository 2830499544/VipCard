<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="WeiXinConvertHistory.aspx.cs" Inherits="ChainStock.WeiXin.WeiXinConvertHistory" %>
<!DOCTYPE html>
<html>
<head>
    <title>礼品兑换记录</title>
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
            if ($("#newsInfo").text().length > 0) {
                $("#showDialog").click();
            }

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
                "Method": "GetConvertHistory",
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
                    child += '<a href="WeiXinGiftExchangeDetail.aspx?MemWeiXinCard=' + $("#MemWeiXinCard").val() + '&ExchangeID=' + item.ExchangeID + '" data-role="button" data-icon="star" data-iconpos="right" data-rel="dialog" data-transition="slidedown" data-inline="true" data-theme="c">';
                    child += '<p>兑换单号：' + item.ExchangeAccount + '</p>';
                    child += '<p>兑换数量：' + parseFloat(item.ExchangeAllNumber).toFixed(0) + '</p>';
                    child += '<p>消费积分：' + parseFloat(item.ExchangeAllPoint).toFixed(0) + '</p>';
                    child += '<p style="color:Red">申请状态：' + (item.ExchangeStatus == 1 ? "申请审核中……" : item.ExchangeStatus == 2 ? "申请已通过审核" : "兑换申请失败") + '</p>';
                    child += '<p>审核备注：' + (item.ExchangeRemark == "" ? "无" : item.ExchangeRemark) + '</p>';
                    child += '<p>兑换时间：' + item.ApplicationTime.split(" ")[0] + '</p>';
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
        <asp:Repeater ID="Rpt_GiftExchange" runat="server">
            <HeaderTemplate><ul data-role="listview" id="listview"></HeaderTemplate>
            <ItemTemplate>
                <li>
                    <a href="<%#string.Format("WeiXinGiftExchangeDetail.aspx?MemWeiXinCard={0}&ExchangeID={1}",MemWeiXinCard,Eval("ExchangeID")) %>" data-role="button" data-icon="star" data-iconpos="right" data-rel="dialog" data-transition="slidedown" data-inline="true" data-theme="c">
                        <p>兑换单号：<%#Eval("ExchangeAccount")%></p>
                        <p>兑换数量：<%#Eval("ExchangeAllNumber")%></p>
                        <p>消费积分：<%#Eval("ExchangeAllPoint")%></p>
                        <p style="color:Red">申请状态：<%# Convert.ToInt32(Eval("ExchangeStatus").ToString()) == 1 ? "申请审核中……" : Convert.ToInt32(Eval("ExchangeStatus").ToString()) == 2 ? "申请已通过审核" : "兑换申请失败"%></p>
                        <p>审核备注：<%#Eval("ExchangeRemark").ToString()==""?"无":Eval("ExchangeRemark")%></p>
                        <p>兑换时间：<%#Eval("ApplicationTime", "{0:yyyy-MM-dd}")%></p>
                    </a>
                </li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
        <div style="text-align:center;display:none;" id="loadingImg">
            <img src="../images/ico/download.gif" />
        </div>
        <div id="noProduct" runat="server" style="text-align:center;display:none;"><h3>暂无礼品兑换记录！</h3></div>
        <a data-role="button" data-theme="c" href="MemberManipulate.aspx?MemWeiXinCard=<%=MemWeiXinCard %>" rel="external">返回</a>
        <input type="hidden" value="<%=MemWeiXinCard %>" id="MemWeiXinCard" />
    </div>
</body>
</html>
