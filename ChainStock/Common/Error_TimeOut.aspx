<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error_TimeOut.aspx.cs"
    Inherits="ChainStock.Common.Error_TimeOut" %>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Inc/Style/style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-common.js" type="text/javascript"></script> 
    
        <script>
            var time = 3;
            function Jump() {
                if (time == 0) {
                        top.location.href = "../index.aspx";
                }
                else
                    setTimeout("Jump()", 1000);
                $("#Jump_Time").html(time);
                time--;
            }
            Jump();
        </script>
</head>
<body style="background-color:#D9F3FF">
    <form id="form1" runat="server">
     <div id="bg"> 
        <table style="width: 487px; margin: auto; padding-top: 160px">           
            <tr>
                <td style="background-image: url(../images/main/timeout.gif); width: 487px; height: 157px; text-align: right">
                    <table style="margin-top: 5px; margin-left: 140px">
                        <tr>
                            <td style="height: 40px;text-align:left">
                                <asp:Literal ID="ErrInfo" runat="server"></asp:Literal>
                            </td> 
                        </tr> 
                        <tr>
                            <td style="height: 40px; text-align: left">
                               系统将自动跳转到相关页面 <span id="Jump_Time" style="color:Red">3</span> 秒后自动跳转……
                            </td>
                        </tr> 
                    </table>
                    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
