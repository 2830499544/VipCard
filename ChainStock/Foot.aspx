<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Foot.aspx.cs" Inherits="ChainStock.Foot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=edge">
<style>
    
.foot {height: 30px;width: 100%; background-color:#27ABFA; border: #1280ba 1px solid;color: #002c4a;text-align: center;font: 10px;font:12px/1.2 "Microsoft YaHei"; line-height: 30px;}
</style>
 
 <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-common.js" type="text/javascript"></script>
   <script language="JavaScript">

       setTimeout('myrefresh()', 5000); //指定1秒刷新一次



       function myrefresh() {

           doAjax("../", "GetNewMessageCount", {}, "json",
        function (json) {
     
            $("#spMessageCount").html(json);
        });
           setTimeout('myrefresh()', 5000); //指定1秒刷新一次
       }


 
    </script>

</head>


<body>
    <form id="form1" runat="server" class="foot">
    <div class="newmessage" style=" text-align:right;padding-right:20px;font-size:9pt;  font-family:微软雅黑;   font-weight:bold;" >
    
  
        
     <a href="MicroWebsite/Opinion.aspx"  target="mainFrame" style="  color:#ffd900; text-decoration:none;"> <span  id="divMessage" runat="server"  style=" cursor: pointer; "> <img src="images/Gift/newmessage.gif" />留言(<span style=" font-size:10pt;" id="spMessageCount" runat="server">0</span>)</span>
    </a>
        </div>
    <div id="spFoot" runat="server" class="foot">
   
      
        <span>&copy;</span>版权所有：<a href="http://www.zhiluo.net/" target="_blank">深圳市可百科技有限公司</a>
    </div>
    </form>
</body>
</html>
