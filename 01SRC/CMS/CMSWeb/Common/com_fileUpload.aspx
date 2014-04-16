<%@ Page Language="C#" AutoEventWireup="true" CodeFile="com_fileUpload.aspx.cs" Inherits="com_fileUpload" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>文件上传</title> 
    <base target=_self>
    <link href="../Styles/Sites.css" type="text/css" rel="Stylesheet" />
    <script language="javascript" type="text/javascript">
        function SetFileUpload(img_photo, image_src) {
            var doc = window.dialogArguments;
            if (doc != null) {
                if (doc.callback != null) {
                    doc.callback(img_photo, image_src);
                }
            }
            else {
                if (window.opener.callback != null) {
                    window.opener.callback(img_photo, image_src);
                }
            }
        }

        function SetFileMulUpload(img_photo) {
            var doc = window.dialogArguments;
            if (doc != null) {
                if (doc.callback != null) {
                    doc.callback(img_photo);
                }
            }

            else {
                if (window.opener.callback != null) {
                    window.opener.callback(img_photo);
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="allFileSize" runat="server" Value="0" />
    <div id="messageContent" runat="server" style="color:red;width:800px;"></div>
      <table>
        <tr>
          <td align="right">
            本地文件：
          </td>
          <td>
            <asp:FileUpload ID="FileUpload" runat="server" Width="500px" />
            <asp:Button ID="btnAdd" runat="server" CssClass="btn primary" Text="添加文件" OnClick="btnAdd_Click" />
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
        <tr id="displayPre" runat="server">
          <td align="right">
            预览：
          </td>
          <td id="preViewList" runat="server">
          </td>
        </tr>
        <tr>
          <td colspan="2" style="text-align: center">
            <asp:Button ID="btnDelete" CssClass="btn primary" runat="server" Text="删除文件" OnClick="btnDelete_Click" />&nbsp;&nbsp;
            <asp:Button ID="btnPost" CssClass="btn primary" runat="server" Text="完成上传" OnClick="btnPost_Click" />
          </td>
        </tr>
      </table>
    </form>
</body>
</html>
