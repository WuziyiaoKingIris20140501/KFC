﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HotelGroupDetail.aspx.cs"  Title="酒店集团管理明细" Inherits="HotelGroupDetail" %>
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
        <li class="title"><asp:Label ID="lbHotelGroupTitle" runat="server" Text="" /></li>
        <li>
            <table>
                <tr>
                    <td>酒店集团名称：</td>
                    <td><input name="textfield" type="text" id="txtHotelGroupNM" value="" runat="server" maxlength="32" style="width:300px"/><font color="red">*</font></td>
                </tr>
                <tr>
                    <td>酒店集团代码：</td>
                    <td><input name="textfield" type="text" id="txtHotelGroupCode" value="" runat="server" maxlength="20" style="width:300px"/><font color="red">*</font></td>
                </tr>
                <tr>
                    <td>酒店集团类型：</td>
                    <td><asp:DropDownList ID="ddpGroupTypeList" CssClass="noborder_inactive" runat="server"/> </td>
                </tr>
                <tr>
                    <td valign="top">酒店集团描述：</td>
                    <td><asp:TextBox ID="txtDescribe" runat="server" TextMode="MultiLine" Width="500px" Height="200px"/><span style="color:#AAAAAA">&nbsp;最多200个中文字符</span></td>
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
        <li class="button">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" Text="保存" onclick="btnUpdate_Click" />
            <%--<asp:Button ID="btnDelete" runat="server" Width="80px" Height="20px" Text="删除" onclick="btnDelete_Click" />--%>
            <input type="button" value="取消" class="btn" onclick="window.returnValue=false;window.close();"/>
        </li>
      </ul>
    </div>
   </div>
<asp:HiddenField ID="hidGroupNo" runat="server"/>
<asp:HiddenField ID="hiddenType" runat="server"/>
</form>
</body>
</html>