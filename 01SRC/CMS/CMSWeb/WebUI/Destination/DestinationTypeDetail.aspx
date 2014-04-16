<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DestinationTypeDetail.aspx.cs"  Title="目的地类型管理明细" Inherits="DestinationTypeDetail" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">

<head id="Head1" runat="server">
    <title>CMS酒店管理系统</title>
    <base target=_self>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=8,chrome=1">
    <link href="../../Styles/public.css" type="text/css" rel="Stylesheet" />
    <link href="../../Styles/Sites.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">

    </script>
</head>
<body>
<form id="form1" runat="server">
  <div id="right">
    <div class="frame01">
    <ul>
        <li class="title">目的地类型管理</li>
     </ul>
        <table>
            <tr>
                <td>
                     目的的类型名称：
           
                </td>
                <td>
                     <input name="textfield" type="text" id="txtDestinationName" value="" runat="server" maxlength="12"/>
                </td>
            </tr>
            <tr>
                <td>
                    上级类型：
                </td>
                <td>
                     <asp:DropDownList ID="ddpTypeList" CssClass="noborder_inactive" runat="server"></asp:DropDownList> 
                </td>
            </tr>
            <tr>
                <td>
                    状态：
                </td>
                <td>
                    <asp:DropDownList ID="ddpStatusList" CssClass="noborder_inactive" runat="server"></asp:DropDownList> 
                </td>
            </tr>
        </table>      
        <li><div id="detailMessageContent" runat="server" style="color:red"></div></li>
        <li class="button">&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" Text="修改" onclick="btnUpdate_Click" />
            
            <%--<input type="button" value="修改" runat"server" style="width:80px;height:20px" onclick="btnSave_Click();window.returnValue=true;window.close();"/>--%>
            <input type="button" value="取消" class="btn" onclick="window.returnValue=false;window.close();"/>
        </li>
     
    </div>
   </div>
<asp:HiddenField ID="hidDestinationID" runat="server"/>
</form>
</body>
</html>