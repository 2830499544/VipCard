<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="WeiXinPointExchange.aspx.cs" Inherits="ChainStock.WeiXin.WeiXinPointExchange" %>
<!DOCTYPE html>
<html>
<head>
    <title>礼品兑换</title>
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <link href="../Inc/Style/jquery.mobile-1.2.0.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Module/WeiXin/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/WeiXin/jquery.mobile-1.2.0.min.js" type="text/javascript"></script>
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
                "Method": "GetListForPointGift",
                "pageIndex": pageIndex,
                "pageSize": pageSize
            }, function (e) {
                addChilds(e);
            }, "json")
        }

        function addChilds(e) {
            if (e.length > 0) {
                $.each(e, function (index, item) {
                    var child = '';
                    child += '<li>';
                    child += '<a href=WeiXinGiftShow.aspx?GiftID=' + item.GiftID + '&MemWeiXinCard=' + getCard() + ' data-theme="c" rel="external">';
                    child += '<img src="' + item.GiftPhoto + '" />';
                    child += '<h3>' + item.GiftName + '</h3>';
                    child += '<img src="../images/ico/point.png" /> ' + item.GiftExchangePoint + ' 积分';
                    child += '</a>';
                    child += '</li>';
                    $("#listview").append(child).listview("refresh").trigger('create');
                })
            }
            if (e.length < pageSize) { isData = false; }
            loading = false;
            $("#loadingImg").css("display", "none");
        }

        function getCard() {
            var result = /MemWeiXinCard=(\w+)/.test(window.location.href);
            return RegExp.$1;
        }
    </script>
</head>
<body>
<div data-role="page">
    <a href="#dialog" data-rel="dialog" id="showDialog" style="display:none;"></a>
    <asp:Repeater ID="Rpt_WeiXinPointExchange" runat="server" onitemdatabound="Rpt_WeiXinPointExchange_ItemDataBound">
        <HeaderTemplate><ul data-role="listview" id="listview"></HeaderTemplate>
        <ItemTemplate>
            <li>
                <a id="linkimg" href='<%#Eval("GiftID") %>' runat="server" data-theme="c" rel="external">
                    <img src='<%# this.GetPhoto(Eval("GiftPhoto").ToString()) %>' />
                    <h3><%#Eval("GiftName") %></h3>
                    <img src="../images/ico/point.png" /> <%#Eval("GiftExchangePoint") %> 积分
                </a>
            </li>
        </ItemTemplate>
        <FooterTemplate></ul></FooterTemplate>
     </asp:Repeater>
     <div style="text-align:center;display:none;" id="loadingImg">
        <img src="../images/ico/download.gif" />
    </div>
    <div id="noProduct" runat="server" style="text-align:center;display:none;"><h3>很抱歉，本店暂无礼品！</h3></div>
    <a data-role="button" data-theme="c" href="MemberManipulate.aspx?MemWeiXinCard=<%=MemWeiXinCard %>&rc=<%=rc %>" rel="external">返回</a>
</div>
</body>
</html>
