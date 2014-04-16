<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CityDetail.aspx.cs"  Title="城市管理明细" Inherits="CityDetail" %>
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
        <li class="title">LM城市明细</li>
        <li>
            <table>
                <tr>
                    <td>LM城市ID：</td>
                    <td><input name="textfield" type="text" id="txtCityID" value="" runat="server" 
                            readonly="readonly"/></td>
                </tr>
                <tr>
                    <td>艺龙城市ID:</td>
                    <td><input name="textfield0" type="text" id="txtELCityID" value="" runat="server" 
                           /></td>
                </tr>
                <tr>
                    <td>城市名称：</td>
                    <td><input name="textfield" type="text" id="txtCityNM" value="" runat="server" maxlength="30" style="width:300px"/><font color="red">*</font></td>
                </tr>
                <tr>
                    <td>城市排序：</td>
                    <td><input name="textfield" type="text" id="txtSEQ" value="" runat="server" maxlength="3" style="width:300px" readonly="readonly"/><font color="red">*</font></td>
                </tr>
                <tr>
                    <td>城市拼音：</td>
                    <td><input name="textfield" type="text" id="txtPinyin" value="" runat="server" maxlength="40" style="width:300px"/><font color="red">*</font></td>
                </tr>
                <tr>
                    <td>城市拼音（缩写）：</td>
                    <td><input name="textfield" type="text" id="txtPinyin_Short" value="" runat="server" maxlength="40" style="width:300px"/><font color="red">*</font></td>
                </tr>
                <tr>
                    <td>城市经度：</td>
                    <td><input name="textfield" type="text" id="txtLongitude" value="" runat="server" maxlength="12" style="width:300px"/><font color="red">*</font></td>
                </tr>
                <tr>
                    <td>城市纬度：</td>
                    <td><input name="textfield" type="text" id="txtLatitude" value="" runat="server" maxlength="12" style="width:300px"/><font color="red">*</font></td>
                </tr>
                <tr>
                    <td>城市类型：</td>
                    <td>
                        <asp:CheckBox ID="ckLm" runat="server" Text="LM" />
                        <asp:CheckBox ID="ckHubs1" runat="server" Text="hubs1" />
                        <asp:CheckBox ID="ckYL" runat="server" Text="艺龙" />
                        <asp:CheckBox ID="ckXC" runat="server" Text="携程" />
                    </td>
                </tr>
                 <tr>
                    <td>热门城市：</td>
                    <td><%--<asp:DropDownList ID="ddpIsHot" CssClass="noborder_inactive" runat="server">
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:DropDownList>--%>
                        <asp:CheckBox ID="ckLmHotCity" runat="server" Text="LM热门城市" />
                        <asp:CheckBox ID="ckHubs1HotCity" runat="server" Text="hubs1热门城市" />
                        <asp:CheckBox ID="ckYLHotCity" runat="server" Text="艺龙热门城市" />
                        <asp:CheckBox ID="ckXCHotCity" runat="server" Text="携程热门城市" />
                    </td>
                </tr>
                <tr>
                    <td>销售开始时间：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="marketData">
                            <asp:ListItem Value="111100000011111111111111">10点</asp:ListItem>
                            <asp:ListItem Value="111100000000111111111111">12点</asp:ListItem>
                            <asp:ListItem Value="111100000000001111111111">14点</asp:ListItem>
                            <asp:ListItem Value="111100000000000011111111">16点</asp:ListItem>
                            <asp:ListItem Value="111100000000000000111111">18点</asp:ListItem>
                        </asp:DropDownList>

                   </td>
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
        <li class="button">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnFogRead" runat="server" CssClass="btn primary" Text="读取Fog信息" onclick="btnFogRead_Click" />
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" Text="保存" onclick="btnUpdate_Click" />
            <%--<asp:Button ID="btnDelete" runat="server" Width="80px" Height="20px" Text="删除" onclick="btnDelete_Click" />--%>
            <input type="button" value="取消" class="btn" onclick="window.returnValue=false;window.close();"/>
        </li>
      </ul>
    </div>
   </div>
<asp:HiddenField ID="hidCityID" runat="server"/>
</form>
</body>
</html>