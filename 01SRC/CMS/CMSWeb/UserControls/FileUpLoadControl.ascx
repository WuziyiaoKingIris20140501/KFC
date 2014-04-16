<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileUpLoadControl.ascx.cs" Inherits="FileUpLoadControl" %>
<asp:HiddenField ID="allFileSize" runat="server" Value="0" />
<div id="messageContent" runat="server" style="color:red;width:800px;"></div>
  <table>
    <tr>
      <td align="right">
        本地文件：
      </td>
      <td>
        <asp:FileUpload ID="FileUpload" runat="server" Width="500px" />
        <asp:Button ID="btnAdd" runat="server" Text="添加文件" OnClick="btnAdd_Click" />
      </td>
    </tr>
    <tr style="vertical-align:top">
      <td align="right">
        文件列表：
      </td>
      <td>
        <asp:ListBox ID="lbxFile" runat="server" Height="145px" Width="245px" CssClass="txt">
        </asp:ListBox>
        <img alt="" src="" style="width: 200px; height: 200px; display: none;float:right" id="preViewSimple" runat="server" />
      </td>
    </tr>
    <tr>
      <td align="right">
        预览：
      </td>
      <td id="preViewList" runat="server">
      </td>
    </tr>
    <tr>
      <td colspan="2" style="text-align: center">
        <asp:Button ID="btnDelete" runat="server" Text="删除文件" OnClick="btnDelete_Click" />&nbsp;&nbsp;
        <asp:Button ID="btnPost" runat="server" Text="完成上传" OnClick="btnPost_Click" />
      </td>
    </tr>
  </table>