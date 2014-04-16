<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DestinationManagerDetail.aspx.cs"  Title="目的地管理明细" Inherits="DestinationManagerDetail" %>
<%@ Register src="../../UserControls/WebAutoComplete.ascx" tagname="WebAutoComplete" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">

<head id="Head1" runat="server">
    <title>CMS酒店管理系统</title>
    <base target=_self>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=8,chrome=1">
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/jquery-1.7.1.min.js")%>'></script>
    <script type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/jquery-ui-1.8.18.custom.min.js")%>'></script>
    <script language="javascript" type="text/javascript" src='<%=ResolveClientUrl("~/Scripts/jquery.autocomplete.js")%>'></script>

    <link href="../../Styles/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" href="../../Styles/jquery.autocomplete.css" />
    <link href="../../Styles/public.css" type="text/css" rel="Stylesheet" />
    <link href="../../Styles/Sites.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">
    function SetControlValue() {
        document.getElementById("<%=hidCity.ClientID%>").value = document.getElementById("wctCity").value;
    }
    function PageClosed() {
        window.returnValue = true;
        window.opener = null;
        window.close();
    }
    function InitialValue(arg) {
        document.getElementById("wctCity").value = arg;
        document.getElementById("wctCity").text = arg;
    } 
    </script>
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeOut="36000" runat="server" ></asp:ScriptManager>
  <div id="right">
    <div class="frame01">
      <ul>
        <li class="title">目的地管理</li>
        <li>
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <table>
             <tr>
                <td align="right">
                    目的地名称：
                </td>
                <td>
                    <input name="textfield" type="text" id="txtDestinationName" value="" style="width:210px;" runat="server" maxlength="12"/>
                </td>
                <td align="right">
                    类型：
                </td>
                <td>
                    <asp:DropDownList ID="ddpTypeList" CssClass="noborder_inactive" runat="server"></asp:DropDownList> 
                </td>
            </tr>
             <tr>
                <td align="right">
                    地址：
                </td>
                <td>
                    <input name="textfield" type="text" id="txtAddress" value="" style="width:210px;" runat="server" maxlength="32"/>
                </td>
                <td align="right">
                    电话：
                </td>
                <td>
                    <input name="textfield" type="text" id="txtTelST" value="" style="width:40px;" runat="server" maxlength="4"/>
                    <input name="textfield" type="text" id="txtTelLG" value="" style="width:150px;" runat="server" maxlength="12"/>
                </td>
            </tr>
             <tr>
                <td align="right">
                    经度：
                </td>
                <td>
                    <input name="textfield" type="text" id="txtLongitude" value="" style="width:150px;" runat="server" maxlength="12"/>
                </td>
                <td align="right">
                    纬度：
                </td>
                <td>
                    <input name="textfield" type="text" id="txtLatitude" value="" style="width:150px;" runat="server" maxlength="12"/>
                </td>
            </tr>
             <tr>
                <td align="right">
                     状态：
                </td>
                <td>
                    <asp:DropDownList ID="ddpStatusList" CssClass="noborder_inactive" runat="server"></asp:DropDownList>
                </td>
                <td align="right">
                    城市：<div style="display:none;"><asp:DropDownList ID="ddpCityList" CssClass="noborder_inactive" runat="server"></asp:DropDownList></div>
                </td>
                <td>
                    <uc1:WebAutoComplete ID="wctCity" CTLID="wctCity" runat="server" AutoType="city" AutoParent="DestinationManagerDetail.aspx?Type=city" />
                </td>
            </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
        </li>
        <li>
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
            <ContentTemplate>
            <div id="detailMessageContent" runat="server" style="color:red"></div>
            <asp:HiddenField ID="hidDestinationID" runat="server"/>
            <asp:HiddenField ID="hidCity" runat="server"/>
            </ContentTemplate>
            </asp:UpdatePanel>
        </li>
        <li class="button">
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" Text="修改" OnClientClick="SetControlValue();" onclick="btnUpdate_Click" />
            <input type="button" value="取消" class="btn" onclick="window.returnValue=false;window.close();"/>
            </ContentTemplate>
            </asp:UpdatePanel>
        </li>
      </ul>
    </div>
   </div>
</form>
</body>
</html>