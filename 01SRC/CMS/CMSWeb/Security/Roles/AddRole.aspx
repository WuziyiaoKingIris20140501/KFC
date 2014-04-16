<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="AddRole.aspx.cs" Inherits="Security_Roles_AddRole" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>城市列表</title>    
     <link href="../../Styles/Sites.css" type="text/css" rel="Stylesheet" />
</head>   
<body>
     <form id="form1" runat="server">
        <table width="70%" align="center" id="Table1">
        <tr class="RowTitle"><td colspan=2  align="center"><asp:Literal Text="新增角色" ID="lbAdviceTitle" runat="server"></asp:Literal></td></tr>
             <tr><td colspan=2><div id="messageContent" runat="server" style="color:red"></div></td></tr>
            <tr>
                <td style="width: 20%;" class="tdCellBlue">角色名</td>
                <td style="height: 30px" class="tdcell">
                    <asp:TextBox ID="txtRoleName" runat="server" Width="98%" SkinID="txtchange"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan=2 align="center" >           
                    <asp:Button ID="btnSave" runat="server" CssClass="btn primary" Text="保存" onclick="btnSave_Click" />&nbsp; &nbsp;&nbsp;
                    <input type="button" class="btn" value="关闭" id="Return" name="Button2" onclick="javascript:window.close();" runat="server" /></td>
            </tr>
        </table>

        <script language="javascript" type="text/javascript">
     function validate() {
         var txtRoleName = document.getElementById("<%=this.txtRoleName.ClientID %>");
         var msg = "";
         if (txtRoleName.value.replace("'", "") == "") {
             msg += "RoleName";
             txtRoleName.className = "textAlarm";
         }
         document.getElementById("<%=this.txtRoleName.ClientID %>").value = txtRoleName.value.replace("'", "");
         if (msg == "") {
             return true;
         }
         else {
             msg = "Please input the content!:\n" + msg;
             alert(msg);
             return false;
         }
     }
    </script>

     </form>
</body>
</html>

