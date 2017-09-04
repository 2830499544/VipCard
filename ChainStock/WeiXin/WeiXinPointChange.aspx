<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="WeiXinPointChange.aspx.cs" Inherits="ChainStock.WeiXin.WeiXinPointChange" %>
<!DOCTYPE html>

<html>
<head>
    <title>积分变动</title>
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
                "Method": "GetPointChange",
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
                    child += '<a href="MemberManipulate.aspx?MemWeiXinCard=' + $("#MemWeiXinCard").val() + '&rc=' + $("#rc").val() + '" data-role="button" data-icon="back" data-iconpos="right"> data-theme="c" rel="external">';

                    child += '<p>积分变动：' + parseFloat(item.PointNumber).toFixed(0) + '</p>';
                    child += '<p>变动详情：' + item.PointRemark + '</p>';
                    child += '<p>变动日期：' + item.PointCreateTime.split(" ")[0] + '</p>';

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
        <asp:Repeater ID="Rpt_WeiXinPointChange" runat="server">
            <HeaderTemplate><ul data-role="listview" id="listview"></HeaderTemplate>
            <ItemTemplate>
                <li>
                    <a href="MemberManipulate.aspx?MemWeiXinCard=<%=MemWeiXinCard %>&rc=<%=rc %>" data-role="button" data-icon="back" data-rel="back" data-iconpos="right" data-theme="c" rel="external">
                        <p>积分变动：<%#Eval("PointNumber", "{0:F2}")%></p>
                        <p>变动详情：<%#Eval("PointRemark")%></p>
                        <p>变动日期：<%#Eval("PointCreateTime", "{0:yyyy-MM-dd}")%></p>
                    </a>
                </li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
         </asp:Repeater>
         <div id="noProduct" runat="server" style="text-align:center;display:none;"><h3>暂无积分变动记录！</h3></div>
        <a data-role="button" data-theme="c" href="MemberManipulate.aspx?MemWeiXinCard=<%=MemWeiXinCard %>&rc=<%=rc %>" rel="external">返回</a>
         <input type="hidden" value="<%=MemWeiXinCard %>" id="MemWeiXinCard" />
         <input type="hidden" value="<%=rc %>" id="rc" />
    </div>
</body>
</html>
