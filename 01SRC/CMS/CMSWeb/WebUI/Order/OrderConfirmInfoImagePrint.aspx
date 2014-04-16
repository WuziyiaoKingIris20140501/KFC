<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderConfirmInfoImagePrint.aspx.cs" Inherits="OrderConfirmInfoImagePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
     <meta http-equiv="content-type" content="text/html;charset=utf-8" />
</head>
<script language="javascript" type="text/javascript">
    function SetImagePreview(arg) {
        var output = "";
        if (arg.indexOf(",") != -1) {
            var arrmp = arg.split(",");
            for (i = 0; i < arrmp.length; i++) {
                if (arrmp[i] != "") {
                    output += "<img src='" + arrmp[i] + "' alt=\"\" style=\"width:100%;height:920px;\"/><br/>";
                }
            }
        }
        else {
            output += "<img src='" + arg + "' alt=\"\" style=\"width:100%;height:920px;\"/>";
        }

        document.getElementById("<%=dvfaxImage.ClientID%>").innerHTML = output;
        window.print();
    }
</script>
<body>
    <form id="form1" runat="server" style="width:112%; height:100%;">
        <div runat="server" id="dvfaxImage" style="width:112%; height:100%;">
        </div>
    </form>
</body>
</html>
