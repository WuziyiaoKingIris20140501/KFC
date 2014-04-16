<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PromotionTypeDetail.aspx.cs"  Title="促销方式" Inherits="PromotionTypeDetail" %>
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
        <li class="title">促销信息明细</li>
        <li>
            <table>
                <tr>
                    <td>促销名称：</td>
                    <td><input name="textfield" type="text" id="txtPromotionTypeName" value="" runat="server" 
                            /></td>
                </tr>
                <tr>
                    <td>促销排序:</td>
                    <td><input name="textfield0" type="text" id="txtPromotionTypeSEQ" value="" runat="server" 
                           /></td>
                </tr>
                </table>
        </li>
        <li>
            <div id="detailMessageContent" runat="server" style="color:red"></div>
        </li>
        <li class="button">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" Text="保存" onclick="btnUpdate_Click" />
            <input type="button" value="取消" class="btn" onclick="window.returnValue=false;window.close();"/>
        </li>
      </ul>
    </div>
   </div>
<asp:HiddenField ID="hidCityID" runat="server"/>
</form>
</body>
</html>
