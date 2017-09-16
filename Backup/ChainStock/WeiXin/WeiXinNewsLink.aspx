<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeiXinNewsLink.aspx.cs" Inherits="ChainStock.WeiXin.WeiXinNewsLink" %>
<!DOCTYPE html>
<html>
<head>
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no">
    <link href="../Inc/Style/jquery.mobile-1.2.0.min.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Module/WeiXin/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../Scripts/Module/WeiXin/jquery.mobile-1.2.0.min.js" type="text/javascript"></script>
    <style type="text/css">
        .ui-mobile img {max-width: 100%;}
    </style>
</head>
<body>
    <div style="margin:10px;padding:0 10px;">
        <img src='<%=imgUrl %>' />
    </div>
    <div style="margin:10px;padding:0 10px;word-break:break-all;">
        <%=divMain%>
    </div>
</body>
</html>
