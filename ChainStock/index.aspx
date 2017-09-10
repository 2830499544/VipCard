<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ChainStock.index" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>商家联盟系统</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <!-- Mobile specific metas -->
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <meta name="author" content="SuggeElson" />
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta name="application-name" content="sprFlat admin template" />
    <link href="assets/css/icons.css" rel="stylesheet" />
    <link href="assets/css/sprflat-theme/jquery.ui.all.css" rel="stylesheet" />
    <link href="assets/css/bootstrap.css" rel="stylesheet" />
    <link href="assets/css/plugins.css" rel="stylesheet" />
    <link href="assets/css/main.css" rel="stylesheet" />
    <link href="assets/css/custom.css" rel="stylesheet" />
    <link rel="apple-touch-icon-precomposed" sizes="144x144" href="assets/img/ico/apple-touch-icon-144-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="114x114" href="assets/img/ico/apple-touch-icon-114-precomposed.png">
    <link rel="apple-touch-icon-precomposed" sizes="72x72" href="assets/img/ico/apple-touch-icon-72-precomposed.png">
    <link rel="apple-touch-icon-precomposed" href="assets/img/ico/apple-touch-icon-57-precomposed.png">
    <link rel="icon" href="assets/img/ico/favicon.ico" type="image/png">
    <link rel="stylesheet" type="text/css" href="css/common.css">
    <link rel="stylesheet" type="text/css" href="css/style.css">
    <meta name="msapplication-TileColor" content="#3399cc" />


</head>
<body>

    <script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
    <script src="Scripts/Module/System/index.js" type="text/javascript"></script>


    <div class="section" id="container">

        <div id="header">
            <div class="container-fluid">
                <div class="navbar">
                    <div class="navbar-header">
                        <a class="navbar-brand" href="index.aspx">
                            <img src="images/logo.png" alt="商家联盟系统" />
                        </a>
                    </div>
                    <nav class="top-nav" role="navigation">
                        <ul class="nav navbar-nav pull-left">
                            <li id="toggle-sidebar-li">
                                <a href="index.aspx" title="主页" id="toggle-sidebar"><i class="en-arrow-left2"></i>
                                </a>
                            </li>
                            <li>
                                <a href="Member/MemList.aspx" title="会员列表" target="main" class="full-screen"><i class="fa-fullscreen"></i></a>
                            </li>
                            <li class="dropdown">
                                <a href="StockManage/GoodsExpense.aspx" title="商品消费" data-toggle="dropdown" target="main"><i class="ec-cog"></i></a>
                            </li>
                            <li class="dropdown">
                                <a href="MemExpense/ConsumeMemCount.aspx" title="计次消费" data-toggle="dropdown" target="main"><i class="ec-mail"></i></a>

                            </li>
                        </ul>
                        <ul class="nav navbar-nav pull-right">
                            <li>
                                <a href="#" title="退出" onclick="LoginOut()" id="toggle-header-area"><i class="ec-download"></i></a>
                            </li>
                            <li class="dropdown">
                                <a href="Common/TodayRemind.aspx" title="提醒" target="main" data-toggle="dropdown"><i class="br-alarm"></i><span class="notification" id="spRemindCount" runat="server">5</span></a>

                            </li>
                            <li class="dropdown">
                                <div class="f-left admin-img">
                                    <img src="images/head.png" id="spShopPhoto" runat="server" /></div>
                                <a href="##" class="admin-name" id="spUserName" runat="server">2<span></span></a>

                            </li>
                            <li id="toggle-right-sidebar-li"><a href="MicroWebsite/Opinion.aspx" title="留言" target="main" id="toggle-right-sidebar"><i class="ec-users"></i><span class="notification" style="background-color: #ff6c00;" id="spMessageCount" runat="server">0</span></a>
                            </li>
                        </ul>
                    </nav>
                </div>
            </div>
        </div>

        <div id="aside" class="f-left">
            <div id="sidebar">
                <div class="sidebar-inner">
                    <ul class="section aside-nav">
                        <asp:Repeater ID="rptFirstMenu" runat="server" OnItemDataBound="rptFirstMenu_ItemDataBound">
                            <ItemTemplate>


                                <li class="section">
                                    <div class="section main-nav">
                                        <i>
                                            <img src='<%#Eval("ModuleIcoPath") %>' /></i>
                                        <p><%#Eval("ModuleCaption") %></p>
                                        <span>
                                            <img src="images/icon (3).png" /></span>
                                    </div>
                                    <div class="section min-nav">
                                        <asp:Repeater ID="rptSecondMenu" runat="server">
                                            <ItemTemplate>
                                                <a href='<%#Eval("ModuleLink") %>' class="" target="main"><%#Eval("ModuleCaption") %></a>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>



                </div>
            </div>
        </div>

        <script type="text/javascript" src="scripts/script.js"></script>
        <div id="content" class="section f-left">
            <iframe class="s_iframe" name="main" id="main" width="100%" height="100%" src="FirstPage.aspx" frameborder="0" data-id="FirstPage.aspx" scrolling="yes">
        </div>
    </div>
</body>
</html>
