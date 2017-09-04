<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="ChainStock.mobile.member.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="scripts/jquery-2.1.4.min.js"></script>
    <script>

        function getPath(obj) {
            alert(obj);
            if (obj) {

                if (window.navigator.userAgent.indexOf("MSIE") >= 1) {
                    obj.select();

                    return document.selection.createRange().text;
                }

                else if (window.navigator.userAgent.indexOf("Firefox") >= 1) {
                    if (obj.files) {

                        return obj.files.item(0).getAsDataURL();
                    }
                    return obj.value;
                }
                return obj.value;
            }
        }
        function getPath1() {
            var url = getPath(this);
            alert(url);
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="file" name="选择文件" id="File1" runat="server" />
        <input id="Button1" type="button" value="button" onclick="getPath1(this);" />

    </div>
    </form>
</body>
</html>
