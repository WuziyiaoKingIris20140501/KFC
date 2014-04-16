<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HotelFacilitiesDetail.aspx.cs"  Title="酒店服务设施管理明细" Inherits="HotelFacilitiesDetail" %>
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
        <li class="title">酒店服务设施明细</li>
        <li>
            <table>
               <tr>
                <td>
                    ID：
                </td>
                <td>
                    <asp:Label ID="lbId" runat="server" Text="" />
                </td>
               </tr>
               <tr>
                <td>
                    <asp:Label ID="lbName" runat="server" Text="服务设施名称：" />
                </td>
                <td>
                    <input name="textfield" type="text" id="txtName" value="" runat="server" style="width:300px" maxlength="12"/>
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
               <tr>
                <td>
                    服务设施类别：
                </td>
                <td>
                    <asp:DropDownList ID="ddpFtTypeList" CssClass="noborder_inactive" 
                        runat="server" Width="120px" 
                        onselectedindexchanged="ddpFtTypeList_SelectedIndexChanged" AutoPostBack="true"/>
                </td>
               </tr>
               <tr>
                <td>
                    排序：
                </td>
                <td>
                    <asp:Label ID="lbFtSeq" runat="server" Text="" />
                </td>
               </tr>
            </table>
          </li>
          <li>
            <asp:Button ID="btnUpdate" runat="server" CssClass="btn primary" Text="修改" onclick="btnUpdate_Click" />
            &nbsp;<input type="button" value="取消" class="btn" onclick="window.returnValue=false;window.close();"/>
          </li>
         <li><div id="detailMessageContent" runat="server" style="color:red;width:800px;"></div></li>
         <li class="button"></li>
      </ul>
    </div>
   </div>
<asp:HiddenField ID="hiddenId" runat="server"/>
</form>
</body>
</html>