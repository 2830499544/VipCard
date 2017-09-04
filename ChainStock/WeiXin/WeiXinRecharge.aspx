<%@ Page Language="C#" AutoEventWireup="true"  EnableViewState="false" CodeBehind="WeiXinRecharge.aspx.cs" Inherits="ChainStock.WeiXin.WeiXinRecharge" %>
<!DOCTYPE html>

<html>
<head>
    <title>充值记录</title>
   <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <link href="../Inc/Style/jquery.mobile-1.2.0.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Module/WeiXin/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/WeiXin/jquery.mobile-1.2.0.min.js" type="text/javascript"></script>
	<style type="text/css">
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
                "Method": "GetRecharge",
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
                    child += '<p>充值类型：' + (item.RechargeType == "2" ? "现金充值" : (item.RechargeType == "3" ? "银联充值" : "未知类型")) + '</p>';
                    child += '<p>充值单号：' + item.RechargeAccount + '</p>';
                    child += '<p>充值总额：' + parseFloat(item.RechargeMoney).toFixed(2) + '</p>';
                    child += '<p>充值金额：' + parseFloat(item.RechargeOrdMoney).toFixed(2) + '</p>';
                    child += '<p>赠送金额：' + parseFloat(item.RechargeGive).toFixed(2) + '</p>';
                    child += '<p>充值时间：' + item.RechargeCreateTime.split(" ")[0] + '</p>';
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
        <asp:Repeater ID="rpt_Recharge" runat="server">
            <HeaderTemplate><ul data-role="listview" id="listview"></HeaderTemplate>
            <ItemTemplate>
                <li>
                    <a href="MemberManipulate.aspx?MemWeiXinCard=<%=MemWeiXinCard %>&rc=<%=rc %>" data-role="button" data-icon="back" data-iconpos="right" data-theme="c" rel="external">
                        <p>充值类型：<%#Eval("RechargeType").ToString()=="2"?"现金充值":Eval("RechargeType").ToString()=="3"?"银联充值":"未知类型"%></p>
                        <p>充值单号：<%#Eval("RechargeAccount")%></p>
                        <p>充值总额：<%#Eval("RechargeMoney", "{0:F2}")%></p>
                        <p>充值金额：<%#Eval("RechargeOrdMoney", "{0:F2}")%></p>
                        <p>赠送金额：<%#Eval("RechargeGive", "{0:F2}")%></p>
                        <p>充值时间：<%#Eval("RechargeCreateTime", "{0:yyyy-MM-dd}")%></p>
                    </a>
                </li>
            </ItemTemplate>
            <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
        <div style="text-align:center;display:none;" id="loadingImg">
            <img src="../images/ico/download.gif" />
        </div>
        <div id="noProduct" runat="server" style="text-align:center;display:none;"><h3>暂无充值记录！</h3></div>
        <a data-role="button" data-theme="c" href="MemberManipulate.aspx?MemWeiXinCard=<%=MemWeiXinCard %>&rc=<%=rc %>" rel="external">返回</a>
        <input type="hidden" value="<%=MemWeiXinCard %>" id="MemWeiXinCard" />
        <input type="hidden" value="<%=rc %>" id="rc" />
    </div>
</body>
</html>
