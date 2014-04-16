<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FacilitiesTypeDetail.aspx.cs"  Title="服务设施类别管理明细" Inherits="FacilitiesTypeDetail" %>
<%@ Register Assembly="DateTextBox" Namespace="Titan.WebForm" TagPrefix="TW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">

<head id="Head1" runat="server">
    <title>CMS酒店管理系统</title>
    <base target=_self>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=8,chrome=1">
    <link href="../../Styles/Css.css" type="text/css" rel="Stylesheet" />
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
        <li class="title">服务设施类别明细</li>
        <li>
            <table>
                <tr>
                    <td>服务设施类别名称：</td>
                    <td><input name="textfield" type="text" id="txtFtName" value="" runat="server" maxlength="12"/><font color="red">*</font></td>
                </tr>
                <tr>
                    <td>服务设施类别代码：</td>
                    <td><input name="textfield" type="text" id="txtFtCode" value="" runat="server" maxlength="12" readonly="readonly"/></td>
                </tr>
                <tr>
                    <td>排序：</td>
                    <td><asp:Label ID="lbFtSeq" runat="server" Text="" /></td>
                </tr>
                <tr>
                    <td>状态：</td>
                    <td><asp:DropDownList ID="ddpStatusList" CssClass="noborder_inactive" runat="server"/></td>
                </tr>
            </table>
        </li>
        <li>
            <div id="detailMessageContent" runat="server" style="color:red"></div>
        </li>
        <li class="button">&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" Text="修改" onclick="btnUpdate_Click" />
            <input type="button" value="取消" class="btn" onclick="window.returnValue=false;window.close();"/>
        </li>
      </ul>
    </div>
   </div>
<asp:HiddenField ID="hidFtID" runat="server"/>
</form>
</body>
</html>